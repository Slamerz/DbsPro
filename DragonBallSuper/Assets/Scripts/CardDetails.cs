// Decompiled with JetBrains decompiler
// Type: CardDetails
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ABA70E52-8491-417E-BCC4-DE60A04F1D86
// Assembly location: C:\Users\jacob\RiderProjects\DragonBallSuperOnline\bin\DragonBallSuper_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class CardDetails : MonoBehaviour
{
  public GameObject Canvas;
  public GameObject ZoomedInCard;
  private GameObject zoomCard;
  public GameObject Parent;

  public void Awake()
  {
    this.Canvas = GameObject.Find("Main_Canvas");
    this.Parent = GameObject.Find("Card_Details");
  }

  public void onHoverEnter()
  {
    this.zoomCard = Object.Instantiate<GameObject>(this.ZoomedInCard, (Vector3) new Vector2(0.0f, 0.0f), Quaternion.identity);
    this.zoomCard.transform.SetParent(this.Parent.transform, false);
    this.zoomCard.GetComponent<RectTransform>().sizeDelta = new Vector2(260f, 363f);
  }

  public void OnHoverExit() => Object.Destroy((Object) this.zoomCard);
}
