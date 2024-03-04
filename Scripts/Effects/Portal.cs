using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace PraktikumProjekt.Scripts.Effects
{
    internal class Portal : GameObject
    {
        public static List<Portal> Portals = new List<Portal>(); 
        public Portal(Vector2 position )
        {
            _position = position;

            _currentAnimation = new SpriteAnimation(Game1._portalTextures[8], 15, 10);
            _currentAnimation.IsLooping = false;
        }   

        public void UpdatePortal(GameTime gameTime)
        {
            _deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            //Centers the Sprite to its position
            _currentAnimation.Position = new Vector2(_position.X - 35, _position.Y - 40);
            _currentAnimation.Update(gameTime);
        }
    }
}
