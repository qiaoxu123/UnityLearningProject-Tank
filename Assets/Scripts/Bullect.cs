using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullect : MonoBehaviour
{
    public float moveSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.up * moveSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        switch (collision.tag)
        {
            case "Tank":
                collision.SendMessage("Die");
                break;
            case "Heart":
                break;
            case "Enemy":
                break;
            case "Wall":
                break;
            case "Barrier":
                break;
            default:
                break;
        }
    }
}
