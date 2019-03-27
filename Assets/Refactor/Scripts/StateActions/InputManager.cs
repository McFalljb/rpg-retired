using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace R2
{
    public class InputManager : StateAction
    {
        PlayerStateManager s;

        //Triggers & bumpers
        bool Rb, Rt, Lb, Lt, isAttacking, b_Input, y_Input, x_Input, inventoryInput,
        leftArrow, rightArrow, upArrow, downArrow;

        public InputManager(PlayerStateManager states)
        {
            s = states;
        }

        public override bool Execute()
        {
            bool retVal = false;
            isAttacking = false;

            s.horizontal = Input.GetAxis("Horizontal");
            s.vertical = Input.GetAxis("Vertical");
            Rb = Input.GetButton("RB");
            Rt = Input.GetButton("RT");
            Lb = Input.GetButton("LB");
            Lt = Input.GetButton("LT");
            inventoryInput = Input.GetButton("Inventory");
            b_Input = Input.GetButton("B");
            y_Input = Input.GetButtonDown("Y");
            x_Input = Input.GetButton("X");
           /* leftArrow = Input.GetButton("Left");
            rightArrow = Input.GetButton("Right");
            upArrow = Input.GetButton("Up");
            downArrow = Input.GetButton("Down");*/
            s.mouseX = Input.GetAxis("Mouse X");
            s.mouseY = Input.GetAxis("Mouse Y");

            s.moveAmount = Mathf.Clamp01(Mathf.Abs(s.horizontal) + Mathf.Abs(s.vertical));

            retVal = HandleAttacking();

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (s.lockOn)
                {
                    s.OnClearLookOverride();
                }
                else
                {
                    s.OnAssignLookOverride(s.target);
                }
            }


            return retVal;
        }

        bool HandleAttacking()
        {
            AttackInputs attackInput = AttackInputs.rt;

            if (Rb || Rt || Lb || Lt)
            {
                isAttacking = true;

                if (Rb)
                {
                    attackInput = AttackInputs.rb;
                }

                if (Rt)
                {
                    attackInput = AttackInputs.rt;
                }
                if (Lb)
                {
                    attackInput = AttackInputs.lb;
                }
                if (Lt)
                {
                    attackInput = AttackInputs.lt;
                }
            }



            if (y_Input)
            {
                isAttacking = false;
            }

            if (isAttacking)
            {
                //Find the actual attack animation from the items etc.
                //play animation
                s.PlayTargetItemAction(attackInput);
                s.ChangeState(s.attackStateId);
            }

            return isAttacking;
        }


    }
}
