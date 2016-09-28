using System;
using System.Collections;

namespace ModularLauncherUtil
{
    public interface IPlugin
    {
        IPluginHost Host { get; set; }

        string Name { get; }
        string Description { get; }
        int ID { get; }
        string Author { get; }
        string Version { get; }

        System.Windows.Forms.UserControl MainInterface { get; }
        System.Windows.Forms.UserControl SettingsPanel { get; }

        event EventHandler SendNotification;
        event EventHandler LaunchGame;

        void Initialize();
        void Dispose();
    }

    public interface IPluginHost
    {
    }
}
