using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sccs
{

    //for now, this implementation works and should work well. the possibility that further functionality being needed is unlikely
    public class Timer
    {
        Action Method;

        float timeElapsed;

        float seconds;

        public bool active { get; private set; }

        public bool isCooldown { get; private set; }
        ///Cooldown vs Countdown
        ///cooldown is where the timer does a method until the time elapsed
        ///countdown is when the method is invoked when the time elapses
        public bool timerDone { get; private set; }

        public Timer()
        {
            active = false;
        }

        public void SetCountDown(float seconds, Action Method)
        {
            this.seconds = seconds;
            this.Method = Method;
            active = true;
            timerDone = false;
            isCooldown = false;
        }

        public void SetCoolDown(float seconds, Action Method)
        {
            this.seconds = seconds;
            this.Method = Method;
            active = true;
            timerDone = false;
            isCooldown = true;
        }

        public void Update(GameTime gameTime)
        {            
            if (active)
            {
                if (isCooldown)
                {
                    if (seconds > timeElapsed)
                    {
                        Method?.Invoke();///if method is not null, do method
                    }
                    else
                    {
                        timeElapsed = 0;///reset timeElapsed after method call   
                        active = false;
                        timerDone = true;
                    }
                }
                else
                {
                    if (seconds < timeElapsed)
                    {
                        Method?.Invoke();///if method is not null, do method
                        timeElapsed = 0;///reset timeElapsed after method call
                        active = false;
                        timerDone = true;
                    }
                }
                timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }
    }
}
