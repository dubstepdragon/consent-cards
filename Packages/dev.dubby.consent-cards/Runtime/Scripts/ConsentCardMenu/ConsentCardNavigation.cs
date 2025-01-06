
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
    public GameObject moreCustomizationsPage;
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

    //TODO: Change pages to be more expandable maybe
    public void SetInfoPage()
    {
        infoPage.SetActive(true);
        settingsPage.SetActive(false);
        moreCustomizationsPage.SetActive(false);
        worldMasterSettingsPage.SetActive(false);
    }

    public void SetSettingsPage()
    {
        infoPage.SetActive(false);
        settingsPage.SetActive(true);
        moreCustomizationsPage.SetActive(false);
        worldMasterSettingsPage.SetActive(false);
    }

    public void SetCustomizationPage()
    {
        infoPage.SetActive(false);
        settingsPage.SetActive(false);
        moreCustomizationsPage.SetActive(true);
        worldMasterSettingsPage.SetActive(false);
    }

    public void SetMasterSettingsPage()
    {
        infoPage.SetActive(false);
        settingsPage.SetActive(false);
        moreCustomizationsPage.SetActive(false);
        worldMasterSettingsPage.SetActive(true);
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
