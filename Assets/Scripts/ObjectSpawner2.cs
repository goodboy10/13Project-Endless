using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner2 : MonoBehaviour
{
    [SerializeField] List<GameObject> spawnPoints;
    [SerializeField] [Range(0,100)] float coinChance = 0;

    public float spawnTime = 1;
    public float obstacleSpeed = 15f;
    public GameObject cloneParent;
    public GameObject coin;
    public GameObject obstacle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    float timer = 0;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > spawnTime)
        {
            timer = 0;

            List<int> spawnList = generateSpawnList();
            // Make sure there is an object which lets you pass (make sure it not impossible)
            while (!(spawnList.Contains(0) || spawnList.Contains(2)))
            {
                spawnList = generateSpawnList();
            }

            for (int i = 0; i < spawnList.Count; i++)
            {
                if (spawnList[i] == 1)
                {
                    GameObject obj = Instantiate(obstacle, spawnPoints[i].transform.position, transform.rotation, cloneParent.transform); 
                    obj.GetComponent<ObstacleMovement>().speed = obstacleSpeed;
                    Debug.Log("Spawn Obstacle");
                } else if (spawnList[i] == 2)
                {
                    GameObject obj = Instantiate(coin, spawnPoints[i].transform.position, transform.rotation, cloneParent.transform); 
                    obj.GetComponent<ObstacleMovement>().speed = obstacleSpeed;
                    Debug.Log("Spawn coin");
                }
            }
        }
    }

    // 0 is empty
    // 1 is obstacle
    // 2 is coin
    // 3 is health (to add)

    List<int> generateSpawnList()
    {
        List<int> spawns = new List<int>();
        foreach (GameObject point in spawnPoints)
        {
            int random = Random.Range(0,2);
            spawns.Add(random);
        }

        //chance for an empty to turn into a coin
        for (int i = 0; i < spawns.Count; i++)
        {
            if (spawns[i] == 0 && Random.Range(0,101) < coinChance)
            {
                spawns[i] = 2;
            }
        }
        return spawns;
    }
}