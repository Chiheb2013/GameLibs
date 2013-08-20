using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Window;
using SFML.Graphics;

namespace Mappy.Maps
{
    public class Teleporter : Tile, IGameObject, IPhysicObject
    {
        string nextWorld;

        IntRect hitbox;

        public string NextWorld { get { return nextWorld; } }

        IntRect IPhysicObject.Hitbox { get { return hitbox; } }
        Vector2D Center { get { return CollisionHelper.GetCenter(hitbox); } }

        public Teleporter(Vector2D position, string nextWorld, string texture)
            : base(true, texture, position)
        {
            this.nextWorld = nextWorld;
            this.hitbox = CollisionHelper.CreateHitbox(position);
        }

        void IGameObject.Update(float deltaTime)
        {
        }

        bool IPhysicObject.Collides(Vector2D hitterPosition)
        {
            if (hitterPosition == Position)
            {
                base.RaiseCollisionEvent(this, new TeleportationEventArgs(nextWorld));
                return true;
            }
            return false;
        }

        bool IPhysicObject.Collides(IPhysicObject hitter)
        {
            return hitbox.Intersects(hitter.Hitbox);
        }

        void IGameObject.Render(RenderWindow renderWindow)
        {
            base.Render(renderWindow);
        }
    }
}
