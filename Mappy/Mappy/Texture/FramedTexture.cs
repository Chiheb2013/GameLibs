using SFML.Graphics;

namespace Mappy.Texture
{
    public class FramedTexture : SFML.Graphics.Texture
    {
        Frame frame;
        Sprite tilesetSprite;

        Vector2D position;

        public Vector2D Position { get { return position; } 
            set 
            { 
                position = value;
                tilesetSprite.Position = position.InternalVector;
            } 
        }

        public FramedTexture(string tileset, Frame frame)
            : base(GeneralTextureManager.GetTexture(tileset))
        {
            this.frame = frame;

            this.tilesetSprite = new Sprite(this);
            this.tilesetSprite.TextureRect = frame.FrameRect;
        }

        public void Update(float deltaTime)
        {
        }

        public void Render(RenderWindow renderWindow)
        {
            renderWindow.Draw(tilesetSprite);
        }
    }
}
