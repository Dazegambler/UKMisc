using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UKMiscRevamp
{
    class SpeedMod : MonoBehaviour
    {
        private void Speed(float spd, float jump)
        {
            MonoSingleton<NewMovement>.Instance.walkSpeed = spd;
            MonoSingleton<NewMovement>.Instance.jumpPower = jump;
            MonoSingleton<NewMovement>.Instance.wallJumpPower = jump + 60;
        }
    }
}
