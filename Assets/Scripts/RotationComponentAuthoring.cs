using Unity.Entities;
using UnityEngine;

public class RotationComponentAuthoring : MonoBehaviour
{
	public float Speed;

	public class RotationComponentBaker : Baker<RotationComponentAuthoring>
	{
		public override void Bake(RotationComponentAuthoring authoring)
		{
			var entity = GetEntity(TransformUsageFlags.Dynamic);
			AddComponent(entity, new RotationComponent { Speed = authoring.Speed });
		}
	}
}

public struct RotationComponent : IComponentData
{
	public float Speed;
}

