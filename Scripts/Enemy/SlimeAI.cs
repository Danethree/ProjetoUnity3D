using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using UnityEngine.AI;
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
         private bool isAlert,isPlayerVisible;
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
                    break;
                case EnemyState.ALERT:
                    //
                    break;
               
                case EnemyState.FOLLOW:
                    break;
                case EnemyState.FURY:
                    _destination = GameManager.instance.getPlayer();
                    _agent.stoppingDistance = GameManager.instance.slimeDistanceToAttack;
                    _agent.destination = _destination;
                    break;
                case EnemyState.PATROL:
                    break;
            }
        }

        void ChangeState(EnemyState newState)
        {
            
            StopAllCoroutines();
            enemyState = newState;
            switch (enemyState)
            {
                case EnemyState.IDLE:
                    _destination = transform.position;
                    _agent.destination = _destination;
                    StartCoroutine("IDLE");
                    break;
                case EnemyState.ALERT:
                    _agent.stoppingDistance = 0;
                    _destination = transform.position;
                    _agent.destination = _destination;
                    isAlert = true;
                    break;
                case EnemyState.PATROL:
                    _agent.stoppingDistance = 0;
                    StartCoroutine("PATROL");
                    break;
                case EnemyState.FURY:
                  
                    break;
                    
            }
        }

       
        
        #endregion
        #region Behaviour of Enemy

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
            if (other.gameObject.tag == "Player" && enemyState!= EnemyState.FURY)
            {
                ChangeState(EnemyState.ALERT);
            }
        }
    

        #endregion
    }

   
}

