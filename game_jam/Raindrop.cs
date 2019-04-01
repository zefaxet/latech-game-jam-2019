using Godot;
using System;

namespace GameJam {

    public class Raindrop : Area2D
    {

        public Vector2 Velocity { get; set; }

        private bool active = true;
        public bool wind = false;

        private AnimatedSprite sprite;

        public override void _Ready()
        {

            sprite = (AnimatedSprite)FindNode("Sprite");

            if (wind)
            {

                sprite.Animation = "wind";

            }

        }

        public override void _Process(float delta)
        {

            if (active)
            {

                Godot.Array overlappingAreas = GetOverlappingAreas();
                if (overlappingAreas.Contains(Itsy.LIGHTCONE_AREA) && !wind)
                {

                    Position += Velocity * 0.05F;

                }
                else
                {

                    Position += Velocity;

                }
                if (overlappingAreas.Contains(Itsy.ITSY_AREA) && !wind)
                {

                    Collide();
                    Itsy.ITSY.Damage();

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