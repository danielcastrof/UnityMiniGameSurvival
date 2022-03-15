using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opponent : MonoBehaviour
{
    public float speed;
    public float moveTime;

    private bool rig = true;
    private float time;

    public Transform head;


    // Update is called once per frame
    void Update()
    {
        if(rig){
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else{
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }

        time += Time.deltaTime;
        if(time >= moveTime){
            rig = !rig;
            time = 0f;
        }
    }

   
}
