using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.MonoActions
{
    public class ActionHook : MonoBehaviour
    {
        public Action[] updateActions;
        public Action[] fixedUpdateActions;

        void Update()
        {
            for (int i = 0; i < updateActions.Length; i++)
            {
                updateActions[i].Execute();
            }
        }
        void FixedUpdate()
        {
            for (int i = 0; i < fixedUpdateActions.Length; i++)
            {
                fixedUpdateActions[i].Execute();
            }
        }

    }
}
