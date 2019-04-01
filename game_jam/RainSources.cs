using Godot;
using System;
using System.Collections.Generic;

namespace GameJam
{

    public class RainSources : Node2D
    {

        private const int MAX_CONCURRENT_SOURCES = 18;

        public static readonly Random random = new Random();
        private static readonly PackedScene rainDropScene = (PackedScene)ResourceLoader.Load("res://Raindrop.tscn");

        private int activeSources = 0;

        private class RainSource
        {

            private const float SHOT_INTERVAL = 0.25F;

            public Node2D source;
            private Vector2 primaryDirection;
            private float directionRange;
            private float remainingShots;
            private float cooldown = 0;

            public bool isActive = false;

            public RainSource(Node2D source, Vector2 primaryDirection, float directionRange)
            {

                this.source = source;
                this.primaryDirection = primaryDirection;
                this.directionRange = directionRange;

            }

            public void StartVolley(int numberOfShots)
            {

                isActive = true;
                remainingShots = numberOfShots;
                LaunchRain(primaryDirection, true);
                cooldown = 1;

            }

            public bool UpdateSource(float delta)
            {

                if (cooldown == 0 && remainingShots > 0)
                {

                    LaunchRain(primaryDirection);
                    remainingShots--;
                    cooldown = SHOT_INTERVAL;
                    if (remainingShots == 0)
                    {
                        isActive = false;
                        return true;
                    }

                }
                else
                {

                    cooldown = Mathf.Max(0, cooldown - delta);

                }

                return false;

            }

            public void LaunchRain(Vector2 direction, bool wind = false)
            {

                Raindrop t = (Raindrop)rainDropScene.Instance();
                t.wind = wind;
                source.AddChild(t);
                t.Velocity = primaryDirection * 15;
                t.Velocity = t.Velocity.Rotated(Mathf.Deg2Rad(random.Next((int)directionRange) * new int[] { -1, 1}[random.Next(2)]));

                if (wind)
                {

                    t.Rotation = t.Velocity.Angle();

                }

            }

        }

        private List<RainSource> sources = new List<RainSource>();

        public override void _Ready()
        {

            sources.Add(new RainSource((Node2D)FindNode("RainSourceTop1"), new Vector2(0, 1), 5));
            sources.Add(new RainSource((Node2D)FindNode("RainSourceTop2"), new Vector2(0, 1), 5));
            sources.Add(new RainSource((Node2D)FindNode("RainSourceTop3"), new Vector2(0, 1), 5));
            sources.Add(new RainSource((Node2D)FindNode("RainSourceTop4"), new Vector2(0, 1), 5));
            sources.Add(new RainSource((Node2D)FindNode("RainSourceTop5"), new Vector2(0, 1), 5));
            sources.Add(new RainSource((Node2D)FindNode("RainSourceTop6"), new Vector2(0, 1), 5));
            sources.Add(new RainSource((Node2D)FindNode("RainSourceTop7"), new Vector2(0, 1), 5));
            sources.Add(new RainSource((Node2D)FindNode("RainSourceTop8"), new Vector2(0, 1), 5));
            sources.Add(new RainSource((Node2D)FindNode("RainSourceTop9"), new Vector2(0, 1), 5));
            sources.Add(new RainSource((Node2D)FindNode("RainSourceTop10"), new Vector2(0, 1), 5));
            sources.Add(new RainSource((Node2D)FindNode("RainSourceTop11"), new Vector2(0, 1), 5));
            sources.Add(new RainSource((Node2D)FindNode("RainSourceTop12"), new Vector2(0, 1), 5));
            sources.Add(new RainSource((Node2D)FindNode("RainSourceTop13"), new Vector2(0, 1), 5));
            sources.Add(new RainSource((Node2D)FindNode("RainSourceTop14"), new Vector2(0, 1), 5));
            sources.Add(new RainSource((Node2D)FindNode("RainSourceBottom1"), new Vector2(0, -1), 5));
            sources.Add(new RainSource((Node2D)FindNode("RainSourceBottom2"), new Vector2(0, -1), 5));
            sources.Add(new RainSource((Node2D)FindNode("RainSourceBottom3"), new Vector2(0, -1), 5));
            sources.Add(new RainSource((Node2D)FindNode("RainSourceBottom4"), new Vector2(0, -1), 5));
            sources.Add(new RainSource((Node2D)FindNode("RainSourceBottom5"), new Vector2(0, -1), 5));
            sources.Add(new RainSource((Node2D)FindNode("RainSourceBottom6"), new Vector2(0, -1), 5));
            sources.Add(new RainSource((Node2D)FindNode("RainSourceBottom7"), new Vector2(0, -1), 5));
            sources.Add(new RainSource((Node2D)FindNode("RainSourceBottom8"), new Vector2(0, -1), 5));
            sources.Add(new RainSource((Node2D)FindNode("RainSourceBottom9"), new Vector2(0, -1), 5));
            sources.Add(new RainSource((Node2D)FindNode("RainSourceBottom10"), new Vector2(0, -1), 5));
            sources.Add(new RainSource((Node2D)FindNode("RainSourceBottom11"), new Vector2(0, -1), 5));
            sources.Add(new RainSource((Node2D)FindNode("RainSourceBottom12"), new Vector2(0, -1), 5));
            sources.Add(new RainSource((Node2D)FindNode("RainSourceBottom13"), new Vector2(0, -1), 5));
            sources.Add(new RainSource((Node2D)FindNode("RainSourceBottom14"), new Vector2(0, -1), 5));
            sources.Add(new RainSource((Node2D)FindNode("RainSourceLeft1"), new Vector2(1, 0), 5));
            sources.Add(new RainSource((Node2D)FindNode("RainSourceLeft2"), new Vector2(1, 0), 5));
            sources.Add(new RainSource((Node2D)FindNode("RainSourceLeft3"), new Vector2(1, 0), 5));
            sources.Add(new RainSource((Node2D)FindNode("RainSourceLeft4"), new Vector2(1, 0), 5));
            sources.Add(new RainSource((Node2D)FindNode("RainSourceLeft5"), new Vector2(1, 0), 5));
            sources.Add(new RainSource((Node2D)FindNode("RainSourceLeft6"), new Vector2(1, 0), 5));
            sources.Add(new RainSource((Node2D)FindNode("RainSourceLeft7"), new Vector2(1, 0), 5));
            sources.Add(new RainSource((Node2D)FindNode("RainSourceLeft8"), new Vector2(1, 0), 5));
            sources.Add(new RainSource((Node2D)FindNode("RainSourceRight1"), new Vector2(-1, 0), 5));
            sources.Add(new RainSource((Node2D)FindNode("RainSourceRight2"), new Vector2(-1, 0), 5));
            sources.Add(new RainSource((Node2D)FindNode("RainSourceRight3"), new Vector2(-1, 0), 5));
            sources.Add(new RainSource((Node2D)FindNode("RainSourceRight4"), new Vector2(-1, 0), 5));
            sources.Add(new RainSource((Node2D)FindNode("RainSourceRight5"), new Vector2(-1, 0), 5));
            sources.Add(new RainSource((Node2D)FindNode("RainSourceRight6"), new Vector2(-1, 0), 5));
            sources.Add(new RainSource((Node2D)FindNode("RainSourceRight7"), new Vector2(-1, 0), 5));
            sources.Add(new RainSource((Node2D)FindNode("RainSourceRight8"), new Vector2(-1, 0), 5));

        }

        public override void _Process(float delta)
        {

            if (!Global.Paused)
            {

                if (activeSources < MAX_CONCURRENT_SOURCES)
                {

                    if (MAX_CONCURRENT_SOURCES - activeSources >= 14 && random.Next(0, 40) == 1)
                    {

                        string topOrBottom = new string[] { "Top", "Bottom" }[random.Next(2)];
                        sources.FindAll(s => s.source.Name.Contains(topOrBottom)).ForEach(s => s.StartVolley(random.Next(0, 10)));
                        activeSources += 14;
                        
                    }
                    else if (MAX_CONCURRENT_SOURCES - activeSources >= 8 && random.Next(0, 50) == 1)
                    {

                        string leftOrRight = new string[] { "Left", "Right" }[random.Next(2)];
                        sources.FindAll(s => s.source.Name.Contains(leftOrRight)).ForEach(s => s.StartVolley(random.Next(0, 10)));
                        activeSources += 8;

                    }
                    else if (random.Next(0, 20) == 5)
                    {
                        
                        int index = random.Next(0, sources.Count);
                        while (sources[index].isActive)
                        {

                            index = random.Next(0, sources.Count);

                        }

                        sources[index].StartVolley(random.Next(0, 20));
                        activeSources++;

                    }

                }

                sources.FindAll(s => s.isActive).FindAll(s => s.UpdateSource(delta)).ForEach(s => activeSources--);

            }

        }

    }

}