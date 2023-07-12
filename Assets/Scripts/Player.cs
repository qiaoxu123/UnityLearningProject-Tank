using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 属性值
    public int moveSpeed = 3;
    private Vector3 bullectEulerAngles;
    private float timeVal;
    private float defendTimeVal = 3;        // 出生 3s 内无敌状态
    private bool isDefended = true;

    // 引用
    private SpriteRenderer sr;
    public Sprite[] tankSprite;
    public GameObject bullectPrefab;
    public GameObject explosionPrefab;
    public GameObject defendEffectPrefab;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // 开启无敌特效
        defendEffectPrefab.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        // 是否处于无敌状态
        if (isDefended) {
            if (defendTimeVal > 0) {
                defendTimeVal -= Time.deltaTime;
            } else {
                isDefended = false;
                defendEffectPrefab.SetActive(false); // 关闭无敌特效
            }
        }

        // 攻击 CD 
        if (timeVal >= 0.4f) Attack();
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bullectPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bullectEulerAngles)); 
            timeVal = 0;
        }
    }

    private void Move()
    {
        // 保证不会抖动
        float h = Input.GetAxisRaw("Horizontal");
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

        float v = Input.GetAxisRaw("Vertical");
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
        // 处于无敌状态时免疫死亡
        if (isDefended) return;

        // 产生爆炸特效
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        // Die
        Destroy(gameObject);
    }
}
