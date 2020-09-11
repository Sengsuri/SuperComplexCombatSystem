using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sccs
{
    interface IEffect
    {
        //have the GameState load the necessary effects in load content, then have the entity/drawn item
        //call ApplyEffects() within the draw method
        //The state class loads in effects that are guranteed to be used (UI, particles, etc)
        //and then the class that's using a unique effect will load that 
        //effect on it's own with LoadContent(). It's done this way so that effects arent loaded
        //in twice
        Action ApplyEffects { get; set; }

        /// <summary>
        /// This method exists so that the effects can either be set within the class itself or 
        /// by another class(namely the state class)
        /// </summary>
        //void ApplyEffects(Action applyEffects);
    }
}
