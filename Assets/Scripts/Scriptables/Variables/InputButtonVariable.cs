using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    [CreateAssetMenu(menuName = "Variables/IB Variable (Enum)")]
    public class InputButtonVariable : ScriptableObject
    {
        public StatesManager.InputButton value;
        
        public void Set(StatesManager states)
        {
            if (states.rb)
            {
                value = StatesManager.InputButton.rb;
            }
            if (states.lb)
            {
                value = StatesManager.InputButton.lb;
            }
            if (states.rt)
            {
                value = StatesManager.InputButton.rt;
            }
            if (states.lt)
            {
                value = StatesManager.InputButton.lt;
            }
        }
    }
}