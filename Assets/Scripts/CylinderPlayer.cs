using Photon.Pun;
using UnityEngine;

public class CylinderPlayer : MonoBehaviour, IPunObservable
{
	[SerializeField] private float _speed;

	private float _health;

	private PhotonView _pv;
	private MeshRenderer _mr;

	private void Start()
	{
		_mr = GetComponent<MeshRenderer>();
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

	private void OnCollisionEnter(Collision other)
	{
		var comp = other.gameObject.GetComponent<PhotonView>();
		if (comp is not null)
		{
			if (_pv.IsMine)
			{
				comp.RPC(nameof(DealDamage), RpcTarget.Others);
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

	public void SetHealthPoints(float value)
	{
		_health = value;
	}

	[PunRPC]
	public void DealDamage()
	{
		_health -= 5.0f;
	}
}
