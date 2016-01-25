using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ColorManager;
using ColorManager.ICC;
using Eto.Forms;
using Eto.Serialization.Xaml;

namespace ColorConverter
{
    public class ColorControl : Panel
    {
        #region Variables

        public bool ReadOnly { get; set; }
        public Type CurrentColor
        {
            get { return Colors[ModelDropDown.SelectedIndex]; }
        }

        StackLayout ModelSettingsLayout { get; set; }
        StackLayout ValuesTable { get; set; }
        DropDown ModelDropDown { get; set; }
        DropDown ColorspaceDropDown { get; set; }

        TextBox ICCTextBox;
        NumericUpDown GammaNum;
        DropDown WPDropDown;
        DropDown RGBDropDown;

        List<Type> Colors;
        List<TextBox> ValueTextBoxes;
        List<NumericUpDown> ValueNumBoxes;

        #endregion

        public ColorControl()
        {
            XamlReader.Load(this);
            Colors = new List<Type>();
            ModelDropDown.SelectedIndexChanged += ModelDropDown_SelectedIndexChanged;
            ColorspaceDropDown.SelectedIndexChanged += ColorspaceDropDown_SelectedIndexChanged;
            FillModels();
        }

        public void SetValues(Color color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));
            if (color.GetType() != CurrentColor) throw new ArgumentException("Color is not the same as selected type", nameof(color));

            if (ReadOnly)
            {
                for (int i = 0; i < ValueTextBoxes.Count; i++)
                {
                    ValueTextBoxes[i].Text = color[i].ToString("F6");
                }
            }
            else
            {
                for (int i = 0; i < ValueTextBoxes.Count; i++)
                {
                    ValueNumBoxes[i].Value = color[i];
                }
            }
        }

        public Color GetColorInstance()
        {
            Color color = CreateInstance();
            if (!ReadOnly)
            {
                for (int i = 0; i < ValueNumBoxes.Count; i++)
                {
                    color[i] = ValueNumBoxes[i].Value;
                }
            }
            return color;
        }

        #region Event Handling

        private void ModelDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type color = CurrentColor;
            SetColorspaces(color);
            SetValues(color);
        }

        private void ColorspaceDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ColorspaceDropDown.SelectedIndex < 0) return;
            IListItem selected = ColorspaceDropDown.Items[ColorspaceDropDown.SelectedIndex];
            SetSettings(selected.Key);
        }

        #endregion

        #region Subroutines

        private void FillModels()
        {
            Assembly asm = typeof(Color).Assembly;
            foreach (var tp in asm.DefinedTypes)
            {
                if (ValidColor(tp))
                {
                    Colors.Add(tp);
                    ModelDropDown.Items.Add(GetName(tp) ?? tp.Name);
                }
            }
            if (ModelDropDown.Items.Count > 0) ModelDropDown.SelectedIndex = 0;
        }

        private void SetColorspaces(Type color)
        {
            var wpCtor = GetWpCtor(color);
            var rgbCtor = GetRgbCtor(color);
            var grayCtor = GetGrayCtor(color);
            var iccCtor = GetIccCtor(color);

            ColorspaceDropDown.Items.Clear();
            if (wpCtor != null) ColorspaceDropDown.Items.Add(new ListItem { Key = "wp", Text = "Whitepoint", Tag = wpCtor });
            if (rgbCtor != null) ColorspaceDropDown.Items.Add(new ListItem { Key = "rgb", Text = "RGB Colorspace", Tag = rgbCtor });
            if (grayCtor != null) ColorspaceDropDown.Items.Add(new ListItem { Key = "gray", Text = "Gray Colorspace", Tag = grayCtor });
            if (iccCtor != null) ColorspaceDropDown.Items.Add(new ListItem { Key = "icc", Text = "ICC Profile", Tag = iccCtor });

            if (ColorspaceDropDown.Items.Count > 0) ColorspaceDropDown.SelectedIndex = 0;
        }

        private void SetSettings(string key)
        {
            ModelSettingsLayout.Items.Clear();
            switch (key)
            {
                case "wp":
                    SetWPDropDown();
                    ModelSettingsLayout.Items.Add(WPDropDown);
                    break;
                case "rgb":
                    SetRGBDropDown();
                    ModelSettingsLayout.Items.Add(RGBDropDown);
                    break;
                case "gray":
                    SetWPDropDown();
                    GammaNum = new NumericUpDown();
                    GammaNum.MaxValue = 100;
                    GammaNum.MinValue = 0;
                    GammaNum.MaximumDecimalPlaces = 6;
                    ModelSettingsLayout.Items.Add(WPDropDown);
                    ModelSettingsLayout.Items.Add(GammaNum);
                    break;
                case "icc":
                    ICCTextBox = new TextBox();
                    ICCTextBox.ReadOnly = true;
                    Button iccBtn = new Button();
                    iccBtn.Text = "Open";
                    iccBtn.Click += IccBtn_Click;
                    var sli = new StackLayoutItem
                    {
                        HorizontalAlignment = HorizontalAlignment.Right,
                        Control = iccBtn
                    };
                    ModelSettingsLayout.Items.Add(ICCTextBox);
                    ModelSettingsLayout.Items.Add(sli);
                    break;
            }
        }

        private void SetWPDropDown()
        {
            WPDropDown = new DropDown();
            Assembly asm = typeof(Whitepoint).Assembly;
            foreach (var tp in asm.DefinedTypes)
            {
                ConstructorInfo ctor = GetWPCtor(tp);
                if (ctor != null)
                {
                    WPDropDown.Items.Add(new ListItem
                    {
                        Text = tp.Name,
                        Key = tp.Name,
                        Tag = ctor
                    });
                }
            }
            if (WPDropDown.Items.Count > 0) WPDropDown.SelectedIndex = 0;
        }

        private void SetRGBDropDown()
        {
            RGBDropDown = new DropDown();
            Assembly asm = typeof(Whitepoint).Assembly;
            foreach (var tp in asm.DefinedTypes)
            {
                ConstructorInfo ctor = GetRGBCtor(tp);
                if (ctor != null)
                {
                    RGBDropDown.Items.Add(new ListItem
                    {
                        Text = tp.Name,
                        Key = tp.Name,
                        Tag = ctor
                    });
                }
            }
            if (RGBDropDown.Items.Count > 0) RGBDropDown.SelectedIndex = 0;
        }

        private ConstructorInfo GetWPCtor(Type wp)
        {
            Type wpType = typeof(Whitepoint);
            var ctors = wp.GetConstructors();
            if (!wp.IsAbstract && wp.IsSubclassOf(wpType))
            {
                return ctors.FirstOrDefault(t => t.GetParameters().Length == 0);
            }
            else return null;
        }

        private ConstructorInfo GetRGBCtor(Type rgb)
        {
            Type rgbType = typeof(ColorspaceRGB);
            var ctors = rgb.GetConstructors();
            if (!rgb.IsAbstract && rgb.IsSubclassOf(rgbType))
            {
                return ctors.FirstOrDefault(t => t.GetParameters().Length == 0);
            }
            else return null;
        }


        private void IccBtn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filters.Add(new FileDialogFilter("ICC Profile", ".icc", ".icm"));
                dlg.Filters.Add(new FileDialogFilter("All Files", ".*"));

                var res = dlg.ShowDialog(this);
                if (res == DialogResult.Ok)
                {
                    ICCTextBox.Text = dlg.FileName;
                }
            }
        }

        private void SetValues(Type color)
        {
            ValuesTable.Items.Clear();

            double[] min = GetMin(color);
            double[] max = GetMax(color);
            int count = min.Length;

            if (ReadOnly)
            {
                ValueTextBoxes = new List<TextBox>();
                for (int i = 0; i < count; i++)
                {
                    TextBox txt = new TextBox();
                    txt.ReadOnly = true;
                    ValueTextBoxes.Add(txt);
                    ValuesTable.Items.Add(new StackLayoutItem
                    {
                        Expand = true,
                        Control = txt
                    });
                }
            }
            else
            {
                ValueNumBoxes = new List<NumericUpDown>();
                for (int i = 0; i < count; i++)
                {
                    NumericUpDown num = new NumericUpDown();
                    num.MinValue = min[i];
                    num.MaxValue = max[i];
                    num.DecimalPlaces = 6;
                    num.Increment = 0.1;
                    ValueNumBoxes.Add(num);
                    ValuesTable.Items.Add(new StackLayoutItem
                    {
                        Expand = true,
                        Control = num
                    });
                }
            }
        }

        private Color CreateInstance()
        {
            var sctor = ColorspaceDropDown.Items[ColorspaceDropDown.SelectedIndex] as ListItem;
            ConstructorInfo ctor = sctor.Tag as ConstructorInfo;

            switch (sctor.Key)
            {
                case "wp":
                    Whitepoint wp = GetWhitepoint();
                    return ctor.Invoke(new object[] { wp }) as Color;
                case "rgb":
                    ColorspaceRGB rgbspace = GetRGBColorspace();
                    return ctor.Invoke(new object[] { rgbspace }) as Color;
                case "gray":
                    double gamma = GammaNum.Value;
                    Whitepoint gwp = GetWhitepoint();
                    var gspace = new ColorspaceGray(gwp, gamma);
                    return ctor.Invoke(new object[] { gspace }) as Color;
                case "icc":
                    string iccPath = ICCTextBox.Text;
                    var profile = new ICCProfileReader().Read(iccPath);
                    return ctor.Invoke(new object[] { profile }) as Color;

                default:
                    throw new Exception("Invalid Key");
            }
        }

        private Whitepoint GetWhitepoint()
        {
            var sel = WPDropDown.Items[WPDropDown.SelectedIndex] as ListItem;
            var ctor = sel.Tag as ConstructorInfo;
            return ctor.Invoke(null) as Whitepoint;
        }

        private ColorspaceRGB GetRGBColorspace()
        {
            var sel = RGBDropDown.Items[RGBDropDown.SelectedIndex] as ListItem;
            var ctor = sel.Tag as ConstructorInfo;
            return ctor.Invoke(null) as ColorspaceRGB;
        }


        private ConstructorInfo GetWpCtor(Type color)
        {
            return GetCtor(color, typeof(Whitepoint));
        }

        private ConstructorInfo GetRgbCtor(Type color)
        {
            return GetCtor(color, typeof(ColorspaceRGB));
        }

        private ConstructorInfo GetGrayCtor(Type color)
        {
            return GetCtor(color, typeof(ColorspaceGray));
        }

        private ConstructorInfo GetIccCtor(Type color)
        {
            return GetCtor(color, typeof(ICCProfile));
        }

        private ConstructorInfo GetCtor(Type color, Type parameter)
        {
            var ctors = color.GetConstructors();
            return ctors.FirstOrDefault(t =>
            {
                var para = t.GetParameters();
                return para.Length == 1 &&
                para.First().ParameterType.IsAssignableFrom(parameter);
            });
        }

        private bool ValidColor(Type color)
        {
            if (color == typeof(ColorX)) return false;

            Type colorType = typeof(Color);
            var constructors = color.GetConstructors();
            return !color.IsAbstract
                && color.IsSubclassOf(colorType)
                && constructors.Length > 0
                && constructors.Any(t =>
                {
                    var para = t.GetParameters();
                    return para.Length == 0 || (para.Length == 1
                     && para.First().ParameterType.IsAssignableFrom(typeof(ICCProfile)));
                });
        }

        private string GetName(Type color)
        {
            var prop = color.GetProperty("ModelName", BindingFlags.Static | BindingFlags.Public);
            return prop?.GetValue(null) as string;
        }

        private double[] GetMin(Type color)
        {
            var prop = color.GetProperty("Min", BindingFlags.Static | BindingFlags.Public);
            return prop?.GetValue(null) as double[];
        }

        private double[] GetMax(Type color)
        {
            var prop = color.GetProperty("Max", BindingFlags.Static | BindingFlags.Public);
            return prop?.GetValue(null) as double[];
        }

        #endregion
    }
}
