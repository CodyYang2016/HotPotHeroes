// using Microsoft.Xna.Framework;
// using Microsoft.Xna.Framework.Graphics;

// namespace Mono
// {
//     public class SpriteAnimation : ISprite
//     {

//         public Texture2D Texture { get; private set; }
//         private int frameWidth; // Width of a single frame in the sprite sheet
//         private int frameHeight; // Height of a single frame in the sprite sheet
//         private int totalFrames; // Total number of frames in the animation
//         private int currentFrame; // Current frame index
//         private float timePerFrame; // Time for each frame (in seconds)
//         private float timeSinceLastFrame; // Time since the last frame update

//         public SpriteAnimation(Texture2D texture, int frameWidth, int frameHeight, int totalFrames, float timePerFrame)
//         {
//             Texture = texture;
//             this.frameWidth = frameWidth;
//             this.frameHeight = frameHeight;
//             this.totalFrames = totalFrames;
//             this.timePerFrame = timePerFrame;
//             this.currentFrame = 0;
//             this.timeSinceLastFrame = 0f;
//         }

//         public void Update(GameTime gameTime)
//         {
//             timeSinceLastFrame += (float)gameTime.ElapsedGameTime.TotalSeconds;

//             // If enough time has passed, move to the next frame
//             if (timeSinceLastFrame >= timePerFrame)
//             {
//                 currentFrame++;
//                 if (currentFrame >= totalFrames)
//                 {
//                     currentFrame = 0; // Loop back to the first frame
//                 }
//                 timeSinceLastFrame = 0f;
//             }
//         }

//         public void DrawA(SpriteBatch spriteBatch, Vector2 location)
//         {
//             // Calculate the source rectangle for the current frame


//             // Rectangle sourceRectangle = new Rectangle(currentFrame * frameWidth, 0, frameWidth, frameHeight);
//             // Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, frameWidth, frameHeight);

//             Rectangle sourceRectangle = new Rectangle(1, 11, 16, 16);
//             Rectangle destinationRectangle = new Rectangle(50, 50, 60, 60);

//             if(currentFrame == 0)
//             {
//                 sourceRectangle = new Rectangle(1, 11, 16, 16);
//                 destinationRectangle = new Rectangle((int)location.X,
//                 (int)location.Y, 32, 32);
//             }
//             else if(currentFrame == 1)
//             {
//                 sourceRectangle = new Rectangle(35, 11, 16, 16);
//                 destinationRectangle = new Rectangle((int)location.X,
//                 (int)location.Y, 32, 32);
//             }
//             else if(currentFrame == 2)
//             {
//                 sourceRectangle = new Rectangle(70, 11, 16, 16);
//                 destinationRectangle = new Rectangle((int)location.X,
//                 (int)location.Y, 32, 32);
//             }
//             else if(currentFrame == 3)
//             {
//                 sourceRectangle = new Rectangle(103, 11, 16, 16);
//                 destinationRectangle = new Rectangle((int)location.X,
//                 (int)location.Y, 32, 32);
//             }

//             // Draw the sprite
//             //spriteBatch.Begin();
//             spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
//             //spriteBatch.End();
//         }

//         public void DrawB(SpriteBatch spriteBatch, Vector2 location)
//         {
//             // Calculate the source rectangle for the current frame


//             // Rectangle sourceRectangle = new Rectangle(currentFrame * frameWidth, 0, frameWidth, frameHeight);
//             // Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, frameWidth, frameHeight);

//             Rectangle sourceRectangle = new Rectangle(1, 11, 16, 16);
//             Rectangle destinationRectangle = new Rectangle(50, 50, 60, 60);


//             sourceRectangle = new Rectangle(191, 185, 16, 16);
//             destinationRectangle = new Rectangle((int)location.X,
//             (int)location.Y, 32, 32);
            

//             // Draw the sprite
//             //spriteBatch.Begin();
//             spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
//             //spriteBatch.End();
//         }
//     }
// }
