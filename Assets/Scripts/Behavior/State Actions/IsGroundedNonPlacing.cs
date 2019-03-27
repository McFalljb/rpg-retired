using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
	[CreateAssetMenu(menuName = "Actions/State Actions/IsGroundedNonPlacing")]
	public class IsGroundedNonPlacing : StateActions
	{
		public float groundedDis = .8f;
		public float onAirDis = .85f;

		public override void Execute(StatesManager states)
		{
			Vector3 origin = states.mTransform.position;

			origin.y += .7f;
			Vector3 dir = -Vector3.up;
			float dis = groundedDis;
			if (!states.isGrounded)
				dis = onAirDis;

			RaycastHit hit;

			Debug.DrawRay(origin, dir * dis);
			if (Physics.SphereCast(origin, .3f, dir, out hit, dis, states.ignoreForGroundCheck))
			{
				states.isGrounded = true;
			}
			else
			{
				states.isGrounded = false;
			}
		}
	}
}