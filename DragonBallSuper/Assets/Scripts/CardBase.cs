using System;using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Mirror;
using UnityEngine;
using UnityEngine.UI;

public abstract class CardBase : NetworkBehaviour
{
    public PlayerManager PlayerManager;
    public string CardId;
    public string CardName;
    public string CardType;

    void Start()
    {
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        PlayerManager = networkIdentity.GetComponent<PlayerManager>();
    }


    public void Initialize(string Id)
    {
        var cardDetailsFilePath = "/CardDetails.json";
        if (!File.Exists(Application.dataPath + cardDetailsFilePath))
        {
            return;
        }

        var cardsDetailsJson = JsonDataTool.GetJsonFromFile(cardDetailsFilePath);
        var cardsDetails = JsonUtility.FromJson<CardsModel>(cardsDetailsJson);

        var cardDetails =
            cardsDetails.Cards.FirstOrDefault(card => card.CardId.Equals(Id, StringComparison.CurrentCultureIgnoreCase));

        if (cardDetails == null)
        {
            return;
        }

        CardId = cardDetails.CardId;
        CardName = cardDetails.Name;
        CardType = cardDetails.Type;
        var imageFilePath = Application.dataPath + $"/{cardDetails.ImagePath}";
        if (File.Exists(imageFilePath))
        {
            var fileData = File.ReadAllBytes(imageFilePath);
            Texture2D texture = new Texture2D(0, 0, TextureFormat.RGB24, false);
            texture.LoadImage(fileData);
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, 260, 363), new Vector2(0, 0));
            gameObject.GetComponent<Image>().sprite = sprite;
        }
    }
}

