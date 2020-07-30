using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sccs.Engines
{
    /// <summary>
    /// How this element engine works is that it takes in the element from an attack and compares it to it's own
    /// it then outputs a damage modifier to the object
    /// damage modifier is damage done TO the defending object
    /// </summary>
    public class ElementEngine
    {
        public enum Elements
        {
            NORMAL, FIRE, WATER, METAL, EXPLOSION, ICE, WIND, EARTH, POISON, DARK, LIGHT, PLANT
        }

        public float getDamageMod(Elements DefElement, Elements AtkElement)
        {
            float damageMod = 1;

            switch (DefElement)
            {
                case Elements.NORMAL:
                    damageMod = Normal(AtkElement);
                    break;
                case Elements.FIRE:
                    damageMod = Fire(AtkElement);
                    break;
                case Elements.WATER:
                    damageMod = Water(AtkElement);
                    break;
                case Elements.METAL:
                    damageMod = Metal(AtkElement);
                    break;
                case Elements.EXPLOSION:
                    damageMod = Explosion(AtkElement);
                    break;
                case Elements.ICE:
                    damageMod = Ice(AtkElement);
                    break;
                case Elements.WIND:
                    damageMod = Wind(AtkElement);
                    break;
                case Elements.EARTH:
                    damageMod = Earth(AtkElement);
                    break;
                case Elements.POISON:
                    damageMod = Poision(AtkElement);
                    break;
                case Elements.DARK:
                    damageMod = Dark(AtkElement);
                    break;
                case Elements.LIGHT:
                    damageMod = Light(AtkElement);
                    break;
                case Elements.PLANT:
                    damageMod = Plant(AtkElement);
                    break;
            }
            return damageMod;
        }

        private float Normal(Elements element)
        {
            float damageMod = 1;

            switch (element)
            {
                case Elements.DARK:
                    damageMod = 1.5f;
                    break;
            }
            return damageMod;
        }
        private float Fire(Elements element)
        {
            float damageMod = 1;
            switch (element)
            {
                case Elements.WATER:
                    damageMod = 2;
                    break;
                case Elements.ICE:
                case Elements.PLANT:
                case Elements.EARTH:
                    damageMod = 0.5f;
                    break;
            }
            return damageMod;
        }
        private float Water(Elements element)
        {
            float damageMod = 1;

            switch (element)
            {
                case Elements.ICE:
                case Elements.PLANT:
                    damageMod = 1.5f;
                    break;

            }

            return damageMod;
        }
        private float Metal(Elements element)
        {
            float damageMod = 1;

            switch (element)
            {
                case Elements.POISON:
                    damageMod = 0.5f;
                    break;
                case Elements.EARTH:
                    damageMod = 1.5f;
                    break;
            }
            return damageMod;
        }
        private float Explosion(Elements element)
        {
            float damageMod = 1;

            return damageMod;
        }
        private float Ice(Elements element)
        {
            float damageMod = 1;

            return damageMod;
        }
        private float Wind(Elements element)
        {
            float damageMod = 1;

            return damageMod;
        }
        private float Earth(Elements element)
        {
            float damageMod = 1;

            return damageMod;
        }
        private float Poision(Elements element)
        {
            float damageMod = 1;

            return damageMod;
        }
        private float Dark(Elements element)
        {
            float damageMod = 1;

            return damageMod;
        }
        private float Light(Elements element)
        {
            float damageMod = 1;

            return damageMod;
        }
        private float Plant(Elements element)
        {
            float damageMod = 1;

            return damageMod;
        }
    }
}
