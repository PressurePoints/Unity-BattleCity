using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public AudioClip fireAudio;
    private GameObject parent;
    private bool isPlayerBullet;
    public void SetStatus(bool isPlayerBullet,GameObject parent){
        this.isPlayerBullet = isPlayerBullet;
        this.parent = parent;
    }
    public float moveSpeed = 10;
    // Start is called before the first frame update
    void Start()
    {
        if(isPlayerBullet){AudioSource.PlayClipAtPoint(fireAudio,transform.position);}
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime,Space.Self);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.tag){
            case "Tank":
                if(!isPlayerBullet){
                    collision.GetComponent<Player>().Die();
                }
                if(this.parent != collision.gameObject){
                    Destroy(gameObject);
                }
                break;
            case "Heart":
                collision.GetComponent<Heart>().Die();
                Destroy(gameObject);
                break;
            case "Enemy":
                if(isPlayerBullet){
                    collision.GetComponent<Enemy>().Die();
                }
                if(this.parent != collision.gameObject){
                    Destroy(gameObject);
                }
                break;
            case "Wall":
                Destroy(collision.gameObject);
                Destroy(gameObject);
                break;
            case "Barrier":
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }
}
