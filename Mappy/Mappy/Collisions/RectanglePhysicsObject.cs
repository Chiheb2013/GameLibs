using SFML.Graphics;

namespace Mappy.Collisions
{
    public class RectanglePhysicsObject : PhysicsObject
    {
        IntRect hitbox;

        public IntRect Hitbox { get { return hitbox; } }

        public RectanglePhysicsObject(Vector2D position)
        {
            this.position = position;
            this.hitbox = CollisionHelper.CreateHitbox(position);
        }

        public RectanglePhysicsObject(Vector2D position, IntRect hitbox)
        {
            this.hitbox = hitbox;
            this.position = position;
        }

        public override bool CollidesWith(IPhysicObject other)
        {
            RectanglePhysicsObject rpo = ExceptionHelper.AssertIsT<RectanglePhysicsObject>(other,"RectanglePhysicsObject.Collides()");
            return rpo.hitbox.Intersects(this.hitbox);
        }

        public override void Update(float deltaTime)
        {
            hitbox.Top = (int)position.Y;
            hitbox.Left = (int)position.X;
        }

        public override string ToString()
        {
            return "H" + hitbox.ToString() + " P" + position.ToString();
        }
    }
}
