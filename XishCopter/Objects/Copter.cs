using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XishCopter
{
    class Copter
    {

        public Sprite sprite;

        float Acceleration = 0;
        float MaxAcceleration = 0;

        public int Score;
        public int Velocity;

        bool Engine;
        bool IsExpired;
        public bool Removable;

        public float ParticlesToken;
        float ParticleThreshold;
        float TimerPerParticle;
        bool IsInsane;

        SmokeLine smokeLine;

        public Copter(Texture2D texture, Rectangle position, Rectangle frame,
                      int frameCount, int Xoffset, int Yoffset,
                      Color color,float speedchangingvalue,float maxspdchngval,
                      float particlethreshold,float timePerParticle,
                        SmokeLine smokeline)
        {
            sprite = new Sprite(texture,position,frame,frameCount,Xoffset,Yoffset,color);

            Engine = false;
            Acceleration = speedchangingvalue;
            MaxAcceleration = maxspdchngval;

            Score = 0;
            Velocity = 0;
            IsExpired = false;

            IsInsane = false;
            ParticlesToken = 0;
            ParticleThreshold = particlethreshold;
            TimerPerParticle = timePerParticle;

            smokeLine = smokeline;
            Removable = false;
        }

        public void EngineOn()
        {
            Engine = true;
        }

        public void EngineOff()
        {
            Engine = false;
        }

        public int ScoreVal
        {
            get
            {
                return Score;
            }
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


        public void Update(GameTime gameTime, Vector2 MapVelocity)
        {
            sprite.Update(gameTime);

            if (!Expired)
            {
                if (!IsInsane)
                {
                    if (Engine)
                    {
                        Acceleration = MathHelper.Max(Acceleration - (MaxAcceleration / 3), -MaxAcceleration);
                    }
                    else
                    {
                        Acceleration = MathHelper.Min(Acceleration + (MaxAcceleration / 3), MaxAcceleration);
                    }
                    sprite.Speed += new Vector2(0, Acceleration);

                    if (ParticlesToken > ParticleThreshold)
                    {
                        IsInsane = true;
                    }
                }
                else
                {
                    sprite.Speed = new Vector2(0, sprite.Speed.Y * 0.9f);
                    sprite.Speed += new Vector2(0, new Random().Next(-3, 2));

                    ParticlesToken -= TimerPerParticle * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (ParticlesToken <= 0)
                    {
                        ParticlesToken = 0;
                        IsInsane = false;
                    }
                }
            }
            else
            {
                smokeLine.RemainingTimeForSmoke = 10;
            }

            smokeLine.Update(new Vector2(sprite.Position.X, sprite.Position.Y + (sprite.Position.Y / 2)), MapVelocity, gameTime);
            if (smokeLine.Expired) Removable = true;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (!Expired)
            {
                sprite.Draw(spriteBatch, gameTime);
            }
            smokeLine.Draw(spriteBatch, gameTime);
        }
    }
}
