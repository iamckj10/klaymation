using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBehavior : MonoBehaviour
{
    private bool hasBeenDropped;
    private bool hasLanded;
    private GameObject player;
    private float speed;
    

    // Start is called before the first frame update
    void Start()
    {
        gameObject.name = "CurBlock";
        hasLanded = false;
        hasBeenDropped = false;
        player = GameObject.Find("Player");
        speed = 0.3f;
    }

    // Update is called once per frame
    void Update()
    {
        // if the block has landed, no updates are needed
        if (hasLanded) return;

        // drop the block if the timer has run out or the input key is pressed
        if (Timer.time <= 0 || Input.GetKeyDown(KeyCode.Space))
        {
            print("falling...");
            hasBeenDropped = true;
            gameObject.name = "FallingBlock";
        }

        Move();

        if (!hasBeenDropped) return;

        Collision();
    }

    //EFFECTS: if the block is being held, keep it with the player
    // if the block is falling, move it downward at speed
    private void Move()
    {
        if (!hasBeenDropped)
        {
            gameObject.transform.position = new Vector3(player.transform.position.x, 12);
        }
        else
        {
            GridService.GetInstance().MoveBlock(gameObject, transform.position.x, transform.position.y - speed);
        }
    }

    //EFFECTS: If the block reaches or passes floorPosition, stop the block at this position.
    // if the block collides with an enemy, destroy the enemy and increment the score
    private void Collision()
    {
        KillEnemies();

        if (WillLand())
        {
            Land();
        }
    }



    private void Land()
    {
        foreach (Transform cell in gameObject.transform)
        {
            cell.gameObject.name = "StoppedCell";
        }
        gameObject.name = "StoppedBlock";
        
        GridService.GetInstance().MoveBlock(gameObject, (int)gameObject.transform.position.x, (int)gameObject.transform.position.y);
        hasLanded = true;

        GridService.GetInstance().PrintGrid();
    }

    //EFFECTS: returns true if the block will be forced to stop
    // either by hitting the floor or colliding with another block
    private bool WillLand() {
        // return true if the block will land on the floor
        if (gameObject.transform.position.y <= 0)
        {
            Debug.Log("landing on the floor.");
            return true;
        }

        // return false if one of the cells is directly above a stopped block
        foreach (Transform cell in gameObject.transform) {
            Transform next = GridService.GetInstance().Get((int)cell.transform.position.x, (int)cell.transform.position.y - 1);
            //Debug.Log((int)cell.transform.position.x + "," + (int)cell.transform.position.y);
            if (next != null && next.gameObject.name == "StoppedCell")
            {
                Debug.Log("landing on another block.");
                return true;
            }
        }
        return false;
    }

    //EFFECTS: destroys enemies if they collide with falling blocks

    private void KillEnemies()
    {
        foreach (Transform cell in gameObject.transform)
        {
            Transform below = GridService.GetInstance().Get((int)cell.position.x, (int)(cell.position.y - 1 + speed));
            if (below != null && below.gameObject.name == "Enemy")
            {
                Debug.Log("kill!");
                GridService.GetInstance().DestroyInGrid(below.gameObject);
            }
        }
    }

    //EFFECTS: returns true if all cells are within the GridService boundaries
    public bool AreCellsInBound() {
        bool areCellsInBound = true;
        foreach (Transform cell in gameObject.transform) {
            if (!GridService.GetInstance().InBounds((int) cell.position.x, (int) cell.position.y)) {  
                areCellsInBound = false;
            }
        }
        return areCellsInBound;
    }
}
