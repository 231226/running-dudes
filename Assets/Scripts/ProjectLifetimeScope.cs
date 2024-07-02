using System.Collections.Generic;
using MessagePipe;
using Photon.Realtime;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class ProjectLifetimeScope : LifetimeScope
{
	[SerializeField] private PhotonService _photonService;

	protected override void Configure(IContainerBuilder builder)
	{
		var options = builder.RegisterMessagePipe();
		builder.RegisterMessageBroker<PhotonMessages, List<Player>>(options);
		builder.RegisterMessageBroker<PhotonMessages, bool>(options);
		builder.RegisterMessageBroker<PlayFabMessages, string>(options);
		builder.RegisterComponent(_photonService);
		builder.RegisterEntryPoint<PlayFabService>().AsSelf();
	}
}
