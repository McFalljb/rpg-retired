using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    [CreateAssetMenu(menuName = "Conditions/MonitorSpellInput")]
    public class MonitorSpellInput : Condition
    {
        public StateActions attackResponse;
        public InputButtonVariable inpButtonVariable;

        private void OnEnable()
        {
            description = "Monitors spell Inputs";
        }

        public override bool CheckCondition(StatesManager state)
        {
           /* if (state.rb || state.lb || state.rt || state.lt)
            {
                inpButtonVariable.Set(state);
                attackResponse.Execute(state);
                return true;
            }*/
            return false;
        }

    }
}
