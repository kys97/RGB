using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoSpawn : MonoBehaviour
{
    public float SpawnTime;
    public float SpawnDistance;
    public GameObject MeteoPrefab;

    private float time;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;    
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if(time > SpawnTime)
        {
            float X = Random.Range(SpawnDistance * -1, SpawnDistance);
            Instantiate(MeteoPrefab, new Vector3(X + transform.position.x, transform.position.y, 0), Quaternion.identity);
            time = 0.0f;
        }
    }
}
