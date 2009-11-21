using System;
using System.Collections.Generic;
using GameDev.Core;
using GameDev.Core.Graphics;
using GameDev.Core.Menus;
using GameDev.Core.Particles;
using GameDev.Core.SceneManagement;
#if SILVERLIGHT
using SilverArcade.SilverSprite;
using SilverArcade.SilverSprite.Audio;
using SilverArcade.SilverSprite.Graphics;
using SilverArcade.SilverSprite.Input;
using SilverlightHelpers;
#else
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
#endif

namespace KlimaKonflikt.Scenes
{
    public class KKMenuScene :  MenuScene
    {

        SoundEffectInstance m_creditsTune;
        private VectorSprite m_Oilbarrel, m_seedSack, m_fireOil, m_fireSeed;
        private SmokePlumeParticleSystem m_smokeParticleSystem;
        private List<VectorSprite> m_sprites = new List<VectorSprite>();
        private Vector2 m_topLeft , m_bottomRight ;
        private float m_timeToNextSmokePuff;

        public KKMenuScene()
            : base(SceneNames.MENUSCENE, new KKGameMenu(), Color.Black)
        {
            SoundEffect effect = GameDevGame.Current.Content.Load<SoundEffect>(@"GameTunes\CreditsTune");
            m_creditsTune = effect.CreateInstance();
#if SILVERLIGHT
            m_creditsTune.Loop = true;
#else
            m_creditsTune.IsLooped = true;
#endif
            this.Menu.MenuItemActivated += new GameDev.Core.Menus.MenuItemHandler(Menu_MenuItemActivated);
            m_Oilbarrel = GameImages.GetOilBarrelImage();
            m_seedSack = GameImages.GetFlowersackImage();
            m_fireSeed = GameImages.GetIldImage();
            m_fireOil = GameImages.GetIldImage();
            m_sprites.Add(m_Oilbarrel);
            m_sprites.Add(m_seedSack);
            m_sprites.Add(m_fireOil);
            m_sprites.Add(m_fireSeed);

            foreach (VectorSprite sprite in m_sprites)
            {
                sprite.Scale = new Vector2(1.3F, 1.3F);
                Components.Add(sprite);
            }

            m_topLeft = new Vector2(-100, 80);
            m_bottomRight = new Vector2(GameDevGame.Current.GraphicsDevice.Viewport.GetRight() + 100, 700);


            m_smokeParticleSystem = new SmokePlumeParticleSystem(50);
            Components.Add(m_smokeParticleSystem);
        }

        private void ResetSprite(VectorSprite sprite)
        {
            if (sprite.Position.Y > GameDevGame.Current.ViewPortCenter.Y)
            {
                sprite.Position = m_topLeft;
            }
            else
            {
                sprite.Position = m_bottomRight;
            }
            sprite.Speed *= -Vector2Helper.Right;

        }

        void Menu_MenuItemActivated(GameDev.Core.Menus.MenuItem sender, EventArgs e)
        {
            int numberOfWinsToWinGame = ((IntervalMenuItem)Menu.GetMenuItem(KKGameMenu.MENU_NUMBEROFWINS)).CurrentValue;
            MainScene mainScene = (MainScene)SceneManager.GetScene(SceneNames.MAINSCENE);
            switch (sender.Name)
            {

                case KKGameMenu.MENU_STARTSINGLEPLAYER:
                       mainScene.SinglePlayer = true;
                    mainScene.NumberOfWinsToWinGame = numberOfWinsToWinGame;
                    SceneManager.ChangeScene(mainScene);
                    break;

                case KKGameMenu.MENU_START2PLAYER:
                    
                    mainScene.SinglePlayer = false;
                    mainScene.NumberOfWinsToWinGame = numberOfWinsToWinGame;
                    SceneManager.ChangeScene(mainScene);
                    break;

                case KKGameMenu.MENU_EXIT:
                    GameDevGame.Current.Exit();
                    break;

                case KKGameMenu.MENU_TOGGLEFULLSCREEN:
                    GameDevGame.Current.GraphicsDeviceManager.ToggleFullScreen();
                    break;

                case KKGameMenu.MENU_LETHALFIRE :
                    mainScene.FireIsLethal = ((CheckBoxMenuItem)Menu.GetMenuItem(KKGameMenu.MENU_LETHALFIRE)).Checked;
                    break;

                case KKGameMenu.MENU_CREDITS:
                    SceneManager.Current.ChangeScene(SceneNames.CREDITSSCENE);
                    break;

                case KKGameMenu.MENU_INSTRUCTIONS:
                    SceneManager.Current.ChangeScene(SceneNames.INSTRUCTIONSSCENE);
                    break;
            }
	}


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (NoKeysPressed)
            {if(UpdatedKeyboardState.IsKeyDown(Keys.Escape))
                {
                    GameDevGame.Current.Exit();  
                }
            }

            foreach (VectorSprite sprite in m_sprites)
            {
                if ((sprite.Position.X < -100 && sprite.Speed.X < 0) || (sprite.Speed.X > 0 && sprite.Position.X > GameDevGame.Current.GraphicsDevice.Viewport.GetRight() + 100))
                {
                    ResetSprite(sprite);
                }
            }

            m_timeToNextSmokePuff -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (m_timeToNextSmokePuff <= 0)
            {
                m_smokeParticleSystem.AddParticles(m_fireOil.Position + Vector2Helper.Down * 8);
                m_smokeParticleSystem.AddParticles(m_fireSeed.Position + Vector2Helper.Down * 8);
                m_timeToNextSmokePuff = GameDevGame.Current.Random.Next(300);
            }

        }

        public override void OnEntered()
        {
            base.OnEntered();
            
            m_creditsTune.Play();
            m_smokeParticleSystem.Clear();

            m_Oilbarrel.Position = m_topLeft;
            m_fireOil.Position = m_topLeft + Vector2Helper.Left * 100;
            m_seedSack.Position = m_bottomRight;
            m_fireSeed.Position = m_bottomRight + Vector2Helper.Right * 100;
            
            m_Oilbarrel.Speed = Vector2Helper.Right * 0.2F;
            m_fireOil.Speed = Vector2Helper.Right * 0.2F;
            
            m_seedSack.Speed = Vector2Helper.Left * 0.2F;
            m_fireSeed.Speed = Vector2Helper.Left * 0.2F;
        }

        public override void OnLeft()
        {
            base.OnLeft();
            m_creditsTune.Stop();
        }

    }
}
