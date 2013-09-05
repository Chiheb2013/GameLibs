using System;
using System.Collections.Generic;

using SFML.Graphics;

namespace Mappy.Textures.Animation
{
    public class AnimationGroup : Sprite, IGameObject
    {
        int interval;
        int currentFrameId;
        float cumulatedInterval;

        int width;
        int height;
        int frameCount;

        string currentAnimation;

        Vector2D frameSize;
        List<Frame> frames;

        public string CurrentAnimation { get { return currentAnimation; } set { currentAnimation = value; } }

        public AnimationGroup(int interval, int framesOnX, int framesOnY,
            string frameset, Vector2D frameSize, int[] references)
        {
            this.interval = interval;
            this.frameSize = frameSize;
            this.frameCount = (width = framesOnX) * (height = framesOnY);
            this.currentAnimation = string.Empty;

            base.Texture = GeneralTextureManager.GetTexture(frameset);

            if (references.Length == frameCount)
                CreateFrames(references);
            else throw new ArgumentException("Invalid references array. source:AnimationGroup cntr");
        }

        public void SetCurrentFrame(int index)
        {
            if (index > 0 && index < frames.Count)
                currentFrameId = index;
            else throw new ArgumentException("Index is invalid. source:AnimationGroup.SetCurrentFrame()");
        }

        public void SetCurrentAnimation(string name)
        {
            SetCurrentAnimation(name, false);
        }

        public void SetCurrentAnimation(string name, bool reset)
        {
            int animationId = AnimationGroupAnimationManager.GetAnimation(name);

            if (!IsCurrentAnimationFrame(animationId) || reset)
            {
                cumulatedInterval = 0;
                currentAnimation = name;
                currentFrameId = animationId;
            }
        }

        public void Reset()
        {
            SetCurrentAnimation(currentAnimation, reset:true);  //i should rename this variable...
        }

        private bool IsCurrentAnimationFrame(int animationId)
        {
            return GetAnimationReferences(animationId).Contains(currentFrameId);
        }

        private List<int> GetAnimationReferences(int animFirst)
        {
            List<int> passed = new List<int>();

            for (int i = animFirst; true; i = frames[i].NextFrameID)
            {
                if (passed.Contains(i)) break;
                passed.Add(i);
            }

            return passed;
        }

        public void Update(float deltaTime)
        {
            cumulatedInterval += deltaTime;

            if (cumulatedInterval >= interval)
            {
                currentFrameId = frames[CurrentFrameIndex].NextFrameID;
                cumulatedInterval = 0;
            }
        }

        private int CurrentFrameIndex
        {
            get
            {
                for (int i = 0; i < frames.Count; i++)
                    if (frames[i].ID == currentFrameId)
                        return i;
                throw new Exception("Id doesn't correspond to any frame. " +
                                    "source:AnimationGroup.CurrentFrameIndex");
            }
        }

        public void Render(RenderWindow renderWindow)
        {
            base.TextureRect = frames[CurrentFrameIndex].FrameRect;
            renderWindow.Draw(this);
        }

        private void CreateFrames(int[] nextFrameIds)
        {
            frames = new List<Frame>();

            for (int i = 0; i < frameCount; i++)
            {
                Vector2D plane = CoordinateSystemConverter.LineToPlane(i, width);
                int x = (int)plane.X;
                int y = (int)plane.Y;

                IntRect frameRect = new IntRect(x * (int)frameSize.X, y * (int)frameSize.Y, (int)frameSize.X, (int)frameSize.Y);
                Frame frame = new Frame(i, nextFrameIds[i], frameRect);

                frames.Add(frame);
            }
        }
    }
}
