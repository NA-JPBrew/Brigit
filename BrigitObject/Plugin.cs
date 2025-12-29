using System;
using System.Collections.Generic;
using System.Linq;
using YukkuriMovieMaker.Plugin;
using YukkuriMovieMaker.Plugin.Shape;
using YukkuriMovieMaker.Plugin.Shape.Animations;
using BrigitObject.Bridge;

namespace BrigitObject
{
    public class Plugin : IPlugin 
    {
        public string Name => "BrigitObject";
        public void Initialize() 
        {
            VersionChecker.CheckAsync("1.0.0", "user/repo"); 
        }
    }

    public class MyShape : IShapePlugin
    {
        public string Name => "BrigitObject";
        public IEnumerable<IAnimatable> GetAnimatables() => Enumerable.Empty<IAnimatable>();
    }
}