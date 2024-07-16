using Unity.Entities;
using UnityEngine;

public struct CubeComponent : IComponentData
{
	public Entity Ent;
	public int Count;
}

public class SpawnCubeAuthoring : MonoBehaviour
{
	public GameObject Prefab;
	public int Count;

	public class CubeComponentBaker : Baker<SpawnCubeAuthoring>
	{
		public override void Bake(SpawnCubeAuthoring authoring)
		{
			var entity = GetEntity(TransformUsageFlags.None);
			AddComponent(entity, new CubeComponent
			{
				Ent = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic),
				Count = authoring.Count
			});
		}
	}
}
