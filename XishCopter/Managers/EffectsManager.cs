using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XishCopter
{
    static class EffectsManager
    {

        public static Texture2D ParticleTexture;
        public static Rectangle ParticleFrame;
        public static int ParticleFrameCount;

        public static Texture2D SmokeTexture;
        public static Rectangle SmokeTextureFrame;
        public static int SmokeTextureFrameCount;
        public static int SmokeTextureFramePerRow;

        public static Rectangle Screen;

        #region Explosions

        public static Explosion SimpleMissileExplosion(Rectangle Position,Vector2 Speed)
        {
            Explosion explosion = null;

            return explosion;
        }


        public static Explosion ParticleExplosion(Rectangle Position, Vector2 Speed)
        {
            Explosion explosion = null;

            return explosion;
        }


        public static Explosion CopterExplosion(Rectangle Position, Vector2 Speed)
        {
            Explosion explosion = null;

            return explosion;
        }

        #endregion


        #region SmokeLines

        public static SmokeLine CopterSmokeLine(Vector2 Speed)
        {
            SmokeLine smokeLine = null;

            return smokeLine;
        }

        public static SmokeLine SimpleMissileSmokeLine(Vector2 Speed)
        {
            SmokeLine smokeLine = null;

            return smokeLine;
        }

        public static SmokeLine SpecialMissileSmokeLine(Vector2 Speed)
        {
            SmokeLine smokeLine = null;

            return smokeLine;
        }

        #endregion


        #region

        public static Missile SimpleMissile(Vector2 position)
        {
            Missile missile = null;

            return missile;
        }

        public static Missile SpecialMissile(Vector2 position)
        {
            Missile missile = null;

            return missile;
        }

        #endregion


    }
}
