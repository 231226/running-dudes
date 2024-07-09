using System;
using System.Collections.Generic;
using MessagePipe;
using PlayFab.ClientModels;
using VContainer;
using VContainer.Unity;

public class InventoryPresenter : IStartable, IDisposable
{
	private IDisposable _ds;
	[Inject] private InventoryModel _model;
	[Inject] private PhotonService _photonService;
	[Inject] private PlayFabService _playFabService;
	[Inject] private ISubscriber<PlayFabMessages, List<ItemInstance>> _subscriber;
	[Inject] private InventoryView _view;

	public void Dispose()
	{
		_ds.Dispose();

		_view.ItemClicked -= Test;
	}

	public void Start()
	{
		_subscriber.Subscribe(PlayFabMessages.InventoryReceived, OnInventoryReceived);

		_view.ItemClicked += Test;
	}

	private void OnInventoryReceived(List<ItemInstance> items)
	{
		_model.Items = items;
		_view.RefreshView();
	}

	private void Test(string id)
	{
		_photonService.SetSkin(id);
		_playFabService.SetSkin(id);
	}
}
