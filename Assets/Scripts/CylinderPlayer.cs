using Photon.Pun;
using UnityEngine;

public class CylinderPlayer : MonoBehaviour, IPunObservable
{
	[SerializeField] private float _speed;

	private float _health = 100.0f;

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
			
			if (Input.GetKeyDown(KeyCode.P))
			{
				_health -= 5.0f;
			}
		}
	}

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.IsWriting)
		{
			stream.SendNext(_health);
		}
		else
		{
			_health = (float)stream.ReceiveNext();
		}
	}
}
