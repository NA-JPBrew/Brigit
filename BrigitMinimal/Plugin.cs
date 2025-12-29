using System;
using YukkuriMovieMaker.Plugin;
using BrigitMinimal.Bridge;

namespace BrigitMinimal
{
    public class Plugin : IPlugin 
    {
        public string Name => "BrigitMinimal";
        public void Initialize() 
        {
            VersionChecker.CheckAsync("1.0.0", "user/repo"); 
        }
    }
}