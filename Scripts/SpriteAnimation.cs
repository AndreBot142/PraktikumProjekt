using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace PraktikumProjekt.Scripts
{
    public class SpriteManager
    {

        protected Texture2D Texture;
        private Color Color = Color.White;
        private Vector2 Origin;
        private float Rotation = 0f;
        private float Scale = 1f;
        private SpriteEffects SpriteEffect;
        protected Rectangle[] Rectangles;
        private Vector2 position = Vector2.Zero; public Vector2 Position { get { return position; } set { position = value; } }
        protected int frameIndex = 0; public int Frameindex { get { return frameIndex; } set { frameIndex = value; } }

        public SpriteManager(Texture2D Texture, int frames)
        {
            this.Texture = Texture;

            int width = Texture.Width / frames;
            Rectangles = new Rectangle[frames];
            for (int i = 0; i < frames; i++)
                Rectangles[i] = new Rectangle(i * width, 0, width, Texture.Height);

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Rectangles[frameIndex], Color, Rotation, Origin, Scale, SpriteEffect, 0f);
        }
    }

    public class SpriteAnimation : SpriteManager
    {
        private float timeElapsed;
        public bool IsLooping = true;
        private float timeToUpdate;
        public int FramesPerSecond { set { timeToUpdate = 1f / value; } }

        public SpriteAnimation(Texture2D Texture, int frames, int fps) : base(Texture, frames)
        {
            FramesPerSecond = fps;
        }

        public void Update(GameTime gameTime)
        {
            timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timeElapsed > timeToUpdate)
            {
                timeElapsed -= timeToUpdate;

                if (frameIndex < Rectangles.Length - 1)
                    frameIndex++;

                else if (IsLooping)
                    frameIndex = 0;
            }

        }
        public void setFrame(int frame)
        {
            frameIndex = frame;
        }
    }
}