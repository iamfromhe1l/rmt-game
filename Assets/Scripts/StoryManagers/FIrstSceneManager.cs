using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Dialogues;
using UnityEngine;
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
    
    private int stage = 0;
    private int dialogsEnded = 0;
    private int dialogsStarted = 0;
    private CameraFollower _follower;
    private BoxCollider exitCollider;
    private string _currentTask;
    private DialoguesManager _dialoguesManager;
    private List<Animator> _dummyAnimators;
    private static readonly int Die = Animator.StringToHash("die");


    void Awake()
    {
        magicTrainingFieldExiting.gameObject.SetActive(false);
        exitCollider = magicTrainingFieldExiting.GetComponent<BoxCollider>();
        _follower = Camera.main!.GetComponent<CameraFollower>();
        _dialoguesManager = FindObjectOfType<DialoguesManager>();
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
                stage = 2;
            }
        }
        else if (stage == 2) // убить манекенов
        {
            if (dialogsEnded == 3)
                _currentTask = tasks[0];
            if (_dummyAnimators.All(animator => !animator.GetBool(Die))) // Check is working animator
            {
                tasks.Remove(_currentTask);
                _currentTask = tasks[0];
                Debug.Log("ended stage 2");
                stage = 3;
            }
                
        }
        else if (stage == 3) // Сжечь стены
        {
            // if (All walls are destroyed){    Indus Logic
            // tasks.Remove(_currentTask);
            // _currentTask = tasks[0];
            // stage = 4;
            // }
            
            stage = 4; //delete this after implementing the above
        }
        else if (stage == 4) // Сдвинуть коробки
        {
            // if (Any boxes are shifted){        Check Position Changing
            // tasks.Remove(_currentTask);
            // _currentTask = tasks[0];
            // magicTrainingFieldExiting.gameObject.SetActive(true);
            // stage = 5;
            // }
            
            //delete this after implementing the above
            magicTrainingFieldExiting.gameObject.SetActive(true);
            stage = 5; 
        }
        else if (stage == 5) // Подойти к учителю 
        {
            if (Physics.OverlapBox(exitCollider.bounds.center, exitCollider.bounds.extents, Quaternion.identity).Any(collider => collider.gameObject.CompareTag("HeroTag")))
            {
                tasks.Remove(_currentTask);
                _currentTask = "";
                stage = 6;
            }
        }
        else if (stage == 6) // Катсцена конца первой сцены
        {
            //LoadScene(2);
        }
        
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log(dialogsEnded);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Debug.Log(_currentTask);
        }
    }

}
