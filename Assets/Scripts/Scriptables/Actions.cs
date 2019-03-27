using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.Scriptable
{
    [System.Serializable]
    public class Actions
    {
        public ActionType actionType;
        public Object action_obj;
    }

    public enum ActionType
    {
        attack, block, spell, parry
    }
}