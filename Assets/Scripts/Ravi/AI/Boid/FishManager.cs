using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

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
    private GameObject[] fishPrefabs;
    [SerializeField]
    private GameObject centerMass;
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private int chosenFish;

    //---------------settings for the game designer-------------
    [Header("Settings")]
    [Tooltip("Set amount of fishes")]
    [Range(1, 10000)]
    public int fishQuantity;
    [Tooltip("Update fish seperation distance")]
    [Range(1, 20)]
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

    public float maxHeightRange = 20f;
    public float swimTimeStart = 5f;
    public float pauseTimeStart = 5f;
    public float cartSpeed = 2;

    public bool enableCap;

    private float swimTime = 5f;
    private float pauseTime = 5f;
    private GameObject targetObject;

    //---------------extra settings for the game designer------
    [Header("Extra settings")]
    [Tooltip("How fast do we move to the target?")]
    [Range(1, 100)]
    public float movementSpeed = 5;

    private CinemachineDollyCart CDC;

    private void Start()
    {
        Vector3 posTotal = Vector3.zero;
        GameObject parentObj = new GameObject();

        targetObject = gameObject;
        CDC = gameObject.GetComponent<CinemachineDollyCart>();
        parentObj.name = "Collection " + gameObject.name;
        averagePosition = targetObject.transform.position;
        swimTime = swimTimeStart;
        pauseTime = pauseTimeStart;

        for (int i = 0; i < fishQuantity; i++)
        {
            Vector3 startPos = new Vector3(
                targetObject.transform.position.x + (Random.Range(1, 1000) / 100f),
                targetObject.transform.position.y + (Random.Range(1, 1000) / 100f),
                targetObject.transform.position.z + (Random.Range(1, 1000) / 100f)
            );

            GameObject fishObj = Instantiate(fishPrefabs[chosenFish], startPos, Quaternion.identity);
            fishObj.name = "" + i;
            fishObj.tag = "fish";
            fishObj.transform.parent = parentObj.transform;

            Fish newFish = fishObj.GetComponent<Fish>();
            newFish.FishSetup(this, layerMask, maxHeightRange, enableCap);
            fishInstances.Add(newFish);
            fishInstances[i].myObject = fishObj;
            fishInstances[i].position = startPos;
            fishInstances[i].velocity = Vector3.zero;
            fishInstances[i].quantity = fishQuantity;
            UpdatefishSettings(fishInstances[i]);

            posTotal += startPos;
        }

        isDone = true;
    }

    private void Update()
    {
        if (isDone) StartCoroutine(UpdateFishes());

        Vector3 normalizedPos = (targetObject.transform.position - averagePosition).normalized;
        averagePosition += normalizedPos * Time.deltaTime * movementSpeed;
        centerMass.transform.position = averagePosition;

        if (swimTime > 0)
        {
            swimTime -= Time.deltaTime;
            CDC.m_Speed = cartSpeed;
        }

        if (swimTime <= 0)
        {
            pauseTime -= Time.deltaTime;
            CDC.m_Speed = 0;

            if (pauseTime <= 0)
            {
                pauseTime = pauseTimeStart;
                swimTime = swimTimeStart;
            }
        }
    }

    private IEnumerator UpdateFishes()
    {
        isDone = false;
        totalPosition = Vector3.zero;
        totalVelocity = Vector3.zero;

        // get total values
        for (int i = 0; i < fishInstances.Count; i++)
        {
            totalPosition += fishInstances[i].position;
            totalVelocity += fishInstances[i].velocity;
            UpdatefishSettings(fishInstances[i]);
        }

        // update individual fishs
        for (int i = 0; i < fishInstances.Count; i++)
        {
            fishInstances[i].Update();
        }

        isDone = true;
        yield return new WaitForEndOfFrame();
    }

    private void UpdatefishSettings(Fish fish)
    {
        fish.speed = fishSpeed;
        fish.maxNeighbourDistance = maxNeighbourDistance;
        fish.noise = fishNoise;
        fish.flock = fishFlocking;
        fish.separationForce = fishSeparationForce;
    }

    public void RemoveFish(Fish fish)
    {
        EventSystem<int>.InvokeEvent(EventType.SCORE_UP, 10);
        fishInstances.Remove(fish);
        Destroy(fish.myObject);
    }
}
