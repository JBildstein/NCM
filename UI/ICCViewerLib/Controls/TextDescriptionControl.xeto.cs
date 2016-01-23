using ColorManager.ICC;
using Eto.Forms;
using Eto.Serialization.Xaml;

namespace ICCViewer.Controls
{
    public class TextDescriptionControl : Panel
    {
        TextArea AsciiTextArea { get; set; }
        TextArea UnicodeTextArea { get; set; }
        TextArea ScriptCodeTextArea { get; set; }

        Label LanguageCodeLabel { get; set; }
        Label ScriptCodeLabel { get; set; }

        public TextDescriptionControl()
        {
            XamlReader.Load(this);
        }

        public TextDescriptionControl(TextDescriptionTagDataEntry data)
            : this()
        {
            AsciiTextArea.Text = data.ASCII;
            UnicodeTextArea.Text = data.Unicode;
            ScriptCodeTextArea.Text = data.ScriptCode;

            LanguageCodeLabel.Text = data.UnicodeLanguageCode.ToString();
            ScriptCodeLabel.Text = data.ScriptCodeCode.ToString();
        }
    }
}
