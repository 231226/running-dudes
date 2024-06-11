using Login;
using Profile;
using Room;
using UnityEngine;
using Zenject;

public class MetaSceneInstaller : MonoInstaller
{
	[SerializeField] private LoginView _loginView;
	[SerializeField] private RoomView _roomView;
	[SerializeField] private ProfileView _profileView;

	public override void InstallBindings()
	{
		SignalBusInstaller.Install(Container);

		Container.DeclareSignal<UserLoggedInSuccessfullySignal>();

		Container.Bind<IService>().WithId(Constants.RoomViewDependencyID).To<RoomService>().AsSingle().NonLazy();
		Container.BindInterfacesTo<LoginService>().AsSingle().NonLazy();
		Container.Bind<IService>().WithId(Constants.ProfileViewDependencyID).To<ProfileService>().AsSingle().NonLazy();

		Container.Bind<RoomPresenter>().AsSingle().NonLazy();
		Container.BindInterfacesTo<LoginPresenter>().AsSingle().NonLazy();
		Container.Bind<ProfilePresenter>().AsSingle().NonLazy();

		Container.BindInstance(_loginView);
		Container.BindInstance(_roomView);
		Container.BindInstance(_profileView);
	}
}
