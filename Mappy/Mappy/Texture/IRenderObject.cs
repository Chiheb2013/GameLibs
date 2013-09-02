using Mappy.Collisions;

namespace Mappy.Texture
{
    public interface IRenderObject : IPhysicObject
    {
        string texture { get; }
    }
}
