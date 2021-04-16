using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Scripts.Enemy
{
    public class SlimeAI : MonoBehaviour,ICharComponents
    {
        private EnemyAnimations _anim;
         private ParticleSystem _blood;
         public int _hp;
         
         public EnemyState enemyState;

         
         private NavMeshAgent _agent;
         private Vector3 _destination;
         private int _idwayPoint;
         private bool isAlert,isPlayerVisible,isAttack;
        void Start()
        {
            
            _agent = GetComponent<NavMeshAgent>();
           
            _anim = GetComponent<EnemyAnimations>();
                _blood = GameObject.FindWithTag("Blood").GetComponentInChildren<ParticleSystem>();
                _hp = 3;
              
                ChangeState(enemyState);
               

        }

            // Update is called once per frame
            void Update()
            {
                EnemyStatesManagement();
               WhenSlimeWalks();
               setAlertAnimation();
            }

        

            void setAlertAnimation()
            {
                _anim.enemySetAlertAnimation(isAlert);
            }

            void WhenSlimeWalks()
            {
                
                if (_agent.desiredVelocity.magnitude >= 0.1f)
                {
                    _anim.enemySetWalkAnimation(true);
                }
                else
                {
                    
                    _anim.enemySetWalkAnimation(false);
                }
                
            }

      
        
        
        public void GetHit(int amount)
        {
            _hp --;
            if (_hp > 0)
            {
                ChangeState(EnemyState.FURY);
                _anim.EnemytHit(1);
                _blood.Emit(20);

            }
            else
            {
                _anim.EnemyDieAnimation();
               
                Destroy(this.gameObject,1.40f);
            }
          
        }

        #region States of Enemy
        public void EnemyStatesManagement()
        {
            switch (enemyState)
            {
                case EnemyState.IDLE:
                    //
                    _destination = transform.position;
                    _agent.destination = _destination;
                    break;
                case EnemyState.ALERT:
                    _agent.stoppingDistance = 0;
                    _destination = transform.position;
                    _agent.destination = _destination;
                    isAlert = true;
                    break;
               
                case EnemyState.FOLLOW:
                    _agent.stoppingDistance = GameManager.instance.slimeDistanceToAttack;
                    _agent.destination = GameManager.instance.getPlayer();
                    if (_agent.remainingDistance <= _agent.stoppingDistance)
                    {
                        Attack();
                    }
                    break;
                case EnemyState.FURY:
                    _destination = GameManager.instance.getPlayer();
                    _agent.stoppingDistance = GameManager.instance.slimeDistanceToAttack;
                    _agent.destination = _destination;
                    if (_agent.remainingDistance <= _agent.stoppingDistance)
                    {
                        Attack();
                    }
                    break;
                case EnemyState.PATROL:
                    break;
            }
        }

        void ChangeState(EnemyState newState)
        {
            
            StopAllCoroutines();
            enemyState = newState;
            isAlert = false;
            switch (enemyState)
            {
                case EnemyState.IDLE:
                    
                    StartCoroutine("IDLE");
                    break;
                case EnemyState.ALERT:
                    
                    StartCoroutine("ALERT");
                    break;
                case EnemyState.PATROL:
                    _agent.stoppingDistance = 0;
                    StartCoroutine("PATROL");
                    break;
                case EnemyState.FOLLOW:
                    
                    StartCoroutine("ATTACK");
                    break;
                case EnemyState.FURY:
                  
                    StartCoroutine("ATTACK");
                    break;
                    
            }
        }

       
        
        #endregion
        #region Coroutines

        IEnumerator IDLE()
        {
            yield return new WaitForSeconds(GameManager.instance.slimeIdleWaitTime);
            StayStill(50);
        }

        IEnumerator PATROL()
        {
            _idwayPoint = Random.Range(0, GameManager.instance.slimeWayPoints.Length);
            _destination = GameManager.instance.slimeWayPoints[_idwayPoint].position;
            _agent.destination = _destination;
            yield return new WaitUntil(()=>_agent.remainingDistance <=0);
            StayStill(30);
        }

        IEnumerator ALERT()
        {
            yield return new WaitForSeconds(GameManager.instance.slimeIdleWaitTime);
            if (isPlayerVisible)
            {
                ChangeState(EnemyState.FOLLOW);
            }
            else
            {
                StayStill(10);
            }
            
        }

        IEnumerator ATTACK()
        {
            yield return new WaitForSeconds(GameManager.instance.slimeAttackDelay);
            isAttack = false;
        }

       
        
        #endregion
        
        
        #region Random function

        int Rand()
        {
            int rand;
            rand = Random.Range(0, 100);
            return rand;
        }
        

        #endregion
       
        
        #region ProbabilityToChangeStateToIdleOrPatrol
        void StayStill(int yes)
        {
            if (Rand() <= yes)
            {
                ChangeState(EnemyState.IDLE);
            }
            else
            {
                ChangeState(EnemyState.PATROL);
            }
        }
        

        #endregion
        #region OnTrigger
        void OnTriggerEnter(Collider other)
        {
            isPlayerVisible = true;
            if (other.gameObject.tag == "Player" && enemyState!= EnemyState.FURY)
            {
                ChangeState(EnemyState.ALERT);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                isPlayerVisible = false;
            }
        }

        #endregion

        void Attack()
        {
            if (!isAttack)
            {
                isAttack = true;
                _anim.enemySetAttackAnimation();
            }
           
        }

        void AttackIsDone()
        {

            StartCoroutine("ATTACK");

        }
    }

   
}

