
using TMPro;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class IconButton : UdonSharpBehaviour
{
    public ConsentCardNavigation cardNavigation;
    
    public override void Interact()
    {
        cardNavigation.manager.SetPlayerIndicator(transform.GetSiblingIndex());
    }
    
}
