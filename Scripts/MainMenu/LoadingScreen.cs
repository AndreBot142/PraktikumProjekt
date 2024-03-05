using Comora;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;


namespace PraktikumProjekt.Scripts.MainMenu
{
    internal class LoadingScreen
    {
        MouseState mouseState;
        int mouseX;
        int mouseY;
        Rectangle _startButton = new Rectangle(800, 100, 200, 100);
        public LoadingScreen() { }

        public void Update(GameTime gameTime)
        {

            mouseState = Mouse.GetState();
            mouseX = mouseState.X;
            mouseY = mouseState.Y;

            if (_startButton.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed)
            {
                Game1._gameState = "Running";
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {


            spriteBatch.Draw(Game1._loadingScreenBG, new Vector2(0, 0), Color.White);
            spriteBatch.DrawString(Game1._spriteFont, "START", new Vector2(800, 100), Color.White);
            spriteBatch.Draw(Game1.cursor, new Vector2(mouseX, mouseY), Color.White);
        }
    }
}
