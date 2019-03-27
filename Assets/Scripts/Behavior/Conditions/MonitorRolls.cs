using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SA
{
    [CreateAssetMenu(menuName = "Conditions/Monitor Rolls")]
    public class MonitorRolls : Condition
    {
        public MonoActions.InputManager inpManager;
        float bTimer;

        public override bool CheckCondition(StatesManager state)
        {
            bool retval = false;

            if (inpManager.b_Input.isPressed)
            {
                bTimer += Time.deltaTime;

                if (bTimer > .5f)
                {
                    //sprint
                }
            }
            else
            {
                if (bTimer > 0)
                {
                    state.isBackstep = false;
                    retval = true;
                    state.generalT = 0;

                    if (state.moveAmount > 0)
                    {
                        state.anim.SetFloat("vertical", 1);
                        state.PlayAnimation("Rolls");
                        if (state.isLockingOn)
                        {
                            state.rollDirection = state.rotateDirection;
                        }
                        else
                        {
                            state.rollDirection = state.mTransform.forward;
                        }
                    }
                    else
                    {
                        state.isBackstep = true;
                        state.anim.SetFloat("vertical", 0);
                        state.PlayAnimation("step_back");
                        state.rollDirection = -state.mTransform.forward;
                    }
                }
                bTimer = 0;
            }

            return retval;
        }
    }
}
