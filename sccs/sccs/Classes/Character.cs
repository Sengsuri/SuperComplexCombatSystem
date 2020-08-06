using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using sccs.Engines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace sccs
{
    /// <summary>
    /// This is a template for characters
    /// Characters define animations, combos, speed, stamina, etc
    /// </summary>
    public abstract class Character
    {
        public Dictionary<string, Animation> animations;

        public Texture2D texture;

        public Vector2 handPosition;///the position where weapons will be

        public Vector2 shoulderPosition;///where the arm will pivot

        public AnimationEngine animationEngine;

        float staminaTimer;

        float manaTimer;

        float subtractTimer;

        const int regenRate = 1;

        public Action Attack;
        //TODO: make a list of special attacks        


        //TODO: make a way to draw the status effects
        public List<StatusEffect> statusEffects = new List<StatusEffect>();



        ElementEngine elementEngine;

        #region Properties
        public bool isDead { get; set; }
        public abstract int speed { get; set; }
        public abstract int maxSpeed { get; set; }
        public float stamina { get; set; }
        public abstract float maxStamina { get; }
        /// <summary>
        /// The rate of stamina regenerated
        /// </summary>
        public abstract float regenStamina { get; set; }
        public abstract float maxMana { get; }
        public float mana { get; set; }
        public abstract float regenMana { get; set; }
        public abstract float maxHealth { get; set; }
        public float health { get; set; }
        public abstract string description { get; }
        public abstract ElementEngine.Elements element { get; }
        //this formula will have to be tweaked over time
        public float powerPoints { get { return (maxHealth) + (maxStamina * regenStamina) + (maxMana * regenMana) + (maxSpeed); } }
        #endregion

        public Character(AnimationEngine animationEngine, ElementEngine elementEngine)
        {
            isDead = false;
            this.animationEngine = animationEngine;
            this.elementEngine = elementEngine;
            health = maxHealth;
            stamina = maxStamina;
            mana = maxMana;
        }

        public abstract void LoadTextures(ContentManager content);

        public void Update(GameTime gameTime)
        {
            //Miiiiiiiigghht need to change this because this will more than likely cause problems
            for (int i = 0; i <= statusEffects.Count; i++)
            {
                StatusEffect status = statusEffects[i];
                status.Update(gameTime);
                if (!status.active)
                {
                    statusEffects.RemoveAt(i);
                    //TODO: undo the status
                    sortStatusEffects();
                }
            }

            Regen(gameTime);
        }

        private void sortStatusEffects()
        {
            statusEffects.Sort((x, y) => x.timeLeft.CompareTo(y.timeLeft));

            statusEffects.Reverse();

            foreach (StatusEffect status in statusEffects)
            {
                status.ApplyStatus(this);
            }
        }


        public virtual void SubtractHealth(int damage)
        {
            ///you see, I could add logic that would make it so that health won't go below zero, but it would also be kind of funny to 
            ///see how far negative it goes, like an attack is so powerful it goes beyond killing you lol
            health -= damage;
            if (health - damage <= 0)
            {
                isDead = true;
            }
        }
        public virtual void SubtractHealth(int damage, ElementEngine.Elements element, StatusEffect statusEffect)
        {

            health -= damage * elementEngine.getDamageMod(this.element, element);
            if (health - damage <= 0)
            {
                isDead = true;
            }

            if (statusEffect != null)
            {
                statusEffect.ApplyStatus(this);
                statusEffects.Add(statusEffect);
            }
        }

        public virtual void AddHealth(int heal)
        {
            health = (health + heal > maxHealth) ? maxHealth : health + heal;
        }

        public virtual void SubtractStamina(int value, GameTime gameTime)
        {
            subtractTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (subtractTimer > 1 / value)
            {
                if (stamina > 0)
                    stamina -= 1 + regenRate;
                else
                    stamina = 0;
                subtractTimer = 0;
            }

        }
        /// <summary>
        /// regenerate both mana and stamina
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Regen(GameTime gameTime)
        {
            ///Regenerate stamina
            staminaTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (staminaTimer > 1 / regenStamina)
            {
                stamina = (stamina + regenRate < maxStamina) ? stamina + regenRate : maxStamina;
                staminaTimer = 0;
            }

            ///regenerate mana
            manaTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (manaTimer > 1 / regenMana)
            {
                mana = (mana + regenRate < maxMana) ? mana + regenRate : maxMana;
                manaTimer = 0;
            }

        }


        #region MovementAndActions
        /// <summary>
        /// All characters must have at least an idle animation 
        /// </summary>
        public abstract void Idle();

        public virtual void WalkLeft()
        {

        }

        public virtual void WalkRight()
        {

        }

        public virtual void WalkUp()
        {

        }

        public virtual void WalkDown()
        {

        }

        public virtual void Die() /// me irl lol
        {

        }

        public virtual void BasicAttack()
        {

        }

        public virtual void SpecialAttack()
        {

        }

        public virtual void UseItem()
        {

        }

        #endregion
    }
}
