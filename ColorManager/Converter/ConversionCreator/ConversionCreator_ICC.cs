using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Collections.Generic;
using ColorManager.ICC;
using ColorManager.Conversion;
using ColorManager.ICC.Conversion;

namespace ColorManager
{
    public partial class ColorConverter
    {
        protected sealed unsafe class ConversionCreator_ICC : ConversionCreator
        {
            #region Variables

            public Type PCSColor;

            public Type InPCSColor;
            public Type OutPCSColor;

            private Type InType;
            private Type OutType;

            private ICCProfile InProfile
            {
                get { return InColor.ICCProfile; }
            }
            private ICCProfile OutProfile
            {
                get { return OutColor.ICCProfile; }
            }

            private bool? IsInPCS
            {
                get { return IsPCS(InColor, OutColor.ICCProfile); }
            }
            private bool? IsOutPCS
            {
                get { return IsPCS(OutColor, InColor.ICCProfile); }
            }

            private bool IsInput;


            private ConvType ConversionType = ConvType.NotSet;

            private enum ConvType
            {
                /// <summary>
                /// Conversion type is not set => invalid
                /// </summary>
                NotSet,
                /// <summary>
                /// No conversion is necessary
                /// </summary>
                None,
                /// <summary>
                /// No ICC profile available -> normal color conversion
                /// </summary>
                Color,

                //Mixed (use PCSColor)

                /// <summary>
                /// Color conversion from any color to PCS and then ICC conversion from PCS to data
                /// </summary>
                ColorToPCS_ICCToData,
                /// <summary>
                /// ICC conversion from data to PCS and then color conversion to any color
                /// </summary>
                ICCToPCS_ColorToAny,
                /// <summary>
                /// ICC conversion from PCS to PCS and then color conversion to any color
                /// </summary>
                ICCPCSToPCS_ColorToAny,
                /// <summary>
                /// Color conversion to PCS and then ICC conversion from PCS to PCS
                /// </summary>
                ColorToPCS_ICCPCSToPCS,

                //Pure ICC

                /// <summary>
                /// ICC conversion from data to PCS
                /// </summary>
                ICCToPCS,
                /// <summary>
                /// ICC conversion from PCS to data
                /// </summary>
                ICCToData,
                /// <summary>
                /// ICC conversion from first profile data to second profile data
                /// </summary>
                ICC1DataToICC2Data,
                /// <summary>
                /// ICC conversion from first profile data to second profile PCS
                /// </summary>
                ICC1DataToICC2PCS,
                /// <summary>
                /// ICC conversion from first profile PCS to second profile data
                /// </summary>
                ICC1PCSToICC2Data,
                /// <summary>
                /// ICC conversion from first profile PCS to second profile PCS
                /// </summary>
                ICC1PCSToICC2PCS,
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
            /// <param name="parent">The parent <see cref="ConversionCreator"/></param>
            /// <param name="isFirst">States if the input color is the first color</param>
            /// <param name="isLast">States if the output color is the last color</param>
            /// <param name="inColor">The input color</param>
            /// <param name="outColor">The output color</param>
            public ConversionCreator_ICC(ConversionCreator parent, Color inColor, Color outColor, bool isFirst, bool isLast)
                : base(parent, inColor, outColor, isFirst, isLast)
            {
                Init();
            }

            private void Init()
            {
                this.InType = InColor.GetType();
                this.OutType = OutColor.GetType();

                if (InProfile == null && OutProfile == null) ConversionType = ConvType.Color;
                else
                {
                    if (InProfile != null && OutProfile != null) CheckBothProfiles();
                    else if (InProfile != null) CheckConversionType(true);
                    else if (OutProfile != null) CheckConversionType(false);

                    CheckProfileClassValidity();
                }
            }

            public override void SetConversionMethod()
            {
                switch (ConversionType)
                {
                    case ConvType.None:
                        //TODO: handle conversion type "none" (does it have same channels?)
                        break;
                    case ConvType.Color:
                        throw new NotImplementedException();
                        break;
                    case ConvType.ColorToPCS_ICCToData:
                        throw new NotImplementedException();
                        break;
                    case ConvType.ICCToPCS_ColorToAny:
                        throw new NotImplementedException();
                        break;
                    case ConvType.ICCPCSToPCS_ColorToAny:
                        throw new NotImplementedException();
                        break;
                    case ConvType.ColorToPCS_ICCPCSToPCS:
                        throw new NotImplementedException();
                        break;
                    case ConvType.ICCToPCS:
                        ConvertICC_DataPCS();
                        break;
                    case ConvType.ICCToData:
                        ConvertICC_PCSData();
                        break;
                    case ConvType.ICC1DataToICC2Data:
                        throw new NotImplementedException();
                        break;
                    case ConvType.ICC1DataToICC2PCS:
                        throw new NotImplementedException();
                        break;
                    case ConvType.ICC1PCSToICC2Data:
                        throw new NotImplementedException();
                        break;
                    case ConvType.ICC1PCSToICC2PCS:
                        throw new NotImplementedException();
                        break;
                    case ConvType.ICCDataToData:
                        throw new NotImplementedException();
                        break;
                    case ConvType.ICCPCSToPCS:
                        throw new NotImplementedException();
                        break;

                    case ConvType.NotSet:
                    default:
                        throw new ConversionSetupException("Not able to perform conversion");
                }
            }

            #region Profile and Color Checks

            private void CheckConversionType(bool isInput)
            {
                var cl = isInput ? InProfile.Class : OutProfile.Class;
                switch (cl)
                {
                    case ProfileClassName.InputDevice:
                        CheckDevicePCS(isInput);
                        break;
                    case ProfileClassName.DisplayDevice:
                        CheckDevicePCS(isInput);
                        break;
                    case ProfileClassName.OutputDevice:
                        CheckDevicePCS(isInput);
                        break;
                    case ProfileClassName.DeviceLink:
                        CheckDeviceLinkProfile();
                        break;
                    case ProfileClassName.ColorSpace:
                        CheckDevicePCS(isInput);
                        break;
                    case ProfileClassName.Abstract:
                        CheckAbstractProfile();
                        break;
                    case ProfileClassName.NamedColor:
                        CheckDevicePCS(isInput);
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
                else if (IsInPCS == true && IsOutPCS == null)
                {
                    PCSColor = InProfile.PCS;
                    ConversionType = ConvType.ICCPCSToPCS_ColorToAny;
                }
                else if (IsOutPCS == true && IsInPCS == null)
                {
                    PCSColor = OutProfile.PCS;
                    ConversionType = ConvType.ColorToPCS_ICCPCSToPCS;
                }
            }

            private void CheckDevicePCS(bool isInput)
            {
                if (isInput) CheckDevicePCSInput();
                else CheckDevicePCSOutput();
            }

            private void CheckDevicePCSInput()
            {
                if (IsInPCS == false)
                {
                    var spaceType = OutColor.Space.ReferenceWhite.GetType();
                    if (IsOutPCS == true && spaceType == typeof(WhitepointD50)) ConversionType = ConvType.ICCToPCS;
                    else
                    {
                        PCSColor = InProfile.PCS;
                        ConversionType = ConvType.ICCToPCS_ColorToAny;
                    }
                }
                else if (IsInPCS == true) ConversionType = ConvType.Color;
            }

            private void CheckDevicePCSOutput()
            {
                if (IsOutPCS == false)
                {
                    PCSColor = OutProfile.PCS;
                    ConversionType = ConvType.ColorToPCS_ICCToData;
                }
                else if (IsOutPCS == true) ConversionType = ConvType.Color;
            }

            private void CheckBothProfiles()
            {
                if (IsInPCS == null || IsOutPCS == null) return;

                var inPCS = IsInPCS == true;
                var outPCS = IsOutPCS == true;

                if (InProfile == OutProfile)
                {
                    if (!inPCS && !outPCS) ConversionType = ConvType.None;
                    else if (inPCS && outPCS) ConversionType = ConvType.None;
                    else if (!inPCS && outPCS) ConversionType = ConvType.ICCToPCS;
                    else if (inPCS && !outPCS) ConversionType = ConvType.ICCToData;
                }
                else
                {
                    InPCSColor = InProfile.PCS;
                    OutPCSColor = OutProfile.PCS;

                    if (!inPCS && !outPCS) ConversionType = ConvType.ICC1DataToICC2Data;
                    else if (!inPCS && outPCS) ConversionType = ConvType.ICC1DataToICC2PCS;
                    else if (inPCS && !outPCS) ConversionType = ConvType.ICC1PCSToICC2Data;
                    else if (inPCS && outPCS) ConversionType = ConvType.ICC1PCSToICC2PCS;
                }
            }

            private void CheckProfileClassValidity()
            {
                //Device Link needs both colors to have a profile and the profiles need to be the same
                var dl = ProfileClassName.DeviceLink;
                if ((InProfile != null && InProfile.Class == dl) || (OutProfile != null && OutProfile.Class == dl))
                {
                    bool valid = true;
                    if (InProfile == null || OutProfile == null) valid = false;
                    else if (InProfile != OutProfile) valid = false;

                    if (!valid) throw new ConversionSetupException("Profile type \"Device Link\" needs both colors to have the same ICC profile");
                }
            }

            #endregion

            #region Profile Conversion Type

            private bool IsNComponentLUT(ICCProfile profile)
            {
                switch (profile.Class)
                {
                    case ProfileClassName.InputDevice:
                        return profile.HasTag(TagSignature.AToB0);

                    case ProfileClassName.DisplayDevice:
                        return profile.HasTag(TagSignature.AToB0) && profile.HasTag(TagSignature.BToA0);

                    case ProfileClassName.OutputDevice:
                        if (profile.DataColorspace == typeof(ColorX) && !profile.HasTag(TagSignature.ColorantTable)) return false;
                        return profile.HasTag(TagSignature.AToB0) && profile.HasTag(TagSignature.BToA0)
                            && profile.HasTag(TagSignature.AToB1) && profile.HasTag(TagSignature.BToA1)
                            && profile.HasTag(TagSignature.AToB2) && profile.HasTag(TagSignature.BToA2)
                            && profile.HasTag(TagSignature.Gamut);

                    default:
                        return false;
                }
            }

            private bool IsMonochrome(ICCProfile profile)
            {
                switch (profile.Class)
                {
                    case ProfileClassName.InputDevice:
                    case ProfileClassName.DisplayDevice:
                    case ProfileClassName.OutputDevice:
                        return profile.HasTag(TagSignature.GrayTRC);

                    default:
                        return false;
                }
            }

            private bool IsThreeComponentMatrix(ICCProfile profile)
            {
                switch (profile.Class)
                {
                    case ProfileClassName.InputDevice:
                    case ProfileClassName.DisplayDevice:
                        return profile.HasTag(TagSignature.RedMatrixColumn)
                            && profile.HasTag(TagSignature.GreenMatrixColumn)
                            && profile.HasTag(TagSignature.BlueMatrixColumn)
                            && profile.HasTag(TagSignature.RedTRC)
                            && profile.HasTag(TagSignature.GreenTRC)
                            && profile.HasTag(TagSignature.BlueTRC);

                    default:
                        return false;
                }
            }

            #endregion

            //TODO: include chromatic adaption if CATag is existing
            //TODO: check for media white/black point tag and use it if existing
            //TODO: some colors have to adjusted before and after conversion

            #region Conversion

            //TODO: move this method to the ColorConverter class
            private void ConvertColor(Color inColor, Color outColor, bool first, bool last)
            {
                var cc = new ConversionCreator_Color(this, inColor, outColor, first, last);
                cc.SetConversionMethod();
            }


            #region PCS -> Data

            private void ConvertICC_PCSData()
            {
                var profile = InProfile ?? OutProfile;
                if (profile == null) throw new ConversionSetupException();

                var entries = profile.GetConversionTag(false);
                if (entries == null) throw new InvalidProfileException();

                double[][] data;

                if (IsNComponentLUT(profile)) data = ConvertICC_PCSData_LUT(entries[0], profile.PCSType);
                else if (IsThreeComponentMatrix(profile)) data = ConvertICC_PCSData_Matrix(entries);
                else if (IsMonochrome(profile))
                {
                    //TODO: gray Lab needs adjustment: val/100
                    //if (profile.PCSType == ColorSpaceType.CIELAB) { //val/= 100d; }

                    data = ConvertICC_PCSData_Monochrome(entries);
                }
                else throw new InvalidProfileException();

                if (data != null) Data.SetICCData(new ICCData(data), null);

                if (IsLastG) CMIL.Emit(OpCodes.Ret);
            }

            private double[][] ConvertICC_PCSData_LUT(TagDataEntry entry, ColorSpaceType inColor)
            {
                switch (entry.Signature)
                {
                    case TypeSignature.Lut8:
                        return WriteLUT8(entry as Lut8TagDataEntry, true, inColor);
                    case TypeSignature.Lut16:
                        return WriteLUT16(entry as Lut16TagDataEntry, true, inColor);
                    case TypeSignature.LutBToA:
                        return WriteLutBToA(entry as LutBToATagDataEntry, true);

                    default:
                        throw new InvalidProfileException();
                }
            }

            private double[][] ConvertICC_PCSData_Matrix(TagDataEntry[] entries)
            {
                List<double[]> convData = new List<double[]>();

                convData.Add(GetMatrix(entries, true));
                WriteLdICCData(true, 0);//Load Matrix
                WriteLdArg();//Load in and output values
                var m = typeof(UMath).GetMethod("MultiplyMatrix_3x3_3x1");//TODO: make typesafe
                WriteMethodCall(m, false);

                IsFirst = false;
                IsLast = true;
                convData.Add(WriteTRC(entries, TagSignature.RedTRC, true, 0, 1, true));
                convData.Add(WriteTRC(entries, TagSignature.GreenTRC, true, 1, 2, true));
                convData.Add(WriteTRC(entries, TagSignature.BlueTRC, true, 2, 3, true));

                return convData.ToArray();
            }

            private double[][] ConvertICC_PCSData_Monochrome(TagDataEntry[] entries)
            {
                IsLast = true;
                return new double[][] { WriteTRC(entries, TagSignature.GrayTRC, true, 0, 0, true) };
            }

            #endregion

            #region Data -> PCS

            private void ConvertICC_DataPCS()
            {
                var profile = InProfile ?? OutProfile;
                if (profile == null) throw new ConversionSetupException();

                var entries = profile.GetConversionTag(true);
                if (entries == null) throw new InvalidProfileException();

                double[][] data;

                if (IsNComponentLUT(profile)) data = ConvertICC_DataPCS_LUT(entries[0], profile.DataColorspaceType);
                else if (IsThreeComponentMatrix(profile)) data = ConvertICC_DataPCS_Matrix(entries);
                else if (IsMonochrome(profile))
                {
                    //TODO: gray Lab needs adjustment: val/100
                    //if (profile.PCSType == ColorSpaceType.CIELAB) { //val/= 100d; }

                    data = ConvertICC_DataPCS_Monochrome(entries);
                }
                else throw new InvalidProfileException();

                if (data != null) Data.SetICCData(new ICCData(data), null);

                if (IsLastG) CMIL.Emit(OpCodes.Ret);
            }

            private double[][] ConvertICC_DataPCS_LUT(TagDataEntry entry, ColorSpaceType inColor)
            {
                switch (entry.Signature)
                {
                    case TypeSignature.Lut8:
                        return WriteLUT8(entry as Lut8TagDataEntry, true, inColor);
                    case TypeSignature.Lut16:
                        return WriteLUT16(entry as Lut16TagDataEntry, true, inColor);
                    case TypeSignature.LutAToB:
                        return WriteLutAToB(entry as LutAToBTagDataEntry, true);

                    default:
                        throw new InvalidProfileException();
                }
            }

            private double[][] ConvertICC_DataPCS_Matrix(TagDataEntry[] entries)
            {
                List<double[]> convData = new List<double[]>();

                convData.Add(WriteTRC(entries, TagSignature.RedTRC, false, 0, 0, true));
                convData.Add(WriteTRC(entries, TagSignature.GreenTRC, false, 1, 1, true));
                convData.Add(WriteTRC(entries, TagSignature.BlueTRC, false, 2, 2, true));

                IsTempVar1 = !IsTempVar1;
                IsFirst = false;
                IsLast = true;

                WriteLdICCData(true, 3);//Load Matrix
                WriteLdArg();//Load in and output values
                var m = typeof(UMath).GetMethod("MultiplyMatrix_3x3_3x1");//TODO: make typesafe
                WriteMethodCall(m, false);
                convData.Add(GetMatrix(entries, false));

                return convData.ToArray();
            }

            private double[][] ConvertICC_DataPCS_Monochrome(TagDataEntry[] entries)
            {
                IsLast = true;
                return new double[][] { WriteTRC(entries, TagSignature.GrayTRC, false, 0, 0, true) };
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
                var Mr = GetMatrixColumn(entries, TagSignature.RedMatrixColumn);
                var Mg = GetMatrixColumn(entries, TagSignature.GreenMatrixColumn);
                var Mb = GetMatrixColumn(entries, TagSignature.BlueMatrixColumn);

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
                if (entry == null || entry.Data == null || entry.Data.Length == 0) throw new InvalidProfileException();
                return entry.Data[0];
            }

            #endregion

            #endregion

            #region Write IL Code

            #region Curves

            /// <summary>
            /// Writes the IL code for a TRC curve
            /// </summary>
            /// <param name="entries">The entries containing the TRC data</param>
            /// <param name="signature">The TRC curve signature</param>
            /// <param name="inverted">True if the curve should be inverted, false otherwise</param>
            /// <param name="index">The index of the color channel</param>
            /// <param name="position">The position of the ICCData</param>
            /// <param name="input">True if the ICCData is from the input profile, false otherwise</param>
            /// <returns>The ICCData for this curve</returns>
            private double[] WriteTRC(TagDataEntry[] entries, TagSignature signature, bool inverted, int index, int position, bool input)
            {
                var trc = entries.FirstOrDefault(t => t.TagSignature == signature);
                if (trc == null) throw new InvalidProfileException();

                return WriteCurve(trc, inverted, index, position, input);
            }

            private double[][] WriteCurve(TagDataEntry[] entries, bool inverted, int position, bool input)
            {
                double[][] data = new double[entries.Length][];

                for (int i = 0; i < entries.Length; i++) data[i] = WriteCurve(entries[0], inverted, i, position + i, input);
                IsTempVar1 = !IsTempVar1;

                return data;
            }

            private double[] WriteCurve(TagDataEntry curve, bool inverted, int index, int position, bool input)
            {
                double[] values;
                if (curve.Signature == TypeSignature.Curve) values = WriteCurve(curve as CurveTagDataEntry, inverted, index, position, input);
                else if (curve.Signature == TypeSignature.ParametricCurve) values = WriteCurve(curve as ParametricCurveTagDataEntry, inverted, index, position, input);
                else throw new InvalidProfileException();
                return values;
            }

            /// <summary>
            /// Writes the IL code for a curve
            /// </summary>
            /// <param name="curve">The entry containing the curve data</param>
            /// <param name="inverted">True if the curve should be inverted, false otherwise</param>
            /// <param name="index">The index of the color channel</param>
            /// <param name="position">The position of the ICCData</param>
            /// <param name="input">True if the ICCData is from the input profile, false otherwise</param>
            /// <returns>The ICCData for this curve</returns>
            private double[] WriteCurve(CurveTagDataEntry curve, bool inverted, int index, int position, bool input)
            {
                if (curve == null) throw new InvalidProfileException();

                if (inverted) return WriteCurveInverted(curve, index, position, input);
                else return WriteCurve(curve, index, position, input);
            }

            /// <summary>
            /// Writes the IL code for a parametric curve
            /// </summary>
            /// <param name="curve">The entry containing the curve data</param>
            /// <param name="inverted">True if the curve should be inverted, false otherwise</param>
            /// <param name="index">The index of the color channel</param>
            /// <param name="position">The position of the ICCData</param>
            /// <param name="input">True if the ICCData is from the input profile, false otherwise</param>
            /// <returns>The ICCData for this curve</returns>
            private double[] WriteCurve(ParametricCurveTagDataEntry curve, bool inverted, int index, int position, bool input)
            {
                if (curve == null) throw new InvalidProfileException();

                if (inverted) return WriteParametricCurveInverted(curve.Curve);
                else return WriteParametricCurve(curve.Curve);
            }


            /// <summary>
            /// Writes the IL code for a non-inverted curve
            /// </summary>
            /// <param name="curve">The entry containing the curve data</param>
            /// <param name="index">The index of the color channel</param>
            /// <param name="position">The position of the ICCData</param>
            /// <param name="input">True if the ICCData is from the input profile, false otherwise</param>
            /// <returns>The ICCData for this curve</returns>
            private double[] WriteCurve(CurveTagDataEntry curve, int index, int position, bool input)
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
                    WriteLdICCData(input, position);
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

                return curve.CurveData;
            }

            /// <summary>
            /// Writes the IL code for a inverted curve
            /// </summary>
            /// <param name="curve">The entry containing the curve data</param>
            /// <param name="index">The index of the color channel</param>
            /// <param name="position">The position of the ICCData</param>
            /// <param name="input">True if the ICCData is from the input profile, false otherwise</param>
            /// <returns>The ICCData for this curve</returns>
            private double[] WriteCurveInverted(CurveTagDataEntry curve, int index, int position, bool input)
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
                    CMIL.Emit(OpCodes.Stloc_0);
                    //int scopeEnd = curve.Length - 1;
                    CMIL.Emit(OpCodes.Ldc_I4, curve.CurveData.Length - 1d);
                    CMIL.Emit(OpCodes.Stloc_1);
                    //int foundIndex = 0;
                    CMIL.Emit(OpCodes.Ldc_I4_0);
                    CMIL.Emit(OpCodes.Stloc_2);

                    //while start
                    CMIL.Emit(OpCodes.Br, whileEndLabel);
                    CMIL.MarkLabel(whileStartLabel);

                    //foundIndex = (scopeStart + scopeEnd) / 2;
                    CMIL.Emit(OpCodes.Ldloc_0);
                    CMIL.Emit(OpCodes.Ldloc_1);
                    CMIL.Emit(OpCodes.Add);
                    CMIL.Emit(OpCodes.Ldc_I4_2);
                    CMIL.Emit(OpCodes.Div);
                    CMIL.Emit(OpCodes.Stloc_2);

                    //if (inColor[index] > curve[index])
                    WriteLdInput();
                    WriteLdPtr(index);
                    CMIL.Emit(OpCodes.Ldind_R8);
                    WriteLdICCData(input, position);
                    CMIL.Emit(OpCodes.Ldloc_2);
                    CMIL.Emit(OpCodes.Conv_I);
                    CMIL.Emit(OpCodes.Ldc_I4_8);
                    CMIL.Emit(OpCodes.Mul);
                    CMIL.Emit(OpCodes.Add);
                    CMIL.Emit(OpCodes.Ldind_R8);
                    CMIL.Emit(OpCodes.Ble_Un, ifLabel);

                    //if == true: scopeStart = foundIndex + 1;
                    CMIL.Emit(OpCodes.Ldloc_2);
                    CMIL.Emit(OpCodes.Ldc_I4_1);
                    CMIL.Emit(OpCodes.Add);
                    CMIL.Emit(OpCodes.Stloc_0);
                    CMIL.Emit(OpCodes.Br, whileEndLabel);
                    CMIL.MarkLabel(ifLabel);

                    //else: scopeEnd = index;
                    CMIL.Emit(OpCodes.Ldloc_2);
                    CMIL.Emit(OpCodes.Stloc_1);

                    //while end
                    CMIL.MarkLabel(whileEndLabel);

                    //while condition
                    CMIL.Emit(OpCodes.Ldloc_1);
                    CMIL.Emit(OpCodes.Ldloc_0);
                    CMIL.Emit(OpCodes.Bgt, whileStartLabel);

                    //outColor[index] = curve[index];
                    WriteLdOutput();
                    WriteLdPtr(index);
                    WriteLdICCData(input, position);
                    CMIL.Emit(OpCodes.Ldloc_2);
                    CMIL.Emit(OpCodes.Conv_I);
                    CMIL.Emit(OpCodes.Ldc_I4_8);
                    CMIL.Emit(OpCodes.Mul);
                    CMIL.Emit(OpCodes.Add);
                    CMIL.Emit(OpCodes.Ldind_R8);
                    CMIL.Emit(OpCodes.Stind_R8);

                    #endregion
                }

                return curve.CurveData;
            }


            private double[] WriteParametricCurve(ParametricCurve curve)
            {
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

                return new double[0];
            }

            private double[] WriteParametricCurveInverted(ParametricCurve curve)
            {
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

                return new double[0];
            }


            private double[] WriteOneDimensionalCurve(OneDimensionalCurve curve)
            {
                double input = 0;//TODO: IL for one-dimensional curve

                int idx = -1;
                if (curve.Segments.Length != 1)
                {
                    for (int i = 0; i < curve.BreakPoints.Length; i++) { if (input <= curve.BreakPoints[i]) { idx = i; break; } }
                    if (idx == -1) { idx = curve.Segments.Length - 1; }
                }
                else { idx = 0; }

                //var result = curve.Segments[idx].GetValue(input);
                WriteCurveSegment(curve.Segments[idx]);


                return new double[0];
            }

            private double[] WriteCurveSegment(CurveSegment segment)
            {
                var formula = segment as FormulaCurveElement;
                if (formula != null) return WriteFormulaCurveSegment(formula);

                var sampled = segment as SampledCurveElement;
                if (sampled != null) return WriteSampledCurveESegment(sampled);

                throw new InvalidProfileException();
            }

            private double[] WriteFormulaCurveSegment(FormulaCurveElement segment)
            {
                //TODO: WriteFormulaCurveSegment

                /*switch (type)
                {
                    case 0: return Math.Pow(a * X + b, gamma) + c;
                    case 1: return a * Math.Log10(b * Math.Pow(X, gamma) + c) + d;
                    case 2: return a * Math.Pow(b, c * X + d) + e;
                    default: return 0;
                }*/
                return null;
            }

            private double[] WriteSampledCurveESegment(SampledCurveElement segment)
            {
                //TODO: WriteSampledCurveESegment

                /*double t = X * (CurveEntries.Length - 1);
                if (t % 1 != 0)
                {
                    int i = (int)Math.Floor(t);
                    return CurveEntries[i] + ((CurveEntries[i + 1] - CurveEntries[i]) * (t % 1));
                }
                else { return CurveEntries[(int)t]; }*/

                return null;
            }

            #endregion

            //TODO: converting CMYK->Lab with LUT16 seems to be wrong (maybe LUT8 behaves the same way)
            //TODO: LutAtoB and LutBtoA has to be tested (seems to have a problem with in/output(i.e. IsTempVar1) usage)

            #region LUT

            /// <summary>
            /// Writes the IL code for an 8-bit LUT entry
            /// </summary>
            /// <param name="lut">The entry containing the LUT data</param>
            /// <param name="input">True if the ICCData is from the input profile, false otherwise</param>
            /// <param name="inColor">The input color type</param>
            /// <returns>The ICCData for this LUT entry</returns>
            private double[][] WriteLUT8(Lut8TagDataEntry lut, bool input, ColorSpaceType inColor)
            {
                List<double[]> convData = new List<double[]>();
                LUT8[] InCurve = lut.InputValues;
                CLUT8 CLUT = lut.CLUTValues;
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
                    WriteLdICCData(input, 0);
                    WriteLdArg();
                    var m = typeof(UMath).GetMethod("MultiplyMatrix_3x3_3x1");//TODO: make typesafe
                    WriteMethodCall(m, false);
                    convData.Add(Matrix);
                    IsFirst = false;
                }

                //Input LUT
                for (int i = 0; i < InCurve.Length; i++)
                {
                    var data = WriteLUT8(InCurve[i], i, convData.Count, input, true, inColor);
                    if (data != null) convData.Add(data);
                }
                IsFirst = false;
                IsTempVar1 = !IsTempVar1;

                //CLUT
                convData.AddRange(WriteCLUT(CLUT, convData.Count, input));

                IsLast = true;
                //Output LUT
                for (int i = 0; i < OutCurve.Length; i++)
                {
                    var data = WriteLUT8(OutCurve[i], i, convData.Count, input, false, inColor);
                    if (data != null) convData.Add(data);
                }

                return convData.ToArray();

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
            private double[] WriteLUT8(LUT8 lut, int index, int position, bool input, bool adjust, ColorSpaceType inColor)
            {
                if (lut.Values.Length == 2)
                {
                    WriteAssignSingle(index);
                    return null;
                }
                else
                {
                    WriteLUT(lut.Values.Length, index, position, input, adjust, inColor);
                    return lut.Values.Select(t => t / 255d).ToArray();
                }
            }


            /// <summary>
            /// Writes the IL code for an 16-bit LUT entry
            /// </summary>
            /// <param name="lut">The entry containing the LUT data</param>
            /// <param name="input">True if the ICCData is from the input profile, false otherwise</param>
            /// <param name="inColor">The input color type</param>
            /// <returns>The ICCData for this LUT entry</returns>
            private double[][] WriteLUT16(Lut16TagDataEntry lut, bool input, ColorSpaceType inColor)
            {
                List<double[]> convData = new List<double[]>();
                LUT16[] InCurve = lut.InputValues;
                CLUT16 CLUT = lut.CLUTValues;
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
                    WriteLdICCData(input, 0);
                    WriteLdArg();
                    var m = typeof(UMath).GetMethod("MultiplyMatrix_3x3_3x1");//TODO: make typesafe
                    WriteMethodCall(m, false);
                    convData.Add(Matrix);
                    IsFirst = false;
                }

                //Input LUT
                for (int i = 0; i < InCurve.Length; i++)
                {
                    var data = WriteLUT16(InCurve[i], i, convData.Count, input, true, inColor);
                    if (data != null) convData.Add(data);
                }
                IsFirst = false;
                IsTempVar1 = !IsTempVar1;

                //CLUT
                convData.AddRange(WriteCLUT(CLUT, convData.Count, input));

                IsLast = true;
                //Output LUT
                for (int i = 0; i < OutCurve.Length; i++)
                {
                    var data = WriteLUT16(OutCurve[i], i, convData.Count, input, false, inColor);
                    if (data != null) convData.Add(data);
                }

                return convData.ToArray();
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
            private double[] WriteLUT16(LUT16 lut, int index, int position, bool input, bool adjust, ColorSpaceType inColor)
            {
                if (lut.Values.Length == 2)
                {
                    WriteAssignSingle(index);
                    return null;
                }
                else
                {
                    WriteLUT(lut.Values.Length, index, position, input, adjust, inColor);
                    return lut.Values.Select(t => t / 65535d).ToArray();
                }
            }


            private double[][] WriteLutAToB(LutAToBTagDataEntry entry, bool input)
            {
                List<double[]> convData = new List<double[]>();

                bool ca = entry.CurveA != null;
                bool cb = entry.CurveB != null;
                bool cm = entry.CurveM != null;
                bool matrix = entry.Matrix3x1 != null && entry.Matrix3x3 != null;
                bool clut = entry.CLUTValues != null;

                if (ca && clut && cm && matrix && cb)
                {
                    convData.AddRange(WriteCurve(entry.CurveA, false, convData.Count, input));
                    IsFirst = false;
                    convData.AddRange(WriteCLUT(entry.CLUTValues, convData.Count, input));
                    convData.AddRange(WriteCurve(entry.CurveM, false, convData.Count, input));
                    convData.AddRange(WriteMatrix(entry.Matrix3x3, entry.Matrix3x1, convData.Count, input));
                    IsLast = true;
                    convData.AddRange(WriteCurve(entry.CurveB, false, convData.Count, input));
                }
                else if (ca && clut && cb)
                {
                    convData.AddRange(WriteCurve(entry.CurveA, false, convData.Count, input));
                    IsFirst = false;
                    WriteCLUT(entry.CLUTValues, convData.Count, input);
                    IsLast = true;
                    convData.AddRange(WriteCurve(entry.CurveB, false, convData.Count, input));
                }
                else if (cm && matrix && cb)
                {
                    convData.AddRange(WriteCurve(entry.CurveM, false, convData.Count, input));
                    IsFirst = false;
                    convData.AddRange(WriteMatrix(entry.Matrix3x3, entry.Matrix3x1, convData.Count, input));
                    IsLast = true;
                    convData.AddRange(WriteCurve(entry.CurveB, false, convData.Count, input));
                }
                else if (cb)
                {
                    IsLast = true;
                    convData.AddRange(WriteCurve(entry.CurveB, false, convData.Count, input));
                }
                else throw new InvalidProfileException("AToB tag has an invalid configuration");

                return convData.ToArray();
            }

            private double[][] WriteLutBToA(LutBToATagDataEntry entry, bool input)
            {
                List<double[]> convData = new List<double[]>();

                bool ca = entry.CurveA != null;
                bool cb = entry.CurveB != null;
                bool cm = entry.CurveM != null;
                bool matrix = entry.Matrix3x1 != null && entry.Matrix3x3 != null;
                bool clut = entry.CLUTValues != null;

                if (cb && matrix && cm && clut && ca)
                {
                    convData.AddRange(WriteCurve(entry.CurveB, false, convData.Count, input));
                    IsFirst = false;
                    convData.AddRange(WriteMatrix(entry.Matrix3x3, entry.Matrix3x1, convData.Count, input));
                    convData.AddRange(WriteCurve(entry.CurveM, false, convData.Count, input));
                    convData.AddRange(WriteCLUT(entry.CLUTValues, convData.Count, input));
                    IsLast = true;
                    convData.AddRange(WriteCurve(entry.CurveA, false, convData.Count, input));
                }
                else if (cb && clut && ca)
                {
                    convData.AddRange(WriteCurve(entry.CurveB, false, convData.Count, input));
                    IsFirst = false;
                    convData.AddRange(WriteCLUT(entry.CLUTValues, convData.Count, input));
                    IsLast = true;
                    convData.AddRange(WriteCurve(entry.CurveA, false, convData.Count, input));
                }
                else if (cb && matrix && cm)
                {
                    convData.AddRange(WriteCurve(entry.CurveB, false, convData.Count, input));
                    IsFirst = false;
                    convData.AddRange(WriteMatrix(entry.Matrix3x3, entry.Matrix3x1, convData.Count, input));
                    IsLast = true;
                    convData.AddRange(WriteCurve(entry.CurveM, false, convData.Count, input));
                }
                else if (cb)
                {
                    IsLast = true;
                    convData.AddRange(WriteCurve(entry.CurveB, false, convData.Count, input));
                }
                else throw new InvalidProfileException("BToA tag has an invalid configuration");

                return convData.ToArray();
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
            private void WriteLUT(int length, int index, int position, bool input, bool adjust, ColorSpaceType inColor)
            {
                //outColor[index] = lut.Values[(int)((inColor[index] * lut.Length - 1) + 0.5)];
                WriteLdOutput();
                WriteLdPtr(index);
                WriteLdICCData(input, position);
                WriteLdInput();
                WriteLdPtr(index);

                CMIL.Emit(OpCodes.Ldind_R8);
                CMIL.Emit(OpCodes.Ldc_R8, length - 1d);
                CMIL.Emit(OpCodes.Mul);
                if (adjust && inColor == ColorSpaceType.CIELAB)
                {
                    if (index == 0) CMIL.Emit(OpCodes.Ldc_R8, 100d);
                    else
                    {
                        CMIL.Emit(OpCodes.Ldc_R8, 256d);
                        CMIL.Emit(OpCodes.Add);
                        CMIL.Emit(OpCodes.Ldc_R8, 512d);
                    }
                    CMIL.Emit(OpCodes.Div);
                }
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
            private double[][] WriteCLUT(CLUT lut, int position, bool input)
            {
                #region IL Code

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
                    WriteLdICCData(input, position + i);
                    WriteLdloc(idx);
                    CMIL.Emit(OpCodes.Conv_I);
                    CMIL.Emit(OpCodes.Ldc_I4_8);
                    CMIL.Emit(OpCodes.Mul);
                    CMIL.Emit(OpCodes.Add);
                    CMIL.Emit(OpCodes.Ldind_R8);
                    CMIL.Emit(OpCodes.Stind_R8);
                }

                IsTempVar1 = !IsTempVar1;

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
                    return vals;
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
                    return vals;
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
                    return vals;
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
            private double[][] WriteMatrix(double[,] matrix3x3, double[] matrix3x1, int position, bool input)
            {
                WriteLdICCData(input, position);
                WriteLdArg();
                var m = typeof(UMath).GetMethod("MultiplyMatrix_3x3_3x1");//TODO: make typesafe
                WriteMethodCall(m, false);

                WriteLdICCData(input, position + 1);
                WriteLdArg();
                m = typeof(UMath).GetMethod("AddMatrix_3x1");//TODO: make typesafe
                WriteMethodCall(m, false);

                double[] m3x3 = new double[9]
                {
                    matrix3x3[0, 0], matrix3x3[0, 1], matrix3x3[0, 2],
                    matrix3x3[1, 0], matrix3x3[1, 1], matrix3x3[1, 2],
                    matrix3x3[2, 0], matrix3x3[2, 1], matrix3x3[2, 2],                        
                };

                return new double[][] { m3x3, matrix3x1 };
            }

            #endregion


            #region Subroutines

            private void WriteLdICCData(bool input, int position)
            {
                string fname = (input ? "In" : "Out") + "ICCData";//TODO: make typesafe

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
}
