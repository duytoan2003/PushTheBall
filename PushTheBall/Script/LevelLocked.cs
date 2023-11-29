
using UnityEngine;
using UnityEngine.UI;

public class LevelLocked : MonoBehaviour
{
    [SerializeField] Button[] buttons;
    int unlockedLevelsNumber;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("levelsUnlocked"))
        {
            PlayerPrefs.SetInt("levelsUnlocked", 1);
        }

        unlockedLevelsNumber = PlayerPrefs.GetInt("levelsUnlocked");

        for (int i = 0; i < 50; i++)
        {
            buttons[i].interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        unlockedLevelsNumber = PlayerPrefs.GetInt("levelsUnlocked");
        if(unlockedLevelsNumber <=50)
        for(int i = 0; i < unlockedLevelsNumber; i++)
        {
            buttons[i].interactable = true;
        }
     
    }
}
