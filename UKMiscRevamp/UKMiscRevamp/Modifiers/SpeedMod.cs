﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UKMiscRevamp.Modifiers
{
    class SpeedMod : MonoSingleton<SpeedMod>
    {
        public float
            spd,
            jump;

        void Start()
        {
            MonoSingleton<NewMovement>.Instance.walkSpeed = spd;
            MonoSingleton<NewMovement>.Instance.jumpPower = jump;
            MonoSingleton<NewMovement>.Instance.wallJumpPower = jump + 60;
        }
    }
}
