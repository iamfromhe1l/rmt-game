using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Random = System.Random;

public class SceneTransition : MonoBehaviour
{
    [SerializeField]
    private TMP_Text advice;
    private List<string> advices;
    private static SceneTransition instance;
    private static bool shouldPlayAnumation = false;
    private Animator componentAnimator;
    private AsyncOperation loadingSceneOperation;
    Transform headTransform;
    public static void SwitchToScene(string sceneName)
    {
        instance.componentAnimator.SetTrigger("SceneClosing");
        instance.loadingSceneOperation = SceneManager.LoadSceneAsync(sceneName);
        instance.loadingSceneOperation.allowSceneActivation = false;
    }
    void Start()
    {
        instance = this;
        componentAnimator = GetComponent<Animator>();
        if (shouldPlayAnumation) componentAnimator.SetTrigger("SceneOpening");
        Random rnd = new Random();
        int value = rnd.Next(0, advices.Count);
        advice.text = advices[value];
    }
    private void Awake()
    {
        advices = Resources.Load<AdvicesScriptableObject>("Advices").Advices;
        headTransform = transform.Find("SkeletonHead");
    }

    // Update is called once per frame
    void Update()
    {
        if (headTransform != null)
            headTransform.Rotate(new Vector3(0, 100f, 0) * Time.deltaTime);
    }

    public void OnAnimationOver()
    {
        shouldPlayAnumation = true;
        loadingSceneOperation.allowSceneActivation = true;
    }
}
