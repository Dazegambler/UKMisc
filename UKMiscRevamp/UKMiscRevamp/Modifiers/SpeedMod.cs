using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UKMiscRevamp.Modifiers
{
    class SpeedMod : MonoSingleton<SpeedMod>
    {
        public bool
            Active;
        public float
            spd,
            jump;
        NewMovement
            mov;

        void Start()
        {
            spd = 750;
            jump = 90;
            mov = NewMovement.Instance;
        }
        void Update()
        {
            if (Active)
            {
                mov.walkSpeed = spd;
                mov.jumpPower = jump;
                mov.wallJumpPower = jump + 60;
            }
        }
    }
}
