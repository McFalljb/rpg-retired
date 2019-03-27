using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    [CreateAssetMenu (menuName = "Conditions/Check For Parry Enemy")]
    public class CheckForParryEnemy : Condition
    {
        public float originOffset = 1.4f;

        public override bool CheckCondition(StatesManager s)
        {
            bool result = false;

            RaycastHit hit;
            Vector3 origin = s.mTransform.position;
            origin.y += originOffset;
            Debug.DrawRay(origin, s.mTransform.forward, Color.red);
            if (Physics.SphereCast(origin, .3f, s.mTransform.forward, out hit, 1, s.ignoreLayers))
            {

                IParryable parryable = hit.transform.GetComponentInParent<IParryable>();
                if (parryable != null)
                {
                    parryable.isGettingParried(s);
                }
            }

            return result;
        }
    }
}