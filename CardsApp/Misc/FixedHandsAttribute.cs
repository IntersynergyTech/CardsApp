using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardsApp.Misc
{
    public class FixedHandsAttribute : Attribute
    {

        public FixedHandsAttribute()
        {
            PerPlayer = true;
        }

        public FixedHandsAttribute(bool perPlayer)
        {
            PerPlayer = perPlayer;
        }

        /// <summary>
        /// If the amount of hands is per player or not. Defaults to true.
        /// </summary>
        public bool PerPlayer { get; set; } = true;

    }
}
