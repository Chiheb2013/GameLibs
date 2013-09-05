namespace Mappy.Textures
{
    class GeneralTexture : IGameObject
    {
        SFML.Graphics.Sprite sprite;
        SFML.Graphics.Texture texture;

        Vector2D position;

        public Vector2D Position { get { return position; } set { position = value; } }

        public GeneralTexture(string texture, Vector2D position)
        {
            this.position = position;

            this.texture = GeneralTextureManager.GetTexture(texture);
            this.sprite = new SFML.Graphics.Sprite(this.texture);
            this.sprite.Position = this.position.InternalVector;
        }

        public void Update(float deltaTime)
        {
        }

        public void Render(SFML.Graphics.RenderWindow renderWindow)
        {
            if (texture is FramedTexture)
            {
                FramedTexture framed = (FramedTexture)texture;
                framed.Position = position;

                framed.Render(renderWindow);
            }
            else
                renderWindow.Draw(sprite);
        }
    }
}
