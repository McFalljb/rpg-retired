using UnityEngine;
using System.Collections;

namespace R2
{
	[CreateAssetMenu(menuName = "R2/Items/Weapon Item")]
	public class WeaponItem : Item
	{
		public GameObject modelPrefab;
        public ItemActionContainer[] itemActions;
	
	}
}
