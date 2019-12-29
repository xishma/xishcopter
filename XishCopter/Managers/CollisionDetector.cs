using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XishCopter
{
    class CollisionDetector
    {

        static public Copter copter;
        static public List<Block> Blocks;
        static public List<Missile> SimpleMissiles;
        static public List<Missile> SpecialMissiles;
        static public List<Explosion> Explosions;

        static public void Check(int ScreenWidth, int ScreenHeight)
        {
            if (copter.sprite.CollidesScreenEdge(ScreenWidth, ScreenHeight))
                copter.Expired = true;

            else
            {
                if (Blocks != null)
                {
                    foreach (Block block in Blocks)
                    {
                        if (copter.sprite.Collides(block.BlockSprite))
                        {
                            copter.Expired = true;
                        }
                        if (block.BlockSprite.IsInScreen(ScreenWidth, ScreenHeight) == false)
                        {
                            block.IsExpired = true;
                        }
                    }
                }

                if (SimpleMissiles != null)
                {
                    foreach (Missile missile in SimpleMissiles)
                    {
                        if (copter.sprite.Collides(missile.MissileSprite))
                        {
                            copter.Expired = true;
                        }
                        if (missile.MissileSprite.IsInScreen(ScreenWidth, ScreenHeight) == false)
                        {
                            missile.IsExpired = true;
                        }
                        if (Blocks != null)
                            foreach (Block block in Blocks)
                            {
                                if (missile.MissileSprite.Collides(block.BlockSprite))
                                    missile.IsExpired = true;
                            }
                    }
                
                }

                if (SpecialMissiles != null)
                {
                    foreach (Missile missile in SpecialMissiles)
                    {
                        if (missile.InDistance(copter.sprite.CollideRect))
                            missile.IsExpired = true;
                        if (missile.MissileSprite.IsInScreen(ScreenWidth, ScreenHeight) != true)
                            missile.IsExpired = true;
                    }
                }


                for (int i = 0; i < Explosions.Count; i++)
                {
                    if (Explosions[i].Collidable)
                    {
                        for (int j = Explosions[i].Particles.Count - 1; j >= 0; j++)
                        {
                            if (copter.sprite.Collides(Explosions[i].Particles[j].Position))
                            {
                                copter.ParticlesToken++;
                                Explosions[i].Particles.RemoveAt(j);
                            }
                        }
                    }
                }

            }
        }

    }
}
