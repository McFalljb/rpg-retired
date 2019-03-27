using UnityEngine;
using System.Collections;

namespace SA
{
	public class DamageCollider : MonoBehaviour
	{
        public StatesManager ourStates;

		public new Collider collider;

		void Start()
		{
			this.gameObject.layer = 10;
			collider = GetComponent<Collider>();
            ourStates = GetComponentInParent<StatesManager>();
		}

		void OnTriggerEnter(Collider other)
		{
			StatesManager states = other.transform.GetComponentInParent<StatesManager>();
			if (states != null)
			{
                if (states != ourStates);
                {
                    states.OnHit(ourStates);
                }
			}
		}
	}
}
