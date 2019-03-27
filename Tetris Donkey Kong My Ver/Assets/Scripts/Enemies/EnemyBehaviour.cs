using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

    // constants for direction, for using in Move functions
    private const int RIGHT = 1;
    private const int DOWN = 2;
    private const int LEFT = 3;
    private const int UP = 4;

    // this enemy's movement direction
    private int direction;
    
    // distance the enemy moves in a frame
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        // begin moving right
        gameObject.name = "Enemy";
        if (CanMoveRight())
        {
            direction = RIGHT;
        } else
        {
            direction = LEFT;
        }
        speed = 0.06f;
    }

    // Update is called once per frame
    void Update()
    {

        // collision physics
        if (direction == RIGHT)
        {
            if (CanMoveRight())
            {
                // move
                GridService.GetInstance().MoveSingle(gameObject, transform.position.x + speed, transform.position.y);
            } else
            {
                // move up one block and swap directions
                GridService.GetInstance().MoveSingle(gameObject, transform.position.x, transform.position.y + 1);
                direction = LEFT;
            }
        } else if (direction == LEFT)
        {
            if (CanMoveLeft())
            {
                // move
                GridService.GetInstance().MoveSingle(gameObject, transform.position.x - speed, transform.position.y);
            }
            else
            {
                // move up one block and swap directions
                GridService.GetInstance().MoveSingle(gameObject, transform.position.x, transform.position.y + 1);
                direction = RIGHT;
            }
        }

        // destroy this enemy if it is out of bounds
        // TODO
        // this is where the enemy should "impact" the player and reduce points
        if (gameObject.transform.position.y > GridService.HEIGHT)
        {
            GridService.GetInstance().DestroyInGrid(gameObject);
        }
    }

    // returns if the space to the right of the block is unblocked and within bounds
    bool CanMoveRight()
    {
        if (!GridService.GetInstance().InBounds((int)gameObject.transform.position.x + 1, (int)gameObject.transform.position.y)) return false;

        Transform next = GridService.GetInstance().Get((int)gameObject.transform.position.x + 1, (int)gameObject.transform.position.y);
        if (next != null && next.name == "StoppedCell") return false;

        return true;
    }

    // returns if the space to the left of the block is unblocked and within bounds
    bool CanMoveLeft()
    {
        if (!GridService.GetInstance().InBounds((int)(gameObject.transform.position.x - 1), (int)gameObject.transform.position.y)) return false;

        Transform next = GridService.GetInstance().Get((int)(gameObject.transform.position.x - speed), (int)gameObject.transform.position.y);
        if (next != null && next.name == "StoppedCell") return false;

        return true;
    }
}
