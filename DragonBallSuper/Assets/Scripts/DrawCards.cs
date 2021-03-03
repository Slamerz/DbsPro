using Mirror;

public class DrawCards : NetworkBehaviour
{
  public PlayerManager PlayerManager;

  public void OnClick()
  {
    this.PlayerManager = NetworkClient.connection.identity.GetComponent<PlayerManager>();
    this.PlayerManager.CmdDealCards();
  }
}
