using UnityEngine;
using UnityEngine.SceneManagement;

namespace ScenesManager
{
    public class ExitSecondScene : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("HeroTag"))
            {
                FindObjectOfType<ScreenFader>().FadeToBlack();
                Invoke("nextScene",3f);
            }
        }

        private void nextScene()
        {
            SceneManager.LoadScene(4);
        }
    }
}