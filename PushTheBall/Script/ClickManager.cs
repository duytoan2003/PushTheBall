using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ClickManager : MonoBehaviour
{
    public TextMeshProUGUI textObject;
    private void Start()
    {
        if (textObject != null)
        {
            
            textObject.text = "Level " + SceneManager.GetActiveScene().buildIndex;
        }
        else
        {
            Debug.LogError("Text Object ko t?n t?i !");
        }
        if (SceneManager.GetActiveScene().buildIndex % 2 == 0)
        {
            BannerAd.instance.ShowBannerAd();
        }
        else
        {
            BannerAd.instance.HideBannerAd();
        }
    }
    public void home()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void resett()
    {
        int ad = PlayerPrefs.GetInt("canad", 1);
        ad++;
        PlayerPrefs.SetInt("canad", ad);
        Debug.Log(ad);
        if (ad == 10)
        {
            InterstitialAd.instance.LoadAd();
        }
        if (ad % 20 == 0)
        {
            PlayerPrefs.SetInt("canad", 0);
            InterstitialAd.instance.ShowAd();

        }
        Manager.instance.reset();
    }
}
