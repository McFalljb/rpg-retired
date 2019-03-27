using UnityEngine;
using System.Collections;
using System;

namespace R2
{
	public class ClothItemHook : MonoBehaviour
	{
		public ClothItemType clothItemType;
		public SkinnedMeshRenderer meshRenderer;
		public Mesh defaultMesh;
		public Material defaultMaterial;

		public void Init()
		{
			ClothManager clothManager = GetComponentInParent<ClothManager>();
			clothManager.RegisterClothHook(this);
		}

		internal void LoadClothItem(ClothItem clothItem)
		{
			meshRenderer.sharedMesh = clothItem.mesh;
			meshRenderer.material = clothItem.clothMaterial;
			meshRenderer.enabled = true;
		}

		internal void UnloadItem()
		{
			if (clothItemType.isDisabledWhenNoItem)
			{
				meshRenderer.enabled = false;
			}
			else
			{
				meshRenderer.sharedMesh = defaultMesh;
				meshRenderer.material = defaultMaterial;
			}

		}
	}
}
