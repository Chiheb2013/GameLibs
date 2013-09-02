using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Window;
using SFML.Graphics;

using Mappy;
using Mappy.Maps;
using Mappy.States;
using Mappy.Texture;
using Mappy.Texture.Animation;

namespace Pokemon
{
    class Player : IGameObject, IPhysicObject
    {
        bool collided;
        int prevDir;
        float deltaTime;

        Vector2D speed;
        Vector2D position;

        AnimationGroup animation;

        GameState parent;

        IntRect hitbox;
        IntRect bufferHitbox;

        public IntRect Hitbox { get { return hitbox; } }
        public Vector2D Position { get { return position; } }
        public Vector2D Center { get { return CollisionHelper.GetCenter(bufferHitbox); } }

        public Player(GameState parent)
        {
            this.parent = parent;

            this.collided = false;
            this.speed = new Vector2D(1, 1);
            this.position = new Vector2D(197, 67);

            animation = AnimationGroupManager.GetAnimationGroup("player_animation");
            animation.CurrentAnimation = "player_up";

            hitbox = CollisionHelper.CreateHitbox(position);

            Program.RenderWindow.KeyPressed += RenderWindow_KeyPressed;
            Program.RenderWindow.KeyReleased += RenderWindow_KeyReleased;
        }

        public void Update(float deltaTime)
        {
            this.deltaTime = deltaTime;
        }

        private void RenderWindow_KeyPressed(object sender, SFML.Window.KeyEventArgs e)
        {
            Vector2D copy = new Vector2D(position.X, position.Y);

            if (!collided)
            {
                if (e.Code == Keyboard.Key.Left)
                {
                    prevDir = 0;
                    copy.X -= speed.X;
                }
                if (e.Code == Keyboard.Key.Right)
                {
                    prevDir = 1;
                    copy.X += speed.X;
                }
                if (e.Code == Keyboard.Key.Up)
                {
                    prevDir = 3;
                    copy.Y -= speed.Y;
                }
                if (e.Code == Keyboard.Key.Down)
                {
                    prevDir = 2;
                    copy.Y += speed.Y;
                }
            }
            else
            {
                if (e.Code == Keyboard.Key.Left && prevDir == 1)
                    copy.X -= 2 * TextureManager.TextureSize.X;
                if (e.Code == Keyboard.Key.Right && prevDir == 0)
                    copy.X += 2 * TextureManager.TextureSize.X;
                if (e.Code == Keyboard.Key.Up && prevDir == 2)
                    copy.Y -= 2 * TextureManager.TextureSize.Y;
                if (e.Code == Keyboard.Key.Down && prevDir == 3)
                    copy.Y += 2 * TextureManager.TextureSize.Y;
            }

            if (!Collides(copy))
            {
                this.animation.SetCurrentAnimation(GetAnimation(e));
                position = copy;
                hitbox = bufferHitbox;
                this.animation.Update(deltaTime);

                collided = false;
            }
            else
                collided = true;
        }

        private string GetAnimation(KeyEventArgs e)
        {
            return
                e.Code == Keyboard.Key.Left ? "player_left" :
                e.Code == Keyboard.Key.Right ? "player_right" :
                e.Code == Keyboard.Key.Down ? "player_down" :
                "player_up";
        }

        public bool Collides(Vector2D hitterPosition)
        {
            bufferHitbox = CollisionHelper.CreateHitbox(hitterPosition);
            Vector2D worldCoords = CoordinateSystemConverter.PixelsToWorld(hitterPosition);

            if (parent.World.IsInBounds(worldCoords))       //if in world :
            {                                              //
                bool c = parent.World.Collides(this, 0);  //"advienne que pourra"
                return c;                                //====================
            }                                           //
            return true;                               //else, can't move
        }

        public bool Collides(IPhysicObject hitter)
        {
            throw new NotImplementedException();
        }

        private void RenderWindow_KeyReleased(object sender, KeyEventArgs e)
        {
            animation.Reset();
        }

        public void Render(SFML.Graphics.RenderWindow renderWindow)
        {
            animation.Position = position.InternalVector;
            animation.Render(renderWindow);
        }
    }
}
