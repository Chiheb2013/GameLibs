using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Mappy.Textures.Animation
{
    public static class AnimationGroupAnimationManager
    {
        static Dictionary<string, int> animations = new Dictionary<string, int>();

        public static Dictionary<string, int> Animations { get { return animations; } }

        public static int GetAnimation(string name)
        {
            if (AnimationAlreadyExists(name))
                return animations[name];
            else throw new KeyNotFoundException("Symbol '" + name + "' was not found. " +
                                                "source:AGAM.GetAnimation()");
        }

        public static void LoadAnimations(string animationFile)
        {
            if (File.Exists(animationFile))
            {
                string[] lines = StringHelper.GetCleanLines(animationFile);
                GetAnimations(lines);
            }
            else throw new FileNotFoundException("Can't find '" + animationFile + "'. " +
                                             "source:AGAM.LoadAnimations()");
        }

        private static void GetAnimations(string[] lines)
        {
            foreach (string line in lines)
                if (line.StartsWith("animation:"))
                    LoadAnimation(line);
        }

        private static void LoadAnimation(string line)
        {
            string[] parts = line.Replace("animation:", "").Split(',');
            
            string name = parts[0];
            int index = int.Parse(parts[1]);

            AddAnimation(name, index);
        }

        private static void AddAnimation(string name, int index)
        {
            if (!AnimationAlreadyExists(name))
                animations.Add(name, index);
            else throw new ArgumentException("Symbol '" + name + "' already exists. " +
                                             "source:AGAM.AddAnimation()");
        }

        private static bool AnimationAlreadyExists(string name)
        {
            return animations.Keys.Contains(name);
        }
    }
}
