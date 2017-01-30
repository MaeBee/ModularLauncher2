using System.Collections.Generic;
using System.Windows.Forms;

namespace ModularLauncher2
{
    public partial class Settings : Form
    {
        Dictionary<string, Dictionary<string, string>> Config;
        public Settings(Dictionary<string, Dictionary<string, string>>  Config)
        {
            this.Config = new Dictionary<string, Dictionary<string, string>>();
            this.Config = Config;
            InitializeComponent();
            // Initialise plugin panels, give it Config[PluginName] to work with.
            foreach (AvailablePlugin pluginOn in Global.Plugins.AvailablePlugins)
            {
                // Initialises each plugin and adds the control to the tab control
                if (pluginOn.Instance.PopulateSettings(Config[pluginOn.Instance.Name]))
                {
                    TabPage NewPage = new TabPage(pluginOn.Instance.Name);
                    pluginOn.Instance.MainInterface.Dock = DockStyle.Fill;
                    NewPage.Controls.Add(pluginOn.Instance.SettingsPanel);
                    tabControl1.TabPages.Add(NewPage);
                    NewPage = null;
                }
            }
        }
    }
}
