using System.Collections.Generic;
using ColorManager.ICC;
using Eto.Forms;
using Eto.Serialization.Xaml;

namespace ICCViewer.Controls
{
    public class LocalizedStringControl : Panel
    {
        private TabControl LocaleTabs { get; set; }

        public LocalizedStringControl()
        {
            XamlReader.Load(this);
        }

        public LocalizedStringControl(IEnumerable<LocalizedString> data)
            : this()
        {
            foreach (var lstring in data)
            {
                var tab = new TabPage();
                var scroll = new Scrollable();
                var text = new TextArea();
                text.ReadOnly = true;
                text.Text = lstring.Text;
                tab.Text = lstring.Culture;
                tab.Content = scroll;
                scroll.Content = text;
                LocaleTabs.Pages.Add(tab);
            }
        }
    }
}
