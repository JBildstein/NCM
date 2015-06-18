using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Collections.Generic;
using ColorManager.ICC;
using ColorManager.Conversion;

namespace ColorManager.ICC.Conversion
{
    public sealed unsafe class ConversionCreator_ICC : ConversionCreator
    {
        #region Variables

        private Type InType;
        private Type OutType;

        private ICCProfile Profile;

        private bool? IsInPCS
        {
            get { return IsPCS(InColor, OutColor.ICCProfile); }
        }
        private bool? IsOutPCS
        {
            get { return IsPCS(OutColor, InColor.ICCProfile); }
        }

        private bool IsInput;
        private int DataPos
        {
            get { return ICCData.Count; }
        }
        private List<double[]> ICCData = new List<double[]>();


        private ConvType ConversionType = ConvType.NotSet;

        private enum ConvType
        {
            /// <summary>
            /// Conversion type is not set => invalid
            /// </summary>
            NotSet,

            /// <summary>
            /// ICC conversion from data to PCS
            /// </summary>
            ICCToPCS,
            /// <summary>
            /// ICC conversion from PCS to data
            /// </summary>
            ICCToData,
            /// <summary>
            /// ICC conversion from data to data
            /// </summary>
            ICCDataToData,
            /// <summary>
            /// ICC conversion from PCS to PCS
            /// </summary>
            ICCPCSToPCS,
        }

        private enum ConvIntent
        {
            D0,
            D1,
            D2,
            D3,
            A0,
            A1,
            A2,
            ColorTRC,
            GrayTRC,
        }

        #endregion

        #region Init

        /// <summary>
        /// Creates a new instance of the <see cref="ConversionCreator_ICC"/> class
        /// </summary>
        /// <param name="CMIL">The ILGenerator for the conversion method</param>
        /// <param name="data">The conversion data</param>
        /// <param name="inColor">The input color</param>
        /// <param name="outColor">The output color</param>
        public ConversionCreator_ICC(ILGenerator CMIL, ConversionData data, Color inColor, Color outColor)
            : base(CMIL, data, inColor, outColor)
        {
            Init();
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ConversionCreator_ICC"/> class
        /// </summary>
        /// <param name="CMIL">The ILGenerator for the conversion method</param>
        /// <param name="data">The conversion data</param>
        /// <param name="inColor">The input color</param>
        /// <param name="outColor">The output color</param>
        /// <param name="isLast">States if the output color is the last color</param>
        public ConversionCreator_ICC(ILGenerator CMIL, ConversionData data, Color inColor, Color outColor, bool isLast)
            : base(CMIL, data, inColor, outColor, isLast)
        {
            Init();
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ConversionCreator_ICC"/> class
        /// </summary>
        /// <param name="parent">The parent <see cref="ConversionCreator"/></param>
        /// <param name="isLast">States if the output color is the last color</param>
        /// <param name="inColor">The input color</param>
        /// <param name="outColor">The output color</param>
        public ConversionCreator_ICC(ConversionCreator parent, Color inColor, Color outColor, bool isLast)
            : base(parent, inColor, outColor, isLast)
        {
            Init();
        }

        private void Init()
        {
            this.InType = InColor.GetType();
            this.OutType = OutColor.GetType();

            if (InColor.ICCProfile != null) { Profile = InColor.ICCProfile; IsInput = true; }
            if (OutColor.ICCProfile != null)
            {
                if (Profile != null && Profile != OutColor.ICCProfile) throw new ConversionSetupException();
                else if (Profile == null) { Profile = OutColor.ICCProfile; IsInput = false; }
            }
            if (Profile == null) throw new ConversionSetupException();

            CheckConversionType();
            CheckProfileClassValidity();
        }

        public override void SetConversionMethod()
        {
            switch (ConversionType)
            {
                case ConvType.ICCToPCS:
                    AdjustInputColor(Profile.DataColorspaceType);
                    ConvertICC_DataPCS();
                    AdjustOutputColor(Profile.PCSType);
                    break;

                case ConvType.ICCToData:
                    AdjustInputColor(Profile.PCSType);
                    ConvertICC_PCSData();
                    AdjustOutputColor(Profile.DataColorspaceType);
                    break;

                case ConvType.ICCDataToData:
                    AdjustInputColor(Profile.DataColorspaceType);
                    AdjustOutputColor(Profile.DataColorspaceType);
                    throw new NotImplementedException();

                case ConvType.ICCPCSToPCS:
                    AdjustInputColor(Profile.PCSType);
                    AdjustOutputColor(Profile.PCSType);
                    throw new NotImplementedException();

                default:
                    throw new ConversionSetupException("Not able to perform conversion");
            }

            if (ICCData != null && ICCData.Count > 0)
            {
                if (IsInput) Data.SetICCData(new ICCData(ICCData.ToArray()), null);
                else Data.SetICCData(null, new ICCData(ICCData.ToArray()));
            }
            if (IsLastG) CMIL.Emit(OpCodes.Ret);
        }

        #endregion

        #region Profile and Color Checks

        private void CheckConversionType()
        {
            switch (Profile.Class)
            {
                case ProfileClassName.InputDevice:
                case ProfileClassName.DisplayDevice:
                case ProfileClassName.OutputDevice:
                case ProfileClassName.ColorSpace:
                case ProfileClassName.NamedColor:
                    CheckDevicePCS();
                    break;

                case ProfileClassName.DeviceLink:
                    CheckDeviceLinkProfile();
                    break;

                case ProfileClassName.Abstract:
                    CheckAbstractProfile();
                    break;

                default:
                    throw new InvalidProfileClassException();
            }
        }

        private void CheckDeviceLinkProfile()
        {
            if (IsInPCS == false && IsOutPCS == false) ConversionType = ConvType.ICCDataToData;
        }

        private void CheckAbstractProfile()
        {
            if (IsInPCS == true && IsOutPCS == true) ConversionType = ConvType.ICCPCSToPCS;
        }

        private void CheckDevicePCS()
        {
            if (IsInPCS == false)
            {
                var spaceWP = OutColor.Space.ReferenceWhite.GetType();
                if (IsOutPCS == true && spaceWP == typeof(WhitepointD50)) ConversionType = ConvType.ICCToPCS;
            }
            else if (IsOutPCS == false)
            {
                var spaceWP = InColor.Space.ReferenceWhite.GetType();
                if (IsInPCS == true && spaceWP == typeof(WhitepointD50)) ConversionType = ConvType.ICCToData;
            }
        }

        private void CheckProfileClassValidity()
        {
            //Device Link needs both colors to have a profile and the profiles need to be the same
            var dl = ProfileClassName.DeviceLink;
            if ((InColor.ICCProfile != null && InColor.ICCProfile.Class == dl) || (OutColor.ICCProfile != null && OutColor.ICCProfile.Class == dl))
            {
                bool valid = true;
                if (InColor.ICCProfile == null || OutColor.ICCProfile == null) valid = false;
                else if (InColor.ICCProfile != OutColor.ICCProfile) valid = false;

                if (!valid) throw new ConversionSetupException("Profile type \"Device Link\" needs both colors to have the same ICC profile");
            }
        }

        #endregion

        #region Profile Conversion Type

        private bool IsNComponentLUT()
        {
            switch (Profile.Class)
            {
                case ProfileClassName.InputDevice:
                    return Profile.HasTag(TagSignature.AToB0);

                case ProfileClassName.DisplayDevice:
                    return Profile.HasTag(TagSignature.AToB0) && Profile.HasTag(TagSignature.BToA0);

                case ProfileClassName.OutputDevice:
                    if (Profile.DataColorspace == typeof(ColorX) && !Profile.HasTag(TagSignature.ColorantTable)) return false;
                    return Profile.HasTag(TagSignature.AToB0) && Profile.HasTag(TagSignature.BToA0)
                        && Profile.HasTag(TagSignature.AToB1) && Profile.HasTag(TagSignature.BToA1)
                        && Profile.HasTag(TagSignature.AToB2) && Profile.HasTag(TagSignature.BToA2)
                        && Profile.HasTag(TagSignature.Gamut);

                default:
                    return false;
            }
        }

        private bool IsMonochrome()
        {
            switch (Profile.Class)
            {
                case ProfileClassName.InputDevice:
                case ProfileClassName.DisplayDevice:
                case ProfileClassName.OutputDevice:
                    return Profile.HasTag(TagSignature.GrayTRC);

                default:
                    return false;
            }
        }

        private bool IsThreeComponentMatrix()
        {
            switch (Profile.Class)
            {
                case ProfileClassName.InputDevice:
                case ProfileClassName.DisplayDevice:
                    return Profile.HasTag(TagSignature.RedMatrixColumn)
                        && Profile.HasTag(TagSignature.GreenMatrixColumn)
                        && Profile.HasTag(TagSignature.BlueMatrixColumn)
                        && Profile.HasTag(TagSignature.RedTRC)
                        && Profile.HasTag(TagSignature.GreenTRC)
                        && Profile.HasTag(TagSignature.BlueTRC);

                default:
                    return false;
            }
        }

        #endregion

        //TODO: include chromatic adaption if CATag is existing
        //TODO: check for media white/black point tag and use it if existing

        #region Conversion

        #region PCS -> Data

        private void ConvertICC_PCSData()
        {
            var entries = Profile.GetConversionTag(false);
            if (entries == null || entries.Length < 1) throw new InvalidProfileException();

            if (IsNComponentLUT()) ConvertICC_PCSData_LUT(entries[0], Profile.PCSType);
            else if (IsThreeComponentMatrix()) ConvertICC_PCSData_Matrix(entries);
            else if (IsMonochrome()) ConvertICC_PCSData_Monochrome(entries);
            else throw new InvalidProfileException();
        }

        private void ConvertICC_PCSData_LUT(TagDataEntry entry, ColorSpaceType inColor)
        {
            switch (entry.Signature)
            {
                case TypeSignature.Lut8:
                    WriteLUT8(entry as Lut8TagDataEntry, inColor);
                    break;
                case TypeSignature.Lut16:
                    WriteLUT16(entry as Lut16TagDataEntry, inColor);
                    break;
                case TypeSignature.LutBToA:
                    WriteLutBToA(entry as LutBToATagDataEntry);
                    break;

                default:
                    throw new InvalidProfileException();
            }
        }

        private void ConvertICC_PCSData_Matrix(TagDataEntry[] entries)
        {
            ICCData.Add(GetMatrix(entries, true));
            WriteLdICCData(DataPos);//Load Matrix
            WriteLdArg();//Load in and output values
            var m = typeof(UMath).GetMethod("MultiplyMatrix_3x3_3x1");//TODO: make typesafe
            WriteMethodCall(m, false);

            IsFirst = false;
            IsLast = true;
            WriteRGBTRC(entries, true);
        }

        private void ConvertICC_PCSData_Monochrome(TagDataEntry[] entries)
        {
            IsLast = true;
            WriteGrayTRC(entries, true);
        }

        #endregion

        #region Data -> PCS

        private void ConvertICC_DataPCS()
        {
            var entries = Profile.GetConversionTag(true);
            if (entries == null || entries.Length < 1) throw new InvalidProfileException();

            if (IsNComponentLUT()) ConvertICC_DataPCS_LUT(entries[0], Profile.DataColorspaceType);
            else if (IsThreeComponentMatrix()) ConvertICC_DataPCS_Matrix(entries);
            else if (IsMonochrome()) ConvertICC_DataPCS_Monochrome(entries);
            else throw new InvalidProfileException();
        }

        private void ConvertICC_DataPCS_LUT(TagDataEntry entry, ColorSpaceType inColor)
        {
            switch (entry.Signature)
            {
                case TypeSignature.Lut8:
                    WriteLUT8(entry as Lut8TagDataEntry, inColor);
                    break;
                case TypeSignature.Lut16:
                    WriteLUT16(entry as Lut16TagDataEntry, inColor);
                    break;
                case TypeSignature.LutAToB:
                    WriteLutAToB(entry as LutAToBTagDataEntry);
                    break;

                default:
                    throw new InvalidProfileException();
            }
        }

        private void ConvertICC_DataPCS_Matrix(TagDataEntry[] entries)
        {
            WriteRGBTRC(entries, false);

            SwitchTempVar();
            IsFirst = false;
            IsLast = true;

            WriteLdICCData(DataPos);//Load Matrix
            WriteLdArg();//Load in and output values
            var m = typeof(UMath).GetMethod("MultiplyMatrix_3x3_3x1");//TODO: make typesafe
            WriteMethodCall(m, false);
            ICCData.Add(GetMatrix(entries, false));
        }

        private void ConvertICC_DataPCS_Monochrome(TagDataEntry[] entries)
        {
            IsLast = true;
            WriteGrayTRC(entries, false);
        }

        #endregion

        #region PCS -> PCS

        private void ConvertICC_PCSPCS()
        {

        }



        #region Subroutines


        #endregion

        #endregion

        #region Data -> Data

        private void ConvertICC_DataData()
        {

        }


        #region Subroutines


        #endregion

        #endregion


        #region Subroutines

        private double[] GetMatrix(TagDataEntry[] entries, bool inverted)
        {
            XYZNumber Mr = GetMatrixColumn(entries, TagSignature.RedMatrixColumn);
            XYZNumber Mg = GetMatrixColumn(entries, TagSignature.GreenMatrixColumn);
            XYZNumber Mb = GetMatrixColumn(entries, TagSignature.BlueMatrixColumn);

            var matrix = new double[9]
                {
                    Mr.X, Mg.X, Mb.X,
                    Mr.Y, Mg.Y, Mb.Y,
                    Mr.Z, Mg.Z, Mb.Z,
                };

            if (!inverted) return matrix;
            else
            {
                var matrixInv = new double[9];
                fixed (double* mp = matrix)
                fixed (double* mip = matrixInv)
                {
                    UMath.InvertMatrix_3x3(mp, mip);
                }
                return matrixInv;
            }
        }

        private XYZNumber GetMatrixColumn(TagDataEntry[] entries, TagSignature signature)
        {
            XYZTagDataEntry entry = entries.FirstOrDefault(t => t.TagSignature == signature) as XYZTagDataEntry;
            if (entry == null || entry.Data == null || entry.Data.Length < 1) throw new InvalidProfileException();
            return entry.Data[0];
        }

        #endregion

        #endregion

        #region Write IL Code

        #region Curves

        private void WriteRGBTRC(TagDataEntry[] entries, bool inverted)
        {
            var rtrc = entries.FirstOrDefault(t => t.TagSignature == TagSignature.RedTRC);
            if (rtrc == null) throw new InvalidProfileException();
            WriteCurve(rtrc, inverted, 0);

            var gtrc = entries.FirstOrDefault(t => t.TagSignature == TagSignature.GreenTRC);
            if (gtrc == null) throw new InvalidProfileException();
            WriteCurve(gtrc, inverted, 1);

            var btrc = entries.FirstOrDefault(t => t.TagSignature == TagSignature.BlueTRC);
            if (btrc == null) throw new InvalidProfileException();
            WriteCurve(btrc, inverted, 2);
        }

        private void WriteGrayTRC(TagDataEntry[] entries, bool inverted)
        {
            var gtrc = entries.FirstOrDefault(t => t.TagSignature == TagSignature.GrayTRC);
            if (gtrc == null) throw new InvalidProfileException();
            WriteCurve(gtrc, inverted, 0);
        }

        private void WriteCurve(TagDataEntry[] entries, bool inverted)
        {
            for (int i = 0; i < entries.Length; i++) WriteCurve(entries[i], inverted, i);
            SwitchTempVar();
        }

        private void WriteCurve(TagDataEntry curve, bool inverted, int index)
        {
            if (curve.Signature == TypeSignature.Curve && curve is CurveTagDataEntry)
            {
                if (inverted) WriteCurveInverted(curve as CurveTagDataEntry, index);
                else WriteCurve(curve as CurveTagDataEntry, index);
            }
            else if (curve.Signature == TypeSignature.ParametricCurve && curve is ParametricCurveTagDataEntry)
            {
                if (inverted) WriteParametricCurveInverted(curve as ParametricCurveTagDataEntry);
                else WriteParametricCurve(curve as ParametricCurveTagDataEntry);
            }
            else throw new InvalidProfileException();
        }


        private void WriteCurve(CurveTagDataEntry curve, int index)
        {
            if (curve.IsIdentityResponse) WriteAssignSingle(index);
            else if (curve.IsGamma)
            {
                //outColor[index] = Math.Pow(inColor[index], curve[0]);
                WriteLdOutput();
                WriteLdPtr(index);
                WriteLdInput();
                WriteLdPtr(index);
                CMIL.Emit(OpCodes.Ldind_R8);
                CMIL.Emit(OpCodes.Ldc_R8, curve.CurveData[0]);
                var m = typeof(Math).GetMethod("Pow");//TODO: make typesafe
                WriteMethodCall(m, false);
                CMIL.Emit(OpCodes.Stind_R8);
            }
            else
            {
                //outColor[index] = curve[(int)((inColor[index] * curve.Length - 1) + 0.5)];
                WriteLdOutput();
                WriteLdPtr(index);
                WriteLdICCData(DataPos);
                WriteLdInput();
                WriteLdPtr(index);

                CMIL.Emit(OpCodes.Ldind_R8);
                CMIL.Emit(OpCodes.Ldc_R8, curve.CurveData.Length - 1d);
                CMIL.Emit(OpCodes.Mul);
                CMIL.Emit(OpCodes.Ldc_R8, 0.5);
                CMIL.Emit(OpCodes.Add);
                CMIL.Emit(OpCodes.Conv_I4);
                CMIL.Emit(OpCodes.Conv_I);
                CMIL.Emit(OpCodes.Ldc_I4_8);
                CMIL.Emit(OpCodes.Mul);
                CMIL.Emit(OpCodes.Add);
                CMIL.Emit(OpCodes.Ldind_R8);
                CMIL.Emit(OpCodes.Stind_R8);
            }

            ICCData.Add(curve.CurveData);
        }

        private void WriteCurveInverted(CurveTagDataEntry curve, int index)
        {
            if (curve.IsIdentityResponse) WriteAssignSingle(index);
            else if (curve.IsGamma)
            {
                //outColor[index] = Math.Pow(inColor[index], 1 / curve[0]);
                WriteLdOutput();
                WriteLdPtr(index);
                WriteLdInput();
                WriteLdPtr(index);
                CMIL.Emit(OpCodes.Ldind_R8);
                CMIL.Emit(OpCodes.Ldc_R8, 1d / curve.CurveData[0]);
                var m = typeof(Math).GetMethod("Pow");//TODO: make typesafe
                WriteMethodCall(m, false);
                CMIL.Emit(OpCodes.Stind_R8);
            }
            else
            {
                /* int scopeStart = 0;
                 * int scopeEnd = curve.CurveData.Length - 1;
                 * int foundIndex = 0;
                 * while (scopeEnd > scopeStart)
                 * {
                 *     foundIndex = (scopeStart + scopeEnd) / 2;
                 *     if (inColor[index] > curve[foundIndex]) scopeStart = foundIndex + 1;
                 *     else scopeEnd = foundIndex;
                 * }
                 * outColor[index] = curve[foundIndex];
                 */

                #region Write IL Code

                var whileStartLabel = CMIL.DefineLabel();
                var whileEndLabel = CMIL.DefineLabel();
                var ifLabel = CMIL.DefineLabel();

                //int scopeStart = 0;
                CMIL.Emit(OpCodes.Ldc_I4_0);
                var scopeStart = WriteStloc(typeof(int));
                //int scopeEnd = curve.Length - 1;
                CMIL.Emit(OpCodes.Ldc_I4, curve.CurveData.Length - 1);
                var scopeEnd = WriteStloc(typeof(int));
                //int foundIndex = 0;
                CMIL.Emit(OpCodes.Ldc_I4_0);
                var foundIndex = WriteStloc(typeof(int));

                //while start
                CMIL.Emit(OpCodes.Br, whileEndLabel);
                CMIL.MarkLabel(whileStartLabel);

                //foundIndex = (scopeStart + scopeEnd) / 2;
                WriteLdloc(scopeStart);
                WriteLdloc(scopeEnd);
                CMIL.Emit(OpCodes.Add);
                CMIL.Emit(OpCodes.Ldc_I4_2);
                CMIL.Emit(OpCodes.Div);
                WriteStloc(foundIndex);

                //if (inColor[index] > curve[index])
                WriteLdInput();
                WriteLdPtr(index);
                CMIL.Emit(OpCodes.Ldind_R8);
                WriteLdICCData(DataPos);
                WriteLdloc(foundIndex);
                CMIL.Emit(OpCodes.Conv_I);
                CMIL.Emit(OpCodes.Ldc_I4_8);
                CMIL.Emit(OpCodes.Mul);
                CMIL.Emit(OpCodes.Add);
                CMIL.Emit(OpCodes.Ldind_R8);
                CMIL.Emit(OpCodes.Ble_Un, ifLabel);

                //if == true: scopeStart = foundIndex + 1;
                WriteLdloc(foundIndex);
                CMIL.Emit(OpCodes.Ldc_I4_1);
                CMIL.Emit(OpCodes.Add);
                WriteStloc(scopeStart);
                CMIL.Emit(OpCodes.Br, whileEndLabel);
                CMIL.MarkLabel(ifLabel);

                //else: scopeEnd = foundIndex;
                WriteLdloc(foundIndex);
                WriteStloc(scopeEnd);

                //while end
                CMIL.MarkLabel(whileEndLabel);

                //while condition
                WriteLdloc(scopeEnd);
                WriteLdloc(scopeStart);
                CMIL.Emit(OpCodes.Bgt, whileStartLabel);

                //outColor[index] = curve[foundIndex];
                WriteLdOutput();
                WriteLdPtr(index);
                WriteLdICCData(DataPos);
                WriteLdloc(foundIndex);
                CMIL.Emit(OpCodes.Conv_I);
                CMIL.Emit(OpCodes.Ldc_I4_8);
                CMIL.Emit(OpCodes.Mul);
                CMIL.Emit(OpCodes.Add);
                CMIL.Emit(OpCodes.Ldind_R8);
                CMIL.Emit(OpCodes.Stind_R8);

                #endregion
            }

            ICCData.Add(curve.CurveData);
        }

        private void WriteParametricCurve(ParametricCurveTagDataEntry data)
        {
            ParametricCurve curve = data.Curve;

            double X = 0;//actual calc value
            double res = 0;

            switch (curve.type)//TODO: IL for parametric curve
            {
                case 0:
                    res = Math.Pow(X, curve.g);
                    break;
                case 1:
                    res = (X >= -curve.b / curve.a) ? Math.Pow(curve.a * X + curve.b, curve.g) : 0;
                    break;
                case 2:
                    res = (X >= -curve.b / curve.a) ? Math.Pow(curve.a * X + curve.b, curve.g) + curve.c : curve.c;
                    break;
                case 3:
                    res = (X >= curve.d) ? Math.Pow(curve.a * X + curve.b, curve.g) : curve.c * X;
                    break;
                case 4:
                    res = (X >= curve.d) ? Math.Pow(curve.a * X + curve.b, curve.g) + curve.c : curve.c * X + curve.f;
                    break;

                default:
                    throw new CorruptProfileException("ParametricCurve");
            }

        }

        private void WriteParametricCurveInverted(ParametricCurveTagDataEntry data)
        {
            ParametricCurve curve = data.Curve;

            double X = 0;//actual calc value
            double res = 0;

            switch (curve.type)//TODO: IL for parametric curve
            {
                case 0:
                    res = Math.Pow(X, 1 / curve.g);
                    break;
                case 1:
                    res = (X >= -curve.b / curve.a) ? (Math.Pow(curve.a, 1 / curve.g) - curve.b) / X : 0;
                    break;
                case 2:
                    res = (X >= -curve.b / curve.a) ? (Math.Pow(X - curve.c, 1 / curve.g) - curve.b) / curve.a : curve.c;
                    break;
                case 3:
                    res = (X >= curve.d) ? (Math.Pow(curve.a, 1 / curve.g) - curve.b) / X : X / curve.c;
                    break;
                case 4:
                    res = (X >= curve.d) ? (Math.Pow(X - curve.c, 1 / curve.g) - curve.b) / curve.a : (X - curve.f) / curve.c;
                    break;

                default:
                    throw new CorruptProfileException("ParametricCurve");
            }

        }


        private void WriteOneDimensionalCurve(OneDimensionalCurve curve)
        {
            double input = 0;//TODO: IL for one-dimensional curve

            int idx = -1;
            if (curve.Segments.Length != 1)
            {
                for (int i = 0; i < curve.BreakPoints.Length; i++)
                {
                    if (input <= curve.BreakPoints[i]) { idx = i; break; }
                }
                if (idx == -1) { idx = curve.Segments.Length - 1; }
            }
            else { idx = 0; }

            //var result = curve.Segments[idx].GetValue(input);
            WriteCurveSegment(curve.Segments[idx]);
        }

        private void WriteCurveSegment(CurveSegment segment)
        {
            var formula = segment as FormulaCurveElement;
            if (formula != null) WriteFormulaCurveSegment(formula);

            var sampled = segment as SampledCurveElement;
            if (sampled != null) WriteSampledCurveESegment(sampled);

            throw new InvalidProfileException();
        }

        private void WriteFormulaCurveSegment(FormulaCurveElement segment)
        {
            //TODO: WriteFormulaCurveSegment

            /*switch (type)
            {
                case 0: return Math.Pow(a * X + b, gamma) + c;
                case 1: return a * Math.Log10(b * Math.Pow(X, gamma) + c) + d;
                case 2: return a * Math.Pow(b, c * X + d) + e;
                default: return 0;
            }*/
        }

        private void WriteSampledCurveESegment(SampledCurveElement segment)
        {
            //TODO: WriteSampledCurveESegment

            /*double t = X * (CurveEntries.Length - 1);
            if (t % 1 != 0)
            {
                int i = (int)Math.Floor(t);
                return CurveEntries[i] + ((CurveEntries[i + 1] - CurveEntries[i]) * (t % 1));
            }
            else { return CurveEntries[(int)t]; }*/
        }

        #endregion

        #region LUT

        /// <summary>
        /// Writes the IL code for an 8-bit LUT entry
        /// </summary>
        /// <param name="lut">The entry containing the LUT data</param>
        /// <param name="input">True if the ICCData is from the input profile, false otherwise</param>
        /// <param name="inColor">The input color type</param>
        /// <returns>The ICCData for this LUT entry</returns>
        private void WriteLUT8(Lut8TagDataEntry lut, ColorSpaceType inColor)
        {
            LUT8[] InCurve = lut.InputValues;
            LUT8[] OutCurve = lut.OutputValues;
            double[] Matrix = new double[9]
                {
                    lut.Matrix[0, 0], lut.Matrix[0, 1], lut.Matrix[0, 2],
                    lut.Matrix[1, 0], lut.Matrix[1, 1], lut.Matrix[1, 2],
                    lut.Matrix[2, 0], lut.Matrix[2, 1], lut.Matrix[2, 2],                        
                };

            //Matrix
            if (inColor == ColorSpaceType.CIEXYZ)
            {
                WriteLdICCData(DataPos);
                WriteLdArg();
                var m = typeof(UMath).GetMethod("MultiplyMatrix_3x3_3x1");//TODO: make typesafe
                WriteMethodCall(m, false);
                ICCData.Add(Matrix);
                IsFirst = false;
            }

            //Input LUT
            for (int i = 0; i < InCurve.Length; i++) WriteLUT8(InCurve[i], i);
            IsFirst = false;
            SwitchTempVar();

            //CLUT
            WriteCLUT(lut.CLUTValues);

            IsLast = true;
            //Output LUT
            for (int i = 0; i < OutCurve.Length; i++) WriteLUT8(OutCurve[i], i);
        }

        /// <summary>
        /// Writes the IL code for an 8-bit LUT
        /// </summary>
        /// <param name="lut">The LUT for this channel</param>
        /// <param name="index">The channel index</param>
        /// <param name="position">The position of the ICCData</param>
        /// <param name="input">True if the ICCData is from the input profile, false otherwise</param>
        /// <param name="adjust">True to adjust the input color channel</param>
        /// <param name="inColor">The input color type</param>
        /// <returns>The ICCData for this LUT</returns>
        private void WriteLUT8(LUT8 lut, int index)
        {
            if (lut.Values.Length == 2) WriteAssignSingle(index);
            else
            {
                WriteLUT(lut.Values.Length, index);
                ICCData.Add(lut.Values.Select(t => t / 255d).ToArray());
            }
        }


        /// <summary>
        /// Writes the IL code for an 16-bit LUT entry
        /// </summary>
        /// <param name="lut">The entry containing the LUT data</param>
        /// <param name="input">True if the ICCData is from the input profile, false otherwise</param>
        /// <param name="inColor">The input color type</param>
        /// <returns>The ICCData for this LUT entry</returns>
        private void WriteLUT16(Lut16TagDataEntry lut, ColorSpaceType inColor)
        {
            LUT16[] InCurve = lut.InputValues;
            LUT16[] OutCurve = lut.OutputValues;
            double[] Matrix = new double[9]
                {
                    lut.Matrix[0, 0], lut.Matrix[0, 1], lut.Matrix[0, 2],
                    lut.Matrix[1, 0], lut.Matrix[1, 1], lut.Matrix[1, 2],
                    lut.Matrix[2, 0], lut.Matrix[2, 1], lut.Matrix[2, 2],                        
                };

            //Matrix
            if (inColor == ColorSpaceType.CIEXYZ)
            {
                WriteLdICCData(DataPos);
                WriteLdArg();
                var m = typeof(UMath).GetMethod("MultiplyMatrix_3x3_3x1");//TODO: make typesafe
                WriteMethodCall(m, false);
                ICCData.Add(Matrix);
                IsFirst = false;
            }

            //Input LUT
            for (int i = 0; i < InCurve.Length; i++) WriteLUT16(InCurve[i], i);
            IsFirst = false;
            SwitchTempVar();

            //CLUT
            WriteCLUT(lut.CLUTValues);
            IsLast = true;

            //Output LUT
            for (int i = 0; i < OutCurve.Length; i++) WriteLUT16(OutCurve[i], i);
        }

        /// <summary>
        /// Writes the IL code for an 16-bit LUT
        /// </summary>
        /// <param name="lut">The LUT for this channel</param>
        /// <param name="index">The channel index</param>
        /// <param name="position">The position of the ICCData</param>
        /// <param name="input">True if the ICCData is from the input profile, false otherwise</param>
        /// <param name="adjust">True to adjust the input color channel</param>
        /// <param name="inColor">The input color type</param>
        /// <returns>The ICCData for this LUT</returns>
        private void WriteLUT16(LUT16 lut, int index)
        {
            if (lut.Values.Length == 2) WriteAssignSingle(index);
            else
            {
                WriteLUT(lut.Values.Length, index);
                ICCData.Add(lut.Values.Select(t => t / 65535d).ToArray());
            }
        }


        private void WriteLutAToB(LutAToBTagDataEntry entry)
        {
            bool ca = entry.CurveA != null;
            bool cb = entry.CurveB != null;
            bool cm = entry.CurveM != null;
            bool matrix = entry.Matrix3x1 != null && entry.Matrix3x3 != null;
            bool clut = entry.CLUTValues != null;

            if (ca && clut && cm && matrix && cb)
            {
                WriteCurve(entry.CurveA, false);
                IsFirst = false;
                WriteCLUT(entry.CLUTValues);
                WriteCurve(entry.CurveM, false);
                WriteMatrix(entry.Matrix3x3, entry.Matrix3x1);
                IsLast = true;
                WriteCurve(entry.CurveB, false);
            }
            else if (ca && clut && cb)
            {
                WriteCurve(entry.CurveA, false);
                IsFirst = false;
                WriteCLUT(entry.CLUTValues);
                IsLast = true;
                WriteCurve(entry.CurveB, false);
            }
            else if (cm && matrix && cb)
            {
                WriteCurve(entry.CurveM, false);
                IsFirst = false;
                WriteMatrix(entry.Matrix3x3, entry.Matrix3x1);
                IsLast = true;
                WriteCurve(entry.CurveB, false);
            }
            else if (cb)
            {
                IsLast = true;
                WriteCurve(entry.CurveB, false);
            }
            else throw new InvalidProfileException("AToB tag has an invalid configuration");
        }

        private void WriteLutBToA(LutBToATagDataEntry entry)
        {
            bool ca = entry.CurveA != null;
            bool cb = entry.CurveB != null;
            bool cm = entry.CurveM != null;
            bool matrix = entry.Matrix3x1 != null && entry.Matrix3x3 != null;
            bool clut = entry.CLUTValues != null;

            if (cb && matrix && cm && clut && ca)
            {
                WriteCurve(entry.CurveB, false);
                IsFirst = false;
                WriteMatrix(entry.Matrix3x3, entry.Matrix3x1);
                WriteCurve(entry.CurveM, false);
                WriteCLUT(entry.CLUTValues);
                IsLast = true;
                WriteCurve(entry.CurveA, false);
            }
            else if (cb && clut && ca)
            {
                WriteCurve(entry.CurveB, false);
                IsFirst = false;
                WriteCLUT(entry.CLUTValues);
                IsLast = true;
                WriteCurve(entry.CurveA, false);
            }
            else if (cb && matrix && cm)
            {
                WriteCurve(entry.CurveB, false);
                IsFirst = false;
                WriteMatrix(entry.Matrix3x3, entry.Matrix3x1);
                IsLast = true;
                WriteCurve(entry.CurveM, false);
            }
            else if (cb)
            {
                IsLast = true;
                WriteCurve(entry.CurveB, false);
            }
            else throw new InvalidProfileException("BToA tag has an invalid configuration");
        }



        /// <summary>
        /// Writes the IL code for a LUT
        /// </summary>
        /// <param name="lut">The LUT for this channel</param>
        /// <param name="index">The channel index</param>
        /// <param name="position">The position of the ICCData</param>
        /// <param name="input">True if the ICCData is from the input profile, false otherwise</param>
        /// <param name="adjustment">The adjustment value. e.g. 256 for 8-bit or -1 if no adjustment is wanted</param>
        /// <param name="inColor">The input color type</param>
        private void WriteLUT(int length, int index)
        {
            //outColor[index] = lut.Values[(int)((inColor[index] * lut.Length - 1) + 0.5)];
            WriteLdOutput();
            WriteLdPtr(index);
            WriteLdICCData(DataPos);
            WriteLdInput();
            WriteLdPtr(index);

            CMIL.Emit(OpCodes.Ldind_R8);
            CMIL.Emit(OpCodes.Ldc_R8, length - 1d);
            CMIL.Emit(OpCodes.Mul);
            CMIL.Emit(OpCodes.Ldc_R8, 0.5);
            CMIL.Emit(OpCodes.Add);
            CMIL.Emit(OpCodes.Conv_I4);
            CMIL.Emit(OpCodes.Conv_I);
            CMIL.Emit(OpCodes.Ldc_I4_8);
            CMIL.Emit(OpCodes.Mul);
            CMIL.Emit(OpCodes.Add);
            CMIL.Emit(OpCodes.Ldind_R8);
            CMIL.Emit(OpCodes.Stind_R8);
        }

        /// <summary>
        /// Writes the IL code for a CLUT
        /// </summary>
        /// <param name="lut">The CLUT for this channel</param>
        /// <param name="position">The position of the ICCData</param>
        /// <param name="input">True if the ICCData is from the input profile, false otherwise</param>
        /// <returns>The ICCData for this CLUT</returns>
        private void WriteCLUT(CLUT lut)
        {
            #region IL Code

            /* int idx = (int)((inColor[0] * Math.Pow(lut.GridPointCount[0], 3)
                                  + inColor[1] * Math.Pow(lut.GridPointCount[1], 2)
                                  + inColor[2] * Math.Pow(lut.GridPointCount[2], 1)) + 0.5);
                
                    outColor[0] = data.InICCData[position + 0][idx];
                    outColor[1] = data.InICCData[position + 1][idx];
                    outColor[2] = data.InICCData[position + 2][idx];
                    outColor[3] = data.InICCData[position + 3][idx];
                */

            int c = lut.InputChannelCount;
            for (int i = 0; i < lut.InputChannelCount; i++, c--)
            {
                WriteLdInput();
                WriteLdPtr(i);
                CMIL.Emit(OpCodes.Ldind_R8);
                CMIL.Emit(OpCodes.Ldc_R8, Math.Pow(lut.GridPointCount[i], c));
                CMIL.Emit(OpCodes.Mul);
                if (i != 0) CMIL.Emit(OpCodes.Add);
            }

            CMIL.Emit(OpCodes.Ldc_R8, 0.5);
            CMIL.Emit(OpCodes.Add);
            CMIL.Emit(OpCodes.Conv_I4);
            var idx = WriteStloc(typeof(Int32));

            for (int i = 0; i < lut.OutputChannelCount; i++)
            {
                WriteLdOutput();
                WriteLdPtr(i);
                WriteLdICCData(DataPos + i);
                WriteLdloc(idx);
                CMIL.Emit(OpCodes.Conv_I);
                CMIL.Emit(OpCodes.Ldc_I4_8);
                CMIL.Emit(OpCodes.Mul);
                CMIL.Emit(OpCodes.Add);
                CMIL.Emit(OpCodes.Ldind_R8);
                CMIL.Emit(OpCodes.Stind_R8);
            }

            SwitchTempVar();

            #endregion

            #region Reordering Array

            var lut8 = lut as CLUT8;
            if (lut8 != null)
            {
                double[][] vals = new double[lut.OutputChannelCount][];
                for (int i = 0; i < vals.Length; i++)
                {
                    vals[i] = new double[lut8.Values.Length];
                    for (int j = 0; j < vals[i].Length; j++)
                    {
                        vals[i][j] = lut8.Values[j][i] / 255d;
                    }
                }
                ICCData.AddRange(vals);
            }

            var lut16 = lut as CLUT16;
            if (lut16 != null)
            {
                double[][] vals = new double[lut.OutputChannelCount][];
                for (int i = 0; i < vals.Length; i++)
                {
                    vals[i] = new double[lut16.Values.Length];
                    for (int j = 0; j < vals[i].Length; j++)
                    {
                        vals[i][j] = lut16.Values[j][i] / 65535d;
                    }
                }
                ICCData.AddRange(vals);
            }

            var lutf32 = lut as CLUTf32;
            if (lutf32 != null)
            {
                double[][] vals = new double[lut.OutputChannelCount][];
                for (int i = 0; i < vals.Length; i++)
                {
                    vals[i] = new double[lutf32.Values.Length];
                    for (int j = 0; j < vals[i].Length; j++)
                    {
                        vals[i][j] = lutf32.Values[j][i];
                    }
                }
                ICCData.AddRange(vals);
            }

            throw new InvalidProfileException();

            #endregion
        }

        /// <summary>
        /// Writes the IL code for (3x3 * 3x1) + 3x1 matrix calculation
        /// </summary>
        /// <param name="matrix3x3">The 3x3 matrix</param>
        /// <param name="matrix3x1">The 3x1 matrix</param>
        /// <param name="position">The position of the ICCData</param>
        /// <param name="input">True if the ICCData is from the input profile, false otherwise</param>
        /// <returns>The ICCData for this matrix calculation</returns>
        private void WriteMatrix(double[,] matrix3x3, double[] matrix3x1)
        {
            WriteLdICCData(DataPos);
            WriteLdArg();
            var m = typeof(UMath).GetMethod("MultiplyMatrix_3x3_3x1");//TODO: make typesafe
            WriteMethodCall(m, false);

            WriteLdICCData(DataPos + 1);
            WriteLdArg();
            m = typeof(UMath).GetMethod("AddMatrix_3x1");//TODO: make typesafe
            WriteMethodCall(m, false);

            double[] m3x3 = new double[9]
                {
                    matrix3x3[0, 0], matrix3x3[0, 1], matrix3x3[0, 2],
                    matrix3x3[1, 0], matrix3x3[1, 1], matrix3x3[1, 2],
                    matrix3x3[2, 0], matrix3x3[2, 1], matrix3x3[2, 2],                        
                };

            ICCData.Add(m3x3);
            ICCData.Add(matrix3x1);
        }

        #endregion


        #region Subroutines

        private void WriteLdICCData(int position)
        {
            string fname = (IsInput ? "In" : "Out") + "ICCData";//TODO: make typesafe

            CMIL.Emit(OpCodes.Ldarg_3);
            FieldInfo fi = typeof(ConversionData).GetField(fname);
            CMIL.Emit(OpCodes.Ldfld, fi);
            //TODO: this could probably be optimized (similar to WriteLdPtr) because sizeof(double*) should always be the same (=InPtr.Size) at runtime
            if (position != 0)
            {
                switch (position)
                {
                    case 1:
                        CMIL.Emit(OpCodes.Ldc_I4_1);
                        break;
                    case 2:
                        CMIL.Emit(OpCodes.Ldc_I4_2);
                        break;
                    case 3:
                        CMIL.Emit(OpCodes.Ldc_I4_3);
                        break;
                    case 4:
                        CMIL.Emit(OpCodes.Ldc_I4_4);
                        break;
                    case 5:
                        CMIL.Emit(OpCodes.Ldc_I4_5);
                        break;
                    case 6:
                        CMIL.Emit(OpCodes.Ldc_I4_6);
                        break;
                    case 7:
                        CMIL.Emit(OpCodes.Ldc_I4_7);
                        break;
                    case 8:
                        CMIL.Emit(OpCodes.Ldc_I4_8);
                        break;

                    default:
                        if (position < 256) CMIL.Emit(OpCodes.Ldc_I4_S, position);
                        else CMIL.Emit(OpCodes.Ldc_I4, position);
                        break;
                }

                CMIL.Emit(OpCodes.Conv_I);
                CMIL.Emit(OpCodes.Sizeof, typeof(double*));
                CMIL.Emit(OpCodes.Mul);
                CMIL.Emit(OpCodes.Add);
            }
            CMIL.Emit(OpCodes.Ldind_I);
        }

        private void AdjustInputColor(ColorSpaceType colorType)
        {
            //TODO: write adjustment code

            switch (colorType)
            {
                case ColorSpaceType.CIELAB:
                    // L / 100
                    // (ab + 256) / 512
                    break;
                case ColorSpaceType.CIELUV:
                    // L / 100
                    // (uv + 256) / 512
                    break;
                case ColorSpaceType.YCbCr:
                    // ?
                    break;
                case ColorSpaceType.HSV:
                    // ?
                    break;
                case ColorSpaceType.HLS:
                    // ?
                    // switch L and S channel
                    break;
            }
        }

        private void AdjustOutputColor(ColorSpaceType colorType)
        {
            //TODO: write adjustment code
        }

        #endregion

        #endregion


        #region Subroutines

        private bool IsSameColor(Color inColor, Color outColor)
        {
            return inColor.GetType() == outColor.GetType()
                && inColor.Space.ReferenceWhite == outColor.Space.ReferenceWhite
                && InColor.ChannelCount == OutColor.ChannelCount;
        }

        private bool? IsPCS(Color color, ICCProfile otherProfile)
        {
            if (color.IsColorICC) return color.IsICCPCS;
            else if (otherProfile != null)
            {
                var tp = color.GetType();
                if (tp == otherProfile.PCS) return true;
                else if (tp == otherProfile.DataColorspace) return false;
                else return null;
            }
            else return null;
        }

        private ICCProfile GetProfile(Colorspace space)
        {
            var iccspace = space as ColorspaceICC;
            if (iccspace != null)
            {
                if (iccspace.Profile == null || iccspace.Profile.Validate() == false) throw new Exception("Invalid profile");
                return iccspace.Profile;
            }
            return null;
        }

        #endregion
    }
}
