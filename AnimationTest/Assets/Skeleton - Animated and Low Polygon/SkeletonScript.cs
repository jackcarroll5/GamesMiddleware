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
    float moveSpeedMultiplier = 1f;
    float runCycleLegOffset = 0.2f;
    private const float KHalf = 0.5f;
    float forwardAmount;
    Vector3 _groundNormal;
    private Vector3 _move;
    float movingTurnSpeed = 90;
    float stationaryTurnSpeed = 180;
    public LayerMask whatGround;
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int TurnSpeed = Animator.StringToHash("TurnSpeed");
    private static readonly int Jump = Animator.StringToHash("Jump");
    private static readonly int Grounded = Animator.StringToHash("Grounded");
    private static readonly int Attacker = Animator.StringToHash("Attacker");
    private static readonly int SwingHard = Animator.StringToHash("SwingHard");

    public Transform chest;
    public Transform head;
    public Transform cubeTarget;
    public Vector3 Offset;
    
    // Start is called before the first frame update
    void Start()
    {
        _thisAnimation = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody>();
        
       chest = _thisAnimation.GetBoneTransform(HumanBodyBones.Chest);
       head = _thisAnimation.GetBoneTransform((HumanBodyBones.Head));
    }

    private void LateUpdate()
    {


       
    }
    
    public void OnAnimatorMove()
    {
        // we implement this function to override the default root motion.
        // this allows us to modify the positional speed before it's applied.
        if (Time.deltaTime > 0)
        {
            Vector3 v = (_thisAnimation.deltaPosition * moveSpeedMultiplier) / Time.deltaTime;

            // we preserve the existing y part of the current velocity.
            v.y = _rigidBody.velocity.y;
            _rigidBody.velocity = v;
        }
    }
   
      
    
    // Update is called once per frame
    void Update()
    {
        var vert = Input.GetAxis("Vertical");
        _thisAnimation.SetFloat(Speed, vert);
        transform.position += transform.forward * vert * 0.3f;
        
        var horizontal = Input.GetAxis("Horizontal");
        _thisAnimation.SetFloat(TurnSpeed, horizontal);
        
        
        // help the character turn faster (this is in addition to root rotation in the animation)
        float turnSpeed = Mathf.Lerp(stationaryTurnSpeed, movingTurnSpeed, forwardAmount);
        transform.Rotate(0, horizontal * turnSpeed * Time.deltaTime, 0);
        
        
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
        
        float runCycle =
            Mathf.Repeat(
                _thisAnimation.GetCurrentAnimatorStateInfo(0).normalizedTime + runCycleLegOffset, 1);
        float jumpLeg = (runCycle < KHalf ? 1 : -1) * forwardAmount;

        
        if (_thisAnimation.GetBool(Grounded))
        {
            _thisAnimation.SetFloat("JumpLeg", jumpLeg);
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
