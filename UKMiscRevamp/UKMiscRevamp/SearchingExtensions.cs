using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class SearchingExtensions
{
    public  static GameObject AssetFind(this GameObject Object, string name)
    {
        //Find set Object in the prefabs
        foreach (GameObject obj in Resources.FindObjectsOfTypeAll<GameObject>())
        {
            if (obj.gameObject.name == name)
            {
                Object = obj;
            }
        }
        return Object;
    }
}
