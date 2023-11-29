using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class Manager : MonoBehaviour
{
    // Start is called before the first frame update
    public static Manager instance;
    public Animator a;
    
    private void Start()
    {
    }
    private void Awake()
    {
        
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }   
    public void next()
    {
        StartCoroutine(loadlv());
    }
    public void reset()
    {
        StartCoroutine(resetlv());
    }
    IEnumerator loadlv()
    {
        a.SetTrigger("End");
        yield return new WaitForSeconds(1);
        if(SceneManager.GetActiveScene().buildIndex+1 <= 84)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        else
           SceneManager.LoadScene("MainMenu");
        a.SetTrigger("Start");
    }
    IEnumerator resetlv()
    {
        yield return new WaitForSeconds(0.5f);
        if (SceneManager.GetActiveScene().buildIndex + 1 <= 84)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex );
        else
            SceneManager.LoadScene("MainMenu");
        a.SetTrigger("Start");
    }
}
