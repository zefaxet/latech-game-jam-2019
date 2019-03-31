using Godot;
using System;

namespace GameJam {

    public class Raindrop : Area2D
    {

        public Vector2 Velocity { get; set; }

        private bool active = true;

        public override void _Ready()
        {



        }

        public override void _Process(float delta)
        {

            if (active)
            {

                Position += Velocity;
                if (GetOverlappingAreas().Contains(Itsy.ITSY_AREA))
                {

                    GD.Print("FOUDN IT");
                    Collide();

                }

            }

        }

        private void Collide()
        {

            active = false;
            AnimatedSprite sprite = (AnimatedSprite) FindNode("Sprite");
            sprite.Animation = "collide";
            sprite.Playing = true;


        }

    }

}