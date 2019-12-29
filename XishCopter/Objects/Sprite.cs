using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XishCopter
{
    class Sprite
    {
        Texture2D Texture;

        Vector2 Location = Vector2.Zero;
        Vector2 Velocity = Vector2.Zero;

        int Width = 0;
        int Height = 0;

        List<Rectangle> Frames = new List<Rectangle>();
        int CurrentFrame = 0;
        float FrameTime = 0.07f;
        float CurrentFrameTime = 0.0f;

        int XOffset = 0;
        int YOffset = 0;

        public Color TintColor;



        public Sprite(Texture2D texture, Rectangle position,Rectangle frame,int frameCount,int Xoffset,int Yoffset,Color color)
        {
            Texture = texture;
            Location = new Vector2(position.X, position.Y);
            Width = position.Width;
            Height = position.Height;
            for (int i = 0; i < frameCount; i++)
            {
                Frames.Add(new Rectangle(frame.X + (frame.Width * i), frame.Y, frame.Width, frame.Height));
            }
            XOffset = Xoffset;
            YOffset = Yoffset;
            TintColor = color;
        }

        public Vector2 Speed
        {
            get
            {
                return Velocity;
            }
            set
            {
                Velocity = value;
            }
        }



        public Rectangle Position
        {
            get
            {
                return new Rectangle((int)Location.X,(int)Location.Y,Width,Height);
            }
            set
            {
                Velocity.X = value.X;
                Velocity.Y = value.Y;
                Width = value.Width;
                Height = value.Height;
            }
        }



        public bool IsInScreen(int ScreenWidth,int ScreenHeight)
        {
            return Position.Intersects(new Rectangle(0, 0, ScreenWidth, ScreenHeight));
        }



        public bool CollidesScreenEdge(int ScreenWidth, int ScreenHeight)
        {
            if (IsInScreen(ScreenWidth, ScreenHeight))
            {
                if (CollideRect.Top < 0 || CollideRect.Bottom > ScreenHeight) return true;
                if (CollideRect.Left < 0 || CollideRect.Right > ScreenWidth) return true;
            }
            return false;
        }



        public Rectangle CollideRect
        {
            get
            {
                return new Rectangle(Position.X + XOffset, Position.Y + YOffset, Position.Width - (2 * XOffset), Position.Height - (2 * YOffset));
            }
        }



        public bool Collides(Rectangle OtherGuy)
        {
            return CollideRect.Intersects(OtherGuy);
        }


        public bool Collides(Sprite OtherGuy)
        {
            return CollideRect.Intersects(OtherGuy.CollideRect);
        }


        public void Update(GameTime gameTime)
        {
            float Elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            CurrentFrameTime += Elapsed;
            if (CurrentFrameTime >= FrameTime)
            {
                CurrentFrame = (CurrentFrame + 1) % Frames.Count;
                CurrentFrameTime = 0;
            }
            Location += Velocity * Elapsed;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(Texture, Position, Frames[CurrentFrame], TintColor);
        }


    }


}




