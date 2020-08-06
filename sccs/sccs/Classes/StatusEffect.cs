using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sccs
{
    public class StatusEffect
    //TODO: be sure to take into affect items/weapons that add status effects when calulating power points
    {
        public enum status
        {
            Speed, Health, Stamina, Mana, Attack
        }

        public bool active;
        public float speedEffect { get; private set; }
        public float healthEffect { get; private set; }
        public float staminaEffect { get; private set; }
        public float manaEffect { get; private set; }
        public float attackEffect { get; private set; }
        public float cooldown { get; private set; }
        public float timeLeft { get { return cooldown - timer; } }
        public float speedRevert { get; private set; }
        public float healthRevert { get; private set; }///max health
        public float staminaRevert { get; private set; }
        public float manaRevert { get; private set; }

        public Action otherEffect;

        float timer;

        //TODO: figure out how to deal with this....
        Texture2D displayTexture;///the texture that displays when the effect is.... taking effect


        public StatusEffect(float cooldown, float speedEffect = 0, float healthEffect = 0, float staminaEffect = 0,
            float manaEffect = 0, float attackEffect = 0)
        {
            this.cooldown = cooldown;
            this.speedEffect = speedEffect;
            this.healthEffect = healthEffect;
            this.staminaEffect = staminaEffect;
            this.manaEffect = manaEffect;
            this.attackEffect = attackEffect;
        }

        public void ApplyStatus(Character character)
        {
            speedRevert = character.speed;
            healthRevert = character.maxHealth;
            staminaRevert = character.regenStamina;
            manaRevert = character.regenMana;

            character.speed += (int)speedEffect;
            character.maxHealth += healthEffect;
            character.regenStamina += staminaEffect;
            character.regenMana += manaEffect;

            active = true;
        }

        public virtual void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timer > cooldown)
            {
                active = false;
            }

            otherEffect?.Invoke();
        }

        public float RevertStatus(status status)
        {
            switch (status)
            {
                case status.Speed:
                    return speedRevert;
                case status.Health:
                    return healthRevert;
                case status.Stamina:
                    return staminaRevert;
                case status.Mana:
                    return manaRevert;
            }
            return 1f;
        }
    }
}
