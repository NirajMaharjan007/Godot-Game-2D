using System;
using Godot;

public partial class Player : CharacterBody2D
{
    public enum State
    {
        Idle,
        Running,
        Attacking,
    }

    public enum Direction
    {
        Left,
        Right,
        Up,
        Down,
    }

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
