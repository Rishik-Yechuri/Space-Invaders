using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace Space_Invaders
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        bool stopMoving = false;
        Alien[,] aliens = new Alien[8, 8];
        enum GameState { Left, Right, Down, Up }
        GameState gameState = GameState.Right;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 1000;
            graphics.PreferredBackBufferWidth = 1000;
        }
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    aliens[i, j] = new Alien();
                    aliens[i, j].Rect = new Rectangle(i * 40 + 75, j * 20+80, 20, 20);
                }
            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)aliens[x, y].Text = this.Content.Load<Texture2D>("SpaceInvaders" + y);
            }
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)this.Exit();
            // TODO: Add your update logic here
            stopMoving = false;

            if (aliens[7, 7].Rect.X >= 930)
            { gameState = GameState.Down; }
            if (aliens[7,7].Rect.Y >= 930) { gameState = GameState.Left; }
            if (aliens[0, 0].Rect.X <= 70) { gameState = GameState.Up; }
            if (aliens[0, 0].Rect.Y <= 70 && gameState == GameState.Up) gameState = GameState.Right;
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    if (gameState == GameState.Right /*&& aliens[i, j].Rect.X < 950*/) aliens[i, j].setPosition(5, true);
                    /*else if (gameState == GameState.Right && aliens[i, j].Rect.X >= 950)
                    {
                        stopMoving = true;
                        gameState = GameState.Down;
                    }*/
                    if (gameState == GameState.Down /*&& aliens[i, j].Rect.Y < 950*/) aliens[i, j].setPosition(5, false);
                    /*else if (gameState == GameState.Down && aliens[i, j].Rect.Y >= 950)
                    {
                        stopMoving = true;
                        gameState = GameState.Left;
                     }*/
                     if (gameState == GameState.Left /*&& aliens[i, j].Rect.X > 70*/) aliens[i, j].setPosition(-5, true);
                     /*else if (gameState == GameState.Left && aliens[i, j].Rect.X <= 70)
                     {
                        stopMoving = true;
                        gameState = GameState.Up;
                      }*/
                      if (gameState == GameState.Up /*&& aliens[i, j].Rect.Y > 70*/) aliens[i, j].setPosition(-5, false);
                      /*else if (gameState == GameState.Up && aliens[i, j].Rect.Y <= 70)
                      {
                        stopMoving = true;
                        gameState = GameState.Right;
                       }*/
                }
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            for (int i = 0; i < 8; i++)for (int j = 0; j < 8; j++)spriteBatch.Draw(aliens[i, j].Text, aliens[i, j].Rect, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}