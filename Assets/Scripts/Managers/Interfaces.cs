using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{

    public interface ILockable
    {
        Transform LockOn();
        
    }

    public interface IHittable
    {

        void OnHit(StatesManager hitter);
    }

}