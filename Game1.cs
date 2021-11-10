using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace rpg
{
    public class Game1 : Game
    {
        //basic game driving fields
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private GameContent gameContent;
        public static int ScreenHeight; //master screen height, update as the user resizes the screen
        public static int ScreenWidth; //master screen width, update as the user resizes the screen
        private Camera camera;
        private List<Component> gameComponents;
        private readonly float MASTER_VOLUME = 0.05f; //change this later when implementing user options

        //game specific fields
        private Player player;
        private Vector2 playerScale; //scale the player to be drawn with the size of the screen
        private Sprite house;
        private List<GameState> gameStates;
        private GameState currentGameState;
        private KeyboardInputHandler keyboardInputHandler;
        private Song song;
        private bool loadingDone = false;
        private Texture2D loadingScreen;
        private Texture2D menu;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            //TODO why doesn't this resize the window, just the screen?
            //graphics.PreferredBackBufferWidth = 1920;
            //graphics.PreferredBackBufferHeight = 1080;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            this.Window.AllowUserResizing = true;
        }

        protected override void Initialize()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            gameContent = new GameContent(Content);
            ScreenHeight = graphics.PreferredBackBufferHeight;
            ScreenWidth = graphics.PreferredBackBufferWidth;
            gameStates = new List<GameState> 
            {
                //TODO force textures to resize during some states, like
                //loading and main menu, but not gameplay(to prevent issues,
                //limit how small the user can make the screen)
                new GameState("loading", 0), 
                new GameState("main menu", 1), 
                new GameState("gameplay", 2) 
            };
            currentGameState = gameStates.Find(item => item.name == "loading");
            keyboardInputHandler = new KeyboardInputHandler();
            loadingScreen = Content.Load<Texture2D>("loading");
            base.Initialize();
        }

        protected override void LoadContent()
        {
            /*
            song = Content.Load<Song>("Shepherds-Landing");
            MediaPlayer.Play(song);
            MediaPlayer.Volume = 0.05f;
            MediaPlayer.IsRepeating = true;
            */
            var loading = Task.Factory.StartNew(() =>
            {
                // load content here, catch and handle any exceptions
                menu = Content.Load<Texture2D>("menu");
                InitializePlayer();
                house = new Sprite(Content.Load<Texture2D>("house"))
                {
                    Position = new Vector2(200, 200)
                };
                camera = new Camera();
                gameComponents = new List<Component>() { house, player };
                Content.Load<SoundEffect>("ding").Play(volume: MASTER_VOLUME, pitch: 0.0f, pan: 0.0f);
                currentGameState = gameStates.Find(item => item.name == "main menu");
                loadingDone = true;
            });
        }

        protected override void Update(GameTime gameTime)
        {
            if (IsActive == false)
            {
                return;  //our window is not active don't update
            }
            if (loadingDone == false)
            {
                //check if screen size changed TODO figure out why this doesn't update(we must
                //update graphics.preffered... but to what??)
                ScreenHeight = graphics.PreferredBackBufferHeight;
                ScreenWidth = graphics.PreferredBackBufferWidth;
                //update loading screen perhaps with a moving bar, for now just do a static picture so nothing else to do here
                return;
            }
            //check if screen size changed
            ScreenHeight = graphics.PreferredBackBufferHeight;
            ScreenWidth = graphics.PreferredBackBufferWidth;
            //TODO update playerScale vector
            //check if new input has been received by the keyboard
            keyboardInputHandler.Update(gameTime, currentGameState, gameComponents);
            //check if new input has been received by the mouse
            //in the future, only follow the player if they are a certain distance from the current frame boundary. Leave for now.
            camera.Follow(player);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            if (loadingDone == false) { drawLoadingScreen(); return; }
            if (currentGameState.id == 1) //main menu
            {
                spriteBatch.Begin();
                spriteBatch.Draw(menu,
                    new Rectangle(0, 0, ScreenWidth, ScreenHeight),
                    new Rectangle(0, 0, menu.Width, menu.Height),
                    Color.White);
                //TODO draw buttons, implement their functionality
                spriteBatch.End();
                return;
            }
            spriteBatch.Begin(transformMatrix: camera.Transform);
            //TODO incorporate player scale vector into drawing
            foreach (var component in gameComponents)
                component.Draw(gameTime, spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        private void InitializePlayer()
        {
            player = new Player(new Dictionary<string, Animation>()
        {
          { "WalkUp", new Animation(Content.Load<Texture2D>("player/walkup"), 3) },
          { "WalkDown", new Animation(Content.Load<Texture2D>("player/walkdown"), 3) },
          { "WalkLeft", new Animation(Content.Load<Texture2D>("player/walkleft"), 3) },
          { "WalkRight", new Animation(Content.Load<Texture2D>("player/walkright"), 3) },
        })
            {
                Position = new Vector2(100, 100),
            };
        }
        private void drawLoadingScreen()
        {
            spriteBatch.Begin();
            //Thread.Sleep(500);
            spriteBatch.Draw(loadingScreen,
                new Rectangle(0, 0, ScreenWidth, ScreenHeight),
                new Rectangle(0, 0, loadingScreen.Width, loadingScreen.Height),
                Color.White);
            spriteBatch.End();
        }
    }
}

