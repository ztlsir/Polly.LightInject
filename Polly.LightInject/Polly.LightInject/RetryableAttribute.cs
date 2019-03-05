using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Polly.LightInject
{
    [AttributeUsage(AttributeTargets.Method)]
    public class RetryableAttribute : Attribute
    {
        public RetryableAttribute(int maxAttempts, int delay, int multiplier, params Type[] vlaue)
        {
            this.Vlaue = vlaue.ToList();
            this.MaxAttempts = maxAttempts;
            this.Delay = delay;
            this.Multiplier = multiplier;
        }

        public List<Type> Vlaue { get; set; }
        public int MaxAttempts { get; set; }
        public int Delay { get; set; }
        public int Multiplier { get; set; }
    }
}
