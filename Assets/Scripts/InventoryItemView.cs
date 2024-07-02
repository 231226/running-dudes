using TMPro;
using UnityEngine;

public class InventoryItemView : MonoBehaviour
{
	[SerializeField] private TMP_Text _itemId;

	public void SetId(string id)
	{
		_itemId.SetText(id);
	}
}
