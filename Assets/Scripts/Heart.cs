using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    private SpriteRenderer sr; 
    public GameObject explosionPrefab;
    public Sprite BrokenSprite;
    public AudioClip dieAudio;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    public void Die()
    {
        sr.sprite = BrokenSprite;
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        PlayerManager.Instance.isDead = true;
        PlayerManager.Instance.lifeValue = -1;
        AudioSource.PlayClipAtPoint(dieAudio, transform.position);
    }
}
