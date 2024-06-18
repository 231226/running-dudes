using Photon.Pun;
using UnityEngine;

public class CyllinderPlayer : MonoBehaviour
{
	[SerializeField] private float _speed;

	private PhotonView _pv;

	private void Start()
	{
		_pv = GetComponent<PhotonView>();
	}

	private void Update()
	{
		if (_pv.IsMine)
		{
			transform.Translate(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * _speed);
		}
	}
}
