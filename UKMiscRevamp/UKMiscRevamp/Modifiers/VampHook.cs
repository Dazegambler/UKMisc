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
        void Start()
        {

        }

        private void Update()
        {
            //var eid = MonoSingleton<HookArm>.Instance?.GetPrivate<HookArm>("CaughtEid");
            //switch (mov)
            //{
            //    case null:
            //        break;
            //    default:
            //        foreach (GameObject obj in Enemies)
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
            //                                switch (eid.enemyType)
            //                                {
            //                                    case EnemyType.Filth:
            //                                        var Filhp = obj.GetComponent<Zombie>();
            //                                        if (Filhp.health > 0.1f) Filhp.health -= Filhp.health * 0.03f;
            //                                        break;
            //                                    case EnemyType.Stray:
            //                                        var Strhp = obj.GetComponent<Zombie>();
            //                                        if (Strhp.health > 0.1f) Strhp.health -= Strhp.health * 0.0250f;
            //                                        break;
            //                                    case EnemyType.Schism:
            //                                        var Schhp = obj.GetComponent<Zombie>();
            //                                        if (Schhp.health > 0.1f) Schhp.health -= Schhp.health * 0.0250f;
            //                                        break;
            //                                    case EnemyType.Soldier:
            //                                        var Solhp = obj.GetComponent<Zombie>();
            //                                        if (Solhp.health > 0.1f) Solhp.health -= Solhp.health * 0.0250f;
            //                                        break;
            //                                    case EnemyType.Stalker:
            //                                        var Stalhp = obj.GetComponent<Machine>();
            //                                        if (Stalhp.health > 0.1f) Stalhp.health -= Stalhp.health * 0.0250f;
            //                                        break;
            //                                    case EnemyType.Sisyphus:
            //                                        var Sisyhp = obj.GetComponent<Machine>();
            //                                        if (Sisyhp.health > 0.1f) Sisyhp.health -= Sisyhp.health * 0.005f;
            //                                        break;
            //                                    case EnemyType.Swordsmachine:
            //                                        var Swohp = obj.GetComponent<Machine>();
            //                                        if (Swohp.health > 0.1f) Swohp.health -= Swohp.health * 0.0065f;
            //                                        break;
            //                                    case EnemyType.Drone:
            //                                        var Drohp = obj.GetComponent<Drone>();
            //                                        if (Drohp.health > 0.1f) Drohp.health -= Drohp.health * 0.03f;
            //                                        break;
            //                                    case EnemyType.Streetcleaner:
            //                                        var Strehp = obj.GetComponent<Machine>();
            //                                        if (Strehp.health > 0.1f) Strehp.health -= Strehp.health * 0.0095f;
            //                                        break;
            //                                    case EnemyType.Mindflayer:
            //                                        var Mndhp = obj.GetComponent<Machine>();
            //                                        if (Mndhp.health > 0.1f) Mndhp.health -= Mndhp.health * 0.01f;
            //                                        break;
            //                                    case EnemyType.MaliciousFace:
            //                                        var Malhp = obj.GetComponentInChildren<SpiderBody>();
            //                                        if (Malhp.health > 0.1f) Malhp.health -= Malhp.health * 0.0065f;
            //                                        break;
            //                                    case EnemyType.Cerberus:
            //                                        var Cerhp = obj.GetComponentInChildren<Statue>();
            //                                        if (Cerhp.health > 0.1f) Cerhp.health -= Cerhp.health * 0.0065f;
            //                                        break;
            //                                    case EnemyType.HideousMass:
            //                                        var Mashp = obj.GetComponent<Statue>();
            //                                        if (Mashp.health > 0.1f) Mashp.health -= Mashp.health * 0.0075f;
            //                                        break;
            //                                    case EnemyType.Virtue:
            //                                        var Virhp = obj.GetComponent<Drone>();
            //                                        if (Virhp.health > 0.1f) Virhp.health -= Virhp.health * 0.01f;
            //                                        break;
            //                                    default:
            //                                        break;
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
            //                foreach (GameObject _obj in Enemies)
            //                {
            //                    Enemies.Remove(obj);
            //                }
            //            }
            //            catch (NullReferenceException)
            //            {
            //                Debug.LogWarning("INVALID OBJECT FOUND...Deleting Array");
            //                foreach (GameObject _obj in Enemies)
            //                {
            //                    Enemies.Remove(obj);
            //                }
            //            }
            //        }
            //        break;
            //}
        }
    }
}
