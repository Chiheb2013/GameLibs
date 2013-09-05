using System;
using System.IO;

using SFML.Window;
using SFML.Graphics;

using Mappy.Textures;
using Mappy.Collisions;

namespace Mappy.Worlds
{
    public class Tile : IPhysicObject
    {
        public event EventHandler OnCollision;

        bool isHollow;

        Vector2D position;
        Vector2D screenPosition;
        GeneralTexture texture;

        RectanglePhysicsObject hitbox;

        public bool IsHollow { get { return isHollow; } }

        public Vector2D Position { get { return position; } }
        public Vector2D ScreenPosition { get { return screenPosition; } }

        public RectanglePhysicsObject Hitbox { get { return hitbox; } }

        /// <summary>
        /// Creates a new instance of Tile
        /// </summary>
        /// <param name="isHollow"></param>
        /// <param name="texture"></param>
        /// <param name="position"></param>
        /// <remarks>This creates a Tile with a hitbox which (x,y) is the center of the tile texture.
        /// The formula is :
        ///    int w = TextureManager.TextureSize.X
        ///    int h = TextureManager.TextureSize.Y
        ///    int x = position.X - w / 2
        ///    int y = position.Y - h / 2
        ///    hitbox = new Rectangle(x, y, w/2, h/2)</remarks>
        public Tile(bool isHollow, string texture, Vector2D position)
        {
            this.isHollow = isHollow;
            this.position = position;
            this.screenPosition = CoordinateSystemConverter.WorldToPixels(this.position);
            this.hitbox = new RectanglePhysicsObject(position);

            if (texture != "cmd:none")
                GetSprite(texture);
        }

        public void Update(float deltaTime)
        {
            hitbox.Update(deltaTime);
        }

        public bool CollidesWith(IPhysicObject other)
        {
            return hitbox.CollidesWith(other);
        }

        public void Render(RenderWindow renderWindow)
        {
            texture.Render(renderWindow);
        }

        protected virtual void RaiseCollisionEvent(object sender, EventArgs e)
        {
            if (OnCollision != null)
                OnCollision(sender, e);
        }

        private void GetSprite(string texture)
        {
            ExceptionHelper.AssertTextureExists(texture, "Tile.GetSprite()");
            this.texture = new GeneralTexture(texture, screenPosition);
        }
    }
}
