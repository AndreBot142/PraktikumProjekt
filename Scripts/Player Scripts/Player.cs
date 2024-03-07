using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PraktikumProjekt.Scripts;
using PraktikumProjekt.Scripts.Projectiles;

namespace PraktikumProjekt
{
    internal class Player : GameObject
    {
        public Player() { } 
        public Player(Vector2 position)
        {
            _width = 18;
            _length = 36;
            _speed = 300;
            _sleepTime = 2D;

            _isMoving = false;
            _isShooting = false;
            _isDead = false;

            _position = position;

            
            _animUp = new SpriteAnimation(Game1._playerTextures[0], 4, 8);
            _animRight = new SpriteAnimation(Game1._playerTextures[2], 4, 8);
            _animDown = new SpriteAnimation(Game1._playerTextures[4], 4, 8);
            _animLeft = new SpriteAnimation(Game1._playerTextures[6], 4, 8);
            _idleAnimation = new SpriteAnimation(Game1._playerTextures[8], 12, 4);
            _currentAnimation = _idleAnimation;
        }
        
        private double _multishotTimer = 0;             public double MultishotTimer { get { return _multishotTimer; } set { _multishotTimer = value; } }
        private double _shootCooldown = 0.5D;
        private double _time = 0;
        private Direction _shootDirection;

        public void Update(GameTime gameTime, bool isDead)
        {
            KeyboardState kState = Keyboard.GetState();
            
            float _deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds; 
            _isMoving = false;
            _isShooting = false;

            if (kState.IsKeyDown(Keys.W))
            {
                _direction = Direction.Up;
                _isMoving = true;
            }
            if (kState.IsKeyDown(Keys.D))
            {
                _direction = Direction.Right;
                _isMoving = true;
            }
            if (kState.IsKeyDown(Keys.S)) 
            {                                                            
                _direction = Direction.Down;
                _isMoving = true;
            }
            if (kState.IsKeyDown(Keys.A))
            {
                _direction = Direction.Left;
                _isMoving = true;
            }
            //Move Logic
            if (_isMoving)
            {
                switch ((int)_direction)
                {
                    case 0:
                        if (_position.Y > -1141 && !isDead)
                        {
                            _position.Y -= _speed * _deltaTime;
                            _currentAnimation = _animUp;
                        }
                        break;
                    case 2:
                        if (_position.X < 1210 && !isDead)
                        {
                            _position.X += _speed * _deltaTime;
                            _currentAnimation = _animRight;
                        }
                        break;
                    case 4:
                        if (_position.Y < 1170 && !isDead)
                        {
                            _position.Y += _speed * _deltaTime;
                            _currentAnimation = _animDown;
                        }
                        break;
                    case 6:
                        if (_position.X > -1210 && !isDead)
                        {
                            _position.X -= _speed * _deltaTime;
                            _currentAnimation = _animLeft;
                        }
                        break;

                }
            }
            else { _currentAnimation = _idleAnimation;  }

            _currentAnimation.Position = new Vector2(_position.X - 48, Position.Y - 48);
            _currentAnimation.Update(gameTime);
            

            //Shoot Logic
            if (kState.IsKeyDown(Keys.Up) && kState.IsKeyDown(Keys.Right))
            {
                _shootDirection = Direction.UpRight;
                _isShooting = true;
            }
            else if (kState.IsKeyDown(Keys.Up) && kState.IsKeyDown(Keys.Left))
            {
                _shootDirection = Direction.UpLeft;
                _isShooting = true;
            }
            else if (kState.IsKeyDown(Keys.Down) && kState.IsKeyDown(Keys.Right))
            {
                _shootDirection = Direction.DownRight;
                _isShooting = true;
            }
            else if (kState.IsKeyDown(Keys.Down) && kState.IsKeyDown(Keys.Left))
            {
                _shootDirection = Direction.DownLeft;
                _isShooting = true;
            }
            else if (kState.IsKeyDown(Keys.Up))
            {
                _shootDirection = Direction.Up;
                _isShooting = true;
            }
            else if (kState.IsKeyDown(Keys.Right))
            {
                _shootDirection = Direction.Right;
                _isShooting = true;
            }
            else if (kState.IsKeyDown(Keys.Down))
            {
                _shootDirection = Direction.Down;
                _isShooting = true;
            }
            else if (kState.IsKeyDown(Keys.Left))
            {
                _shootDirection = Direction.Left;
                _isShooting = true;
            }


            if (_isShooting && _time < 0 && !isDead)                                                                              
            {

                Projectile.Projectiles.Add(new Projectile(_position, (int)_shootDirection));
                if(_multishotTimer > 0)
                {
                    Projectile.Projectiles.Add(new Projectile(_position, (int)_shootDirection - 1));
                    Projectile.Projectiles.Add(new Projectile(_position, (int)_shootDirection + 1));
                }
                _isShooting = true;
                _time = _shootCooldown;

            }
            if(_multishotTimer > 0) 
            {
            _multishotTimer -= gameTime.ElapsedGameTime.TotalSeconds;
            }
            _time -= gameTime.ElapsedGameTime.TotalSeconds;

            _collider = new Rectangle((int)_position.X, (int)_position.Y, _width, _length);
        }
    }
}
