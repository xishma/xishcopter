using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XishCopter
{
    class Background
    {


        List<Texture2D> Textures;
        Rectangle ScreenSize;
        Vector2 Velocity;
        List<BGBlock> Positions=new List<BGBlock>();
        Color TintColor;
        float LyerDepth;

        public Background(List<Texture2D> textures, Rectangle screenSize, Vector2 velocity, Color tintColor, float lyerDepth)
        {
            Textures = textures;
            ScreenSize = screenSize;
            Velocity = velocity;
            TintColor = tintColor;
            LyerDepth = lyerDepth;

            Random random = new Random();
            int Index = random.Next(0, Textures.Count);
            BGBlock NewBlock = new BGBlock();
            NewBlock.TextureIndex = Index;
            NewBlock.Position = new Rectangle(0,0,(int)(Textures[Index].Width * (ScreenSize.Height/Textures[Index].Height)),ScreenSize.Height);
            Positions.Add(NewBlock);
            while ((Positions[Positions.Count - 1].Position.X + Positions[Positions.Count - 1].Position.Width) < ScreenSize.Width)
            {
                Index = random.Next(0, Textures.Count);
                NewBlock = new BGBlock();
                NewBlock.TextureIndex = Index;
                NewBlock.Position = new Rectangle((Positions[Positions.Count - 1].Position.X + Positions[Positions.Count - 1].Position.Width), 0,
                                                  (int)(Textures[Index].Width * (ScreenSize.Height / Textures[Index].Height))
                                                  , ScreenSize.Height);
                Positions.Add(NewBlock);
            }
        }


        public void Update(GameTime gameTime)
        {
            for (int i = Positions.Count - 1; i >= 0; i--)
            {
                BGBlock bgBlock = Positions[i];
                bgBlock.Position.X -= (int)(Velocity.X * gameTime.ElapsedGameTime.TotalSeconds);
                if (bgBlock.Position.Right < 0) Positions.RemoveAt(i);
            }
            
            Random random = new Random();
            while ((Positions[Positions.Count - 1].Position.X + Positions[Positions.Count - 1].Position.Width) < ScreenSize.Width)
            {
                int Index = random.Next(0, Textures.Count);
                BGBlock NewBlock = new BGBlock();
                NewBlock.TextureIndex = Index;
                NewBlock.Position = new Rectangle((Positions[Positions.Count - 1].Position.X + Positions[Positions.Count - 1].Position.Width), 0,
                                                  (int)(Textures[Index].Width * (ScreenSize.Height / Textures[Index].Height))
                                                  , ScreenSize.Height);
                Positions.Add(NewBlock);
            }
        }

        public void Draw(SpriteBatch spriteBatch,GameTime gameTime)
        {
            foreach (BGBlock bgBlock in Positions)
            {
                spriteBatch.Draw(Textures[bgBlock.TextureIndex],bgBlock.Position,null,TintColor,0,Vector2.Zero,SpriteEffects.None,LyerDepth);
            }
        }

    }
}
