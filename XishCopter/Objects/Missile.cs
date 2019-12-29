using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XishCopter
{
    class Missile
    {

        public Sprite MissileSprite;

        bool IsSpecial;
        Vector2 ExplosionCenter;
        float ExplosionDist;

        public bool IsExpired;
        public bool Removable;
        SmokeLine smokeLine;

        public Missile(Sprite sprite, Vector2 speed, SmokeLine smokeline)
        {
            MissileSprite = sprite;
            IsSpecial = false;
            IsExpired = false;
            smokeLine = smokeline;
            Removable = false;
        }

        public Missile(Sprite sprite,Vector2 explosionCenter,float explosionDist,SmokeLine smokeline)
        {
            MissileSprite = sprite;
            IsSpecial = true;
            ExplosionCenter = explosionCenter;
            ExplosionDist = explosionDist;
            IsExpired = false;
            smokeLine = smokeline;
            Removable = false;
        }

        public bool InDistance(Rectangle Target)
        {
            if (ExplosionCenter.Y <= Target.Top)
            {
                if (ExplosionCenter.X <= Target.Left)
                {
                    if (Vector2.Distance(ExplosionCenter, new Vector2(Target.X, Target.Y)) <= ExplosionDist) 
                        return true;
                }
                if (ExplosionCenter.X >= Target.Right)
                {
                    if (Vector2.Distance(ExplosionCenter, new Vector2(Target.X + Target.Width, Target.Y)) <= ExplosionDist)
                        return true;
                }
                else
                {
                    if (ExplosionCenter.Y + ExplosionDist >= Target.Top)
                        return true;
                }
            }

            else if (ExplosionCenter.Y >= Target.Bottom)
            {
                if (ExplosionCenter.X <= Target.Left)
                {
                    if (Vector2.Distance(ExplosionCenter, new Vector2(Target.X, Target.Y + Target.Height)) <= ExplosionDist)
                        return true;
                }
                if (ExplosionCenter.X >= Target.Right)
                {
                    if (Vector2.Distance(ExplosionCenter, new Vector2(Target.X + Target.Width, Target.Y + Target.Height)) <= ExplosionDist)
                        return true;
                }
                else
                {
                    if (ExplosionCenter.Y - ExplosionDist <= Target.Bottom)
                        return true;
                }
            }

            else
            {
                if (ExplosionCenter.X <= Target.Left)
                {
                    if (ExplosionCenter.X + ExplosionDist >= Target.Left)
                        return true;
                }
                if (ExplosionCenter.X >= Target.Right)
                {
                    if (ExplosionCenter.X - ExplosionDist <= Target.Right)
                        return true;
                }
                else
                {
                    return true;
                }
            }

            return false;
        }


        public void Update(GameTime gameTime, Vector2 MapVelocity)
        {
            if (!IsExpired)
            {
                MissileSprite.Update(gameTime);
            }
            else
            {
                smokeLine.RemainingTimeForSmoke = 10;
            }
            smokeLine.Update(new Vector2(MissileSprite.Position.X, MissileSprite.Position.Y + (MissileSprite.Position.Y / 2)), MapVelocity, gameTime);
            if (smokeLine.Expired) Removable = true;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (!IsExpired)
            {
                MissileSprite.Draw(spriteBatch, gameTime);
            }
            smokeLine.Draw(spriteBatch, gameTime);
        }

    }
}
