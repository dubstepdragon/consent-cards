
using System;
using UdonSharp;
using UnityEngine;
using VRC.SDK3.Data;
using VRC.SDKBase;
using VRC.Udon;

public class ConsentCardManager : UdonSharpBehaviour
{
    
    public DataDictionary playersAndCards;
    
    public GameObject cardPrefab;
    
    public Transform cardSpawnPoint;

    public void Start()
    {
        playersAndCards = new DataDictionary();
    }

    public void ResetCard(VRCPlayerApi player)
    {
        if (playersAndCards.TryGetValue(player.playerId, TokenType.Reference, out var cardToRemove))
        {

        }
    }

    public void SpawnCard(VRCPlayerApi player)
    {

        GameObject newCard = Instantiate(cardPrefab);
        ConsentCard cc = newCard.GetComponent<ConsentCard>();
        cc.owningPlayerId = player.playerId;
        
        playersAndCards.Add(player.playerId, cc);
    }

    public void RemoveCard(VRCPlayerApi player)
    {
        if (playersAndCards.TryGetValue(player.playerId, TokenType.Reference, out var cardToRemove))
        {
            ConsentCard cc = (ConsentCard)cardToRemove.Reference;
            Destroy(cc.gameObject);
            playersAndCards.Remove(player.playerId);
        }
    }
    

    public override void OnPlayerLeft(VRCPlayerApi player)
    {
        RemoveCard(player);
        base.OnPlayerLeft(player);
    }
}
