using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XishCopter
{
    class Block
    {

        public Sprite BlockSprite;
        bool IsMovable;
        Vector2 Acceleration;
        float MaxSpeed;

        public bool IsExpired;

        public Block(Sprite blockSprite,Vector2 acceleration,float maxSpeed)
        {
            BlockSprite = blockSprite;
            IsMovable = true;
            Acceleration = acceleration;
            MaxSpeed = maxSpeed;
            IsExpired = false;
        }

        public Block(Sprite blockSprite)
        {
            BlockSprite = blockSprite;
            IsMovable = false;
            IsExpired = false;
        }

        public bool Expired
        {
            get
            {
                return IsExpired;
            }
            set
            {
                IsExpired = value;
            }
        }

        public void Move()
        {
            IsMovable = true;
        }

        public void Stop()
        {
            IsMovable = false;
        }

        public void Update(GameTime gameTime)
        {
            if (IsMovable)
            {
                BlockSprite.Speed += Acceleration;
                if (BlockSprite.Speed.Y >= MaxSpeed || BlockSprite.Speed.Y <= -MaxSpeed)
                    Acceleration *= -1;
            }
            BlockSprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            BlockSprite.Draw(spriteBatch, gameTime);
        }
    }
}
