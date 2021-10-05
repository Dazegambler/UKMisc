using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UKMiscRevamp
{
    class FrictionMod : MonoBehaviour
    {
        void Awake()
        {
        }
        
        void Update()
        {
            MonoSingleton<NewMovement>.Instance.jumping = true;
        }
    }
}
