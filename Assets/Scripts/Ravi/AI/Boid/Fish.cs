using UnityEngine;

public class Fish
{
    public GameObject myObject;
    public Material myMat;
    public Vector3 position;
    public Vector3 velocity;
    public int quantity { get; set; }
    public float speed { get; set; }
    public float separationForce { get; set; }
    public float maxNeighbourDistance { get; set; }
    public float noise { get; set; }
    public float flock { get; set; }

    private float minY = 0;
    private float maxY = 0;

    private Vector3 currentVelocity = Vector3.zero;

    private FishManager manager;
    private LayerMask LM;

    public Fish(FishManager _manager, LayerMask mask, float height)
    {
        manager = _manager;
        LM = mask;
        minY = manager.transform.position.y - height;
        maxY = manager.transform.position.y + height;
    }

    public void Update()
    {
        velocity += Force();
        velocity = velocity.normalized;
        currentVelocity = Vector3.MoveTowards(currentVelocity, velocity, (separationForce * Time.deltaTime));
        // clamp fish height
        if ((position.y > maxY && currentVelocity.y > 0) || (position.y < minY && currentVelocity.y < 0))
        {
            currentVelocity = new Vector3(currentVelocity.x, -currentVelocity.y / separationForce, currentVelocity.z);
        }
        position += currentVelocity * Time.deltaTime * speed;
        myObject.transform.position = position;
        myObject.transform.LookAt(position + currentVelocity);
    }

    // calculate fish direction and force
    Vector3 Force()
    {
        Vector3 fishDirection = manager.averagePosition;
        fishDirection -= position;
        fishDirection = fishDirection / flock;

        Vector3 fishVelocity = manager.totalVelocity - velocity;
        fishVelocity = fishVelocity / (quantity - 1);
        fishVelocity = (fishVelocity - velocity) / noise;

        Vector3 fishForce = Vector3.zero;
        Collider[] neighbours = Physics.OverlapSphere(position, maxNeighbourDistance, LM);
        foreach (Collider neighbour in neighbours)
        {
            try
            {
                if (neighbour.CompareTag("fish")) // can be from different school!
                {
                    fishForce -= (manager.fishInstances[int.Parse(neighbour.name)].position - position);
                }
                else
                {
                    fishForce -= neighbour.transform.position - position;
                }
            }
            catch { }
        }

        return fishDirection + fishVelocity + fishForce;
    }
}