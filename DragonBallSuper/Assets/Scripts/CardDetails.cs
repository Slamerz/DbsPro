// Decompiled with JetBrains decompiler
// Type: CardDetails
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ABA70E52-8491-417E-BCC4-DE60A04F1D86
// Assembly location: C:\Users\jacob\RiderProjects\DragonBallSuperOnline\bin\DragonBallSuper_Data\Managed\Assembly-CSharp.dll

using System.IO;
using UnityEngine;
using UnityEngine.UI;

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

    public void Start()
    {
        byte[] fileData;

        var filePath = Application.dataPath + "/42ea51ee-4e38-421c-a52d-87b0c410144b.png";
        if (File.Exists(filePath))
        {
            fileData = File.ReadAllBytes(filePath);
            Texture2D texture = new Texture2D(0, 0, TextureFormat.RGB24, false);
            texture.LoadImage(fileData);
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, 260, 363), new Vector2(0, 0));
            gameObject.GetComponent<Image>().sprite = sprite;
        }
        
     }
}
