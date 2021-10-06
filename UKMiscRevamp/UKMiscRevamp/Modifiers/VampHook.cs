using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UKMiscRevamp.Modifiers
{
    class VampHook : MonoSingleton<VampHook>
    {
        public bool
            Active;

        NewMovement
            mov;

        HookArm
            hk;

        EnemyIdentifier
            eid;

        void Start()
        {
            mov = NewMovement.Instance;
            hk = HookArm.Instance;
        }

        private void Update()
        {
            if (Active)
            {
                eid = hk.GetPrivate<EnemyIdentifier>("caughtEid");
                if(eid != null)
                {
                    if(mov.hp < 200)
                    {
                        mov.hp++;
                    }
                    if (eid.health > .1f)
                    {
                        switch (eid.enemyType)
                        {
                            case EnemyType.Filth:
                                var Filhp = eid.gameObject.GetComponent<Zombie>();
                                if (Filhp.health > 0.1f) Filhp.health -= Filhp.health * 0.03f;
                                break;
                            case EnemyType.Stray:
                                var Strhp = eid.gameObject.GetComponent<Zombie>();
                                if (Strhp.health > 0.1f) Strhp.health -= Strhp.health * 0.0250f;
                                break;
                            case EnemyType.Schism:
                                var Schhp = eid.gameObject.GetComponent<Zombie>();
                                if (Schhp.health > 0.1f) Schhp.health -= Schhp.health * 0.0250f;
                                break;
                            case EnemyType.Soldier:
                                var Solhp = eid.gameObject.GetComponent<Zombie>();
                                if (Solhp.health > 0.1f) Solhp.health -= Solhp.health * 0.0250f;
                                break;
                            case EnemyType.Stalker:
                                var Stalhp = eid.gameObject.GetComponent<Machine>();
                                if (Stalhp.health > 0.1f) Stalhp.health -= Stalhp.health * 0.0250f;
                                break;
                            case EnemyType.Sisyphus:
                                var Sisyhp = eid.gameObject.GetComponent<Machine>();
                                if (Sisyhp.health > 0.1f) Sisyhp.health -= Sisyhp.health * 0.005f;
                                break;
                            case EnemyType.Swordsmachine:
                                var Swohp = eid.gameObject.GetComponent<Machine>();
                                if (Swohp.health > 0.1f) Swohp.health -= Swohp.health * 0.0065f;
                                break;
                            case EnemyType.Drone:
                                var Drohp = eid.gameObject.GetComponent<Drone>();
                                if (Drohp.health > 0.1f) Drohp.health -= Drohp.health * 0.03f;
                                break;
                            case EnemyType.Streetcleaner:
                                var Strehp = eid.gameObject.GetComponent<Machine>();
                                if (Strehp.health > 0.1f) Strehp.health -= Strehp.health * 0.0095f;
                                break;
                            case EnemyType.Mindflayer:
                                var Mndhp = eid.gameObject.GetComponent<Machine>();
                                if (Mndhp.health > 0.1f) Mndhp.health -= Mndhp.health * 0.01f;
                                break;
                            case EnemyType.MaliciousFace:
                                var Malhp = eid.gameObject.GetComponentInChildren<SpiderBody>();
                                if (Malhp.health > 0.1f) Malhp.health -= Malhp.health * 0.0065f;
                                break;
                            case EnemyType.Cerberus:
                                var Cerhp = eid.gameObject.GetComponentInChildren<Statue>();
                                if (Cerhp.health > 0.1f) Cerhp.health -= Cerhp.health * 0.0065f;
                                break;
                            case EnemyType.HideousMass:
                                var Mashp = eid.gameObject.GetComponent<Statue>();
                                if (Mashp.health > 0.1f) Mashp.health -= Mashp.health * 0.0075f;
                                break;
                            case EnemyType.Virtue:
                                var Virhp = eid.gameObject.GetComponent<Drone>();
                                if (Virhp.health > 0.1f) Virhp.health -= Virhp.health * 0.01f;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            //var eid = MonoSingleton<HookArm>.Instance?.GetPrivate<HookArm>("CaughtEid");
            //switch (mov)
            //{
            //    case null:
            //        break;
            //    default:
            //        foreach (Gameeid.gameObjectect eid.gameObject in Enemies)
            //        {
            //            try
            //            {
            //                switch (eid.dead)
            //                {
            //                    case false:
            //                        switch (eid.hooked)
            //                        {
            //                            case true:
            //                                if (eid.health > 0.1f)
            //                                {
            //                                    if (mov.hp < 200) mov.hp++;
            //                                }
            //                                break;
            //                            default:
            //                                break;
            //                        }
            //                        break;
            //                    case true:
            //                        break;
            //                }
            //            }
            //            catch (InvalidOperationException)
            //            {
            //                Debug.LogWarning("ERROR IN ARRAY FOUND...Deleting Array");
            //                foreach (Gameeid.gameObjectect _eid.gameObject in Enemies)
            //                {
            //                    Enemies.Remove(eid.gameObject);
            //                }
            //            }
            //            catch (NullReferenceException)
            //            {
            //                Debug.LogWarning("INVALID eid.gameObjectECT FOUND...Deleting Array");
            //                foreach (Gameeid.gameObjectect _eid.gameObject in Enemies)
            //                {
            //                    Enemies.Remove(eid.gameObject);
            //                }
            //            }
            //        }
            //        break;
            //}
        }
    }
}
