using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public AudioClip DieAudio;
    public float moveSpeed = 3;
    //public Sprite[] tankSprite;// up right down left
    public GameObject explosionPrefab;
    public GameObject ShieldPrefab;
    public bool isDefended = true;
    private float defendTimeVal = 3;
    public GameObject bulletPrefab;
    private float bulletOffset = 0.5f;
    private float attackTimeVal;
    const float ATTACKTIMEVAL = 0.4f;
    //private SpriteRenderer sr;
    // Start is called before the first frame update
    private int v_last;
    private int h_last;
    private void CheckDefended(){
        if(defendTimeVal >= 0){
            defendTimeVal -= Time.deltaTime;
            if(defendTimeVal <= 0){
                isDefended = false;
                ShieldPrefab.SetActive(false);
            }
        }
    }
    public void Die(){
        if(isDefended){
            return;
        }
        AudioSource.PlayClipAtPoint(DieAudio,transform.position);
        Destroy(gameObject);
        Instantiate(explosionPrefab,transform.position,transform.rotation);
        PlayerManager1.Instance.isDead = true;
    }
    /**
    * v and h can't be both nonzero
    */
    private void Move(int v,int h){
        if(v != 0 && h != 0){
            throw new System.Exception("v and h can't be both nonzero");
        }
        transform.Translate(Vector3.right * h * moveSpeed * Time.deltaTime,Space.World);
        transform.Translate(Vector3.up * v * moveSpeed * Time.deltaTime,Space.World);
        if (h > 0){
            //sr.sprite = tankSprite[1];
            transform.eulerAngles = new Vector3(0,0,-90);
        }
        else if (h < 0){
            //sr.sprite = tankSprite[3];
            transform.eulerAngles = new Vector3(0,0,90);
        }
        else if (v > 0){
            //sr.sprite = tankSprite[0];
            transform.eulerAngles = new Vector3(0,0,0);
        }
        else if (v < 0){
            //sr.sprite = tankSprite[2];
            transform.eulerAngles = new Vector3(0,0,180);
        }
    }
    
    private void Move(){
        // Move tank according to input
        int horizontal = (int)Input.GetAxisRaw("Horizontal");
        int vertical = (int)Input.GetAxisRaw("Vertical");

        if(horizontal != 0 && vertical != 0){
            if(horizontal != h_last && vertical == v_last){
                Move(0,horizontal);
                //h_last = horizontal;
            }
            else if(horizontal == h_last && vertical != v_last){
                Move(vertical,0);
                //v_last = vertical;
            }
        }
        else{
            Move(vertical,horizontal);
            v_last = vertical;
            h_last = horizontal;
        }
    }
    private void Attack(){
        if(attackTimeVal <= 0){
            if(Input.GetKey(KeyCode.Space)){
                GameObject bullet = Instantiate(bulletPrefab,transform.position + transform.up * bulletOffset,transform.rotation);
                bullet.GetComponent<bullet>().SetStatus(true,this.gameObject);
                attackTimeVal = ATTACKTIMEVAL;
            }
        }
        else{
            attackTimeVal -= Time.deltaTime;
        }
    }
    void Start()
    {
        //sr = GetComponent<SpriteRenderer>();
        v_last = 0;
        h_last = 0;
        ShieldPrefab.SetActive(true);
        attackTimeVal = ATTACKTIMEVAL;
    }

    // Update is called once per frame
    void Update()
    {
        CheckDefended();
        Attack();
    }
    private void FixedUpdate(){
        Move();
    }
}
