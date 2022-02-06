using UnityEngine;

public class Fish : MonoBehaviour, IDamagable
{
    public GameObject myObject;
    public Material myMat;
    public Vector3 position;
    public Vector3 velocity;
    public AudioClip hit1, hit2;

    public int quantity { get; set; }
    public float speed { get; set; }
    public float separationForce { get; set; }
    public float maxNeighbourDistance { get; set; }
    public float noise { get; set; }
    public float flock { get; set; }

    private float minY = 0;
    private float maxY = 0;

    private bool enableCap;

    private Vector3 currentVelocity = Vector3.zero;
    private FishManager fishManager;
    private LayerMask LM;
    private AudioSystem audioSystem;

    public void Update()
    {
        velocity += Force();
        velocity = velocity.normalized;

        // clamp fish height
        if ((position.y > maxY || position.y < minY) && enableCap)
        {
            velocity = new Vector3(velocity.x, -(velocity.y / separationForce), velocity.z);
        }

        currentVelocity = Vector3.MoveTowards(currentVelocity, velocity, (separationForce * Time.deltaTime));
        position += currentVelocity * Time.deltaTime * speed;

        myObject.transform.position = position;
        myObject.transform.LookAt(position + currentVelocity);
    }

    public void FishSetup(FishManager manager, LayerMask mask, float height, bool enableCap)
    {
        this.fishManager = manager;
        this.enableCap = enableCap;
        LM = mask;
        minY = manager.transform.position.y - height;
        maxY = manager.transform.position.y + height;
        audioSystem = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioSystem>();
    }

    public void TakeDamage(int damage)
    {
        fishManager.RemoveFish(this);
        if (Random.Range(0, 100) > 50)
            audioSystem.ShootSFX(hit1, transform.position);
        else
            audioSystem.ShootSFX(hit2, transform.position);
    }

    // calculate fish direction and force
    private Vector3 Force()
    {
        Vector3 fishDirection = fishManager.averagePosition;
        fishDirection -= position;
        fishDirection = fishDirection / flock;

        Vector3 fishVelocity = fishManager.totalVelocity - velocity;
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
                    fishForce -= (fishManager.fishInstances[int.Parse(neighbour.name)].position - position);
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
