using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using MessagePipe;
using Photon.Pun;
using Photon.Realtime;
using VContainer;
using Random = UnityEngine.Random;

public class PhotonService : MonoBehaviourPunCallbacks
{
	private IDisposable _ds1;
	private IDisposable _ds2;

	[Inject] private IPublisher<PhotonMessages, bool> _masterPublisher;
	[Inject] private IPublisher<PhotonMessages, List<Player>> _publisher;
	[Inject] private ISubscriber<PlayFabMessages, string> _subscriber;

	private void Awake()
	{
		_ds1 = _subscriber.Subscribe(PlayFabMessages.NicknameChanged, NicknameChanged);
		_ds2 = _subscriber.Subscribe(PlayFabMessages.SkinChanged, SetSkin);
		var stats = new Hashtable
		{
			["hp"] = Random.Range(80.0f, 100.0f),
			["skin"] = "road"
		};
		PhotonNetwork.LocalPlayer.SetCustomProperties(stats);
		PhotonNetwork.AutomaticallySyncScene = true;
	}

	private void OnDestroy()
	{
		_ds1.Dispose();
		_ds2.Dispose();
	}

	private void Connect()
	{
		if (!PhotonNetwork.IsConnected)
		{
			PhotonNetwork.ConnectUsingSettings();
		}
	}

	private void NicknameChanged(string newNickname)
	{
		PhotonNetwork.NickName = newNickname;
		Connect();
	}

	public override void OnConnectedToMaster()
	{
		PhotonNetwork.JoinLobby();
	}

	public override void OnJoinedLobby()
	{
		PhotonNetwork.JoinRandomOrCreateRoom();
	}

	public override void OnJoinedRoom()
	{
		var players = new List<Player>();
		foreach (var pair in PhotonNetwork.CurrentRoom.Players)
		{
			players.Add(pair.Value);
		}

		_publisher.Publish(PhotonMessages.Joined, players);
		_masterPublisher.Publish(PhotonMessages.MasterSwitched, PhotonNetwork.IsMasterClient);
	}

	public override void OnPlayerEnteredRoom(Player newPlayer)
	{
		var players = new List<Player>();
		foreach (var pair in PhotonNetwork.CurrentRoom.Players)
		{
			players.Add(pair.Value);
		}

		_publisher.Publish(PhotonMessages.PlayerEntered, players);
	}

	public override void OnPlayerLeftRoom(Player otherPlayer)
	{
		var players = new List<Player>();
		foreach (var pair in PhotonNetwork.CurrentRoom.Players)
		{
			players.Add(pair.Value);
		}

		_publisher.Publish(PhotonMessages.PlayerLeft, players);
	}

	public override void OnMasterClientSwitched(Player newMasterClient)
	{
		_masterPublisher.Publish(PhotonMessages.MasterSwitched, PhotonNetwork.IsMasterClient);
	}

	public void SetSkin(string id)
	{
		var stats = new Hashtable
		{
			["skin"] = id
		};
		PhotonNetwork.LocalPlayer.SetCustomProperties(stats);
	}
}
