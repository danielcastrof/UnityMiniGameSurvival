using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    private Rigidbody2D rigd;
    private Animator anim;
    public float speed;
    public Transform rgt;
    public Transform lft;
    public Transform head;
    private bool collid;
    private bool playerDead;
    public LayerMask layer;

    
    // Start is called before the first frame update
    void Start()
    {
        rigd = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rigd.velocity = new Vector2(speed, rigd.velocity.y);

        collid = Physics2D.Linecast(rgt.position, lft.position, layer);

        if(collid){
            transform.localScale = new Vector2(transform.localScale.x * -1f, transform.localScale.y);
            speed *= -1f;
        }
    }

    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Player" && !playerDead){
            float height = collision.contacts[0].point.y - head.position.y;

            if(height > 0){
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 8);
                speed = 0;
                anim.SetTrigger("die");
                Destroy(gameObject, 0.3f);
            }
            else{
                playerDead = true;
                Destroy(collision.gameObject);
                SceneManager.LoadScene("SampleScene");
            }
        }
    }

}