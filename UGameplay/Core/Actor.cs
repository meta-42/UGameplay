using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DamageParams
{
    public Vector3 impulseDirection;
    public float impulseForce;
    public float impulseUpForce;
    public float damage;
}

namespace UGameplay
{
    //游戏中存在的实体
    public class Actor : MonoBehaviour
    {
        //受到伤害会被调用
        public virtual void TakeDamage(DamageParams inParams, GameObject causer)
        {

        }

        public void BroadcastPropertyChange(string propertyName, object newValue)
        {
            NotificationCenter.Instance.PostNotification(noticeTag + propertyName, this, newValue);
        }

        public void ReceivePropertyChange(string propertyName, Actor actor, System.Action<object, object> handle)
        {
            NotificationCenter.Instance.AddObserver(handle, actor.noticeTag + propertyName, this);
        }

        public string noticeTag
        {
            get { return GetInstanceID().ToString() + "_"; }
        }

    }

}
