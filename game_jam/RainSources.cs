using Godot;
using System;
using System.Collections.Generic;

namespace GameJam
{

    public class RainSources : Node2D
    {

        private static readonly Random random = new Random();
        private static readonly PackedScene rainDropScene = (PackedScene)ResourceLoader.Load("res://Raindrop.tscn");

        private class RainSource
        {

            private const float SHOT_INTERVAL = 0.5F;

            private Node2D source;
            private Vector2 primaryDirection;
            private float directionRange;
            private float remainingShots;
            private float cooldown = 0;

            public RainSource(Node2D source, Vector2 primaryDirection, float directionRange)
            {

                this.source = source;
                this.primaryDirection = primaryDirection;
                this.directionRange = directionRange;

            }

            public void StartVolley(int numberOfShots)
            {

                remainingShots = numberOfShots;

            }

            public void UpdateSource(float delta)
            {

                if (cooldown == 0 && remainingShots > 0)
                {

                    LaunchRain(primaryDirection);
                    remainingShots--;
                    cooldown = SHOT_INTERVAL;

                }
                else
                {

                    cooldown = Mathf.Max(0, cooldown - delta);

                }

            }

            public void LaunchRain(Vector2 direction)
            {

                Raindrop t = (Raindrop)rainDropScene.Instance();
                source.AddChild(t);
                t.Velocity = primaryDirection * 10;

            }

        }

        private List<RainSource> sources = new List<RainSource>();

        public override void _Ready()
        {

            sources.Add(new RainSource((Node2D)FindNode("RainSource1"), new Vector2(0, 1), 0));
            sources[0].StartVolley(5);

        }

        public override void _Process(float delta)
        {

            sources.ForEach(s => s.UpdateSource(delta));

        }

    }

}