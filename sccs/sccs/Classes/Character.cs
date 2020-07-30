using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using sccs.Engines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public AnimationEngine animationEngine;

        float staminaTimer;

        float manaTimer;

        float subtractTimer;

        const int regenRate = 1;

        #region Properties
        public bool isDead { get; set; }
        public abstract int speed { get; }
        public abstract int maxSpeed { get; }
        public float stamina { get; set; }
        public abstract float maxStamina { get; }
        /// <summary>
        /// The amount of stamina regenerated every half second
        /// </summary>
        public abstract float regenStamina { get; }
        public abstract float maxMana { get; }
        public float mana { get; set; }

        public abstract float regenMana { get; }
        public abstract float maxHealth { get; }
        public float health { get; set; }

        //this formula will have to be tweaked over time
        public float powerPoints { get { return (maxHealth) + (maxStamina * regenStamina) + (maxMana * regenMana) + (maxSpeed); } }
        #endregion

        public Character(AnimationEngine animationEngine)
        {
            isDead = false;
            this.animationEngine = animationEngine;
        }

        public abstract void LoadTextures(ContentManager content);


        public virtual void SubtractHealth(int damage)
        {
            ///you see, I could add logic that would make it so that health won't go below zero, but it would also be kind of funny to 
            ///see how far negative it goes, like an attack is so powerful it kills you twice lol
            health -= damage;
            if (health - damage <= 0)
            {
                isDead = true;
            }
        }

        public virtual void AddHealth(int heal)
        {
            health = (health + heal > maxHealth) ? maxHealth : health + heal;
        }

        public virtual void SubtractStamina(int value, GameTime gameTime)
        {
            subtractTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (subtractTimer > 1/value)
            {
                if (stamina > 0)
                    stamina -= 1 + regenRate;
                else
                    stamina = 0;
                subtractTimer = 0;
            }

        }

        //TODO: regen variables should be based on time and not quantity. quantities will be based on a set value
        //use frequency formula to calculate the regen speed

        public virtual void Regen(GameTime gameTime)///regen both stamina and mana
        {

            RegenStamina(gameTime);

            //regenTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            //if (regenTimer > 0.05f)
            //{
            //    stamina = (stamina + regenStamina > maxStamina) ? maxStamina : stamina + regenMana;
            //    mana = (mana + regenMana > maxMana) ? maxMana : mana + regenMana;
            //    regenTimer = 0;
            //}
        }

        public virtual void RegenStamina(GameTime gameTime)
        {
            staminaTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (staminaTimer > 1 / regenStamina)
            {
                stamina = (stamina + regenRate < maxStamina) ? stamina + regenRate : maxStamina;
                staminaTimer = 0;
            }
        }

        public virtual void RegenMana(GameTime gameTime)
        {
            manaTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

        }



        #region Movement
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

        #endregion
    }
}
