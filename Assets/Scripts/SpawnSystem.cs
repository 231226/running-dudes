using Unity.Entities;

public partial class SpawnSystem : SystemBase
{
	protected override void OnUpdate()
	{
		Enabled = false;
		foreach (var component in SystemAPI.Query<RefRO<CubeComponent>>())
		{
			for (int i = 0; i < component.ValueRO.Count; i++)
			{
				EntityManager.Instantiate(component.ValueRO.Ent);
			}
		}
	}
}
