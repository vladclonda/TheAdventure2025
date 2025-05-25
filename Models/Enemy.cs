// In Enemy.cs
using Silk.NET.Maths;

namespace TheAdventure.Models;

public class Enemy : RenderableGameObject
{
    private const int Damage = 20;
    private const double AttackCooldown = 1.0;
    private double _timeSinceLastAttack;
    private readonly int _speed;
    public bool ShouldRemove { get; private set; }

    public Enemy(SpriteSheet spriteSheet, (int X, int Y) position) 
        : base(spriteSheet, position)
    {
        var random = new Random();
        _speed = random.Next(40, 80);
        SpriteSheet.ActivateAnimation("Walk");
    }

    public void Update(PlayerObject player, double deltaTime)
    {
        _timeSinceLastAttack += deltaTime;
        
        var enemyPos = new Vector2D<double>(Position.X, Position.Y);
        var playerPos = new Vector2D<double>(player.Position.X, player.Position.Y);
        
        var direction = playerPos - enemyPos;
        if (direction.Length > 0)
        {
            direction = Vector2D.Normalize(direction);
            
            var movement = direction * _speed * (deltaTime / 1000.0);
            Position = (
                (int)(Position.X + movement.X),
                (int)(Position.Y + movement.Y)
            );
        }

        var dx = player.Position.X - Position.X;
        var dy = player.Position.Y - Position.Y;
        var distance = Math.Sqrt(dx * dx + dy * dy);

        if (distance < 32 && _timeSinceLastAttack >= AttackCooldown)
        {
            player.TakeDamage(Damage);
            ShouldRemove = true; 
            _timeSinceLastAttack = 0;
        }
    }
}