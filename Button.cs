using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace rpg
{
    public class Button
    {
        string name;
        Texture2D texture;
        int buttonX, buttonY;

        public int ButtonX
        {
            get
            {
                return buttonX;
            }
        }

        public int ButtonY
        {
            get
            {
                return buttonY;
            }
        }

        public Button(string name, Texture2D texture, int buttonX, int buttonY)
        {
            this.name = name;
            this.texture = texture;
            this.buttonX = buttonX;
            this.buttonY = buttonY;
        }

        /**
         * @return true: If a player enters the button with mouse
         */
        /*
        public bool enterButton()
        {
            if (MouseInput.getMouseX() < buttonX + Texture.Width &&
                    MouseInput.getMouseX() > buttonX &&
                    MouseInput.getMouseY() < buttonY + Texture.Height &&
                    MouseInput.getMouseY() > buttonY)
            {
                return true;
            }
            return false;
        }

        public void Update(GameTime gameTime)
        {
            if (enterButton() && MouseInput.LastMouseState.LeftButton == ButtonState.Released && MouseInput.MouseState.LeftButton == ButtonState.Pressed)
            {
                switch (Name)
                {
                    case "buy_normal_fish": //the name of the button
                        if (Player.Gold >= 10)
                        {
                            ScreenManager.addFriendly("normal_fish", new Vector2(100, 100), 100, -3, 10, 100);
                            Player.Gold -= 10;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        public void Draw()
        {
            Screens.ScreenManager.Sprites.Draw(Texture, new Rectangle((int)ButtonX, (int)ButtonY, Texture.Width, Texture.Height), Color.White);
        }
        */
    }
}
