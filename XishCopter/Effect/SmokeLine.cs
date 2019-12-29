using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XishCopter
{
    class SmokeLine
    {

        public List<Smoke> Smokes = new List<Smoke>();

        Texture2D Texture;

        Rectangle Frame;

        float Width;
        float ChangingWidth;
        float Height;
        float ChangingHeight;

        public Color InitialColor;
        public Color FinalColor;

        float WholeTime;

        float TimePerFrame;

        int FrameCount;
        int FramePerRow;

        float TimePerSmoke;
        public float RemainingTimeForSmoke;
        public bool Expired;

        public SmokeLine(Texture2D texture,float width,float changingWidth,
                         float height,float changingHeigh,Color initialColor,Color finalColor,
                         float wholeTime, float timePerFrame,int frameCount,int framePerRow,
                         Rectangle frame, float timePerSmoke)
        {
            Texture = texture;
            Width = width;
            ChangingWidth = changingWidth;
            Height = height;
            ChangingHeight = changingWidth;
            InitialColor = initialColor;
            FinalColor = finalColor;
            WholeTime = wholeTime;
            TimePerFrame = timePerFrame;
            FrameCount = frameCount;
            FramePerRow = framePerRow;
            Frame = frame;
            TimePerSmoke = timePerSmoke;
            RemainingTimeForSmoke = 0;
            Expired = false;
        }

        public void AddSmoke(Vector2 Location, Vector2 Velocity)
        {
            Smokes.Add(new Smoke(Texture, Location, Velocity, Width, ChangingWidth, Height, ChangingHeight,
                                 InitialColor, FinalColor, Frame, FrameCount, FramePerRow, TimePerFrame, WholeTime));
        }


        public void Update(Vector2 Location, Vector2 Velocity, GameTime gameTime)
        {
            float Elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            RemainingTimeForSmoke -= Elapsed;
            if (RemainingTimeForSmoke <= 0)
            {
                RemainingTimeForSmoke = TimePerSmoke;
                this.AddSmoke(Location, Velocity);
            }
            for (int i = Smokes.Count - 1; i >= 0; i--)
            {
                Smokes[i].Update(gameTime);
                if (Smokes[i].Expired)
                    Smokes.RemoveAt(i);
            }
            if (Smokes.Count == 0 && RemainingTimeForSmoke > TimePerSmoke) Expired = true;
        }

        public void Draw(SpriteBatch spriteBatch,GameTime gameTime)
        {
            for (int i = Smokes.Count-1; i >= 0; i--)
            {
                Smokes[i].Draw(spriteBatch,gameTime);
            }
        }
    }
}
