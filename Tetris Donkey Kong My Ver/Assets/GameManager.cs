using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] listOfBlock;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(player);
        Instantiate(listOfBlock[0], new Vector3(player.transform.position.x, 13), new Quaternion());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
