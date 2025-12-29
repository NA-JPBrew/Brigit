using System;
using System.Collections.Generic;
using System.Linq;
using YukkuriMovieMaker.Plugin;
using YukkuriMovieMaker.Plugin.Effects;
using YukkuriMovieMaker.Plugin.Effects.Animations;
using BrigitEffect.Bridge;

namespace BrigitEffect
{
    public class Plugin : IPlugin 
    {
        public string Name => "BrigitEffect";
        public void Initialize() 
        {
            VersionChecker.CheckAsync("1.0.0", "user/repo"); 
        }
    }

    public class MyVideoEffect : VideoEffectBase 
    {
        public override string Label => "BrigitEffect";
        protected override IEnumerable<IAnimatable> GetAnimatables() => Enumerable.Empty<IAnimatable>();
    }

    public static class CoreLogic {
        public static void Run(string msg) { Console.WriteLine($"[Effect] {msg}"); }
    }
}