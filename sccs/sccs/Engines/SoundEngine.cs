using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sccs.Engines
{

    //the point of this engine is to play sounds based on their position and velocity, meaning you should hear bullets or cars flying past, or or explosions in the distance
    class SoundEngine
    {
        double baseDB;//the how loud the thing should be whenit is within proximinity of the player
        double DBRate;//rate db changes based on position
        double playSpeed;//the speed the sound should play

        //TODO create a method that changes the db of a sound file based on the position of an object
    }
}
