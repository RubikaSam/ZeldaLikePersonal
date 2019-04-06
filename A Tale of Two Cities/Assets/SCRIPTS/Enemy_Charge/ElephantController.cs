using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.Serialization;
using Sirenix.OdinInspector;

public class ElephantController : SerializedMonoBehaviour
{
    private bool isDetectingPlayer = true;
    private bool isDetectingWalls = false;
    private bool isAnimatorFacingDirection = false;
    
    private bool hasSpottedPlayer = false;
    private bool isCharging = false;

    Vector3 gridStartPosition;
    Vector3 gridArrivalPosition;
    Vector3 normalizedDirection;

    private Animator anim;
    private Transform target;

    List<Vector3> CardinalDirections = new List<Vector3>();

    void Awake()
    {
        target = GameObject.FindWithTag("Player").transform;
        anim = GetComponent<Animator>();

        Vector3 NorthRay = new Vector3(0, 50);
        Vector3 SouthRay = new Vector3(0, -50);
        Vector3 EastRay = new Vector3(50, 0);
        Vector3 WestRay = new Vector3(-50, 0);

        CardinalDirections.AddRange(new Vector3[] { NorthRay, SouthRay, EastRay, WestRay });
    }
    
    void Update()
    {
        gridStartPosition = this.transform.position;

        CheckForPlayer();
    }

    private void CheckForPlayer()
    {
        foreach (Vector3 direction in CardinalDirections)
        {
            if (isDetectingPlayer == true)
            {
                RaycastHit2D rangeChecker = RaycastManager(direction, isDetectingPlayer);

                RaycastHit2D facingDirection = RaycastManager(target.position, isAnimatorFacingDirection);
                Debug.DrawLine(transform.position, target.position);

                FacingDirection();

                Debug.DrawRay(transform.position, direction, Color.green);
                
                if (rangeChecker.collider)
                {
                    if (rangeChecker.collider.tag == "Player")
                    {
                        Debug.Log("I've hit the Player");
                        hasSpottedPlayer = true;
                        isDetectingWalls = true;

                        Charge(direction);
                    }
                    isDetectingPlayer = false;
                    break;
                }
            }
        }
    }

    private void FacingDirection()
    {
        Vector3 playerDirection = (target.position - transform.position);
        playerDirection = new Vector3((int)playerDirection.x, (int)playerDirection.y);

        //Debug.Log(playerDirection);

        if(isCharging == false)
        {
            anim.SetFloat("MoveX", playerDirection.x);
            anim.SetFloat("MoveY", playerDirection.y);
        }
    }

    private void CheckForWall(Vector3 direction)
    {
        if (isDetectingWalls == true)
        {
            Debug.Log("I'm now looking for walls");

            RaycastHit2D hit = RaycastManager(direction, isDetectingWalls);
            Debug.DrawRay(transform.position, direction, Color.green);

            if (hit.collider)
            {
                if (hit.collider.tag == "Obstacle")
                {
                    Debug.Log("I've hit an Obstacle");
                }

                Debug.Log("I've hit the collidable object " + hit.collider.name);
            }
        }
    }

    private void Charge(Vector3 direction)
    {
        if(hasSpottedPlayer == true)
        {
            StartCoroutine(Charging(direction));
            CheckForWall(direction);
        }
    }

    private IEnumerator Charging(Vector3 direction)
    {
        Vector3 newPosition = new Vector3(transform.position.x + direction.x, transform.position.y + direction.y);

        anim.SetFloat("MoveX", 0.001f);
        anim.SetFloat("MoveY", 0.001f);

        /*
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * 2;
            transform.position = Vector3.Lerp(transform.position, newPosition, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }
        */

        transform.position = Vector3.MoveTowards(transform.position, newPosition, 500 * Time.deltaTime);
        yield return null;
    }

    private RaycastHit2D RaycastManager(Vector3 direction, bool rayCastType)
    {
        float maxDirection = 0f;
        LayerMask mask = LayerMask.GetMask("Default");

        if (rayCastType == isDetectingPlayer)
        {
            maxDirection = 4;
            //mask = LayerMask.GetMask("Default");
        }

        if(rayCastType == isDetectingWalls)
        {
            maxDirection = 1;
            mask = LayerMask.GetMask("ObstacleDetection");
        }

        if(rayCastType == isAnimatorFacingDirection)
        {
            maxDirection = float.PositiveInfinity;
        }

        return Physics2D.Raycast(transform.position, direction, maxDirection, mask);
    }
}
