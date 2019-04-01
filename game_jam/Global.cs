using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Godot;

namespace GameJam
{

    public class Global
    {

        public static Node stage;

        public static bool Paused { get; private set; } = true;

        public static void Pause()
        {

            GD.Print("Paused...");
            Input.SetMouseMode(Input.MouseMode.Confined);
            Paused = true;

        }

        public static void Unpause()
        {

            GD.Print("Unpaused...");
            ((AnimatedSprite)stage.FindNode("GameOver")).Animation = "default";
            stage.FindNode("Play").QueueFree();
            Input.SetMouseMode(Input.MouseMode.Confined);
            Paused = false;

        }

        public static void GameOver()
        {

            ((AnimatedSprite)stage.FindNode("GameOver")).Animation = new Random().Next(4).ToString();

        }

    }

}
