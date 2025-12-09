using UnityEngine;

public class Fish : MonoBehaviour
{
    private int numberOfBoids = 30;
    private GameObject[] boids;
    BodyProperty[] bp;

    struct BodyProperty
    {
        public Vector3 velocity;
        public Vector3 position;
    }
    public float neighborRadius = 6f; 
    public float separationRadius = 2f;

    public float cohesionWeight = 0.8f;
    public float separationWeight = 2.0f;
    public float alignmentWeight = 1.0f;

    public float maxSpeed = 5f;
    public float steeringLimit = 3f;

    public float bounds = 25f;
    public GameObject boid_prefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bp = new BodyProperty[numberOfBoids];
        boids = new GameObject[numberOfBoids];

        for (int i = 0; i < numberOfBoids; i++)
        {
            Vector3 pos = Random.insideUnitSphere * 10f;
            GameObject b = Instantiate(boid_prefab, pos, Quaternion.identity);

            boids[i] = b;
            bp[i].position = pos;
            bp[i].velocity = Random.insideUnitSphere * 0.5f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;

        for (int i = 0; i < numberOfBoids; i++)
        {
            Vector3 v1 = RuleOne(i);   // cohesion
            Vector3 v2 = RuleTwo(i);   // separation
            Vector3 v3 = RuleThree(i); // alignment

            Vector3 velocityChange = v1 + v2 + v3;

            bp[i].velocity += velocityChange;
            bound(i);
            LimitVelocity(i);

            // update position
            bp[i].position += bp[i].velocity * dt;
            boids[i].transform.position = bp[i].position;

            // rotate to face velocity
            if (bp[i].velocity != Vector3.zero)
                boids[i].transform.rotation = Quaternion.LookRotation(bp[i].velocity);
        }
    }

    private Vector3 RuleOne(int j)
    {
        // calculate center of mass of all the boids
        Vector3 center = new Vector3(0, 0, 0);

        for (int i = 0; i < numberOfBoids; i++)
        {
            if (i != j)
            {
                center = center + bp[i].position;
            }
        }

        center = center / (numberOfBoids - 1);

        // move 1% of the way to the center of mass of all the boids
        return (center - bp[j].position) / 50;
    }

    // if the boids get too close to each other, redirect them away
    private Vector3 RuleTwo(int j)
    {
        Vector3 c = new Vector3(0, 0, 0);

        for (int i = 0; i < numberOfBoids; i++)
        {
            if (i != j)
            {
                if (Vector3.Distance(bp[i].position, bp[j].position) < 1f)
                {
                    c = c + (bp[j].position - bp[i].position);
                }
            }
        }

        return c;
    }

    private Vector3 RuleThree(int j)
    {
        Vector3 newVc = new Vector3(0, 0, 0);

        for (int i = 0; i < numberOfBoids; i++)
        {
            if (i != j)
            {
                newVc = newVc + bp[i].velocity;
            }
        }

        newVc = newVc / (numberOfBoids - 1);

        return (newVc - bp[j].velocity) / 8f; // add about an eigth to current velocity
    }

    private void bound(int i)
    {
        float boundLimit = 20f;
        Vector3 pos = bp[i].position;

        float distance = pos.magnitude; // full 3D distance

        if (distance > boundLimit)
        {
            Vector3 correction = -pos.normalized * 0.5f;
            bp[i].velocity += correction;
        }
    }
    private void LimitVelocity(int i)
    {
        float limit = 3f;
        Vector3 v = bp[i].velocity;

        // If the magnitude is greater than the limit, clamp it
        if (v.magnitude > limit)
        {
            bp[i].velocity = v.normalized * limit;
        }
    }
}
