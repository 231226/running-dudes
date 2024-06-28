using System;
using MessagePipe;
using TMPro;
using UnityEngine;
using VContainer;

namespace Profile
{
	public class ProfileView : MonoBehaviour
	{
		[SerializeField] private GameObject _inputPanel;
		[SerializeField] private TMP_Text _nicknameText;

		private string _currentNickname;

		private IDisposable _disposableSubscriber;

		[Inject] private PlayFabService _playFabService;
		[Inject] private ISubscriber<PlayFabMessages, string> _subscriber;

		private void Start()
		{
			_disposableSubscriber = _subscriber.Subscribe(PlayFabMessages.NicknameChanged, NicknameChanged);
		}

		private void OnDestroy()
		{
			_disposableSubscriber.Dispose();
		}

		private void NicknameChanged(string newNickname)
		{
			_nicknameText.gameObject.SetActive(true);
			_nicknameText.SetText(newNickname);
		}

		public void OnNicknameClick()
		{
			_nicknameText.gameObject.SetActive(false);
			_inputPanel.SetActive(true);
		}

		public void OnOkButtonClick()
		{
			_inputPanel.SetActive(false);
			_playFabService.ChangeNickname(_currentNickname);
		}

		public void OnInputFieldValueChanged(string value)
		{
			_currentNickname = value;
		}
	}
}
