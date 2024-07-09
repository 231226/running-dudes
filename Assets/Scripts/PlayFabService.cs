using System;
using System.Collections.Generic;
using MessagePipe;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class PlayFabService : IStartable
{
	private string _id;
	[Inject] private IPublisher<PlayFabMessages, string> _publisher;
	[Inject] private IPublisher<PlayFabMessages, List<ItemInstance>> _publisherInventory;

	public void Start()
	{
		_id = Load();

		var request = new LoginWithCustomIDRequest
		{
			CustomId = _id,
			CreateAccount = true
		};
		PlayFabClientAPI.LoginWithCustomID(request, ResultCallback, ErrorCallback);
	}

	public void ChangeNickname(string value)
	{
		var request = new UpdateUserTitleDisplayNameRequest
		{
			DisplayName = value
		};
		PlayFabClientAPI.UpdateUserTitleDisplayName(request, ResultCallback, ErrorCallback);
	}

	private void ResultCallback(UpdateUserTitleDisplayNameResult obj)
	{
		_publisher.Publish(PlayFabMessages.NicknameChanged, obj.DisplayName);
	}

	private void Save(string id)
	{
		PlayerPrefs.SetString(Constants.PlayerPrefsPlayFabId, id);
		PlayerPrefs.Save();
	}

	private string Load()
	{
		return PlayerPrefs.GetString(Constants.PlayerPrefsPlayFabId, Guid.NewGuid().ToString());
	}

	private void ErrorCallback(PlayFabError obj)
	{
		Debug.LogError(obj.ErrorMessage);
	}

	private void ResultCallback(LoginResult obj)
	{
		Save(_id);

		var profileRequest = new GetPlayerProfileRequest
		{
			PlayFabId = obj.PlayFabId
		};
		PlayFabClientAPI.GetPlayerProfile(profileRequest, ResultCallback, ErrorCallback);

		var inventoryRequest = new GetUserInventoryRequest();
		PlayFabClientAPI.GetUserInventory(inventoryRequest, ResultCallback, ErrorCallback);

		// CreateNewCharacter();
	}

	private void ResultCallback(GetUserInventoryResult obj)
	{
		_publisherInventory.Publish(PlayFabMessages.InventoryReceived, obj.Inventory);
	}

	private void ResultCallback(GetPlayerProfileResult obj)
	{
		_publisher.Publish(PlayFabMessages.NicknameChanged, obj.PlayerProfile.DisplayName);

		var dataRequest = new GetUserDataRequest
		{
			PlayFabId = obj.PlayerProfile.PlayerId
		};
		PlayFabClientAPI.GetUserData(dataRequest, ResultCallback, ErrorCallback);

		// var request = new GetStoreItemsRequest
		// {
		// 	CatalogVersion = "1.0",
		// 	StoreId = "main_store"
		// };
		//PlayFabClientAPI.GetStoreItems(request, ResultCallback, ErrorCallback);
	}

	private void ResultCallback(GetUserDataResult obj)
	{
		if (obj.Data.TryGetValue("skin", out UserDataRecord record))
		{
			_publisher.Publish(PlayFabMessages.SkinChanged, record.Value);
		}
	}

	private void ResultCallback(GetStoreItemsResult obj)
	{
		foreach (var item in obj.Store)
		{
			Debug.Log(item.ItemId);
		}

		var request = new PurchaseItemRequest
		{
			CatalogVersion = "1.0",
			ItemId = obj.Store[0].ItemId,
			Price = (int)obj.Store[0].VirtualCurrencyPrices["SC"],
			VirtualCurrency = "SC"
		};

		//PlayFabClientAPI.PurchaseItem(request, ResultCallback, ErrorCallback);
	}

	private void ResultCallback(PurchaseItemResult obj)
	{
	}

	private void CreateNewCharacter()
	{
		PlayFabClientAPI.GetStoreItems(new GetStoreItemsRequest
		{
			CatalogVersion = "1.0",
			StoreId = "main_store"
		}, result =>
		{
			PlayFabClientAPI.PurchaseItem(new PurchaseItemRequest
			{
				CatalogVersion = "1.0",
				ItemId = "hero_token",
				Price = 1,
				VirtualCurrency = "TP"
			}, itemResult =>
			{
				PlayFabClientAPI.GrantCharacterToUser(new GrantCharacterToUserRequest
				{
					CatalogVersion = "1.0",
					ItemId = itemResult.Items[0].ItemId,
					CharacterName = "Conan"
				}, characterResult => { Debug.Log($"MAX SUCCESS! {characterResult.CharacterId}"); }, ErrorCallback);
			}, ErrorCallback);
		}, ErrorCallback);
	}

	public void SetSkin(string id)
	{
		var request = new UpdateUserDataRequest
		{
			Data = new Dictionary<string, string>
			{
				["skin"] = id
			}
		};
		PlayFabClientAPI.UpdateUserData(request, ResultCallback, ErrorCallback);
	}

	private void ResultCallback(UpdateUserDataResult obj)
	{
	}
}
