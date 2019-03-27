using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SA
{
    [CreateAssetMenu(menuName = "Items/Spell")]
    public class Spell : Item
    {
        public SpellAction spellAction;
        public GameObject spellParticle;
        public GameObject spellProjectile;


        public void PrepareCast(StatesManager states)
        {
            spellAction.PrepareCast(states, this);
            SetSpellToCast(states);
        }

        public void SetSpellToCast(StatesManager states)
        {
            states.currentSpell = this;
        }

    }
}
