using System;
using System.Collections.Generic;
using MessagePipe;
using PlayFab.ClientModels;
using UnityEngine;
using VContainer;

public class InventoryView : MonoBehaviour
{
	[SerializeField] private InventoryItemView _view;
	[SerializeField] private Transform _parent;

	private IDisposable _ds;

	[Inject] private ISubscriber<PlayFabMessages, List<ItemInstance>> _subscriber;

	private void Start()
	{
		_subscriber.Subscribe(PlayFabMessages.InventoryReceived, RefreshView);
	}

	private void OnDestroy()
	{
		_ds.Dispose();
	}

	private void RefreshView(List<ItemInstance> items)
	{
		foreach (var item in items)
		{
			Instantiate(_view, _parent).SetId(item.ItemId);
		}
	}
}
