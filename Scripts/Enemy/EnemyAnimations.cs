using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Enemy
{
    public class EnemyAnimations : MonoBehaviour
    {
       private Animator enemyAnim;
       
        // Start is called before the first frame update
        void Start()
        {
            enemyAnim = GetComponent<Animator>();
        }

        #region EnemyDamage
        public void EnemytHit(int amount)
        {
            enemyAnim.SetTrigger("GetHit");
        }
    

        #endregion

        #region  EnemyDies
        public void EnemyDieAnimation()
        {
            enemyAnim.SetTrigger("Die");
        }

        public void enemySetWalkAnimation(bool isWalk)
        {
            enemyAnim.SetBool("isWalk",isWalk);
        }

        public void enemySetAlertAnimation(bool isAlert)
        {
            enemyAnim.SetBool("isAlert",isAlert);
        }

        public void enemySetAttackAnimation()
        {
            enemyAnim.SetTrigger("Attack");
        }

        #endregion
    }
}

