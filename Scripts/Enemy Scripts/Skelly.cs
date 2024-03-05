using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PraktikumProjekt.Scripts;
using PraktikumProjekt.Scripts.Effects;
using System;
using System.Collections.Generic;

namespace PraktikumProjekt
{
    internal class Skelly : GameObject
    {   
        public static List<Skelly> Skellies = new List<Skelly>(); 
        public Skelly(Vector2 position)
        {
            _width = 18;
            _length = 36;
            _speed = 80;
            _sleepTime = 2D;
            
            _position = position;
            _maxHealth = 2;

            _animUp = new SpriteAnimation(Game1._skellyTextures[0], 4, 8);
            _animRight = new SpriteAnimation(Game1._skellyTextures[2], 4, 8);
            _animDown = new SpriteAnimation(Game1._skellyTextures[4], 4, 8);
            _animLeft = new SpriteAnimation(Game1._skellyTextures[6], 4, 8);
            _idleAnimation = new SpriteAnimation(Game1._skellyTextures[8], 37, 8);
            _currentAnimation = _idleAnimation;
            _currentAnimation.setFrame(1);
        }
        static double _maxTime2 = 2D;
        static double _time2 = 2D;
        public void UpdateSkelly(GameTime gameTime, Vector2 playerPos, bool isDead)
        {

            float _dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            _sleepTime -= gameTime.ElapsedGameTime.TotalSeconds;

            _moveDirection = playerPos - _position;
            _moveDirection.Normalize();

            //Causes Sprite Animation to face the dominant direction it needs go to

            _angleDegrees = GetAngleDegree(_moveDirection);
            if (_angleDegrees > -135 && _angleDegrees <= -45)
            {
                _currentAnimation = _animUp;
            }
            else if (_angleDegrees > -45 && _angleDegrees <= 45)
            {
                _currentAnimation = _animRight;
            }
            else if (_angleDegrees > 45 && _angleDegrees <= 135)
            {
                _currentAnimation = _animDown;
            }
            else
            {
                _currentAnimation = _animLeft;
            }


            //If Player isnt dead, and it isnt on sleep, it will move towards Player
            if (!isDead && _sleepTime < 0)
            {
                _position += _moveDirection * _speed * _dt;
            }
            //Otherwise it will move in place
            else { _currentAnimation = _idleAnimation; }

            _currentAnimation.Update(gameTime);
            //Centering the Sprite to its Position
            _currentAnimation.Position = new Vector2(_position.X - 48, _position.Y - 48); //TO DO Splice Sprites Automatically

            _collider = new Rectangle((int)_position.X, (int)_position.Y, _width, _length);
        }
        public static void UpdateSkellySpawner(GameTime gameTime, Texture2D[] skellyTextures, Texture2D[] portalTextures, bool isdead)
        {
            _time2 -= gameTime.ElapsedGameTime.TotalSeconds;
            if (_time2 < 0)
            {
                Random _random = new Random();
                int side = _random.Next(4);
                if (!isdead)
                {
                    switch (side)
                    {
                        case 0:
                            Vector2 posUp = new Vector2(_random.Next(-1150, 1150), -1150);
                            Skelly.Skellies.Add(new Skelly(posUp));
                            Portal.Portals.Add(new Portal(posUp));                                                                                            //4 map edges where the enemy could spawn
                            break;
                        case 1:
                            Vector2 posRight = new Vector2(1150, _random.Next(-1150, 1150));
                            Skelly.Skellies.Add(new Skelly(posRight));
                            Portal.Portals.Add(new Portal(posRight));
                            break;
                        case 2:
                            Vector2 posDown = new Vector2(_random.Next(-1150, 1150), 1150);
                            Skelly.Skellies.Add(new Skelly(posDown));
                            Portal.Portals.Add(new Portal(posDown));
                            break;
                        case 3:
                            Vector2 posLeft = new Vector2(-1150, _random.Next(-1150, 1150));
                            Skelly.Skellies.Add(new Skelly(posLeft));
                            Portal.Portals.Add(new Portal(posLeft));
                            break;
                    }
                }
                _time2 = _maxTime2;
                if (_maxTime2 > 1)
                {
                    _maxTime2 -= 0.05D;
                }
            }
        }
    }
}

