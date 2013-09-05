using SFML.Graphics;

using Mappy.States;
using Mappy.Textures;

namespace Mappy.Effects.Background
{
    public class Background : IGameObject
    {
        SFML.Graphics.Sprite sprite;
        SFML.Graphics.Texture texture;

        float pushRate;

        Vector2D push;
        Vector2D position;

        public float PushRate { get { return pushRate; } set { pushRate = value; } }

        public Vector2D Push { get { return push; } set { push = value; } }
        public Vector2D Position { get { return position; } set { position = value; } }

        public Background(string texture, Vector2D position, Vector2D push)
        {
            this.push = push;
            this.pushRate = 0.15f;
            
            this.position = position;

            this.texture = GeneralTextureManager.GetTexture(texture);
            this.sprite = new Sprite(this.texture);
        }

        public void Scale(float factor)
        {
            sprite.Scale = new SFML.Window.Vector2f(factor, factor);
        }

        public void Update(float deltaTime)
        {
            position += push * pushRate * deltaTime;

            CheckPosition();
        }

        private void CheckPosition()
        {
            if (position.X < 0) position.X = StateSystem.RenderWindow.Size.X;
            if (position.X > StateSystem.RenderWindow.Size.X) position.X = 0;
            if (position.Y < 0) position.Y = StateSystem.RenderWindow.Size.Y;
            if (position.Y > StateSystem.RenderWindow.Size.Y) position.Y = 0;
        }

        public void Render(RenderWindow renderWindow)
        {
            sprite.Position = position.InternalVector;
            renderWindow.Draw(sprite);
        }
    }
}
