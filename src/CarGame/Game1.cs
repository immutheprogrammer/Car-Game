using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CarGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Player player;

        private List<Entity> gas;
        private Entity gas1;
        private Entity gas2;

        private ScoreManager scoreManager;

        private SpriteFont font;
        public static int score;

        Random rand;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            Console.WriteLine(_graphics.PreferredBackBufferWidth);
            Console.WriteLine(_graphics.PreferredBackBufferHeight);
        }

        protected override void Initialize()
        {
            rand = new Random();
            gas = new List<Entity>();

            score = 0;

            scoreManager = ScoreManager.Load();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            player = new Player(Content.Load<Texture2D>("Car"), new Vector2(0, 0), 0);
            gas1 = new Entity(Content.Load<Texture2D>("GasCan"), new Vector2(100, 100), 0);
            gas2 = new Entity(Content.Load<Texture2D>("GasCan"), new Vector2(156, 107), 0);

            gas.Add(gas1);
            gas.Add(gas2);

            font = Content.Load<SpriteFont>("Score");
;
        }

        protected override void Update(GameTime gameTime)
        {
            player.Move();

            player.Update();

            foreach (Entity entity in gas)
            {
                entity.Update();
            }

            foreach (Entity entity in gas)
            { 
                if (player.rectangle.Intersects(entity.rectangle))
                {
                    entity.color = Color.Transparent;
                    entity.position.X = rand.Next(0 + entity.texture.Width, 800 - entity.texture.Height);
                    entity.position.Y = rand.Next(0 + entity.texture.Width, 480 - entity.texture.Width);
                    entity.color = Color.White;
                    score++;

                }
            }

            scoreManager.Add(new Score()
            {
                Value = score
            }
            );

            ScoreManager.Save(scoreManager);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.SlateGray);

            _spriteBatch.Begin();

            player.Draw(_spriteBatch);
            
            foreach (Entity entity in gas)
            {
                entity.Draw(_spriteBatch);
            }

            _spriteBatch.DrawString(font, "Score: " + score, new Vector2(800 - font.Texture.Width , 0 + font.Texture.Height / 6), Color.Black);
            _spriteBatch.DrawString(font, "HighScore: " + string.Join("\n", scoreManager.HighScores.Select(c => c.Value).ToArray()[0]), new Vector2(800 - font.Texture.Width, 50 + font.Texture.Height / 6), Color.Black);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
