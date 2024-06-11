using System;
using MessagePipe;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Login
{
	public class LoginPresenter : IStartable, IDisposable
	{
		[Inject] private ISubscriber<string, Color> _subscriber;
		private IDisposable _subscribers;
		[Inject] private LoginView _view;

		public void Dispose()
		{
			_subscribers.Dispose();
		}

		public void Start()
		{
			_subscribers = _subscriber.Subscribe(Constants.Keys.LoginResult, Smth);
		}

		private void Smth(Color value)
		{
			_view.SetColor(value);
		}
	}
}
