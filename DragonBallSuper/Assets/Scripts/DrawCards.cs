// Decompiled with JetBrains decompiler
// Type: DrawCards
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ABA70E52-8491-417E-BCC4-DE60A04F1D86
// Assembly location: C:\Users\jacob\RiderProjects\DragonBallSuperOnline\bin\DragonBallSuper_Data\Managed\Assembly-CSharp.dll

using Mirror;

public class DrawCards : NetworkBehaviour
{
  public PlayerManager PlayerManager;

  public void OnClick()
  {
    this.PlayerManager = NetworkClient.connection.identity.GetComponent<PlayerManager>();
    this.PlayerManager.CmdDealCards();
  }

  private void MirrorProcessed()
  {
  }
}
