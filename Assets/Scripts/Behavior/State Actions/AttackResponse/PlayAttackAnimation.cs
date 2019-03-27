using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
	[CreateAssetMenu(menuName = "Actions/State Actions/Attacks/Attack Response")]
	public class PlayAttackAnimation : StateActions
	{
		public InputButtonVariable inpButtonVariable;
        public Condition isParryEnemy;

        public State waitForAnimationState;

		public override void Execute(StatesManager states)
		{	
				switch (inpButtonVariable.value)
				{
					case StatesManager.InputButton.rb:
                    if (isParryEnemy)
                    {
                        bool success = isParryEnemy.CheckCondition(states);
                        if (success)
                        {
                            states.PlayAnimation("parry_attack");
                            return;
                        }
                    }
						states.PlayAnimation("oh_attack_1");

						break;
					case StatesManager.InputButton.rt:
						states.PlayAnimation("oh_attack_2");

						break;
					case StatesManager.InputButton.lb:
						states.PlayAnimation("oh_attack_3");

						break;
					case StatesManager.InputButton.lt:
						states.PlayAnimation("oh_attack_3");

						break;
					default:
						break;
				}
		}

	}
}
