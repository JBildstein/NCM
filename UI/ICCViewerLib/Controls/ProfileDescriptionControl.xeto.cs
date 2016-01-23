using ColorManager.ICC;
using Eto.Forms;
using Eto.Serialization.Xaml;

namespace ICCViewer.Controls
{
    public class ProfileDescriptionControl : Panel
    {
        private Label DeviceManufacturerLabel { get; set; }
        private Label DeviceModelLabel { get; set; }
        private Label TechnologyInformationLabel { get; set; }
        private Label OpacityLabel { get; set; }
        private Label ReflectivityLabel { get; set; }
        private Label PolarityLabel { get; set; }
        private Label ChromaLabel { get; set; }
        private Label VendorDataLabel { get; set; }
        private LocalizedStringControl DeviceManufacturerInfoPanel { get; set; }
        private LocalizedStringControl DeviceModelInfoPanel { get; set; }
        
        public ProfileDescriptionControl()
        {
            XamlReader.Load(this);
        }

        public ProfileDescriptionControl(ProfileDescription data)
            : this()
        {
            DeviceManufacturerLabel.Text = data.DeviceManufacturer.ToString();
            DeviceModelLabel.Text = data.DeviceModel.ToString();
            TechnologyInformationLabel.Text = data.TechnologyInformation.ToString();
            OpacityLabel.Text = data.DeviceAttributes.Opacity.ToString();
            ReflectivityLabel.Text = data.DeviceAttributes.Reflectivity.ToString();
            PolarityLabel.Text = data.DeviceAttributes.Polarity.ToString();
            ChromaLabel.Text = data.DeviceAttributes.Chroma.ToString();
            VendorDataLabel.Text = Conversion.FromBytesShort(data.DeviceAttributes.VendorData, true);

            DeviceManufacturerInfoPanel = new LocalizedStringControl(data.DeviceManufacturerInfo);
            DeviceModelInfoPanel = new LocalizedStringControl(data.DeviceModelInfo);
        }
    }
}
