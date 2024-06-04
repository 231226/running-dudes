using System;
using Zenject;

namespace Login
{
	public class LoginPresenter : IInitializable, IDisposable
	{
		[Inject] private SignalBus _signalBus;
		[Inject] private LoginView _view;

		public void Dispose()
		{
			_signalBus.Unsubscribe<UserLoggedInSuccessfullySignal>(Smth);
		}

		public void Initialize()
		{
			_signalBus.Subscribe<UserLoggedInSuccessfullySignal>(Smth);
		}

		private void Smth()
		{
			_view.SetColor();
		}
	}
}
