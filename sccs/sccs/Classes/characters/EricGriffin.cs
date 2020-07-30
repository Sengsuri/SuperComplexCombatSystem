using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sccs
{
    public class EricGriffin : Character
    {
        public override int speed { get { return 3; } }
        public override int maxSpeed { get { return 5; } }
        public override float maxStamina { get { return 100; } }
        public override float regenStamina { get { return 100f; } }
        public override float maxMana { get { return 100; } }
        public override float regenMana { get { return 1f; } }
        public override float maxHealth { get { return 100; } }


        public EricGriffin(AnimationEngine animationEngine) : base(animationEngine)
        {
            health = maxHealth;
            stamina = maxStamina;
            mana = maxMana;
        }

        public override void LoadTextures(ContentManager content)
        {
            animations = new Dictionary<string, Animation>()
            {
                {"Idle",new Animation (content.Load<Texture2D>("eric-griffin/eric-griffin-idle"),2 , new float[]{5f, .5f })},
                {"WalkUp",new Animation (content.Load<Texture2D>("eric-griffin/eric-griffin-walking-forward"), 4, .15f)},
                {"WalkDown",new Animation (content.Load<Texture2D>("eric-griffin/eric-griffin-walking-back"), 1, .15f)},
                {"WalkRight",new Animation (content.Load<Texture2D>("eric-griffin/eric-griffin-walking-right"), 4, .15f)},
                {"WalkLeft",new Animation (content.Load<Texture2D>("eric-griffin/eric-griffin-walking-left"), 4, .15f)}
            };
        }

        #region Movement
        public override void Idle()
        {
            animationEngine.Play(animations["Idle"]);
        }

        public override void WalkLeft()
        {
            base.WalkLeft();
            animationEngine.Play(animations["WalkLeft"]);
        }
        public override void WalkRight()
        {
            base.WalkRight();
            animationEngine.Play(animations["WalkRight"]);
        }
        public override void WalkDown()
        {
            base.WalkDown();
            animationEngine.Play(animations["WalkDown"]);
        }
        public override void WalkUp()
        {
            base.WalkUp();
            animationEngine.Play(animations["WalkUp"]);
        }
        #endregion
    }
}
