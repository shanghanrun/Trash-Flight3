

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemies;
    [SerializeField]
    private GameObject bossEnemy;
    private float[] arrPosX = { -2.2f, -1.1f, 0, 1.1f, 2.2f };

    [SerializeField]
    private float spawnInterval = 1.5f;
    
    void Start()
    {
        StartEnemyRoutine();
    }  
    void StartEnemyRoutine(){
        StartCoroutine("EnemyRoutine");
    }

    public void StopEnemyRoutine(){
        Debug.Log("EnemySpawner의 StopEnemyRoutine 호출됨");
        StopCoroutine("EnemyRoutine");
    }

    IEnumerator EnemyRoutine(){
        yield return new WaitForSeconds(3f); // 처음 시작시 몇초 기다린다.

        float moveSpeed = 5f;
        float maxSpeed = 20f;
        int spawnCount =0;
        int enemyIndex =0;
        // int chance =0;
        while(true){  // 무한 반복
            foreach (float posX in arrPosX)  // 5개 에너미 만들기
            {
                enemyIndex = Random.Range(0, enemies.Length);
                if(spawnCount < 5 && enemyIndex >1){
                    enemyIndex = 0;
                } else if(spawnCount<10 && enemyIndex > 2){
                    enemyIndex =1; 
                } else if(spawnCount<20 && enemyIndex >3){
					enemyIndex = 2; 
                } else if(spawnCount<30 && enemyIndex >4){
					enemyIndex = 3; 
                } else if(spawnCount<40 && enemyIndex >5){
					enemyIndex = 4; 
                } else if(spawnCount>=40 && spawnCount<60 && enemyIndex <5){ //!
					enemyIndex = 5; 
                } else if(spawnCount>=60){
					enemyIndex = 6;
                } 
                
                // chance = Random.Range(1, 100);
                if (spawnCount >50){   // 무조건 생성
                    SpawnEnemy(posX, enemyIndex, moveSpeed); 
                } else {
                    // if (chance > 7){  // 93 프로로 생성
                        SpawnEnemy(posX, enemyIndex, moveSpeed);
                    // }
                }
            }
            spawnCount++;
            if(spawnCount % 10 ==0){
                moveSpeed +=2;
            }
            if(moveSpeed >=maxSpeed){ // 최대속도 제한
                moveSpeed = maxSpeed;
            }

			if (enemyIndex >= enemies.Length - 1){ // 최후에너미에 도달
                SpawnBossEnemy();
                //그런데 보스가 등장하면, 에너미들을 초기화(약한 것으로)
                enemyIndex = 0;
                spawnCount = 0; // 실제로는 spawnCount에 의해서 enemyIndex가 결정되는 구조이다.
                moveSpeed = 5f;
			}

            yield return new WaitForSeconds(spawnInterval);//에너미 생성주기
        }
    }
    

    void SpawnEnemy(float posX, int eIndex, float moveSpeed){
        //에너미 만들어지는 위치
        Vector3 spawnPos = new Vector3(posX, transform.position.y, transform.position.z);
        GameObject enemyObject = Instantiate(enemies[eIndex],spawnPos, Quaternion.identity ); 
        Enemy enemy = enemyObject.GetComponent<Enemy>();
        enemy.SetMoveSpeed(moveSpeed);
    }

    void SpawnBossEnemy(){
        // 등장위치는 화면중앙  EnemySpawner의 위치(Vector3값)로 하면 된다.
        Instantiate(bossEnemy, transform.position, Quaternion.identity);
    }
}
