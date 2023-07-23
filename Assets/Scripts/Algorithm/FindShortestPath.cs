using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Node
{
    public int x;
    public int y;
    public Node parent;

    public Node(int x, int y, Node parent)
    {
        this.x = x;
        this.y = y;
        this.parent = parent;
    }
}

public class FindShortestPath : MonoBehaviour
{
    // 引用
    public GameObject mazeMapPrefab; // 存储了所有的障碍物位置以及终点位置
    public GameObject playerPrefab;
    public GameObject heartPrefab;
    public GameObject pathPrefab;

    public int mazeMapHeight;
    public int mazeMapWidth;

    // 局部变量
    private GameObject[,] basicMap;
    private int offsetX;
    private int offsetY;

    void Awake() {
        basicMap = new GameObject[mazeMapWidth + 1, mazeMapHeight + 1];
        InitBasicMap();
    }


    // Start is called before the first frame update
    void Start()
    {
        int startPosX = Mathf.FloorToInt(playerPrefab.transform.position.x) + offsetX;
        int startPosY = Mathf.FloorToInt(playerPrefab.transform.position.y) + offsetY;

        int targetPosX = Mathf.FloorToInt(heartPrefab.transform.position.x) + offsetX;
        int targetPosY = Mathf.FloorToInt(heartPrefab.transform.position.y) + offsetY;

        Node startNode = new Node(startPosX, startPosY, null);
        Node targetNode = new Node(targetPosX, targetPosY, null);

        List<Node> shortestPath = BroadFirstSearchPath(startNode, targetNode);

        if (shortestPath.Count > 0) {
            Debug.Log("Shortest Path:");
            foreach (var node in shortestPath) {
                // Debug.Log("(" + node.x + ", " + node.y + ")");

                if (node.x == startPosX && node.y == startPosY) continue;
                if (node.x == targetPosX && node.y == targetPosY) continue;

                float posX = node.x - offsetX + 0.5f;
                float posY = node.y - offsetY + 0.5f;

                GameObject pathNode = Instantiate(pathPrefab, new Vector3(posX, posY, 0), Quaternion.identity);
                pathNode.transform.SetParent(gameObject.transform);
            }
        } else {
            Debug.Log("No path found!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 初始化地图，将已有地图中保存的墙体对象都更新到新的 basicMap 中
    private void InitBasicMap() {
        offsetX = mazeMapWidth / 2 + 1;     // 27 / 2 = 13
        offsetY = mazeMapHeight / 2 + 1;    // 17 / 2 = 8

        if (mazeMapPrefab != null)
        {
            // 获取该Prefab下的所有子Prefab
            Transform[] childTransforms = mazeMapPrefab.GetComponentsInChildren<Transform>(true);

            // 输出所有子Prefab的名称
            foreach (Transform childTransform in childTransforms) {
                GameObject childGameObject = childTransform.gameObject;

                if (childGameObject == playerPrefab || childGameObject == heartPrefab) 
                    continue;

                float posX = childGameObject.transform.position.x;
                float posY = childGameObject.transform.position.y;
                
                int adjustedX = Mathf.FloorToInt(posX) + offsetX;
                int adjustedY = Mathf.FloorToInt(posY) + offsetY;

                basicMap[adjustedX, adjustedY] = childGameObject;
            }
        }
        else
        {
            Debug.LogWarning("Main Prefab is not assigned!");
        }
    }

    private List<Node> BroadFirstSearchPath(Node startNode, Node targetNode) {
        Queue<Node> queue = new Queue<Node>();
        bool[,] visited = new bool[mazeMapWidth, mazeMapHeight];

        queue.Enqueue(startNode);
        visited[startNode.x, startNode.y] = true;

        List<int[]> shortestPath = new List<int[]>();

        while (queue.Count > 0) {
            Node currentNode = queue.Dequeue();

            // 如果找到目标节点，则回溯路径并返回
            if (currentNode.x == targetNode.x && currentNode.y == targetNode.y) {
                return BacktrackPath(currentNode);
            }

            foreach (var neighbor in GetNeighbors(currentNode)) {
                if (!visited[neighbor.x, neighbor.y]) {
                    queue.Enqueue(neighbor);
                    visited[neighbor.x, neighbor.y] = true;
                }
            }
        }

        return new List<Node>();
    }

    private List<Node> BacktrackPath(Node targetNode) {
        List<Node> path = new List<Node>();
        Node currentNode = targetNode;

        while (currentNode != null) {
            path.Insert(0, currentNode);
            currentNode = currentNode.parent;
        }

        return path;
    }

    private List<Node> GetNeighbors(Node node) {
        List<Node> neighbors = new List<Node>();

        int[] dx = { 0, 1, 0, -1};
        int[] dy = { -1,0, 1, 0 };

        for (int i = 0; i < 4; ++i) {
            int newX = node.x + dx[i];
            int newY = node.y + dy[i];

            if (newX >= 0 && newX < mazeMapWidth && newY >= 0 && newY < mazeMapHeight &&
                basicMap[newX, newY] == null) {
                    neighbors.Add(new Node(newX, newY, node));
                }
        }

        return neighbors;
    }
}