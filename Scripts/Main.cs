using System;
using Godot;

namespace MyGame.Scripts;

public partial class Main : Node2D
{
    private Player _player;
    private Enemy _enemy;

    public override void _Ready()
    {
        base._Ready();

        _player = GetNode<Player>("Player");
        _enemy = GetNode<Enemy>("Enemy");
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        CheckAttackBox();
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
    }

    private void CheckAttackBox()
    {
        bool flag =
            _player.AttackBox.Shape.Collide(
                _player.AttackBox.GlobalTransform,
                _enemy.HitBox.Shape,
                _enemy.HitBox.GlobalTransform
            ) && _player.IsAttack;

        _enemy.IsHurt = flag;

        bool collide = _enemy.AttackBox.Shape.Collide(
            _enemy.AttackBox.GlobalTransform,
            _player.HitBox.Shape,
            _player.HitBox.GlobalTransform
        );
        _enemy.IsCollide = collide;
        GD.Print("Collide " + collide);
    }
}
