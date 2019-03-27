using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    interface IParryable 
    {
        void OnGettingParried();

        void isGettingParried(StatesManager states);
    }
}
