using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreation : MonoBehaviour
{
    // 用来装饰初始化地图所需物体的数组
    // 0.Heart 1.Born 2.Wall 3.Barrier 4.Grass 5.Water 6.AirWall
    public GameObject[] item;

    // 已有物体的位置
    private List<Vector3> itemPositionList = new List<Vector3>();

    // 实例化物体 
    private void Awake() {
        // 实例化老家
        CreateItem(item[0], new Vector3(0, -4.5f, 0), Quaternion.identity);
        CreateItem(item[2], new Vector3(-1, -4.5f, 0), Quaternion.identity);
        CreateItem(item[2], new Vector3(1, -4.5f, 0), Quaternion.identity);
        for (float i = -1; i < 2; ++ i) { 
            CreateItem(item[2], new Vector3(i, -3.5f, 0), Quaternion.identity);
        }

        // 实例化外围 AirWall
        for (float i = -8.5f; i < 9.5f; ++ i) {
            CreateItem(item[6], new Vector3(i, -5.5f, 0), Quaternion.identity);
            CreateItem(item[6], new Vector3(i, 5.5f, 0), Quaternion.identity);
        }
        for (float i = -5.5f; i < 6.5f; ++ i) {
            CreateItem(item[6], new Vector3(-8.5f, i, 0), Quaternion.identity);
            CreateItem(item[6], new Vector3(8.5f, i, 0), Quaternion.identity);
        }
    }

    private void CreateItem(GameObject createGameObject, Vector3 createPosition, Quaternion createRotation) {
        if (HasThePosition(createPosition)) return;
        GameObject itemGo = Instantiate(createGameObject, createPosition, createRotation);
        itemGo.transform.SetParent(gameObject.transform);
        itemPositionList.Add(createPosition);
    }

    // 产生随机位置的方法
    private Vector3 CreateRandomPosition() {
        // 限定地图边界，不让其在这里生成物体。当前地图大小为 x(-7.5, 7.5), y(-4.5, 4.5)
        while (true) {
            Vector3 createPosition = new Vector3(Random.Range(-7.5f, 8.5f), Random.Range(-4.5f, 5.5f), 0); 
            if (!HasThePosition(createPosition)) {
                return createPosition;
            }
        }
    }

    // 判定该位置是否已占用,循环位置列表
    private bool HasThePosition(Vector3 createPos) {
        for (int i = 0; i < itemPositionList.Count; i++) {
            if (Mathf.Approximately(createPos.x, itemPositionList[i].x) && 
                Mathf.Approximately(createPos.y, itemPositionList[i].y) && 
                Mathf.Approximately(createPos.z, itemPositionList[i].z)) {
                return true;
            }
        }
        return false;
    }
}
