using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Object = System.Object;

public class CardDetails : MonoBehaviour
{
    /*  public GameObject Canvas;
      public GameObject ZoomedInCard;
      private GameObject zoomCard;
      public GameObject Parent;

      public void Awake()
      {
        this.Canvas = GameObject.Find("Main-Canvas");
        this.Parent = GameObject.Find("Card-Zoom");
      }

      public void onHoverEnter()
      {
        this.zoomCard = Object.Instantiate<GameObject>(this.ZoomedInCard, (Vector3) new Vector2(0.0f, 0.0f), Quaternion.identity);
        this.zoomCard.transform.SetParent(this.Parent.transform, false);
        this.zoomCard.GetComponent<RectTransform>().sizeDelta = new Vector2(260f, 363f);
      }

      public void OnHoverExit() => Object.Destroy((Object) this.zoomCard);*/

    public GameObject ZoomedInCard;
    public GameObject ZoomCardZone;

    public void Start()
    {
        ZoomCardZone = GameObject.Find("Card-Zoom");
    }


    public void onHoverEnter()
    {
        ZoomedInCard = Instantiate(gameObject, new Vector2(0, 0), Quaternion.identity);
        ZoomedInCard.transform.SetParent(ZoomCardZone.transform, false);
        ZoomedInCard.GetComponent<RectTransform>().sizeDelta = new Vector2(88, 122);
    }

    public void onHoverExit() => Destroy(ZoomedInCard);
}
