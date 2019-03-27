using UnityEngine;
using System.Collections;

namespace SA
{
    [CreateAssetMenu(menuName = "Actions/Unique Actions/Change Weapons")]
    public class ChangeWeapon : ScriptableObject
    {
        public void Execute(StatesManager states, bool isLeft)
        {
            Weapon prevWeapon = (isLeft) ? states.inventory.leftHandWeapon : states.inventory.rightHandWeapon;

            if (prevWeapon)
            {
                prevWeapon.runtime.modelInstance.SetActive(false);
            }

            Weapon w = states.inventory.GetNextWeaponOnSlot(isLeft);
            if (w != null)
            {
                if (w.runtime == null)
                    w.Init();
                states.ParentWeaponUnderHand(states, w, (isLeft) ? HumanBodyBones.LeftHand : HumanBodyBones.RightHand);
                states.inventory.SetWeapon(w, isLeft, true);
                states.anim.SetBool("mirror", isLeft);
                states.anim.CrossFade("changeWeapon", 0.2f);
                if (isLeft)
                {
                    w.runtime.modelInstance.transform.localPosition = states.inventory.leftHandPosition.value;
                    w.runtime.modelInstance.transform.localEulerAngles = states.inventory.leftHandRotation.value;
                }
            }
        }
    }
}
