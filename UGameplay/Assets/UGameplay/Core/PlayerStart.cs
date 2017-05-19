using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UGameplay
{
    public class PlayerStart : MonoBehaviour
    {
        public static PlayerStart DefaultStart()
        {
            PlayerStart ret = null;
            var go = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            ret = go.AddComponent<PlayerStart>();
            return ret;
        }
    }
}


