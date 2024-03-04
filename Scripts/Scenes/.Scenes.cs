using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;



namespace PraktikumProjekt.Scripts.Scenes
{
     class Scenes
    {
        private  Vector2 _position = new Vector2(-1248, -1248);
        private  Texture2D _currentBackground; public Texture2D CurrentBackground { get { return _currentBackground; } set { CurrentBackground = value; } }
        private  Rectangle _currentBackgroundCollider;
        private  int _colliderWidth = 0;
        private  int _colliderHeight = 0;
        public Scenes()
        {
            _currentbackgroundCollider = new Rectangle((int)_position.X, (int)_position.Y, _colliderWidth, _colliderHeight);
        }

        public void Update(GameTime gameTime)
        {

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_backgroundTexture, new Vector2(-1248, -1248), Color.White); //background is drawn so that the center is 0,0
        }
    }
}
