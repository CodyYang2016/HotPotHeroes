using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using sprint0Test.Link1;
using sprint0Test;

namespace sprint0Test.Link1
{
    public enum LinkDirection { Up, Down, Left, Right }
    public enum LinkAction { Idle, Walking, Attacking, Damaged, UsingItem }

    public class LinkSprite
    {
        private const int FramesPerImage = 8;
        private const float BaseScale = 1f;
        private const string MissingKeyError = "Key ({0}, {1}) not found in spriteMap!";

        private readonly AnimationManager _animationManager;
        private readonly SpriteDimensions _spriteDimensions;
        private readonly Dictionary<LinkDirection, Vector2> _baselineSizes;

        public float Scale { get; set; } = BaseScale;
        public LinkAction CurrentAction => _animationManager.CurrentAction;
        public LinkDirection CurrentDirection => _animationManager.CurrentDirection;
        public bool IsVisible { get; set; } = true;
        
        private Dictionary<(LinkAction, LinkDirection), List<Texture2D>> spriteMap;
        private List<Texture2D> currentFrames = new List<Texture2D>();
        private int currentFrameIndex;
        private int frameCounter;

        private int framesPerImage = 8;

        private bool isVisible = true; // Track visibility


        public static Dictionary<(LinkAction, LinkDirection), List<Texture2D>> CreateDefaultSpriteMap(ContentManager content)
        {
            var map = new Dictionary<(LinkAction, LinkDirection), List<Texture2D>>();
            // Link update code
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

            var dot = content.Load<Texture2D>("dot");
            var Map = content.Load<Texture2D>("Map");
            Dictionary<(LinkAction, LinkDirection), List<Texture2D>> linkMap =
        new Dictionary<(LinkAction, LinkDirection), List<Texture2D>>();

            // Idle
            linkMap.Add((LinkAction.Idle, LinkDirection.Down), new List<Texture2D> { link1 });
            linkMap.Add((LinkAction.Idle, LinkDirection.Up), new List<Texture2D> { linkB1 });
            linkMap.Add((LinkAction.Idle, LinkDirection.Left), new List<Texture2D> { linkL1 });
            linkMap.Add((LinkAction.Idle, LinkDirection.Right), new List<Texture2D> { linkR1 });

            // Walking
            linkMap.Add((LinkAction.Walking, LinkDirection.Down),
                new List<Texture2D> { link1, link2 });
            linkMap.Add((LinkAction.Walking, LinkDirection.Up),
                new List<Texture2D> { linkB1, linkB2 });
            linkMap.Add((LinkAction.Walking, LinkDirection.Left),
                new List<Texture2D> { linkL1, linkL2 });
            linkMap.Add((LinkAction.Walking, LinkDirection.Right),
                new List<Texture2D> { linkR1, linkR2 });

            // Attacking
            linkMap.Add((LinkAction.Attacking, LinkDirection.Down),
                new List<Texture2D> { linkS1, linkS2, linkS3, linkS4 });
            linkMap.Add((LinkAction.Attacking, LinkDirection.Up),
                new List<Texture2D> { linkBS1, linkBS2, linkBS3, linkBS4 });
            linkMap.Add((LinkAction.Attacking, LinkDirection.Left),
                new List<Texture2D> { linkLS1, linkLS2, linkLS3, linkLS4 });
            linkMap.Add((LinkAction.Attacking, LinkDirection.Right),
                new List<Texture2D> { linkRS1, linkRS2, linkRS3, linkRS4 });

            // Damage
            linkMap.Add((LinkAction.Damaged, LinkDirection.Down),
                new List<Texture2D> { linkH });
            linkMap.Add((LinkAction.Damaged, LinkDirection.Up),
                new List<Texture2D> { linkH });
            linkMap.Add((LinkAction.Damaged, LinkDirection.Left),
                new List<Texture2D> { linkH });
            linkMap.Add((LinkAction.Damaged, LinkDirection.Right),
                new List<Texture2D> { linkH });

                return linkMap;
        }
        public LinkSprite(Dictionary<(LinkAction, LinkDirection), List<Texture2D>> spriteMap)
        {
            ValidateSpriteMap(spriteMap);

            _animationManager = new AnimationManager(spriteMap);
            _baselineSizes = CalculateBaselineSizes(spriteMap);
            _spriteDimensions = new SpriteDimensions(this);
        }
            
        
        public void SetState(LinkAction action, LinkDirection direction)
            => _animationManager.SetState(action, direction);

        public void Update() => _animationManager.Update();

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            if (!IsVisible) return;

            var (currentTexture, drawPosition) = CalculateDrawParameters(position);
            spriteBatch.Draw(currentTexture, drawPosition, Color.White, Scale);
        }

        public Vector2 GetScaledDimensions() => _spriteDimensions.GetCurrentDimensions();

        private static void ValidateSpriteMap(Dictionary<(LinkAction, LinkDirection), List<Texture2D>> spriteMap)
        {
            if (spriteMap == null || spriteMap.Count == 0)
                throw new ArgumentException("Invalid sprite map");
        }

        private Dictionary<LinkDirection, Vector2> CalculateBaselineSizes(
            Dictionary<(LinkAction, LinkDirection), List<Texture2D>> spriteMap)
        {
            var sizes = new Dictionary<LinkDirection, Vector2>();
            foreach (LinkDirection direction in Enum.GetValues(typeof(LinkDirection)))
            {
                var key = (LinkAction.Idle, direction);
                if (spriteMap.TryGetValue(key, out var frames) && frames.Count > 0)
                {
                    sizes[direction] = new Vector2(frames[0].Width, frames[0].Height);
                }
                else
                {
                    throw new InvalidOperationException($"Missing idle frames for {direction}");
                }
            }
            return sizes;
        }

        private (Texture2D texture, Vector2 position) CalculateDrawParameters(Vector2 basePosition)
        {
            var currentTexture = _animationManager.CurrentFrame;
            var baseSize = _baselineSizes[CurrentDirection] * Scale;
            var currentSize = new Vector2(currentTexture.Width, currentTexture.Height) * Scale;

            AdjustSizeForSpecialCases(ref baseSize, currentTexture);

            var offset = new Vector2(
                (baseSize.X - currentSize.X) / 2f,
                baseSize.Y - currentSize.Y
            );

            return (currentTexture, basePosition + offset);
        }

        private void AdjustSizeForSpecialCases(ref Vector2 baseSize, Texture2D currentTexture)
        {
            if (CurrentAction == LinkAction.Attacking && CurrentDirection == LinkDirection.Down)
            {
                baseSize.Y = currentTexture.Height * Scale;
            }
        }

        private class AnimationManager
        {
            private readonly Dictionary<(LinkAction, LinkDirection), List<Texture2D>> _spriteMap;
            private List<Texture2D> _currentFrames = new List<Texture2D>();
            private int _currentFrameIndex;
            private int _frameCounter;

            public LinkAction CurrentAction { get; private set; }
            public LinkDirection CurrentDirection { get; private set; }
            public Texture2D CurrentFrame => _currentFrames.Count > 0
                ? _currentFrames[_currentFrameIndex]
                : throw new InvalidOperationException("No frames available");

            public AnimationManager(Dictionary<(LinkAction, LinkDirection), List<Texture2D>> spriteMap)
            {
                _spriteMap = spriteMap ?? throw new ArgumentNullException(nameof(spriteMap));
                SetState(LinkAction.Idle, LinkDirection.Down);
            }

            public void SetState(LinkAction action, LinkDirection direction)
            {
                CurrentAction = action;
                CurrentDirection = direction;
                UpdateCurrentFrames();
                ResetAnimation();
            }

            public void Update()
            {
                if (CurrentAction == LinkAction.Idle) return;
                if (_currentFrames.Count == 0) return;

                if (++_frameCounter >= FramesPerImage)
                {
                    _frameCounter = 0;
                    _currentFrameIndex = (_currentFrameIndex + 1) % _currentFrames.Count;
                }
            }

            private void UpdateCurrentFrames()
            {
                if (!_spriteMap.TryGetValue((CurrentAction, CurrentDirection), out _currentFrames))
                {
                    throw new KeyNotFoundException(string.Format(
                        MissingKeyError, CurrentAction, CurrentDirection));
                }
            }

            private void ResetAnimation()
            {
                _currentFrameIndex = 0;
                _frameCounter = 0;
            }
        }

        private class SpriteDimensions
        {
            private readonly LinkSprite _linkSprite;

            public SpriteDimensions(LinkSprite linkSprite)
            {
                _linkSprite = linkSprite ?? throw new ArgumentNullException(nameof(linkSprite));
            }

            public Vector2 GetCurrentDimensions()
            {
                var texture = _linkSprite._animationManager.CurrentFrame;
                return new Vector2(texture.Width, texture.Height) * _linkSprite.Scale;
            }
        }
    }

    public static class SpriteBatchExtensions
    {
        public static void Draw(this SpriteBatch spriteBatch, Texture2D texture, Vector2 position,
            Color color, float scale, float depth = 0f)
        {
            spriteBatch.Draw(
                texture,
                position,
                null,
                color,
                0f,
                Vector2.Zero,
                scale,
                SpriteEffects.None,
                depth);
        }
    }
}