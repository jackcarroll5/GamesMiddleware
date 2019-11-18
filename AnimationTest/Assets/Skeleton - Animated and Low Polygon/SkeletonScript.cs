
using System;
using UnityEngine;
using UnityEngine.Serialization;

public class SkeletonScript : MonoBehaviour
{
    private Animator _thisAnimation;
    private Rigidbody _rigidBody;
    public float jumpingForce = 300f;
    public float groundDistance = 0.2f;
    private Transform leftHandObj;
    
    float moveSpeedMultiplier = 1f;
    float runCycleLegOffset = 0.2f;
    private const float KHalf = 0.5f;
   public float forwardAmount;
    Vector3 _groundNormal;
    private Vector3 _move;
    readonly float movingTurnSpeed = 45;
    readonly float stationaryTurnSpeed = 180;
    public LayerMask whatGround;
    
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int TurnSpeed = Animator.StringToHash("TurnSpeed");
    private static readonly int Jump = Animator.StringToHash("Jump");
    private static readonly int Grounded = Animator.StringToHash("Grounded");
    private static readonly int Attacker = Animator.StringToHash("Attacker");
    private static readonly int SwingHard = Animator.StringToHash("SwingHard");
    
    Transform _head;
    private Transform _chest;
    public Transform target;
    public Vector3 offset; 
    public Quaternion initRotation; 
    public Quaternion targetRot;
    
    [Range(0.0f,1.0f)]
    float timeValue = 0.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        _thisAnimation = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody>();
      
        

        //initRotation = head.rotation;
       //head.LookAt(target.position);
       //targetRot = head.rotation;
    }

    private void LateUpdate()
    {
       // var position = target.position;
       // head.LookAt(position);

       // var rotation = head.rotation;
       // rotation = rotation * Quaternion.Euler(offset);
       // rotation = Quaternion.Lerp( initRotation , _targetRot , timeValue);
       // _head.rotation = rotation;
       
        
       //Quaternion lookRotate = Quaternion.LookRotation((position - _head.position));
        //_head.rotation = lookRotate * initRotation;
        
        OnAnimatorIK(0);
    }

    private void OnAnimatorIK(int layerIndex)
    {
        _head = _thisAnimation.GetBoneTransform(HumanBodyBones.Head);
        
        var position = target.transform.position;
        
        _thisAnimation.SetLookAtPosition(position);
        
        _thisAnimation.SetBoneLocalRotation(HumanBodyBones.Head,Quaternion.Euler(position));
        
        //float distance = Vector3.Distance( _thisAnimation.GetBoneTransform(HumanBodyBones.Head).position, position);
        
        _thisAnimation.SetLookAtWeight(1.0f);
     
       // _thisAnimation.SetIKRotation(AvatarIKGoal.LeftHand,leftHandObj.rotation);
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
        target = GameObject.FindGameObjectWithTag("Target").transform;
        Debug.Log(target.name); 
        
        var vert = Input.GetAxis("Vertical");
        _thisAnimation.SetFloat(Speed, vert);
        
        var transform1 = transform;
        transform1.position += 0.3f * vert * transform1.forward;
        
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
