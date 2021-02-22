// Decompiled with JetBrains decompiler
// Type: DragDrop
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ABA70E52-8491-417E-BCC4-DE60A04F1D86
// Assembly location: C:\Users\jacob\RiderProjects\DragonBallSuperOnline\bin\DragonBallSuper_Data\Managed\Assembly-CSharp.dll

using Mirror;
using UnityEngine;

public class DragDrop : NetworkBehaviour
{
  public GameObject Canvas;
  private bool isDragging = false;
  private bool isOverDropZone = false;
  private bool isDraggable = true;
  private GameObject startDropZone;
  private GameObject dropZone;
  private Vector2 startPosition;

  public PlayerManager PlayerManager { get; private set; }

  private void Start()
  {
    this.Canvas = GameObject.Find("Main_Canvas");
    if (this.hasAuthority)
      return;
    this.isDraggable = false;
  }

  private void Update()
  {
    this.ResizeCollider();
    if (!this.isDragging)
      return;
    this.transform.position = (Vector3) new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    this.transform.SetParent(this.Canvas.transform, true);
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    if (!((Object) collision.gameObject != (Object) this.startDropZone))
      return;
    this.isOverDropZone = true;
    this.dropZone = collision.gameObject;
  }

  private void OnCollisionExit2D(Collision2D collision)
  {
    if (!((Object) collision.gameObject != (Object) this.startDropZone) || !((Object) collision.gameObject == (Object) this.dropZone))
      return;
    this.isOverDropZone = false;
    this.dropZone = (GameObject) null;
  }

  public void StartDrag()
  {
    if (!this.isDraggable)
      return;
    this.startPosition = (Vector2) this.transform.position;
    this.startDropZone = this.transform.parent.gameObject;
    this.isDragging = true;
  }

  public void EndDrag()
  {
    if (!this.isDraggable)
      return;
    this.isDragging = false;
    if (this.isOverDropZone && this.dropZone.GetComponent<ZoneScript>().IsOwner)
    {
      this.transform.SetParent(this.dropZone.transform, false);
      this.PlayerManager = NetworkClient.connection.identity.GetComponent<PlayerManager>();
      this.PlayerManager.PlayCard(this.gameObject, this.dropZone);
    }
    else
    {
      this.transform.position = (Vector3) this.startPosition;
      this.transform.SetParent(this.startDropZone.transform, false);
    }
  }

  public void ResizeCollider()
  {
    Vector2 sizeDelta = this.gameObject.GetComponentInChildren<RectTransform>().sizeDelta;
    this.gameObject.GetComponentInChildren<BoxCollider2D>().size = sizeDelta;
  }

  private void MirrorProcessed()
  {
  }
}
