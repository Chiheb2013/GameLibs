using Mappy.Entities;

namespace Mappy.Collisions
{
    public interface IPhysicObject : IGameObject
    {
        bool CollidesWith(IPhysicObject other);
    }
}
