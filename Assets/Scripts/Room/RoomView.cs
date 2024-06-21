using Photon.Pun;
using UnityEngine;

namespace Room
{
	public class RoomView : MonoBehaviour
	{
		[SerializeField] private GameObject _startButton;

		public void SetButtonVisible(bool value)
		{
			_startButton.gameObject.SetActive(value);
		}

		public void StartGame()
		{
			PhotonNetwork.LoadLevel("Scenes/Core");
		}
	}
}
