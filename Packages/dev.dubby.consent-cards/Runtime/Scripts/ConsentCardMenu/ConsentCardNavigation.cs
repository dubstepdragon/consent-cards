
using System.Linq;
using UdonSharp;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

public class ConsentCardNavigation : UdonSharpBehaviour
{
    public ConsentCardManager manager;
    
    public Toggle worldMasterSettingsPageToggle;
    
    public GameObject infoPage;
    public GameObject settingsPage;
    public GameObject worldMasterSettingsPage;
    
    public Slider heightSlider;
    public Slider scaleSlider;
    
  
    
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
    
    
    public void SetPlayerRed()
    {
        manager.SetPlayerRed();
    }

    public void SetPlayerYellow()
    {
        manager.SetPlayerYellow();
    }

    public void SetPlayerGreen()
    {
        manager.SetPlayerGreen();
    }


    public void SetBadgeHeight()
    {
        manager.SetBadgeHeight(heightSlider.value);
    }

    public void SetBadgeScale()
    {
        manager.SetBadgeScale(scaleSlider.value);
    }
    

 
}
