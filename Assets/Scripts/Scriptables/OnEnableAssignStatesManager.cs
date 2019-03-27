using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public class OnEnableAssignStatesManager : MonoBehaviour
    {
        public StatesManagerVariable targetStatesManagerVariable;

        private void OnEnable()
        {
            targetStatesManagerVariable.value = GetComponent<StatesManager>();
        }
    }
}
