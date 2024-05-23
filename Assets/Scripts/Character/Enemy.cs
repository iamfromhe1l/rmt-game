using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;


namespace Assets.Scripts.Character
{
    public class Enemy : Character
    {
        PatroleTypes patroleType;
        float minDistance;
        [Header("Игрок")]
        public Transform myPlayer;
        private Transform _target;
        private NavMeshAgent myAgent;
        [SerializeField]
        private float _gravity = 11f;
        private CharacterController _characterController;
        private Vector3 _velocity;
        private Vector3 _moveDirection;
        [SerializeField]
        private float DetectionDistanse = 10;
        private float AttackDistance;
        private Weapon weapon;
        private AnimatorStateInfo _stateInfo;
        bool _isAttacking;
        private bool isDied;
        float timeOut;
        bool isWaiting = false;
        public bool IsDied
        {
            set { isDied = value; }
        }
        void Initialize(EnemyTypes type)
        {
            var allDistanceInfos = Resources.Load<EnemyDistanceScriptableObject>("EnemyDistanceScriptableObject");
            minDistance = allDistanceInfos.Distances.Where(p => p.EnemyType == type).First().Distance;
        }
        private void Awake()
        {
            _target = myPlayer;
            _characterController = GetComponent<CharacterController>();
            _animator = GetComponent<Animator>();
            myAgent = GetComponent<NavMeshAgent>();
            _stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
            switch (gameObject.tag)
            {
                case "Wizard":
                    AttackDistance = 4f;
                    weapon = GetComponent<MagicFire>();
                    break;
                case "Meele":
                    AttackDistance = 1.2f;
                    weapon = GetComponent<MeeleSword>();
                    break;
                case "Warrior":
                    AttackDistance = 1.2f;
                    weapon = GetComponent<MeeleAxe>();
                    break;
            }
        }
        void Update()
        {
            if (!isDied)
            {
                _stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
                IsAttacking();
                if (_isAttacking)
                {
                    myAgent.enabled = false;
                    return;
                }
                Move();
            }
        }
        private void Move()
        {
            if (Vector3.Distance(transform.position, _target.transform.position) >= DetectionDistanse)
            {
                myAgent.enabled = false;
                _animator.SetBool("IsWalking", false);
                _animator.SetBool("IsRunning", false);
            }

            if (Vector3.Distance(transform.position, _target.transform.position) <= DetectionDistanse)
            {
                    myAgent.enabled = true;
                    myAgent.destination = myPlayer.transform.position;
                    _animator.SetBool("IsWalking", true);
                    _animator.SetBool("IsRunning", true);
            }
            if (Vector3.Distance(transform.position, _target.transform.position) <= AttackDistance)
            {
                myAgent.enabled = false;
                _animator.SetBool("IsWalking", false);
                _animator.SetBool("IsRunning", false);
                 transform.LookAt(new Vector3(_target.position.x, transform.position.y, _target.position.z));
                timeOut = weapon.GetTimeOut();
                if (isWaiting == false)
                {
                    _animator.SetTrigger("IsAttacking");
                    weapon.Attack();
                    isWaiting = true;
                    StartCoroutine(WaitTime());
                }
            }
        }

        IEnumerator WaitTime()
        {
            yield return new WaitForSeconds(timeOut);
            isWaiting = false;
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
        void IsAttacking()
        {
            if (_stateInfo.IsName("1H_Melee_Attack_Slice_Diagonal") ||
                 _stateInfo.IsName("Spellcast_Shoot") ||
                    _stateInfo.IsName("2H_Melee_Attack_Spin"))
            {
                _isAttacking = true;
                return;
            }
            _isAttacking = false;
        }

        override protected void Death()
        {

        }
        protected void GodAction()
        {
        }
    }
}
