using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace sccs
{

    /// <summary>
    /// an input wrapper that also takes settings from the config file
    /// </summary>
    //TODO: have this class handle inputs so it simplifies code in other classes    
    public class Input
    {
        public Keys Left;
        public Keys Right;
        public Keys Up;
        public Keys Down;
        public Keys Primary;
        public Keys Action1;
        public Keys Action2;
        public Keys Dodge;
        public Keys Sprint;
        //TODO: add a COMBAT SHIFT key that adds extra macros, which could either make it so there's less buttons to press 
        //or make it possible to have about 180 special attacks on a full sized keyboard

        public Input()
        {
            assignKeys();
        }

        public void assignKeys()
        {
            Up = Keys.W;
            Left = Keys.A;
            Down = Keys.S;
            Right = Keys.D;
            Primary = Keys.A;
            Action1 = Keys.A;
            Action2 = Keys.A;
            Dodge = Keys.Space;
            Sprint = Keys.LeftShift;

            //TODO check if there is a config file, if not then assign default values to each key
        }



    }
}
