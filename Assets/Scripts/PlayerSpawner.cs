using ExitGames.Client.Photon.StructWrapping;
using Photon.Pun;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
	[SerializeField] private string _prefabName;

	private void Start()
	{
		var randX = Random.Range(-5.0f, 5.0f);
		var randZ = Random.Range(-5.0f, 5.0f);
		
		

		var go = PhotonNetwork.Instantiate(_prefabName, new Vector3(randX, 0.0f, randZ), Quaternion.identity);
		var comp = go.GetComponent<CylinderPlayer>();

		float hp;
		PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue("hp", out hp);
		comp.SetHealthPoints(hp);
	}
}
