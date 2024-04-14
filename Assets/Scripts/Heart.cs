using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public AudioClip HeartDieAudio;
    private SpriteRenderer sr;
    public Sprite brokenSprite;
    public GameObject explosionPrefab;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Die(){ 
        AudioSource.PlayClipAtPoint(HeartDieAudio,transform.position);       
        sr.sprite = brokenSprite;
        Instantiate(explosionPrefab,transform.position,transform.rotation);
        PlayerManager1.Instance.playerLife = 1;
        PlayerManager1.Instance.isDead = false;
        PlayerManager1.Instance.Text_PlayerLife.text = PlayerManager1.Instance.playerLife.ToString();
    }
}
