using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResolutionManager : MonoBehaviour
{
    [SerializeField] protected TMP_Dropdown ResolutionDropdown;
    [SerializeField] protected Toggle fullScreenToggle;

    [SerializeField] resolution[] resolutions;
    protected List<Resolution> allResolutions = new List<Resolution>();
    protected bool isFullScreen;
    protected int selectedResolution;
    protected List<Resolution> selectedResolutionList = new List<Resolution>();

    [System.Serializable]
    public struct resolution
    {
        public int w;
        public int h;
    }
    private void Start()
    {
        isFullScreen = false;
       var rate =  Screen.currentResolution.refreshRateRatio;
            for(int i = 0; i < resolutions.Length; i++)
        {
            var res = new Resolution();
            res.height = resolutions[i].w;
            res.width = resolutions[i].h;
            res.refreshRateRatio = rate;
            allResolutions.Add(res);

        }

        string newRes;
        List<string> resolutionStringList = new List<string>();
        foreach (Resolution res in allResolutions)
        {
            newRes = res.width.ToString() + " x " + res.height.ToString();
            if (!resolutionStringList.Contains(newRes))
            {
                resolutionStringList.Add(newRes);
                selectedResolutionList.Add(res);
            }
        }

        ResolutionDropdown.AddOptions(resolutionStringList);
    }

    public void ChangeResolution()
    {
        selectedResolution = ResolutionDropdown.value;
        Screen.SetResolution(selectedResolutionList[selectedResolution].width, selectedResolutionList[selectedResolution].height, isFullScreen);
    }

    public void ChangeFullScreen()
    {
        isFullScreen = fullScreenToggle.isOn;
        Screen.SetResolution(selectedResolutionList[selectedResolution].width, selectedResolutionList[selectedResolution].height, isFullScreen);
    }

}
