﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sccs
{
    public class Weapon : Entity
    {
        public int damage;

        //temp constructor for creative purposes, might be permanent
        public Weapon(Texture2D texture, Vector2 startingPosition)
        {
            this.texture = texture;
            Position = startingPosition;
            Scale = 1;
        }

    }
}
