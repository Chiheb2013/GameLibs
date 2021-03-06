﻿using SFML.Graphics;

using Mappy.Textures;
using Mappy.Collisions;
using Mappy.Textures.Animation;

namespace Mappy.Worlds
{
    public class AnimatedTile : Tile, IPhysicObject
    {
        AnimationGroup animation;
        
        public AnimatedTile(bool isHollow, string animation, Vector2D position)
            : base(isHollow, "cmd:none", position)
        {
            this.animation = AnimationGroupManager.GetAnimationGroup(animation);
            this.animation.Position = CoordinateSystemConverter.WorldToPixels(position).InternalVector;
        }

        public new void Update(float deltaTime)
        {
            base.Update(deltaTime);

            animation.Update(deltaTime);
        }

        public new void Render(RenderWindow renderWindow)
        {
            animation.Render(renderWindow);
        }
    }
}
