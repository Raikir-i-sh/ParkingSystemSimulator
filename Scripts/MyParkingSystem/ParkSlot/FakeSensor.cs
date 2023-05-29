using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeSensor : MonoBehaviour
{
	
	public event EventHandler<VehicleUnit> OnSensorTriggered;
	public bool IsCarDetected{get; set;}
     private void OnCollisionEnter(Collision collision)
    { 
    }
	   private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")) {
			print("Car detected");
			IsCarDetected = true;	
			OnSensorTriggered?.Invoke(this , other.GetComponent<VehicleUnit>());
		}	
    }
	
	  private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player")) {
			IsCarDetected = false;
		}	
    }

}
