using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviourPunCallbacks
{
    [FormerlySerializedAs("m_Speed")] public float mSpeed = 12f;                 // How fast the tank moves forward and back.
    [FormerlySerializedAs("m_TurnSpeed")] public float mTurnSpeed = 180f;            // How fast the tank turns in degrees per second.
    private Rigidbody m_Rigidbody;              // Reference used to move the tank.
    private float _mMovementInputValue;         // The current value of the movement input.
    private float _mTurnInputValue;             // The current value of the turn input.
 
 
    private void Awake ()
    {
        m_Rigidbody = GetComponent<Rigidbody> ();
 
        if(!photonView.IsMine)
        {
            enabled = false;
        }
    }
   private void OnEnable ()
    {
        // When the tank is turned on, make sure it's not kinematic.
        m_Rigidbody.isKinematic = false;
 
        // Also reset the input values.
        _mMovementInputValue = 0f;
        _mTurnInputValue = 0f;
    }
 
 
     private void OnDisable ()
    {
        // When the tank is turned off, set it to kinematic so it stops moving.
        m_Rigidbody.isKinematic = true;
    }

    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    private void FixedUpdate ()
    {
        // Adjust the rigidbodies position and orientation in FixedUpdate.
        Move ();
        Turn ();
        m_Rigidbody.velocity = Vector3.zero;
        m_Rigidbody.angularVelocity = Vector3.zero;
    }
 
    private void Move ()
    {
        // Create a vector in the direction the tank is facing with a magnitude based on
        // the input, speed and the time between frames.
        Vector3 movement = transform.forward * _mMovementInputValue * mSpeed * Time.deltaTime;
 
        // Apply this movement to the rigidbody's position.
        m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
    }
 
    private void Turn ()
    {
        // Determine the number of degrees to be turned based on the input,
        // speed and time between frames.
        float turn = _mTurnInputValue * mTurnSpeed * Time.deltaTime;
 
        // Make this into a rotation in the y axis.
        Quaternion turnRotation = Quaternion.Euler (0f, turn, 0f);
 
        // Apply this rotation to the rigidbody's rotation.
        m_Rigidbody.MoveRotation (m_Rigidbody.rotation * turnRotation);
    }

    // Update is called once per frame
    void Update()
    {
        _mMovementInputValue = Input.GetAxis ("Vertical");
        _mTurnInputValue = Input.GetAxis ("Horizontal");
    }
}
