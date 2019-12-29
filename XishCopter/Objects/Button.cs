using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XishCopter
{
    class Button 
    {
        Texture2D Texture;
        List<Rectangle> Frames=new List<Rectangle>();
        int CurrentFrame;

        Rectangle Position;

        Color[] TintColor;
        

        bool LastPressingState;

        string HoldingFunction;
        string PressingFunction;

        public Button(Texture2D texture, int frameCount, Rectangle frame,
                       Rectangle position,
                       Color normalColor, Color mouseonColor, Color pressedColor,
                       string holdingFunc, string pressingFunc)
        {
            Texture = texture;
            for (int i = 0; i < frameCount; i++)
            {
                Frames.Add(new Rectangle(frame.X + (frame.Width * i), frame.Y, frame.Width, frame.Height));
            }

            CurrentFrame = 1;
            LastPressingState = false;
            Position = position;

            TintColor = new Color[3];
            TintColor[0] = normalColor;
            TintColor[1] = mouseonColor;
            TintColor[2] = pressedColor;

            HoldingFunction = holdingFunc;
            PressingFunction = pressingFunc;
        }


        public string Update(bool PressingState,int TapX,int TapY,GameTime gameTime)
        {
            if (Position.Contains(TapX, TapY))
            {
                CurrentFrame = 1;
                if (PressingState == false && LastPressingState == false)
                {
                    LastPressingState = PressingState;
                    return null;
                }
                else if (PressingState == false && LastPressingState == true)
                {
                    LastPressingState = PressingState;
                    CurrentFrame = 1;
                    return PressingFunction;
                }
                else if (PressingState == true && LastPressingState == false)
                {
                    LastPressingState = PressingState;
                    CurrentFrame = 2;
                    return HoldingFunction;
                }
                else if (PressingState == true && LastPressingState == true)
                {
                    LastPressingState = PressingState;
                    CurrentFrame = 2;
                    return HoldingFunction;
                }
            }
            else
            {
                LastPressingState = false;
                CurrentFrame = 0;
                return null;
            }

            return null;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(Texture, Position, Frames[CurrentFrame], TintColor[CurrentFrame]);
        }

    }
}
