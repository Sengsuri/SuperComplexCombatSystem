﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sccs.Classes.characters
{
    class DefaultCharacter : Character
    {
        public override int speed { get { return 3; } set { _ = speed; } }
        public override int maxSpeed { get { return 6; } set { _ = maxSpeed; } }
        public override float maxStamina { get { return 100; } }
        public override float regenStamina { get { return 10; } set { _ = regenStamina; } }
        public override float maxMana { get { return 100; } }
        public override float regenMana { get { return 10; } set { _ = regenMana; } }
        public override float maxHealth { get { return 100; } set { _ = maxHealth; } }
        public override string description { get { return "like most game, there needs to be a base character. for the game, this is it"; } }
        public override ElementEngine.Elements element { get { return ElementEngine.Elements.NORMAL; } }


        public override Vector2 shoulderPivot => throw new NotImplementedException();
        public override Vector2 armPivot => throw new NotImplementedException();
        public override Vector2 handPoint => throw new NotImplementedException();

        public DefaultCharacter(AnimationEngine animationEngine, ElementEngine elementEngine)
            : base(animationEngine, elementEngine)
        { }


        public override void LoadTextures(ContentManager content)
        {
            try
            {
                animations = new Dictionary<string, Animation>()
            {
                {"WalkUp", new Animation(content.Load<Texture2D>("DefaultCharacter/Up"),1,1)},
                {"WalkDown", new Animation(content.Load<Texture2D>("DefaultCharacter/Down"),1,1)},
                {"WalkRight", new Animation(content.Load<Texture2D>("DefaultCharacter/Right"),1,1)},
                {"WalkLeft", new Animation(content.Load<Texture2D>("DefaultCharacter/Left"),1,1)}
            };
                armTexture = content.Load<Texture2D>("DefaultCharacter/arm");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }

        public override void WalkLeft()
        {
            animationEngine.Play(animations["WalkLeft"]);
        }
        public override void WalkRight()
        {
            animationEngine.Play(animations["WalkRight"]);
        }
        public override void WalkDown()
        {
            animationEngine.Play(animations["WalkDown"]);
        }
        public override void WalkUp()
        {
            animationEngine.Play(animations["WalkUp"]);
        }

        public override string ToString()
        {
            return "Default Character";
        }

        public override Animation startingAnimation()
        {
            return animations["WalkDown"];
        }
    }
}
