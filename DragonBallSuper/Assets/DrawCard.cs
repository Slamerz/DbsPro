using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class DrawCard : MonoBehaviour
{
    public List<string> Cards;
    void Start()
    {
        var deckAsJson = JsonDataTool.GetJsonFromFile("./decks/deck.json");
        var deckData = JsonUtility.FromJson<Deck>(deckAsJson);

        foreach(var deckCard in deckData.Cards)
        {
            for(int i = 0; i < deckCard.Quantity; i++)
            {
              Cards.Add(deckCard.CardId);
            }
        }
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

        List<WeightedCards> Bridge(List<WeightedCards> input)
        {
            int half = (int) Math.Floor((double) (input.Count / 2));
            List<WeightedCards> bucketA = input.Take( half).ToList();
            List<WeightedCards> bucketB = input.Take(input.Count - bucketA.Count).Skip(half).ToList();
        }
    }
}


