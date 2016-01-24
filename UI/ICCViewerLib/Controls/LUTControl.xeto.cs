using Eto.Forms;
using Eto.Drawing;
using Eto.Serialization.Xaml;
using ColorManager.ICC;
using System.Text;

namespace ICCViewer.Controls
{
    public class LUTControl : Panel
    {
        Label InChCountLabel { get; set; }
        Label OutChCountLabel { get; set; }
        TabControl ContentTabs { get; set; }
        TableLayout MainTable { get; set; }

        public LUTControl()
        {
            XamlReader.Load(this);
        }

        public LUTControl(Lut8TagDataEntry lut)
            : this()
        {
            SetLabels(lut.InputChannelCount, lut.OutputChannelCount);
            SetFromLUT(lut.CLUTValues, lut.InputValues, lut.OutputValues);
        }

        public LUTControl(Lut16TagDataEntry lut)
            : this()
        {
            SetLabels(lut.InputChannelCount, lut.OutputChannelCount);
            SetFromLUT(lut.CLUTValues, lut.InputValues, lut.OutputValues);
        }

        public LUTControl(LutAToBTagDataEntry lut)
            : this()
        {
            SetLabels(lut.InputChannelCount, lut.OutputChannelCount);
            SetMatrixControl(lut.Matrix3x1, lut.Matrix3x3);
            SetFromLutAB(lut.CLUTValues, lut.CurveA, lut.CurveB, lut.CurveM);
        }

        public LUTControl(LutBToATagDataEntry lut)
            : this()
        {
            SetLabels(lut.InputChannelCount, lut.OutputChannelCount);
            SetMatrixControl(lut.Matrix3x1, lut.Matrix3x3);
            SetFromLutAB(lut.CLUTValues, lut.CurveA, lut.CurveB, lut.CurveM);
        }

        private void SetLabels(int inChCount, int outChCount)
        {
            InChCountLabel.Text = inChCount.ToString();
            OutChCountLabel.Text = outChCount.ToString();
        }

        private void SetFromLUT(CLUT clut, LUT[] inLut, LUT[] outLut)
        {
            if (inLut != null)
            {
                var inCurve = GetLutCurveControl(inLut);
                AddTabPage("In LUT", inCurve);
            }

            if (clut.Values != null)
            {
                var clutCtrl = new ClutControl(clut);
                AddTabPage("CLUT", clutCtrl);
            }

            if (outLut != null)
            {
                var outCurve = GetLutCurveControl(outLut);
                AddTabPage("Out LUT", outCurve);
            }
        }

        private void SetFromLutAB(CLUT clut, TagDataEntry[] curveA, TagDataEntry[] curveB, TagDataEntry[] curveM)
        {
            if (clut.Values != null)
            {
                var clutCtrl = new ClutControl(clut);
                AddTabPage("CLUT", clutCtrl);
            }

            if (curveA != null)
            {
                var curveACtrl = new CurveControl(GetCurveValues(curveA));
                AddTabPage("Curve A", curveACtrl);
            }

            if (curveB != null)
            {
                var curveBCtrl = new CurveControl(GetCurveValues(curveB));
                AddTabPage("Curve B", curveBCtrl);
            }

            if (curveM != null)
            {
                var curveMCtrl = new CurveControl(GetCurveValues(curveM));
                AddTabPage("Curve M", curveMCtrl);
            }
        }

        private void AddTabPage(string name, Control ctrl)
        {
            if (ctrl == null) return;

            var page = new TabPage();
            page.Text = name;
            page.Content = ctrl;
            ContentTabs.Pages.Add(page);
        }

        private CurveControl GetLutCurveControl(LUT[] lut)
        {
            double[][] values = new double[lut.Length][];
            for (int i = 0; i < values.Length; i++) { values[i] = lut[i].Values; }
            return new CurveControl(values);
        }

        private double[][] GetCurveValues(TagDataEntry[] curve)
        {
            double[][] result = new double[curve.Length][];
            for (int i = 0; i < curve.Length; i++)
            {
                var pcurve = curve[i] as ParametricCurveTagDataEntry;
                if (pcurve != null)
                {
                    result[i] = Conversion.FromParametricCurve(pcurve.Curve);
                    continue;
                }

                var ccurve = curve[i] as CurveTagDataEntry;
                if (ccurve != null)
                {
                    if (ccurve.IsGamma) result[i] = Conversion.FromGamma(ccurve.Gamma);
                    else result[i] = ccurve.CurveData;
                    continue;
                }

                //Should never come to this:
                result[i] = new double[] { 0.0 };
            }

            return result;
        }
        
        private void SetMatrixControl(double[] m3x1, double[,] m3x3)
        {
            if (m3x1 == null || m3x3 == null) return;

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < m3x1.Length; i++)
            {
                builder.Append(m3x1[i]);
                builder.AppendLine("   ");
                for (int j = 0; j < m3x3.GetLength(1); j++)
                {
                    builder.Append(m3x3[i, j]);
                    builder.AppendLine(" ");
                }
                builder.AppendLine();
            }

            MainTable.Rows.Add(new TableRow
            {
                Cells =
                {
                    new Label
                    {
                        Text ="Matrix:",
                        TextAlignment = TextAlignment.Right
                    },
                    new TableCell
                    (
                        new TextArea
                        {
                            Font = Fonts.Monospace(10),
                            ReadOnly = true,
                            Text = builder.ToString()
                        }
                    ) { ScaleWidth = true }
                }
            });
        }
    }
}
