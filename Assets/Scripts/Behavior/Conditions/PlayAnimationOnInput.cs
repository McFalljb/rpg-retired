using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
	[CreateAssetMenu(menuName = "Conditions/PlayAnimationOnInput")]
	public class PlayAnimationOnInput : Condition
	{
		public StatesManager.InputButton button;
		public string targetBool = "isInteracting";

		public override bool CheckCondition(StatesManager state)
		{
			bool retVal = false;

			switch (button)
			{
				case StatesManager.InputButton.rb:
					retVal = state.rb;
					break;
				case StatesManager.InputButton.rt:
					retVal = state.rt;
					break;
				case StatesManager.InputButton.lb:
					retVal = state.lb;
					break;
				case StatesManager.InputButton.lt:
					retVal = state.lt;
					break;
				default:
					break;
			}

			if (retVal)
			{
				state.anim.CrossFade("oh_attack_1",0.2f);
				state.anim.SetBool(targetBool, true);
			}

			return retVal;
		}

	}
}
