using System;
using System.Linq;
using ColorManager;
using Eto.Forms;
using Eto.Serialization.Xaml;
using CConverter = ColorManager.ColorConverter;

namespace ColorConverter
{
    public class MainForm : Form
    {
        ColorControl InColorCtrl { get; set; }
        ColorControl OutColorCtrl { get; set; }
        Button ConvertButton { get; set; }

        public MainForm()
        {
            XamlReader.Load(this);
            ConvertButton.Click += ConvertButton_Click;

            //Eto Bug, custom control is not assigned:
            if (InColorCtrl == null)
            {
                InColorCtrl = Children
                    .FirstOrDefault(t => t is ColorControl && t.ID == nameof(InColorCtrl))
                    as ColorControl;
            }

            //Eto Bug, custom control is not assigned:
            if (OutColorCtrl == null)
            {
                OutColorCtrl = Children
                    .FirstOrDefault(t => t is ColorControl && t.ID == nameof(OutColorCtrl))
                    as ColorControl;
            }
        }

        private void ConvertButton_Click(object sender, EventArgs e)
        {
            try
            {
                ConvertButton.Enabled = false;
                Color colIn = InColorCtrl.GetColorInstance();
                Color colOut = OutColorCtrl.GetColorInstance();
                using (CConverter conv = new CConverter(colIn, colOut))
                {
                    conv.Convert();
                }
                OutColorCtrl.SetValues(colOut);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK); }
            finally { ConvertButton.Enabled = true; }
        }
    }
}
