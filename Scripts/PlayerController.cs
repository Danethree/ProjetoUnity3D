using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController _controller;
    [Header("Player Settings")] public float movementSpeed = 3f;
    private Vector3 _direction;
    private PlayerAnimator anim;
    
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

    void PlayerMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        _direction = new Vector3(horizontal,0f,vertical).normalized;
         SetPlayerAngle();
        _controller.Move(_direction*movementSpeed*Time.deltaTime);

    }

    /*
     * Esta função verifica em que direção o player anda e "gira" para o ângulo correto.
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

    void CallPlayerAttack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            anim.CallAnimationPlayerAttack();
        }
    }
}
