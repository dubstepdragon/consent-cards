
using System;
using UdonSharp;
using UnityEngine;
using VRC.SDK3.Data;
using VRC.SDKBase;
using VRC.Udon;

public class ConsentCardManager : UdonSharpBehaviour
{
    
    public DataDictionary playersAndCards = new DataDictionary();
    
    public GameObject cardPrefab;
    

    

    public void ResetCard(VRCPlayerApi player)
    {
        if (playersAndCards.TryGetValue(player.playerId, TokenType.Reference, out var cardToRemove))
        {

        }
    }

    public override void OnAvatarEyeHeightChanged(VRCPlayerApi player, float prevEyeHeightAsMeters)
    {
        base.OnAvatarEyeHeightChanged(player, prevEyeHeightAsMeters);
        
        
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
        var cc = GetCard(player);
        if(cc != null )
        {
            Destroy(cc.gameObject);
        }
        playersAndCards.Remove(player.playerId);
    }

    public ConsentCard GetCard(VRCPlayerApi player)
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
        card.SetIndicatorRed();
    }

    public void SetPlayerYellow()
    {
        VRCPlayerApi player = Networking.LocalPlayer;
        var card = GetCard(player);
        if (card == null) return;
        card.SetIndicatorYellow();
    }

    public void SetPlayerGreen()
    {
        VRCPlayerApi player = Networking.LocalPlayer;
        var card = GetCard(player);
        if (card == null) return;
        card.SetIndicatorGreen();
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
