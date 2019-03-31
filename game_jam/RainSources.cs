using Godot;
using System;
using System.Collections.Generic;

public class RainSources : Node2D
{

    private static readonly PackedScene rainDropScene = (PackedScene)ResourceLoader.Load("res://Raindrop.tscn");

    private class RainSource
    {

        private Node2D source;
        private Vector2 primaryDirection;
        private float directionRange;

        public RainSource(Node2D source, Vector2 primaryDirection, float directionRange)
        {

            this.source = source;
            this.primaryDirection = primaryDirection;
            this.directionRange = directionRange;

        }

        public void LaunchRain(Vector2 direction)
        {

            Raindrop t = (Raindrop) rainDropScene.Instance();
            source.AddChild(t);
            t.Velocity = primaryDirection;

        }

    }

    private List<RainSource> sources = new List<RainSource>();

    public override void _Ready()
    {

        sources.Add(new RainSource((Node2D)FindNode("RainSource1"), new Vector2(0, 1), 0));

        sources[0].LaunchRain(new Vector2(0, 1));
        
    }

    public override void _Process(float delta)
    {


        
    }

}
