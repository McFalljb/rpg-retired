using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
	[CreateAssetMenu(menuName ="Managers/ResourcesManager")]
	public class ResourcesManager : ScriptableObject
	{
		public List<Item> allItems = new List<Item>();
		Dictionary<string, Item> itemDict = new Dictionary<string, Item>();

		public void Init()
		{
			for (int i = 0; i < allItems.Count; i++)
			{
				if (!itemDict.ContainsKey(allItems[i].name))
				{
					itemDict.Add(allItems[i].name, allItems[i]);
				}
				else
				{
					//debug log for duplicate item
				}
			}
		}

		public Item GetItemInstance(string targetId)
		{
			Item defaultItem = GetItem(targetId);
			Item newItem = Instantiate(defaultItem);
			newItem.name = defaultItem.name;


			return newItem;
		}

		public Item GetItem(string targetId)
		{
			Item retVal = null;
			itemDict.TryGetValue(targetId, out retVal);
			return retVal;
		}
	}
}
