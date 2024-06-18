using Photon.Pun;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
	[SerializeField] private string _prefabName;

	private void Start()
	{
		PhotonNetwork.Instantiate(_prefabName, Vector3.zero, Quaternion.identity);
	}
}
