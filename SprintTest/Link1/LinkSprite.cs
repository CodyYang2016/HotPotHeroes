using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

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