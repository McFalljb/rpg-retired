using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    [CreateAssetMenu(menuName = "Actions/State Actions/Init Inventory")]
    public class InitInventory : StateActions
    {
        public ChangeWeapon changeWeaponAction;

        public override void Execute(StatesManager states)
        {
            if (states.profile == null)
                return;

            states.isPlayer = true;
            states.playerStatsManager = Resources.Load("Player Stats") as PlayerStatsManager;
            states.profile.SetStatsToStartingValue();
            ResourcesManager rm = GameManager.GetResourcesManager();

            states.inventory.unarmedWeapon.Init();
            states.inventory.unarmedWeapon.runtime.modelInstance.SetActive(false);

            for (int i = 0; i < states.profile.rightHandWeapon.Length; i++)
            {
                Item rh = rm.GetItemInstance(states.profile.rightHandWeapon[i]);
                if (rh != null)
                {
                    Weapon rhWeapon = (Weapon)rh;
                    if (i > states.inventory.rightHandSlots.Length - 1)
                        break;
                    states.inventory.rightHandSlots[i] = rhWeapon;
                    changeWeaponAction.Execute(states, false);
                }
            }

            for (int i = 0; i < states.profile.leftHandSlots.Length; i++)
            {
                Item lh = rm.GetItemInstance(states.profile.leftHandSlots[i]);
                if (lh != null)
                {
                    Weapon lhWeapon = (Weapon)lh;
                    if (i > states.inventory.leftHandSlots.Length - 1) 
                        break;
                        states.inventory.leftHandSlots[i] = lhWeapon;
                    changeWeaponAction.Execute(states, true);
                }
            }

            for (int i = 0; i < states.profile.spellId.Length; i++)
            {
                Item spell = rm.GetItemInstance(states.profile.spellId[i]);
                if (spell != null)
                {
                    Spell s = (Spell)spell;
                    states.inventory.SetSpell(s, true);
                }
            }

            states.character = states.activeModel.GetComponent<CharacterViz>();
            states.character.SetToDefaults();

            for (int i = 0; i < states.profile.wearedClothItems.Length; i++)
            {
                states.SetWearedCloth(states.profile.wearedClothItems[i], rm, states);
            }
        }


    }
}
