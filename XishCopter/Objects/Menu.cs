using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace XishCopter
{
    class Menu
    {

        public List<Button> Buttons = new List<Button>();

        public Menu(List<Button> buttons)
        {
            Buttons = buttons;
        }

        public string Update(GameTime gameTime)
        {
            string Action = null;
            foreach (Button button in Buttons)
            {
                string Output = button.Update(Mouse.GetState().LeftButton == ButtonState.Pressed, Mouse.GetState().X, Mouse.GetState().Y, gameTime);
                if (Output != null) Action += Output + ",";
            }
            return Action;
        }

        public void Draw(SpriteBatch spriteBatch,GameTime gameTime)
        {
            foreach (Button button in Buttons)
            {
                button.Draw(spriteBatch, gameTime);
            }
        }

    }
}
