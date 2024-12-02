
using System;
using UdonSharp;
using UnityEngine;
using UnityEngine.Serialization;
using VRC.SDKBase;
using VRC.Udon;

public enum IndicatorState
{
    Red,
    Green,
    Yellow
}


public class ConsentCard : UdonSharpBehaviour
{
    [UdonSynced]
    public int owningPlayerId;
    
    [UdonSynced]
    public IndicatorState indicatorState = IndicatorState.Red;
    
    
    //TODO: make this a height slider
    [UdonSynced]
    public float y_offset = 0.2f;

    //TODO: make this a size slider
    [UdonSynced]
    public float cardScale = 1.0f;
    
 
    public GameObject frontRed;
    public GameObject frontGreen;
    public GameObject frontYellow;
    

    public GameObject backRed;
    public GameObject backGreen;
    public GameObject backYellow;
    
   
    
    void Start()
    {
        SetIndicator(IndicatorState.Red);
    }

    public void SetIndicator(IndicatorState state)
    {
        switch (state)
        {
            case IndicatorState.Red:
                SetIndicatorRed();
                break;
            case IndicatorState.Green:
                SetIndicatorGreen();
                break;
            case IndicatorState.Yellow:
                SetIndicatorYellow();
                break;
            default:
                SetIndicatorRed();
                break;
        }
    }
    

    public void SetIndicatorRed()
    {
        indicatorState = IndicatorState.Red;
        
    }
    
    public void SetIndicatorGreen()
    {
        indicatorState = IndicatorState.Green;
        
    }
    
    public void SetIndicatorYellow()
    {
        indicatorState = IndicatorState.Yellow;
        
    }

    public void FixedUpdate()
    {
        if (owningPlayerId == -1) return;
        
        var owningPlayer = VRCPlayerApi.GetPlayerById(owningPlayerId);
        
        var headPos = owningPlayer.GetBonePosition(HumanBodyBones.Head);
        var Rot = owningPlayer.GetRotation();
        
        headPos.y += y_offset;
        
        this.transform.position = headPos;
        transform.localScale = new Vector3(cardScale, cardScale, cardScale);
    }

    public void Update()
    {
        switch (indicatorState)
        {
            case IndicatorState.Red:
                frontRed.SetActive(true);
                frontGreen.SetActive(false);
                frontYellow.SetActive(false);
        
                backRed.SetActive(true);
                backGreen.SetActive(false);
                backYellow.SetActive(false);
                break;
            case IndicatorState.Green:
                frontRed.SetActive(false);
                frontGreen.SetActive(true);
                frontYellow.SetActive(false);
        
                backRed.SetActive(false);
                backGreen.SetActive(true);
                backYellow.SetActive(false);
                break;
            case IndicatorState.Yellow:
                frontRed.SetActive(false);
                frontGreen.SetActive(false);
                frontYellow.SetActive(true);
        
                backRed.SetActive(false);
                backGreen.SetActive(false);
                backYellow.SetActive(true);
                break;
            default:
                frontRed.SetActive(true);
                frontGreen.SetActive(false);
                frontYellow.SetActive(false);
        
                backRed.SetActive(true);
                backGreen.SetActive(false);
                backYellow.SetActive(false);
                break;
        }
    }
}
