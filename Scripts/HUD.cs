using Comora;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace PraktikumProjekt.Scripts
{
    internal class HUD
    {
        int maxHealth = 3;
        private int currentHealth = 3; public int CurrentHealth { get{ return currentHealth; } set { currentHealth = value; } }
        private int startEmpty = 0; public int StartEmpty { get { return startEmpty; } set { startEmpty = value; } }
        Vector2 _cameraPosition;
        int renderTargetX;
        int renderTargetY;
        private double timeCounter; public double TimeCounter { get { return timeCounter; } set { timeCounter = value; } }
        public HUD(int X, int Y) {
            renderTargetX = X;
            renderTargetY = Y;
        }

        public void Update(GameTime gameTime, Vector2 position, bool isdead)
        {
            if (!isdead)
            {
            _cameraPosition = position;
            timeCounter += gameTime.ElapsedGameTime.TotalSeconds;

            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            for(int j = 0; j < currentHealth; j++)
            {
                spriteBatch.Draw(Game1._heartTextures[2], new Vector2(50 + (_cameraPosition.X - renderTargetX/2) + 50 * j , _cameraPosition.Y - renderTargetY/2 + 50), Color.White);

                for (int k = maxHealth; k > currentHealth ; k--)
                {
                    spriteBatch.Draw(Game1._heartTextures[0], new Vector2((_cameraPosition.X - renderTargetX / 2) + 50 * k, _cameraPosition.Y - renderTargetY / 2 + 50), Color.White);
                }
            }

            spriteBatch.DrawString(Game1._spriteFont, $"Time : {Math.Floor(timeCounter)}", new Vector2((_cameraPosition.X - renderTargetX / 2) + 1600, _cameraPosition.Y - renderTargetY / 2 + 900), Color.White);
            

        }
    }
}
