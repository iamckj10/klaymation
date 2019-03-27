using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    GameObject currentBlock;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = new Vector3(GridService.WIDTH / 2, GridService.HEIGHT + 2);
        currentBlock = GameObject.Find("CurBlock");
    }

    // Update is called once per frame
    void Update()
    {
        currentBlock = GameObject.Find("CurBlock");
        InputHandler();
    }

    private void InputHandler()
    { 
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            gameObject.transform.position += new Vector3(1, 0, 0);
            if (!IsBlockInGridRight()) {
                gameObject.transform.position += new Vector3(-1, 0, 0);
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            gameObject.transform.position += new Vector3(-1, 0, 0);
            if (!IsBlockInGridLeft())
            {
                gameObject.transform.position += new Vector3(1, 0, 0);
            }
        }
       
    }

    private bool IsBlockInGridLeft() {
        bool isInGrid = true;
        foreach (Transform cell in currentBlock.transform) {
            if (cell.transform.position.x - 1 < 0) {
                isInGrid = false;
            }
        }
        return isInGrid;
    }

    private bool IsBlockInGridRight()
    {
        bool isInGrid = true;
        foreach (Transform cell in currentBlock.transform)
        {
            if (cell.transform.position.x + 1 >= GridService.WIDTH)
            {
                isInGrid = false;
            }
        }
        return isInGrid;
    }
}
