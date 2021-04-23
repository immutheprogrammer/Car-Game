using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CarGame
{ 
    public class Entity
    {
        public Texture2D texture;
        public Vector2 position;
        public Vector2 origin;
        public Rectangle rectangle;
        public float rotation;
        public Color color;

        public Entity(Texture2D texture, Vector2 position, float rotation)
        {
            this.texture = texture;
            this.position = position;
            this.origin = new Vector2(texture.Width / 2, texture.Height / 2);
            this.rotation = rotation;

            color = Color.White;


            rectangle = new Rectangle(
                (int)this.position.X, (int)this.position.Y,
                this.texture.Width, this.texture.Height
                );
        }

        public virtual void Update()
        {
            rectangle.X = (int)position.X;
            rectangle.Y = (int)position.Y;
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, color, MathHelper.ToRadians(rotation), origin, 1, SpriteEffects.None, 1);
        }

        public void Dispose()
        {
            texture.Dispose();
            rectangle = Rectangle.Empty;
        }

    }
}
