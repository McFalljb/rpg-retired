using UnityEngine;
using System.Collections;

namespace SA
{
	[CreateAssetMenu(menuName = "Actions/State Actions/MovementAnimationsOnLock")]
	public class MovementAnimationsOnLock : StateActions
	{
		public override void Execute(StatesManager states)
		{
			states.anim.SetFloat("horizontal", states.horizontal, 0.2f, states.delta);
			states.anim.SetFloat("vertical", states.vertical, 0.2f, states.delta);
		}
	}
}
