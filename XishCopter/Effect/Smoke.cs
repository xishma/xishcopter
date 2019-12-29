using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XishCopter
{
    class Smoke
    {

        Texture2D Texture;

        Vector2 Location;
        Vector2 Velocity;

        float Width;
        float ChangingWidth;
        float Height;
        float ChangingHeight;

        Color InitialColor;
        Color FinalColor;

        float WholeTime;
        float CurrentTime;

        float TimePerFrame;
        float CurrentFrameTime;

        List<Rectangle> Frames;
        int CurrentFrame;

        public Smoke(Texture2D texture, Vector2 location, Vector2 velocity,
                    float initialFrameWidth, float finalFrameWidth,
                    float initialFrameHeight, float finalFrameHeight,
                    Color initialColor, Color finalColor,
                    Rectangle frame, int frameCount,int framesInRow,
                    float timePerFrame, float wholeTime)
        {
            Texture = texture;
            Location = location;
            Velocity = velocity;
            
            Width = initialFrameWidth;
            Height = initialFrameHeight;
            ChangingWidth = (finalFrameWidth - initialFrameWidth) / wholeTime;
            ChangingHeight = (finalFrameHeight - initialFrameHeight) / wholeTime;

            WholeTime = wholeTime;
            CurrentTime = 0;
            TimePerFrame = timePerFrame;
            CurrentFrameTime = 0;

            InitialColor = initialColor;
            FinalColor = finalColor;

            CurrentFrame = 0;
            Frames = new List<Rectangle>();
            int j = 0;
            int k = 0;
            for (int i = 0; i < frameCount; i++)
            {
                Frames.Add(new Rectangle(frame.X + (k * frame.Width), frame.Y + (j * frame.Height), frame.Width, frame.Height));
                k++;
                if (k == framesInRow)
                {
                    k = 0;
                    j++;
                }
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

            Width += ChangingWidth * Elapsed;
            Height += ChangingHeight * Elapsed;

            Location += Velocity * Elapsed;

            CurrentFrameTime += Elapsed;
            if (CurrentFrameTime >= TimePerFrame)
            {
                CurrentFrame = (CurrentFrame + 1) % Frames.Count;
                CurrentFrameTime = 0;
            }

            CurrentTime += Elapsed;
        }

        public void Draw(SpriteBatch spriteBatch,GameTime gameTime)
        {
            spriteBatch.Draw(Texture, Position, Frames[CurrentFrame], Color.Lerp(InitialColor, FinalColor, CurrentTime / WholeTime));
        }

    }
}
