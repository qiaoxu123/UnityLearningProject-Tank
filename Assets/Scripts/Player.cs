using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 属性值
    public int moveSpeed = 3;
    private Vector3 bullectEulerAngles;

    // 引用
    private SpriteRenderer sr;
    public Sprite[] tankSprite;
    public GameObject bullectPrefab;

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
        Attack();
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
        }
    }

    private void Move()
    {
        // 保证不会抖动
        float h = Input.GetAxisRaw("Horizontal");
        transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime, Space.World);

        if (h < 0) sr.sprite = tankSprite[3];
        else if (h > 0) sr.sprite = tankSprite[1];

        if (h != 0) return;

        float v = Input.GetAxisRaw("Vertical");
        transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime, Space.World);

        if (v < 0) sr.sprite = tankSprite[2];
        else if (v > 0) sr.sprite = tankSprite[0];
    }
}