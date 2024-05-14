using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Scripts.Character
{
    public class Character : MonoBehaviour
    {
        //protected Health _health;
        //protected Weapon _weapon;
        protected Animator _animator;
        protected bool IsDialog;
        virtual protected void Attack() { }
        virtual protected void Death() { }
        void changeAnim(string trig)
        {
            _animator.SetTrigger(trig);
        }
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}