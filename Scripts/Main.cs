using Godot;

public partial class Main : Node2D
{
    private Enemy _enemy;

    private CollisionShape2D _northWall,
        _eastWall,
        _southWall,
        _westWall;

    public Main() { }

    public override void _Ready()
    {
        base._Ready();

        _enemy = GetNode<Enemy>("Enemy");

        StaticBody2D staticBody = GetNode<Node2D>("Node2D").GetNode<StaticBody2D>("Wall");

        _northWall = staticBody.GetNode<CollisionShape2D>("North");
        _southWall = staticBody.GetNode<CollisionShape2D>("South");
        _eastWall = staticBody.GetNode<CollisionShape2D>("East");
        _westWall = staticBody.GetNode<CollisionShape2D>("West");
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
    }
}
