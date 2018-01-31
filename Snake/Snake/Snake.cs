using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Snake
{
    public class Snake
    {
        Texture2D texture;
        Rectangle head;
        List<Rectangle> body;
        int baseMove;

        public Snake(Texture2D texture, int baseMove)
        {
            this.texture = texture;
            this.baseMove = baseMove;
            int x = baseMove * 12;
            int y = baseMove * 9;
            this.head = new Rectangle(x, y, texture.Width, texture.Height);
            body = new List<Rectangle>
            {
                new Rectangle(x - baseMove,y - baseMove, texture.Width, texture.Height),
                new Rectangle(x - baseMove * 2,y - baseMove * 2, texture.Width, texture.Height)
            };
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, head, Color.White);
            foreach (Rectangle bodyPart in body)
            {
                spriteBatch.Draw(texture, bodyPart, Color.White);
            }            
        }

        public void Update(Vector2 movement, GraphicsDeviceManager graphics, Rectangle appleRect)
        {
            Rectangle oldPos = head;
            Rectangle newPos = oldPos;

            //Head movement
            newPos.X += (int)(movement.X * baseMove);
            newPos.Y += (int)(movement.Y * baseMove);

            //Wrapping screen
            if (newPos.X + 1 > graphics.PreferredBackBufferWidth)
            {
                newPos.X = 0;
            }
            if (newPos.X < 0)
            {
                newPos.X = graphics.PreferredBackBufferWidth - baseMove;
            }
            if (newPos.Y + 1 > graphics.PreferredBackBufferHeight)
            {
                newPos.Y = 0;
            }
            if (newPos.Y < 0)
            {
                newPos.Y = graphics.PreferredBackBufferHeight - baseMove;
            }
            head = newPos;

            //Body follow up
            for (int i = 0; i < body.Count; i++)
            {
                newPos = body[i];
                body[i] = oldPos;
                oldPos = newPos;

                //Kill Snake if it intersects with Body
                if (head.Intersects(body[i]))
                {
                    new Game1();
                    break;
                }
            }

            //Eating Apple
            if (head.Intersects(appleRect))
            {
                body.Add(new Rectangle(body[body.Count - 1].X, body[body.Count - 1].Y, texture.Width, texture.Height));
            }
        }
    }
}
