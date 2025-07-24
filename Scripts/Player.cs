using System;
using Godot;

public partial class Player : CharacterBody2D
{
    public enum State
    {
        Idle,
        Running,
        Walk,
        Attacking,
    }

    public enum Direction
    {
        Left,
        Right,
        Up,
        Down,
    }

    private int _speed = 128;

    /*  private bool _idle,
         _walk;
 
     public bool Idle
     {
         get => _idle;
         set => _idle = value;
     }
 
     public bool Walk
     {
         get => _walk;
         set => _walk = value;
     }
  */
    private State _state = State.Idle;
    private Direction _direction = Direction.Down;

    private bool _run = false;

    private int _runCounter = 0;

    private AnimatedSprite2D spirte;

    public override void _Ready()
    {
        base._Ready();

        spirte = GetNode<AnimatedSprite2D>("AnimatedCharacter2D");
    }

    private void Update()
    {
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
                break;

            case Direction.Up:
                if (_state.Equals(State.Idle))
                    anim = "up_idle";
                else if (_state.Equals(State.Walk))
                    anim = "up_walk";
                else if (_state.Equals(State.Running))
                    anim = "up_run";
                break;

            case Direction.Left:
                if (_state.Equals(State.Idle))
                    anim = "left_idle";
                else if (_state.Equals(State.Walk))
                    anim = "left_walk";
                else if (_state.Equals(State.Running))
                    anim = "left_run";
                break;

            case Direction.Right:
                if (_state.Equals(State.Idle))
                    anim = "right_idle";
                else if (_state.Equals(State.Walk))
                    anim = "right_walk";
                else if (_state.Equals(State.Running))
                    anim = "right_run";
                break;
        }

        spirte.Play(anim);
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        Update();
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        Vector2 direction = Vector2.Zero;

        if (Input.IsActionPressed("ui_right"))
        {
            _direction = Direction.Right;
            if (Input.IsActionPressed("Run"))
            {
                _state = State.Running;
                direction.X = 3;
            }
            else
            {
                _state = State.Walk;
                direction.X = 1;
            }
        }
        else if (Input.IsActionPressed("ui_left"))
        {
            _direction = Direction.Left;
            if (Input.IsActionPressed("Run"))
            {
                _state = State.Running;
                direction.X = -3;
            }
            else
            {
                _state = State.Walk;
                direction.X = -1;
            }
        }
        else if (Input.IsActionPressed("ui_down"))
        {
            _direction = Direction.Down;
            if (Input.IsActionPressed("Run"))
            {
                direction.Y = 3;
                _state = State.Running;
            }
            else
            {
                direction.Y = 1;
                _state = State.Walk;
            }
        }
        else if (Input.IsActionPressed("ui_up"))
        {
            _direction = Direction.Up;
            if (Input.IsActionPressed("Run"))
            {
                _state = State.Running;
                direction.Y = -3;
            }
            else
            {
                direction.Y = -1;
                _state = State.Walk;
            }
        }
        else
        {
            direction = Vector2.Zero;
            _state = State.Idle;
        }

        Velocity = direction * _speed;
        GD.Print(Velocity);
        MoveAndSlide();
    }
}
