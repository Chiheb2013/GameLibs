using Mappy.Collisions;

namespace Mappy.Textures
{
    public interface IRenderObject : IPhysicObject
    {
        string texture { get; }
    }
}
