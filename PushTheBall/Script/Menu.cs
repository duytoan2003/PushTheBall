using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    
    public void play()
    {
        if (!PlayerPrefs.HasKey("CurrentLevel"))
        {
            PlayerPrefs.SetInt("CurrentLevel", 1);
        }
        
        int loadedLevel = PlayerPrefs.GetInt("CurrentLevel");   
        SceneManager.LoadScene("Level " + loadedLevel);
    }
    public void resett()
    {
        PlayerPrefs.SetInt("CurrentLevel", 1);
        PlayerPrefs.SetInt("levelsUnlocked", 1);
    }
    
    
}
