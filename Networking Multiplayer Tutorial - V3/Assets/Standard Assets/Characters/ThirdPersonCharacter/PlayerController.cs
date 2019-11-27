using UnityEngine;

namespace Standard_Assets.Characters.ThirdPersonCharacter
{
    public class PlayerController : MonoBehaviour
    {
        private Animator thisAnimation;
        private Rigidbody rigBody;
        public float JumpingForce = 500f;
        public float groundDistance = 0.3f;
        public LayerMask whatGround;
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int TurnSpeed = Animator.StringToHash("TurnSpeed");
        private static readonly int Jump = Animator.StringToHash("Jump");
        private static readonly int Grounded = Animator.StringToHash("Grounded");

        // Start is called before the first frame update
        void Start()
        {
            thisAnimation = GetComponent<Animator>();
            rigBody = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            var vert = Input.GetAxis("Vertical");
            var horizontal = Input.GetAxis("Horizontal");

            thisAnimation.SetFloat(Speed, vert);
            thisAnimation.SetFloat(TurnSpeed, horizontal);

            if(Input.GetButtonDown("Jump"))
            {
                rigBody.AddForce(Vector3.up * JumpingForce);
                thisAnimation.SetTrigger(Jump);
            }

            if(Physics.Raycast(transform.position + (Vector3.up * 0.1f),Vector3.down, groundDistance, whatGround))
            {
                thisAnimation.SetBool(Grounded,true);
                thisAnimation.applyRootMotion = true;
            }
            else
            {
                thisAnimation.SetBool(Grounded, false);
            }
       
        }
    }
}
