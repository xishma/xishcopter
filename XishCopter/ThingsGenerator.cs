using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XishCopter
{
    static class ThingsGenerator
    {

        public static Texture2D BlockTexture;
        public static Texture2D SimpleMissileTextur;
        public static Texture2D SpecialMissileTexture;

        public static List<Block> Blocks;
        public static List<Missile> SpecialMissiles;
        public static List<Missile> SimpleMissiles;
        public static Copter XishCopter;

        public static float MinTimePerBlock;
        public static float MaxTimePerBlock;
        float RemainigTimeForBlock=3;

        public static float MinTimePerSimpleMissile;
        public static float MaxTimePerSimpleMissile;
        float RemainigTimeForSimpleMissile = 20;

        public static float MinTimePerSpecialMissile;
        public static float MaxTimePerSpecialMissile;
        float RemainigTimeForSpecialMissile = 30;


    }
}
