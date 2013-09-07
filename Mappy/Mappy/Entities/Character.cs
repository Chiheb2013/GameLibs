using System;

using SFML.Graphics;

using Mappy.Worlds;
using Mappy.Collisions;
using Mappy.Textures.Animation;

namespace Mappy.Entities
{
    public class Character : RectanglePhysicsObject
    {
        float deltaTime;
        int collisionLayer;

        IPhysicObject world;

        Vector2D speed;

        AnimationGroup animation;
        DirectionAnimationCouple animationCouples;

        public World World { get { return (World)world; } }
        public LayeredWorld LayeredWorld { get { return (LayeredWorld)world; } }

        public AnimationGroup Animation { get { return animation; } }
        public DirectionAnimationCouple AnimationCouples { get { return animationCouples; } }

        public Character(Vector2D position, Vector2D speed, DirectionAnimationCouple animationCouples,
            World world, string animation, int collisionLayer)
            : base(position)
        {
            this.collisionLayer = collisionLayer;
            this.world = world is LayeredWorld ? (LayeredWorld)world : world;

            this.speed = speed;
            this.position = position;
            this.animationCouples = animationCouples;
            this.animation = AnimationGroupManager.GetAnimationGroup(animation);

            this.animation.CurrentAnimation = animationCouples[Direction.Down];
        }

        public void ResetAnimation()
        {
            animation.Reset();
        }

        public override void Update(float deltaTime)
        {
            this.deltaTime = deltaTime;

            base.Update(deltaTime);
        }

        public void Move(Direction direction)
        {
            Vector2D movement = VectorHelper.GetDirectionVector(direction, speed);

            animation.SetCurrentAnimation(animationCouples[direction]);
            animation.Update(deltaTime);

            if (!Collides(direction))
            {
                position += movement;
                base.UpdateHitboxPosition();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Collides");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }

        private bool Collides(Direction direction)
        {
            if (world is LayeredWorld)
            {
                LayeredWorld lworld = (LayeredWorld)world;
                lworld.Layer = collisionLayer;

                return lworld.CollidesWith(this, direction);
            }
            return world.CollidesWith(this);
        }

        public override void Render(RenderWindow renderWindow)
        {
            animation.Position = position.InternalVector;
            animation.Render(renderWindow);

            base.Render(renderWindow);
        }
    }
}
