using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    float speed = 5f;
    float radius;
    Vector2 direction;
    private Vector2 startPos;
    
    GameManager gameManager;
    GameObject gameManagerObject;

    // Start is called before the first frame update
    void Start()
    {
        speed = 5f;

        float randomX = Random.Range(0, 2f) * 2 - 1; // get -1 or 1
        direction = new Vector2(randomX, 0).normalized;
        radius = transform.localScale.x / 2; // half width`
        startPos = transform.position;
        gameManagerObject = GameObject.FindWithTag("GameController");

        if (gameManagerObject != null) {
            gameManager = gameManagerObject.GetComponent<GameManager>();
        } else {
            Debug.Log("GameManager object not found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        // bounce off top and bottom
        if (transform.position.y < GameManager.bottomLeft.y + radius && direction.y < 0) {
            direction.y = -direction.y; // invert the directon of the ball when it hits boundary
        }

        if (transform.position.y > GameManager.topRight.y - radius && direction.y > 0) {
            direction.y = -direction.y; // invert the directon of the ball when it hits boundary
        }


        if (transform.position.x < GameManager.bottomLeft.x + radius && direction.x < 0) {
            Time.timeScale = 0;
            Debug.Log("Right player scores!");
            gameManager.PlayerScored(true);
        }

        if (transform.position.x > GameManager.topRight.x - radius && direction.x > 0) {
            Time.timeScale = 0;
            transform.position = startPos;
            Debug.Log("Left player scores!");
            gameManager.PlayerScored(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Paddle") {
            // collided with paddle
            bool isRight = other.GetComponent<Paddle>().isRight;

            // flip direction when ball hits paddle
            if (isRight == true && direction.x > 0) {
                if (speed < 15f) {
                    speed += 1f;
                }
                direction.x = -direction.x;
                direction.y += Random.Range(-0.5f, 0.5f);
            } else if (isRight == false && direction.x < 0) {
                if (speed < 15f) {
                    speed += 1f;
                }
                direction.x = -direction.x;
                direction.y += Random.Range(-0.5f, 0.5f);
            }
        }
    }

    public void ResetBall() {
        transform.position = Vector2.zero;
        speed = 5f;
        float randomX = Random.Range(0, 2f) * 2 - 1; // get -1 or 1
        direction = new Vector2(randomX, 0).normalized;
    }
}
