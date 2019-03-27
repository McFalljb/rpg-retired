using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    [CreateAssetMenu(menuName = "Actions/State Actions/Attacks/Attack Response Based on Items")]
    public class PlayItemAction : StateActions
    {
        public InputButtonVariable inpButtonVariable;
        public bool powerPosing;
        public Condition isParryEnemy;

        public override void Execute(StatesManager states)
        {
            if (states.isTwoHanded)
            {
                TwoHandedAction(states);
                return;
            }

            bool isMirror = false;

            ItemActions targetAction = null;

            switch (inpButtonVariable.value)
            {
                case StatesManager.InputButton.rb:
                case StatesManager.InputButton.rt:

                    if (isParryEnemy)
                    {
                        bool success = isParryEnemy.CheckCondition(states);
                        if (success)
                        {
                            states.PlayAnimation("parry_attack", false);
                            Debug.Log("f");
                            return;
                        }
                    }

                    if (states.inventory.rightHandWeapon !=states.inventory.unarmedWeapon)
                    {
                        isMirror = false;
                        targetAction = states.inventory.rightHandWeapon.GetItemActions(inpButtonVariable.value);
                    }
                    else
                    {
                        if (states.inventory.leftHandWeapon != states.inventory.unarmedWeapon)
                        {
                            isMirror = false;
                            StatesManager.InputButton newButton = (StatesManager.InputButton)((int)inpButtonVariable.value + 2);
                            targetAction = states.inventory.rightHandWeapon.GetItemActions(newButton);
                        }
                        else
                        {
                            isMirror = false;
                            targetAction = states.inventory.rightHandWeapon.GetItemActions(inpButtonVariable.value);
                        }

                    }
                    break;
                case StatesManager.InputButton.lb:
                case StatesManager.InputButton.lt:
                    if (states.inventory.leftHandWeapon == states.inventory.unarmedWeapon)
                    {
                        targetAction = states.inventory.leftHandWeapon.GetItemActions(inpButtonVariable.value);
                        isMirror = true;
                        //------------------------check this after play to see if left atacks no longer double mirror
                    }
                    else
                    {
                        if (powerPosing)
                        {
                            StatesManager.InputButton newButton = (StatesManager.InputButton)((int)inpButtonVariable.value - 2);

                            isMirror = true;
                            targetAction = states.inventory.leftHandWeapon.GetItemActions(newButton);
                        }
                        else
                        {
                            isMirror = true;
                            targetAction = states.inventory.leftHandWeapon.GetItemActions(inpButtonVariable.value);
                        }
                    }
                    break;
                default:
                    break;
            }

            states.inventory.currentItemAction = targetAction;

            states.anim.SetBool("mirror", isMirror);
            if (targetAction != null)
                targetAction.Execute(states);

        }


        void TwoHandedAction(StatesManager states)
        {
            Weapon w = states.inventory.twoHandedWeapon;
            ItemActions targetAction = null;
            targetAction = w.GetTHItemActions(inpButtonVariable.value);

            states.anim.SetBool("mirror", false);
            if (targetAction != null)
                targetAction.Execute(states);
        }
    }
}
