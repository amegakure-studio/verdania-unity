using System;
using System.Collections.Generic;
using Amegakure.Verdania.GridSystem;
using Dojo;
using UnityEngine;

[Serializable]
public struct ItemGoBinding
{
    public ItemType itemType;
    public GameObject itemGo; 
}

public class Character : MonoBehaviour
{
    [Header("Dojo")]
    private string dojoId;

    [Header("Movement variables")]
    private const float speed = 3f;
    private List<TileRenderer> pathVectorList;
    private int currentPathIndex;
    private TileRenderer currentTile;

    [Header("Climb variables")]
    [SerializeField] GameObject stepRayUpper;
    [SerializeField] GameObject stepRayLower;
    [SerializeField] float stepHeight = 0.3f;
    [SerializeField] float stepSmooth = 2f;

    [Header("BodyParts")]
    [SerializeField] Transform head;

    [Header("Rig")]
    [SerializeField] GameObject headRig;
    [SerializeField] GameObject spineRig;

    [Header("Inventory GO")]
    [SerializeField] ItemGoBinding[] itemsGo;

    private PlayerFinder playerFinder;
    private WorldManager worldManager;

    public List<TileRenderer> PathVectorList
    {
        get => pathVectorList;
        set
        {
            pathVectorList = value;

            if (pathVectorList != null && pathVectorList.Count > 1)
            {
                pathVectorList.RemoveAt(0);
            }
        }
    }
    public int CurrentPathIndex { get => currentPathIndex; set => currentPathIndex = value; }
    public TileRenderer CurrentTile { get => currentTile; set => currentTile = value; }
    public Transform Head { get => head; set => head = value; }
    public GameObject HeadRig { get => headRig; set => headRig = value; }
    public GameObject SpineRig { get => spineRig; set => spineRig = value; }
    public string DojoId { get => dojoId; set => dojoId = value; }
    public ItemGoBinding[] ItemsGo { get => itemsGo; set => itemsGo = value; }

    private void Awake()
    {
        //playerFinder = UnityUtils.FindOrCreateComponent<PlayerFinder>();
        //worldManager = GameObject.FindObjectOfType<WorldManager>();
    }

    public void HandleMovement()
    {
        if (PathVectorList != null)
        {
            TileRenderer lastTile = PathVectorList[CurrentPathIndex];
            Vector3 targetPosition = lastTile.gameObject.transform.position;
            targetPosition.y = transform.position.y;

            if (Vector3.Distance(transform.position, targetPosition) > 0.3f)
            {
                MoveAndRotate(targetPosition);
                this.currentTile = lastTile;
                EventManager.Instance.Publish(GameEvent.CHARACTER_MOVE_START, new() { { "Character", gameObject } });
            }
            else
            {
                CurrentPathIndex++;
                if (CurrentPathIndex >= PathVectorList.Count)
                {
                    StopMoving();
                    EventManager.Instance.Publish(GameEvent.CHARACTER_MOVE_END, new() { { "Character", gameObject } });
                }
            }
        }
    }

    private void MoveAndRotate(Vector3 destination)
    {
        Vector3 moveDir = (destination - transform.position).normalized;

        float distanceBefore = Vector3.Distance(transform.position, destination);
        transform.position = transform.position + moveDir * speed * Time.deltaTime;

        Vector3 vectorRotation = transform.position.DirectionTo(destination).Flat();

        if (vectorRotation != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(vectorRotation, Vector3.up);

        Climb();
    }

    private void Climb()
    {
        RaycastHit hitLower;
        if (Physics.Raycast(stepRayLower.transform.position, transform.TransformDirection(Vector3.forward), out hitLower, 0.1f))
        {
            RaycastHit hitUpper;
            if (!Physics.Raycast(stepRayUpper.transform.position, transform.TransformDirection(Vector3.forward), out hitUpper, 0.2f))
            {
                transform.position -= new Vector3(0f, -stepSmooth * Time.deltaTime, 0f);
            }
        }

        RaycastHit hitLower45;
        if (Physics.Raycast(stepRayLower.transform.position, transform.TransformDirection(1.5f, 0, 1), out hitLower45, 0.1f))
        {

            RaycastHit hitUpper45;
            if (!Physics.Raycast(stepRayUpper.transform.position, transform.TransformDirection(1.5f, 0, 1), out hitUpper45, 0.2f))
            {
                transform.position -= new Vector3(0f, -stepSmooth * Time.deltaTime, 0f);
            }
        }

        RaycastHit hitLowerMinus45;
        if (Physics.Raycast(stepRayLower.transform.position, transform.TransformDirection(-1.5f, 0, 1), out hitLowerMinus45, 0.1f))
        {

            RaycastHit hitUpperMinus45;
            if (!Physics.Raycast(stepRayUpper.transform.position, transform.TransformDirection(-1.5f, 0, 1), out hitUpperMinus45, 0.2f))
            {
                transform.position -= new Vector3(0f, -stepSmooth * Time.deltaTime, 0f);
            }
        }
    }

    private void StopMoving()
    {
        TileRenderer lastTile = PathVectorList[PathVectorList.Count - 1];
        this.currentTile = lastTile;
        PathVectorList = null;
    }
}
