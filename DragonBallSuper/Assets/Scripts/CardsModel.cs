using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class CardsModel
{
    public CardModel[] Cards;
}

[System.Serializable]
public class CardModel
{
    public string CardId;
    public string Name;
    public string ImagePath;
    public string Type;
    public string BackImagePath;
}