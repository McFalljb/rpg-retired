using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    [CreateAssetMenu(menuName ="Actions/Item Actions/Attack Action")]
    public class AttackItemAction : ItemActions
    {
        public string targetAnim;

        public override void Execute(StatesManager states)
        {
            states.PlayAnimation(targetAnim);  
            states.currentState = targetState;
        }

        public override void OnHit(StatesManager owner, StatesManager defender)
        {
            if(itemLogic != null)
            {
                itemLogic.Execute(defender);
            }
        }
    }
}
