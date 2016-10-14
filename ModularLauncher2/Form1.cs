using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ModularLauncherUtil;
using System.IO;

namespace ModularLauncher2
{
    public partial class Form1 : Form
    {
        public Dictionary<string, Dictionary<string, string>> Config = new Dictionary<string, Dictionary<string, string>>();

        public Form1()
        {
            InitializeComponent();
            GetPlugins();
        }

        private void GetPlugins()
        {
            // Get a list of plugins in the Plugins directory
            Global.Plugins.FindPlugins(Application.StartupPath + "\\Plugins");
            foreach (AvailablePlugin pluginOn in Global.Plugins.AvailablePlugins)
            {
                // Initialises each plugin and 
                toolStripStatusLabel1.Text = "Initializing Plugin: " + pluginOn.Instance.Name;
                TabPage NewPage = new TabPage(pluginOn.Instance.Name);
                pluginOn.Instance.MainInterface.Dock = DockStyle.Fill;
                NewPage.Controls.Add(pluginOn.Instance.MainInterface);
                tabControl1.TabPages.Add(NewPage);
                NewPage = null;
            }
            if (Global.Plugins.AvailablePlugins.Count == 0)
            {
                // If no plugins are detected, create a dummy tab page and fill it with a control telling the user that no plugins exist
                toolStripStatusLabel1.Text = "No plugins found";
                TabPage NewPage = new TabPage("WARNING!");
                NoPluginWarning Warning = new NoPluginWarning();
                Warning.Dock = DockStyle.Fill;
                NewPage.Controls.Add(Warning);
                tabControl1.TabPages.Add(NewPage);
                NewPage = null;
            }
            this.Update();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm About = new AboutForm();
            About.ShowDialog(this);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings SettingsWindow = new Settings(Config);
            SettingsWindow.ShowDialog(this);
            //string SourceFile = Application.StartupPath + "\\test\\zipper.zip";
            //MessageBox.Show(SourceFile.Substring(0, SourceFile.LastIndexOf("\\")));
            //Zipper.Unzip(SourceFile);
        }
    }
}
