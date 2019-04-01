using Godot;
using System;

namespace GameJam
{

    public class hourglasstoken : Area2D
    {

        public static readonly PackedScene hourglassScene = (PackedScene)ResourceLoader.Load("res://hourglasstoken.tscn");
        public static bool exists = false;
        public static hourglasstoken thisone;

        public override void _Ready()
        {



        }

        public override void _Process(float delta)
        {



        }

    }

}