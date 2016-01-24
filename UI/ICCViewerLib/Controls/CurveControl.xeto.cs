using Eto.Forms;
using Eto.Drawing;
using Eto.Serialization.Xaml;
using System.Linq;
using ColorManager.ICC;

namespace ICCViewer.Controls
{
    public class CurveControl : Drawable
    {
        double[][] Curve;

        public readonly Color[] CurveColors =
        {
            Colors.Red,
            Colors.Green,
            Colors.Blue,
            Colors.Cyan,
            Colors.Magenta,
            Colors.Yellow,
            Colors.Black,
            Colors.Brown,
            Colors.Orange,
            Colors.Purple,
            Colors.Silver,
            Colors.Teal,
            Colors.YellowGreen,
            Colors.Aquamarine,
            Colors.Coral,
            Colors.DarkOliveGreen,
        };

        public CurveControl()
        {
            XamlReader.Load(this);
        }

        public CurveControl(params double[][] curves)
            : this()
        {
            SetCurve(curves);
        }
        
        public CurveControl(params ParametricCurve[] curves)
            : this()
        {
            double[][] values = new double[curves.Length][];
            for (int i = 0; i < curves.Length; i++)
            {
                values[i] = Conversion.FromParametricCurve(curves[i]);
            }
            SetCurve(values);
        }

        public CurveControl(params CurveTagDataEntry[] curves)
            : this()
        {
            double[][] values = new double[curves.Length][];
            for (int i = 0; i < curves.Length; i++)
            {
                if (curves[i].IsGamma) values[i] = Conversion.FromGamma(curves[i].Gamma);
                else values[i] = curves[i].CurveData;
            }
            SetCurve(values);
        }

        private void SetCurve(double[][] curves)
        {
            double[] max = new double[curves.Length];
            double[] min = new double[curves.Length];
            for (int i = 0; i < curves.Length; i++)
            {
                max[i] = curves[i].Max();
                min[i] = curves[i].Min();
                //Normalize:
                for (int j = 0; j < curves[i].Length; j++)
                {
                    curves[i][j] = (curves[i][j] - min[i]) / max[i];
                }
            }
            Curve = curves;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Curve == null) return;
            
            Graphics g = e.Graphics;
            g.AntiAlias = true;
            for (int i = 0; i < Curve.Length; i++)
            {
                PaintCurve(Curve[i], CurveColors[i], g);
            }
        }

        private void PaintCurve(double[] points, Color col, Graphics g)
        {
            float w = Width / (points.Length - 1f);
            float h = Height - 1;

            var path = new GraphicsPath();
            path.MoveTo(0, h);
            for (int i = 1; i < points.Length; i++)
            {
                path.LineTo(i * w, (float)(h - (points[i] * h)));
            }
            g.DrawPath(col, path);
        }
    }
}
