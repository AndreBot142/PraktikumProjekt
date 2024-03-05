using Comora;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;



namespace PraktikumProjekt.Scripts.Scenes
{
     class Scenes
    {
        private  Vector2 _position = new Vector2(-1248, -1248);

        
        private  Texture2D _currentBackground = Game1._caveBackground; public Texture2D CurrentBackground { get { return _currentBackground; } set { CurrentBackground = value; } }

        public void Update(GameTime gameTime)
        {

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_currentBackground, _position, Color.White); //background is drawn so that the center is 0,0
        }
    }
}
