using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;

using Mappy;

namespace Mappy.Collisions
{
    class CirclePhysicsObject : PhysicsObject
    {
        int radius;

        public int Radius { get { return radius; } }
        public int Diameter { get { return radius * 2; } }

        public CirclePhysicsObject(int radius, Vector2D position)
        {
            this.radius = radius;
            this.position = position;
        }

        public override bool CollidesWith(IPhysicObject other)
        {
            CirclePhysicsObject cpo = ExceptionHelper.AssertIsTAndReturnCasted<CirclePhysicsObject>(other, "CirclePhysicsObject.Collides()");
            float distance = (float)(Math.Abs((decimal)((cpo.position - this.position).Magnitude)));
            // The distance is the magnitude of the vector linking this object's
            // position to the other's position.

            //Because 'radius' is always positive, we have to be sure that
            //'distance' is too. That's the perfect job for Math.Abs().

            return distance < radius;   //It's on the circle hitbox if is inferior
                                       //to the radius of this circle.
        }

        public override string ToString()
        {
            return "R" + radius + " P" + position.ToString();
        }
    }
}
