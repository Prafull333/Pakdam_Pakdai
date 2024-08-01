using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using TMPro;

[RequireComponent(typeof(Player))]
public class AiPlayer : ThirdPersonController
{
    bool targetreach = false;
    public Vector3 targetLocation;

    public float tarnClamTimer;
    public float tarnClamVelocity;
    public float targetDestance;

    public GameObject nearestPlayer;
    public float nearestPlayerDistance = Mathf.Infinity;

    // Start is called before the first frame update
    internal override void Start()
    {
        _hasAnimator = TryGetComponent(out _animator);
        _controller = GetComponent<CharacterController>();

        GetComponentInChildren<TextMeshPro>().text = GetComponent<Player>().nameString;
        changeTargetLocation();
    }

    internal override void Update()
    {
        targetDestance = GetTargetDistance();
        Move();

        if (GameManager.Instance.Players.Count < 3)
        {
            nearestPlayer = GameManager.Instance.Players[0];
        }

        if (GetComponent<Player>().isRaider)
        {
            IamRaider();
        }
    }

    internal override void LateUpdate()
    {
      
    }

    void changeTargetLocation()
    {
        // USe for set Target 
        targetLocation = new Vector3(UnityEngine.Random.Range(-30, 30), 0, UnityEngine.Random.Range(-30, 30));
    }

    internal override void Move()
    {
        if(!GetComponent<Player>().isRaider)
        {
            MoveNormal();
        }
        else
        {
            MoveWhenRaider();
        }
    }

    void MoveNormal()
    {
        float moveSpeed = MoveSpeed;

        if (targetDestance < 5)
        {
            changeTargetLocation();
        }
        else if (targetDestance < 20)
        {
            // Use for Sprint(Running) Setting
            moveSpeed = SprintSpeed;
            _animator.SetFloat("Speed", 6);
        }
        else
        {
            _animator.SetFloat("Speed", 2);
        }
        _animator.SetFloat("MotionSpeed", 1f);

        Vector3 offset = new Vector3(transform.position.x - targetLocation.x,
           transform.position.y - targetLocation.y, transform.position.z - targetLocation.z);
        float newAngle = Mathf.Atan2(-offset.x, -offset.z) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.Euler(0, newAngle, 0),1f);

        var moveDirection = Quaternion.Euler(0, newAngle, 0f) * Vector3.forward;
        _controller.Move(moveDirection * moveSpeed * Time.deltaTime);

    }

    void MoveWhenRaider()
    {
        float moveSpeed = MoveSpeed;

        if (targetDestance < 20 && targetDestance > 5)
        {
            // Use for Sprint(Running) Setting
            moveSpeed = SprintSpeed;
            _animator.SetFloat("Speed", 6);
        }
        else
        {
            _animator.SetFloat("Speed", 2);
        }
        _animator.SetFloat("MotionSpeed", 1f);

        Vector3 offset = new Vector3(transform.position.x - targetLocation.x,
           transform.position.y - targetLocation.y, transform.position.z - targetLocation.z);
        float newAngle = Mathf.Atan2(-offset.x, -offset.z) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.Euler(0, newAngle, 0),1f);

        var moveDirection = Quaternion.Euler(0, newAngle, 0f) * Vector3.forward;
        _controller.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    void IamRaider()
    {

            for (int i = 0; i < GameManager.Instance.Players.Count; i++)
            {
                for (int j = 0; j < GameManager.Instance.Players.Count; j++)
                {
                    if (i == j || GameManager.Instance.Players[j] == this.gameObject
                        || GameManager.Instance.Players[i] == this.gameObject)
                    { continue; }

                    float dis = Vector3.Distance(transform.position,
                        GameManager.Instance.Players[j].transform.position);

                    if (nearestPlayer != null || nearestPlayerDistance < dis)
                    {
                        nearestPlayerDistance = Vector3.Distance(transform.position,
                        nearestPlayer.transform.position);
                    }

                    if (nearestPlayer == null || nearestPlayerDistance > dis)
                    {
                        nearestPlayer = GameManager.Instance.Players[j];
                        nearestPlayerDistance = dis;
                    }

                }
            }
        

       targetLocation = nearestPlayer.transform.position;
    }

    float GetTargetDistance()
    {
        float dis = Vector3.Distance(transform.position, targetLocation);
        return dis;
    }
}
