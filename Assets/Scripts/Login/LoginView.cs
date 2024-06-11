using UnityEngine;
using UnityEngine.UI;

namespace Login
{
	public class LoginView : MonoBehaviour
	{
		[SerializeField] private Image _image;
		
		public void SetColor()
		{
			_image.color = Color.green;
		}
	}
}
