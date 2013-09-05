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
            if (!Collides(direction))
            {
                Vector2D movement = GetMovement(direction);
                animation.CurrentAnimation = animationCouples[direction];

                position += movement;
            }
        }

        private bool Collides(Direction direction)
        {
            if (world is LayeredWorld)
            {
                LayeredWorld lworld = (LayeredWorld)world;
                lworld.Layer = collisionLayer;

                return lworld.CollidesWith(this);
            }
            return world.CollidesWith(this);
        }

        private Vector2D GetMovement(Direction direction)
        {
            if (direction == Direction.Up)
                return new Vector2D(0, -speed.Y);
            if (direction == Direction.Down)
                return new Vector2D(0, speed.Y);
            if (direction == Direction.Left)
                return new Vector2D(-speed.X, 0);
            return new Vector2D(speed.X, 0);
        }

        public override void Render(RenderWindow renderWindow)
        {
            animation.Position = position.InternalVector;
            animation.Render(renderWindow);

            base.Render(renderWindow);
        }
    }
}
