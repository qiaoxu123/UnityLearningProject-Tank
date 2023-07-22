using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreation : MonoBehaviour
{
    // 初始化地图所需物体的模板
    // 0.Heart 1.Born 2.Wall 3.Barrier 4.Grass 5.Water 6.AirBarrier
    public GameObject[] itemTypes;
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
            CreateItem(itemTypes[6], new Vector3(i, -8.5f, 0), Quaternion.identity);
            CreateItem(itemTypes[6], new Vector3(i, 8.5f, 0), Quaternion.identity);
        }
        for (float i = -8.5f; i <= 8.5f; ++ i) {
            CreateItem(itemTypes[6], new Vector3(-13.5f, i, 0), Quaternion.identity);
            CreateItem(itemTypes[6], new Vector3(13.5f, i, 0), Quaternion.identity);
        }

        // 实例化老家
        CreateItem(itemTypes[0], new Vector3(0.5f, -7.5f, 0), Quaternion.identity);
        CreateItem(itemTypes[2], new Vector3(-0.5f, -7.5f, 0), Quaternion.identity);
        CreateItem(itemTypes[2], new Vector3(1.5f, -7.5f, 0), Quaternion.identity);
        for (float i = -0.5f; i < 2.5f; ++ i) { 
            CreateItem(itemTypes[2], new Vector3(i, -6.5f, 0), Quaternion.identity);
        }

        // 产生敌人
        CreateItem(itemTypes[1], new Vector3(-12.5f, 7.5f, 0), Quaternion.identity);
        CreateItem(itemTypes[1], new Vector3(0.5f, 7.5f, 0), Quaternion.identity);
        CreateItem(itemTypes[1], new Vector3(12.5f, 7.5f, 0), Quaternion.identity);

        // 初始化玩家
        int playerPosX = Mathf.FloorToInt(-1.5f) + offsetX;
        int playerPosY = Mathf.FloorToInt(-7.5f) + offsetY;
        gameMap[playerPosX, playerPosY] = Instantiate(itemTypes[1], new Vector3(-1.5f, -7.5f, 0), Quaternion.identity);
        gameMap[playerPosX, playerPosY].GetComponent<Born>().createPlayer = true;

        //实例化地图
        for (int i = 0; i < 80; ++ i) {
            CreateItem(itemTypes[2], CreateRandomPosition(), Quaternion.identity);
        }
        for (int i = 0; i < 30; ++ i) {   
            CreateItem(itemTypes[3], CreateRandomPosition(), Quaternion.identity);
            CreateItem(itemTypes[4], CreateRandomPosition(), Quaternion.identity);
            CreateItem(itemTypes[5], CreateRandomPosition(), Quaternion.identity);
        }

        InvokeRepeating("CreateEnemy", 4, 4);
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

    // 产生敌人的方法
    private void CreateEnemy() {
        int num = Random.Range(0, 3);
        Vector3 EnemyPos = new Vector3();
        switch (num)
        {
            case 0: 
                EnemyPos = new Vector3(-12.5f, 6.5f, 0);
                break;
            case 1: 
                EnemyPos = new Vector3(0, 6.5f, 0);
                break;
            case 2: 
                EnemyPos = new Vector3(11.5f, 6.5f, 0);
                break;
        }
        GameObject enemyBorn = Instantiate(itemTypes[1], EnemyPos, Quaternion.identity);
        enemyBorn.transform.SetParent(gameObject.transform);
    }
}
