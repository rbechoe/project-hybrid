using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishManager : MonoBehaviour
{
    private bool isDone;

    //---------------used for maths-----------------------------
    [HideInInspector]
    public List<Fish> fishInstances = new List<Fish>();
    //[HideInInspector]
    public Vector3 averagePosition;
    //[HideInInspector]
    public Vector3 totalPosition;
    //[HideInInspector]
    public Vector3 totalVelocity;

    //---------------things to set------------------------------
    [Header("Assign before running")]
    [SerializeField]
    private GameObject fishPrefab;
    [SerializeField]
    private GameObject targetObject;
    [SerializeField]
    private GameObject centerMass;
    [SerializeField]
    private LayerMask layerMask;

    //---------------settings for the game designer-------------
    [Header("Settings")]
    [Tooltip("Set amount of fishes")]
    [Range(1, 10000)]
    public int fishQuantity;
    [Tooltip("Update fish seperation")]
    [Range(1, 10)]
    public float maxNeighbourDistance = 1f;
    [Tooltip("Update fish speed")]
    [Range(1, 50)]
    public float fishSpeed = 5f;
    [Tooltip("Update fish flocking behaviour")]
    [Range(1, 500)]
    public float fishFlocking = 100f;
    [Tooltip("Update fish uniqueness")]
    [Range(1, 500)]
    public float fishNoise = 5f;
    [Tooltip("Update how aggressively fishs avoid each other")]
    [Range(1, 100)]
    public float fishSeparationForce = 5f;

    //---------------extra settings for the game designer------
    [Header("Extra settings")]
    [Tooltip("How fast do we move to the target?")]
    [Range(1, 100)]
    public float movementSpeed = 5; // TODO move cart to follow path

    void Start()
    {
        Vector3 posTotal = Vector3.zero;
        GameObject parentObj = new GameObject();
        parentObj.name = "Collection " + gameObject.name;
        averagePosition = targetObject.transform.position;

        for (int i = 0; i < fishQuantity; i++)
        {
            Vector3 startPos = new Vector3(
                targetObject.transform.position.x + (Random.Range(1, 500) / 100f),
                targetObject.transform.position.y + (Random.Range(1, 500) / 100f),
                targetObject.transform.position.z + (Random.Range(1, 500) / 100f)
            );
            GameObject fishObj = Instantiate(fishPrefab, startPos, Quaternion.identity);
            fishObj.name = "" + i;
            fishObj.tag = "fish";
            fishObj.transform.parent = parentObj.transform;

            fishInstances.Add(new Fish(this, layerMask));
            fishInstances[i].myObject = fishObj;
            fishInstances[i].myMat = fishObj.GetComponent<Renderer>().material;
            fishInstances[i].myMat.EnableKeyword("_EMISSION");
            fishInstances[i].position = startPos;
            fishInstances[i].velocity = Vector3.zero;
            fishInstances[i].quantity = fishQuantity;
            UpdatefishSettings(fishInstances[i]);

            posTotal += startPos;
        }
        isDone = true;
    }

    void Update()
    {
        if (isDone) StartCoroutine(Updatefishes());

        averagePosition += (targetObject.transform.position - averagePosition).normalized * Time.deltaTime * movementSpeed;

        centerMass.transform.position = averagePosition;
    }

    IEnumerator Updatefishes()
    {
        isDone = false;
        totalPosition = Vector3.zero;
        totalVelocity = Vector3.zero;

        // get total values
        for (int i = 0; i < fishQuantity; i++)
        {
            totalPosition += fishInstances[i].position;
            totalVelocity += fishInstances[i].velocity;
            UpdatefishSettings(fishInstances[i]);
        }

        // update individual fishs
        for (int i = 0; i < fishQuantity; i++)
        {
            fishInstances[i].Update();
        }

        isDone = true;
        yield return new WaitForEndOfFrame();
    }

    void UpdatefishSettings(Fish _fish)
    {
        _fish.speed = fishSpeed;
        _fish.maxNeighbourDistance = maxNeighbourDistance;
        _fish.noise = fishNoise;
        _fish.flock = fishFlocking;
        _fish.separationForce = fishSeparationForce;
    }
}