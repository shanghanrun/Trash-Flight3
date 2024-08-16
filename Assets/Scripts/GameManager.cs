

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private TextMeshProUGUI hpText;
    [SerializeField]
    private GameObject gameOverPanel;
    
    public static GameManager instance = null;


    public int score =0;
    [SerializeField]
    public int hp =2; // 외부에서 조정가능하게

    [HideInInspector]
    public bool isGameOver = false;

    void Awake(){
        if(instance == null){
            instance = this;
        }
        hpText.SetText(hp.ToString());//시작과 함께 체력표시
    }


    public void IncreaseScore(){
        score++;
        scoreText.SetText(score.ToString());
    }

    public void DecreaseHp(){
        if(hp>=1){
            hp--;
        }
        hpText.SetText(hp.ToString());
    }
    public void IncreaseHp(){
        hp++;
        hpText.SetText(hp.ToString());
    }

    public void SetGameOver(){
        isGameOver = true;
        Debug.Log("SetGameOver 호출됨");

        EnemySpawner enemySpawner = FindObjectOfType<EnemySpawner>();
        if(enemySpawner != null){
            Debug.Log("StopEnemyRouting 호출됨");
            enemySpawner.StopEnemyRoutine();
        }
        Invoke("ShowGameOver", 1f);
        
    }
	void ShowGameOver(){
		gameOverPanel.SetActive(true);
	}
    public void PlayAgain(){
        SceneManager.LoadScene("SampleScene");
    }
}

