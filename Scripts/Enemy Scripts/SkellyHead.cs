using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PraktikumProjekt.Scripts;
using System;
using System.Collections.Generic;

namespace PraktikumProjekt
{
    internal class SkellyHead : GameObject
    {
        public static List<SkellyHead> SkellyHeads = new List<SkellyHead>();
        
        public SkellyHead(Vector2 position)
        {
            _radius = 10;
            _speed = 150;
            _position = position;
            _currentAnimation = new SpriteAnimation(Game1._skellyHeadTextures[8], 4, 4);


        }
        static double _maxTime1 = 2D;
        static double _time1 = 2D;

        public void UpdateSkellyHead(GameTime gameTime, Vector2 playerPos, bool isDead)
        {
            _deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            //If Player isnt dead, Moves Towards the Player          
            _moveDirection = playerPos - _position;
            _moveDirection.Normalize();
            if (!isDead)
            {
                _position += _moveDirection * _speed * _deltaTime;
            }

            //Centers the Sprite to its Position
            _currentAnimation.Position = new Vector2(_position.X -48, _position.Y - 66);
            _currentAnimation.Update(gameTime);

        }

        public static void UpdateSkellyHeadSpawner(GameTime gameTime, Texture2D[] textures, bool isdead)
        {
            _time1 -= gameTime.ElapsedGameTime.TotalSeconds;
            if (_time1 < 0)
            {
                Random _random = new Random();
                int side = _random.Next(1, 5);
                if (!isdead)
                {
                    switch (side)
                    {
                        case 0:
                            SkellyHead.SkellyHeads.Add(new SkellyHead(new Vector2(_random.Next(-2400, 2400), -2400)));                                                                          //4 map edges where the enemy could spawn
                            break;
                        case 1:
                            SkellyHead.SkellyHeads.Add(new SkellyHead(new Vector2(_random.Next(-2400, 2400), 2400)));
                            break;
                        case 2:
                            SkellyHead.SkellyHeads.Add(new SkellyHead(new Vector2(-2400, _random.Next(-2400, 2400))));
                            break;
                        case 3:
                            SkellyHead.SkellyHeads.Add(new SkellyHead(new Vector2(2400, _random.Next(-2400, 2400))));
                            break;
                    }
                }
                _time1 = _maxTime1;
                if (_maxTime1 > 1)
                {
                    _maxTime1 -= 0.05D;
                }


            }
        }
    }
}
