using UnityEngine;

namespace ScenesManager
{
    public class ArrowCollision : MonoBehaviour
    {
        public delegate void ArrowHitHero();
        public static event ArrowHitHero OnArrowHitHero;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("HeroTag"))
            {
                Debug.Log("Enter in Collider");
                OnArrowHitHero?.Invoke();
            }
        }
    }
}