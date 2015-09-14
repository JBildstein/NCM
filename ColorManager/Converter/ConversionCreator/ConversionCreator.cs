using System;
using System.Reflection;
using System.Reflection.Emit;

namespace ColorManager.Conversion
{
    /// <summary>
    /// Factory to create a conversion method (abstract class)
    /// </summary>
    public abstract class ConversionCreator
    {
        #region Variables

        /// <summary>
        /// States if it's the first conversion within this <see cref="ConversionCreator"/>
        /// </summary>
        protected virtual bool IsFirst
        {
            get { return _IsFirst && IsFirstG; }
            set { _IsFirst = value; }
        }
        /// <summary>
        /// States if it's the last conversion within this <see cref="ConversionCreator"/>
        /// </summary>
        protected virtual bool IsLast
        {
            get { return _IsLast && IsLastG; }
            set { _IsLast = value; }
        }
        /// <summary>
        /// States if it's the first conversion globally
        /// </summary>
        protected bool IsFirstG
        {
            get { return _IsFirstG; }
        }
        /// <summary>
        /// States if it's the last conversion globally
        /// </summary>
        protected bool IsLastG
        {
            get { return _IsLastG; }
        }

        /// <summary>
        /// States which temporary variable for the color values should be used
        /// </summary>
        private bool IsTempVar1 = true;

        private bool _IsFirst = true;
        private bool _IsLast = false;
        private bool _IsFirstG = true;
        private bool _IsLastG = true;

        /// <summary>
        /// The ILGenerator that is used to create the conversion method.
        /// <para>Valid when <see cref="SetConversionMethod"/> is called</para>
        /// </summary>
        protected readonly ILGenerator CMIL;
        /// <summary>
        /// The conversion data
        /// </summary>
        protected readonly ConversionData Data;
        /// <summary>
        /// The input color
        /// </summary>
        protected readonly Color InColor;
        /// <summary>
        /// The output color
        /// </summary>
        protected readonly Color OutColor;

        #endregion

        #region Init

        /// <summary>
        /// Creates a new instance of the <see cref="ConversionCreator"/> class
        /// </summary>
        /// <param name="CMIL">The ILGenerator for the conversion method</param>
        /// <param name="data">The conversion data</param>
        /// <param name="inColor">The input color</param>
        /// <param name="outColor">The output color</param>
        protected ConversionCreator(ILGenerator CMIL, ConversionData data, Color inColor, Color outColor)
        {
            if (CMIL == null) throw new ArgumentNullException(nameof(CMIL));
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (inColor == null) throw new ArgumentNullException(nameof(inColor));
            if (outColor == null) throw new ArgumentNullException(nameof(outColor));

            this.CMIL = CMIL;
            Data = data;
            InColor = inColor;
            OutColor = outColor;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ConversionCreator"/> class
        /// </summary>
        /// <param name="CMIL">The ILGenerator for the conversion method</param>
        /// <param name="data">The conversion data</param>
        /// <param name="inColor">The input color</param>
        /// <param name="outColor">The output color</param>
        /// <param name="isLast">States if the output color is the last color</param>
        protected ConversionCreator(ILGenerator CMIL, ConversionData data, Color inColor, Color outColor, bool isLast)
            : this(CMIL, data, inColor, outColor)
        {
            _IsLastG = isLast;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ConversionCreator"/> class
        /// </summary>
        /// <param name="parent">The parent <see cref="ConversionCreator"/></param>
        /// /// <param name="isLast">States if the output color is the last color</param>
        /// <param name="inColor">The input color</param>
        /// <param name="outColor">The output color</param>
        protected ConversionCreator(ConversionCreator parent, Color inColor, Color outColor, bool isLast)
        {
            if (parent == null) throw new ArgumentNullException(nameof(parent));
            if (inColor == null) throw new ArgumentNullException(nameof(inColor));
            if (outColor == null) throw new ArgumentNullException(nameof(outColor));

            CMIL = parent.CMIL;
            Data = parent.Data;
            InColor = inColor;
            OutColor = outColor;
            _IsFirstG = false;
            _IsLastG = isLast;
            IsTempVar1 = !parent.IsTempVar1;
        }

        /// <summary>
        /// Sets the conversion method
        /// </summary>
        public abstract void SetConversionMethod();

        #endregion

        #region Subroutines

        /// <summary>
        /// Switches the value of the temp variable
        /// </summary>
        protected void SwitchTempVar()
        {
            IsTempVar1 = !IsTempVar1;
        }

        #endregion

        #region Write IL code

        /// <summary>
        /// Writes the IL code for a range check of the output values
        /// <para>This method considers normal range and circular range (can be different from 0-360°)</para>
        /// </summary>
        /// <param name="c">The output color</param>
        protected virtual void WriteRangeCheck(Color c)
        {
            double[] max = c.MaxValues;
            double[] min = c.MinValues;

            for (int i = 0; i < c.ChannelCount; i++)
            {
                IsLast = i == c.ChannelCount - 1;

                if (i == c.CylinderChannel)
                {
                    var ifLabel = CMIL.DefineLabel();
                    var endLabel = CMIL.DefineLabel();

                    //if (value >= max
                    CMIL.Emit(OpCodes.Ldarg_2);
                    WriteLdPtr(i);
                    CMIL.Emit(OpCodes.Ldind_R8);
                    CMIL.Emit(OpCodes.Ldc_R8, max[i]);
                    CMIL.Emit(OpCodes.Bge, ifLabel);
                    //|| value < min)
                    CMIL.Emit(OpCodes.Ldarg_2);
                    WriteLdPtr(i);
                    CMIL.Emit(OpCodes.Ldind_R8);
                    CMIL.Emit(OpCodes.Ldc_R8, min[i]);
                    CMIL.Emit(OpCodes.Bge_Un, endLabel);
                    CMIL.MarkLabel(ifLabel);

                    //range 0-max
                    if (min[i] == 0d) WriteRangeCheckCircularSimple(i, max[i]);
                    else WriteRangeCheckCircularFull(i, min[i], max[i]);

                    CMIL.MarkLabel(endLabel);
                }
                else
                {
                    bool doMax = !double.IsNaN(max[i]);
                    bool doMin = !double.IsNaN(min[i]);

                    if (doMax && doMin)
                    {
                        var ifLabel = CMIL.DefineLabel();
                        var endLabel = CMIL.DefineLabel();

                        //if (value > max) value = max;
                        WriteRangeCheckIf(i, max[i]);
                        CMIL.Emit(OpCodes.Ble_Un, ifLabel);
                        WriteRangeCheckAssign(i, max[i]);
                        if (IsLast) CMIL.Emit(OpCodes.Ret);
                        CMIL.Emit(OpCodes.Br, endLabel);
                        CMIL.MarkLabel(ifLabel);

                        //else if (value < min) value = min;
                        WriteRangeCheckIf(i, min[i]);
                        CMIL.Emit(OpCodes.Bge_Un, endLabel);
                        WriteRangeCheckAssign(i, min[i]);

                        CMIL.MarkLabel(endLabel);
                    }
                    else if (doMax)
                    {
                        var ifLabel = CMIL.DefineLabel();

                        //if (value > max) value = max;
                        WriteRangeCheckIf(i, max[i]);
                        CMIL.Emit(OpCodes.Ble_Un, ifLabel);
                        WriteRangeCheckAssign(i, max[i]);
                        CMIL.MarkLabel(ifLabel);
                    }
                    else if (doMin)
                    {
                        var ifLabel = CMIL.DefineLabel();

                        //if (value < min) value = min;
                        WriteRangeCheckIf(i, min[i]);
                        CMIL.Emit(OpCodes.Bge_Un, ifLabel);
                        WriteRangeCheckAssign(i, min[i]);
                        CMIL.MarkLabel(ifLabel);
                    }
                }
                
                if (IsLast) CMIL.Emit(OpCodes.Ret);
            }
        }

        /// <summary>
        /// Writes the IL code to assign the input value to the output value for a given amount of channels
        /// </summary>
        /// <param name="channels">The number of color channels</param>
        protected void WriteAssign(int channels)
        {
            for (int i = 0; i < channels; i++) WriteAssignSingle(i);
        }

        /// <summary>
        /// Writes the IL code to assign the input value to the output value at a given index
        /// </summary>
        /// <param name="index">The index of the color values to assign</param>
        protected void WriteAssignSingle(int index)
        {
            //outColor[index] = inColor[index];
            WriteLdOutput(index);
            WriteLdInput(index);
            CMIL.Emit(OpCodes.Ldind_R8);
            CMIL.Emit(OpCodes.Stind_R8);
        }

        /// <summary>
        /// Writes the IL code to load a pointer of an array at a specified zero-based index
        /// </summary>
        /// <param name="position">The index</param>
        protected unsafe void WriteLdPtr(int position)
        {
            if (position == 0) return;

            WriteLdInt(position * IntPtr.Size);
            CMIL.Emit(OpCodes.Conv_I);
            CMIL.Emit(OpCodes.Add);
        }

        /// <summary>
        /// Writes the IL code to load an integer value (depending on size of value)
        /// </summary>
        /// <param name="value">The value to load</param>
        protected void WriteLdInt(int value)
        {
            switch (value)
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
                    if (value < 256) CMIL.Emit(OpCodes.Ldc_I4_S, value);
                    else CMIL.Emit(OpCodes.Ldc_I4, value);
                    break;
            }
        }

        /// <summary>
        /// Writes the IL code to load the input and output values
        /// </summary>
        protected void WriteLdArg()
        {
            if (IsFirst || IsLast)
            {
                WriteLdInput();
                WriteLdOutput();
                if (IsFirst) IsTempVar1 = !IsTempVar1;
            }
            else WriteLdVar();
        }

        /// <summary>
        /// Writes the IL code to load the input value pointer
        /// </summary>
        protected void WriteLdInput()
        {
            if (IsFirst) CMIL.Emit(OpCodes.Ldarg_1);
            else WriteLdVarX(true);
        }

        /// <summary>
        /// Writes the IL code to load the input value at a specific index
        /// </summary>
        /// <param name="index">The index to load</param>
        protected void WriteLdInput(int index)
        {
            WriteLdInput();
            WriteLdPtr(index);
        }

        /// <summary>
        /// Writes the IL code to load the output value pointer
        /// </summary>
        protected void WriteLdOutput()
        {
            if (IsLast) CMIL.Emit(OpCodes.Ldarg_2);
            else WriteLdVarX(false);
        }

        /// <summary>
        /// Writes the IL code to load the output value at a specific index
        /// </summary>
        /// <param name="index">The index to load</param>
        protected void WriteLdOutput(int index)
        {
            WriteLdOutput();
            WriteLdPtr(index);
        }

        /// <summary>
        /// Writes the IL code for a method call
        /// </summary>
        /// <param name="m">The method to call</param>
        /// <param name="loadArgs">True if the IL code to load arguments should be written</param>
        protected void WriteMethodCall(MethodInfo m, bool loadArgs = true)
        {
            if (!m.IsStatic) CMIL.Emit(OpCodes.Ldarg_0);

            if (loadArgs)
            {
                int args = m.GetParameters().Length;

                if (args >= 2) WriteLdArg();
                else if (args == 1)
                {
                    if (IsLast) CMIL.Emit(OpCodes.Ldarg_2);
                    else WriteLdInput();
                }

                if (args == 3) CMIL.Emit(OpCodes.Ldarg_3);
            }

            if (m.IsVirtual) CMIL.Emit(OpCodes.Callvirt, m);
            else CMIL.Emit(OpCodes.Call, m);
        }

        /// <summary>
        /// Writes the IL code to store a local variable
        /// </summary>
        /// <param name="tp">They type of the variable</param>
        /// <returns>The information about the stored variable</returns>
        protected LocalBuilder WriteStloc(Type tp)
        {
            var lc = CMIL.DeclareLocal(tp);
            WriteStloc(lc);
            return lc;
        }

        /// <summary>
        /// Writes the IL code to store a local variable
        /// </summary>
        /// <param name="lc">The information about the stored variable</param>
        protected void WriteStloc(LocalBuilder lc)
        {
            switch (lc.LocalIndex)
            {
                case 0:
                    CMIL.Emit(OpCodes.Stloc_0);
                    break;
                case 1:
                    CMIL.Emit(OpCodes.Stloc_1);
                    break;
                case 2:
                    CMIL.Emit(OpCodes.Stloc_2);
                    break;
                case 3:
                    CMIL.Emit(OpCodes.Stloc_3);
                    break;
                default:
                    CMIL.Emit(OpCodes.Stloc, lc.LocalIndex);
                    break;
            }
        }

        /// <summary>
        /// Writes the IL code to load a local variable
        /// </summary>
        /// <param name="lc">The variable to load</param>
        protected void WriteLdloc(LocalBuilder lc)
        {
            switch (lc.LocalIndex)
            {
                case 0:
                    CMIL.Emit(OpCodes.Ldloc_0);
                    break;
                case 1:
                    CMIL.Emit(OpCodes.Ldloc_1);
                    break;
                case 2:
                    CMIL.Emit(OpCodes.Ldloc_2);
                    break;
                case 3:
                    CMIL.Emit(OpCodes.Ldloc_3);
                    break;
                default:
                    CMIL.Emit(OpCodes.Ldloc, lc);
                    break;
            }
        }

        /// <summary>
        /// Writes the IL code to load a field from the Data parameter
        /// </summary>
        /// <param name="field">The name of the field</param>
        protected void WriteDataLdfld(string field)
        {
            CMIL.Emit(OpCodes.Ldarg_3);
            FieldInfo fi = typeof(ConversionData).GetField(field);
            CMIL.Emit(OpCodes.Ldfld, fi);
        }

        /// <summary>
        /// Writes the IL code to call the Math.Pow(double, double) method
        /// </summary>
        protected void WriteCallPow()
        {
            var pow = typeof(Math).GetMethod(nameof(Math.Pow));
            WriteMethodCall(pow, false);
        }

        /// <summary>
        /// Writes the IL code to call the <see cref="UMath.MultiplyMatrix_3x3_3x1(double*, double*, double*)"/> method
        /// </summary>
        protected void WriteCallMultiplyMatrix_3x3_3x1()
        {
            var mm = typeof(UMath).GetMethod(nameof(UMath.MultiplyMatrix_3x3_3x1));
            WriteMethodCall(mm, false);
        }

        /// <summary>
        /// Writes the IL code to call the <see cref="UMath.AddMatrix_3x1"/> method
        /// </summary>
        protected void WriteCallAddMatrix_3x1()
        {
            var mm = typeof(UMath).GetMethod(nameof(UMath.AddMatrix_3x1));
            WriteMethodCall(mm, false);
        }


        /// <summary>
        /// Writes the IL code for loading the values of an if check
        /// </summary>
        /// <param name="pos">Index of the color channel</param>
        /// <param name="value">The value for which will be checked</param>
        private void WriteRangeCheckIf(int pos, double value)
        {
            CMIL.Emit(OpCodes.Ldarg_2);
            WriteLdPtr(pos);
            CMIL.Emit(OpCodes.Ldind_R8);
            CMIL.Emit(OpCodes.Ldc_R8, value);
        }

        /// <summary>
        /// Writes the IL code that assigns a value
        /// </summary>
        /// <param name="pos">Index of the color channel</param>
        /// <param name="value">The value that will be assigned</param>
        private void WriteRangeCheckAssign(int pos, double value)
        {
            CMIL.Emit(OpCodes.Ldarg_2);
            WriteLdPtr(pos);
            CMIL.Emit(OpCodes.Ldc_R8, value);
            CMIL.Emit(OpCodes.Stind_R8);
        }

        /// <summary>
        /// Writes the IL code for a simple circular range check (for when min == 0)
        /// </summary>
        /// <param name="pos">Index of the color channel</param>
        /// <param name="max">Maximum value</param>
        private void WriteRangeCheckCircularSimple(int pos, double max)
        {
            //value -= Math.Floor(value / max) * max;
            CMIL.Emit(OpCodes.Ldarg_2);
            WriteLdPtr(pos);
            CMIL.Emit(OpCodes.Dup);
            CMIL.Emit(OpCodes.Ldind_R8);
            CMIL.Emit(OpCodes.Ldarg_2);
            WriteLdPtr(pos);
            CMIL.Emit(OpCodes.Ldind_R8);
            CMIL.Emit(OpCodes.Ldc_R8, max);
            CMIL.Emit(OpCodes.Div);
            CMIL.Emit(OpCodes.Call, typeof(Math).GetMethod("Floor", new Type[] { typeof(double) }));
            CMIL.Emit(OpCodes.Ldc_R8, max);
            CMIL.Emit(OpCodes.Mul);
            CMIL.Emit(OpCodes.Sub);
            CMIL.Emit(OpCodes.Stind_R8);
        }

        /// <summary>
        /// Writes the IL code for a complete circular range check (for when min != 0)
        /// </summary>
        /// <param name="pos">Index of the color channel</param>
        /// <param name="min">Minimum value</param>
        /// <param name="max">Maximum value</param>
        private void WriteRangeCheckCircularFull(int pos, double min, double max)
        {
            FieldInfo VarsField = typeof(ConversionData).GetField("Vars");

            //offsetValue = value - min
            CMIL.Emit(OpCodes.Ldarg_3);
            CMIL.Emit(OpCodes.Ldfld, VarsField);
            CMIL.Emit(OpCodes.Ldarg_2);
            WriteLdPtr(pos);
            CMIL.Emit(OpCodes.Ldind_R8);
            CMIL.Emit(OpCodes.Ldc_R8, min);
            CMIL.Emit(OpCodes.Sub);
            CMIL.Emit(OpCodes.Stind_R8);

            double width = max - min;
            //value = (offsetValue - (Math.Floor(offsetValue / width) * width)) + min
            CMIL.Emit(OpCodes.Ldarg_2);
            WriteLdPtr(pos);
            CMIL.Emit(OpCodes.Ldarg_3);
            CMIL.Emit(OpCodes.Ldfld, VarsField);
            CMIL.Emit(OpCodes.Ldind_R8);
            CMIL.Emit(OpCodes.Ldarg_3);
            CMIL.Emit(OpCodes.Ldfld, VarsField);
            CMIL.Emit(OpCodes.Ldind_R8);
            CMIL.Emit(OpCodes.Ldc_R8, width);
            CMIL.Emit(OpCodes.Div);
            CMIL.Emit(OpCodes.Call, typeof(Math).GetMethod("Floor", new Type[] { typeof(double) }));
            CMIL.Emit(OpCodes.Ldc_R8, width);
            CMIL.Emit(OpCodes.Mul);
            CMIL.Emit(OpCodes.Sub);
            CMIL.Emit(OpCodes.Ldc_R8, min);
            CMIL.Emit(OpCodes.Add);
            CMIL.Emit(OpCodes.Stind_R8);
        }

        /// <summary>
        /// Writes the IL code to load input and output color variables from data
        /// </summary>
        private void WriteLdVar()
        {
            WriteLdVarX(true);
            WriteLdVarX(false);
            SwitchTempVar();
        }

        /// <summary>
        /// Writes the IL code to load a ColVars field from data
        /// </summary>
        /// <param name="input">True to load the current input values, false to load the current output values</param>
        private unsafe void WriteLdVarX(bool input)
        {
            bool tmp1 = input ? IsTempVar1 : !IsTempVar1;
            string fname;
            if (tmp1) fname = nameof(ConversionData.ColVars1);
            else fname = nameof(ConversionData.ColVars2);

            WriteDataLdfld(fname);
        }

        #endregion
    }
}