using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "231226/Skin List", fileName = "NewSkinList")]
public class SkinList : ScriptableObject
{
	[SerializeField] private List<IdMaterialPair> _idMaterialPairs;

	public Material GetMaterialById(string id)
	{
		var pair = _idMaterialPairs.Find(pair => pair.ID.Equals(id));
		return pair?.Material;
	}
}

[Serializable]
public class IdMaterialPair
{
	[SerializeField] private string _id;
	[SerializeField] private Material _material;

	public string ID => _id;
	public Material Material => _material;
}
