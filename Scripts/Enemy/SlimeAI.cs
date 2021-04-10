using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Enemy
{
    public class SlimeAI : MonoBehaviour,ICharComponents
    {
        private EnemyAnimations anim;
        void Start()
        {
            anim = GetComponent<EnemyAnimations>();
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }


        public void GetHit(int amount)
        {
           anim.EnemytHit(1);
        }
    }
}

