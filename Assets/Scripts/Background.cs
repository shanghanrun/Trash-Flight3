using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    private float moveSpeed =3f;

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;  //(0,-1,0)
        if(transform.position.y <-10){
            transform.position += new Vector3(0,20f,0); // 위로 10씩 2번 올린다.
        }
    }
}
