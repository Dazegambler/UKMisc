using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UKMiscRevamp.Modifiers
{
    class SandMode : MonoSingleton<SandMode>
    {

        public bool
            Active;
        void Start()
        {

        }
        private void Update()
        {
            if (Active)
            {
                foreach (EnemyIdentifier eid in FindObjectsOfType<EnemyIdentifier>())
                {
                    try
                    {
                        if (eid.sandified == false)
                        {
                            eid.Sandify();
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }
    }
}
