using System;
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
		var request = new GetPlayerProfileRequest
		{
			PlayFabId = obj.PlayFabId
		};
		PlayFabClientAPI.GetPlayerProfile(request, ResultCallback, ErrorCallback);
	}

	private void ResultCallback(GetPlayerProfileResult obj)
	{
		_publisher.Publish(PlayFabMessages.NicknameChanged, obj.PlayerProfile.DisplayName);
	}
}
