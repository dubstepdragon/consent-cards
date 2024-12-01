
using System.Linq;
using UdonSharp;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

public class ConsentCardNavigation : UdonSharpBehaviour
{
    public Toggle worldMasterSettingsPageToggle;
    
    public GameObject infoPage;
    public GameObject settingsPage;
    public GameObject worldMasterSettingsPage;
    
  
    
    void Start()
    {
        SetInfoPage();
    }

    public override void OnPlayerJoined(VRCPlayerApi player)
    {
        worldMasterSettingsPageToggle.interactable = Networking.IsMaster;
    }

    public void SetInfoPage()
    {
        infoPage.SetActive(true);
        settingsPage.SetActive(false);
        worldMasterSettingsPage.SetActive(false);
    }

    public void SetSettingsPage()
    {
        infoPage.SetActive(false);
        settingsPage.SetActive(true);
        worldMasterSettingsPage.SetActive(false);
    }

    public void SetMasterSettingsPage()
    {
        infoPage.SetActive(false);
        settingsPage.SetActive(false);
        worldMasterSettingsPage.SetActive(true);
    }
    
    
    

 
}
