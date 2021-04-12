using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Enemy
{
    public class SlimeAI : MonoBehaviour,ICharComponents
    {
        private EnemyAnimations _anim;
         private ParticleSystem _blood;
         public int _hp;
         public SkinnedMeshRenderer enemyRenderer;
         
        void Start()
        {
            enemyRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
            _anim = GetComponent<EnemyAnimations>();
                _blood = GameObject.FindWithTag("Blood").GetComponentInChildren<ParticleSystem>();
                _hp = 3;
               

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
    }
}

