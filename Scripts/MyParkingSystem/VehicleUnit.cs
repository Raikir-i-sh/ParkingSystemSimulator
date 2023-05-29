using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class VehicleUnit : MonoBehaviour
{
	public ParkingType m_Parkingtype;
	public string vehicleRegNumber = "forgot";
	public Ticket m_Ticket{get; set;}
	public bool isParking{ get; set;}
	public bool isSensorControlled{get; set;}
	
	void Start(){
		vehicleRegNumber += Random.Range(0,100);
	}
	
	void OnMouseDown(){
		isParking = false;
		SimulationManager.Instance.currentUsingCar = this.gameObject;
	}
	
}
