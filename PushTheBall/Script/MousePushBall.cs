
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MousePushBall : MonoBehaviour
{
    public Rigidbody2D ballRigidbody; // Tham chi?u t?i Rigidbody2D c?a qu? bóng
    public float maxDragDistance = 5f; // Kho?ng cách kéo t?i ?a
    private Vector3 dragStartPosition; // V? trí b?t ??u kéo
    private Vector3 dragEndPosition; // V? trí k?t thúc kéo
    private bool isDragging = false; // Bi?n ?ánh d?u vi?c kéo chu?t
    private float minDragDistance = 0.1f; // Kho?ng cách kéo t?i thi?u ?? b?n qu? bóng
    private Vector3  pushDirection; // H??ng b?n
    //Vector3 lastVelocity;
    public float force;
    public LineRenderer dragLineRenderer;
    //public GameObject[] clickCount;
    public int click;
    public float time,timeEnd;
    public AudioSource audio;
    public AudioClip cl;
    public AudioClip end;
    public float minVelocityMagnitude = 0.1f;
    bool stop;
    bool nex = true;
    public Slider slider;
    bool canad;
    public bool checkEnd;
    public GameObject ad;
    private void Start()
    {
        checkEnd = false;
        canad = true;
        if (PlayerPrefs.HasKey("Volume"))
        {
            loadVolum();
        }
        else
        {
            setVolum();
        }
        
    }
    void Update()
    {   
        
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began && click > 0)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

                if (hit.collider != null && hit.collider.CompareTag("Player"))
                {
                    Debug.Log("??i t??ng có tag 'Player' ?ã ???c ch?m vào!");
                    isDragging = true;
                    dragStartPosition = Camera.main.ScreenToWorldPoint(touch.position);
                }
            }
            else if (touch.phase == TouchPhase.Ended && isDragging)
            {
                audio.PlayOneShot(cl);
                isDragging = false;
                dragEndPosition = Camera.main.ScreenToWorldPoint(touch.position);

                float dragDistance = Vector3.Distance(dragStartPosition, dragEndPosition);

                if (dragDistance > minDragDistance)
                {
                    pushDirection = (dragStartPosition - dragEndPosition).normalized;
                    float pushForce = Mathf.Clamp(dragDistance, 0, maxDragDistance);
                    ballRigidbody.AddForce(pushDirection * pushForce * force, ForceMode2D.Impulse);
                }
                click -= 1;
                //clickCount[click].SetActive(false);
            }

            if (isDragging)
            {
                Vector3 dragCurrentPosition = Camera.main.ScreenToWorldPoint(touch.position);
                float dragDistance = Vector3.Distance(dragStartPosition, dragCurrentPosition);
                dragDistance = Mathf.Min(dragDistance, maxDragDistance);
                dragLineRenderer.enabled = true;
                dragLineRenderer.SetPosition(0, transform.position);
                dragLineRenderer.SetPosition(1, dragCurrentPosition);
            }
            else
            {
                dragLineRenderer.enabled = false;
            }
        }

        //lastVelocity = ballRigidbody.velocity;
        Vector2 currentVelocity = ballRigidbody.velocity;

        // N?u v?n t?c c?a qu? bóng có ?? l?n nh? h?n ng??ng
        if (currentVelocity.magnitude < minVelocityMagnitude)
        {
            stop = true;   
        }
        else stop = false;
        if (stop&&canad && click == 0)
        {
            InterstitialAl();
            Manager.instance.reset();
            canad = false;   
        }
        setVolum();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("End")&&nex)
        {
            audio.PlayOneShot(end);
            nex = false;
            next();
            checkEnd = true;
            int currentLevel = SceneManager.GetActiveScene().buildIndex;          
            if (currentLevel < 84)
            // L?u giá tr? currentLevel vào PlayerPrefs
            {
                currentLevel++;
                if (currentLevel - 1 == PlayerPrefs.GetInt("CurrentLevel"))
                {
                    PlayerPrefs.SetInt("CurrentLevel", currentLevel);
                    PlayerPrefs.Save();
                }
            }
            else
            {
                PlayerPrefs.SetInt("CurrentLevel", 1);
                PlayerPrefs.Save();
            }
        }
    }
    public void resett()
    {
        InterstitialAl();
        Manager.instance.reset();
    }
    public void next()
    {
        InterstitialAl();
        Manager.instance.next();
    }
    public void setVolum()
    {
        
        float vl = slider.value;
        audio.volume = vl;
        PlayerPrefs.SetFloat("Volume", vl);
    }
    public void loadVolum()
    {
        slider.value = PlayerPrefs.GetFloat("Volume",1);
        setVolum();
    }
    public void InterstitialAl()
    {
        int ad = PlayerPrefs.GetInt("canad", 1);
        ad++;
        PlayerPrefs.SetInt("canad", ad);
        Debug.Log(ad);
        if(ad == 10)
        {
            InterstitialAd.instance.LoadAd();
        }
        if (ad % 20 == 0)
        {
            PlayerPrefs.SetInt("canad", 0);
            InterstitialAd.instance.ShowAd();

        }
    }
}
