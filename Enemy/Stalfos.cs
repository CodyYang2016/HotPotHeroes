
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using sprint0Test.Enemy;
using sprint0Test.Link1;
using System;
using System.Collections.Generic;

public class Stalfos : AbstractEnemy
{
    private Random random;
    private float moveTimer = 0;
    private float moveInterval = 2f;
    private Vector2 moveDirection;
    private float attackCooldown = 1.5f; // Attack cooldown in seconds
    private float attackTimer = 0f; // Timer to track attack cooldown
    private int health = 2;

    public Stalfos(Vector2 startPosition, Dictionary<string, Texture2D> Stalfos_textures)
        : base(startPosition, new Texture2D[] {
            Stalfos_textures["Skeleton"]
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
        }
        // Sprint5 Speed Adjustment
        position += moveDirection * 0.8f;

        // Attack if player is close and cooldown is over
        if (Vector2.Distance(position, Link.Instance.Position) < attackRange && attackTimer >= attackCooldown)
        {
            PerformAttack();
            attackTimer = 0f; // Reset cooldown
        }
    }

    public override void PerformAttack()
    {
        Console.WriteLine("Stalfos swings sword at Link!");
        Link.Instance.TakeDamage(); // Apply damage to the player character
    }

    public override Vector2 GetDimensions()
    {
        if (animationFrames.Length > 0 && animationFrames[0] != null)
        {
            return new Vector2(animationFrames[0].Width * scale, animationFrames[0].Height * scale);
        }
        return Vector2.Zero; // Return (0,0) if no texture is found
    }

}
