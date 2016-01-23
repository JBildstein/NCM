using Eto.Forms;
using Eto.Serialization.Xaml;
using ColorManager.ICC;

namespace ICCViewer.Controls
{
    public class ProfileSequenceIdentifierControl : Panel
    {
        Label ProfileIDLabel { get; set; }
        LocalizedStringControl EntryLabeDescriptionPanell { get; set; }

        public ProfileSequenceIdentifierControl()
        {
            XamlReader.Load(this);
        }

        public ProfileSequenceIdentifierControl(ProfileSequenceIdentifier data)
            : this()
        {
            ProfileIDLabel.Text = data.ID.ToString();
            EntryLabeDescriptionPanell = new LocalizedStringControl(data.Description);
        }
    }
}
