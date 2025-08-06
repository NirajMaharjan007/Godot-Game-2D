using System;
using System.Collections.Generic;
using Godot;
using Misc;

public partial class Enemy : CharacterBody2D
{
    private const int SPEED = 150;

    private State _state = State.Idle;
    private Direction _direction = Direction.Down;

    private Area2D _area2D;
    private AnimatedSprite2D spirte;

    private Timer _timer;

    public Area2D Detection
    {
        get => _area2D;
    }

    // Pre-calculate direction vectors to avoid recreation each frame
    private static readonly Dictionary<Direction, Vector2> _directionVectors = new()
    {
        { Direction.Left, Vector2.Left },
        { Direction.Right, Vector2.Right },
        { Direction.Up, Vector2.Up },
        { Direction.Down, Vector2.Down },
    };

    public override void _Ready()
    {
        base._Ready();

        spirte = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        _area2D = GetNode<Area2D>("Area2D");
        _timer = GetNode<Timer>("DirectionTimer");
        _timer.Timeout += PickRandomDirection;
        _timer.Start();
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
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        Vector2 velocity = _directionVectors.GetValueOrDefault(_direction, Vector2.Zero);

        _state = velocity.IsZeroApprox() ? State.Idle : State.Walk;

        /* if (velocity.IsZeroApprox())
            _state = State.Idle;
        else
        {
            _state = State.Walk;
            if (velocity.X > 0)
                _direction = Direction.Right;
            else if (velocity.X < 0)
                _direction = Direction.Left;
            else if (velocity.Y > 0)
                _direction = Direction.Down;
            else if (velocity.Y < 0)
                _direction = Direction.Up;
        } */

        Velocity = velocity * SPEED;
        MoveAndSlide();

        GD.Print(_state.ToString() + "\t" + _direction.ToString());
    }

    private void PickRandomDirection()
    {
        // Use a single random call for both direction and idle chance
        float randomValue = GD.Randf();

        // 20% chance to be idle
        if (randomValue < 0.2f)
        {
            _direction = Direction.Down; // Better to use explicit None than -1
            return;
        }

        // Map the remaining 80% to 4 directions (20% each)
        // Using array lookup is faster than switch for small enums
        Direction[] directions = [Direction.Up, Direction.Down, Direction.Left, Direction.Right];

        _direction = directions[(int)((randomValue - 0.2f) * 5)]; // Scale to 0-3 index
    }
}
