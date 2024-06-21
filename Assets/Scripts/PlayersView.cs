using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;

public class PlayersView : MonoBehaviour
{
	[SerializeField] private PlayerItemView _view;
	[SerializeField] private Transform _parent;

	public void RefreshView(List<Player> players)
	{
		for (var i = 0; i < _parent.childCount; i++)
		{
			Destroy(_parent.GetChild(i).gameObject);
		}

		foreach (var player in players)
		{
			Instantiate(_view, _parent);
		}
	}
}
