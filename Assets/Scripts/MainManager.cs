using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
//using UnityEditor.SearchService;
//using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public GameObject OptionIndex;
    public Transform Pos1;
    public Transform Pos2;

    private void Awake()
    {

    }
    void Start()
    {
        OptionIndex.transform.position = Pos1.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)){
            MoveUp();
        }
        if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)){
            MoveDowm();
        }
        if(Input.GetKeyDown(KeyCode.Space)){
            if(OptionIndex.transform.position == Pos1.position){
                //Start Game
                SceneManager.LoadScene("Game");
            }
        }
    }
    private void MoveDowm(){
        OptionIndex.transform.position = Pos2.position;
    }
    private void MoveUp(){
        OptionIndex.transform.position = Pos1.position;
    }
}
