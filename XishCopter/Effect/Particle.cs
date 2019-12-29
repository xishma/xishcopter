using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XishCopter
{
    class Particle
    {

        Texture2D Texture;

        Vector2 Location;
        Vector2 Velocity;

        float Width;
        float Height;

        Color InitialColor;
        Color FinalColor;

        float WholeTime;
        float CurrentTime;

        float TimePerFrame;
        float CurrentFrameTime;

        List<Rectangle> Frames;
        int CurrentFrame;

        public Particle(Texture2D texture, Vector2 location, Vector2 velocity,
                    int initialFrameWidth,
                    int initialFrameHeight,
                    Color initialColor, Color finalColor,
                    Rectangle frame, int frameCount,
                    float timePerFrame, float wholeTime)
        {
            Texture = texture;
            Location = location;
            Velocity = velocity;

            Width = initialFrameWidth;
            Height = initialFrameHeight;

            WholeTime = wholeTime;
            CurrentTime = 0;
            TimePerFrame = timePerFrame;
            CurrentFrameTime = 0;

            InitialColor = initialColor;
            FinalColor = finalColor;

            CurrentFrame = 0;
            Frames = new List<Rectangle>();
            for (int i = 0; i < frameCount; i++)
            {
                Frames.Add(new Rectangle(frame.X + (i * frame.Width), frame.Y, frame.Width, frame.Height));
            }
        }


        public Rectangle Position
        {
            get
            {
                return new Rectangle((int)(Location.X - (Width / 2)), (int)(Location.Y - (Height / 2)), (int)Width, (int)Height);
            }
        }    

        public bool Expired
        {
            get
            {
                return (CurrentTime >= WholeTime);
            }
        }


        public void Update(GameTime gameTime)
        {
            float Elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Location += Velocity * Elapsed;

            CurrentFrameTime += Elapsed;
            if (CurrentFrameTime >= TimePerFrame)
            {
                CurrentFrame = (CurrentFrame + 1) % Frames.Count;
                CurrentFrameTime = 0;
            }

            CurrentTime += Elapsed;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(Texture, Position, Frames[CurrentFrame], Color.Lerp(InitialColor, FinalColor, CurrentTime / WholeTime));
        }

    }
}
