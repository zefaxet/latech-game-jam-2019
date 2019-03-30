using Godot;
using System;

namespace GameJam {

    public class Itsy : AnimatedSprite
    {

        private Vector2 positionBuffer;
        private bool physicsProcessFlipFlop = true;
        private bool live = true; //TODO move to game starting
        private double liveTime = 0.0;

        public override void _Ready()
        {

            Global.Unpause();
            positionBuffer = GetPosition();
        
        }

        public override void _Process(float delta)
        {

            if (live)
            {

                liveTime += delta;
                ((Label)GetNode(new NodePath("/root/Stage/GUI/Label"))).Text = liveTime.ToString();

                Vector2 lastPosition = GetPosition();
                Vector2 newPosition = new Vector2(positionBuffer);

                if (newPosition == lastPosition)
                {
                    return;
                }

                float newDirection = lastPosition.AngleToPoint(newPosition);
                newDirection = Mathf.Min((newDirection + 0.05F) * 0.9F, newDirection); //Play with this, possible average over 3 frames or something

                SetPosition(newPosition);
                SetRotation(-newDirection);

            }

        }

        public override void _PhysicsProcess(float delta)
        {

            physicsProcessFlipFlop ^= true;
            if (physicsProcessFlipFlop)
            {

                base._PhysicsProcess(delta);
                positionBuffer = GetViewport().GetMousePosition();

            }

        }

    }

}