using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UKMiscRevamp
{
    class SandMode : MonoBehaviour
    {
        private void Sand()
        {
            switch (Objects)
            {
                case null:
                    break;
                default:
                    foreach (GameObject Obj in Enemies)
                    {
                        try
                        {
                            var eid = Obj.GetComponent<EnemyIdentifier>();
                            switch (eid.sandified)
                            {
                                case true:
                                    break;
                                default:
                                    eid.Sandify();
                                    break;
                            }
                        }
                        catch (NullReferenceException)
                        {
                            Debug.LogWarning("INVALID OBJECT FOUND...Deleting Array");
                            foreach (GameObject _obj in Enemies)
                            {
                                Enemies.Remove(_obj);
                            }
                        }
                    }
                    break;
            }
        }
    }
}
