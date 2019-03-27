using UnityEngine;
using System.Collections;

namespace SA
{
    [CreateAssetMenu(menuName = "Actions/State Actions/Change Weapons Prompt")]
    public class ChangeWeaponPrompt : StateActions
    {
        public ChangeWeapon changeWeapon;

        public SA.MonoActions.InputButton leftSlot;
        public SA.MonoActions.InputButton rightSlot;
        public SA.MonoActions.InputButton upSlot;
        public SA.MonoActions.InputButton downSlot;

        float l_timer;
        float u_timer;
        float d_timer;
        float r_timer;

        bool l_press;
        bool r_press;
        bool u_press;
        bool d_press;

        public override void Execute(StatesManager states)
        {
            leftSlot.Execute();
            rightSlot.Execute();

            bool goToFirst = false;

            if (CheckTimer(ref l_timer, ref l_press, leftSlot, states, ref goToFirst))
            {
                leftSlot.isPressed = false;
                //leftSlot.controllerAxis.isPressed = false;
                changeWeapon.Execute(states, true);
                return;
            }

            if (goToFirst)
            {
                leftSlot.isPressed = false;
                //leftSlot.controllerAxis.isPressed = false;
                states.inventory.SetFirstWeapon(true);
                changeWeapon.Execute(states, true);
                return;
            }

            if (CheckTimer(ref r_timer, ref r_press, rightSlot, states, ref goToFirst))
            {
                rightSlot.isPressed = false;
                //rightSlot.controllerAxis.isPressed = false;
                changeWeapon.Execute(states, false);
                return;
            }

            if (goToFirst)
            {
                rightSlot.isPressed = false;
                //rightSlot.controllerAxis.isPressed = false;
                states.inventory.SetFirstWeapon(false);
                changeWeapon.Execute(states, false);
                return;
            }

            //if (CheckTimer(ref u_timer, ref u_press, upSlot, states, ref goToFirst))
            //{
            //	upSlot.isPressed = false;
            //	upSlot.controllerAxis.isPressed = false;
            //	return;
            //}

            //if (goToFirst)
            //{
            //	//go to first spell
            //	return;
            //}

            //if (CheckTimer(ref d_timer, ref d_press, downSlot, states, ref goToFirst))
            //{
            //	downSlot.isPressed = false;
            //	downSlot.controllerAxis.isPressed = false;
            //	return;
            //}

            //if (goToFirst)
            //{
            //	//go to first item

            //}
        }

        bool CheckTimer(ref float timer, ref bool isPrevPressed, SA.MonoActions.InputButton b, StatesManager states, ref bool goToFirst)
        {
            bool result = false;

            if (b.isPressed)
            {
                isPrevPressed = true;
                timer += states.delta;
                if (timer > 1)
                {
                    timer = 0;
                    result = true;
                    isPrevPressed = false;
                    goToFirst = true;
                }
            }
            else
            {
                if (isPrevPressed)
                {
                    result = true;
                    isPrevPressed = false;
                }
                timer = 0;
            }

            return result;
        }
    }
}
