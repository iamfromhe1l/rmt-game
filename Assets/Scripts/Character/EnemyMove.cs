using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyMove : MonoBehaviour
{
    [SerializeField]
    float helthpoints;
    [Header("Игрок")]
    public Transform myPlayer;
    private Transform _target;
    private NavMeshAgent myAgent;
    [SerializeField]
    private float _gravity = 11f;
    private CharacterController _characterController;
    private Animator _animator;
    private Vector3 _velocity;
    private Vector3 _moveDirection;
    [SerializeField]
    private float DetectionDistanse = 10;
    private float AttackDistance;


    // Start is called before the first frame update
    void Start()
    {

    }
    private void Awake()
    {
        _target = myPlayer;
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        myAgent = GetComponent<NavMeshAgent>();
        tag = gameObject.tag;
        switch(tag)
        {
            case "Wizard":
                AttackDistance = 5f;
                break;
            case "Melee":
                AttackDistance = 1.4f;
                break;
            case "Warrior":
                AttackDistance = 1.4f;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, _target.transform.position) >= DetectionDistanse)
        {
            myAgent.enabled = false;
            _animator.SetBool("isWalking", false);
            _animator.SetBool("isRunning", false);
        }

        if (Vector3.Distance(transform.position, _target.transform.position) <= DetectionDistanse)
        {
            myAgent.enabled = true;
            myAgent.destination = myPlayer.transform.position;
            _animator.SetBool("isWalking", false);
            _animator.SetBool("isRunning", true);
        }
        if (Vector3.Distance(transform.position, _target.transform.position) <= AttackDistance)
        {
            myAgent.enabled = false;
            _animator.SetBool("isWalking", false);
            _animator.SetBool("isRunning", false);
            transform.LookAt(new Vector3(_target.position.x, transform.position.y, _target.position.z));
            
        }
    }

    void FixedUpdate()
    {
        GravityMovement(_characterController.isGrounded);
    }

    void GravityMovement(bool isGrounded)
    {
        if (isGrounded && _velocity.y < 0.0f)
            _velocity.y = -1f;
        else
        {
            _velocity.y -= _gravity * Time.fixedDeltaTime;
            _characterController.Move(_velocity * Time.fixedDeltaTime);
        }
    }
}
