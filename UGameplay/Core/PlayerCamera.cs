using System;
using UnityEngine;


namespace UGameplay
{
    public abstract class PlayerCamera : MonoBehaviour
    {
        public enum UpdateType // The available methods of updating are:
        {
            FixedUpdate, // Update in FixedUpdate (for tracking rigidbodies).
            LateUpdate, // Update in LateUpdate. (for tracking objects that are moved in Update)
            ManualUpdate, // user must call to update camera
        }
        public Transform essence;

        [SerializeField] protected Transform m_Target;            // The target object to follow
        [SerializeField] private bool m_AutoTargetPlayer = true;  // Whether the rig should automatically target the player.
        [SerializeField] private UpdateType m_UpdateType;         // stores the selected update type

        protected Rigidbody targetRigidbody;



        protected virtual void Start()
        {
            AutoTargetPlayer();
            if (m_Target == null) return;
            targetRigidbody = m_Target.GetComponent<Rigidbody>();
            essence.tag = "MainCamera";
        }


        private void FixedUpdate()
        {
            AutoTargetPlayer();
            if (m_UpdateType == UpdateType.FixedUpdate)
            {
                FollowTarget(Time.deltaTime);
            }
        }


        private void LateUpdate()
        {
            AutoTargetPlayer();
            if (m_UpdateType == UpdateType.LateUpdate)
            {
                FollowTarget(Time.deltaTime);
            }
        }


        public void ManualUpdate()
        {
            AutoTargetPlayer();
            if (m_UpdateType == UpdateType.ManualUpdate)
            {
                FollowTarget(Time.deltaTime);
            }
        }

        protected abstract void FollowTarget(float deltaTime);


        void AutoTargetPlayer()
        {
            if (m_AutoTargetPlayer && (m_Target == null || !m_Target.gameObject.activeSelf))
            {
                var targetObj = GameObject.FindGameObjectWithTag("Player");
                if (targetObj)
                {
                    SetTarget(targetObj.transform);
                }
            }
        }


        public virtual void SetTarget(Transform newTransform)
        {
            m_Target = newTransform;
        }


        public Transform Target
        {
            get { return m_Target; }
        }
    }
}
