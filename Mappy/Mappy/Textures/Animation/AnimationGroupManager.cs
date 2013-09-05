using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Mappy.Textures.Animation
{
    public static class AnimationGroupManager
    {
        static Dictionary<string, AnimationGroup> animationGroups = new Dictionary<string, AnimationGroup>();

        public static Dictionary<string, AnimationGroup> AnimationGroups { get { return animationGroups; } }

        public static AnimationGroup GetAnimationGroup(string name)
        {
            ExceptionHelper.AssertIsInDictionary<AnimationGroup>(animationGroups, name, "AGM.GetAniatmionGroup()");
            return animationGroups[name];
        }

        public static void AddAnimationGroup(string name, AnimationGroup animationGroup)
        {
            ExceptionHelper.AssertIsNotInDictionary<AnimationGroup>(animationGroups, name, "AGM.AddAnimationGroup()");
            animationGroups.Add(name, animationGroup);
        }

        public static bool Contains(string name)
        {
            return animationGroups.Keys.Contains(name);
        }

        public static void LoadAnimationGroups(string animationFile)
        {
            string[] lines = StringHelper.GetCleanLines(animationFile);
            GetAnimationGroups(lines);
        }

        private static void GetAnimationGroups(string[] lines)
        {
            foreach (string line in lines)
                if (line.StartsWith("animation_group:"))
                    LoadAnimationGroup(line);
        }

        const int NAME = 0;
        const int FRAME_SET = 1;
        const int FRAMES_ON_X = 2;
        const int FRAMES_ON_Y = 3;
        const int INTERVAL = 4;
        const int FRAME_WIDTH = 5;
        const int FRAME_HEIGHT = 6;
        const int REFS_START_INDEX = 7;

        private static void LoadAnimationGroup(string line)
        {
            string[] parts = line.Replace("animation_group:", "").Split(',');
            string name = parts[NAME];

            ExceptionHelper.AssertIsNotInDictionary<AnimationGroup>(animationGroups, name, "AGM.LoadAnimationGroup()");

            int interval = int.Parse(parts[INTERVAL]);
            int framesOnX = int.Parse(parts[FRAMES_ON_X]);
            int framesOnY = int.Parse(parts[FRAMES_ON_Y]);
            int frameWidth = int.Parse(parts[FRAME_WIDTH]);
            int frameHeight = int.Parse(parts[FRAME_HEIGHT]);
            string frameset = parts[FRAME_SET];
            int[] references = GetReferences(parts);

            AnimationGroup group = new AnimationGroup(
                    interval, framesOnX, framesOnY, frameset,
                    new Vector2D(frameWidth, frameHeight), references
                );

            animationGroups.Add(name, group);
        }

        private static int[] GetReferences(string[] parts)
        {
            List<int> refs = new List<int>();
            for (int i = REFS_START_INDEX; i < parts.Length; i++)
                refs.Add(int.Parse(parts[i]));
            return refs.ToArray();
        }
    }
}
