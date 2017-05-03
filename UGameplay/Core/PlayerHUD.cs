using UnityEngine;
using System.Collections;

namespace UGameplay
{
    public class PlayerHUD : MonoBehaviour
    {
        public Canvas canvas;

        public void ReceivePropertyChange(string propertyName, Actor actor, System.Action<object, object> handle)
        {
            NotificationCenter.Instance.AddObserver(handle, actor.noticeTag + propertyName, this);
        }
    }

}

