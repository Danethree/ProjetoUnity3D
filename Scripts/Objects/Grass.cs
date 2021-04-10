using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Objects
{
    public class Grass : MonoBehaviour,ICharComponents
    {
        public ParticleSystem fxHit;
        private bool isCut = false;
       public void GetHit(int amount)
        {
            if (!isCut)
            {
                isCut = true;
                fxHit.Emit(10);
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
          
        }
    }

}
