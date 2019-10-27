using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonScript : MonoBehaviour
{
    private Animator _thisAnimation;
    private Rigidbody _rigidBody;
    public float jumpingForce = 300f;
    public float groundDistance = 0.2f;
    public LayerMask whatGround;
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int TurnSpeed = Animator.StringToHash("TurnSpeed");
    private static readonly int Jump = Animator.StringToHash("Jump");
    private static readonly int Grounded = Animator.StringToHash("Grounded");
    private static readonly int Attacker = Animator.StringToHash("Attacker");
    private static readonly int SwingHard = Animator.StringToHash("SwingHard");
    
    // Start is called before the first frame update
    void Start()
    {
        _thisAnimation = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void LateUpdate()
    {


        //_thisAnimation.GetBoneTransform(HumanBodyBones.Chest);
    }

    // Update is called once per frame
    void Update()
    {
        var vert = Input.GetAxis("Vertical");
        var horizontal = Input.GetAxis("Horizontal");

        _thisAnimation.SetFloat(Speed, vert);
        _thisAnimation.SetFloat(TurnSpeed, horizontal);

        if(Input.GetButtonDown("Jump"))
        {
            _rigidBody.AddForce(Vector3.up * jumpingForce);
            _thisAnimation.SetTrigger(Jump);
        }
        
        if(Physics.Raycast(transform.position + (Vector3.up * 0.1f),Vector3.down, groundDistance, whatGround))
        {
            _thisAnimation.SetBool(Grounded,true);
            _thisAnimation.applyRootMotion = true;
        }
        else
        {
            _thisAnimation.SetBool(Grounded, false);
        }
        
        if(Input.GetKeyDown((KeyCode.L)))
        {
            _thisAnimation.SetTrigger(Attacker);
        }
        
        if(Input.GetKeyDown((KeyCode.P)))
        {
          _thisAnimation.SetTrigger(SwingHard);
        }

        
       
    }
}
