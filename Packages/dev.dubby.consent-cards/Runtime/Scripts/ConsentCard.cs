
using System;
using UdonSharp;
using UnityEngine;
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
        frontRed.SetActive(true);
        frontGreen.SetActive(false);
        frontYellow.SetActive(false);
        
        backRed.SetActive(true);
        backGreen.SetActive(false);
        backYellow.SetActive(false);
    }
    
    public void SetIndicatorGreen()
    {
        indicatorState = IndicatorState.Green;
        frontRed.SetActive(false);
        frontGreen.SetActive(true);
        frontYellow.SetActive(false);
        
        backRed.SetActive(false);
        backGreen.SetActive(true);
        backYellow.SetActive(false);
    }
    
    public void SetIndicatorYellow()
    {
        indicatorState = IndicatorState.Yellow;
        frontRed.SetActive(false);
        frontGreen.SetActive(false);
        frontYellow.SetActive(true);
        
        backRed.SetActive(false);
        backGreen.SetActive(false);
        backYellow.SetActive(true);
    }
}
