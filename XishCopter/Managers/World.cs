using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace XishCopter
{
    class World
    {

        enum GameState
        { 
            MainMenu,InGame,GamePaused,LooseScreen,ScoreSave,NameType,HighScores
        };

        
        #region objects & variables
        Copter XishCopter;
        List<Missile> SimpleMissiles;
        List<Missile> SpecialMissiles;
        List<Block> Blocks;
        List<Explosion> Explosions;

        Background backGround;

        float MapSpeed;
        int Score;

        GameState gameState;

        Rectangle Screen;

        #endregion


        #region constructor


        public World(Copter copter,Rectangle screen,float mapSpeed,Background background)
        {
            XishCopter = copter;
            SimpleMissiles = new List<Missile>();
            SpecialMissiles = new List<Missile>();
            Blocks = new List<Block>();
            Explosions = new List<Explosion>();
            
            CollisionDetector.copter = XishCopter;
            CollisionDetector.SimpleMissiles = SimpleMissiles;
            CollisionDetector.SpecialMissiles = SpecialMissiles;
            CollisionDetector.Blocks = Blocks;
            CollisionDetector.Explosions = Explosions;

            Screen = screen;
            MapSpeed = mapSpeed;
            gameState = GameState.MainMenu;
            backGround = background;
        }

        #endregion



        #region Update

        public void Update(GameTime gameTime)
        {

            switch (gameState)
            { 
            
                case GameState.MainMenu:
                    {
                    }
                    break;


                case GameState.InGame:
                    {
                        #region InGame
                        backGround.Update(gameTime);
                        if (XishCopter.Expired == false)
                        {
                            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                                XishCopter.EngineOn();
                            else XishCopter.EngineOff();
                        }

                        else
                        {
                            if (Explosions.Count == 0) gameState = GameState.LooseScreen;
                        }


                        XishCopter.Update(gameTime, new Vector2(MapSpeed, 0));
                        for (int i = Explosions.Count - 1; i >= 0; i--)
                        {
                            if (Explosions[i].Expired) Explosions.RemoveAt(i);
                            else Explosions[i].Update(gameTime);
                        }
                        foreach (Missile missile in SimpleMissiles) missile.Update(gameTime, new Vector2(MapSpeed, 0));
                        foreach (Missile missile in SpecialMissiles) missile.Update(gameTime, new Vector2(MapSpeed, 0));
                        foreach (Block block in Blocks) block.Update(gameTime);
                        
                        
                        CollisionDetector.Check(Screen.Width, Screen.Height);
                        
                        if (XishCopter.Expired)
                        {
                            Explosions.Add(EffectsManager.CopterExplosion(XishCopter.sprite.Position,new Vector2(0,MapSpeed)));
                        }
                        for (int i = SimpleMissiles.Count - 1; i >= 0; i--)
                        {
                            if (SimpleMissiles[i].Removable)
                            {
                                Explosions.Add(EffectsManager.SimpleMissileExplosion(SimpleMissiles[i].MissileSprite.Position, new Vector2(0, MapSpeed)));
                                SimpleMissiles.RemoveAt(i);
                            }
                        }
                        for (int i = SpecialMissiles.Count - 1; i >= 0; i--)
                        {
                            if (SpecialMissiles[i].Removable)
                            {
                                Explosions.Add(EffectsManager.ParticleExplosion(SpecialMissiles[i].MissileSprite.Position, new Vector2(0, MapSpeed)));
                                SpecialMissiles.RemoveAt(i);
                            }
                        }
                        for (int i = Blocks.Count - 1; i >= 0; i--)
                        {
                            if (Blocks[i].Expired)
                                Blocks.RemoveAt(i);
                        }
                        #endregion
                    }
                    break;


                case GameState.GamePaused:
                    {
                    }
                    break;

                case GameState.LooseScreen:
                    { 
                    }
                    break;

                case GameState.ScoreSave:
                    { 
                    }
                    break;

                case GameState.NameType:
                    {
                    }
                    break;

                case GameState.HighScores:
                    {
                    }
                    break;

                default:
                    break;
            }
            
        }
        #endregion


        public void Draw(SpriteBatch spriteBatch,GameTime gameTime)
        {

            switch (gameState)
            {

                case GameState.MainMenu:
                    {
                    }
                    break;


                case GameState.InGame:
                    {
                        backGround.Draw(spriteBatch,gameTime);
                        foreach (Block B in Blocks) B.Draw(spriteBatch,gameTime);
                        foreach (Missile M in SimpleMissiles) M.Draw(spriteBatch,gameTime);
                        foreach (Missile M in SpecialMissiles) M.Draw(spriteBatch, gameTime);
                        XishCopter.Draw(spriteBatch,gameTime);
                        foreach (Explosion E in Explosions) E.Draw(spriteBatch,gameTime);
                    }
                    break;


                case GameState.GamePaused:
                    {
                        backGround.Draw(spriteBatch, gameTime);
                        foreach (Block B in Blocks) B.Draw(spriteBatch, gameTime);
                        foreach (Missile M in SimpleMissiles) M.Draw(spriteBatch, gameTime);
                        foreach (Missile M in SpecialMissiles) M.Draw(spriteBatch, gameTime);
                        XishCopter.Draw(spriteBatch, gameTime);
                        foreach (Explosion E in Explosions) E.Draw(spriteBatch, gameTime);
                    }
                    break;

                case GameState.LooseScreen:
                    {
                    }
                    break;

                case GameState.ScoreSave:
                    {
                    }
                    break;

                case GameState.NameType:
                    {
                    }
                    break;

                case GameState.HighScores:
                    {
                    }
                    break;

                default:
                    break;
            }
        }
    }
}
