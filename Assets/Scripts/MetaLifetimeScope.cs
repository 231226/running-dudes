using Login;
using Profile;
using Room;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class MetaLifetimeScope : LifetimeScope
{
	[SerializeField] private LoginView _loginView;
	[SerializeField] private RoomView _roomView;
	[SerializeField] private ProfileView _profileView;
	[SerializeField] private PhotonService _photonService;

	protected override void Configure(IContainerBuilder builder)
	{
		builder.Register<RoomService>(Lifetime.Singleton).As<IService>();
		builder.Register<ProfileService>(Lifetime.Singleton).As<IService>();

		builder.Register<RoomPresenter>(Lifetime.Singleton);
		builder.Register<ProfilePresenter>(Lifetime.Singleton);
		builder.RegisterEntryPoint<LoginPresenter>();

		builder.RegisterComponent(_loginView);
		builder.RegisterComponent(_roomView);
		builder.RegisterComponent(_profileView);
		builder.RegisterComponent(_photonService);
	}
}
