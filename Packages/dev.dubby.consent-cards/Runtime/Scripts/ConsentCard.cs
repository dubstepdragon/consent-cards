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
    /// Red = 0
    /// Green = 1
    /// Yellow = 2
    /// </summary>
    [UdonSynced] public int indicatorState = 0;
    
    

    [UdonSynced]
    public float y_offset = 0.2f;

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
        SetIndicator(0);
    }

    public override void OnDeserialization()
    {
        base.OnDeserialization();
        SetIndicator(indicatorState);
    }

    public void SetIndicator(int state)
    {
        switch (state)
        {
            case 0:
                SetIndicatorRed();
                break;
            case 1:
                SetIndicatorGreen();
                break;
            case 2:
                SetIndicatorYellow();
                break;
            default:
                SetIndicatorRed();
                break;
        }
        RequestSerialization();
    }
    

    
    public void SetIndicatorRed()
    {
        indicatorState = 0;
    }
    
    public void SetIndicatorGreen()
    {
        indicatorState = 1;
    }
    
    
    public void SetIndicatorYellow()
    {
        indicatorState = 2;
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

    public void Update()
    {
        switch (indicatorState)
        {
            case 0:
                frontRed.SetActive(true);
                frontGreen.SetActive(false);
                frontYellow.SetActive(false);
        
                backRed.SetActive(true);
                backGreen.SetActive(false);
                backYellow.SetActive(false);
                break;
            case 1:
                frontRed.SetActive(false);
                frontGreen.SetActive(true);
                frontYellow.SetActive(false);
        
                backRed.SetActive(false);
                backGreen.SetActive(true);
                backYellow.SetActive(false);
                break;
            case 2:
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
