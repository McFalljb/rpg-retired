using UnityEngine;
using System.Collections;

namespace R2
{
	[CreateAssetMenu(menuName ="R2/Item Actions/Attack Action")]
	public class AttackAction : ItemAction
	{
		public override void ExecuteAction(ItemActionContainer ic,CharacterStateManager cs)
		{
			cs.PlayTargetAnimation(ic.animName, true, ic.isMirrored);
		}
	}
}
