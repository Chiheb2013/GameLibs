using Mappy.Entities;

namespace Mappy
{
    public static class VectorHelper
    {
        public static Vector2D GetDirectionVector(Direction direction)
        {
            Vector2D extra = new Vector2D(1, 1);
            return GetDirectionVector(direction, extra);
        }

        public static Vector2D GetDirectionVector(Direction direction, Vector2D extra)
        {
            if (direction == Direction.Up)
                return new Vector2D(0, -extra.Y);
            if (direction == Direction.Down)
                return new Vector2D(0, extra.Y);
            if (direction == Direction.Left)
                return new Vector2D(-extra.X, 0);
            return new Vector2D(extra.X, 0);
        }
    }
}
