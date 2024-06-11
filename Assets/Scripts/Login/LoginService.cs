using System.Threading.Tasks;
using MessagePipe;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Login
{
	public class LoginService : IStartable, IService
	{
		[Inject] private IPublisher<string, Color> _publisher;
		[Inject] private Color _color;

		public void Log()
		{
		}

		public void Start()
		{
			Login();
		}

		private async void Login()
		{
			await Task.Delay(1000);
			Debug.Log("FIRE");
			_publisher.Publish(Constants.Keys.LoginResult, _color);
		}
	}
}
