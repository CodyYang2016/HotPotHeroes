using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using sprint0Test.Link1;
using sprint0Test;

namespace sprint0Test.Link1

{
    
    public enum LinkDirection
    {
        Up, Down, Left, Right
    }

    public enum LinkAction
    {
        Idle, Walking, Attacking, Damaged, UsingItem
    }
    public class LinkSprite
    {
        
        // ӳ��: (LinkAction, LinkDirection) -> �ö���/�����µ�һ��֡
        private Dictionary<(LinkAction, LinkDirection), List<Texture2D>> spriteMap;
        private List<Texture2D> currentFrames = new List<Texture2D>();
        private int currentFrameIndex;
        private int frameCounter;

        private int framesPerImage = 8;

        private bool isVisible = true; // Track visibility

        public float Scale { get; set; } = 1f;
        public LinkAction CurrentAction { get; private set; }
        public LinkDirection CurrentDirection { get; private set; }

        public static Dictionary<(LinkAction, LinkDirection), List<Texture2D>> CreateDefaultSpriteMap(ContentManager content)
        {
            var map = new Dictionary<(LinkAction, LinkDirection), List<Texture2D>>();

            var link1 = content.Load<Texture2D>("Link1");
            var link2 = content.Load<Texture2D>("Link2");
            var linkB1 = content.Load<Texture2D>("LinkB1");
            var linkB2 = content.Load<Texture2D>("LinkB2");
            var linkL1 = content.Load<Texture2D>("LinkL1");
            var linkL2 = content.Load<Texture2D>("LinkL2");
            var linkR1 = content.Load<Texture2D>("LinkR1");
            var linkR2 = content.Load<Texture2D>("LinkR2");

            var linkS1 = content.Load<Texture2D>("LinkS1");
            var linkS2 = content.Load<Texture2D>("LinkS2");
            var linkS3 = content.Load<Texture2D>("LinkS3");
            var linkS4 = content.Load<Texture2D>("LinkS4");

            var linkBS1 = content.Load<Texture2D>("LinkBS1");
            var linkBS2 = content.Load<Texture2D>("LinkBS2");
            var linkBS3 = content.Load<Texture2D>("LinkBS3");
            var linkBS4 = content.Load<Texture2D>("LinkBS4");

            var linkLS1 = content.Load<Texture2D>("LinkLS1");
            var linkLS2 = content.Load<Texture2D>("LinkLS2");
            var linkLS3 = content.Load<Texture2D>("LinkLS3");
            var linkLS4 = content.Load<Texture2D>("LinkLS4");

            var linkRS1 = content.Load<Texture2D>("LinkRS1");
            var linkRS2 = content.Load<Texture2D>("LinkRS2");
            var linkRS3 = content.Load<Texture2D>("LinkRS3");
            var linkRS4 = content.Load<Texture2D>("LinkRS4");

            var linkH = content.Load<Texture2D>("Linkh");

            // Idle
            map[(LinkAction.Idle, LinkDirection.Down)] = new() { link1 };
            map[(LinkAction.Idle, LinkDirection.Up)] = new() { linkB1 };
            map[(LinkAction.Idle, LinkDirection.Left)] = new() { linkL1 };
            map[(LinkAction.Idle, LinkDirection.Right)] = new() { linkR1 };

            // Walking
            map[(LinkAction.Walking, LinkDirection.Down)] = new() { link1, link2 };
            map[(LinkAction.Walking, LinkDirection.Up)] = new() { linkB1, linkB2 };
            map[(LinkAction.Walking, LinkDirection.Left)] = new() { linkL1, linkL2 };
            map[(LinkAction.Walking, LinkDirection.Right)] = new() { linkR1, linkR2 };

            // Attacking
            map[(LinkAction.Attacking, LinkDirection.Down)] = new() { linkS1, linkS2, linkS3, linkS4 };
            map[(LinkAction.Attacking, LinkDirection.Up)] = new() { linkBS1, linkBS2, linkBS3, linkBS4 };
            map[(LinkAction.Attacking, LinkDirection.Left)] = new() { linkLS1, linkLS2, linkLS3, linkLS4 };
            map[(LinkAction.Attacking, LinkDirection.Right)] = new() { linkRS1, linkRS2, linkRS3, linkRS4 };

            // Damaged
            map[(LinkAction.Damaged, LinkDirection.Down)] =
            map[(LinkAction.Damaged, LinkDirection.Up)] =
            map[(LinkAction.Damaged, LinkDirection.Left)] =
            map[(LinkAction.Damaged, LinkDirection.Right)] = new() { linkH };

            return map;
        }
        // IsVisible property
        public bool IsVisible
        {
            get => isVisible;
            set => isVisible = value;
        }
        
        public LinkSprite(Dictionary<(LinkAction, LinkDirection), List<Texture2D>> map)
        {
            spriteMap = map;
            SetState(LinkAction.Idle, LinkDirection.Down); // Initialize to Idle state
        }


        public void SetState(LinkAction action, LinkDirection dir)
        {
            CurrentAction = action;
            CurrentDirection = dir;

            if (!spriteMap.TryGetValue((CurrentAction, CurrentDirection), out currentFrames) || currentFrames.Count == 0)
            {
                Console.WriteLine($"ERROR: Key ({CurrentAction}, {CurrentDirection}) not found in spriteMap!");
                currentFrames = new List<Texture2D>(); // Assign an empty list to prevent crashes
            }
            currentFrameIndex = 0;
            frameCounter = 0;
        }

        public void Update()
        {
        if (currentFrames.Count == 0) return;
        frameCounter ++;
            if (frameCounter >= framesPerImage)
            {
                frameCounter = 0;
                //Loop through Frames
                currentFrameIndex = (currentFrameIndex + 1) % currentFrames.Count;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            Texture2D currentTex = currentFrames[currentFrameIndex];
            spriteBatch.Draw(currentTex,position,null,Color.White,0f, Vector2.Zero,Scale, SpriteEffects.None,0f  
            );
        }

        public Vector2 GetScaledDimensions()
        {
            Texture2D currentTex = currentFrames[currentFrameIndex];
            float width = currentTex.Width * Scale;
            float height = currentTex.Height * Scale;
            return new Vector2(width, height);
        }
    }
}
