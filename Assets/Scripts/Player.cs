
// using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    
    [SerializeField]
    private GameObject[] weapons;
    private int weaponIndex =0;

    [SerializeField]
    private Transform shootTransform;

    [SerializeField]
    private float shootInterval = 0.05f;
    private float lastShotTime = 0f;
    

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float toX = Mathf.Clamp(mousePos.x, -2.35f, 2.35f);
        // Debug.Log(mousePos);
        transform.position = new Vector3(toX, transform.position.y, transform.position.z);

        if(GameManager.instance.isGameOver == false){
            Shoot();
        }
    }

    void Shoot(){
        // 10 -0 > 0.05 
        if(Time.time - lastShotTime > shootInterval){
            Instantiate(weapons[weaponIndex], shootTransform.position, Quaternion.identity); //Quaternion 아무 회전 없음
            lastShotTime = Time.time;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Enemy" || other.gameObject.tag =="BossEnemy"){
            // Debug.Log("적과 충돌");
            GameManager.instance.DecreaseHp();

            if(GameManager.instance.hp ==0){
				Destroy(gameObject);
				// Invoke("DelayedGameOver", 2f);
				// StartCoroutine(DelayedGameOver());
				GameManager.instance.SetGameOver();
            }

        } else if(other.gameObject.tag =="Coin"){
            Destroy(other.gameObject); // 코인제거
            GameManager.instance.IncreaseScore();

			// 예시: 코인 점수에 따라 HP 증가
			if (GameManager.instance.score % 10 == 0){
				GameManager.instance.IncreaseHp();
			}
            if (GameManager.instance.score % 40 ==0){
				UpgradeWeapon();
            }

        }
    }

	// private void DelayedGameOver(){
	// 	GameManager.instance.SetGameOver();
	// }
	// private IEnumerator DelayedGameOver()
	// {
	// 	yield return new WaitForSeconds(2f);
	// 	GameManager.instance.SetGameOver();
	// }
    void UpgradeWeapon(){
		weaponIndex++;
		if (weaponIndex >= weapons.Length)
		{
			weaponIndex = weapons.Length - 1; // 최후 인덱스
		}
    }
}
