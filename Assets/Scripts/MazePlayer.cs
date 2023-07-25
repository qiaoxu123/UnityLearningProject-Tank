using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazePlayer : MonoBehaviour
{
    // 属性值
    public int moveSpeed = 3;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    // 适用于处理物理相关的逻辑，保证物理引擎在每个固定的时间步长内对物体的运动、碰撞和力的作用进行处理
    // 只有刚体存在时才会调用该方法，可以保证物理模拟的稳定性和准确性，从而减少碰撞抖动等问题
    // 注意修改 Time.deltaTime 为 Time.fixedDeltaTime
    private void FixedUpdate()
    {
  
    }

    // private void Move(float h, float v)
    // {
    //     // 保证不会抖动
    //     transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime, Space.World);

    //     Vector3 bullectEulerAngles;

    //     if (h < 0) {
    //         bullectEulerAngles = new Vector3(0,0,90);
    //         gameObject.transform.rotation = Quaternion.Euler(bullectEulerAngles);
    //     }
        
    //     else if (h > 0) {
    //         bullectEulerAngles = new Vector3(0,0,-90);
    //         gameObject.transform.rotation = Quaternion.Euler(bullectEulerAngles);
    //     }

    //     if (h != 0) return;

    //     transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime, Space.World);

    //     if (v < 0) {
    //         bullectEulerAngles = new Vector3(0,0,-180);
    //         gameObject.transform.rotation = Quaternion.Euler(bullectEulerAngles);
    //     }
    //     else if (v > 0) {
    //         bullectEulerAngles = new Vector3(0,0,0);
    //         gameObject.transform.rotation = Quaternion.Euler(bullectEulerAngles);
    //     }
    // }

    public System.Collections.IEnumerator MoveToTarget(Vector3 targetPosition)
    {
        while (transform.position != targetPosition)
        {
            // 计算当前位置指向目标位置的方向向量
            Vector3 direction = (targetPosition - transform.position).normalized;

            // 计算每一帧移动的距离
            float distanceThisFrame = moveSpeed * Time.deltaTime;

            // 根据距离和方向向量更新车辆的位置
            transform.position += direction * distanceThisFrame;

            yield return null; // 等待下一帧
        }
    }
}
