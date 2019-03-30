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

        public static bool Paused { get; private set; } = false;

        public static void Pause()
        {

            GD.Print("Paused...");
            Input.SetMouseMode(Input.MouseMode.Confined);
            Paused = true;

        }

        public static void Unpause()
        {

            GD.Print("Unpaused...");
            Input.SetMouseMode(Input.MouseMode.Confined);
            Paused = false;

        }

    }

}
