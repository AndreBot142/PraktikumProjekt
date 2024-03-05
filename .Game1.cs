using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Comora;
using System;
using PraktikumProjekt.Scripts.Effects;
using PraktikumProjekt.Scripts.Projectiles;
using PraktikumProjekt.Scripts.Loots;
using PraktikumProjekt.Scripts.Scenes;
using PraktikumProjekt.Scripts.MainMenu;

namespace PraktikumProjekt
{
    enum Direction { Up, UpRight, Right, DownRight, Down, DownLeft, Left, UpLeft}
    public class Game1 : Game
    {
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;
        RenderTarget2D _renderTarget;
        Rectangle _renderScale;
        int renderTargetX = 1920;
        int renderTargetY = 1080;
        KeyboardState _keyboardState;

        public static Texture2D cursor;

        public static Texture2D[] _playerTextures = new Texture2D[9];
        public static Texture2D[] _skellyTextures = new Texture2D[9];
        public static Texture2D[] _skellyHeadTextures = new Texture2D[9];
        public static Texture2D[] _lootTextures = new Texture2D[9];
        public static Texture2D[] _portalTextures = new Texture2D[9];
        public static Texture2D[] _projectileTextures = new Texture2D[9];
        public static Texture2D[] _healthBarTextures = new Texture2D[2];

        public static Texture2D _mainMenuBackground; 
        public static Texture2D _loadingScreenBG;
        public static Texture2D _pauseScreen;

        public static Texture2D _caveBackground;
        public static SpriteFont _spriteFont;
        Player _player;
        MainMenu _mainMenu;
        LoadingScreen _loadingScreen;

        Random _random = new Random();
        Camera _camera;
        Scenes _currentScene;

        public static string _gameState = "inMainMenu";
        public bool _PkeyReleased = true;
        public bool _RkeyReleased = true;
        public bool _newGame = true;
        public bool _inMainMenu = true;

        public Game1()
        {
            
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.IsBorderless = true;
        }

        protected override void Initialize()
        {
            _camera = new Camera(_graphics.GraphicsDevice);
            _camera.Position = new Vector2(0, 0);
            _camera.ViewportOffset.Position = new Vector2(0, 0);
            bool _defaultResolution = true; 
            if (_defaultResolution) 
            {
                _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width; 
                _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            }
            _graphics.ApplyChanges();
            base.Initialize();
            IsMouseVisible = false;
        }

        protected override void LoadContent()
        {
            
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            
            _renderTarget = new RenderTarget2D(GraphicsDevice, renderTargetX, renderTargetY);
            _renderScale = new Rectangle(0, 0, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);

            _mainMenuBackground = Content.Load<Texture2D>("Background/MainMenuBG");
            _loadingScreenBG = Content.Load<Texture2D>("Background/StartScreen");
            _pauseScreen = Content.Load<Texture2D>("Background/PauseMenu");

            _playerTextures[0] = Content.Load<Texture2D>("Player/playerWalkUp");
            _playerTextures[2] = Content.Load<Texture2D>("Player/playerWalkRight");
            _playerTextures[4] = Content.Load<Texture2D>("Player/playerWalkDown");
            _playerTextures[6] = Content.Load<Texture2D>("Player/playerWalkLeft");
            _playerTextures[8] = Content.Load<Texture2D>("Player/playerIdle");

            _skellyTextures[0] = Content.Load<Texture2D>("Skelly/skeletonWalkUP");
            _skellyTextures[2] = Content.Load<Texture2D>("Skelly/skeletonWalkRight");
            _skellyTextures[4] = Content.Load<Texture2D>("Skelly/skeletonWalkDown");
            _skellyTextures[6] = Content.Load<Texture2D>("Skelly/skeletonWalkLeft");
            _skellyTextures[8] = Content.Load<Texture2D>("Skelly/skeletonIdle");

            cursor = Content.Load<Texture2D>("Cursors/cursor");

            _projectileTextures[8] = Content.Load<Texture2D>("ball");

            _skellyHeadTextures[8] = Content.Load<Texture2D>("SkellyHead/skull");

            _portalTextures[8] = Content.Load<Texture2D>("Portals/portal-Sheet");

            _lootTextures[8] = Content.Load<Texture2D>("Pickups/item_01");

            _caveBackground = Content.Load<Texture2D>("Background/dungeonBackground");

            _healthBarTextures[0] = Content.Load<Texture2D>("HealthBar/healthbar_1");
            _healthBarTextures[1] = Content.Load<Texture2D>("HealthBar/healthbar_2");

            _spriteFont = Content.Load<SpriteFont>("Fonts/galleryFont");

            _player = new Player(Vector2.Zero);
            _mainMenu = new MainMenu();
            _loadingScreen = new LoadingScreen();
            _currentScene = new Scenes();
        }

        protected override void Update(GameTime gameTime)
        {   
            //Escape button to end game
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _keyboardState = Keyboard.GetState();

            if (_keyboardState.IsKeyDown(Keys.P) && _gameState == "Running" && _PkeyReleased == true)
            {
                _gameState = "Paused";

                _PkeyReleased = false;
            }
            else if(_keyboardState.IsKeyDown(Keys.P) && _gameState == "Paused" && _PkeyReleased == true) {

                _gameState = "Running";
                _PkeyReleased = false;

            }
            if (!_keyboardState.IsKeyDown(Keys.P))
            {
                _PkeyReleased = true;
            }

            if (_keyboardState.IsKeyDown(Keys.R) && _gameState == "Running" && _RkeyReleased == true)
            {
                _gameState = "NewGame";
                _RkeyReleased = false;
            }
            if (!_keyboardState.IsKeyDown(Keys.R))
            {
                _RkeyReleased = true;
            }

            if (_gameState == "NewGame")
            {
                _player.Position = new Vector2(renderTargetX / 2, renderTargetY /2);
                _player.IsDead = false; 
                Projectile.Projectiles.Clear();  //when 2 object are collided, the objects will be removed
                SkellyHead.SkellyHeads.Clear();
                Skelly.Skellies.Clear();
                Loot.Loots.Clear();
                Portal.Portals.Clear();
                _gameState = "Running";
            }
            
            if( _gameState == "inMainMenu")
            {
                _mainMenu.Update(gameTime);
            }
            if (_gameState == "Loading")
            {
                _loadingScreen.Update(gameTime);
            }

            if (_gameState == "Running")
            {
                IsMouseVisible = false;
                //Enemy Spawner Update
                Skelly.UpdateSkellySpawner(gameTime, _skellyTextures, _portalTextures, _player.IsDead);
                SkellyHead.UpdateSkellyHeadSpawner(gameTime, _skellyHeadTextures, _player.IsDead);

                //Player Update
                _player.Update(gameTime, _player.IsDead);

                //Camera Position Update
                _camera.Position = new Vector2(_player.Position.X, _player.Position.Y); //camera follows player
                _camera.ViewportOffset.Position = new Vector2(_player.Position.X, _player.Position.Y);
                _camera.Update(gameTime);

                //Enemies Movement Updates
                foreach (Skelly skelly in Skelly.Skellies)
                {
                    skelly.UpdateSkelly(gameTime, _player.Position, _player.IsDead);
                }
                foreach (SkellyHead skellyHead in SkellyHead.SkellyHeads)
                {
                    skellyHead.UpdateSkellyHead(gameTime, _player.Position, _player.IsDead);
                }


                //Projectile Movement Updates
                foreach (Projectile projectile in Projectile.Projectiles) //Updates each Projectile Position in the Projectile List
                {
                    projectile.UpdateProjectile(gameTime);
                }

                //Effects Updates
                foreach (Portal portal in Portal.Portals)
                {
                    portal.UpdatePortal(gameTime);
                }

                //Loot Updates
                foreach (Loot loot in Loot.Loots)
                {
                    loot.UpdateLoot(gameTime);
                }


                //Collider Detection between Enemies and Projectiles
                foreach (Projectile projectile in Projectile.Projectiles)
                {
                    foreach (SkellyHead skellyHead in SkellyHead.SkellyHeads)
                    {
                        int sum = skellyHead.Radius + projectile.Radius + 10;
                        if (Vector2.Distance(skellyHead.Position, projectile.Position) < sum)
                        {
                            skellyHead.IsCollided = true;
                            projectile.IsCollided = true;
                            int chance = _random.Next(1, 21);
                            if (chance == 1)
                            {
                                Loot.Loots.Add(new Loot(skellyHead.Position));
                            }
                        }
                    }
                }
                foreach (Projectile projectile in Projectile.Projectiles)
                {
                    foreach (Skelly skelly in Skelly.Skellies)
                    {
                        if (Vector2.Distance(projectile.Position, skelly.Position) < projectile.Radius + 20)
                        {
                            skelly.MaxHealth--;
                            if (skelly.MaxHealth < 1)
                            {
                                skelly.IsCollided = true;
                                int chance = _random.Next(1, 21);
                                if (chance == 1)
                                {
                                    Loot.Loots.Add(new Loot(skelly.Position));
                                }
                            }
                            projectile.IsCollided = true;

                        }
                    }
                }

                //Collider Detection between Player and Enemies
                foreach (SkellyHead skellyhead in SkellyHead.SkellyHeads)
                {
                    int sum = skellyhead.Radius + 32;
                    if (Vector2.Distance(skellyhead.Position, _player.Position) < sum)
                    {

                        skellyhead.IsCollided = true;
                        _player.IsDead = true;
                    }
                }
                foreach (Skelly skelly in Skelly.Skellies)
                {
                    int sumX = skelly.Width + _player.Width;
                    int sumY = skelly.Length + _player.Length;
                    if (_player.Collider.Intersects(skelly.Collider))
                    {
                        skelly.IsCollided = true;
                        _player.IsDead = true;
                    }
                }


                foreach (Loot loot in Loot.Loots)
                {
                    int sum = loot.Radius + 32;
                    if (Vector2.Distance(loot.Position, _player.Position) < sum)
                    {
                        loot.IsCollided = true;
                        _player.MultishotTimer = 10;
                    }
                }


                //Object Collision Update
                Projectile.Projectiles.RemoveAll(p => p.IsCollided);  //when 2 object are collided, the objects will be removed
                SkellyHead.SkellyHeads.RemoveAll(e => e.IsCollided);
                Skelly.Skellies.RemoveAll(e => e.IsCollided);
                Loot.Loots.RemoveAll(e => e.IsCollided);


            }
                
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(_renderTarget);
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin(this._camera); //screen follows camer


            if (_gameState == "Running" || _gameState == "Paused")
            {
                _currentScene.Draw(_spriteBatch);
                foreach (Projectile proj in Projectile.Projectiles)
                {
                    _spriteBatch.Draw(_projectileTextures[8], new Vector2(proj.Position.X - 48, proj.Position.Y - 48), Color.White);

                }
                if (!_player.IsDead) 
                { 
                    _player.CurrentAnimation.Draw(_spriteBatch); 
                }

                foreach (SkellyHead skellyHead in SkellyHead.SkellyHeads) 
                { 
                    skellyHead.CurrentAnimation.Draw(_spriteBatch); 
                    _spriteBatch.Draw(_healthBarTextures[0], new Vector2(skellyHead.Position.X - 16, skellyHead.Position.Y - 55), Color.White); 
                }

                foreach (Loot loot in Loot.Loots) 
                { 
                    loot.CurrentAnimation.Draw(_spriteBatch); 
                }

                foreach (Skelly skelly in Skelly.Skellies) 
                { if (skelly.SleepTime < 1)
                    { 
                        skelly.CurrentAnimation.Draw(_spriteBatch); 
                    } 
                }

                foreach (Skelly skelly in Skelly.Skellies) 
                { 
                    if (skelly.MaxHealth == 2) 
                    { 
                        _spriteBatch.Draw(_healthBarTextures[0], new Vector2(skelly.Position.X -18  , skelly.Position.Y - 50 ), Color.White);
                    }
                    if (skelly.MaxHealth == 1)
                    {
                        _spriteBatch.Draw(_healthBarTextures[1], new Vector2(skelly.Position.X - 18, skelly.Position.Y -50), Color.White);
                    }
                }
                foreach (Portal portal in Portal.Portals) 
                { 
                    portal.CurrentAnimation.Draw(_spriteBatch); 
                }


                if(_player.IsDead)
                {
                    _spriteBatch.DrawString(_spriteFont, "GAME OVER", new Vector2(_camera.Position.X - 200, _camera.Position.Y - 150), Color.White);
                }
                if (_player.IsDead)
                {
                    _spriteBatch.DrawString(_spriteFont, "Try again? (R)", new Vector2(_camera.Position.X - 200, _camera.Position.Y - 100), Color.White);
                }

            }
            if(_gameState == "Paused")
            {
                _spriteBatch.Draw(_pauseScreen, new Vector2(_camera.Position.X - 960, _camera.Position.Y - 540), Color.White);
            }
            if (_gameState == "inMainMenu")
            {
                _mainMenu.Draw(_spriteBatch);


            }
            if(_gameState == "Loading")
            {
                _loadingScreen.Draw(_spriteBatch);
            }




            _spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);

            _spriteBatch.Begin();
            _spriteBatch.Draw(_renderTarget, _renderScale, Color.White);
            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}








//Andre was here
//Paddy was here
//Tim was here