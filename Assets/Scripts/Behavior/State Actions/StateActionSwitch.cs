using UnityEngine;
using System.Collections;

namespace SA
{
	[CreateAssetMenu(menuName = "Actions/State Actions/State Action Switch")]
	public class StateActionSwitch : StateActions
	{
		public BoolVariable boolVariable;
		public StateActions stateActionTrue;
		public StateActions stateActionFalse;

		public override void Execute(StatesManager states)
		{
			if (boolVariable.value)
			{
				stateActionTrue.Execute(states);
			}
			else
			{
				stateActionFalse.Execute(states);
			}
		}
	}
}
