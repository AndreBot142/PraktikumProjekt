using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Net.Mime;

namespace PraktikumProjekt.Scripts
{
    internal class GameObject //for inheritence
    {
        //Initialization
        //Transformation
        protected int _width;           public int Width { get { return _width; } set { _width = value; } }
        protected int _length;          public int Length { get { return _length; } set { _length = value; } }

        protected Rectangle _collider;  public Rectangle Collider { get { return _collider; } set { _collider = value; } }
        protected int _radius;          public int Radius { get { return _radius; } set { _radius = value; } }
        protected Vector2 _position;    public Vector2 Position { get { return _position; } set { _position = value; } }

        protected float _angleDegrees;  public float AngleDegrees { get { return _angleDegrees; } set { _angleDegrees = value; } }

        protected int _maxHealth;       public int MaxHealth { get { return _maxHealth; } set { _maxHealth = value; } }

        //Physics
        protected Direction _direction;

        protected int _speed;

        protected Vector2 _moveDirection;

        //Sprites and Animation

        protected Texture2D[] _textures = new Texture2D[9];

        protected Texture2D[] _healthTextures = Game1._healthBarTextures;

        protected SpriteAnimation _currentAnimation, _idleAnimation, _animUp, _animUpRight, _animRight, _animDownRight, _animDown, _animDownLeft, _animLeft, _animUpLeft;

        protected SpriteAnimation[] _animations = new SpriteAnimation[9];
        public SpriteAnimation CurrentAnimation { get { return _currentAnimation; } set { _currentAnimation = value; } }


        protected int _currentIndex;

        //Status
        protected bool _isCollided;     public bool IsCollided { get { return _isCollided; } set { _isCollided = value; } }
        protected double _sleepTime;    public double SleepTime { get { return _sleepTime; } set { _sleepTime = value; } }
        protected bool _isDead;         public bool IsDead { get { return _isDead; } set { _isDead = value; } }

        protected float _deltaTime;

        protected bool _isMoving = false;

        protected bool _isShooting = false;

        //Methods
        protected float GetAngleDegree(Vector2 moveDirection)
        {
            float _angle = (float)Math.Atan2(moveDirection.Y, moveDirection.X);
            float _angleDegrees = MathHelper.ToDegrees(_angle);
            return _angleDegrees;
        }
    }
}
