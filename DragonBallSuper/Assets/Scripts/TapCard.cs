// Decompiled with JetBrains decompiler
// Type: TapCard
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ABA70E52-8491-417E-BCC4-DE60A04F1D86
// Assembly location: C:\Users\jacob\RiderProjects\DragonBallSuperOnline\bin\DragonBallSuper_Data\Managed\Assembly-CSharp.dll

using Mirror;
using UnityEngine;
using UnityEngine.EventSystems;

public class TapCard : NetworkBehaviour
{
  public PlayerManager PlayerManager { get; private set; }

  public void OnRightClick(BaseEventData baseEventData)
  {
    if (!this.hasAuthority || !Input.GetMouseButtonUp(1))
      return;
    this.PlayerManager = NetworkClient.connection.identity.GetComponent<PlayerManager>();
    this.PlayerManager.TapCard(this.gameObject);
  }

  private void MirrorProcessed()
  {
  }
}
