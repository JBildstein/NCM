using System;
using System.Text;
using System.Threading.Tasks;
using ColorManager.ICC;
using Eto.Drawing;
using Eto.Forms;
using Eto.Serialization.Xaml;
using ICCViewer.Controls;

namespace ICCViewer
{
    public class MainForm : Form
    {
        #region Variables

        private ICCProfileReader Reader = new ICCProfileReader();
        private string ProfilePath;
        private ICCProfile Profile;

        private Button OpenButton { get; set; }
        private TextBox PathTextBox { get; set; }
        private ListBox TagTableListBox { get; set; }

        private Label ProfileSizeLabel { get; set; }
        private Label CMMTypeLabel { get; set; }
        private Label ProfileVersionLabel { get; set; }
        private Label ProfileClassLabel { get; set; }
        private Label DataColorspaceLabel { get; set; }
        private Label PCSLabel { get; set; }
        private Label CreationDateLabel { get; set; }
        private Label ProfileFileSignatureLabel { get; set; }
        private Label PrimaryPlatformLabel { get; set; }
        private Label ProfileFlagsLabel { get; set; }
        private Label DeviceManufacturerLabel { get; set; }
        private Label DeviceModelLabel { get; set; }
        private Label DeviceAttributesLabel { get; set; }
        private Label RenderingIntentLabel { get; set; }
        private Label PCSIlluminantLabel { get; set; }
        private Label ProfileCreatorLabel { get; set; }
        private Label ProfileIDLabel { get; set; }

        #endregion

        public MainForm()
        {
            XamlReader.Load(this);

            OpenButton.Click += OpenButton_Click;
            TagTableListBox.SelectedIndexChanged += TagTableListBox_SelectedIndexChanged;
            DeviceAttributesLabel.MouseUp += DeviceAttributesLabel_Click;
            ProfileFlagsLabel.MouseUp += ProfileFlagsLabel_Click;

            DeviceAttributesLabel.Cursor = Cursors.Pointer;
            ProfileFlagsLabel.Cursor = Cursors.Pointer;
        }

        #region UI Events

        private async void OpenButton_Click(object sender, EventArgs e)
        {
            string title = Title;
            try
            {
                OpenButton.Enabled = false;
                using (OpenFileDialog dlg = new OpenFileDialog())
                {
                    dlg.Filters.Add(new FileDialogFilter("ICC Profile", ".icc", ".icm"));
                    dlg.Filters.Add(new FileDialogFilter("All Files", ".*"));

                    var res = dlg.ShowDialog(this);
                    if (res == DialogResult.Ok)
                    {
                        ProfilePath = dlg.FileName;
                        PathTextBox.Text = ProfilePath;
                        Title += " - Opening Profile";
                        await Task.Run(() => { Profile = Reader.Read(ProfilePath); });
                        SetHeaderUI();
                        SetTagTableUI();
                    }
                }
            }
            catch (CorruptProfileException cex) { MessageBox.Show(cex.Message, "Profile Error", MessageBoxButtons.OK); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK); }
            finally
            {
                OpenButton.Enabled = true;
                Title = title;
            }
        }

        private void TagTableListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string title = Title;
            try
            {
                Title += " - Loading";
                TagTableListBox.Enabled = false;
                if (TagTableListBox.SelectedIndex > -1)
                {
                    var data = Profile.Data[TagTableListBox.SelectedIndex];
                    Control ctrl = GetControl(data);
                    DetailsForm frm = new DetailsForm(ctrl);
                    frm.Title = data.TagSignature.ToString() + " - " + data.Signature.ToString();
                    frm.Show();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK); }
            finally
            {
                TagTableListBox.Enabled = true;
                Title = title;
            }
        }

        #endregion

        #region Header Detail

        private void ProfileFlagsLabel_Click(object sender, MouseEventArgs e)
        {
            if (Profile == null) return;

            var flag = Profile.Flags;
            string text = "Is Embedded:  " + flag.IsEmbedded + Environment.NewLine;
            text += "Is Independent:  " + flag.IsIndependent + Environment.NewLine;
            text += "Data:  " + flag.Flags[0] + "; " + flag.Flags[1] + Environment.NewLine;
            for (int i = 0; i < flag.Flags.Length; i++) text += "Flag " + i + ": " + flag.Flags[i] + Environment.NewLine;

            MessageBox.Show(text, "Profile Flag", MessageBoxButtons.OK);
        }

        private void DeviceAttributesLabel_Click(object sender, MouseEventArgs e)
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

        #region Create Data Controls

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
                case TypeSignature.TextDescription:
                    return GetControlTextDescription(entry);
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
                    return new Label() { Text = "Unknown Type Signature" };
            }
        }

        private Control GetControlUnknown(TagDataEntry entry)
        {
            var ctrl = entry as UnknownTagDataEntry;
            return CreateTextArea(Conversion.FromBytes(ctrl.Data, true));
        }

        private Control GetControlChromaticity(TagDataEntry entry)
        {
            var ctrl = entry as ChromaticityTagDataEntry;

            StringBuilder txt = new StringBuilder();
            txt.Append($"Channels: {ctrl.ChannelCount}");
            txt.Append($"Colorant Type: {ctrl.ColorantType}");

            for (int i = 0; i < ctrl.ChannelValues.Length; i++)
            {
                txt.Append($"Channel {i}: ");
                for (int j = 0; j < ctrl.ChannelValues[i].Length; j++)
                {
                    txt.Append(ctrl.ChannelValues[i][j].ToString("F3"));
                }
                txt.AppendLine();
            }

            return CreateTextArea(txt.ToString());
        }

        private Control GetControlColorantOrder(TagDataEntry entry)
        {
            var ctrl = entry as ColorantOrderTagDataEntry;

            StringBuilder txt = new StringBuilder();
            for (int i = 0; i < ctrl.ColorantNumber.Length; i++) txt.AppendLine(ctrl.ColorantNumber[i].ToString("F3"));
            return CreateTextArea(txt.ToString());
        }

        private Control GetControlColorantTable(TagDataEntry entry)
        {
            var ctrl = entry as ColorantTableTagDataEntry;

            StringBuilder txt = new StringBuilder();
            for (int i = 0; i < ctrl.ColorantData.Length; i++)
            {
                var d = ctrl.ColorantData[i];
                txt.AppendLine($"{nameof(d.Name)}: {d.Name}");
                txt.AppendLine($"{nameof(d.PCS1)}: {d.PCS1}");
                txt.AppendLine($"{nameof(d.PCS2)}: {d.PCS2}");
                txt.AppendLine($"{nameof(d.PCS3)}: {d.PCS3}");
            }

            return CreateTextArea(txt.ToString());
        }

        private Control GetControlCurve(TagDataEntry entry)
        {
            var ctrl = entry as CurveTagDataEntry;
            if (ctrl.IsGamma) return new Label { Text = $"Gamma: {ctrl.Gamma}" };
            else return new CurveControl(ctrl.CurveData);
        }

        private Control GetControlData(TagDataEntry entry)
        {
            var ctrl = entry as DataTagDataEntry;

            StringBuilder txt = new StringBuilder();
            txt.AppendLine(Conversion.FromBytes(ctrl.Data, false));
            if (ctrl.IsASCII)
            {
                txt.AppendLine();
                txt.AppendLine("ASCII:");
                txt.AppendLine(ctrl.ASCIIString);
            }

            return CreateTextArea(txt.ToString());
        }

        private Control GetControlDateTime(TagDataEntry entry)
        {
            var ctrl = entry as DateTimeTagDataEntry;
            return CreateTextArea(ctrl.Value.ToString());
        }

        private Control GetControlLut16(TagDataEntry entry)
        {
            var ctrl = entry as Lut16TagDataEntry;
            return new LUTControl(ctrl);
        }

        private Control GetControlLut8(TagDataEntry entry)
        {
            var ctrl = entry as Lut8TagDataEntry;
            return new LUTControl(ctrl);
        }

        private Control GetControlLutAToB(TagDataEntry entry)
        {
            var ctrl = entry as LutAToBTagDataEntry;
            return new LUTControl(ctrl);
        }

        private Control GetControlLutBToA(TagDataEntry entry)
        {
            var ctrl = entry as LutBToATagDataEntry;
            return new LUTControl(ctrl);
        }

        private Control GetControlMeasurement(TagDataEntry entry)
        {
            var ctrl = entry as MeasurementTagDataEntry;

            StringBuilder txt = new StringBuilder();
            txt.AppendLine($"{nameof(ctrl.Observer)}: {ctrl.Observer.ToString()}");
            txt.AppendLine($"{nameof(ctrl.Illuminant)}: {ctrl.Illuminant.ToString()}");
            txt.AppendLine($"{nameof(ctrl.Geometry)}: {ctrl.Geometry.ToString()}");
            txt.AppendLine($"{nameof(ctrl.Flare)}: {ctrl.Flare.ToString("F3")}");
            txt.AppendLine($"{nameof(ctrl.XYZBacking)}: {ctrl.XYZBacking.ToString("F3")}");
            return CreateTextArea(txt.ToString());
        }

        private Control GetControlMultiLocalizedUnicode(TagDataEntry entry)
        {
            var ctrl = entry as MultiLocalizedUnicodeTagDataEntry;
            return new LocalizedStringControl(ctrl.Text);
        }

        private Control GetControlMultiProcessElements(TagDataEntry entry)
        {
            //TODO: add MultiProcessElements Control
            var ctrl = entry as MultiProcessElementsTagDataEntry;
            return new Label { Text = "MultiProcessElementsTagDataEntry not implemented yet" };
        }

        private Control GetControlNamedColor2(TagDataEntry entry)
        {
            var ctrl = entry as NamedColor2TagDataEntry;

            StringBuilder txt = new StringBuilder();
            txt.AppendLine($"{nameof(ctrl.VendorFlags)}: {Conversion.FromBytes(ctrl.VendorFlags, true)}");
            txt.AppendLine($"{nameof(ctrl.Prefix)}: {ctrl.Prefix}");
            txt.AppendLine($"{nameof(ctrl.Suffix)}: {ctrl.Suffix}");

            txt.AppendLine($"{nameof(ctrl.Colors)}:");
            for (int i = 0; i < ctrl.Colors.Length; i++)
            {
                var col = ctrl.Colors[i];
                txt.AppendLine($"{nameof(col.Name)}: {col.Name}");

                txt.AppendLine($"{nameof(col.PCScoordinates)}:");
                for (int j = 0; j < col.PCScoordinates.Length; j++) txt.AppendLine($"{col.PCScoordinates[j]};");

                txt.AppendLine($"{nameof(col.DeviceCoordinates)}:");
                for (int j = 0; j < col.DeviceCoordinates.Length; j++) txt.AppendLine($"{col.DeviceCoordinates[j]};");
            }
            return CreateTextArea(txt.ToString());
        }

        private Control GetControlParametricCurve(TagDataEntry entry)
        {
            var ctrl = entry as ParametricCurveTagDataEntry;
            return new CurveControl(ctrl.Curve);
        }

        private Control GetControlProfileSequenceDesc(TagDataEntry entry)
        {
            var ctrl = entry as ProfileSequenceDescTagDataEntry;

            var tabCtrl = new TabControl();
            for (int i = 0; i < ctrl.Descriptions.Length; i++)
            {
                var page = new TabPage();
                var descCtrl = new ProfileDescriptionControl(ctrl.Descriptions[i]);
                page.Content = descCtrl;
                page.Text = $"Entry {i + 1}";
                tabCtrl.Pages.Add(page);
            }
            return tabCtrl;
        }

        private Control GetControlProfileSequenceIdentifier(TagDataEntry entry)
        {
            var ctrl = entry as ProfileSequenceIdentifierTagDataEntry;

            var tabCtrl = new TabControl();
            for (int i = 0; i < ctrl.Data.Length; i++)
            {
                var page = new TabPage();
                var seqCtrl = new ProfileSequenceIdentifierControl(ctrl.Data[i]);
                page.Content = seqCtrl;
                page.Text = $"Entry {i + 1}";
                tabCtrl.Pages.Add(page);
            }
            return tabCtrl;
        }

        private Control GetControlResponseCurveSet16(TagDataEntry entry)
        {
            //TODO: add ResponseCurveSet16 Control
            var ctrl = entry as ResponseCurveSet16TagDataEntry;
            return new Label { Text = "ResponseCurveSet16TagDataEntry not implemented yet" };
        }

        private Control GetControlS15Fixed16Array(TagDataEntry entry)
        {
            var ctrl = entry as Fix16ArrayTagDataEntry;

            StringBuilder txt = new StringBuilder();
            for (int i = 0; i < ctrl.Data.Length; i++) txt.AppendLine($"{ctrl.Data[i].ToString("F3")}");
            return CreateTextArea(txt.ToString());
        }

        private Control GetControlSignature(TagDataEntry entry)
        {
            var ctrl = entry as SignatureTagDataEntry;
            return CreateTextArea(ctrl.SignatureData);
        }

        private Control GetControlText(TagDataEntry entry)
        {
            var ctrl = entry as TextTagDataEntry;
            return CreateTextArea(ctrl.Text);
        }

        private Control GetControlTextDescription(TagDataEntry entry)
        {
            var ctrl = entry as TextDescriptionTagDataEntry;
            return new TextDescriptionControl(ctrl);
        }

        private Control GetControlU16Fixed16Array(TagDataEntry entry)
        {
            var ctrl = entry as UFix16ArrayTagDataEntry;

            StringBuilder txt = new StringBuilder();
            for (int i = 0; i < ctrl.Data.Length; i++) txt.AppendLine($"{ctrl.Data[i].ToString("F3")}");
            return CreateTextArea(txt.ToString());
        }

        private Control GetControlUInt16Array(TagDataEntry entry)
        {
            var ctrl = entry as UInt16ArrayTagDataEntry;

            StringBuilder txt = new StringBuilder();
            for (int i = 0; i < ctrl.Data.Length; i++) txt.AppendLine($"{ctrl.Data[i]}");
            return CreateTextArea(txt.ToString());
        }

        private Control GetControlUInt32Array(TagDataEntry entry)
        {
            var ctrl = entry as UInt32ArrayTagDataEntry;

            StringBuilder txt = new StringBuilder();
            for (int i = 0; i < ctrl.Data.Length; i++) txt.AppendLine($"{ctrl.Data[i]}");
            return CreateTextArea(txt.ToString());
        }

        private Control GetControlUInt64Array(TagDataEntry entry)
        {
            var ctrl = entry as UInt64ArrayTagDataEntry;

            StringBuilder txt = new StringBuilder();
            for (int i = 0; i < ctrl.Data.Length; i++) txt.AppendLine($"{ctrl.Data[i]}");
            return CreateTextArea(txt.ToString());
        }

        private Control GetControlUInt8Array(TagDataEntry entry)
        {
            var ctrl = entry as UInt8ArrayTagDataEntry;

            StringBuilder txt = new StringBuilder();
            for (int i = 0; i < ctrl.Data.Length; i++) txt.AppendLine($"{ctrl.Data[i]}");
            return CreateTextArea(txt.ToString());
        }

        private Control GetControlViewingConditions(TagDataEntry entry)
        {
            var ctrl = entry as ViewingConditionsTagDataEntry;

            StringBuilder txt = new StringBuilder();
            txt.AppendLine($"{nameof(ctrl.IlluminantXYZ)}: {ctrl.IlluminantXYZ.ToString("F3")}");
            txt.AppendLine($"{nameof(ctrl.SurroundXYZ)}: {ctrl.SurroundXYZ.ToString("F3")}");
            txt.AppendLine($"{nameof(ctrl.Illuminant)}: {ctrl.Illuminant.ToString()}");
            return CreateTextArea(txt.ToString());
        }

        private Control GetControlXYZ(TagDataEntry entry)
        {
            var ctrl = entry as XYZTagDataEntry;

            StringBuilder txt = new StringBuilder();
            for (int i = 0; i < ctrl.Data.Length; i++) { txt.AppendLine(ctrl.Data[i].ToString("F3")); }
            return CreateTextArea(txt.ToString());
        }


        private Control CreateTextArea(string value)
        {
            var scroll = new Scrollable();
            var txt = new TextArea();
            txt.ReadOnly = true;
            txt.Font = Fonts.Monospace(10);
            txt.Text = value;
            scroll.Content = txt;
            return scroll;
        }

        #endregion
        
        #region Subroutines

        private void SetHeaderUI()
        {
            ProfileSizeLabel.Text = Profile.Size.ToString() + " bytes";
            CMMTypeLabel.Text = Profile.CMMType;
            ProfileVersionLabel.Text = Profile.Version.ToString();
            ProfileClassLabel.Text = Profile.Class.ToString();
            DataColorspaceLabel.Text = Profile.DataColorspace.ToString();
            PCSLabel.Text = Profile.PCS.ToString();
            CreationDateLabel.Text = Profile.CreationDate.ToString();
            ProfileFileSignatureLabel.Text = Profile.FileSignature;
            PrimaryPlatformLabel.Text = Profile.PrimaryPlatformSignature.ToString();
            ProfileFlagsLabel.Text = Profile.Flags.ToString();
            DeviceManufacturerLabel.Text = Profile.DeviceManufacturer.ToString();
            DeviceModelLabel.Text = Profile.DeviceModel.ToString();
            DeviceAttributesLabel.Text = Profile.DeviceAttributes.ToString();
            RenderingIntentLabel.Text = Profile.RenderingIntent.ToString();
            PCSIlluminantLabel.Text = Profile.PCSIlluminant.ToString("F4");
            ProfileCreatorLabel.Text = Profile.CreatorSignature;
            ProfileIDLabel.Text = Profile.ID.ToString();
        }

        private void SetTagTableUI()
        {
            TagTableListBox.Items.Clear();
            foreach (var tag in Profile.Data)
            {
                TagSignature sig = tag.TagSignature;
                bool defined = Enum.IsDefined(typeof(TagSignature), sig);
                string label = sig.ToString();
                if (!defined)
                {
                    byte[] data = BitConverter.GetBytes((uint)sig);
                    string ascii = Encoding.ASCII.GetString(data);
                    label += $" ({ascii})";
                }
                TagTableListBox.Items.Add(label);
            }
        }

        #endregion
    }
}
