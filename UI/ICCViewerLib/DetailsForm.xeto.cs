using Eto.Forms;
using Eto.Serialization.Xaml;

namespace ICCViewer
{
    public class DetailsForm : Dialog
    {
        Scrollable ScrollContainer { get; set; }

        public DetailsForm(Control ctrl)
        {
            XamlReader.Load(this);
            ScrollContainer.Content = ctrl;
        }
    }
}
