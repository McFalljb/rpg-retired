using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    [CreateAssetMenu(menuName = "Actions/State Actions/Movement Animations")]
    public class MovementAnimations : StateActions
    {
        public string floatName;

        public override void Execute(StatesManager states)
        {
            states.anim.SetFloat(floatName, states.moveAmount, .2f, states.delta);
        }
    }
}
