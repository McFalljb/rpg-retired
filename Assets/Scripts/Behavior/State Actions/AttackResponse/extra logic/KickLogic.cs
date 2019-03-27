using UnityEngine;
using System.Collections;

namespace SA
{
	[CreateAssetMenu(menuName = "Items/Item Logics/Kick Logic")]
	public class KickLogic : ItemLogic
	{
		public override void Execute(StatesManager s)
		{
			if (s.dontInterrupt)
				return;

			s.dontInterrupt = true;
			s.PlayAnimation("attack_interrupt");
		}
	}
}
