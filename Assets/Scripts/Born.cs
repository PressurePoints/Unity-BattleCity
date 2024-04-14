using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Born : MonoBehaviour
{
    // Start is called before the first frame update
    const int ENEMYNUMS = 2;
    public bool isCreatePlayer = false;
    public GameObject playerPrefab;
    public GameObject[] enemyPrefabList;
    public void CreateTank(){
        if(isCreatePlayer){
            Instantiate(playerPrefab,transform.position,transform.rotation);
        }
        else{
            int num = Random.Range(0,ENEMYNUMS);
            Instantiate(enemyPrefabList[num],transform.position,transform.rotation);
        }
    }
    private void Destroy(){
        Destroy(gameObject);
    }
    void Start()
    {
        Invoke("CreateTank",1f);
        Invoke("Destroy",1.1f);
    }

    // Update is called once per frame
    void Update()
    {

    }

}
