using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        private CharacterController _controller;
        [Header("Player Settings")] public float movementSpeed = 3f;
        private Vector3 _direction;
        private PlayerAnimator anim;
        [Header("Attack Settings")] public ParticleSystem fxAttack;
        [SerializeField]private bool isAttack;
        
        void Start()
        {
           
            _controller = GetComponent<CharacterController>();
            anim = GetComponent<PlayerAnimator>();
          
        }
        
        void Update()
        {
          PlayerMovement();
            CallPlayerAttack();
        }
        
        #region MovementsOfPlayer
            void PlayerMovement()
            {
                float horizontal = Input.GetAxis("Horizontal");
                float vertical = Input.GetAxis("Vertical");
                _direction = new Vector3(horizontal,0f,vertical).normalized;
                SetPlayerAngle();
                _controller.Move(_direction*movementSpeed*Time.deltaTime);
        
            }

        #region MovementAndAngle
            
        /*
         * This function verify that direction the player runs and rotate him in correct angle
         */
        void SetPlayerAngle()
        {
            if (_direction.magnitude > 0.1)
            {
                //Mathf.Deg ->Converte radianos para graus
                float targetAngle = Mathf.Atan2(_direction.x, _direction.z)* Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0,targetAngle,0);
                anim.SetWalkAnimation(true);
            }
            else
            {
                anim.SetWalkAnimation(false);
            }
        }
        

        #endregion
        

        #endregion

        #region AttacksOfPlayer
        void CallPlayerAttack()
        {
            if (Input.GetButtonDown("Fire1") && !isAttack)
            {
                isAttack = true;
                anim.CallAnimationPlayerAttack();
                fxAttack.Emit(1);//Amount of particles emitteds
               
            }
        }

        public void AttackIsDone()
        {
            isAttack = false;
        }
      

        #endregion
    
        
    
       
    }

}
