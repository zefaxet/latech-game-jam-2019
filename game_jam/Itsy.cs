using Godot;
using System;
using System.Collections.Generic;

namespace GameJam {

    public class Itsy : Node2D
    {

        public static Area2D ITSY_AREA;

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

        public override void _Ready()
        {

            ITSY_AREA = (Area2D) FindNode("Area");

            Global.Unpause();
            positionBuffer = GetPosition();

            boots.Add(new Boot(20, 19, (Sprite)FindNode("FrontBootLeft")));
            boots.Add(new Boot(20, 6, (Sprite)FindNode("FrontBootRight")));
            boots.Add(new Boot(50, 18, (Sprite)FindNode("ReachBootLeft")));
            boots.Add(new Boot(50, 41, (Sprite)FindNode("ReachBootRight")));
            boots.Add(new Boot(40, 39, (Sprite)FindNode("SupportBootLeft")));
            boots.Add(new Boot(40, 20, (Sprite)FindNode("SupportBootRight")));
            boots.Add(new Boot(30, 8, (Sprite)FindNode("BackBootLeft")));
            boots.Add(new Boot(30, 24, (Sprite)FindNode("BackBootRight")));
        
        }

        public override void _Process(float delta)
        {

            if (live)
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

        }

        public override void _PhysicsProcess(float delta)
        {

            base._PhysicsProcess(delta);
            positionBuffer = GetViewport().GetMousePosition();

        }

    }

}