using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{

    [CreateAssetMenu(menuName = "Actions/State Actions/Disable Stun")]
    public class DisableStun : StateActions
    {
        public override void Execute(StatesManager states)
        {
            states.isStunned = false;
        }
    }
}
