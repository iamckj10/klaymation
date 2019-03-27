using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public GameObject[] listOfBlocks;
    public GameObject player;
    public GameObject enemy;
    private static int index;

    // counts ticks between enemy spawns
    private int ticker = 0;
    // number of ticks before an enemy spawn
    // a lower spawn interval means more spawning
    private int enemySpawnInterval = 125;

    // Start is called before the first frame update
    void Start()
    {
        index = UnityEngine.Random.Range(0, listOfBlocks.Length);
        Instantiate(player).name = "Player";
        Instantiate(listOfBlocks[index], new Vector3(player.transform.position.x, 13), new Quaternion()).name = "CurBlock";
    }

    // Update is called once per frame
    void Update()
    {
        // create an enemy if the user has pressed space
        if (Input.GetKeyDown(KeyCode.Space)) {
            CreateBlock();
        }

        // increment the ticker and create an enemy if the interval has passed
        ticker++;
        if (ticker % enemySpawnInterval == 0)
        {
            SpawnEnemy();
        }
    }

    public void SpawnEnemy()
    {
        int x = GridService.WIDTH - 1;
        if (UnityEngine.Random.Range(0, 2) == 0) x = 0;

        // find lowest possible block to spawn on
        int lowestY = -1;
        for (int y = GridService.HEIGHT - 1; y >= 0; y--) {
            if (GridService.GetInstance().Get(x, y) == null)
            {
                lowestY = y;
            } else
            {
                Transform next = GridService.GetInstance().Get(x, y);
                if (next != null && next.gameObject.name == "StoppedCell")
                {
                    break;
                }
            }
        }
        if (lowestY >= 0)
        {
            Instantiate(enemy, new Vector3(x, lowestY), Quaternion.identity);
        }
    }

    public void CreateBlock()
    {
        index = UnityEngine.Random.Range(0, listOfBlocks.Length);
        Instantiate(listOfBlocks[index], new Vector3(player.transform.position.x, 13), Quaternion.identity);
    }
}
