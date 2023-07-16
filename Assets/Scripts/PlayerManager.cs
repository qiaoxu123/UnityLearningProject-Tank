using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    // 属性值
    public int lifeValue = 3;
    public int playerScore = 0;
    public bool isDead;
    public bool isDefect;

    // 引用
    public GameObject born;
    public Text playerScoreText;
    public Text playerLifeValueText;
    public GameObject isDefeatUI;

    // 单例
    private static PlayerManager instance;

    public static PlayerManager Instance {
        get { return instance; }
        set { instance = value; }
    }

    private void Awake() {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        isDefeatUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) Recover();
        playerScoreText.text = playerScore.ToString();
        playerLifeValueText.text = lifeValue.ToString();
    }

    private void Recover() {
        if (lifeValue < 0) {
            // 游戏失败，返回主界面
            isDefeatUI.SetActive(true);
            return;
        } else {
            lifeValue --;
            GameObject go = Instantiate(born, new Vector3(-2, -6, 0), Quaternion.identity);
            go.GetComponent<Born>().createPlayer = true;
            isDead = false;
        }
    }
}
