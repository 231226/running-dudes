public static class Constants
{
	public const string RoomViewDependencyID = "RoomView";
	public const string LoginViewDependencyID = "LoginView";
	public const string ProfileViewDependencyID = "ProfileView";

	public const string PlayerPrefsPlayFabId = "PlayFabCustomId"; 

	public class Keys
	{
		public const string LoginResult = "Color";
	}
}

public enum PhotonMessages
{
	PlayerEntered,
	PlayerLeft,
	MasterSwitched,
	Joined
}

public enum PlayFabMessages
{
	NicknameChanged
}
