using System;
using YukkuriMovieMaker.Plugin;
using BrigitWindow.Bridge;

namespace BrigitWindow
{
    public class Plugin : IPlugin 
    {
        public string Name => "BrigitWindow";
        public void Initialize() 
        {
            VersionChecker.CheckAsync("1.0.0", "user/repo"); 
        }
    }

    public class MyDockWindow : IDockableWindow
    {
        public string Title => "BrigitWindow";
        public object Content => new System.Windows.Controls.Button { Content = "Click Me" };
    }
}