using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XishCopter.Managers
{
    static class ThingsGenerator
    {

        public static List<Block> Blocks;
        public static List<Missile> SimpleMissiles;
        public static List<Missile> SpecialMissiles;
        public static Copter XishCopter;

        public static float TimePerBlock;
        public static float TimePerSimpleMissile;
        public static float TimePerSpecialMissile;

        public static Rectangle Screen;

        public static void Update(GameTime gameTime,int Score)
        { 
        }

    }
}
