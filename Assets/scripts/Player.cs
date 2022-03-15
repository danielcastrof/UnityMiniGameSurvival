using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float Speed;
    public float JumpForce;
    public bool Pulando, PuloDuplo;
    public Rigidbody2D Grav;
    private Animator an;

    // Start is called before the first frame update
    void Start()
    {
        Grav = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Mover();
        Pular();
    }

    void Mover(){
        Vector3 movimento = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movimento * Time.deltaTime * Speed;
        
        if(Input.GetAxis("Horizontal") > 0f){
        an.SetBool("walk", true);
        transform.eulerAngles = new Vector3(0f,0f,0f);
        }

        if(Input.GetAxis("Horizontal") < 0f){
        an.SetBool("walk", true);
        transform.eulerAngles = new Vector3(0f,180f,0f);
        }

        if(Input.GetAxis("Horizontal") == 0f){
        an.SetBool("walk", false);
        }
    }

    void Pular(){
        if(Input.GetButtonDown("Jump")){
            if(!Pulando){
            Grav.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
            PuloDuplo = true;
            an.SetBool("jump", true);
        }
        else{
            if(PuloDuplo){
                Grav.AddForce(new Vector2(0f, JumpForce/2), ForceMode2D.Impulse);
                PuloDuplo = false;
            }
        }
           
        }
    }


    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.layer == 3){
            Pulando = false;
            an.SetBool("jump", false);
        }

        if(collision.gameObject.tag == "Kill"){
            SceneManager.LoadScene("SampleScene");
        }
    }

    void OnCollisionExit2D(Collision2D collision){
        if(collision.gameObject.layer == 3){
            Pulando = true;
        }
    }

}
