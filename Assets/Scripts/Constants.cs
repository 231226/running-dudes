public static class Constants
{
	public const string RoomViewDependencyID = "RoomView";
	public const string LoginViewDependencyID = "LoginView";
	public const string ProfileViewDependencyID = "ProfileView";

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
