using Unity.Entities;
using UnityEngine;

public class PlayerTagAuthoring : MonoBehaviour
{
	public class RotationComponentBaker : Baker<PlayerTagAuthoring>
	{
		public override void Bake(PlayerTagAuthoring authoring)
		{
			var entity = GetEntity(TransformUsageFlags.None);
			AddComponent(entity, new PlayerComponent());
		}
	}
}

public struct PlayerComponent : IComponentData
{
}
