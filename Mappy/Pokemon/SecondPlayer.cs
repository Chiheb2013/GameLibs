using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Window;
using SFML.Graphics;

using Mappy;
using Mappy.Worlds;
using Mappy.Entities;
using Mappy.Collisions;

namespace Pokemon
{
    class SecondPlayer : IGameObject
    {
        Character character;

        public SecondPlayer(World world)
        {
            DirectionAnimationCouple dac = new DirectionAnimationCouple();
            dac.Couples.Add(Direction.Up, "player_up");
            dac.Couples.Add(Direction.Down, "player_down");
            dac.Couples.Add(Direction.Left, "player_left");
            dac.Couples.Add(Direction.Right, "player_right");

            character = new Character(new Vector2D(0, 0), new Vector2D(3, 3), dac, world, "player_animation", 0);

            Program.RenderWindow.KeyPressed += RenderWindow_KeyPressed;
            Program.RenderWindow.KeyReleased += RenderWindow_KeyReleased;
        }

        private void RenderWindow_KeyPressed(object sender, SFML.Window.KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Up)
                character.Move(Direction.Up);
            if (e.Code == Keyboard.Key.Down)
                character.Move(Direction.Down);
            if (e.Code == Keyboard.Key.Left)
                character.Move(Direction.Left);
            if (e.Code == Keyboard.Key.Right)
                character.Move(Direction.Right);
        }

        private void RenderWindow_KeyReleased(object sender, SFML.Window.KeyEventArgs e)
        {
            character.ResetAnimation();
        }

        public void Update(float deltaTime)
        {
            character.Update(deltaTime);
        }

        public void Render(SFML.Graphics.RenderWindow renderWindow)
        {
            character.Render(renderWindow);
        }
    }
}
