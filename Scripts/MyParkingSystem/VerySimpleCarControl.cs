using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerySimpleCarControl : MonoBehaviour
{
  
  public float turnSpeed = 20;
   public float moveSpeed = 2;
    public VehicleUnit vehicle;
	public Vector3 forwardaxis;
	Rigidbody m_AgentRb;
	
	void Start(){
		 m_AgentRb = GetComponent<Rigidbody>();
		 
	}
	void Update(){
		if(vehicle.isParking) return;
		Move();
	}
	void Move(){
		 var dirToGo = Vector3.zero;
        var rotateDir = Vector3.zero;

		 if (Input.GetKey(KeyCode.D))
        {
           rotateDir = transform.up;
        }
        if (Input.GetKey(KeyCode.W))
        {
            dirToGo = transform.right;
        }
        if (Input.GetKey(KeyCode.A))
        {
			rotateDir = -transform.up;
        }
        if (Input.GetKey(KeyCode.S))
        {
          dirToGo = -transform.right;
        }
		    dirToGo *= 0.5f;
                m_AgentRb.velocity *= 0.75f;
            
            m_AgentRb.AddForce(dirToGo * moveSpeed, ForceMode.VelocityChange);
            transform.Rotate(rotateDir, Time.fixedDeltaTime * turnSpeed);
        
		  if (m_AgentRb.velocity.sqrMagnitude > 25f) // slow it down
        {
            m_AgentRb.velocity *= 0.95f;
        }
	}
	
}
