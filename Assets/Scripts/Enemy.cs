using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public AudioClip EnemyDieAudio;
    public float moveSpeed = 3;
    
    public GameObject explosionPrefab;
    public GameObject bulletPrefab;
    private float bulletOffset = 0.5f;
    private float attackTimeVal = ATTCKTIMEVAL;
    private float directionTimeVal = DIRECTIONTIMEVAL;
    private string movedirection = "up";
    const float ATTCKTIMEVAL = 1;
    const float DIRECTIONTIMEVAL = 2;
    private Transform player;
    private string noDirection = "";

    public void Die(){
        AudioSource.PlayClipAtPoint(EnemyDieAudio,transform.position);
        Destroy(gameObject);
        Instantiate(explosionPrefab,transform.position,transform.rotation);
        PlayerManager1.Instance.playerScore++;
        PlayerManager1.Instance.Text_PlayerScore.text = PlayerManager1.Instance.playerScore.ToString();
    }
    private void Move(string direction){
        switch(direction){
            case "up":
                transform.Translate(Vector3.up * moveSpeed * Time.deltaTime,Space.World);
                transform.eulerAngles = new Vector3(0,0,0);
                break;
            case "down":
                transform.Translate(Vector3.down * moveSpeed * Time.deltaTime,Space.World);
                transform.eulerAngles = new Vector3(0,0,180);
                break;
            case "left":
                transform.Translate(Vector3.left * moveSpeed * Time.deltaTime,Space.World);
                transform.eulerAngles = new Vector3(0,0,90);
                break;
            case "right":
                transform.Translate(Vector3.right * moveSpeed * Time.deltaTime,Space.World);
                transform.eulerAngles = new Vector3(0,0,-90);
                break;
            default:
                break;
        }
    }
    private void Attack(){
        GameObject bullet = Instantiate(bulletPrefab,transform.position + transform.up * bulletOffset,transform.rotation);
        bullet.GetComponent<bullet>().SetStatus(false,this.gameObject);
    }
    // private void OnCollisionEnter2D(Collision2D collision){
    //     if(collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("River") || collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Barrier")){
    //         directionTimeVal = 0;
    //         noDirection = movedirection;
    //     }
    // }
    private void DumyAI(){
        string[] directions = {"up","down","left","right"};
        
        directionTimeVal -= Time.deltaTime;
        if(directionTimeVal <= 0){
            while(true){
                int num = UnityEngine.Random.Range(1,4);
                if(directions[num] != noDirection){
                    movedirection = directions[num];
                    break;
                }
            }
            directionTimeVal = DIRECTIONTIMEVAL;
        }
        
        Move(movedirection);

        attackTimeVal -= Time.deltaTime;
        if(attackTimeVal <= 0){
            Attack();
            attackTimeVal = ATTCKTIMEVAL;
        }
        
    }
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Tank").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //attack();
    }
    private void FixedUpdate(){
        //Move();
        DumyAI();
    }
}
