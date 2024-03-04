using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace PraktikumProjekt.Scripts.Projectiles
{
    internal class Projectile : GameObject
    {
        public static List<Projectile> Projectiles = new List<Projectile>();
        int _sdirection;
        public Projectile(Vector2 position, int Direction)
        {
            _radius = 18;
            _speed = 1000;
            _position = position;
            _sdirection = Direction;
            if(_sdirection == -1) { _sdirection = 7; }
            if(_sdirection == 8) { _sdirection = 0; }
        }

        public void UpdateProjectile(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            switch (_sdirection) 
            {
                case 0:
                    _position.Y -= _speed * dt;
                    break;
                case 1:
                    _position.Y -= _speed * dt;
                    _position.X += _speed * dt;
                    break;
                case 2:
                    _position.X += _speed * dt;
                    break;
                case 3:
                    _position.Y += _speed * dt;
                    _position.X += _speed * dt;
                    break;
                case 4:
                    _position.Y += _speed * dt;
                    break;
                case 5:
                    _position.Y += _speed * dt;
                    _position.X -= _speed * dt;
                    break;
                case 6:
                    _position.X -= _speed * dt;
                    break;
                case 7:
                    _position.Y -= _speed * dt;
                    _position.X -= _speed * dt;
                    break; 
            }
            
        }
    }
}
