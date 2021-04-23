using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CarGame
{
    class Player : Entity
    {
        public Player(Texture2D texture, Vector2 position, float rotation) : base(texture, position, rotation)
        {
            this.texture = texture;
            this.position = position;
            this.origin = new Vector2(texture.Width / 2, texture.Height / 2);
            this.rotation = rotation;

            rectangle = new Rectangle(
                (int)this.position.X, (int)this.position.Y,
                this.texture.Width, this.texture.Height
                );
        }

        public override void Update()
        {
            rectangle.X = (int)position.X;
            rectangle.Y = (int)position.Y;
        }

        public void Move()
        {
            KeyboardState input = Keyboard.GetState();

            if (input.IsKeyDown(Keys.W))
            {
                if (rotation != 180)
                    rotation = 180;
                position.Y -= 4;
            }

            if (input.IsKeyDown(Keys.A))
            {
                if (rotation != 90)
                    rotation = 90;
                position.X -= 4;
            }

            if (input.IsKeyDown(Keys.S))
            {
                if (rotation != 0)
                    rotation = 0;
                position.Y += 4;
            }

            if (input.IsKeyDown(Keys.D))
            {
                if (rotation != -90)
                    rotation = -90;
                position.X += 4;
            }
        }
    }
}
