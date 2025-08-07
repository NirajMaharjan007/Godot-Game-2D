using System.Collections.Generic;
using Godot;
using MyGame.Misc;

public partial class Enemy : CharacterBody2D
{
    private const int SPEED = 150;

    private State _state = State.Idle;
    private Direction _direction = Direction.Down;

    private Area2D _area2D;
    private AnimatedSprite2D spirte;

    private Timer _timer,
        _idleTimer;

    private bool _idle = false;
    public Area2D Detection
    {
        get => _area2D;
    }

    public Direction CurrentDirection
    {
        get => _direction;
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
        _area2D.BodyEntered += OnBodyEntered;
        _area2D.Monitorable = true;
        _area2D.Monitoring = true;

        _timer = GetNode<Timer>("DirectionTimer");
        _timer.Timeout += PickRandomDirection;
        _timer.Start();

        _idleTimer = new();
        AddChild(_idleTimer);
        _idleTimer.WaitTime = 1.0f; // Idle duration
        _idleTimer.OneShot = true;
        _idleTimer.Timeout += () =>
        {
            PickRandomDirection();
            _idle = false;
        };
    }

    private void OnBodyEntered(Node2D body)
    {
        GD.Print(
            "Body Name -> "
                + body.Name
                + " ->"
                + body.Name.ToString().Equals("Wall")
                + body.Name.ToString().Equals("Wall2")
        );

        if (body.Name.ToString().Equals("Wall") || body.Name.ToString().Equals("Wall2"))
        {
            // Get current direction safely
            var currentDir = _direction;

            // Get opposite direction
            Direction oppositeDir = GetOppositeDirection(currentDir);
            _direction = oppositeDir;
            GD.Print(_direction);
        }
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
        GD.Print("Enemy->" + _idle);
        spirte.Play(anim);
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        Vector2 velocity = _directionVectors.GetValueOrDefault(_direction, Vector2.Zero);

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
        if (_idle)
        {
            Velocity = Vector2.Zero;
            _state = State.Idle;
        }
        else
        {
            _state = State.Walk;
            Velocity = velocity * SPEED;
            MoveAndSlide();
        }

        // GD.Print(_state.ToString() + "\t" + _direction.ToString());
    }

    private Direction GetOppositeDirection(Direction dir)
    {
        return dir switch
        {
            Direction.Left => Direction.Right,
            Direction.Right => Direction.Left,
            Direction.Up => Direction.Down,
            Direction.Down => Direction.Up,
            _ => dir, // fallback in case of unknown value
        };
    }

    private void PickRandomDirection()
    {
        _idle = true;
        _idleTimer.Start();

        Direction[] directions = [Direction.Up, Direction.Down, Direction.Left, Direction.Right];
        _direction = directions[GD.Randi() % directions.Length];
    }

    public override void _ExitTree()
    {
        if (_area2D != null)
        {
            _area2D.BodyEntered -= OnBodyEntered;
        }
        if (_timer != null)
        {
            _timer.Timeout -= PickRandomDirection;
        }
    }
}
