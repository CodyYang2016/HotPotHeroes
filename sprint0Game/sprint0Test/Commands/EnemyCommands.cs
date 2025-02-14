using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using sprint0Test.Enemy;
namespace sprint0Test.Commands
{
    internal class EnemyCommands;
    private EnemyManager enemyManager;

    public NextEnemyCommand(EnemyManager enemyManager)
    {
        this.enemyManager = enemyManager;
    }

    public void Execute()
    {
        enemyManager.NextEnemy();
    }
}

public class PreviousEnemyCommand : ICommand
{
    private EnemyManager enemyManager;

    public PreviousEnemyCommand(EnemyManager enemyManager)
    {
        this.enemyManager = enemyManager;
    }

    public void Execute()
    {
        enemyManager.PreviousEnemy();
    }
}
}



public EnemyManager EnemyManager { get; private set; }

protected override void LoadContent()
{
    List<Texture2D> enemyTextures = new List<Texture2D>
    {
        Content.Load<Texture2D>("enemy1"),
        Content.Load<Texture2D>("enemy2"),
        Content.Load<Texture2D>("enemy3")
    };

    EnemyManager = new EnemyManager(enemyTextures);
}

protected override void Update(GameTime gameTime)
{
    EnemyManager.Update(gameTime);
    base.Update(gameTime);
}

protected override void Draw(GameTime gameTime)
{
    spriteBatch.Begin();
    EnemyManager.Draw(spriteBatch);
    spriteBatch.End();
    base.Draw(gameTime);
}


