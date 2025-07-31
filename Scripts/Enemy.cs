using System;
using Godot;

public partial class Enemy : CharacterBody2D
{
    private const int SPEED = 150;

    public override void _Ready()
    {
        base._Ready();
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
    }
}
