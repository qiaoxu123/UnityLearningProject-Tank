using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // 属性值
    public int moveSpeed = 3;
    private Vector3 bullectEulerAngles;
    private int v, h;

    // 引用
    private SpriteRenderer sr;
    public Sprite[] tankSprite;
    public GameObject bullectPrefab;
    public GameObject explosionPrefab;

    // 计时器
    private float timeVal = 0;
    private float timeValChangeDirection = 4;  // 一出生就有一个移动的效果，不用等待计时

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // 攻击的时间间隔
        if (timeVal >=  3) Attack();
        else timeVal += Time.deltaTime;
    }

    // 适用于处理物理相关的逻辑，保证物理引擎在每个固定的时间步长内对物体的运动、碰撞和力的作用进行处理
    // 只有刚体存在时才会调用该方法，可以保证物理模拟的稳定性和准确性，从而减少碰撞抖动等问题
    // 注意修改 Time.deltaTime 为 Time.fixedDeltaTime
    private void FixedUpdate()
    {
        Move();
    }

    private void Attack()
    {
        Instantiate(bullectPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bullectEulerAngles)); 
        timeVal = 0;
    }

    private void Move()
    {
        if (timeValChangeDirection >= 4) {
            int num = Random.Range(0, 8);

            if (num > 5) {
                v = -1; h = 0;
            } else if (num == 0) {
                v = 1; h = 0;
            } else if (num > 0 && num <= 2) {
                h = -1; v = 0;
            } else {
                h = 1; v = 0;
            }

            timeValChangeDirection = 0;
        } else {
            timeValChangeDirection += Time.fixedDeltaTime;
        }

        transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime, Space.World);

        if (h < 0) {
            sr.sprite = tankSprite[3];
            bullectEulerAngles = new Vector3(0,0,90);
        }
        
        else if (h > 0) {
            sr.sprite = tankSprite[1];
            bullectEulerAngles = new Vector3(0,0,-90);
        }

        if (h != 0) return;

        transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime, Space.World);

        if (v < 0) {
            sr.sprite = tankSprite[2];
            bullectEulerAngles = new Vector3(0,0,-180);
        }
        else if (v > 0) {
            sr.sprite = tankSprite[0];
            bullectEulerAngles = new Vector3(0,0,0);
        }
    }

    private void Die()
    {
        // 产生爆炸特效
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        // Die
        Destroy(gameObject);
    }
}
