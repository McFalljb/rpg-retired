using UnityEngine;
using System.Collections;

namespace SA
{

    public abstract class SpellAction : ScriptableObject
    {
        public State castingSpellState;

        public abstract void PrepareCast(StatesManager state, Spell spell);

        public abstract bool Cast(StatesManager state, Spell spell);

        public virtual void CreateSpellParticle(Spell spell, Transform parentTransform)
        {
            if (spell.spellParticle == null)
                return;

            GameObject go = Instantiate(spell.spellParticle) as GameObject;
            go.SetActive(false);
            go.transform.parent = parentTransform;
            go.transform.localPosition = Vector3.zero;
            go.transform.localEulerAngles = Vector3.zero;
            go.SetActive(true);
        }
    }
}