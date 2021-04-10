using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Enemy
{
    public class EnemyAnimations : MonoBehaviour
    {
        [SerializeField]private Animator enemyAnim;
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
    }
}

