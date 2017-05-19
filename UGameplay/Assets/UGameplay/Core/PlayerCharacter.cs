using UnityEngine;
using System.Collections;

namespace UGameplay
{
    public class PlayerCharacter : Actor
    {
        public Transform essence;

        public PlayerController controller { set; get; }

        virtual public void PossessedBy(PlayerController inController)
        {
            controller = inController;
            essence.tag = "Player";
        }

        virtual public void UnPossessed()
        {
            controller = null;
            essence.tag = "Untagged";
        }
    }


}
