using Login;
using MessagePipe;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class ProjectLifetimeScope : LifetimeScope
{
	[SerializeField] private Color _color;

	protected override void Configure(IContainerBuilder builder)
	{
		var options = builder.RegisterMessagePipe();
		builder.RegisterMessageBroker<string, Color>(options);

		builder.RegisterEntryPoint<LoginService>().AsSelf();

		builder.RegisterInstance(_color);
	}
}
