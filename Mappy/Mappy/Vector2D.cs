using System;

using SFML.Window;

namespace Mappy
{
    public class Vector2D
    {
        Vector2f internalVector;

        public float X { get { return internalVector.X; } set { internalVector.X = value; } }
        public float Y { get { return internalVector.Y; } set { internalVector.Y = value; } }
        public float Magnitude { get { return (float)Math.Sqrt(X * X + Y * Y); } }

        public Vector2f InternalVector { get { return internalVector; } set { internalVector = value; } }

        static Vector2D zero = new Vector2D();
        public static Vector2D Zero { get { return zero; } }

        public Vector2D()
        {
            this.internalVector = new Vector2f(0, 0);
        }

        public Vector2D(Vector2f vector)
        {
            this.internalVector = vector;
        }

        public Vector2D(float x, float y)
        {
            this.internalVector = new Vector2f(x, y);
        }

        public override int GetHashCode()
        {
            return ((int)X) ^ ((int)Y);
        }

        public bool Equals(Vector2D b)
        {
            return b.X == X && b.Y == Y;
        }

        public void Normalize()
        {
            if (Magnitude != 0)
                internalVector = internalVector / Magnitude;
        }

        public void Limit(float limit)
        {
            if (Magnitude > limit)
            {
                Normalize();
                internalVector = internalVector * limit;
            }
        }

        public override string ToString()
        {
            return "X : " + X + " Y : " + Y + "  |v| : " + Magnitude;
        }

        public static Vector2D operator +(Vector2D a, Vector2D b)
        {
            return new Vector2D(a.internalVector + b.internalVector);
        }

        public static Vector2D operator -(Vector2D a, Vector2D b)
        {
            return new Vector2D(a.internalVector - b.internalVector);
        }

        public static Vector2D operator *(Vector2D a, float scalar)
        {
            return new Vector2D(a.internalVector * scalar);
        }

        public static Vector2D operator /(Vector2D a, float scalar)
        {
            return new Vector2D(a.internalVector / scalar);
        }
    }
}
