using Godot;
using Misc;

public partial class Main : Node2D
{
    private Enemy _enemy;

    /* private CollisionShape2D _northWall,
        _eastWall,
        _southWall,
        _westWall; */

    public Main() { }

    public override void _Ready()
    {
        base._Ready();

        _enemy = GetNode<Enemy>("Enemy");

        _enemy.Detection.BodyEntered += OnBodyEntered;

        // StaticBody2D staticBody = GetNode<Node2D>("Node2D").GetNode<StaticBody2D>("Wall");

        // _northWall = staticBody.GetNode<CollisionShape2D>("North");
        // _southWall = staticBody.GetNode<CollisionShape2D>("South");
        // _eastWall = staticBody.GetNode<CollisionShape2D>("East");
        // _westWall = staticBody.GetNode<CollisionShape2D>("West");
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
    }

    private void OnBodyEntered(Node2D body)
    {
        GD.Print("Body Name ->" + body.Name);
        if (body.Name.Equals("Wall"))
        {
            // Get opposite direction of current movement
            Direction oppositeDir = _enemy.GetOppositeDirection(_enemy.CurrentDirection);

            // Pick new direction that's not the opposite (to avoid immediate re-collision)
            _enemy.PickNewValidDirection(oppositeDir);
        }
    }
}
