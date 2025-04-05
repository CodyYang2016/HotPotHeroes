using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using sprint0Test.Enemy;
using sprint0Test.Link1;
using System;
using System.Collections.Generic;

public class Darknut : AbstractEnemy
{
    private int direction = 1;
    private float moveTimer = 0;
    private float moveInterval = 3f;
    private bool isBlocking = false;
    private float attackCooldown = 2f;
    private float attackTimer = 0f;
    private int health = 3;

    public Darknut(Vector2 startPosition, Dictionary<string, Texture2D> Darknut_textures)
        : base(startPosition, new Texture2D[]
        {
            Darknut_textures["Darknut_Idle_Down_1"],
            Darknut_textures["Darknut_Idle_Down_2"],
            // Darknut_textures["Darknut_Idle_Up_1"],
            // Darknut_textures["Darknut_Idle_Side_1"],
            // Darknut_textures["Darknut_Idle_Side_2"]
        })
    {
    }


    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        attackTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

        moveTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (moveTimer >= moveInterval)
        {
            moveTimer = 0;
            direction *= -1; // Change direction
        }

        position.X += direction * 1.5f;

        // Block attacks if facing the player
        if (Vector2.Distance(position, Link.Instance.Position) < attackRange)
        {
            isBlocking = true;
            PerformAttack();
        }
        else
        {
            isBlocking = false;
        }
    }


    // take damage int 
    public override void TakeDamage(int damage)
    {
        if (isBlocking)
        {
            Console.WriteLine("Darknut blocked the attack!");
        }
        else
        {
            base.TakeDamage(damage);
        }
    }

    public override void PerformAttack()
    {
        if (attackTimer >= attackCooldown)
        {
            Console.WriteLine("Darknut slashes at Link!");
            Link.Instance.TakeDamage(); // Darknut does more damage
            attackTimer = 0f;
        }
    }
}
