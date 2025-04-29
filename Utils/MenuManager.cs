using System.Collections.Generic;
using sprint0Test.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using sprint0Test.Interfaces;
using sprint0Test.Items;
using sprint0Test.Sprites;
using sprint0Test.Link1;
using System;
using sprint0Test.Dungeon;
using sprint0Test;
using System.Diagnostics;
using sprint0Test.Enemy;
using sprint0Test.Room;
using sprint0Test.Audio;
namespace sprint0Test;

public class MenuManager
{
    private SpriteFont font;
    private Texture2D backgroundTexture;
    private int _selectedIndex = 0;
    private string[] menuItems = { "Start Game", "Controls", "Exit" };
    private KeyboardState _previousKeyboardState; // Tracks previous state
    public Action<int> OnOptionSelected;


    public MenuManager(SpriteFont font, Texture2D backgroundTexture)
    {
        this.font = font;
        this.backgroundTexture = backgroundTexture;
    }

    public void Update(Game1 game)
    {
        KeyboardState currentKeyboardState = Keyboard.GetState();

        if (WasKeyPressed(Keys.Down, currentKeyboardState))
        {
            _selectedIndex = (_selectedIndex + 1) % menuItems.Length;
        }
        else if (WasKeyPressed(Keys.Up, currentKeyboardState))
        {
            _selectedIndex = (_selectedIndex - 1 + menuItems.Length) % menuItems.Length;
        }
        else if (WasKeyPressed(Keys.Enter, currentKeyboardState))
        {
            switch (_selectedIndex)
            {
                case 0:
                    game.ChangeGameState(Game1.GameState.Playing);
                    AudioManager.Instance.SetSong(SongList.Dungeon);
                    break;
                case 1:
                    game.ChangeGameState(Game1.GameState.Options);
                    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    {
                        game._currentGameState = Game1.GameState.StartMenu;
                    }
                    break;
                case 2:
                    game.ChangeGameState(Game1.GameState.Exiting);
                    break;
            }
        }
        _previousKeyboardState = currentKeyboardState; // Save state for next frame
    }

    public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
    {
        int screenWidth = graphicsDevice.Viewport.Width;
        int screenHeight = graphicsDevice.Viewport.Height;

        // Calculate scale factors
        float scaleX = (float)screenWidth / backgroundTexture.Width;
        float scaleY = (float)screenHeight / backgroundTexture.Height;

        // Draw scaled background
        spriteBatch.Draw(backgroundTexture,
            destinationRectangle: new Rectangle(0, 0, screenWidth, screenHeight),
            color: Color.White);

        // Then draw menu text
        for (int i = 0; i < menuItems.Length; i++)
        {
            Color color = (i == _selectedIndex) ? Color.Yellow : Color.White;
            spriteBatch.DrawString(font, menuItems[i], new Vector2(300, 360 + i * 40), color);
        }

    }

    private bool WasKeyPressed(Keys key, KeyboardState currentState)
    {
        return currentState.IsKeyDown(key) && _previousKeyboardState.IsKeyUp(key);
    }
}