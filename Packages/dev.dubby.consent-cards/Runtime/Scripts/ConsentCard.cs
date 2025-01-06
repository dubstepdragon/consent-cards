using System;
using UdonSharp;
using UnityEngine;
using UnityEngine.Serialization;
using VRC.SDKBase;
using VRC.Udon;



public class ConsentCard : UdonSharpBehaviour
{
    [UdonSynced]
    public int owningPlayerId;
    
    /// <summary>
    /// Off = 0
    /// Red = 1
    /// Yellow = 2
    /// Green = 3
    /// </summary>
    [UdonSynced] public int indicatorState = 0;
    
    

    [UdonSynced]
    public float y_offset = 0.2f;

    [UdonSynced]
    public float cardScale = 1.0f;
    

    public GameObject frontIndicators;
    public GameObject backIndicators;

    public GameObject frontCanvas;
    public GameObject backCanvas;

    public GameObject cardModels;
   
    
    void Start()
    {
        SetIndicator(0);
    }

    public override void OnDeserialization()
    {
        base.OnDeserialization();
        SetIndicator(indicatorState);
    }

    /// <summary>
    /// Indicators on the card are one less because actual indicators range from 1-n with 0 being "Off"
    /// so the front and back indicators array will always be one less than the button array
    ///
    /// so we get the "current state" by subtracting one from what ever state is coming in, if the state is 0, we turn off the badge
    ///
    /// We also check if the front and back indicators have the same child count, they SHOULD if they don't that's a problem
    /// </summary>
    /// <param name="state"></param>
    private void SetIndicator(int state)
    {
        if (state == 0)
        {
            SetBadgeOff();
            return;
        }
        
        frontCanvas.SetActive(true);
        backCanvas.SetActive(true);
        cardModels.SetActive(true);
        
        if (frontIndicators.transform.childCount != backIndicators.transform.childCount) return;
        
        int inState = state - 1;
        for (int i = 0; i < frontIndicators.transform.childCount; i++)
        {
            frontIndicators.transform.GetChild(i).gameObject.SetActive(i == inState);
            backIndicators.transform.GetChild(i).gameObject.SetActive(i == inState);
        }
        RequestSerialization();
    }

    private void SetBadgeOff()
    {
        frontCanvas.SetActive(false);
        backCanvas.SetActive(false);
        cardModels.SetActive(false);
    }
    

    public void FixedUpdate()
    {
        if (owningPlayerId == -1) return;
        
        var owningPlayer = VRCPlayerApi.GetPlayerById(owningPlayerId);
        if (owningPlayer == null) return;
        var headPos = owningPlayer.GetBonePosition(HumanBodyBones.Head);
        
        headPos.y += y_offset;
        
        this.transform.position = headPos;
        transform.localScale = new Vector3(cardScale, cardScale, cardScale);
    }


    private int previousIndicatorState = 0;
    
    public void Update()
    {
        if(indicatorState != previousIndicatorState)
        {
            SetIndicator(indicatorState);
        }
        
        previousIndicatorState = indicatorState;
    }
}
