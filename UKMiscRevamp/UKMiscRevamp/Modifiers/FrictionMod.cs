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
        void Start()
        {
        }
        
        void Update()
        {
            MonoSingleton<NewMovement>.Instance.jumping = true;
        }
    }
}
