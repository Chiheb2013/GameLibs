using SFML.Graphics;

namespace Mappy.Collisions
{
    public class RectanglePhysicsObject : PhysicsObject
    {
        IntRect hitbox;

        public IntRect Hitbox { get { return hitbox; } }

        public RectanglePhysicsObject(Vector2D position, bool useTextureSize = false)
        {
            this.hitbox = CollisionHelper.CreateHitbox(position, useTextureSize);
            this.position = new Vector2D(hitbox.Left, hitbox.Top);
        }

        public override bool CollidesWith(IPhysicObject other)
        {
            RectanglePhysicsObject rpo = ExceptionHelper.AssertIsTAndReturnCasted<RectanglePhysicsObject>(other,"RectanglePhysicsObject.Collides()");
            return rpo.hitbox.Intersects(this.hitbox);
        }

        public override void Update(float deltaTime)
        {
            UpdateHitboxPosition();
        }

        public void UpdateHitboxPosition()
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
