using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurboPong
{
    internal class Delay
    {
        public double Timer = 0.0;
        public double DelayTime = 0.0;

        public Delay(double DelayTime)
        {
            this.DelayTime = DelayTime;
        }

        public void Wait(GameTime gt, Action Action)
        {
            if (this.Timer <= gt.TotalGameTime.TotalMilliseconds)
            {
                Timer = gt.TotalGameTime.TotalMilliseconds + DelayTime;
                Action.Invoke();
            }
        }
    }
}
