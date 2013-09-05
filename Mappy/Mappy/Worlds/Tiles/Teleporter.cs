using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Window;
using SFML.Graphics;

using Mappy.Collisions;

namespace Mappy.Worlds
{
    public class Teleporter : Tile, IPhysicObject
    {
        string nextWorld;

        public string NextWorld { get { return nextWorld; } }

        public Teleporter(Vector2D position, string nextWorld, string texture)
            : base(true, texture, position)
        {
            this.nextWorld = nextWorld;
        }

        public new bool CollidesWith(IPhysicObject other)
        {
            if (base.CollidesWith(other))
            {
                base.RaiseCollisionEvent(this, new TeleportationEventArgs(nextWorld));
                return true;
            }
            return false;
        }
    }
}
