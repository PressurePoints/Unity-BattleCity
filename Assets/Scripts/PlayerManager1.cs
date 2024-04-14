using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager1 : MonoBehaviour
{
    public int playerLife = 3;
    public int playerScore = 0;
    public bool isDead = false;
    public GameObject Born;
    public Vector3 BornPos;
    public TMP_Text Text_PlayerLife;
    public TMP_Text Text_PlayerScore;
    private static PlayerManager1 instance;
    public static PlayerManager1 Instance{
        get{
            return instance;
        }
    }
    private PlayerManager1(){}
    private void Awake(){
        instance = this;
        Text_PlayerLife.text = playerLife.ToString();
        Text_PlayerScore.text = playerScore.ToString();
    }
    void Update(){
        if(isDead){
            Recover();
        }
    }
    private void Recover(){
        if(playerLife <= 1){
            //game over
            Text_PlayerLife.text = "0";
            GameData.Instance.PlayerScore = playerScore;
            SceneManager.LoadScene("Defeat");          
        }
        else{
            playerLife--;
            Text_PlayerLife.text = playerLife.ToString();
            isDead = false;
            Instantiate(Born,BornPos,Quaternion.identity);
        }
    }
}
