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

        public List<Action> specialAttacks;

        ElementEngine elementEngine;

        //TODO:consider attack property? would make balancing weapons a bit easier

        #region Properties
        public bool isDead { get; set; }
        public abstract int speed { get; }
        public abstract int maxSpeed { get; }
        public float stamina { get; set; }
        public abstract float maxStamina { get; }
        /// <summary>
        /// The rate of stamina regenerated
        /// </summary>
        public abstract float regenStamina { get; }
        public abstract float maxMana { get; }
        public float mana { get; set; }
        public abstract float regenMana { get; }
        public abstract float maxHealth { get; }
        public float health { get; set; }
        public abstract string description { get; }
        public abstract ElementEngine.Elements element { get; }
        //this formula will have to be tweaked over time
        public float powerPoints { get { return (maxHealth) + (maxStamina * regenStamina) + (maxMana * regenMana) + (maxSpeed); } }

        //TODO: figure out how to do special attacks because OH GOD HOW AM I GONNA BALANCE THIS??????
        //as for the formula, it will be powerpoints - amount of special abilities^7/8. probs gonna change it cause I think it 
        //could be better and it's probably trash anyways

        #endregion

        //TODO: pass a reference to the entities timer list so the character class could use the timers as well

        public Texture2D armTexture;
        public abstract Vector2 shoulderPivot { get; }///where the shoulder pivots on the character
        public abstract Vector2 armPivot { get; }///the orgin of the arm
        protected abstract Vector2 handPoint { get; }///where the weaopons will be drawn, set by the modder. 
                                                     ///the arm should be facing down,
                                                     ///where the shoulder is above the hand 
        public Vector2 _handPoint { get { return shoulderPivot - handPoint; } }//TODO: figure this shit out 
        public float armRotation { get; set; }

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


        /// <summary>
        /// Update can be overrided so the chararcter can handle inputs for special attacks
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
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
        }

        [Obsolete]
        public virtual void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            if (armTexture != null)
            {
                spriteBatch.Draw(armTexture, shoulderPivot + position, rotation: armRotation, origin: armPivot);
            }

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
            if (subtractTimer < regenRate)
            {
                if (stamina > 0)
                    stamina -= 1 + value;
                else
                    stamina = 0;
                subtractTimer = 0;
            }

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
        public virtual void Idle() { }

        public virtual void WalkLeft() { }

        public virtual void WalkRight() { }

        public virtual void WalkUp() { }

        public virtual void WalkDown() { }

        public virtual void Die() /// me irl lol
        {
            ///i think this is a death animation???? might have additional funcionality idk
        }

        public virtual void BasicAttack(Action attack)
        {
            Attack = attack;

            Attack?.Invoke();
        }

        public virtual void SpecialAttack()
        {

        }

        public virtual void UseItem()
        {

        }

        #endregion

        public abstract Animation startingAnimation();
        public abstract override string ToString();
    }
}
