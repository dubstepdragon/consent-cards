
using System;
using UdonSharp;
using UnityEngine;
using VRC.SDK3.Components;
using VRC.SDK3.Data;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;

public class ConsentCardManager : UdonSharpBehaviour
{
    
    private DataDictionary playersAndCards = new DataDictionary();
    
    public VRCObjectPool cardPool;
    
    
    private void SpawnCard(VRCPlayerApi player)
    {
        GameObject newCard = cardPool.TryToSpawn();
        if (newCard == null) return;
        ConsentCard cc = newCard.GetComponent<ConsentCard>();
        cc.owningPlayerId = player.playerId;
        cc.RequestSerialization();
        playersAndCards.Add(player.playerId, cc);
    }
    

    public override void OnDeserialization()
    {
        base.OnDeserialization();
        
        var players = new VRCPlayerApi[VRCPlayerApi.GetPlayerCount()];  
        VRCPlayerApi.GetPlayers(players);

        foreach (var player in players)
        {
            if (player != Networking.LocalPlayer)
            {
                SpawnCard(player);
            }
        }
    }

    private void RemoveCard(VRCPlayerApi player)
    {
        var cc = GetCard(player);
        if(cc != null )
        {
            cardPool.Return(cc.gameObject);
        }
        playersAndCards.Remove(player.playerId);
    }

    private ConsentCard GetCard(VRCPlayerApi player)
    {
        if (playersAndCards.TryGetValue(player.playerId, TokenType.Reference, out var cardToRemove))
        {
           return (ConsentCard)cardToRemove.Reference;
        }
        return null;
    }

    public void SetPlayerRed()
    {
        VRCPlayerApi player = Networking.LocalPlayer;
        var card = GetCard(player);
        if (card == null) return;
        card.SendCustomNetworkEvent(NetworkEventTarget.All,"SetIndicatorRed");
    }

    public void SetPlayerYellow()
    {
        VRCPlayerApi player = Networking.LocalPlayer;
        var card = GetCard(player);
        if (card == null) return;
        card.SendCustomNetworkEvent(NetworkEventTarget.All,"SetIndicatorYellow");
    }

    public void SetPlayerGreen()
    {
        VRCPlayerApi player = Networking.LocalPlayer;
        var card = GetCard(player);
        if (card == null) return;
        card.SendCustomNetworkEvent(NetworkEventTarget.All,"SetIndicatorGreen");
    }
    
    public void SetBadgeHeight(float value)
    {
        VRCPlayerApi player = Networking.LocalPlayer;
        var card = GetCard(player);
        if (card == null) return;
        card.y_offset = value;
    }

    public void SetBadgeScale(float value)
    {
        VRCPlayerApi player = Networking.LocalPlayer;
        var card = GetCard(player);
        if (card == null) return;
        card.cardScale = value;
    }


    public override void OnPlayerJoined(VRCPlayerApi player)
    {
        base.OnPlayerJoined(player);
        SpawnCard(player);
    }

    public override void OnPlayerLeft(VRCPlayerApi player)
    {
        RemoveCard(player);
        base.OnPlayerLeft(player);
    }
}
