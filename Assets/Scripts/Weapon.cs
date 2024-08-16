
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed =10f;
    public float damage = 1f;  // 다른 곳에서 접근 가능하게

    // Start는 처음 생성되고 한번 실행되고 그만 두는 메소드
    void Start(){
        Destroy(gameObject, 1f); // 1초뒤에 없애겠다는 것을 예약한다.
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;

    }
}
