using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XishCopter
{
    class Explosion
    {
        public List<Particle> Particles;
        List<Smoke> Smokes;
        bool IsCollidable;

        public Explosion(Vector2 Location, Vector2 Velocity, int SmokeCount, int ParticleCount, int ExploWidth, int ExploHeight, float WholeTime,
                         Texture2D smokeTexture, Rectangle smokeFrame, int SmokeFrameCount, int SmokeFrameInRow, float SmokeTimePerFrame,
                         Texture2D ParticleTexture, Rectangle ParticleFrame, int ParticleFrameCount, float ParticleTimePerFrame,
                            Color SmokeInitialColor, Color SmokeFinalColor,
                            Color ParticleInitialColor, Color ParticleFinalColor,
                            bool isCollidable)
        {
            IsCollidable = isCollidable;
            float Speed = ExploHeight / WholeTime;
            Random random = new Random();

            Smokes = new List<Smoke>();
            for (int i = 0; i < SmokeCount - 1; i++)
            {
                Vector2 Telorance = new Vector2(random.Next((int)-Speed, (int)Speed), random.Next((int)-Speed, (int)Speed));
                Smokes.Add(new Smoke(smokeTexture, Location - new Vector2(ExploWidth / 8, ExploHeight / 4), Velocity + Telorance, ExploWidth, 2 * ExploWidth, ExploHeight, 2 * ExploHeight, SmokeInitialColor,
                                SmokeFinalColor, smokeFrame, SmokeFrameCount, SmokeFrameInRow, SmokeTimePerFrame, WholeTime));
            }
            Smokes.Add(new Smoke(smokeTexture, Location, Velocity, ExploWidth, 2 * ExploWidth, ExploHeight, 2 * ExploHeight, SmokeInitialColor,
                                SmokeFinalColor, smokeFrame, SmokeFrameCount, SmokeFrameInRow, SmokeTimePerFrame, WholeTime));

            Particles = new List<Particle>();
            for (int i = 0; i < ParticleCount; i++)
            {
                Vector2 Telorance = new Vector2(random.Next((int)-Speed*2, (int)Speed*2), random.Next((int)-Speed*2, (int)Speed*2));
                Particles.Add(new Particle(ParticleTexture, Location, Velocity+Telorance, 6, 6, ParticleInitialColor, ParticleFinalColor,
                                            ParticleFrame, ParticleFrameCount, ParticleTimePerFrame, WholeTime));
            }

        }


        public bool Expired
        {
            get
            {
                return (Smokes.Count + Particles.Count) > 0;
            }
        }


        public bool Collidable
        {
            get
            {
                return IsCollidable;
            }
        }

        public void Update(GameTime gameTime)
        {
            for (int i = Smokes.Count - 1; i >= 0; i--)
            {
                Smokes[i].Update(gameTime);
                if (Smokes[i].Expired) Smokes.RemoveAt(i);
            }
            for (int i = Particles.Count - 1; i >= 0; i--)
            {
                Particles[i].Update(gameTime);
                if (Particles[i].Expired) Particles.RemoveAt(i);
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            for (int i = Particles.Count - 1; i >= 0; i--)
            {
                Particles[i].Draw(spriteBatch, gameTime);
            }
            for (int i = Smokes.Count - 1; i >= 0; i--)
            {
                Smokes[i].Draw(spriteBatch, gameTime);
            }
        }
    }
}
