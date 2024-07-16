using Unity.Entities;
using Unity.Transforms;

public partial struct RotationSystem : ISystem
{
	public void OnUpdate(ref SystemState state)
	{
		foreach (var component in
		         SystemAPI.Query<RefRW<LocalTransform>, RefRO<RotationComponent>>()
			         .WithNone<PlayerComponent>())
		{
			component.Item1.ValueRW =
				component.Item1.ValueRO.RotateY(component.Item2.ValueRO.Speed * SystemAPI.Time.DeltaTime);
		}
	}
}
