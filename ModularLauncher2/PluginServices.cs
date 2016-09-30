using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using ModularLauncherUtil;

namespace ModularLauncher2
{
    class PluginServices : IPluginHost
    {
        public PluginServices()
        {
        }

        private PluginList colAvailablePlugins = new PluginList();

        public PluginList AvailablePlugins
        {
            get { return colAvailablePlugins; }
            set { colAvailablePlugins = value; }
        }

        public void FindPlugins()
        {
            FindPlugins(AppDomain.CurrentDomain.BaseDirectory);
        }

        public void FindPlugins(string Path)
        {
            colAvailablePlugins.Clear();
            if (Directory.Exists(Path))
            {
                foreach (string fileOn in Directory.GetFiles(Path))
                {
                    FileInfo file = new FileInfo(fileOn);
                    if (file.Extension.Equals(".dll"))
                    {
                        this.AddPlugin(fileOn);
                    }
                }
            }
        }

        public void ClosePlugins()
        {
            foreach (AvailablePlugin pluginOn in colAvailablePlugins)
            {
                pluginOn.Instance.Dispose();
                pluginOn.Instance = null;
            }
            colAvailablePlugins.Clear();
        }

        private void AddPlugin(string FileName)
        {
            Assembly pluginAssembly = Assembly.LoadFrom(FileName);
            foreach (Type pluginType in pluginAssembly.GetTypes())
            {
                if (pluginType.IsPublic)
                {
                    if (!pluginType.IsAbstract)
                    {
                        Type typeInterface = pluginType.GetInterface("ModularLauncherUtil.IPlugin", true);
                        if (typeInterface != null)
                        {
                            AvailablePlugin newPlugin = new AvailablePlugin();
                            newPlugin.AssemblyPath = FileName;
                            newPlugin.Instance = (IPlugin)Activator.CreateInstance(pluginAssembly.GetType(pluginType.ToString()));
                            newPlugin.Instance.Host = this;
                            newPlugin.Instance.Initialize();
                            this.colAvailablePlugins.Add(newPlugin);
                            newPlugin = null;
                        }
                        typeInterface = null;
                    }
                }
            }
            pluginAssembly = null;
        }
    }

    public class PluginList : System.Collections.CollectionBase
    {
        // List of Plugins
        public void Add(AvailablePlugin pluginToAdd)
        {
            this.List.Add(pluginToAdd);
        }
        public void Remove(AvailablePlugin pluginToRemove)
        {
            this.List.Remove(pluginToRemove);
        }
        public AvailablePlugin Find(string pluginNameOrPath)
        {
            AvailablePlugin toReturn = null;
            foreach (AvailablePlugin pluginOn in this.List)
            {
                if ((pluginOn.Instance.Name.Equals(pluginNameOrPath)) || pluginOn.AssemblyPath.Equals(pluginNameOrPath))
                {
                    toReturn = pluginOn;
                    break;
                }
            }
            return toReturn;
        }
    }

    public class AvailablePlugin
    {
        // Wrapper for plugin instances
        private IPlugin myInstance = null;
        private string myAssemblyPath = "";

        public IPlugin Instance
        {
            get { return myInstance; }
            set { myInstance = value; }
        }
        public string AssemblyPath
        {
            get { return myAssemblyPath; }
            set { myAssemblyPath = value; }
        }
    }
}
