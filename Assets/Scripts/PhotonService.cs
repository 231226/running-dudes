using MessagePipe;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using VContainer;

public class PhotonService : MonoBehaviourPunCallbacks
{
	[Inject] private IPublisher<string, Color> _publisher;

	private void Awake()
	{
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
		_publisher.Publish(Constants.Keys.LoginResult, Color.red);
	}

	public override void OnPlayerEnteredRoom(Player newPlayer)
	{
		if (PhotonNetwork.CurrentRoom.Players.Count >= 2)
		{
			PhotonNetwork.LoadLevel("Scenes/Core");
		}
	}
}
