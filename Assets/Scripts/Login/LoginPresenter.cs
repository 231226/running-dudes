using System;
using System.Collections.Generic;
using MessagePipe;
using Photon.Realtime;
using VContainer;
using VContainer.Unity;

namespace Login
{
	public class LoginPresenter : IStartable, IDisposable
	{
		[Inject] private ISubscriber<PhotonMessages, List<Player>> _subscriber;
		private IDisposable _subscribers;
		[Inject] private LoginView _view;

		public void Dispose()
		{
			_subscribers.Dispose();
		}

		public void Start()
		{
			_subscribers = _subscriber.Subscribe(PhotonMessages.Joined, Smth);
		}

		private void Smth(List<Player> value)
		{
			_view.SetVisible();
		}
	}
}
