using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.MonoActions
{
    public abstract class Action : ScriptableObject
    {
        public abstract void Execute();
    }
}
