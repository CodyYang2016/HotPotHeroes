
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using sprint0Test.Enemy;
using sprint0Test.Link1;
using System;
using System.Collections.Generic;

public class Keese : AbstractEnemy
{
    private Random random;
    private float moveTimer = 0;
    private float moveInterval = 1f;
    private Vector2 moveDirection;
    private bool isSwooping = false;
    private float attackCooldown = 3f;
    private float attackTimer = 0f;
    private int health = 1;

    public Keese(Vector2 startPosition, Dictionary<string, Texture2D> Keese_textures)
        : base(startPosition, new Texture2D[]
        {
            Keese_textures["Bat_1"],
            Keese_textures["Bat_2"]
        })
    {
        random = new Random();
        moveDirection = new Vector2(random.Next(-1, 2), random.Next(-1, 2));
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        attackTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

        moveTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (moveTimer >= moveInterval)
        {
            moveTimer = 0;
            moveDirection = new Vector2(random.Next(-1, 2), random.Next(-1, 2));

            if (random.NextDouble() < 0.3)
            {
                isSwooping = true;
            }
            else
            {
                isSwooping = false;
            }
        }

        if (isSwooping)
        {
            Vector2 direction = Vector2.Normalize(Link.Instance.Position - position);
            //Sprint5 Speed Adjustment
            position += direction * 1.0f;

            if (Vector2.Distance(position, Link.Instance.Position) < 0.2f && attackTimer >= attackCooldown)
            {
                PerformAttack();
                isSwooping = false;
                attackTimer = 0f;
            }
        }
        else
        {
            position += moveDirection * 1.2f;
        }
    }

    public override void PerformAttack()
    {
        Console.WriteLine("Keese swoops and bites Link!");
        Link.Instance.TakeDamage();
    }
}
