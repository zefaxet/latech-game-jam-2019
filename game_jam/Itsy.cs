using Godot;
using System;
using System.Collections.Generic;

namespace GameJam {

    public class Itsy : Node2D
    {
        
        public static Area2D ITSY_AREA;
        public static Area2D LIGHTCONE_AREA;
        public static Itsy ITSY;

        private class Boot
        {

            private const float MOVE_SPEED = 0.6F;
            private const float RECOVER_SPEED = -1.3F;

            private readonly float threshold;
            private readonly float offset;
            private float current;
            private Sprite sprite;
            private bool recovering = false;

            public Boot(float threshold, float start, Sprite bootSprite)
            {

                this.threshold = threshold;
                sprite = bootSprite;

                current = start;
                offset = bootSprite.Position.y;

            }

            public void Move(float delta, bool moved)
            {

                current = (current + delta * (recovering ? RECOVER_SPEED : MOVE_SPEED));
                sprite.Position = new Vector2(sprite.Position.x, current + offset);

                if (!recovering && current >= threshold)
                {

                    recovering = true;

                }
                else if (recovering && current <= 0)
                {

                    recovering = false;

                }

            }

        }

        private List<Boot> boots = new List<Boot>();

        private Vector2 positionBuffer;
        private bool physicsProcessFlipFlop = true;
        private bool live = true; //TODO move to game starting
        private double liveTime = 0.0;
        private float invincibility = 0;
        private int HP = 3;

        private System.IO.StreamReader file;

        private AnimatedSprite hourglass;
        private float highscore;

        public override void _Ready()
        {

            string lineOfText;
            //READLINE
            var filestream = new System.IO.FileStream("C:\\Users\\Mr. Looks Delicious\\Desktop\\latech-game-jam-2019\\score.txt",
                                          System.IO.FileMode.Open,
                                          System.IO.FileAccess.Read,
                                          System.IO.FileShare.ReadWrite);
            file = new System.IO.StreamReader(filestream, System.Text.Encoding.UTF8, true, 128);

            lineOfText = file.ReadLine();

            ITSY_AREA = (Area2D) FindNode("Area");
            LIGHTCONE_AREA = (Area2D)FindNode("LightconeArea");
            ITSY = this;
            hourglass = (AnimatedSprite)FindNode("Hourglass");
            Global.stage = GetNode(new NodePath("/root/Stage"));

            positionBuffer = GetPosition();

            boots.Add(new Boot(20, 19, (Sprite)FindNode("FrontBootLeft")));
            boots.Add(new Boot(20, 6, (Sprite)FindNode("FrontBootRight")));
            boots.Add(new Boot(50, 18, (Sprite)FindNode("ReachBootLeft")));
            boots.Add(new Boot(50, 41, (Sprite)FindNode("ReachBootRight")));
            boots.Add(new Boot(40, 39, (Sprite)FindNode("SupportBootLeft")));
            boots.Add(new Boot(40, 20, (Sprite)FindNode("SupportBootRight")));
            boots.Add(new Boot(30, 8, (Sprite)FindNode("BackBootLeft")));
            boots.Add(new Boot(30, 24, (Sprite)FindNode("BackBootRight")));

            ((Label)GetNode(new NodePath("/root/Stage/GUI/Label"))).Text = liveTime.ToString();
            ((Label)GetNode(new NodePath("/root/Stage/GUI/Label4"))).Text = lineOfText;
            highscore = float.Parse(lineOfText);

        }

        public override void _Process(float delta)
        {

            if (!hourglasstoken.exists && RainSources.random.Next(0, 150) == 1)
            {

                hourglasstoken hourglass = (hourglasstoken)hourglasstoken.hourglassScene.Instance();
                hourglass.Position = new Vector2(RainSources.random.Next(100, 1500), RainSources.random.Next(100, 800));
                Global.stage.FindNode("HourGlass").AddChild(hourglass);
                hourglasstoken.exists = true;
                hourglasstoken.thisone = hourglass;

            }

            if (Global.Paused)
            {

                if(Input.IsActionJustPressed("ui_accept")) Global.Unpause();

            }
            else if (live && !Global.Paused)
            {

                liveTime += delta;
                ((Label)GetNode(new NodePath("/root/Stage/GUI/Label"))).Text = liveTime.ToString();

                Vector2 lastPosition = GetPosition();
                Vector2 newPosition = new Vector2(positionBuffer);

                bool moved = lastPosition.DistanceTo(newPosition) > 3;
                if (moved)
                {

                    float newDirection = lastPosition.AngleToPoint(newPosition);
                    newDirection = Mathf.Min((newDirection + 0.05F) * 0.9F, newDirection); //Play with this, possible average over 3 frames or something

                    SetPosition(newPosition);
                    SetRotation(-newDirection);
                    
                }

                boots.ForEach(b => b.Move(lastPosition.DistanceTo(newPosition), moved));

            }

            if (ITSY_AREA.GetOverlappingAreas().Contains(hourglasstoken.thisone))
            {
                Damage(-1);
                hourglasstoken.thisone.QueueFree();
                hourglasstoken.exists = false;
                invincibility = 2;

            }
            else
            {

                invincibility -= delta;
                invincibility = Mathf.Max(invincibility, 0);

            }

        }

        public override void _PhysicsProcess(float delta)
        {

            if (!Global.Paused)
            {

                base._PhysicsProcess(delta);
                positionBuffer = GetViewport().GetMousePosition();

            }

        }

        public void Damage(int amount = 1)
        {

            if (HP > 0 && invincibility == 0)
            {

                HP = Math.Min(HP - amount, 3);
                hourglass.Animation = HP.ToString();
                if (HP == 0)
                {
                    //Hide hourglass
                    //Game over
                    live = false;
                    GD.Print("Game Over");
                    Global.GameOver();
                    if (liveTime > highscore)
                    {
                        System.IO.File.WriteAllText("C:\\Users\\Mr. Looks Delicious\\Desktop\\latech-game-jam-2019\\score.txt", liveTime.ToString());
                    }

                }

            }

        }

    }

}