using UnityEngine;
using System.Collections;

namespace UGameplay
{
    public class PlayerController : MonoBehaviour
    {
        private PlayerCharacter _character;
        public PlayerCharacter character
        {
            set
            {
                _character = value;
            }
            get
            {
                if (_character != null) return _character;
                else return null;
            }
        }

        virtual public void Possess(PlayerCharacter inPawn)
        {
            if (inPawn != null)
            {
                if (character && character == inPawn)
                {
                    UnPossess();
                }

                if (inPawn.controller != null)
                {
                    inPawn.controller.UnPossess();
                }

                inPawn.PossessedBy(this);

                character = inPawn;
            }
        }

        public void UnPossess()
        {
            if (character)
            {
                character.UnPossessed();
                character = null;
            }
        }

        void OnDestroy()
        {
            UnPossess();
        }
    }
}

