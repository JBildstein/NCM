using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Collections.Generic;
using ColorManager.Conversion;

namespace ColorManager.ICC.Conversion
{
    /// <summary>
    /// Factory to create a conversion method for ICC colors
    /// </summary>
    public sealed unsafe class ConversionCreator_ICC : ConversionCreator
    {
        #region Variables

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
            DataToPCS,
            /// <summary>
            /// ICC conversion from PCS to data
            /// </summary>
            PCSToData,
            /// <summary>
            /// ICC conversion from data to data
            /// </summary>
            DataToData,
            /// <summary>
            /// ICC conversion from PCS to PCS
            /// </summary>
            PCSToPCS,
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
            if (InColor.ICCProfile != null) { Profile = InColor.ICCProfile; IsInput = true; }
            if (OutColor.ICCProfile != null)
            {
                if (Profile != null && Profile != OutColor.ICCProfile) throw new ConversionSetupException();
                else if (Profile == null) { Profile = OutColor.ICCProfile; IsInput = false; }
            }
            if (Profile == null) throw new ConversionSetupException();

            CheckConversionType();
        }

        /// <summary>
        /// Sets the conversion method
        /// </summary>
        public override void SetConversionMethod()
        {
            switch (ConversionType)
            {
                case ConvType.DataToPCS:
                    AdjustInputColor(Profile.DataColorspace);
                    ConvertICC_DataPCS();
                    AdjustOutputColor(Profile.PCS);
                    break;

                case ConvType.PCSToData:
                    AdjustInputColor(Profile.PCS);
                    ConvertICC_PCSData();
                    AdjustOutputColor(Profile.DataColorspace);
                    break;

                case ConvType.DataToData:
                    AdjustInputColor(Profile.DataColorspace);
                    ConvertICC_DataData();
                    AdjustOutputColor(Profile.DataColorspace);
                    break;

                case ConvType.PCSToPCS:
                    AdjustInputColor(Profile.PCS);
                    ConvertICC_PCSPCS();
                    AdjustOutputColor(Profile.PCS);
                    break;

                default:
                    throw new ConversionSetupException("Not able to perform conversion");
            }

            if (ICCData != null && ICCData.Count > 0)
            {
                if (IsInput) Data.SetICCData(ICCData.ToArray(), null);
                else Data.SetICCData(null, ICCData.ToArray());
            }
            if (IsLastG) Write(OpCodes.Ret);
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
            if (IsInPCS == false && IsOutPCS == true) ConversionType = ConvType.DataToData;
        }

        private void CheckAbstractProfile()
        {
            if (IsInPCS == true && IsOutPCS == true) ConversionType = ConvType.PCSToPCS;
        }

        private void CheckDevicePCS()
        {
            if (IsInPCS == false)
            {
                var spaceWP = OutColor.Space.ReferenceWhite.GetType();
                if (IsOutPCS == true && spaceWP == typeof(WhitepointD50)) ConversionType = ConvType.DataToPCS;
            }
            else if (IsOutPCS == false)
            {
                var spaceWP = InColor.Space.ReferenceWhite.GetType();
                if (IsInPCS == true && spaceWP == typeof(WhitepointD50)) ConversionType = ConvType.PCSToData;
            }
        }

        #endregion

        #region Profile Conversion Type

        private bool IsNComponentLUT()
        {
            switch (Profile.Class)
            {
                case ProfileClassName.DisplayDevice:
                    return Profile.HasTag(TagSignature.AToB0) && Profile.HasTag(TagSignature.BToA0);

                case ProfileClassName.OutputDevice:
                    if (Profile.DataColorspaceType == typeof(ColorX) && !Profile.HasTag(TagSignature.ColorantTable)) return false;
                    return Profile.HasTag(TagSignature.AToB0) && Profile.HasTag(TagSignature.BToA0)
                        && Profile.HasTag(TagSignature.AToB1) && Profile.HasTag(TagSignature.BToA1)
                        && Profile.HasTag(TagSignature.AToB2) && Profile.HasTag(TagSignature.BToA2)
                        && Profile.HasTag(TagSignature.Gamut);

                default:
                    return Profile.HasTag(TagSignature.AToB0);
            }
        }

        private bool IsMonochrome()
        {
            return Profile.HasTag(TagSignature.GrayTRC);
        }

        private bool IsThreeComponentMatrix()
        {
            return Profile.HasTag(TagSignature.RedMatrixColumn)
                && Profile.HasTag(TagSignature.GreenMatrixColumn)
                && Profile.HasTag(TagSignature.BlueMatrixColumn)
                && Profile.HasTag(TagSignature.RedTRC)
                && Profile.HasTag(TagSignature.GreenTRC)
                && Profile.HasTag(TagSignature.BlueTRC);
        }

        private bool IsMultiProcess()
        {
            //TODO: IsMultiProcess is not quite correct yet
            return Profile.HasTag(TagSignature.DToB0)
                || Profile.HasTag(TagSignature.DToB1)
                || Profile.HasTag(TagSignature.DToB2)
                || Profile.HasTag(TagSignature.DToB3)
                || Profile.HasTag(TagSignature.BToD0)
                || Profile.HasTag(TagSignature.BToD1)
                || Profile.HasTag(TagSignature.BToD2)
                || Profile.HasTag(TagSignature.BToD3);
        }

        #endregion

        //TODO: add choice of rendering intent and method
        //LTODO: test multiprocess element conversion
        //LTODO: include chromatic adaption if CATag is existing
        //LTODO: check for media white/black point tag and use it if existing and appropriate

        #region PCS -> Data

        /// <summary>
        /// Writes the IL code for an ICC conversion from PCS to Data
        /// </summary>
        private void ConvertICC_PCSData()
        {
            var entries = Profile.GetConversionTag(false);
            if (entries == null || entries.Length < 1) throw new InvalidProfileException();

            if (IsNComponentLUT()) ConvertICC_PCSData_LUT(entries[0], Profile.PCS);
            else if (IsThreeComponentMatrix()) ConvertICC_PCSData_Matrix(entries);
            else if (IsMultiProcess()) ConcvertICC_PCSData_Multiprocess(entries[0]);
            else if (IsMonochrome()) ConvertICC_PCSData_Monochrome(entries);
            else throw new InvalidProfileException();
        }

        /// <summary>
        /// Writes the IL code for a LUT ICC conversion from PCS to Data
        /// </summary>
        /// <param name="entry">The entry with the LUT data</param>
        /// <param name="inColor">The input color type</param>
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

        /// <summary>
        /// Writes the IL code for a Matrix/TRC ICC conversion from PCS to Data
        /// </summary>
        /// <param name="entries">The entries containing the Matrix and TRC data</param>
        private void ConvertICC_PCSData_Matrix(TagDataEntry[] entries)
        {
            WriteLdICCData(DataPos);//Load Matrix
            WriteLdArg(false);//Load in and output values
            WriteCallMultiplyMatrix_3x3_3x1();
            ICCData.Add(GetMatrix(entries, true));

            IsFirst = false;
            IsLast = true;
            WriteRGBTRC(entries, true);
        }

        /// <summary>
        /// Writes the IL code for a monochrome ICC conversion from PCS to Data
        /// </summary>
        /// <param name="entries">The entries containing the monochrome TRC data</param>
        private void ConvertICC_PCSData_Monochrome(TagDataEntry[] entries)
        {
            IsLast = true;
            WriteGrayTRC(entries, true);
        }

        /// <summary>
        /// Writes the IL code for a multiprocess ICC conversion from PCS to Data
        /// </summary>
        /// <param name="entry">The entry with the multiprocess data</param>
        private void ConcvertICC_PCSData_Multiprocess(TagDataEntry entry)
        {
            var m = entry as MultiProcessElementsTagDataEntry;
            if (m != null) WriteMultiProcessElement(m);
            else throw new Exception("Entry is not a MultiProcessElementsTagDataEntry");
        }

        #endregion

        #region Data -> PCS

        /// <summary>
        /// Writes the IL code for an ICC conversion from Data to PCS
        /// </summary>
        private void ConvertICC_DataPCS()
        {
            var entries = Profile.GetConversionTag(true);
            if (entries == null || entries.Length < 1) throw new InvalidProfileException();

            if (IsNComponentLUT()) ConvertICC_DataPCS_LUT(entries[0], Profile.DataColorspace);
            else if (IsThreeComponentMatrix()) ConvertICC_DataPCS_Matrix(entries);
            else if (IsMultiProcess()) ConcvertICC_PCSData_Multiprocess(entries[0]);
            else if (IsMonochrome()) ConvertICC_DataPCS_Monochrome(entries);
            else throw new InvalidProfileException();
        }

        /// <summary>
        /// Writes the IL code for a LUT ICC conversion from Data to PCS
        /// </summary>
        /// <param name="entry">The entry with the LUT data</param>
        /// <param name="inColor">The input color type</param>
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

        /// <summary>
        /// Writes the IL code for a Matrix/TRC ICC conversion from Data to PCS
        /// </summary>
        /// <param name="entries">The entries containing the Matrix and TRC data</param>
        private void ConvertICC_DataPCS_Matrix(TagDataEntry[] entries)
        {
            WriteRGBTRC(entries, false);

            SwitchTempVar();
            IsFirst = false;
            IsLast = true;

            WriteLdICCData(DataPos);//Load Matrix
            WriteLdArg(false);//Load in and output values
            WriteCallMultiplyMatrix_3x3_3x1();
            ICCData.Add(GetMatrix(entries, false));
        }

        /// <summary>
        /// Writes the IL code for a monochrome ICC conversion from Data to PCS
        /// </summary>
        /// <param name="entries">The entries containing the monochrome TRC data</param>
        private void ConvertICC_DataPCS_Monochrome(TagDataEntry[] entries)
        {
            IsLast = true;
            WriteGrayTRC(entries, false);
        }

        /// <summary>
        /// Writes the IL code for a multiprocess ICC conversion from Data to PCS
        /// </summary>
        /// <param name="entry">The entry with the multiprocess data</param>
        private void ConvertICC_DataPCS_Multiprocess(TagDataEntry entry)
        {
            var m = entry as MultiProcessElementsTagDataEntry;
            if (m != null) WriteMultiProcessElement(m);
            else throw new Exception("Entry is not a MultiProcessElementsTagDataEntry");
        }

        #endregion

        #region PCS -> PCS

        private void ConvertICC_PCSPCS()
        {
            var entries = Profile.GetConversionTag(true);
            if (entries == null || entries.Length < 1) throw new InvalidProfileException();

            if (IsNComponentLUT()) ConvertICC_PCSData_LUT(entries[0], Profile.PCS);
            else if (IsMultiProcess()) ConcvertICC_PCSData_Multiprocess(entries[0]);
            else throw new InvalidProfileException();
        }

        #endregion

        #region Data -> Data

        private void ConvertICC_DataData()
        {
            var entries = Profile.GetConversionTag(true);
            if (entries == null || entries.Length < 1) throw new InvalidProfileException();

            if (IsNComponentLUT()) ConvertICC_PCSData_LUT(entries[0], Profile.DataColorspace);
            else if (IsMultiProcess()) ConcvertICC_PCSData_Multiprocess(entries[0]);
            else throw new InvalidProfileException();
        }

        #endregion


        #region Write Curves IL Code

        /// <summary>
        /// Writes the IL code for R, G and B TRC curves
        /// </summary>
        /// <param name="entries">The entries containing the RGB TRC curves</param>
        /// <param name="inverted">True if the curves should be inverted, false otherwise</param>
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

        /// <summary>
        /// Writes the IL code for a Gray TRC curve
        /// </summary>
        /// <param name="entries">The entries containing the Gray TRC curve</param>
        /// <param name="inverted">True if the curve should be inverted, false otherwise</param>
        private void WriteGrayTRC(TagDataEntry[] entries, bool inverted)
        {
            var gtrc = entries.FirstOrDefault(t => t.TagSignature == TagSignature.GrayTRC);
            if (gtrc == null) throw new InvalidProfileException();
            WriteCurve(gtrc, inverted, 0);
        }

        /// <summary>
        /// Writes the IL code for a set of curves
        /// </summary>
        /// <param name="entries">The entries containing the curves</param>
        /// <param name="inverted">True if the curves should be inverted, false otherwise</param>
        private void WriteCurve(TagDataEntry[] entries, bool inverted)
        {
            for (int i = 0; i < entries.Length; i++) WriteCurve(entries[i], inverted, i);
            SwitchTempVar();
        }

        /// <summary>
        /// Writes the IL code for a curve
        /// </summary>
        /// <param name="curve">The entry containing the curve data</param>
        /// <param name="inverted">True if the curve should be inverted, false otherwise</param>
        /// <param name="index">The channel index of this curve</param>
        private void WriteCurve(TagDataEntry curve, bool inverted, int index)
        {
            if (curve.Signature == TypeSignature.Curve && curve is CurveTagDataEntry)
            {
                if (inverted) WriteCurveInverted(curve as CurveTagDataEntry, index);
                else WriteCurve(curve as CurveTagDataEntry, index);
            }
            else if (curve.Signature == TypeSignature.ParametricCurve && curve is ParametricCurveTagDataEntry)
            {
                if (inverted) WriteParametricCurveInverted(curve as ParametricCurveTagDataEntry, index);
                else WriteParametricCurve(curve as ParametricCurveTagDataEntry, index);
            }
            else throw new InvalidProfileException();
        }

        #region Normal Curve

        /// <summary>
        /// Writes the IL code for a curve
        /// </summary>
        /// <param name="curve">The entry containing the curve data</param>
        /// <param name="index">The channel index of this curve</param>
        private void WriteCurve(CurveTagDataEntry curve, int index)
        {
            if (curve.IsIdentityResponse) WriteAssignSingle(index);
            else if (curve.IsGamma)
            {
                //outColor[index] = Math.Pow(inColor[index], curve[0]);
                WriteLdOutput(index);
                WriteLdInput(index);
                Write(OpCodes.Ldind_R8);
                Write(OpCodes.Ldc_R8, curve.CurveData[0]);
                WriteCallPow();
                Write(OpCodes.Stind_R8);
            }
            else
            {
                //outColor[index] = curve[(int)((inColor[index] * curve.Length - 1) + 0.5)];
                WriteLdOutput(index);
                WriteLdICCData(DataPos);
                WriteLdInput(index);
                Write(OpCodes.Ldind_R8);
                Write(OpCodes.Ldc_R8, curve.CurveData.Length - 1d);
                Write(OpCodes.Mul);
                Write(OpCodes.Ldc_R8, 0.5);
                Write(OpCodes.Add);
                Write(OpCodes.Conv_I4);
                Write(OpCodes.Conv_I);
                WriteLdInt(8);
                Write(OpCodes.Mul);
                Write(OpCodes.Add);
                Write(OpCodes.Ldind_R8);
                Write(OpCodes.Stind_R8);

                ICCData.Add(curve.CurveData);
            }
        }

        /// <summary>
        /// Writes the IL code for an inverted curve
        /// </summary>
        /// <param name="curve">The entry containing the curve data</param>
        /// <param name="index">The channel index of this curve</param>
        private void WriteCurveInverted(CurveTagDataEntry curve, int index)
        {
            if (curve.IsIdentityResponse) WriteAssignSingle(index);
            else if (curve.IsGamma)
            {
                //outColor[index] = Math.Pow(inColor[index], 1 / curve[0]);
                WriteLdOutput(index);
                WriteLdInput(index);
                Write(OpCodes.Ldind_R8);
                Write(OpCodes.Ldc_R8, 1d / curve.CurveData[0]);
                WriteCallPow();
                Write(OpCodes.Stind_R8);
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

                var whileStartLabel = ILG.DefineLabel();
                var whileEndLabel = ILG.DefineLabel();
                var ifLabel = ILG.DefineLabel();

                //int scopeStart = 0;
                WriteLdInt(0);
                var scopeStart = WriteStloc(typeof(int));
                //int scopeEnd = curve.Length - 1;
                WriteLdInt(curve.CurveData.Length - 1);
                var scopeEnd = WriteStloc(typeof(int));
                //int foundIndex = 0;
                WriteLdInt(0);
                var foundIndex = WriteStloc(typeof(int));

                //while start
                Write(OpCodes.Br, whileEndLabel);
                ILG.MarkLabel(whileStartLabel);

                //foundIndex = (scopeStart + scopeEnd) / 2;
                WriteLdloc(scopeStart);
                WriteLdloc(scopeEnd);
                Write(OpCodes.Add);
                Write(OpCodes.Ldc_R8, 0.5);
                Write(OpCodes.Mul);
                WriteStloc(foundIndex);

                //if (inColor[index] > curve[index])
                WriteLdInput(index);
                Write(OpCodes.Ldind_R8);
                WriteLdICCData(DataPos);
                WriteLdloc(foundIndex);
                Write(OpCodes.Conv_I);
                WriteLdInt(8);
                Write(OpCodes.Mul);
                Write(OpCodes.Add);
                Write(OpCodes.Ldind_R8);
                Write(OpCodes.Ble_Un, ifLabel);

                //if == true: scopeStart = foundIndex + 1;
                WriteLdloc(foundIndex);
                WriteLdInt(1);
                Write(OpCodes.Add);
                WriteStloc(scopeStart);
                Write(OpCodes.Br, whileEndLabel);
                ILG.MarkLabel(ifLabel);

                //else: scopeEnd = foundIndex;
                WriteLdloc(foundIndex);
                WriteStloc(scopeEnd);

                //while end
                ILG.MarkLabel(whileEndLabel);

                //while condition
                WriteLdloc(scopeEnd);
                WriteLdloc(scopeStart);
                Write(OpCodes.Bgt, whileStartLabel);

                //outColor[index] = curve[foundIndex];
                WriteLdOutput(index);
                WriteLdICCData(DataPos);
                WriteLdloc(foundIndex);
                Write(OpCodes.Conv_I);
                WriteLdInt(8);
                Write(OpCodes.Mul);
                Write(OpCodes.Add);
                Write(OpCodes.Ldind_R8);
                Write(OpCodes.Stind_R8);

                #endregion

                ICCData.Add(curve.CurveData);
            }
        }

        #endregion

        #region Parametric Curve

        /// <summary>
        /// Writes the IL code for a parametric curve
        /// </summary>
        /// <param name="data">The entry containing the curve data</param>
        /// <param name="index">The channel index of this curve</param>
        private void WriteParametricCurve(ParametricCurveTagDataEntry data, int index)
        {
            ParametricCurve curve = data.Curve;

            switch (curve.type)
            {
                case 0:
                    WriteParametricCurve_Type0(curve, index);
                    break;
                case 1:
                    WriteParametricCurve_Type1(curve, index);
                    break;
                case 2:
                    WriteParametricCurve_Type2(curve, index);
                    break;
                case 3:
                    WriteParametricCurve_Type3(curve, index);
                    break;
                case 4:
                    WriteParametricCurve_Type4(curve, index);
                    break;

                default:
                    throw new CorruptProfileException("ParametricCurve");
            }
        }

        /// <summary>
        /// Writes the IL code for an inverted parametric curve
        /// </summary>
        /// <param name="data">The entry containing the curve data</param>
        /// <param name="index">The channel index of this curve</param>
        private void WriteParametricCurveInverted(ParametricCurveTagDataEntry data, int index)
        {
            ParametricCurve curve = data.Curve;

            switch (curve.type)
            {
                case 0:
                    WriteParametricCurveInverted_Type0(curve, index);
                    break;
                case 1:
                    WriteParametricCurveInverted_Type1(curve, index);
                    break;
                case 2:
                    WriteParametricCurveInverted_Type2(curve, index);
                    break;
                case 3:
                    WriteParametricCurveInverted_Type3(curve, index);
                    break;
                case 4:
                    WriteParametricCurveInverted_Type4(curve, index);
                    break;

                default:
                    throw new CorruptProfileException("ParametricCurve");
            }
        }


        #region IL for Parametric Curve

        private void WriteParametricCurve_Type0(ParametricCurve curve, int index)
        {
            //outColor[index] = Math.Pow(inColor[index], curve.g);

            WriteLdOutput(index);
            WriteLdInput(index);
            Write(OpCodes.Ldind_R8);
            Write(OpCodes.Ldc_R8, curve.g);
            WriteCallPow();
            Write(OpCodes.Stind_R8);
        }

        private void WriteParametricCurve_Type1(ParametricCurve curve, int index)
        {
            //if (inColor[index] >= -curve.b / curve.a) outColor[index] = Math.Pow(curve.a * inColor[index] + curve.b, curve.g);
            //else outColor[index] = 0;

            var ifLabel = ILG.DefineLabel();
            var elseLabel = ILG.DefineLabel();

            //if (inColor[index] >= -curve.b / curve.a)
            WriteLdInput(index);
            Write(OpCodes.Ldind_R8);
            Write(OpCodes.Ldc_R8, -curve.b / curve.a);
            Write(OpCodes.Blt_Un, ifLabel);

            //outColor[index] = Math.Pow(curve.a * inColor[index] + curve.b, curve.g);
            WriteLdOutput(index);
            Write(OpCodes.Ldc_R8, curve.a);
            WriteLdInput(index);
            Write(OpCodes.Ldind_R8);
            Write(OpCodes.Mul);
            Write(OpCodes.Ldc_R8, curve.b);
            Write(OpCodes.Add);
            Write(OpCodes.Ldc_R8, curve.g);
            WriteCallPow();
            Write(OpCodes.Stind_R8);
            Write(OpCodes.Br, elseLabel);

            //else outColor[index] = 0;
            ILG.MarkLabel(ifLabel);
            WriteLdOutput(index);
            Write(OpCodes.Ldc_R8, 0d);
            Write(OpCodes.Stind_R8);
            ILG.MarkLabel(elseLabel);
        }

        private void WriteParametricCurve_Type2(ParametricCurve curve, int index)
        {
            //if (inColor[index] >= -curve.b / curve.a) outColor[index] = Math.Pow(curve.a * inColor[index] + curve.b, curve.g) + curve.c;
            //else outColor[index] = curve.c;

            var ifLabel = ILG.DefineLabel();
            var elseLabel = ILG.DefineLabel();

            //if (inColor[index] >= -curve.b / curve.a)
            WriteLdInput(index);
            Write(OpCodes.Ldind_R8);
            Write(OpCodes.Ldc_R8, -curve.b / curve.a);
            Write(OpCodes.Blt_Un, ifLabel);

            //outColor[index] = Math.Pow(curve.a * inColor[index] + curve.b, curve.g) + curve.c;
            WriteLdOutput(index);
            Write(OpCodes.Ldc_R8, curve.a);
            WriteLdInput(index);
            Write(OpCodes.Ldind_R8);
            Write(OpCodes.Mul);
            Write(OpCodes.Ldc_R8, curve.b);
            Write(OpCodes.Add);
            Write(OpCodes.Ldc_R8, curve.g);
            WriteCallPow();
            Write(OpCodes.Ldc_R8, curve.c);
            Write(OpCodes.Add);
            Write(OpCodes.Stind_R8);
            Write(OpCodes.Br, elseLabel);

            //else outColor[index] = curve.c;
            ILG.MarkLabel(ifLabel);
            WriteLdOutput(index);
            Write(OpCodes.Ldc_R8, curve.c);
            Write(OpCodes.Stind_R8);
            ILG.MarkLabel(elseLabel);
        }

        private void WriteParametricCurve_Type3(ParametricCurve curve, int index)
        {
            //if (inColor[index] >= curve.d) outColor[index] = Math.Pow(curve.a * inColor[index] + curve.b, curve.g);
            //else outColor[index] = curve.c * inColor[index];

            var ifLabel = ILG.DefineLabel();
            var elseLabel = ILG.DefineLabel();

            //if (inColor[index] >= curve.d)
            WriteLdInput(index);
            Write(OpCodes.Ldind_R8);
            Write(OpCodes.Ldc_R8, curve.d);
            Write(OpCodes.Blt_Un, ifLabel);

            //outColor[index] = Math.Pow(curve.a * inColor[index] + curve.b, curve.g);
            WriteLdOutput(index);
            Write(OpCodes.Ldc_R8, curve.a);
            WriteLdInput(index);
            Write(OpCodes.Ldind_R8);
            Write(OpCodes.Mul);
            Write(OpCodes.Ldc_R8, curve.b);
            Write(OpCodes.Add);
            Write(OpCodes.Ldc_R8, curve.g);
            WriteCallPow();
            Write(OpCodes.Stind_R8);
            Write(OpCodes.Br, elseLabel);

            //else outColor[index] = curve.c * inColor[index];
            ILG.MarkLabel(ifLabel);
            WriteLdOutput(index);
            Write(OpCodes.Ldc_R8, curve.c);
            WriteLdInput(index);
            Write(OpCodes.Ldind_R8);
            Write(OpCodes.Mul);
            Write(OpCodes.Stind_R8);
            ILG.MarkLabel(elseLabel);
        }

        private void WriteParametricCurve_Type4(ParametricCurve curve, int index)
        {
            //if (inColor[index] >= curve.d) outColor[index] = Math.Pow(curve.a * inColor[index] + curve.b, curve.g) + curve.c;
            //else outColor[index] = curve.c * inColor[index] + curve.f;

            var ifLabel = ILG.DefineLabel();
            var elseLabel = ILG.DefineLabel();

            //if (inColor[index] >= curve.d)
            WriteLdInput(index);
            Write(OpCodes.Ldind_R8);
            Write(OpCodes.Ldc_R8, curve.c);
            Write(OpCodes.Blt_Un, ifLabel);

            //outColor[index] = Math.Pow(curve.a * inColor[index] + curve.b, curve.g) + curve.c;
            WriteLdOutput(index);
            Write(OpCodes.Ldc_R8, curve.a);
            WriteLdInput(index);
            Write(OpCodes.Ldind_R8);
            Write(OpCodes.Mul);
            Write(OpCodes.Ldc_R8, curve.b);
            Write(OpCodes.Add);
            Write(OpCodes.Ldc_R8, curve.g);
            WriteCallPow();
            Write(OpCodes.Ldc_R8, curve.c);
            Write(OpCodes.Add);
            Write(OpCodes.Stind_R8);
            Write(OpCodes.Br, elseLabel);

            //else outColor[index] = curve.c * inColor[index] + curve.f;
            ILG.MarkLabel(ifLabel);
            WriteLdOutput(index);
            Write(OpCodes.Ldc_R8, curve.c);
            WriteLdInput(index);
            Write(OpCodes.Ldind_R8);
            Write(OpCodes.Mul);
            Write(OpCodes.Ldc_R8, curve.f);
            Write(OpCodes.Add);
            Write(OpCodes.Stind_R8);
            ILG.MarkLabel(elseLabel);
        }


        private void WriteParametricCurveInverted_Type0(ParametricCurve curve, int index)
        {
            //outColor[index] = Math.Pow(inColor[index], 1 / curve.g);

            WriteLdOutput(index);
            WriteLdInput(index);
            Write(OpCodes.Ldind_R8);
            Write(OpCodes.Ldc_R8, 1d / curve.g);
            WriteCallPow();
            Write(OpCodes.Stind_R8);
        }

        private void WriteParametricCurveInverted_Type1(ParametricCurve curve, int index)
        {
            //if (inColor[index] >= -curve.b / curve.a) outColor[index] = (Math.Pow(curve.a, 1 / curve.g) - curve.b) / inColor[index];
            //else outColor[index] = 0;

            var ifLabel = ILG.DefineLabel();
            var elseLabel = ILG.DefineLabel();

            //if (inColor[index] >= -curve.b / curve.a)
            WriteLdInput(index);
            Write(OpCodes.Ldind_R8);
            Write(OpCodes.Ldc_R8, -curve.b / curve.a);
            Write(OpCodes.Blt_Un, ifLabel);

            //outColor[index] = (Math.Pow(curve.a, 1 / curve.g) - curve.b) / inColor[index];
            WriteLdOutput(index);
            Write(OpCodes.Ldc_R8, Math.Pow(curve.a, 1d / curve.g) - curve.b);
            WriteLdInput(index);
            Write(OpCodes.Ldind_R8);
            Write(OpCodes.Div);
            Write(OpCodes.Stind_R8);
            Write(OpCodes.Br, elseLabel);

            //else outColor[index] = 0;
            ILG.MarkLabel(ifLabel);
            WriteLdOutput(index);
            Write(OpCodes.Ldc_R8, 0d);
            Write(OpCodes.Stind_R8);
            ILG.MarkLabel(elseLabel);
        }

        private void WriteParametricCurveInverted_Type2(ParametricCurve curve, int index)
        {
            //if (inColor[index] >= -curve.b / curve.a) outColor[index] = (Math.Pow(inColor[index] - curve.c, 1 / curve.g) - curve.b) / curve.a;
            //else outColor[index] = curve.c;

            var ifLabel = ILG.DefineLabel();
            var elseLabel = ILG.DefineLabel();

            //if (inColor[index] >= -curve.b / curve.a)
            WriteLdInput(index);
            Write(OpCodes.Ldind_R8);
            Write(OpCodes.Ldc_R8, -curve.b / curve.a);
            Write(OpCodes.Blt_Un, ifLabel);

            //outColor[index] = (Math.Pow(inColor[index] - curve.c, 1 / curve.g) - curve.b) / curve.a;
            WriteLdOutput(index);
            WriteLdInput(index);
            Write(OpCodes.Ldind_R8);
            Write(OpCodes.Ldc_R8, curve.c);
            Write(OpCodes.Sub);
            Write(OpCodes.Ldc_R8, 1d / curve.g);
            WriteCallPow();
            Write(OpCodes.Ldc_R8, curve.b);
            Write(OpCodes.Sub);
            Write(OpCodes.Ldc_R8, curve.a);
            Write(OpCodes.Div);
            Write(OpCodes.Stind_R8);
            Write(OpCodes.Br, elseLabel);

            //else outColor[index] = curve.c;
            ILG.MarkLabel(ifLabel);
            WriteLdOutput(index);
            Write(OpCodes.Ldc_R8, curve.c);
            Write(OpCodes.Stind_R8);
            ILG.MarkLabel(elseLabel);
        }

        private void WriteParametricCurveInverted_Type3(ParametricCurve curve, int index)
        {
            //if (inColor[index] >= curve.d) outColor[index] = (Math.Pow(curve.a, 1 / curve.g) - curve.b) / inColor[index];
            //else outColor[index] = inColor[index] / curve.c;

            var ifLabel = ILG.DefineLabel();
            var elseLabel = ILG.DefineLabel();

            //if (inColor[index] >= curve.d)
            WriteLdInput(index);
            Write(OpCodes.Ldind_R8);
            Write(OpCodes.Ldc_R8, curve.d);
            Write(OpCodes.Blt_Un, ifLabel);

            //outColor[index] = (Math.Pow(curve.a, 1 / curve.g) - curve.b) / inColor[index];
            WriteLdOutput(index);
            Write(OpCodes.Ldc_R8, Math.Pow(curve.a, 1 / curve.g) - curve.b);
            WriteLdInput(index);
            Write(OpCodes.Ldind_R8);
            Write(OpCodes.Div);
            Write(OpCodes.Stind_R8);
            Write(OpCodes.Br, elseLabel);

            //else outColor[index] = inColor[index] / curve.c;
            ILG.MarkLabel(ifLabel);
            WriteLdOutput(index);
            WriteLdInput(index);
            Write(OpCodes.Ldind_R8);
            Write(OpCodes.Ldc_R8, curve.c);
            Write(OpCodes.Div);
            Write(OpCodes.Stind_R8);
            ILG.MarkLabel(elseLabel);
        }

        private void WriteParametricCurveInverted_Type4(ParametricCurve curve, int index)
        {
            //if (inColor[index] >= curve.d) outColor[index] = (Math.Pow(inColor[index] - curve.c, 1 / curve.g) - curve.b) / curve.a;
            //else outColor[index] = (inColor[index] - curve.f) / curve.c;

            var ifLabel = ILG.DefineLabel();
            var elseLabel = ILG.DefineLabel();

            //if (inColor[index] >= curve.d)
            WriteLdInput(index);
            Write(OpCodes.Ldind_R8);
            Write(OpCodes.Ldc_R8, curve.d);
            Write(OpCodes.Blt_Un, ifLabel);

            //outColor[index] = (Math.Pow(curve.a, 1 / curve.g) - curve.b) / inColor[index];
            WriteLdOutput(index);
            WriteLdInput(index);
            Write(OpCodes.Ldind_R8);
            Write(OpCodes.Ldc_R8, curve.c);
            Write(OpCodes.Sub);
            Write(OpCodes.Ldc_R8, 1d / curve.g);
            WriteCallPow();
            Write(OpCodes.Ldc_R8, curve.b);
            Write(OpCodes.Sub);
            Write(OpCodes.Ldc_R8, curve.a);
            Write(OpCodes.Div);
            Write(OpCodes.Stind_R8);
            Write(OpCodes.Br, elseLabel);

            //else outColor[index] = (inColor[index] - curve.f) / curve.c;
            ILG.MarkLabel(ifLabel);
            WriteLdOutput(index);
            WriteLdInput(index);
            Write(OpCodes.Ldind_R8);
            Write(OpCodes.Ldc_R8, curve.f);
            Write(OpCodes.Sub);
            Write(OpCodes.Ldc_R8, curve.c);
            Write(OpCodes.Div);
            Write(OpCodes.Stind_R8);
            ILG.MarkLabel(elseLabel);
        }

        #endregion

        #endregion

        #region One Dimensional Curve

        /// <summary>
        /// Writes the IL code for a one dimensional curve
        /// </summary>
        /// <param name="curve">The curve data</param>
        /// <param name="index">The index of the color channel</param>
        private void WriteOneDimensionalCurve(OneDimensionalCurve curve, int index)
        {
            int length = curve.BreakPoints.Length;

            if (length == 0) throw new InvalidProfileException();
            else if (length > 1)
            {
                var endifLabel = ILG.DefineLabel();
                Label? ifLabel = null;
                for (int i = 0; i < length; i++)
                {
                    if (i != 0) ILG.MarkLabel(ifLabel.Value);

                    //if (inColor[index] <= curve.BreakPoints[i]) *CurveSegmentCode*
                    if (i < length - 1)
                    {
                        ifLabel = ILG.DefineLabel();

                        WriteLdInput(index);
                        Write(OpCodes.Ldind_R8);
                        Write(OpCodes.Ldc_R8, curve.BreakPoints[i]);
                        Write(OpCodes.Bgt_Un, ifLabel.Value);
                        WriteCurveSegment(curve.Segments[i], index);
                        Write(OpCodes.Br, endifLabel);
                    }
                    else { WriteCurveSegment(curve.Segments[i], index); }
                }
                ILG.MarkLabel(endifLabel);
            }
            else WriteCurveSegment(curve.Segments[0], index);
        }

        /// <summary>
        /// Writes the IL code for a curve segment
        /// </summary>
        /// <param name="segment">The curve segment</param>
        /// <param name="index">The index of the color channel</param>
        private void WriteCurveSegment(CurveSegment segment, int index)
        {
            var formula = segment as FormulaCurveElement;
            if (formula != null) WriteFormulaCurveSegment(formula, index);
            else
            {
                var sampled = segment as SampledCurveElement;
                if (sampled != null) WriteSampledCurveSegment(sampled, index);
                else throw new InvalidProfileException();
            }
        }

        /// <summary>
        /// Writes the IL code for a formula curve segment
        /// </summary>
        /// <param name="segment">The formula curve segment</param>
        /// <param name="index">The index of the color channel</param>
        private void WriteFormulaCurveSegment(FormulaCurveElement segment, int index)
        {
            switch (segment.type)
            {
                case 0:
                    WriteFormulaCurveSegment_Type0(segment, index);
                    break;
                case 1:
                    WriteFormulaCurveSegment_Type1(segment, index);
                    break;
                case 2:
                    WriteFormulaCurveSegment_Type2(segment, index);
                    break;
                default:
                    throw new InvalidProfileException("FormulaCurveElement has an unknown type.");
            }
        }

        #region Formula Curve Segment

        private void WriteFormulaCurveSegment_Type0(FormulaCurveElement segment, int index)
        {
            //OutColor[index] = Math.Pow(InColor[index] * segment.a + segment.b, segment.gamma) + segment.c;

            WriteLdOutput(index);
            WriteLdInput(index);
            Write(OpCodes.Ldind_R8);
            Write(OpCodes.Ldc_R8, segment.a);
            Write(OpCodes.Mul);
            Write(OpCodes.Ldc_R8, segment.b);
            Write(OpCodes.Add);
            Write(OpCodes.Ldc_R8, segment.gamma);
            WriteCallPow();
            Write(OpCodes.Ldc_R8, segment.c);
            Write(OpCodes.Add);
            Write(OpCodes.Stind_R8);
        }

        private void WriteFormulaCurveSegment_Type1(FormulaCurveElement segment, int index)
        {
            //OutColor[index] = Math.Log10(segment.b * Math.Pow(InColor[index], segment.gamma) + segment.c) * segment.a + segment.d;

            WriteLdOutput(index);
            Write(OpCodes.Ldind_R8);
            Write(OpCodes.Ldc_R8, segment.b);
            WriteLdInput(index);
            Write(OpCodes.Ldc_R8, segment.gamma);
            WriteCallPow();
            Write(OpCodes.Mul);
            Write(OpCodes.Ldc_R8, segment.c);
            Write(OpCodes.Add);
            MethodInfo m = typeof(Math).GetMethod(nameof(Math.Log10));
            WriteCallMethod(m);
            Write(OpCodes.Ldc_R8, segment.a);
            Write(OpCodes.Mul);
            Write(OpCodes.Ldc_R8, segment.d);
            Write(OpCodes.Add);
            Write(OpCodes.Stind_R8);
        }

        private void WriteFormulaCurveSegment_Type2(FormulaCurveElement segment, int index)
        {
            //OutColor[index] = Math.Pow(segment.b, InColor[index] * segment.c + segment.d) * segment.a + segment.e;

            WriteLdOutput(index);
            Write(OpCodes.Ldind_R8);
            Write(OpCodes.Ldc_R8, segment.b);
            WriteLdInput(index);
            Write(OpCodes.Ldc_R8, segment.c);
            Write(OpCodes.Mul);
            Write(OpCodes.Ldc_R8, segment.d);
            Write(OpCodes.Add);
            WriteCallPow();
            Write(OpCodes.Ldc_R8, segment.a);
            Write(OpCodes.Mul);
            Write(OpCodes.Ldc_R8, segment.e);
            Write(OpCodes.Add);
            Write(OpCodes.Stind_R8);
        }

        #endregion

        /// <summary>
        /// Writes the IL code for a sampled curve segment
        /// </summary>
        /// <param name="segment">The sampled curve segment</param>
        /// <param name="index">The index of the color channel</param>
        private void WriteSampledCurveSegment(SampledCurveElement segment, int index)
        {
            int length = segment.CurveEntries.Length;

            if (length == 0) throw new InvalidProfileException();
            else if (length > 1)
            {
                var endifLabel = ILG.DefineLabel();
                Label? ifLabel = null;
                for (int i = 0; i < length; i++)
                {
                    if (i != 0) ILG.MarkLabel(ifLabel.Value);

                    //if (inColor[index] <= segment.CurveEntries[i]) outColor[index] = segment.CurveEntries[i];
                    if (i < length - 1)
                    {
                        ifLabel = ILG.DefineLabel();

                        WriteLdInput(index);
                        Write(OpCodes.Ldind_R8);
                        Write(OpCodes.Ldc_R8, segment.CurveEntries[i]);
                        Write(OpCodes.Bgt_Un, ifLabel.Value);

                        WriteLdOutput(index);
                        Write(OpCodes.Ldc_R8, segment.CurveEntries[i]);
                        Write(OpCodes.Stind_R8);

                        Write(OpCodes.Br, endifLabel);
                    }
                    else
                    {
                        //outColor[index] = segment.CurveEntries[i];
                        WriteLdOutput(index);
                        Write(OpCodes.Ldc_R8, segment.CurveEntries[i]);
                        Write(OpCodes.Stind_R8);
                    }
                }
                ILG.MarkLabel(endifLabel);
            }
            else
            {
                //outColor[index] = segment.CurveEntries[i];
                WriteLdOutput(index);
                Write(OpCodes.Ldc_R8, segment.CurveEntries[0]);
                Write(OpCodes.Stind_R8);
            }
        }

        #endregion

        #endregion

        #region Write LUT IL Code

        /// <summary>
        /// Writes the IL code for an 8-bit LUT entry
        /// </summary>
        /// <param name="lut">The entry containing the LUT data</param>
        /// <param name="inColor">The input color type</param>
        private void WriteLUT8(Lut8TagDataEntry lut, ColorSpaceType inColor)
        {
            WriteLUT(lut.InputValues, lut.OutputValues, lut.CLUTValues, lut.Matrix, inColor);
        }

        /// <summary>
        /// Writes the IL code for an 16-bit LUT entry
        /// </summary>
        /// <param name="lut">The entry containing the LUT data</param>
        /// <param name="inColor">The input color type</param>
        private void WriteLUT16(Lut16TagDataEntry lut, ColorSpaceType inColor)
        {
            WriteLUT(lut.InputValues, lut.OutputValues, lut.CLUTValues, lut.Matrix, inColor);
        }

        /// <summary>
        /// Writes the IL code for a LUT entry
        /// <param name="InCurve">Input LUT values</param>
        /// <param name="OutCurve">Output LUT values</param>
        /// <param name="clut">CLUT</param>
        /// <param name="matrix">Matrix</param>
        /// <param name="inColor">The input color type</param>
        /// </summary>
        private void WriteLUT(LUT[] InCurve, LUT[] OutCurve, CLUT clut, double[,] matrix, ColorSpaceType inColor)
        {
            //Matrix
            if (inColor == ColorSpaceType.CIEXYZ && matrix != null)
            {
                double[] Matrix = new double[9]
                {
                    matrix[0, 0], matrix[0, 1], matrix[0, 2],
                    matrix[1, 0], matrix[1, 1], matrix[1, 2],
                    matrix[2, 0], matrix[2, 1], matrix[2, 2],
                };

                WriteLdICCData(DataPos);
                WriteLdArg(false);
                WriteCallMultiplyMatrix_3x3_3x1();
                ICCData.Add(Matrix);
                IsFirst = false;
            }

            //Input LUT
            for (int i = 0; i < InCurve.Length; i++) WriteLUT(InCurve[i], i);
            IsFirst = false;
            SwitchTempVar();

            //CLUT
            WriteCLUT(clut);

            IsLast = true;
            //Output LUT
            for (int i = 0; i < OutCurve.Length; i++) WriteLUT(OutCurve[i], i);
        }

        /// <summary>
        /// Writes the IL code for an 8-bit LUT
        /// </summary>
        /// <param name="lut">The LUT for this channel</param>
        /// <param name="index">The channel index</param>
        private void WriteLUT(LUT lut, int index)
        {
            if (lut.Values.Length == 2) WriteAssignSingle(index);
            else
            {
                WriteLUT(lut.Values.Length, index);
                ICCData.Add(lut.Values);
            }
        }


        /// <summary>
        /// Writes the IL code for a LUT A to B entry
        /// </summary>
        /// <param name="entry">The entry containing the LUT data</param>
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

        /// <summary>
        /// Writes the IL code for a LUT B to A entry
        /// </summary>
        /// <param name="entry">The entry containing the LUT data</param>
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
        /// <param name="length">The number of values of the LUT</param>
        /// <param name="index">The channel index</param>
        private void WriteLUT(int length, int index)
        {
            //double idx = inColor[index] * (length - 1);
            //double low = lut[(int)idx];
            //double high = lut[(int)idx + 1];
            //outColor[index] = low + ((high - low) * (idx - (int)idx));

            WriteLdInput(index);
            Write(OpCodes.Ldind_R8);
            Write(OpCodes.Ldc_R8, length - 1d);
            Write(OpCodes.Mul);
            var idx = WriteStloc(typeof(double));

            WriteLdICCData(DataPos);
            WriteLdloc(idx);
            Write(OpCodes.Conv_I4);
            WriteLdInt(8);
            Write(OpCodes.Mul);
            Write(OpCodes.Add);
            Write(OpCodes.Ldind_R8);
            var low = WriteStloc(typeof(double));

            WriteLdICCData(DataPos);
            WriteLdloc(idx);
            Write(OpCodes.Conv_I4);
            WriteLdInt(1);
            Write(OpCodes.Add);
            Write(OpCodes.Conv_I);
            WriteLdInt(8);
            Write(OpCodes.Mul);
            Write(OpCodes.Add);
            Write(OpCodes.Ldind_R8);
            var high = WriteStloc(typeof(double));

            WriteLdOutput(index);
            WriteLdloc(low);
            WriteLdloc(high);
            WriteLdloc(low);
            Write(OpCodes.Sub);
            WriteLdloc(idx);
            WriteLdloc(idx);
            Write(OpCodes.Conv_I4);
            Write(OpCodes.Sub);
            Write(OpCodes.Mul);
            Write(OpCodes.Add);
            Write(OpCodes.Stind_R8);
        }

        //TODO: CLUT lookup could need interpolation

        /// <summary>
        /// Writes the IL code for a CLUT
        /// </summary>
        /// <param name="lut">The CLUT to use</param>
        private void WriteCLUT(CLUT lut)
        {
            #region IL Code

            /* int idx = (int)(inColor[2] * lut.GridPointCount[2])
                       + (int)(inColor[1] * lut.GridPointCount[1]) * lut.GridPointCount[2]
                       + (int)(inColor[0] * lut.GridPointCount[0]) * lut.GridPointCount[1] * lut.GridPointCount[2];

                outColor[0] = data.InICCData[position + 0][idx];
                outColor[1] = data.InICCData[position + 1][idx];
                outColor[2] = data.InICCData[position + 2][idx];
                outColor[3] = data.InICCData[position + 3][idx];
            */

            int gpc = 1;
            for (int i = lut.InputChannelCount - 1; i >= 0; i--)
            {
                WriteLdInput(i);
                Write(OpCodes.Ldind_R8);
                Write(OpCodes.Ldc_R8, (double)lut.GridPointCount[i]);
                Write(OpCodes.Mul);

                //Normal rounding
                //Write(OpCodes.Ldc_R8, 0.5);
                //Write(OpCodes.Add);

                Write(OpCodes.Conv_I4);
                if (i != lut.InputChannelCount - 1)
                {
                    WriteLdInt(gpc);
                    Write(OpCodes.Mul);
                    Write(OpCodes.Add);
                }
                gpc *= lut.GridPointCount[i];
            }
            var idx = WriteStloc(typeof(int));

            for (int i = 0; i < lut.OutputChannelCount; i++)
            {
                WriteLdOutput(i);
                WriteLdICCData(DataPos + i);
                WriteLdloc(idx);
                Write(OpCodes.Conv_I);
                WriteLdInt(8);
                Write(OpCodes.Mul);
                Write(OpCodes.Add);
                Write(OpCodes.Ldind_R8);
                Write(OpCodes.Stind_R8);
            }

            SwitchTempVar();

            #endregion

            #region Reordering Array

            double[][] vals = new double[lut.OutputChannelCount][];
            for (int i = 0; i < vals.Length; i++)
            {
                vals[i] = new double[lut.Values.Length];
                for (int j = 0; j < vals[i].Length; j++)
                {
                    vals[i][j] = lut.Values[j][i];
                }
            }
            ICCData.AddRange(vals);

            #endregion
        }
        
        /// <summary>
        /// Writes the IL code for (3x3 * 3x1) + 3x1 matrix calculation
        /// </summary>
        /// <param name="matrix3x3">The 3x3 matrix</param>
        /// <param name="matrix3x1">The 3x1 matrix</param>
        private void WriteMatrix(double[,] matrix3x3, double[] matrix3x1)
        {
            WriteLdICCData(DataPos);
            WriteLdArg(false);
            WriteCallMultiplyMatrix_3x3_3x1();

            WriteLdICCData(DataPos + 1);
            WriteLdArg(false);
            WriteCallAddMatrix_3x1();

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

        #region Write MultiProcessElement IL Code

        private void WriteMultiProcessElement(MultiProcessElementsTagDataEntry entry)
        {
            for (int i = 0; i < entry.Data.Length; i++)
            {
                var element = entry.Data[i];
                IsFirst = i > 0;
                bool last = i == entry.Data.Length - 1;

                switch (element.Signature)
                {
                    case MultiProcessElementSignature.CurveSet:
                        IsLast = last;
                        WriteMPE_CurveSet(element as CurveSetProcessElement);
                        break;

                    case MultiProcessElementSignature.Matrix:
                        WriteMPE_Matrix(element as MatrixProcessElement, last);
                        break;

                    case MultiProcessElementSignature.CLUT:
                        IsLast = last;
                        WriteMPE_CLUT(element as CLUTProcessElement);
                        break;

                    case MultiProcessElementSignature.bACS:
                    case MultiProcessElementSignature.eACS:
                    default:
                        throw new InvalidProfileException();
                }
            }
        }

        private void WriteMPE_CurveSet(CurveSetProcessElement element)
        {
            for (int i = 0; i < element.Curves.Length; i++) { WriteOneDimensionalCurve(element.Curves[i], i); }
        }

        private void WriteMPE_Matrix(MatrixProcessElement element, bool last)
        {
            WriteLdICCData(DataPos);    //Load Matrix IxO
            WriteLdArg(false);          //Load in and output values
            if (element.InputChannelCount == 3 && element.OutputChannelCount == 3) WriteCallMultiplyMatrix_3x3_3x1();
            else WriteCallMultiplyMatrix(element.InputChannelCount, element.OutputChannelCount, element.OutputChannelCount, 1);
            SwitchTempVar();
            IsLast = last;
            WriteLdInput();             //Load input value (previous output)
            WriteLdICCData(DataPos + 1);//Load Matrix Ox1
            WriteLdOutput();            //Load output value
            if (element.OutputChannelCount == 3) WriteCallAddMatrix_3x1();
            else WriteCallAddMatrix(element.OutputChannelCount, element.OutputChannelCount);

            ICCData.Add(GetMatrix(element.MatrixIxO));
            ICCData.Add(element.MatrixOx1);
        }

        private void WriteMPE_CLUT(CLUTProcessElement element)
        {
            WriteCLUT(element.CLUTValue);
        }

        #endregion

        #region Subroutines

        /// <summary>
        /// Writes the IL code to the appropriate ICC data
        /// </summary>
        /// <param name="position">The position of the wanted ICC data (zero based)</param>
        private void WriteLdICCData(int position)
        {
            string fname;
            if (IsInput) fname = nameof(ConversionData.InICCData);
            else fname = nameof(ConversionData.OutICCData);

            WriteDataLdfld(fname);
            WriteLdPtr(position, sizeof(double*));
            Write(OpCodes.Ldind_I);
        }

        /// <summary>
        /// Gets the matrix from MatrixColumn entries
        /// </summary>
        /// <param name="entries">The entries containing the matrix data</param>
        /// <param name="inverted">True if the matrix should be inverted, false otherwise</param>
        /// <returns>The matrix</returns>
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

        /// <summary>
        /// Converts the multi-dimensional matrix array to a one dimensional array
        /// </summary>
        /// <param name="matrix">the matrix array to convert</param>
        /// <returns>the matrix as a one dimensional array</returns>
        private double[] GetMatrix(double[,] matrix)
        {
            double[] result = new double[matrix.Length];
            fixed (double* rp = result)
            fixed (double* mp = matrix)
            {
                for (int i = 0; i < result.Length; i++) { rp[i] = mp[i]; }
            }
            return result;
        }

        /// <summary>
        /// Gets a specified matrix column and checks its validity
        /// </summary>
        /// <param name="entries">The entries containing the matrix column</param>
        /// <param name="signature">The signature of the wanted matrix column</param>
        /// <returns>The matrix column</returns>
        private XYZNumber GetMatrixColumn(TagDataEntry[] entries, TagSignature signature)
        {
            XYZTagDataEntry entry = entries.FirstOrDefault(t => t.TagSignature == signature) as XYZTagDataEntry;
            if (entry == null || entry.Data == null || entry.Data.Length < 1) throw new InvalidProfileException();
            return entry.Data[0];
        }

        /// <summary>
        /// Checks if a color is the PCS color of either ICC profiles
        /// </summary>
        /// <param name="color">The color to check</param>
        /// <param name="otherProfile">The profile of the other color</param>
        /// <returns>True if it's the PCS color, false if it's the Data color, null if neither</returns>
        private bool? IsPCS(Color color, ICCProfile otherProfile)
        {
            if (color.IsColorICC) return color.IsICCPCS;
            else if (otherProfile != null)
            {
                var tp = color.GetType();
                if (tp == otherProfile.PCSType) return true;
                else if (tp == otherProfile.DataColorspaceType) return false;
                else return null;
            }
            else return null;
        }

        #region Adjust Colors

        /// <summary>
        /// Writes the IL code to adjust the input color for ICC conversion (if necessary)
        /// </summary>
        /// <param name="colorType">The input color type</param>
        private void AdjustInputColor(ColorSpaceType colorType)
        {
            bool adjusted = true;

            switch (colorType)
            {
                case ColorSpaceType.CIELUV:
                    //L / 100
                    //(uv + -Min) / -Min + Max
                    AdjustColor_Div(0, ColorLuv.Max_L);
                    AdjustColor_AddDiv(1, -ColorLuv.Min_u, -ColorLuv.Min_u + ColorLuv.Max_u);
                    AdjustColor_AddDiv(2, -ColorLuv.Min_v, -ColorLuv.Min_v + ColorLuv.Max_v);
                    break;

                case ColorSpaceType.CIELAB:
                    //L / 100
                    //(ab + -Min) / -Min + Max
                    AdjustColor_Div(0, ColorLab.Max_L);
                    AdjustColor_AddDiv(1, -ColorLab.Min_a, -ColorLab.Min_a + ColorLab.Max_a);
                    AdjustColor_AddDiv(2, -ColorLab.Min_b, -ColorLab.Min_b + ColorLab.Max_b);
                    break;

                case ColorSpaceType.HSV:
                    //H / 360
                    AdjustColor_Div(0, ColorHSV.Max_H);
                    break;

                case ColorSpaceType.HLS:
                    //H / 360
                    AdjustColor_Div(0, ColorHSL.Max_H);
                    //Switch S and L channel
                    Adjust_SwitchChannel(1, 2, true);
                    WriteAssignSingle(0);
                    break;

                default:
                    adjusted = false;
                    break;
            }

            if (adjusted)
            {
                SwitchTempVar();
                IsFirst = false;
            }
        }

        /// <summary>
        /// Writes the IL code to adjust the output color back to the range used in this library (if necessary)
        /// </summary>
        /// <param name="colorType">The output color type</param>
        private void AdjustOutputColor(ColorSpaceType colorType)
        {
            IsLast = true;
            switch (colorType)
            {
                case ColorSpaceType.CIELUV:
                    //L * 100
                    //(uv * (-Min + Max)) - -Min
                    AdjustColor_Mul(0, ColorLuv.Max_L);
                    AdjustColor_MulSub(1, -ColorLuv.Min_u + ColorLuv.Max_u, -ColorLuv.Min_u);
                    AdjustColor_MulSub(2, -ColorLuv.Min_v + ColorLuv.Max_v, -ColorLuv.Min_v);
                    break;

                case ColorSpaceType.CIELAB:
                    //L * 100
                    //(ab * (-Min + Max)) - -Min
                    AdjustColor_Mul(0, ColorLab.Max_L);
                    AdjustColor_MulSub(1, -ColorLab.Min_a + ColorLab.Max_a, -ColorLab.Min_a);
                    AdjustColor_MulSub(2, -ColorLab.Min_b + ColorLab.Max_b, -ColorLab.Min_b);
                    break;

                case ColorSpaceType.HSV:
                    //H * 360
                    AdjustColor_Mul(0, ColorHSV.Max_H);
                    break;

                case ColorSpaceType.HLS:
                    //H * 360
                    AdjustColor_Mul(0, ColorHSL.Max_H);
                    //Switch S and L channel
                    Adjust_SwitchChannel(1, 2, false);
                    break;
            }
        }

        /// <summary>
        /// Writes the IL code to adjust a color with division: x / div
        /// </summary>
        /// <param name="index">Zero based channel index</param>
        /// <param name="div">The number to divide with</param>
        private void AdjustColor_Div(int index, double div)
        {
            //OutColor[index] = InColor[index] / div
            WriteLdOutput(index);
            WriteLdInput(index);
            Write(OpCodes.Ldind_R8);
            Write(OpCodes.Ldc_R8, div);
            Write(OpCodes.Div);
            Write(OpCodes.Stind_R8);
        }

        /// <summary>
        /// Writes the IL code to adjust a color with addition and division: (x + add) / div
        /// </summary>
        /// <param name="index">Zero based channel index</param>
        /// <param name="add">The number to add</param>
        /// <param name="div">The number to divide with</param>
        private void AdjustColor_AddDiv(int index, double add, double div)
        {
            //OutColor[index] = (InColor[index] + add) / div
            WriteLdOutput(index);
            WriteLdInput(index);
            Write(OpCodes.Ldind_R8);
            Write(OpCodes.Ldc_R8, add);
            Write(OpCodes.Add);
            Write(OpCodes.Ldc_R8, div);
            Write(OpCodes.Div);
            Write(OpCodes.Stind_R8);
        }

        /// <summary>
        /// Writes the IL code to adjust the output color with multiplication: x * mul
        /// </summary>
        /// <param name="index">Zero based channel index</param>
        /// <param name="mul">The number to multiply with</param>
        private void AdjustColor_Mul(int index, double mul)
        {
            //OutColor[index] *= mul
            WriteLdOutput(index);
            Write(OpCodes.Dup);
            Write(OpCodes.Ldind_R8);
            Write(OpCodes.Ldc_R8, mul);
            Write(OpCodes.Mul);
            Write(OpCodes.Stind_R8);
        }

        /// <summary>
        /// Writes the IL code to adjust the output color with multiplication and subtraction: x * mul - sub
        /// </summary>
        /// <param name="index">Zero based channel index</param>
        /// <param name="mul">The number to multiply with</param>
        /// <param name="sub">The number to subtract</param>
        private void AdjustColor_MulSub(int index, double mul, double sub)
        {
            //OutColor[index] = OutColor[index] * mul - sub
            WriteLdOutput(index);
            WriteLdOutput(index);
            Write(OpCodes.Ldind_R8);
            Write(OpCodes.Ldc_R8, mul);
            Write(OpCodes.Mul);
            Write(OpCodes.Ldc_R8, sub);
            Write(OpCodes.Sub);
            Write(OpCodes.Stind_R8);
        }

        /// <summary>
        /// Writes the IL code to switch two channels
        /// </summary>
        /// <param name="index1">Index of the first channel</param>
        /// <param name="index2">Index of the second channel</param>
        /// <param name="input">True to change an input color, false to change an output color</param>
        private void Adjust_SwitchChannel(int index1, int index2, bool input)
        {
            if (input)
            {
                //OutValues[index1] = InValues[index2];
                WriteLdOutput(index1);
                WriteLdInput(index2);
                Write(OpCodes.Ldind_R8);
                Write(OpCodes.Stind_R8);

                //OutValues[index2] = InValues[index1];
                WriteLdOutput(index2);
                WriteLdInput(index1);
                Write(OpCodes.Ldind_R8);
                Write(OpCodes.Stind_R8);
            }
            else
            {
                //Data.Vars[0] = OutValues[index1];
                WriteDataLdfld(nameof(ConversionData.Vars));
                WriteLdOutput(index1);
                Write(OpCodes.Ldind_R8);
                Write(OpCodes.Stind_R8);

                //OutValues[index1] = OutValues[index2];
                WriteLdOutput(index1);
                WriteLdOutput(index2);
                Write(OpCodes.Ldind_R8);
                Write(OpCodes.Stind_R8);

                //OutValues[index2] = Data.Vars[0];
                WriteLdOutput(index2);
                WriteDataLdfld(nameof(ConversionData.Vars));
                Write(OpCodes.Ldind_R8);
                Write(OpCodes.Stind_R8);
            }
        }

        #endregion

        #endregion
    }
}
