using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public GameObject wallPrefab;
    public GameObject airWallPrefab;
    public GameObject endPointPrefab;
    public GameObject playerPrefab;
    public int gameMapHeight;
    public int gameMapWidth;

    // 局部变量
    private GameObject[,] gameMap;
    private int offsetX;
    private int offsetY;

    // 是在对象启用之前进行初始化。它常用于设置初始状态、获取引用和执行其他一次性的初始化操作
    // 与 Start 函数不同的是，Awake 函数在对象实例化后立即调用，而 Start 函数则在所有对象的 Awake 函数都被调用后才开始执行
    private void Awake() {
        InitMap();
    }

    // 实例化物体 
    private void InitMap() {
        // 初始化地图
        gameMap = new GameObject[gameMapWidth + 1, gameMapHeight + 1];  // 默认初始化值均为 0
        offsetX = gameMapWidth / 2 + 1;     // 27 / 2 = 13
        offsetY = gameMapHeight / 2 + 1;    // 17 / 2 = 8

        // 实例化外围,限制地图大小
        for (float i = -13.5f; i <= 13.5f; ++ i) {
            CreateItem(wallPrefab, new Vector3(i, -8.5f, 0), Quaternion.identity);
            CreateItem(wallPrefab, new Vector3(i, 8.5f, 0), Quaternion.identity);
        }
        for (float i = -8.5f; i <= 8.5f; ++ i) {
            CreateItem(wallPrefab, new Vector3(-13.5f, i, 0), Quaternion.identity);
            CreateItem(wallPrefab, new Vector3(13.5f, i, 0), Quaternion.identity);
        }

        // 初始化玩家
        CreateItem(playerPrefab, CreateRandomPosition(), Quaternion.identity);
        // 初始化终点
        CreateItem(endPointPrefab, CreateRandomPosition(), Quaternion.identity);

        //实例化地图
        for (int i = 0; i < 100; ++ i) {
            CreateItem(wallPrefab, CreateRandomPosition(), Quaternion.identity);
        }
    }

    private void CreateItem(GameObject createGameObject, Vector3 createPos, Quaternion createRotation) {
        if (HasThePosition(createPos)) return;

        int adjustedX = Mathf.FloorToInt(createPos.x) + offsetX;
        int adjustedY = Mathf.FloorToInt(createPos.y) + offsetY;

        gameMap[adjustedX, adjustedY] = Instantiate(createGameObject, createPos, createRotation);
        gameMap[adjustedX, adjustedY].transform.SetParent(gameObject.transform);
    }

    // 产生随机位置的方法
    private Vector3 CreateRandomPosition() {
        // 限定地图边界，不让其在这里生成物体
        while (true) {
            float posX = Random.Range(-13, 13) + 0.5f;   // [-12.5, 12.5]
            float posY = Random.Range(-8, 8) + 0.5f;   // [-7.5, 7.5]
            Vector3 createPos = new Vector3(posX, posY, 0);
            if (!HasThePosition(createPos)) {
                return createPos;
            }
        }
    }

    // 判定该位置是否已占用，如未占用则返回 Null
    private bool HasThePosition(Vector3 createPos) {
        int adjustedX = Mathf.FloorToInt(createPos.x) + offsetX;
        int adjustedY = Mathf.FloorToInt(createPos.y) + offsetY;

        return gameMap[adjustedX, adjustedY] == null ? false : true;
    }
}
