using UnityEngine;
using System.Collections;

namespace SA
{
    [CreateAssetMenu(menuName = "Items/Spell Action/Healing Spell")]
    public class HealingSpell : SpellAction
    {
        public int healAmount = 25;
        public float timer = 2;


        public string targetAnimation;

        public override void PrepareCast(StatesManager state, Spell spell)
        {
            state.PlayAnimation(targetAnimation, false);
            state.currentState = castingSpellState;

            state.rigidbody.velocity = Vector3.zero;
            state.rigidbody.isKinematic = true;
            state.forceEndActions = true;

            CreateSpellParticle(spell, state.mTransform);
        }

        public override bool Cast(StatesManager state, Spell spell)
        {
            bool r = false;

            state.generalT += state.delta;

            if (state.generalT > timer)
            {
                r = true;

                //		state.playerStatsManager.health.variable.Add(healAmount);

            }

            return r;
        }


    }
}