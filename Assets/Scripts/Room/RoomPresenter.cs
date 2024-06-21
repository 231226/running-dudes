using System;
using System.Collections.Generic;
using MessagePipe;
using Photon.Realtime;
using VContainer;
using VContainer.Unity;

namespace Room
{
	public class RoomPresenter : IStartable, IDisposable
	{
		[Inject] private PlayersView _playersView;
		[Inject] private ISubscriber<PhotonMessages, List<Player>> _subscriber;
		[Inject] private ISubscriber<PhotonMessages, bool> _masterSubscriber;
		private IDisposable _subscribers;
		[Inject] private RoomView _view;

		public void Dispose()
		{
			_subscribers.Dispose();
		}

		public void Start()
		{
			var bag = DisposableBag.CreateBuilder();
			_subscriber.Subscribe(PhotonMessages.Joined, PlayersUpdated).AddTo(bag);
			_subscriber.Subscribe(PhotonMessages.PlayerEntered, PlayersUpdated).AddTo(bag);
			_subscriber.Subscribe(PhotonMessages.PlayerLeft, PlayersUpdated).AddTo(bag);
			_masterSubscriber.Subscribe(PhotonMessages.MasterSwitched, MasterSwitched).AddTo(bag);
			_subscribers = bag.Build();
		}

		private void PlayersUpdated(List<Player> players)
		{
			_playersView.RefreshView(players);
		}

		private void MasterSwitched(bool isMaster)
		{
			_view.SetButtonVisible(isMaster);
		}
	}
}
