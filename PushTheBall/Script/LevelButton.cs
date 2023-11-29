using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    // Start is called before the first frame update
    public int sceneIndex;
    public Image[] images;
    // Update is called once per frame
    public void OpenScene()
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
