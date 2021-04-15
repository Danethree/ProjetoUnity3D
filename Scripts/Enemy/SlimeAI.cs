using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
namespace Scripts.Enemy
{
    public class SlimeAI : MonoBehaviour,ICharComponents
    {
        private EnemyAnimations _anim;
         private ParticleSystem _blood;
         public int _hp;
         public SkinnedMeshRenderer enemyRenderer;
         public EnemyState enemyState;
         public const float idleWaitTime = 3f;
         public const float patrolWaitTime = 3f;
         
         
        void Start()
        {
            enemyRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
            _anim = GetComponent<EnemyAnimations>();
                _blood = GameObject.FindWithTag("Blood").GetComponentInChildren<ParticleSystem>();
                _hp = 3;
                ChangeState(enemyState);
               

        }

            // Update is called once per frame
            void Update()
            {
               
            }

      

      
        
        
        public void GetHit(int amount)
        {
            _hp --;
            if (_hp > 0)
            {
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
                case EnemyState.EXPLORE:
                    //
                    break;
                case EnemyState.FOLLOW:
                    break;
                case EnemyState.FURY:
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
                    StartCoroutine("IDLE");
                    break;
                case EnemyState.ALERT:
                    break;
                case EnemyState.PATROL:
                    StartCoroutine("PATROL");
                    break;
                    
            }
        }

        #region Behaviour of Enemy

        IEnumerator IDLE()
        {
           yield return new WaitForSeconds(idleWaitTime);
           if (Rand() < 50)
           {
               ChangeState(EnemyState.IDLE);
           }
           else
           {
               ChangeState(EnemyState.PATROL);
           }
        }

        IEnumerator PATROL()
        {
            yield return new WaitForSeconds(patrolWaitTime);
            ChangeState(EnemyState.IDLE);
        }
        

        #endregion
        #endregion

        #region Random function

        int Rand()
        {
            int rand;
            rand = Random.Range(0, 100);
            return rand;
        }
        

        #endregion
       
    }
}

