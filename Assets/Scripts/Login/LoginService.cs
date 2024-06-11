using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Login
{
	public class LoginService : IInitializable, IService
	{
		[Inject] private SignalBus _signalBus;

		public void Initialize()
		{
			Login();
		}

		private async void Login()
		{
			await Task.Delay(1000);
			Debug.Log("FIRE");
			_signalBus.Fire<UserLoggedInSuccessfullySignal>();
		}

		public void Log()
		{
		}
	}
}
