using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bot : MonoBehaviour
{
    public Transform posA, posB;
    public int speed;
    public Vector2 vec;    // Start is called before the first frame update
    public LayerMask player;
    public Transform playerCheck;
    public bool Check;
    public Collider2D colid;
    public Rigidbody2D rb;
    public float fallForce;
    public Animator ani;
    int d=0;
    void Start()
    {
        vec = posA.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, posB.position) < 1.1f)
        {
            rb.transform.localScale = new Vector3(1, 1, 1);
            vec = posA.position;
        }
        if (Vector2.Distance(transform.position, posA.position) < 1.1f)
        {
            rb.transform.localScale = new Vector3(-1, 1, 1);
            vec = posB.position;
        }
        transform.position = Vector2.MoveTowards(transform.position, vec, speed * Time.deltaTime);
        Check = Physics2D.OverlapBox(playerCheck.position, new Vector2(0.8374434f, 0.04633856f), 0, player);
        
    }
    private void FixedUpdate()
    {
        if (Check&&d==0)
        {
            rb.velocity= new Vector2(rb.velocity.x, fallForce );
            colid.enabled = false;
            ani.SetTrigger("Hit");
            d++;
        }
    }
    void OnCollisionEnter2D(Collision2D nhanvat)
    {
        Player nguoidk1 = nhanvat.gameObject.GetComponent<Player>();
        if (nguoidk1 != null)
        {
            nguoidk1.changeHeal(-1);
        }
    }
}
