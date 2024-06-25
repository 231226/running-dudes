using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerItemView : MonoBehaviour
{
	[SerializeField] private TMP_Text _nickname;
	[SerializeField] private GameObject _masterIndicator;

	public void SetNickname(string nickname)
	{
		_nickname.SetText(nickname);
	}

	public void SetMaster(bool isMaster)
	{
		_masterIndicator.SetActive(isMaster);
	}
}
