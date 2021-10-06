using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UKMiscRevamp.Modifiers
{
    class FrictionMod : MonoSingleton<FrictionMod>
    {
        public bool
            Active;

        NewMovement
            mov;
        void Start()
        {
            mov = NewMovement.Instance;
        }
        
        void Update()
        {
            if (Active)
            {
                mov.jumping = true;
            }
        }
    }
}
