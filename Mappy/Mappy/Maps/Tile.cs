using System;
using System.IO;

using SFML.Window;
using SFML.Graphics;

using Mappy.Texture;

namespace Mappy.Maps
{
    public class Tile : IGameObject, IPhysicObject
    {
        public event EventHandler OnCollision;

        bool isHollow;

        Vector2D position;
        Vector2D screenPosition;

        GeneralTexture texture;
        IntRect hitbox;

        public bool IsHollow { get { return isHollow; } }

        public IntRect Hitbox { get { return hitbox; } }
        public Vector2D Center { get { return CollisionHelper.GetCenter(hitbox); } }

        public Vector2D Position { get { return position; } }
        public Vector2D ScreenPosition { get { return screenPosition; } }

        /// <summary>
        /// Creates a new instance of Tile
        /// </summary>
        /// <param name="mapx"></param>
        /// <param name="mapy"></param>
        /// <param name="isHollow"></param>
        /// <param name="texture"></param>
        /// <remarks>This creates a Tile with a hitbox which (x,y) is the center of the tile texture.
        /// The formula is :
        ///    int w = TextureManager.TextureSize.X
        ///    int h = TextureManager.TextureSize.Y
        ///    int x = position.X - w / 2
        ///    int y = position.Y - h / 2
        ///    hitbox = new Rectangle(x, y, w/2, h/2)</remarks>
        public Tile(float mapx, float mapy, bool isHollow, string texture)
        {
            this.isHollow = isHollow;
            this.position = new Vector2D(mapx, mapy);
            this.screenPosition = CoordinateSystemConverter.WorldToPixels(this.position);
            this.hitbox = CollisionHelper.CreateHitbox(this.screenPosition);

            if (texture != "cmd:none")
                GetSprite(texture);
        }

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
            this.hitbox = CollisionHelper.CreateHitbox(this.screenPosition);

            if (texture != "cmd:none")
                GetSprite(texture);
        }

        public void Update(float deltaTime)
        {
        }

        public bool Collides(Vector2D hitterScreenPosition)
        {
            IntRect objHitBox = new IntRect((int)hitterScreenPosition.X, (int)hitterScreenPosition.Y, hitbox.Width, hitbox.Height);
            return hitbox.Intersects(objHitBox);
        }

        public bool Collides(IPhysicObject hitter)
        {
            return hitbox.Intersects(hitter.Hitbox);
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
            ExceptionHelper<SFML.Graphics.Texture>.AssertTextureExists(texture, "Tile.GetSprite()");
            this.texture = new GeneralTexture(texture, screenPosition);
        }
    }
}
