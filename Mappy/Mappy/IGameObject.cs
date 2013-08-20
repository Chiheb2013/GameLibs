using SFML.Graphics;

namespace Mappy
{
    public interface IGameObject
    {
        void Update(float deltaTime);
        void Render(RenderWindow renderWindow);
    }
}
