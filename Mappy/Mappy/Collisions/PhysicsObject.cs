using SFML.Graphics;

namespace Mappy.Collisions
{
    public abstract class PhysicsObject : IPhysicObject
    {
        protected Vector2D position;

        public Vector2D Position { get { return position; } }

        public abstract bool CollidesWith(IPhysicObject other);

        public virtual void Update(float deltaTime) { }

        public virtual void Render(RenderWindow renderWindow) { }
    }
}
