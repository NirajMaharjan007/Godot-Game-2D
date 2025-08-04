using System;
using Godot;
using Misc;

public partial class Enemy : CharacterBody2D
{
    private const int SPEED = 150;

    private State _state = State.Idle;
    private Direction _direction = Direction.Down;

    private AnimatedSprite2D spirte;

    // private CollisionShape2D _collisionShape2D;

    // public Vector2 HitBox
    // {
    //     get => _collisionShape2D.Position;
    // }

    public override void _Ready()
    {
        base._Ready();

        spirte = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        // _collisionShape2D = GetNode<CollisionShape2D>("CollisionShape2D");
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        string anim = "";
        switch (_direction)
        {
            case Direction.Down:
                if (_state.Equals(State.Idle))
                    anim = "down_idle";
                else if (_state.Equals(State.Walk))
                    anim = "down_walk";
                else if (_state.Equals(State.Running))
                    anim = "down_run";
                else if (_state.Equals(State.Attacking))
                    anim = "down_attack";
                break;

            case Direction.Up:
                if (_state.Equals(State.Idle))
                    anim = "up_idle";
                else if (_state.Equals(State.Walk))
                    anim = "up_walk";
                else if (_state.Equals(State.Running))
                    anim = "up_run";
                else if (_state.Equals(State.Attacking))
                    anim = "up_attack";
                break;

            case Direction.Left:
                if (_state.Equals(State.Idle))
                    anim = "left_idle";
                else if (_state.Equals(State.Walk))
                    anim = "left_walk";
                else if (_state.Equals(State.Running))
                    anim = "left_run";
                else if (_state.Equals(State.Attacking))
                    anim = "left_attack";
                break;

            case Direction.Right:
                if (_state.Equals(State.Idle))
                    anim = "right_idle";
                else if (_state.Equals(State.Walk))
                    anim = "right_walk";
                else if (_state.Equals(State.Running))
                    anim = "right_run";
                else if (_state.Equals(State.Attacking))
                    anim = "right_attack";
                break;
        }
        GD.Print("Enemy->" + anim);
        spirte.Play(anim);

        DirectionFlow();
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        if (Velocity.IsZeroApprox())
            _state = State.Idle;
        else
        {
            _state = State.Walk;
            if (Velocity.X > 0)
                _direction = Direction.Right;
            else if (Velocity.X < 0)
                _direction = Direction.Left;
            else if (Velocity.Y > 0)
                _direction = Direction.Down;
            else if (Velocity.Y < 0)
                _direction = Direction.Up;
        }

        GD.Print(_state.ToString() + "\t" + _direction.ToString());
        MoveAndSlide();
    }

    private void DirectionFlow()
    {
        Vector2 direction = Vector2.Zero;
        int x = GD.RandRange(-1, 1);
        int y = GD.RandRange(-1, 1);
    }
}
