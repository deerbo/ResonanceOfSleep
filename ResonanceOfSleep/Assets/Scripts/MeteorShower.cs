using UnityEngine;

public class MeteorShower : MonoBehaviour
{
    static private int numberOfRocks = 50;
    float[] timeOffsets = new float[numberOfRocks];

    public GameObject rock_prefab;

    private GameObject[] rocks;

    float[] lerpTimes = new float[numberOfRocks];
    Vector3[] startPositions = new Vector3[numberOfRocks];
    Vector3[] endPositions = new Vector3[numberOfRocks];

    float time = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rocks = new GameObject[numberOfRocks];

        // for each meteor, spawn it in off the screen in a circle
        for (int i = 0; i < numberOfRocks; i++)
        {
            timeOffsets[i] = Random.Range(0f, Mathf.PI * 2f);

            float r = Random.Range(10, numberOfRocks);
            float angle = i * Mathf.PI * 2 / numberOfRocks;

            // initate start and end position for lerp
            Vector3 pos = new Vector3(Mathf.Cos(angle) * r, 20, Mathf.Sin(angle) * r);
            startPositions[i] = pos;
            Vector3 endPos = new Vector3(pos.x, pos.y - 30f, pos.z);
            endPositions[i] = endPos;

            GameObject b = Instantiate(rock_prefab, pos, Quaternion.identity);
            rocks[i] = b;
        }
    }

    // Update is called once per frame
    void Update()
    {   
        time += Time.deltaTime;

        // for each rock, make it fall to -10
        for (int i = 0; i < numberOfRocks; i++)
        {
            float t = Mathf.Sin(time + timeOffsets[i]) * 0.5f + 0.5f;
            rocks[i].transform.position = Vector3.Lerp(startPositions[i], endPositions[i], t);

            //rocks[i].transform.position = Vector3.Lerp(startPositions[i], endPositions[i], lerpTimes[i]);

            // once it's been too long teleport back to start pos
            Debug.Log(lerpTimes[i]);
            if (lerpTimes[i] >= 0.98f)
            {
                time = 0;
                rocks[i].transform.position = new Vector3(startPositions[i].x, startPositions[i].y, startPositions[i].z);
            }
        }
    }
}
