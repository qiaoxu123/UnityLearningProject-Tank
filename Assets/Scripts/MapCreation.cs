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
        gameMap = new GameObject[gameMapWidth, gameMapHeight];  // 默认初始化值均为 0
        offsetX = gameMapWidth / 2;     // 11 [-11, 10]
        offsetY = gameMapHeight / 2;    // 6 [-6, 5]

        // 实例化老家
        CreateItem(itemTypes[0], new Vector3(0, -5, 0), Quaternion.identity);
        CreateItem(itemTypes[2], new Vector3(-1, -5, 0), Quaternion.identity);
        CreateItem(itemTypes[2], new Vector3(1, -5, 0), Quaternion.identity);
        for (float i = -1; i < 2; ++ i) { 
            CreateItem(itemTypes[2], new Vector3(i, -4, 0), Quaternion.identity);
        }

        // 实例化外围,限制地图大小
        for (float i = -11; i < 11; ++ i) {
            CreateItem(itemTypes[6], new Vector3(i, -6, 0), Quaternion.identity);
            CreateItem(itemTypes[6], new Vector3(i, 5, 0), Quaternion.identity);
        }
        for (float i = -6; i < 6; ++ i) {
            CreateItem(itemTypes[6], new Vector3(-11, i, 0), Quaternion.identity);
            CreateItem(itemTypes[6], new Vector3(10, i, 0), Quaternion.identity);
        }

        // 产生敌人
        GameObject enemy0 = Instantiate(itemTypes[1], new Vector3(-10, 4, 0), Quaternion.identity);
        GameObject enemy1 = Instantiate(itemTypes[1], new Vector3(0, 4, 0), Quaternion.identity);
        GameObject enemy2 = Instantiate(itemTypes[1], new Vector3(9, 4, 0), Quaternion.identity);

        // 初始化玩家
        gameMap[-2 + offsetX, -5 + offsetY] = Instantiate(itemTypes[1], new Vector3(-2, -5, 0), Quaternion.identity);
        gameMap[-2 + offsetX, -5 + offsetY].GetComponent<Born>().createPlayer = true;

        //实例化地图
        for (int i = 0; i < 60; ++ i) {
            CreateItem(itemTypes[2], CreateRandomPosition(), Quaternion.identity);
        }
        for (int i = 0; i < 20; ++ i) {   
            CreateItem(itemTypes[3], CreateRandomPosition(), Quaternion.identity);
            CreateItem(itemTypes[4], CreateRandomPosition(), Quaternion.identity);
            CreateItem(itemTypes[5], CreateRandomPosition(), Quaternion.identity);
        }

        InvokeRepeating("CreateEnemy", 4, 4);
    }

    private void CreateItem(GameObject createGameObject, Vector3 createPos, Quaternion createRotation) {
        if (HasThePosition(createPos)) return;

        int adjustedX = (int)createPos.x + offsetX;
        int adjustedY = (int)createPos.y + offsetY;

        gameMap[adjustedX, adjustedY] = Instantiate(createGameObject, createPos, createRotation);
        gameMap[adjustedX, adjustedY].transform.SetParent(gameObject.transform);
    }

    // 产生随机位置的方法
    private Vector3 CreateRandomPosition() {
        // 限定地图边界，不让其在这里生成物体
        // 限制不在 enemyBorn 地方生成物体
        Vector3 enemyPos0 = new Vector3(-10, 4, 0);
        Vector3 enemyPos1 = new Vector3(0, 4, 0);
        Vector3 enemyPos2 = new Vector3(9, 4, 0);
        while (true) {
            Vector3 createPosition = new Vector3(Random.Range(-10, 10), Random.Range(-5, 5), 0); 
            if (!HasThePosition(createPosition) && 
                createPosition != enemyPos0 && 
                createPosition != enemyPos1 && 
                createPosition != enemyPos2) {
                return createPosition;
            }
        }
    }

    // 判定该位置是否已占用，如未占用则返回 Null
    private bool HasThePosition(Vector3 createPos) {
        int adjustedX = (int)createPos.x + offsetX;
        int adjustedY = (int)createPos.y + offsetY;
        Debug.Log("adjustedX is" + adjustedX);
        Debug.Log("adjustedY is" + adjustedY);
        Debug.Log("length" + gameMap.GetLength(0));
        Debug.Log("length" + gameMap.GetLength(1));

        return gameMap[adjustedX, adjustedY] == null ? false : true;
    }

    // 产生敌人的方法
    private void CreateEnemy() {
        int num = Random.Range(0, 3);
        Vector3 EnemyPos = new Vector3();
        switch (num)
        {
            case 0: 
                EnemyPos = new Vector3(-10, 4, 0);
                break;
            case 1: 
                EnemyPos = new Vector3(0, 4, 0);
                break;
            case 2: 
                EnemyPos = new Vector3(9, 4, 0);
                break;
        }
        GameObject enemyBorn = Instantiate(itemTypes[1], EnemyPos, Quaternion.identity);
        enemyBorn.transform.SetParent(gameObject.transform);
    }
}
