using SFML.Graphics;

using Mappy.Texture;
using Mappy.Texture.Animation;

namespace Mappy.Maps
{
    public class AnimatedTile : Tile, IGameObject, IPhysicObject
    {
        AnimationGroup animation;

        IntRect IPhysicObject.Hitbox { get { throw new System.NotImplementedException(); } }
        
        public AnimatedTile(float mapx, float mapy, bool isHollow, 
            string animation)
            : base(mapx,mapy,isHollow, "cmd:none")
        {
            this.animation = AnimationGroupManager.GetAnimationGroup(animation);
            this.animation.Position = CoordinateSystemConverter.WorldToPixels(new Vector2D(mapx, mapy)).InternalVector;
        }

        void IGameObject.Update(float deltaTime)
        {
            animation.Update(deltaTime);
        }

        bool IPhysicObject.Collides(Vector2D hitterWorldPosition)
        {
            return base.Collides(hitterWorldPosition);
        }

        bool IPhysicObject.Collides(IPhysicObject hitter)
        {
            return base.Collides(hitter);
        }

        void IGameObject.Render(RenderWindow renderWindow)
        {
            animation.Render(renderWindow);
        }
    }
}
