using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Deck
{
    public string Leader;

    public DeckCard[] Cards;
}

[System.Serializable]
public class DeckCard
{
    public string CardId;

    public int Quantity;
}