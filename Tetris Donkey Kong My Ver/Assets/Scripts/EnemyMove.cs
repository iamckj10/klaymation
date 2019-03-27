using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private bool left; //does the enemy move left?
    private bool right; //does the enemy move right?
    private bool canMove; //is the enemy allowed to move right now?
    private double timer; //timer for when the enemy can move. equivalent to the fall speed
    static int MOVE_RIGHT = 1;
    static int MOVE_LEFT = 0;

    // Start is called before the first frame update
    void Start()
    { //initialize enemy
        gameObject.transform.position = new Vector3(4, 5);
        determineDirection();
        canMove = false;
        timer = 1.0;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer == 0.0)
        {
            if (left == true && gameObject.transform.position.x == 1)
            { //if its at the right edge, move up and change directions
                left = false;
                right = true;
                gameObject.transform.Translate(0, 1, 0);
            }

            else if (right == true && gameObject.transform.position.x == 9)
            { //if its at the left edge, move up and change directions
                left = true;
                right = false;
                gameObject.transform.Translate(0, 1, 0);
            }

            else if (left == true && gameObject.transform.position.x != 1)
            { //move left
                gameObject.transform.Translate(-1, 0, 0);
            }

            else if (right == true && gameObject.transform.position.x != 9)
            { //move right
                gameObject.transform.Translate(1, 0, 0);
            }

            timer = 1.0;
        }

        else {
            timer -= 0.0625;
        }

    }

    void determineDirection()
    {
        if(gameObject.transform.position.y % 2 == MOVE_RIGHT)
        {
            right = true;
            left = false;
            return;
        }
        else if (gameObject.transform.position.y % 2 == MOVE_LEFT)
        {
            left = true;
            right = false;
            return;
        }
    }
}
