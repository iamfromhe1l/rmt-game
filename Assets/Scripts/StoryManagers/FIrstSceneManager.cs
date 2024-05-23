using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Dialogues;
using Microsoft.Unity.VisualStudio.Editor;
using ScenesManager;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;


public class FIrstSceneManager : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform teacherTransform;
    [SerializeField] private Transform skeletonTransform;
    [SerializeField] private Transform magicTrainingFieldExiting;
    [SerializeField] private GameObject firstMonolog;
    [SerializeField] private List<GameObject> dummys;
    [SerializeField] private List<GameObject> walls;
    [SerializeField] private List<GameObject> boxes;
    [SerializeField] private List<string> tasks;
    [SerializeField] private List<GameObject> enemies;
    [SerializeField] private Transform arrow;
    [SerializeField] private GameObject shooter;

    
    private int stage = 0;
    private int dialogsEnded = 0;
    private int dialogsStarted = 0;
    private CameraFollower _follower;
    private BoxCollider exitCollider;
    [SerializeField] private string _currentTask;
    private DialoguesManager _dialoguesManager;
    private Animator shooterAnimator;
    private List<Animator> _dummyAnimators;
    private List<Vector3> _initialBoxPositions;
    private List<UnityEngine.AI.NavMeshAgent> _navMeshAgents;
    public float firingAngle = 45.0f;
    public float gravity = 9.8f;


    void Awake()
    {
        magicTrainingFieldExiting.gameObject.SetActive(false);
        exitCollider = magicTrainingFieldExiting.GetComponent<BoxCollider>();
        _follower = Camera.main!.GetComponent<CameraFollower>();
        _dialoguesManager = FindObjectOfType<DialoguesManager>();
        _navMeshAgents = enemies.Select(enemy => enemy.GetComponent<UnityEngine.AI.NavMeshAgent>()).ToList();
        shooterAnimator = shooter.GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _dialoguesManager.OnDialogueEnded += () => dialogsEnded+=1;
        _dialoguesManager.OnDialogueStarted += () => dialogsStarted+=1;
        _dummyAnimators = dummys.Select(dummy => dummy.GetComponent<Animator>()).ToList();
    }

    void Start()
    {
        firstMonolog.SetActive(false);
    }
    

    void Update()
    {
        if (stage == 0) // Первый монолог
        {
            if (_follower.isFirstOccurrence)
            {
                firstMonolog.SetActive(true);
                stage = 1;
            }
        }
        else if (stage == 1) // Диалог с учителем
        {
            if (dialogsEnded == 1)
                _currentTask = tasks[0];
            if (dialogsStarted == 2)
            {
                tasks.Remove(_currentTask);
                _currentTask = "";
                _initialBoxPositions = boxes.Select(box => box.transform.position).ToList();
                stage = 2;
            }
        }
        else if (stage == 2) // убить манекенов
        {
            if (dialogsEnded == 2)
                _currentTask = tasks[0];
            if (_dummyAnimators.All(animator => animator.GetBool("Die")))
            {
                tasks.Remove(_currentTask);
                _currentTask = tasks[0];
                stage = 3;
            }
        }
        else if (stage == 3) // Сжечь стены
        {
            if (walls.All(wall => !wall.activeInHierarchy))
            {
                Debug.Log("Stage 3 is done");
                tasks.Remove(_currentTask);
                _currentTask = tasks[0];
                stage = 4;
            }
        }
        else if (stage == 4) // Сдвинуть коробки
        {
            if (boxes.Select((box, index) => new { box = box, index = index })
                .Any(x => x.box.transform.position != _initialBoxPositions[x.index]))
            {
                tasks.Remove(_currentTask);
                _currentTask = tasks[0];
                magicTrainingFieldExiting.gameObject.SetActive(true);
                stage = 5;
            }
        }
        else if (stage == 5) // Подойти к учителю 
        {
            if (Physics.OverlapBox(exitCollider.bounds.center, exitCollider.bounds.extents, Quaternion.identity).Any(collider => collider.gameObject.CompareTag("HeroTag")))
            {
                tasks.Remove(_currentTask);
                _currentTask = "";
                stage = 6;
                foreach (GameObject enemy in enemies)
                {
                    enemy.SetActive(true);
                    Animator animator = enemy.GetComponent<Animator>();
                    animator.SetBool("IsWalking", true);
                    animator.SetBool("IsRunning", true);
                }
            }
        }
        else if (stage == 6) // Враги бегут к нам
        {
            foreach (var agent in _navMeshAgents)
            {
                agent.enabled = true;
                agent.destination = playerTransform.position;
            }
            shooter.SetActive(true);
            _follower.SetTarget(skeletonTransform);
            Invoke("MakeShooter",5f);
            Invoke("AfterEnemies",7f);
        }
        if (stage == 7) // Полет стрелы
        {
            StartCoroutine("SimulateProjectile");
        }
        if (stage == 8)
        {
            playerTransform.gameObject.GetComponent<Animator>().SetBool("IsDyingFront", true); 
            Invoke("StartFade",1f);
            Invoke("EndGame",3f);
            stage = 9;
        }
    }
    

    void MakeShooter()
    {
        shooterAnimator.SetBool("IsAttacking", true);
    }
    void StartFade()
    {
        Debug.Log("Start Fade Exec");
        FindObjectOfType<ScreenFader>().FadeToBlack();
    }
    void EndGame()
    {
        SceneManager.LoadScene(3);
    }
    void AfterEnemies()
    {
        stage = 7;
        _follower.SetTarget(arrow); 
    }
    
    IEnumerator SimulateProjectile()
    {
        Vector3 startPoint = arrow.position;
        Vector3 endPoint = playerTransform.position + Vector3.up/2;
        float speed = 10;
        float height = 5;
        float distance = Vector3.Distance(startPoint, endPoint);
        float time = distance / speed;
        float timer = 0f;
        while (timer < time)
        {
            float t = timer / time;
            Vector3 newPos = Vector3.Lerp(startPoint, endPoint, t) + Vector3.up * height * Mathf.Sin(t * Mathf.PI);
            arrow.position = newPos;
            Vector3 direction = (Vector3.Lerp(startPoint, endPoint, t + 0.01f) - newPos).normalized;
            arrow.rotation = Quaternion.LookRotation(direction);
            timer += Time.deltaTime;
            yield return null;
        }
        arrow.position = endPoint;
        arrow.LookAt(endPoint);
        stage = 8;
    }
}
