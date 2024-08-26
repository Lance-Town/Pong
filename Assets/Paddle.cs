using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    protected float moveSpeed = 10;
    private Vector2 directionLeft;
    private Vector2 directionRight;
    
    [SerializeField]
    float speed;

    float height;

    string input;
    public bool isRight;

    // Start is called before the first frame update
    void Start()
    {
        height = transform.localScale.y;
        speed = 10;
    }

    public void init(bool isRightPaddle) {
        isRight = isRightPaddle;
        Vector2 pos = Vector2.zero;
        if (isRightPaddle) {
            // place on right of screen
            pos = new Vector2(GameManager.topRight.x, 0);
            pos -= (transform.localScale.x * Vector2.right);
            pos.x -= 0.19f;

            input = "PaddleRight";
        } else {
            // place on left of screen
            pos = new Vector2(GameManager.bottomLeft.x, 0);
            pos += (transform.localScale.x * Vector2.right);
            pos.x += 0.19f;

            input = "PaddleLeft";
        }

        transform.position = pos;
        transform.name = input;
    }

    // Update is called once per frame
    private void Update() {
        // GetAxis is a number between -1 to 1 (-1 for down, 1 for up)
        float move = Input.GetAxis(input) * Time.deltaTime * speed;

        // restrict paddle movement so it doesn't go offscreen
        if (transform.position.y < GameManager.bottomLeft.y + height / 2 && move < 0) {
            move = 0;
        } else if (transform.position.y > GameManager.topRight.y - height / 2 && move > 0) {
            move = 0;
        }

        transform.Translate(move * Vector2.up);
    }

    public void ResetPaddle() {
        Vector2 pos = Vector2.zero;
        bool isRight = (input == "PaddleRight" ? true : false);
        if (isRight) {
            pos = new Vector2(GameManager.topRight.x, 0);
            pos -= transform.localScale.x * Vector2.right;
            pos.x -= 0.19f;
        } else {
            pos = new Vector2(GameManager.bottomLeft.x, 0);
            pos += transform.localScale.x * Vector2.right;
            pos.x += 0.19f;
        }
        transform.position = pos;
    }
}
