using SFML.Graphics;

namespace Mappy
{
    public interface IPhysicObject : IGameObject
    {
        IntRect Hitbox { get; }

        Vector2D Center { get; }
        Vector2D Position { get; }

        bool Collides(IPhysicObject hitter);
        bool Collides(Vector2D hitterPosition);
    }
}
