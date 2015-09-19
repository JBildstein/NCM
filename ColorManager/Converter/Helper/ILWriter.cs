using System;
using System.Reflection;
using System.Reflection.Emit;

namespace ColorManager.Conversion
{
    /// <summary>
    /// Provides methods to write IL code
    /// </summary>
    public class ILWriter
    {
        /// <summary>
        /// The underlying <see cref="ILGenerator"/> used to write IL code
        /// </summary>
        public readonly ILGenerator ILG;

        /// <summary>
        /// States if it's the first conversion within a <see cref="ConversionCreator"/>
        /// </summary>
        protected virtual bool IsFirst
        {
            get { return _IsFirst && IsFirstG; }
            set { _IsFirst = value; }
        }
        /// <summary>
        /// States if it's the last conversion within a <see cref="ConversionCreator"/>
        /// </summary>
        protected virtual bool IsLast
        {
            get { return _IsLast && IsLastG; }
            set { _IsLast = value; }
        }
        /// <summary>
        /// States if it's the first conversion globally
        /// </summary>
        protected virtual bool IsFirstG
        {
            get { return _IsFirstG; }
            set { _IsFirstG = value; }
        }
        /// <summary>
        /// States if it's the last conversion globally
        /// </summary>
        protected virtual bool IsLastG
        {
            get { return _IsLastG; }
            set { _IsLastG = value; }
        }
        /// <summary>
        /// States which temporary variable for the color values should be used
        /// </summary>
        protected virtual bool IsTempVar1
        {
            get { return _IsTempVar1; }
            set { _IsTempVar1 = value; }
        }

        private bool _IsFirst = true;
        private bool _IsLast = false;
        private bool _IsFirstG = true;
        private bool _IsLastG = true;
        private bool _IsTempVar1 = true;

        /// <summary>
        /// Creates a new instance of the <see cref="ILWriter"/> class
        /// </summary>
        /// <param name="ilgenerator">The <see cref="ILGenerator"/> used to write the IL code</param>
        public ILWriter(ILGenerator ilgenerator)
        {
            if (ilgenerator == null) throw new ArgumentNullException(nameof(ilgenerator));
            ILG = ilgenerator;
        }

        #region Emit Wrapper

        /// <summary>
        /// Writes the specified IL instruction
        /// </summary>
        /// <param name="opcode">The IL instruction to add</param>
        public void Write(OpCode opcode)
        {
            ILG.Emit(opcode);
        }

        /// <summary>
        /// Writes the specified IL instruction
        /// </summary>
        /// <param name="opcode">The IL instruction to add</param>
        /// <param name="arg">The numeric argument for the IL instruction</param>
        public void Write(OpCode opcode, int arg)
        {
            ILG.Emit(opcode, arg);
        }

        /// <summary>
        /// Writes the specified IL instruction
        /// </summary>
        /// <param name="opcode">The IL instruction to add</param>
        /// <param name="mi">A <see cref="MethodInfo"/> for which a metadata token will be added</param>
        public void Write(OpCode opcode, MethodInfo mi)
        {
            ILG.Emit(opcode, mi);
        }

        /// <summary>
        /// Writes the specified IL instruction
        /// </summary>
        /// <param name="opcode">The IL instruction to add</param>
        /// <param name="arg">The numeric argument for the IL instruction</param>
        public void Write(OpCode opcode, short arg)
        {
            ILG.Emit(opcode, arg);
        }

        /// <summary>
        /// Writes the specified IL instruction
        /// </summary>
        /// <param name="opcode">The IL instruction to add</param>
        /// <param name="ci">A <see cref="ConstructorInfo"/> for which a metadata token will be added</param>
        public void Write(OpCode opcode, ConstructorInfo ci)
        {
            ILG.Emit(opcode, ci);
        }

        /// <summary>
        /// Writes the specified IL instruction
        /// </summary>
        /// <param name="opcode">The IL instruction to add</param>
        /// <param name="arg">The numeric argument for the IL instruction</param>
        public void Write(OpCode opcode, long arg)
        {
            ILG.Emit(opcode, arg);
        }

        /// <summary>
        /// Writes the specified IL instruction
        /// </summary>
        /// <param name="opcode">The IL instruction to add</param>
        /// <param name="arg">The numeric argument for the IL instruction</param>
        public void Write(OpCode opcode, double arg)
        {
            ILG.Emit(opcode, arg);
        }

        /// <summary>
        /// Writes the specified IL instruction
        /// </summary>
        /// <param name="opcode">The IL instruction to add</param>
        /// <param name="labels">An array of jump labels to branch off from this position</param>
        public void Write(OpCode opcode, Label[] labels)
        {
            ILG.Emit(opcode, labels);
        }

        /// <summary>
        /// Writes the specified IL instruction
        /// </summary>
        /// <param name="opcode">The IL instruction to add</param>
        /// <param name="str">The string to write</param>
        public void Write(OpCode opcode, string str)
        {
            ILG.Emit(opcode, str);
        }

        /// <summary>
        /// Writes the specified IL instruction
        /// </summary>
        /// <param name="opcode">The IL instruction to add</param>
        /// <param name="local">A local variable</param>
        public void Write(OpCode opcode, LocalBuilder local)
        {
            ILG.Emit(opcode, local);
        }

        /// <summary>
        /// Writes the specified IL instruction
        /// </summary>
        /// <param name="opcode">The IL instruction to add</param>
        /// <param name="ci">A <see cref="FieldInfo"/> for which a metadata token will be added</param>
        public void Write(OpCode opcode, FieldInfo field)
        {
            ILG.Emit(opcode, field);
        }

        /// <summary>
        /// Writes the specified IL instruction
        /// </summary>
        /// <param name="opcode">The IL instruction to add</param>
        /// <param name="labels">A jump label to branch off from this position</param>
        public void Write(OpCode opcode, Label label)
        {
            ILG.Emit(opcode, label);
        }

        /// <summary>
        /// Writes the specified IL instruction
        /// </summary>
        /// <param name="opcode">The IL instruction to add</param>
        /// <param name="arg">The numeric argument for the IL instruction</param>
        public void Write(OpCode opcode, float arg)
        {
            ILG.Emit(opcode, arg);
        }

        /// <summary>
        /// Writes the specified IL instruction
        /// </summary>
        /// <param name="opcode">The IL instruction to add</param>
        /// <param name="ci">A <see cref="Type"/> for which a metadata token will be added</param>
        public void Write(OpCode opcode, Type tp)
        {
            ILG.Emit(opcode, tp);
        }

        /// <summary>
        /// Writes the specified IL instruction
        /// </summary>
        /// <param name="opcode">The IL instruction to add</param>
        /// <param name="ci">A helper to create a signature token</param>
        public void Write(OpCode opcode, SignatureHelper signature)
        {
            ILG.Emit(opcode, signature);
        }

        /// <summary>
        /// Writes the specified IL instruction
        /// </summary>
        /// <param name="opcode">The IL instruction to add</param>
        /// <param name="arg">The numeric argument for the IL instruction</param>
        public void Write(OpCode opcode, sbyte arg)
        {
            ILG.Emit(opcode, arg);
        }

        /// <summary>
        /// Writes the specified IL instruction
        /// </summary>
        /// <param name="opcode">The IL instruction to add</param>
        /// <param name="arg">The numeric argument for the IL instruction</param>
        public void Write(OpCode opcode, byte arg)
        {
            ILG.Emit(opcode, arg);
        }

        #endregion

        #region Local Variables

        /// <summary>
        /// Writes the IL code to store a local variable
        /// </summary>
        /// <param name="tp">They type of the variable</param>
        /// <returns>The information about the stored variable</returns>
        public LocalBuilder WriteStloc(Type tp)
        {
            var lc = ILG.DeclareLocal(tp);
            WriteStloc(lc);
            return lc;
        }

        /// <summary>
        /// Writes the IL code to store a local variable
        /// </summary>
        /// <param name="lc">The information about the stored variable</param>
        public void WriteStloc(LocalBuilder lc)
        {
            switch (lc.LocalIndex)
            {
                case 0:
                    ILG.Emit(OpCodes.Stloc_0);
                    break;
                case 1:
                    ILG.Emit(OpCodes.Stloc_1);
                    break;
                case 2:
                    ILG.Emit(OpCodes.Stloc_2);
                    break;
                case 3:
                    ILG.Emit(OpCodes.Stloc_3);
                    break;
                default:
                    ILG.Emit(OpCodes.Stloc, lc.LocalIndex);
                    break;
            }
        }

        /// <summary>
        /// Writes the IL code to load a local variable
        /// </summary>
        /// <param name="lc">The variable to load</param>
        public void WriteLdloc(LocalBuilder lc)
        {
            switch (lc.LocalIndex)
            {
                case 0:
                    ILG.Emit(OpCodes.Ldloc_0);
                    break;
                case 1:
                    ILG.Emit(OpCodes.Ldloc_1);
                    break;
                case 2:
                    ILG.Emit(OpCodes.Ldloc_2);
                    break;
                case 3:
                    ILG.Emit(OpCodes.Ldloc_3);
                    break;
                default:
                    ILG.Emit(OpCodes.Ldloc, lc);
                    break;
            }
        }

        #endregion

        #region Method Calls

        /// <summary>
        /// Writes the IL code for a method call
        /// </summary>
        /// <param name="m">The method to call</param>
        public void WriteCallMethod(MethodInfo m)
        {
            if (!m.IsStatic) ILG.Emit(OpCodes.Ldarg_0);

            if (m.IsVirtual) ILG.Emit(OpCodes.Callvirt, m);
            else ILG.Emit(OpCodes.Call, m);
        }

        /// <summary>
        /// Writes the IL code to call the Math.Pow(double, double) method
        /// </summary>
        public void WriteCallPow()
        {
            var pow = typeof(Math).GetMethod(nameof(Math.Pow));
            WriteCallMethod(pow);
        }

        /// <summary>
        /// Writes the IL code to call the <see cref="UMath.MultiplyMatrix_3x3_3x1(double*, double*, double*)"/> method
        /// </summary>
        public void WriteCallMultiplyMatrix_3x3_3x1()
        {
            var mm = typeof(UMath).GetMethod(nameof(UMath.MultiplyMatrix_3x3_3x1));
            WriteCallMethod(mm);
        }

        /// <summary>
        /// Writes the IL code to call the <see cref="UMath.MultiplyMatrix"/> method
        /// </summary>
        /// <param name="ax">X-Length of first matrix</param>
        /// <param name="ay">Y-Length of first matrix</param>
        /// <param name="bx">X-Length of second matrix</param>
        /// <param name="by">Y-Length of second matrix</param>
        public void WriteCallMultiplyMatrix(int ax, int ay, int bx, int by)
        {
            ILG.Emit(OpCodes.Ldind_I4);
            ILG.Emit(OpCodes.Ldc_I4, ax);
            ILG.Emit(OpCodes.Ldc_I4, ay);
            ILG.Emit(OpCodes.Ldc_I4, bx);
            ILG.Emit(OpCodes.Ldc_I4, by);
            var mm = typeof(UMath).GetMethod(nameof(UMath.MultiplyMatrix));
            WriteCallMethod(mm);
        }

        /// <summary>
        /// Writes the IL code to call the <see cref="UMath.AddMatrix_3x1"/> method
        /// </summary>
        public void WriteCallAddMatrix_3x1()
        {
            var mm = typeof(UMath).GetMethod(nameof(UMath.AddMatrix_3x1));
            WriteCallMethod(mm);
        }

        /// <summary>
        /// Writes the IL code to call the <see cref="UMath.AddMatrix"/> method
        /// </summary>
        /// <param name="la">Length of first matrix</param>
        /// <param name="lb">Length of second matrix</param>
        public void WriteCallAddMatrix(int la, int lb)
        {
            ILG.Emit(OpCodes.Ldind_I4);
            ILG.Emit(OpCodes.Ldc_I4, la);
            ILG.Emit(OpCodes.Ldc_I4, lb);
            var mm = typeof(UMath).GetMethod(nameof(UMath.AddMatrix));
            WriteCallMethod(mm);
        }

        #endregion

        #region Load Values

        /// <summary>
        /// Writes the IL code to load the input and output values
        /// </summary>
        public void WriteLdArg(bool loadData)
        {
            if (IsFirst || IsLast)
            {
                WriteLdInput();
                WriteLdOutput();
                if (IsFirst) SwitchTempVar();
            }
            else
            {
                WriteLdVarX(true);
                WriteLdVarX(false);
                SwitchTempVar();
            }

            if (loadData) Write(OpCodes.Ldarg_3);
        }

        /// <summary>
        /// Writes the IL code to load an integer value (depending on size of value)
        /// </summary>
        /// <param name="value">The value to load</param>
        public void WriteLdInt(int value)
        {
            switch (value)
            {
                case 1:
                    ILG.Emit(OpCodes.Ldc_I4_1);
                    break;
                case 2:
                    ILG.Emit(OpCodes.Ldc_I4_2);
                    break;
                case 3:
                    ILG.Emit(OpCodes.Ldc_I4_3);
                    break;
                case 4:
                    ILG.Emit(OpCodes.Ldc_I4_4);
                    break;
                case 5:
                    ILG.Emit(OpCodes.Ldc_I4_5);
                    break;
                case 6:
                    ILG.Emit(OpCodes.Ldc_I4_6);
                    break;
                case 7:
                    ILG.Emit(OpCodes.Ldc_I4_7);
                    break;
                case 8:
                    ILG.Emit(OpCodes.Ldc_I4_8);
                    break;

                default:
                    if (value < 256) ILG.Emit(OpCodes.Ldc_I4_S, value);
                    else ILG.Emit(OpCodes.Ldc_I4, value);
                    break;
            }
        }

        /// <summary>
        /// Writes the IL code to load a pointer of an array at a specified zero-based index
        /// </summary>
        /// <param name="position">The index</param>
        /// <param name="size">The size of the pointer type</param>
        public void WriteLdPtr(int position, int size = sizeof(double))
        {
            if (position == 0) return;

            WriteLdInt(position * size);
            ILG.Emit(OpCodes.Conv_I);
            ILG.Emit(OpCodes.Add);
        }

        /// <summary>
        /// Writes the IL code to load the input value pointer
        /// </summary>
        public void WriteLdInput()
        {
            if (IsFirst) ILG.Emit(OpCodes.Ldarg_1);
            else WriteLdVarX(true);
        }

        /// <summary>
        /// Writes the IL code to load the input value at a specific index
        /// </summary>
        /// <param name="index">The index to load</param>
        public void WriteLdInput(int index)
        {
            WriteLdInput();
            WriteLdPtr(index);
        }

        /// <summary>
        /// Writes the IL code to load the output value pointer
        /// </summary>
        public void WriteLdOutput()
        {
            if (IsLast) ILG.Emit(OpCodes.Ldarg_2);
            else WriteLdVarX(false);
        }

        /// <summary>
        /// Writes the IL code to load the output value at a specific index
        /// </summary>
        /// <param name="index">The index to load</param>
        public void WriteLdOutput(int index)
        {
            WriteLdOutput();
            WriteLdPtr(index);
        }

        /// <summary>
        /// Writes the IL code to load a field from the Data parameter
        /// </summary>
        /// <param name="field">The name of the field</param>
        public void WriteDataLdfld(string field)
        {
            ILG.Emit(OpCodes.Ldarg_3);
            FieldInfo fi = typeof(ConversionData).GetField(field);
            ILG.Emit(OpCodes.Ldfld, fi);
        }

        #endregion

        #region Range Check

        /// <summary>
        /// Writes the IL code for a range check of the output values
        /// <para>This method considers normal range and circular range (can be different from 0-360°)</para>
        /// </summary>
        /// <param name="c">The output color</param>
        public void WriteRangeCheck(Color c)
        {
            double[] max = c.MaxValues;
            double[] min = c.MinValues;

            for (int i = 0; i < c.ChannelCount; i++)
            {
                if (i == c.CylinderChannel)
                {
                    var ifLabel = ILG.DefineLabel();
                    var endLabel = ILG.DefineLabel();

                    //if (value >= max
                    ILG.Emit(OpCodes.Ldarg_2);
                    WriteLdPtr(i);
                    ILG.Emit(OpCodes.Ldind_R8);
                    ILG.Emit(OpCodes.Ldc_R8, max[i]);
                    ILG.Emit(OpCodes.Bge, ifLabel);
                    //|| value < min)
                    ILG.Emit(OpCodes.Ldarg_2);
                    WriteLdPtr(i);
                    ILG.Emit(OpCodes.Ldind_R8);
                    ILG.Emit(OpCodes.Ldc_R8, min[i]);
                    ILG.Emit(OpCodes.Bge_Un, endLabel);
                    ILG.MarkLabel(ifLabel);

                    //range 0-max
                    if (min[i] == 0d) WriteRangeCheckCircularSimple(i, max[i]);
                    else WriteRangeCheckCircularFull(i, min[i], max[i]);

                    ILG.MarkLabel(endLabel);
                }
                else
                {
                    bool doMax = !double.IsNaN(max[i]);
                    bool doMin = !double.IsNaN(min[i]);

                    if (doMax && doMin)
                    {
                        var ifLabel = ILG.DefineLabel();
                        var endLabel = ILG.DefineLabel();

                        //if (value > max) value = max;
                        WriteRangeCheckIf(i, max[i]);
                        ILG.Emit(OpCodes.Ble_Un, ifLabel);
                        WriteRangeCheckAssign(i, max[i]);
                        if (IsLast) ILG.Emit(OpCodes.Ret);
                        ILG.Emit(OpCodes.Br, endLabel);
                        ILG.MarkLabel(ifLabel);

                        //else if (value < min) value = min;
                        WriteRangeCheckIf(i, min[i]);
                        ILG.Emit(OpCodes.Bge_Un, endLabel);
                        WriteRangeCheckAssign(i, min[i]);

                        ILG.MarkLabel(endLabel);
                    }
                    else if (doMax)
                    {
                        var ifLabel = ILG.DefineLabel();

                        //if (value > max) value = max;
                        WriteRangeCheckIf(i, max[i]);
                        ILG.Emit(OpCodes.Ble_Un, ifLabel);
                        WriteRangeCheckAssign(i, max[i]);
                        ILG.MarkLabel(ifLabel);
                    }
                    else if (doMin)
                    {
                        var ifLabel = ILG.DefineLabel();

                        //if (value < min) value = min;
                        WriteRangeCheckIf(i, min[i]);
                        ILG.Emit(OpCodes.Bge_Un, ifLabel);
                        WriteRangeCheckAssign(i, min[i]);
                        ILG.MarkLabel(ifLabel);
                    }
                }
            }
        }

        /// <summary>
        /// Writes the IL code for loading the values of an if check
        /// </summary>
        /// <param name="pos">Index of the color channel</param>
        /// <param name="value">The value for which will be checked</param>
        private void WriteRangeCheckIf(int pos, double value)
        {
            ILG.Emit(OpCodes.Ldarg_2);
            WriteLdPtr(pos);
            ILG.Emit(OpCodes.Ldind_R8);
            ILG.Emit(OpCodes.Ldc_R8, value);
        }

        /// <summary>
        /// Writes the IL code that assigns a value
        /// </summary>
        /// <param name="pos">Index of the color channel</param>
        /// <param name="value">The value that will be assigned</param>
        private void WriteRangeCheckAssign(int pos, double value)
        {
            ILG.Emit(OpCodes.Ldarg_2);
            WriteLdPtr(pos);
            ILG.Emit(OpCodes.Ldc_R8, value);
            ILG.Emit(OpCodes.Stind_R8);
        }

        /// <summary>
        /// Writes the IL code for a simple circular range check (for when min == 0)
        /// </summary>
        /// <param name="pos">Index of the color channel</param>
        /// <param name="max">Maximum value</param>
        private void WriteRangeCheckCircularSimple(int pos, double max)
        {
            //value -= Math.Floor(value / max) * max;
            ILG.Emit(OpCodes.Ldarg_2);
            WriteLdPtr(pos);
            ILG.Emit(OpCodes.Dup);
            ILG.Emit(OpCodes.Ldind_R8);
            ILG.Emit(OpCodes.Ldarg_2);
            WriteLdPtr(pos);
            ILG.Emit(OpCodes.Ldind_R8);
            ILG.Emit(OpCodes.Ldc_R8, max);
            ILG.Emit(OpCodes.Div);
            ILG.Emit(OpCodes.Call, typeof(Math).GetMethod(nameof(Math.Floor), new Type[] { typeof(double) }));
            ILG.Emit(OpCodes.Ldc_R8, max);
            ILG.Emit(OpCodes.Mul);
            ILG.Emit(OpCodes.Sub);
            ILG.Emit(OpCodes.Stind_R8);
        }

        /// <summary>
        /// Writes the IL code for a complete circular range check (for when min != 0)
        /// </summary>
        /// <param name="pos">Index of the color channel</param>
        /// <param name="min">Minimum value</param>
        /// <param name="max">Maximum value</param>
        private unsafe void WriteRangeCheckCircularFull(int pos, double min, double max)
        {
            FieldInfo VarsField = typeof(ConversionData).GetField(nameof(ConversionData.Vars));

            //offsetValue = value - min
            ILG.Emit(OpCodes.Ldarg_3);
            ILG.Emit(OpCodes.Ldfld, VarsField);
            ILG.Emit(OpCodes.Ldarg_2);
            WriteLdPtr(pos);
            ILG.Emit(OpCodes.Ldind_R8);
            ILG.Emit(OpCodes.Ldc_R8, min);
            ILG.Emit(OpCodes.Sub);
            ILG.Emit(OpCodes.Stind_R8);

            double width = max - min;
            //value = (offsetValue - (Math.Floor(offsetValue / width) * width)) + min
            ILG.Emit(OpCodes.Ldarg_2);
            WriteLdPtr(pos);
            ILG.Emit(OpCodes.Ldarg_3);
            ILG.Emit(OpCodes.Ldfld, VarsField);
            ILG.Emit(OpCodes.Ldind_R8);
            ILG.Emit(OpCodes.Ldarg_3);
            ILG.Emit(OpCodes.Ldfld, VarsField);
            ILG.Emit(OpCodes.Ldind_R8);
            ILG.Emit(OpCodes.Ldc_R8, width);
            ILG.Emit(OpCodes.Div);
            ILG.Emit(OpCodes.Call, typeof(Math).GetMethod(nameof(Math.Floor), new Type[] { typeof(double) }));
            ILG.Emit(OpCodes.Ldc_R8, width);
            ILG.Emit(OpCodes.Mul);
            ILG.Emit(OpCodes.Sub);
            ILG.Emit(OpCodes.Ldc_R8, min);
            ILG.Emit(OpCodes.Add);
            ILG.Emit(OpCodes.Stind_R8);
        }

        #endregion

        #region Assign

        /// <summary>
        /// Writes the IL code to assign the input value to the output value for a given amount of channels
        /// </summary>
        /// <param name="channels">The number of color channels</param>
        public void WriteAssign(int channels)
        {
            for (int i = 0; i < channels; i++) WriteAssignSingle(i);
        }

        /// <summary>
        /// Writes the IL code to assign the input value to the output value at a given index
        /// </summary>
        /// <param name="index">The index of the color values to assign</param>
        public void WriteAssignSingle(int index)
        {
            //outColor[index] = inColor[index];
            WriteLdOutput(index);
            WriteLdInput(index);
            ILG.Emit(OpCodes.Ldind_R8);
            ILG.Emit(OpCodes.Stind_R8);
        }

        #endregion

        #region Subroutines

        /// <summary>
        /// Switches the value of the temp variable
        /// </summary>
        protected void SwitchTempVar()
        {
            IsTempVar1 = !IsTempVar1;
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
