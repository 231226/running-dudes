using System;
using UnityEngine;
using VContainer;

public class InventoryView : MonoBehaviour
{
	[SerializeField] private InventoryItemView _view;
	[SerializeField] private Transform _parent;

	[Inject] private InventoryModel _model;

	public event Action<string> ItemClicked;

	public void RefreshView()
	{
		foreach (var item in _model.Items)
		{
			Instantiate(_view, _parent).Init(item.ItemId, ItemClickCallback);
		}
	}

	private void ItemClickCallback(string id)
	{
		ItemClicked?.Invoke(id);
	}
}
