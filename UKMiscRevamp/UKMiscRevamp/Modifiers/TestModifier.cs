using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UKMiscRevamp.Modifiers
{
    class TestModifier : MonoSingleton<TestModifier>
    {
        public bool Active;
        void Start()
        {

        }
        void Update()
        {
            if (Active)
            {

            }
        }
    }
}
