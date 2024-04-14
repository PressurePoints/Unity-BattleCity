using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class MapCreate : MonoBehaviour
{
    public GameObject Barrier;
    public GameObject Grass;
    public GameObject Heart;
    public GameObject River;
    public GameObject Wall;
    public GameObject Born;
    public GameObject AirBarrier;

    private static int MapLength = 21; 
    private static int MapWidth = 17; //>=5
    private int MapLengthPos;
    private int MapLengthNeg;
    private int MapWidthPos;
    private int MapWidthNeg;
    private int WallNum = 60;
    private int BarrierNum = 20;
    private int RiverNum = 20;
    private int GrassNum = 20;
    private int EnemyTimeVal = 4;

    private bool[,] ExitItem;
    void Awake(){
        MapLengthPos = MapLength / 2;
        MapLengthNeg = -(MapLength - MapLengthPos - 1);
        MapWidthPos = MapWidth / 2;
        MapWidthNeg = -(MapWidth - MapWidthPos - 1);
        ExitItem = new bool[MapLength,MapWidth];

        CreateBoundary();
        CreateHome(new Vector3(0,MapWidthNeg,0));
        CreatePlayer(new Vector3(0,MapWidthNeg + 2,0));
        PlayerManager1.Instance.BornPos = new Vector3(0,MapWidthNeg + 2,0);
        InvokeRepeating("CreateEnemy",1,EnemyTimeVal);
        ExitItem[MapLengthNeg - MapLengthNeg,MapWidthPos - MapWidthNeg] = true;
        ExitItem[0 - MapLengthNeg,MapWidthPos - MapWidthNeg] = true;
        ExitItem[MapLengthPos - MapLengthNeg,MapWidthPos - MapWidthNeg] = true;
        CreateWall();
        CreateBarrier();
        CreateRiver();
        CreateGrass();
    }
    private void CreateBoundary(){
        for(int i = MapLengthNeg - 1; i <= MapLengthPos + 1; i++){
            CreateItem(AirBarrier,new Vector3(i,MapWidthPos + 1,0),Quaternion.identity);
            CreateItem(AirBarrier,new Vector3(i,MapWidthNeg - 1,0),Quaternion.identity);
        }
        for(int i = MapWidthNeg; i <= MapWidthPos; i++){
            CreateItem(AirBarrier,new Vector3(MapLengthPos + 1,i,0),Quaternion.identity);
            CreateItem(AirBarrier,new Vector3(MapLengthNeg - 1,i,0),Quaternion.identity);
        }
    }
    private void CreateItem(GameObject item, Vector3 position, Quaternion rotation){
        GameObject itemObj = Instantiate(item,position,rotation);   
        itemObj.transform.SetParent(this.transform);
    }
    private void CreateHome(Vector3 position){
        CreateItem(Heart,position,Quaternion.identity);
        ExitItem[(int)position.x - MapLengthNeg,(int)position.y - MapWidthNeg] = true;
        CreateItem(Wall,position + new Vector3(-1,0,0),Quaternion.identity);
        CreateItem(Wall,position + new Vector3(1,0,0),Quaternion.identity);
        CreateItem(Wall,position + new Vector3(-1,1,0),Quaternion.identity);
        CreateItem(Wall,position + new Vector3(1,1,0),Quaternion.identity);
        CreateItem(Wall,position + new Vector3(-1,-1,0),Quaternion.identity);
        CreateItem(Wall,position + new Vector3(1,-1,0),Quaternion.identity);
        CreateItem(Wall,position + new Vector3(0,1,0),Quaternion.identity);
        CreateItem(Wall,position + new Vector3(0,-1,0),Quaternion.identity);
    }
    private void CreateWall(){
        for(int i = 0; i < WallNum; i++){
            CreateItem(Wall,CreateRandomPosition(),Quaternion.identity);
        }
    }
    private void CreateBarrier(){
        for(int i = 0; i < BarrierNum; i++){
            CreateItem(Barrier,CreateRandomPosition(),Quaternion.identity);
        }
    }
    private void CreateRiver(){
        for(int i = 0; i < RiverNum; i++){
            CreateItem(River,CreateRandomPosition(),Quaternion.identity);
        }
    }
    private void CreateGrass(){
        for(int i = 0; i < GrassNum; i++){
            CreateItem(Grass,CreateRandomPosition(),Quaternion.identity);
        }
    }

    private Vector3 CreateRandomPosition(){
        while(true){
            //Range API is [min,max]
            int x = UnityEngine.Random.Range(MapLengthNeg,MapLengthPos);
            int y = UnityEngine.Random.Range(MapWidthNeg,MapWidthPos);
            if(ExitItem[x - MapLengthNeg,y - MapWidthNeg] == false){
                ExitItem[x - MapLengthNeg,y - MapWidthNeg] = true;
                return new Vector3(x,y,0);
            }
        }

    }
    private void CreatePlayer(Vector3 position){
        GameObject BornPlayer = Instantiate(Born,position,Quaternion.identity);
        BornPlayer.GetComponent<Born>().isCreatePlayer = true;
        ExitItem[(int)position.x - MapLengthNeg,(int)position.y - MapWidthNeg] = true;
    }
    private void CreateEnemyIn(Vector3 position){
        GameObject BornEnemy = Instantiate(Born,position,Quaternion.identity);
        BornEnemy.GetComponent<Born>().isCreatePlayer = false;
        BornEnemy.transform.SetParent(this.transform);
    }
    private void CreateEnemy(){
        int x = UnityEngine.Random.Range(1,3);
        switch(x){
            case 1:
                CreateEnemyIn(new Vector3(MapLengthNeg,MapWidthPos,0));
                break;
            case 2:
                CreateEnemyIn(new Vector3(0,MapWidthPos,0));
                break;
            case 3:
                CreateEnemyIn(new Vector3(MapLengthPos,MapWidthPos,0));
                break;
        }
    }
}
