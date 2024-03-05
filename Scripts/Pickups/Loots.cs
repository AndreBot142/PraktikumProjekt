using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace PraktikumProjekt.Scripts.Loots
{
    internal class Loot : GameObject
    {
        public static List<Loot> Loots = new List<Loot>(); 
        public Loot(Vector2 position)
        {
            _radius = 32;
            _speed = 1000;
            _position = position;

            _currentAnimation = new SpriteAnimation(Game1._lootTextures[8], 8, 4);
        }

        public void UpdateLoot(GameTime gameTime)
        {
            _deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _currentAnimation.Position = new Vector2(_position.X, _position.Y + 25);
            _currentAnimation.Update(gameTime);
        }
    }
}
