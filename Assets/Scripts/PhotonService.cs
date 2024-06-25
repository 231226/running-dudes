using System.Collections.Generic;
using ExitGames.Client.Photon;
using MessagePipe;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using VContainer;

public class PhotonService : MonoBehaviourPunCallbacks
{
	[Inject] private IPublisher<PhotonMessages, bool> _masterPublisher;
	[Inject] private IPublisher<PhotonMessages, List<Player>> _publisher;

	private void Awake()
	{
		var rnd = Random.Range(0, 100000);
		PhotonNetwork.NickName = $"Player {rnd}";
		var stats = new Hashtable
		{
			["hp"] = Random.Range(80.0f, 100.0f)
		};
		PhotonNetwork.LocalPlayer.SetCustomProperties(stats);

		PhotonNetwork.AutomaticallySyncScene = true;
		PhotonNetwork.ConnectUsingSettings();
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
}
