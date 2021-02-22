// Decompiled with JetBrains decompiler
// Type: PlayerManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ABA70E52-8491-417E-BCC4-DE60A04F1D86
// Assembly location: C:\Users\jacob\RiderProjects\DragonBallSuperOnline\bin\DragonBallSuper_Data\Managed\Assembly-CSharp.dll

using Mirror;
using Mirror.RemoteCalls;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : NetworkBehaviour
{
  public GameObject Card1;
  public GameObject PlayerHand;
  public GameObject EnemyHand;
  private List<GameObject> cards = new List<GameObject>();
  public GameObject PlayerBattleZone;
  public GameObject EnemeyBattleZone;
  public GameObject PlayerComboZone;
  public GameObject EnemyComboZone;
  public GameObject PlayerEnergyZone;
  public GameObject EnemyEnergyZone;

  public override void OnStartClient()
  {
    base.OnStartClient();
    this.PlayerHand = GameObject.Find("Player-Hand");
    this.EnemyHand = GameObject.Find("Enemy-Hand");
    this.PlayerBattleZone = GameObject.Find("Player-Battle-Zone");
    this.EnemeyBattleZone = GameObject.Find("Enemy-Battle-Zone");
  }

  [Server]
  public override void OnStartServer()
  {
    if (!NetworkServer.active)
      Debug.LogWarning((object) "[Server] function 'System.Void PlayerManager::OnStartServer()' called when server was not active");
    else
      this.cards.Add(this.Card1);
  }

  [Command]
  public void CmdDealCards()
  {
    PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
    this.SendCommandInternal(typeof (PlayerManager), nameof (CmdDealCards), (NetworkWriter) writer, 0);
    NetworkWriterPool.Recycle(writer);
  }

  public void PlayCard(GameObject card, GameObject targetZone = null) => this.CmdPlayCard(card, targetZone);

  public void TapCard(GameObject card) => this.CmdTapCard(card);

  [Command]
  private void CmdTapCard(GameObject card)
  {
    PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
    writer.WriteGameObject(card);
    this.SendCommandInternal(typeof (PlayerManager), nameof (CmdTapCard), (NetworkWriter) writer, 0);
    NetworkWriterPool.Recycle(writer);
  }

  [Command]
  private void CmdPlayCard(GameObject card, GameObject targetZone)
  {
    PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
    writer.WriteGameObject(card);
    writer.WriteGameObject(targetZone);
    this.SendCommandInternal(typeof (PlayerManager), nameof (CmdPlayCard), (NetworkWriter) writer, 0);
    NetworkWriterPool.Recycle(writer);
  }

  [ClientRpc]
  private void RpcShowCard(GameObject card, string type, GameObject destination)
  {
    PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
    writer.WriteGameObject(card);
    writer.WriteString(type);
    writer.WriteGameObject(destination);
    this.SendRPCInternal(typeof (PlayerManager), nameof (RpcShowCard), (NetworkWriter) writer, 0, false);
    NetworkWriterPool.Recycle(writer);
  }

  private void MirrorProcessed()
  {
  }

  public void UserCode_CmdDealCards()
  {
    GameObject card = Object.Instantiate<GameObject>(this.cards[Random.Range(0, this.cards.Count)], (Vector3) new Vector2(0.0f, 0.0f), Quaternion.identity);
    NetworkServer.Spawn(card, this.connectionToClient);
    this.RpcShowCard(card, "Dealt", (GameObject) null);
  }

  protected static void InvokeUserCode_CmdDealCards(
    NetworkBehaviour obj,
    NetworkReader reader,
    NetworkConnectionToClient senderConnection)
  {
    if (!NetworkServer.active)
      Debug.LogError((object) "Command CmdDealCards called on client.");
    else
      ((PlayerManager) obj).UserCode_CmdDealCards();
  }

  private void UserCode_CmdTapCard(GameObject card) => this.RpcShowCard(card, "Tap", (GameObject) null);

  protected static void InvokeUserCode_CmdTapCard(
    NetworkBehaviour obj,
    NetworkReader reader,
    NetworkConnectionToClient senderConnection)
  {
    if (!NetworkServer.active)
      Debug.LogError((object) "Command CmdTapCard called on client.");
    else
      ((PlayerManager) obj).UserCode_CmdTapCard(reader.ReadGameObject());
  }

  private void UserCode_CmdPlayCard(GameObject card, GameObject targetZone) => this.RpcShowCard(card, "Played", targetZone);

  protected static void InvokeUserCode_CmdPlayCard(
    NetworkBehaviour obj,
    NetworkReader reader,
    NetworkConnectionToClient senderConnection)
  {
    if (!NetworkServer.active)
      Debug.LogError((object) "Command CmdPlayCard called on client.");
    else
      ((PlayerManager) obj).UserCode_CmdPlayCard(reader.ReadGameObject(), reader.ReadGameObject());
  }

  private void UserCode_RpcShowCard(GameObject card, string type, GameObject destination)
  {
    if (type == "Dealt")
    {
      if (this.hasAuthority)
        card.transform.SetParent(this.PlayerHand.transform, false);
      else
        card.transform.SetParent(this.EnemyHand.transform, false);
    }
    else if (type == "Played")
    {
      if (this.hasAuthority)
        card.transform.SetParent(this.PlayerBattleZone.transform, false);
      else
        card.transform.SetParent(this.EnemeyBattleZone.transform, false);
    }
    else
    {
      if (!(type == "Tap"))
        return;
      float z = card.transform.eulerAngles.z;
      card.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 90f);
      float num1 = z;
      float num2 = 0.0f;
      if (!num2.Equals(num1))
      {
        num2 = 90f;
        if (!num2.Equals(num1))
        {
          num2 = 180f;
          if (!num2.Equals(num1))
          {
            num2 = 270f;
            if (num2.Equals(num1))
              card.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 180f);
          }
          else
            card.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 270f);
        }
        else
          card.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
      }
      else
        card.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 90f);
    }
  }

  protected static void InvokeUserCode_RpcShowCard(
    NetworkBehaviour obj,
    NetworkReader reader,
    NetworkConnectionToClient senderConnection)
  {
    if (!NetworkClient.active)
      Debug.LogError((object) "RPC RpcShowCard called on server.");
    else
      ((PlayerManager) obj).UserCode_RpcShowCard(reader.ReadGameObject(), reader.ReadString(), reader.ReadGameObject());
  }

  static PlayerManager()
  {
    RemoteCallHelper.RegisterCommandDelegate(typeof (PlayerManager), "CmdDealCards", new CmdDelegate(PlayerManager.InvokeUserCode_CmdDealCards), false);
    RemoteCallHelper.RegisterCommandDelegate(typeof (PlayerManager), "CmdTapCard", new CmdDelegate(PlayerManager.InvokeUserCode_CmdTapCard), false);
    RemoteCallHelper.RegisterCommandDelegate(typeof (PlayerManager), "CmdPlayCard", new CmdDelegate(PlayerManager.InvokeUserCode_CmdPlayCard), false);
    RemoteCallHelper.RegisterRpcDelegate(typeof (PlayerManager), "RpcShowCard", new CmdDelegate(PlayerManager.InvokeUserCode_RpcShowCard));
  }
}
