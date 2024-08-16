
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
	private float minY = -6f;
	private Rigidbody2D rigidBody; // Rigidbody2D 변수를 선언

    public Transform playerTransform;// 플레이어의 Transform 참조위한 변수
    void Start()
    {
        Jump(); 
		// 씬 내에서 Player 오브젝트를 찾고 Transform을 가져옵니다.
		Player player = FindObjectOfType<Player>(); // Player 스크립트가 붙어 있는 오브젝트를 찾습니다
		if (player != null)
		{
			playerTransform = player.transform; // Player의 Transform을 가져옵니다
		}
		else
		{
			Debug.LogWarning("Player not found in the scene");
		}

    }

    void Jump(){
        rigidBody = GetComponent<Rigidbody2D>();

        float randomJumpForce = Random.Range(4f,8f); // 4.0, 4.12, 5.6..
        Vector2 jumpVelocity = Vector2.up * randomJumpForce;
        jumpVelocity.x = Random.Range(-2f,2f); // 좌우로도 가능

        rigidBody.AddForce(jumpVelocity, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        // 플레이어쪽으로 이동
        if(playerTransform !=null){
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            rigidBody.velocity = new Vector2(direction.x *3f, rigidBody.velocity.y); // 2f는 이동속도 조절
        }
        if(transform.position.y< minY){
            Destroy(gameObject);
        }
    }
}
