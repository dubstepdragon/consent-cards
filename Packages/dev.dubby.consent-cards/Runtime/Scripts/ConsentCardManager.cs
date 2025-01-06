
using System;
using System.Collections.Generic;
using Cyan.PlayerObjectPool;
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
    
    
    public VRCPlayerApi playerAssignedPlayer;
    [HideInInspector]
    public UdonBehaviour playerAssignedPoolObject;



    public void _OnPlayerAssigned()
    {
        //playerAssignedPlayer.gameObject.SetActive(true);
        ConsentCard cc = playerAssignedPoolObject.GetComponent<ConsentCard>();
        cc.owningPlayerId = playerAssignedPlayer.playerId;
        cc.gameObject.SetActive(true);
        playersAndCards.Add(playerAssignedPlayer.playerId, playerAssignedPoolObject.GetComponent<ConsentCard>());
    }

    public void _OnPlayerUnassigned()
    {
        ConsentCard cc = GetCard(playerAssignedPlayer);
        cc.gameObject.SetActive(false);
        playersAndCards.Remove(playerAssignedPlayer.playerId);
    }
    

    private ConsentCard GetCard(VRCPlayerApi player)
    {
        if (playersAndCards.TryGetValue(player.playerId, TokenType.Reference, out var cardRef))
        {
            ConsentCard cc = (ConsentCard)cardRef.Reference;
            return (ConsentCard)cardRef.Reference;
        }
        return null;
    }


    public void SetPlayerIndicator(int state)
    {
        Debug.Log("Clicked to change indicator to " + state);
        VRCPlayerApi player = Networking.LocalPlayer;
        var card = GetCard(player);
        if (card == null) return;
        card.indicatorState = state;
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


    
    
}
