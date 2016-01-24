using System;
using System.Collections.Generic;
using ColorManager.ICC;
using Eto.Forms;
using Eto.Serialization.Xaml;

namespace ICCViewer.Controls
{
    public class ClutControl : Panel
    {
        Label DataTypeLabel { get; set; }
        TableLayout InValuesTable { get; set; }
        StackLayout OutValuesLayout { get; set; }

        List<Slider> InSliders;
        List<TextBox> OutTextBoxes;

        CLUT Clut;

        public ClutControl()
        {
            XamlReader.Load(this);
            InSliders = new List<Slider>();
            OutTextBoxes = new List<TextBox>();
        }

        public ClutControl(CLUT clut)
            : this()
        {
            DataTypeLabel.Text = clut.DataType.ToString();
            Clut = clut;

            for (int i = 0; i < clut.InputChannelCount; i++)
            {
                NumericUpDown num = new NumericUpDown();
                num.MaximumDecimalPlaces = 0;
                Slider sl = new Slider();
                sl.SnapToTick = true;
                num.MinValue = sl.MinValue = 0;
                num.MaxValue = sl.MaxValue = clut.GridPointCount[i] - 1;
                sl.ValueChanged += Slider_ValueChanged;
                sl.ValueChanged += (o, s) => { if ((int)num.Value != sl.Value) num.Value = sl.Value; };
                num.ValueChanged += (o, s) => { if (sl.Value != (int)num.Value) sl.Value = (int)num.Value; };
                TableRow row = new TableRow
                {
                    Cells =
                    {
                        new TableCell(sl, true),
                        new TableCell(num),
                    }
                };
                InSliders.Add(sl);
                InValuesTable.Rows.Add(row);
            }
            InValuesTable.Rows.Add(new TableRow { ScaleHeight = true });

            for (int i = 0; i < clut.OutputChannelCount; i++)
            {
                Label lb = new Label();
                lb.Text = $"Channel {i}:";
                TextBox txt = new TextBox();
                txt.ReadOnly = true;
                txt.Text = "0";
                OutTextBoxes.Add(txt);
                OutValuesLayout.Items.Add(lb);
                OutValuesLayout.Items.Add(txt);
            }
            OutValuesLayout.Items.Add(new StackLayoutItem { Expand = true });
        }

        private void Slider_ValueChanged(object sender, EventArgs e)
        {
            int inCh = Clut.InputChannelCount;

            int[] inVals = new int[inCh];
            for (int i = 0; i < inVals.Length; i++) { inVals[i] = InSliders[i].Value; }

            int idx = 0;
            for (int i = inCh - 1; i >= 0; i--)
            {
                int factor = 1;
                for (int j = i + 1; j < inCh; j++) { factor *= Clut.GridPointCount[j]; }
                idx += inVals[i] * factor;
            }

            double[] outVals = Clut.Values[idx];
            for (int i = 0; i < outVals.Length; i++) { OutTextBoxes[i].Text = outVals[i].ToString("F6"); }
        }
    }
}
