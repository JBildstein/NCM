using System;
using System.Collections.Generic;
using Eto.Forms;
using Eto.Drawing;
using Eto.Serialization.Xaml;

namespace ICCViewer.Controls
{
    public class CurveControl : Drawable
    {
        public CurveControl()
        {
            XamlReader.Load(this);
        }

        public CurveControl(double[] curve)
            : this()
        {

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawLine(Colors.Red, 0, 0, 10, 10);
            g.DrawLine(Colors.Blue, 10, 10, 10, 20);
        }
    }
}
