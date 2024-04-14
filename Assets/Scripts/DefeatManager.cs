using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class DefeatManager : MonoBehaviour
{

    public TMP_Text Text_PlayerScore;
    public TMP_Text Text_PlayerLife;
    private void Awake()
    {
        Text_PlayerScore.text = GameData.Instance.PlayerScore.ToString();
        Text_PlayerLife.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            SceneManager.LoadScene("Game");
        }
        if(Input.GetKeyDown(KeyCode.Escape)){
            SceneManager.LoadScene("Main");
        }

    }
}
