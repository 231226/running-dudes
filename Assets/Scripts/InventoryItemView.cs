using System;
using TMPro;
using UnityEngine;

public class InventoryItemView : MonoBehaviour
{
	[SerializeField] private TMP_Text _itemId;
	private Action<string> _clickCallback;
	private string _id;

	public void Init(string id, Action<string> clickCallback)
	{
		_id = id;
		_clickCallback = clickCallback;
		_itemId.SetText(id);
	}

	public void OnItemClick()
	{
		_clickCallback?.Invoke(_id);
	}
}
