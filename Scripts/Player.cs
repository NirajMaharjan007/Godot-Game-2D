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

    private int _speed = 100;

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

        var velocity = Velocity;

        if (Input.IsActionPressed("ui_right"))
        {
            _direction = Direction.Right;
            if (Input.IsActionPressed("Run"))
            {
                _state = State.Running;
                velocity.X = _speed + 120;
            }
            else
            {
                _state = State.Walk;
                velocity.X = _speed;
            }
        }
        else if (Input.IsActionPressed("ui_left"))
        {
            _direction = Direction.Left;
            if (Input.IsActionPressed("Run"))
            {
                _state = State.Running;
                velocity.X = -_speed - 120;
            }
            else
            {
                _state = State.Walk;
                velocity.X = -_speed;
            }
        }
        else if (Input.IsActionPressed("ui_down"))
        {
            _direction = Direction.Down;
            if (Input.IsActionPressed("Run"))
            {
                velocity.Y = _speed + 120;
                _state = State.Running;
            }
            else
            {
                velocity.Y = _speed;
                _state = State.Walk;
            }
        }
        else if (Input.IsActionPressed("ui_up"))
        {
            _direction = Direction.Up;
            if (Input.IsActionPressed("Run"))
            {
                _state = State.Running;
                velocity.Y = -_speed - 120;
            }
            else
            {
                velocity.Y = -_speed;
                _state = State.Walk;
            }
        }
        else
        {
            velocity.X = Mathf.MoveToward(velocity.X, 0, _speed);
            velocity.Y = Mathf.MoveToward(velocity.Y, 0, _speed);
            _state = State.Idle;
        }

        Velocity = velocity;
        MoveAndSlide();
    }
}
