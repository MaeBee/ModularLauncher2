using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace ModularLauncher2
{
    public partial class AboutForm : Form
    {
        ArrayList arPlugins;
        string AssemblyProduct, AssemblyVersion, AssemblyCopyright, AssemblyCompany, AssemblyDescription;
        public AboutForm()
        {
            InitializeComponent();
            arPlugins = new ArrayList();
            ReadAssembly();
            lblTitle.Text = AssemblyProduct;
            lblVersion.Text = String.Format("Version {0}", AssemblyVersion);
            lblCopyright.Text = AssemblyCopyright;
            lblCompany.Text = AssemblyCompany;
            txtDescription.Text = AssemblyDescription;
            lstPlugins.Items.Add(AssemblyProduct);
            foreach (Types.AvailablePlugin pluginOn in Global.Plugins.AvailablePlugins)
            {
                arPlugins.Add(pluginOn);
                lstPlugins.Items.Add("Plugin: " + pluginOn.Instance.Name);
            }
            lstPlugins.SelectedIndex = 0;
            this.Update();
        }

        private void ReadAssembly()
        {
            Assembly thisAssembly = this.GetType().Assembly;
            object[] attributes = thisAssembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
            if (attributes.Length == 1)
            {
                AssemblyProduct = ((AssemblyTitleAttribute)attributes[0]).Title;
            }
            AssemblyVersion = thisAssembly.GetName().Version.ToString();
            attributes = thisAssembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
            if (attributes.Length == 1)
            {
                AssemblyCopyright = ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
            attributes = thisAssembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
            if (attributes.Length == 1)
            {
                AssemblyCompany = ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
            attributes = thisAssembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
            if (attributes.Length == 1)
            {
                AssemblyDescription = ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        private void lstPlugins_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstPlugins.SelectedIndex == 0)
            {
                lblTitle.Text = AssemblyProduct;
                lblVersion.Text = String.Format("Version {0}", AssemblyVersion);
                lblCopyright.Text = AssemblyCopyright;
                lblCompany.Text = AssemblyCompany;
                txtDescription.Text = AssemblyDescription;
            }
            else
            {
                Types.AvailablePlugin plugin = (Types.AvailablePlugin)arPlugins[lstPlugins.SelectedIndex - 1];
                if (plugin.Instance.Name == "")
                {
                    lblTitle.Text = "Nameless or corrupted plugin";
                }
                else
                {
                    lblTitle.Text = "Plugin: " + plugin.Instance.Name;
                }
                if (plugin.Instance.Version == "")
                {
                    lblVersion.Text = "Version unknown";
                }
                else
                {
                    lblVersion.Text = "Version " + plugin.Instance.Version;
                }
                if (plugin.Instance.Author == "")
                {
                    lblCopyright.Text = "Author: Unknown";
                }
                else
                {
                    lblCopyright.Text = "Author: " + plugin.Instance.Author;
                }
                lblCompany.Text = null;
                if (plugin.Instance.Description == "")
                {
                    txtDescription.Text = "No description given by plugin author.";
                }
                else
                {
                    txtDescription.Text = plugin.Instance.Description;
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.obbog.net");
        }
    }
}
