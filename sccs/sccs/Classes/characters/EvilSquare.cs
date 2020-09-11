using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using sccs.Engines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sccs
{
    public class EvilSquare : Character
    {
        public override int speed { get { return 1; } }

        public override int maxSpeed { get { return 5; } }

        public override float maxStamina { get { return 50; } }

        public override float regenStamina { get { return 1; } }

        public override float maxMana { get { return 50; } }

        public override float regenMana { get { return 1; } }

        public override float maxHealth { get { return 50; } }

        public override string description { get { return "It is evil because as a child it had been bullied for being a SQUARE."; } }

        public override ElementEngine.Elements element { get { return ElementEngine.Elements.DARK; } }

        public override Vector2 shoulderPivot => throw new NotImplementedException();

        public override Vector2 armPivot => throw new NotImplementedException();

        protected override Vector2 handPoint => throw new NotImplementedException();

        public EvilSquare(AnimationEngine animationEngine, ElementEngine elementEngine)
            : base(animationEngine, elementEngine)
        { }

        public override void LoadTextures(ContentManager content)
        {
            texture = content.Load<Texture2D>("evil-square");
        }

        public override Animation startingAnimation()
        {
            return null;
        }

        public override string ToString()
        {
            return "Evil Square";
        }
    }
}
