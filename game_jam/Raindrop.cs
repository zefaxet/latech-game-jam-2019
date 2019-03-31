using Godot;
using System;

public class Raindrop : Area2D
{
    
    public Vector2 Velocity { get; set; }

    public override void _Ready()
    {

        GD.Print("ready");
        
    }

    public override void _Process(float delta)
    {

        Position += Velocity;
        
    }

}
