using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class DrawCard : MonoBehaviour
{
    public List<string> Cards;
    public GameObject Card;

    public GameObject PlayerHand;


    void Start()
    {
        PlayerHand = GameObject.Find("Player-Hand");
        var deckAsJson = JsonDataTool.GetJsonFromFile("./decks/deck.json");
        var deckData = JsonUtility.FromJson<Deck>(deckAsJson);

        foreach(var deckCard in deckData.Cards)
        {
            for(int i = 0; i < deckCard.Quantity; i++)
            {
              Cards.Add(deckCard.CardId);
            }
        }
        Shuffle();
    }


    public void Draw()
    {
        if (Cards.Count <= 0)
        {
            return;
        }

        var drawCard = Instantiate(Card, new Vector2(0, 0), Quaternion.identity);
        Type cardScript = Type.GetType(Cards[0].Replace("-", ""));
        drawCard.AddComponent(cardScript);
        drawCard.transform.SetParent(PlayerHand.transform, false);

        Cards.RemoveAt(0);
    }







    private class WeightedCards
    {
        public float Weight { get; set; }

        public string Id { get; set; }
    }

    public void Shuffle()
    {
        if (Cards.Count <= 1)
        {
            return;
        }

        var seed = Random.Range(1,10);
        List<WeightedCards> weightedCards = new List<WeightedCards>();
        

        foreach (var card in Cards)
        {
            var weighted = new WeightedCards {Id = card, Weight = Random.value};
            weightedCards.Add(weighted);
        }

        weightedCards.Sort((x, y) => x.Weight.CompareTo(y.Weight));

        for (var i = 0; i < seed; i++)
        {
            weightedCards = Bridge(weightedCards);
        }

        var shuffledCardIds = new List<string>();

        foreach (var c in weightedCards)
        {
            shuffledCardIds.Add(c.Id);
        }

        Cards = shuffledCardIds;

        List<WeightedCards> Bridge(List<WeightedCards> input)
        {
            var result = new List<WeightedCards>();
            int half = (int) Math.Floor((double) (input.Count / 2));
            List<WeightedCards> bucketA = input.Take(half).ToList();
            List<WeightedCards> bucketB = input.Skip(bucketA.Count).ToList();

            for (var i = 0; i < bucketA.Count; i++)
            {
                result.Add(bucketA[i]);
                result.Add(bucketB[i]);
            }

            if (bucketA.Count < bucketB.Count)
            {
                result.Add(bucketB.Last());
            }

            return result;
        }
    }
}


