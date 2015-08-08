using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ColorManager.ICC;

namespace ICCViewer
{
    public partial class MainForm : Form
    {
        private ICCProfileReader Reader = new ICCProfileReader();
        private string ProfilePath;
        private ICCProfile Profile;

        public MainForm()
        {
            InitializeComponent();
        }

        #region UI Events

        private void OpenButton_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog dlg = new OpenFileDialog())
                {
                    dlg.Filter = "ICC Profile|*.icc;*.icm|All Files|*.*";

                    var res = dlg.ShowDialog();
                    if (res == DialogResult.OK)
                    {
                        ProfilePath = dlg.FileName;
                        PathTextBox.Text = ProfilePath;
                        Profile = Reader.Read(ProfilePath);
                        SetHeaderUI();
                        SetTagTableUI();
                    }
                }
            }
            catch (CorruptProfileException cex) { MessageBox.Show(cex.Message, "Profile Error", MessageBoxButtons.OK); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK); }
        }

        private void TagTableListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TagTableListBox.SelectedIndex > -1)
            {
                using (DetailsForm frm = new DetailsForm())
                {
                    var data = Profile.Data[TagTableListBox.SelectedIndex];
                    var ctrl = GetControl(data);
                    frm.Controls.Add(ctrl);
                    frm.Text = data.TagSignature.ToString() + " - " + data.Signature.ToString();
                    frm.ShowDialog(this);
                }
            }
        }

        #endregion

        #region Header Detail

        private void ProfileFlagsLabel_Click(object sender, EventArgs e)
        {
            if (Profile == null) return;

            var flag = Profile.Flags;
            string text = "Is Embedded:  " + flag.IsEmbedded + Environment.NewLine;
            text += "Is Independent:  " + flag.IsIndependent + Environment.NewLine;
            text += "Data:  " + flag.Flags[0] + "; " + flag.Flags[1] + Environment.NewLine;
            for (int i = 0; i < flag.Flags.Length; i++) text += "Flag " + i + ": " + flag.Flags[i] + Environment.NewLine;

            MessageBox.Show(text, "Profile Flag", MessageBoxButtons.OK);
        }

        private void DeviceAttributesLabel_Click(object sender, EventArgs e)
        {
            if (Profile == null) return;

            var att = Profile.DeviceAttributes;
            string text = "Opacity:  " + att.Opacity.ToString() + Environment.NewLine;
            text += "Reflectivity:  " + att.Reflectivity.ToString() + Environment.NewLine;
            text += "Polarity:  " + att.Polarity.ToString() + Environment.NewLine;
            text += "Chroma:  " + att.Chroma.ToString() + Environment.NewLine;
            for (int i = 0; i < att.VendorData.Length; i++) text += "Vendor Data Flag " + i + ":  " + att.VendorData[i] + Environment.NewLine;

            MessageBox.Show(text, "Device Attributes", MessageBoxButtons.OK);
        }

        #endregion

        //TODO: implement specific controls for each ICC tag type

        #region Show Data

        private Control GetControl(TagDataEntry entry)
        {
            switch (entry.Signature)
            {
                case TypeSignature.Unknown:
                    return GetControlUnknown(entry);
                case TypeSignature.Chromaticity:
                    return GetControlChromaticity(entry);
                case TypeSignature.ColorantOrder:
                    return GetControlColorantOrder(entry);
                case TypeSignature.ColorantTable:
                    return GetControlColorantTable(entry);
                case TypeSignature.Curve:
                    return GetControlCurve(entry);
                case TypeSignature.Data:
                    return GetControlData(entry);
                case TypeSignature.DateTime:
                    return GetControlDateTime(entry);
                case TypeSignature.Lut16:
                    return GetControlLut16(entry);
                case TypeSignature.Lut8:
                    return GetControlLut8(entry);
                case TypeSignature.LutAToB:
                    return GetControlLutAToB(entry);
                case TypeSignature.LutBToA:
                    return GetControlLutBToA(entry);
                case TypeSignature.Measurement:
                    return GetControlMeasurement(entry);
                case TypeSignature.MultiLocalizedUnicode:
                    return GetControlMultiLocalizedUnicode(entry);
                case TypeSignature.MultiProcessElements:
                    return GetControlMultiProcessElements(entry);
                case TypeSignature.NamedColor2:
                    return GetControlNamedColor2(entry);
                case TypeSignature.ParametricCurve:
                    return GetControlParametricCurve(entry);
                case TypeSignature.ProfileSequenceDesc:
                    return GetControlProfileSequenceDesc(entry);
                case TypeSignature.ProfileSequenceIdentifier:
                    return GetControlProfileSequenceIdentifier(entry);
                case TypeSignature.ResponseCurveSet16:
                    return GetControlResponseCurveSet16(entry);
                case TypeSignature.S15Fixed16Array:
                    return GetControlS15Fixed16Array(entry);
                case TypeSignature.Signature:
                    return GetControlSignature(entry);
                case TypeSignature.Text:
                    return GetControlText(entry);
                case TypeSignature.U16Fixed16Array:
                    return GetControlU16Fixed16Array(entry);
                case TypeSignature.UInt16Array:
                    return GetControlUInt16Array(entry);
                case TypeSignature.UInt32Array:
                    return GetControlUInt32Array(entry);
                case TypeSignature.UInt64Array:
                    return GetControlUInt64Array(entry);
                case TypeSignature.UInt8Array:
                    return GetControlUInt8Array(entry);
                case TypeSignature.ViewingConditions:
                    return GetControlViewingConditions(entry);
                case TypeSignature.XYZ:
                    return GetControlXYZ(entry);

                default:
                    return new Label() { Text = "N/A" };
            }
        }

        private Control GetControlUnknown(TagDataEntry entry)
        {
            var ctrl = entry as UnknownTagDataEntry;
                        
            return CreateTextBox(FromBytes(ctrl.Data, true));
        }

        private Control GetControlChromaticity(TagDataEntry entry)
        {
            var ctrl = entry as ChromaticityTagDataEntry;

            string txt;
            txt = "Channels: " + ctrl.ChannelCount;
            txt += "Colorant Type: " + ctrl.ColorantType.ToString();

            for (int i = 0; i < ctrl.ChannelValues.Length; i++)
            {
                txt += "Channel " + i + ": ";
                for (int j = 0; j < ctrl.ChannelValues[i].Length; j++)
                {
                    txt += ctrl.ChannelValues[i][j].ToString("F3");
                }
                txt += Environment.NewLine;
            }

            return CreateTextBox(txt);
        }

        private Control GetControlColorantOrder(TagDataEntry entry)
        {
            var ctrl = entry as ColorantOrderTagDataEntry;

            var lb = new Label();
            lb.AutoSize = false;
            lb.Dock = DockStyle.Fill;

            lb.Text = entry.Signature.ToString();

            return lb;
        }

        private Control GetControlColorantTable(TagDataEntry entry)
        {
            var ctrl = entry as ColorantTableTagDataEntry;

            StringBuilder txt = new StringBuilder();

            for (int i = 0; i < ctrl.ColorantData.Length; i++)
            {
                var d = ctrl.ColorantData[i];
                txt.Append($"{nameof(d.Name)}: {d.Name}{Environment.NewLine}");
                txt.Append($"{nameof(d.PCS1)}: {d.PCS1}{Environment.NewLine}");
                txt.Append($"{nameof(d.PCS2)}: {d.PCS2}{Environment.NewLine}");
                txt.Append($"{nameof(d.PCS3)}: {d.PCS3}{Environment.NewLine}");
            }

            return CreateTextBox(txt.ToString());
        }

        private Control GetControlCurve(TagDataEntry entry)
        {
            var ctrl = entry as CurveTagDataEntry;

            var lb = new Label();
            lb.AutoSize = false;
            lb.Dock = DockStyle.Fill;

            lb.Text = entry.Signature.ToString();

            return lb;
        }

        private Control GetControlData(TagDataEntry entry)
        {
            var ctrl = entry as DataTagDataEntry;

            StringBuilder txt = new StringBuilder();
            txt.Append(FromBytes(ctrl.Data, false));
            if (ctrl.IsASCII)
            {
                txt.Append(Environment.NewLine);
                txt.Append("ASCII");
                txt.Append(Environment.NewLine);
                txt.Append(ctrl.ASCIIString);
            }

            return CreateTextBox(txt.ToString());
        }

        private Control GetControlDateTime(TagDataEntry entry)
        {
            var ctrl = entry as DateTimeTagDataEntry;
            
            return CreateTextBox(ctrl.Value.ToString());
        }

        private Control GetControlLut16(TagDataEntry entry)
        {
            var ctrl = entry as Lut16TagDataEntry;

            var lb = new Label();
            lb.AutoSize = false;
            lb.Dock = DockStyle.Fill;

            lb.Text = entry.Signature.ToString();

            return lb;
        }

        private Control GetControlLut8(TagDataEntry entry)
        {
            var ctrl = entry as Lut8TagDataEntry;

            var lb = new Label();
            lb.AutoSize = false;
            lb.Dock = DockStyle.Fill;

            lb.Text = entry.Signature.ToString();

            return lb;
        }

        private Control GetControlLutAToB(TagDataEntry entry)
        {
            var ctrl = entry as LutAToBTagDataEntry;

            var lb = new Label();
            lb.AutoSize = false;
            lb.Dock = DockStyle.Fill;

            lb.Text = entry.Signature.ToString();

            return lb;
        }

        private Control GetControlLutBToA(TagDataEntry entry)
        {
            var ctrl = entry as LutBToATagDataEntry;

            var lb = new Label();
            lb.AutoSize = false;
            lb.Dock = DockStyle.Fill;

            lb.Text = entry.Signature.ToString();

            return lb;
        }

        private Control GetControlMeasurement(TagDataEntry entry)
        {
            var ctrl = entry as MeasurementTagDataEntry;

            StringBuilder txt = new StringBuilder();
            txt.Append($"{nameof(ctrl.Observer)}: {ctrl.Observer.ToString()}");
            txt.Append(Environment.NewLine);

            txt.Append($"{nameof(ctrl.Illuminant)}: {ctrl.Illuminant.ToString()}");
            txt.Append(Environment.NewLine);

            txt.Append($"{nameof(ctrl.Geometry)}: {ctrl.Geometry.ToString()}");
            txt.Append(Environment.NewLine);

            txt.Append($"{nameof(ctrl.Flare)}: {ctrl.Flare.ToString("F3")}");
            txt.Append(Environment.NewLine);

            txt.Append($"{nameof(ctrl.XYZBacking)}: {ctrl.XYZBacking.ToString("F3")}");
            txt.Append(Environment.NewLine);

            return CreateTextBox(txt.ToString());
        }

        private Control GetControlMultiLocalizedUnicode(TagDataEntry entry)
        {
            var ctrl = entry as MultiLocalizedUnicodeTagDataEntry;

            StringBuilder txt = new StringBuilder();
            for (int i = 0; i < ctrl.Text.Length; i++)
            {
                txt.Append(FromLocalized(ctrl.Text[i]));
                txt.Append(Environment.NewLine);
            }
            return CreateTextBox(txt.ToString());
        }

        private Control GetControlMultiProcessElements(TagDataEntry entry)
        {
            var ctrl = entry as MultiProcessElementsTagDataEntry;

            var lb = new Label();
            lb.AutoSize = false;
            lb.Dock = DockStyle.Fill;

            lb.Text = entry.Signature.ToString();

            return lb;
        }

        private Control GetControlNamedColor2(TagDataEntry entry)
        {
            var ctrl = entry as NamedColor2TagDataEntry;

            StringBuilder txt = new StringBuilder();

            txt.Append($"{nameof(ctrl.VendorFlag)}: {FromBytes(ctrl.VendorFlag, true)}");
            txt.Append(Environment.NewLine);

            txt.Append($"{nameof(ctrl.Prefix)}: {ctrl.Prefix}");
            txt.Append(Environment.NewLine);

            txt.Append($"{nameof(ctrl.Suffix)}: {ctrl.Suffix}");
            txt.Append(Environment.NewLine);

            txt.Append($"{nameof(ctrl.Colors)}:{Environment.NewLine}");
            for (int i = 0; i < ctrl.Colors.Length; i++)
            {
                var col = ctrl.Colors[i];
                txt.Append($"{nameof(col.Name)}: {col.Name}");
                txt.Append(Environment.NewLine);

                txt.Append($"{nameof(col.PCScoordinates)}:");
                for (int j = 0; j < col.PCScoordinates.Length; j++) txt.Append($"{col.PCScoordinates[j]};");
                txt.Append(Environment.NewLine);

                txt.Append($"{nameof(col.DeviceCoordinates)}:");
                for (int j = 0; j < col.DeviceCoordinates.Length; j++) txt.Append($"{col.DeviceCoordinates[j]};");
                txt.Append(Environment.NewLine);
            }
            return CreateTextBox(txt.ToString());
        }

        private Control GetControlParametricCurve(TagDataEntry entry)
        {
            var ctrl = entry as ParametricCurveTagDataEntry;

            var lb = new Label();
            lb.AutoSize = false;
            lb.Dock = DockStyle.Fill;

            lb.Text = entry.Signature.ToString();

            return lb;
        }

        private Control GetControlProfileSequenceDesc(TagDataEntry entry)
        {
            var ctrl = entry as ProfileSequenceDescTagDataEntry;

            var lb = new Label();
            lb.AutoSize = false;
            lb.Dock = DockStyle.Fill;

            lb.Text = entry.Signature.ToString();

            return lb;
        }

        private Control GetControlProfileSequenceIdentifier(TagDataEntry entry)
        {
            var ctrl = entry as ProfileSequenceIdentifierTagDataEntry;

            var lb = new Label();
            lb.AutoSize = false;
            lb.Dock = DockStyle.Fill;

            lb.Text = entry.Signature.ToString();

            return lb;
        }

        private Control GetControlResponseCurveSet16(TagDataEntry entry)
        {
            var ctrl = entry as ResponseCurveSet16TagDataEntry;

            var lb = new Label();
            lb.AutoSize = false;
            lb.Dock = DockStyle.Fill;

            lb.Text = entry.Signature.ToString();

            return lb;
        }

        private Control GetControlS15Fixed16Array(TagDataEntry entry)
        {
            var ctrl = entry as Fix16ArrayTagDataEntry;

            StringBuilder txt = new StringBuilder();
            for (int i = 0; i < ctrl.Data.Length; i++) txt.Append($"{ctrl.Data[i].ToString("F3")}{Environment.NewLine}");
            return CreateTextBox(txt.ToString());
        }

        private Control GetControlSignature(TagDataEntry entry)
        {
            var ctrl = entry as SignatureTagDataEntry;
            return CreateTextBox(ctrl.SignatureData);
        }

        private Control GetControlText(TagDataEntry entry)
        {
            var ctrl = entry as TextTagDataEntry;
            return CreateTextBox(ctrl.Text);
        }

        private Control GetControlU16Fixed16Array(TagDataEntry entry)
        {
            var ctrl = entry as UFix16ArrayTagDataEntry;

            StringBuilder txt = new StringBuilder();
            for (int i = 0; i < ctrl.Data.Length; i++) txt.Append($"{ctrl.Data[i].ToString("F3")}{Environment.NewLine}");
            return CreateTextBox(txt.ToString());
        }

        private Control GetControlUInt16Array(TagDataEntry entry)
        {
            var ctrl = entry as UInt16ArrayTagDataEntry;

            StringBuilder txt = new StringBuilder();
            for (int i = 0; i < ctrl.Data.Length; i++) txt.Append($"{ctrl.Data[i]}{Environment.NewLine}");
            return CreateTextBox(txt.ToString());
        }

        private Control GetControlUInt32Array(TagDataEntry entry)
        {
            var ctrl = entry as UInt32ArrayTagDataEntry;

            StringBuilder txt = new StringBuilder();
            for (int i = 0; i < ctrl.Data.Length; i++) txt.Append($"{ctrl.Data[i]}{Environment.NewLine}");
            return CreateTextBox(txt.ToString());
        }

        private Control GetControlUInt64Array(TagDataEntry entry)
        {
            var ctrl = entry as UInt64ArrayTagDataEntry;

            StringBuilder txt = new StringBuilder();
            for (int i = 0; i < ctrl.Data.Length; i++) txt.Append($"{ctrl.Data[i]}{Environment.NewLine}");
            return CreateTextBox(txt.ToString());
        }

        private Control GetControlUInt8Array(TagDataEntry entry)
        {
            var ctrl = entry as UInt8ArrayTagDataEntry;

            StringBuilder txt = new StringBuilder();
            for (int i = 0; i < ctrl.Data.Length; i++) txt.Append($"{ctrl.Data[i]}{Environment.NewLine}");
            return CreateTextBox(txt.ToString());
        }

        private Control GetControlViewingConditions(TagDataEntry entry)
        {
            var ctrl = entry as ViewingConditionsTagDataEntry;

            StringBuilder txt = new StringBuilder();
            txt.Append($"{nameof(ctrl.IlluminantXYZ)}: {ctrl.IlluminantXYZ.ToString("F3")}");
            txt.Append(Environment.NewLine);

            txt.Append($"{nameof(ctrl.SurroundXYZ)}: {ctrl.SurroundXYZ.ToString("F3")}");
            txt.Append(Environment.NewLine);

            txt.Append($"{nameof(ctrl.Illuminant)}: {ctrl.Illuminant.ToString()}");

            return CreateTextBox(txt.ToString());
        }

        private Control GetControlXYZ(TagDataEntry entry)
        {
            var ctrl = entry as XYZTagDataEntry;

            StringBuilder txt = new StringBuilder();
            for (int i = 0; i < ctrl.Data.Length; i++) txt.Append(ctrl.Data[i].ToString("F3") + Environment.NewLine);
            return CreateTextBox(txt.ToString());
        }


        #endregion

        #region GetString

        private string FromBytes(byte[] data, bool ASCII)
        {
            string txt = string.Empty;
            string val;
            for (int i = 0; i < data.Length; i++)
            {
                val = data[i].ToString().PadLeft(3, '0');
                if ((i + 1) % 10 == 0) txt += val + Environment.NewLine;
                else if (i + 1 == data.Length) txt += val;
                else txt += val.PadRight(6);
            }

            if (ASCII)
            {
                txt += Environment.NewLine + Environment.NewLine + "ASCII:" + Environment.NewLine;
                val = Encoding.ASCII.GetString(data);

                string tmp;
                for (int i = 0; i < val.Length; i++)
                {
                    if (val[i] == '\0') tmp = "\\0";
                    else if (val[i] == '\r') tmp = "\\r";
                    else if (val[i] == '\n') tmp = "\\n";
                    else if (val[i] == ' ') tmp = "spc";
                    else tmp = val[i].ToString();
                    tmp = tmp.PadLeft(3);
                    if ((i + 1) % 10 == 0) txt += tmp + Environment.NewLine;
                    else if (i + 1 == data.Length) txt += tmp;
                    else txt += tmp.PadRight(6);
                }
            }

            return txt;
        }
        
        private string FromLocalized(LocalizedString lstring)
        {
            return $"{nameof(lstring.Locale)}: {lstring.Locale.ToString()}{Environment.NewLine}{lstring.Text}";            
        }

        #endregion

        #region Create Control
        
        private TextBox CreateTextBox(string value)
        {
            var txt = new TextBox();
            txt.ReadOnly = true;
            txt.Multiline = true;
            txt.WordWrap = false;
            txt.ScrollBars = ScrollBars.Both;
            txt.BorderStyle = BorderStyle.None;
            txt.Dock = DockStyle.Fill;
            txt.Text = value;
            txt.Select(0, 0);
            return txt;
        }

        private PictureBox CreateCurve(ResponseCurve value)
        {
            return new PictureBox();
        }

        #endregion

        #region Subroutines

        private void SetHeaderUI()
        {
            ProfileSizeLabel.Text = Profile.Size.ToString() + " bytes";
            CMMTypeLabel.Text = Profile.CMMType;
            ProfileVersionNumberLabel.Text = Profile.Version.ToString();
            ProfileClassLabel.Text = Profile.Class.ToString();
            DataColorspaceLabel.Text = Profile.DataColorspaceType.ToString();
            PCSLabel.Text = Profile.PCSType.ToString();
            CreationDateLabel.Text = Profile.CreationDate.ToString();
            ProfileFileSignatureLabel.Text = Profile.FileSignature;
            PrimaryPlatformSignatureLabel.Text = Profile.PrimaryPlatformSignature.ToString();
            ProfileFlagsLabel.Text = Profile.Flags.ToString();
            DeviceManufacturerLabel.Text = Profile.DeviceManufacturer.ToString();
            DeviceAttributesLabel.Text = Profile.DeviceAttributes.ToString();
            RenderingIntentLabel.Text = Profile.RenderingIntent.ToString();
            PCSIlluminantLabel.Text = Profile.PCSIlluminant.ToString("F4");
            ProfileCreatorSignatureLabel.Text = Profile.CreatorSignature;
            ProfileIDLabel.Text = Profile.ID.StringValue;
        }

        private void SetTagTableUI()
        {
            TagTableListBox.Items.Clear();
            foreach (var tag in Profile.Data)
            {
                TagTableListBox.Items.Add(tag.TagSignature.ToString());
            }
        }

        #endregion
    }
}
