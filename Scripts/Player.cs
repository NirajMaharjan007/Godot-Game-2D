using Godot;
using MyGame.Misc;

public partial class Player : CharacterBody2D
{
    private Label _label;
    private const int SPEED = 2;

    private Area2D _area2D;
    private CollisionShape2D _collisionShape2D;

    private ProgressBar _progressBar;

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

    private AnimatedSprite2D spirte;

    public Vector2 HitBox
    {
        get => _collisionShape2D.Position;
    }

    public override void _Ready()
    {
        base._Ready();

        _progressBar = GetNode<ProgressBar>("Stamina");
        _progressBar.Value = 100;
        _progressBar.Hide();

        spirte = GetNode<AnimatedSprite2D>("AnimatedCharacter2D");

        _area2D = GetNode<Area2D>("Area2D");
        _collisionShape2D = _area2D.GetNode<CollisionShape2D>("CollisionShape2D");

        _label = GetNode<Label>("Label");
        _label.Text = _progressBar.ToString();
    }

    private void Update()
    {
        string anim = "";
        switch (_direction)
        {
            case Direction.Down:
                _collisionShape2D.Position = new(0, 24);

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
                _collisionShape2D.Position = new(0, -32);

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
                _collisionShape2D.Position = new(-24, 0);

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
                _collisionShape2D.Position = new(24, 0);

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
        // GD.Print(anim);
        spirte.Play(anim);
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        double stamina = _progressBar.Value;

        if (Input.IsActionJustPressed("Run"))
            _run = !_run;

        if (_run)
        {
            stamina--;
            _progressBar.Show();
        }

        if (stamina == 0)
        {
            _run = false;
        }

        if (!_run)
        {
            if (stamina >= 100)
            {
                stamina = 100;
                _progressBar.Hide();
            }
            else
                stamina++;
        }

        _progressBar.Value = stamina;
        _label.Text = _progressBar.Value.ToString();
        Update();
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        Vector2 direction = Vector2.Zero;

        if (Input.IsActionPressed("ui_right"))
        {
            _direction = Direction.Right;
            if (_run)
            {
                _state = State.Running;
                direction.X = 4;
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
            if (_run)
            {
                _state = State.Running;
                direction.X = -4;
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
            if (_run)
            {
                direction.Y = 4;
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
            if (_run)
            {
                _state = State.Running;
                direction.Y = -4;
            }
            else
            {
                direction.Y = -1;
                _state = State.Walk;
            }
        }
        else if (Input.IsActionPressed("Attack"))
        {
            _state = State.Attacking;
        }
        else
        {
            direction = Vector2.Zero;
            _state = State.Idle;
            _run = false;
        }

        Velocity = direction * SPEED;
        // GD.Print(Velocity);
        MoveAndCollide(Velocity);
    }
}
