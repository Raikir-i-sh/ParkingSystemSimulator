using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Singleton class
public class SimulationManager : Singleton<SimulationManager>
{
	
	[Header("Simulation" )]
	public Button parkBtn;
	public Button destroyCarBtn;
	public Button createCarBtn;
	[Space]
	public GameObject currentUsingCar;
    public List<GameObject> CarPrefabs;
	public Transform CarSpawnPos;
	[Header("NodeMcu specific")]
	public Simulation simul1;
	public Simulation simul2;
	public Transform entryPos;
	public Transform exitPos;
	public Transform parkPos;
	

	void Start(){
		//currentUsingCar = null;
		
	}
	void Update(){
		
	}
	public void SCarAtEntry() {
		if(simul1.car == null) {
			simul1.car = Instantiate(CarPrefabs[UnityEngine.Random.Range(0,CarPrefabs.Count)] ,
								  entryPos.position,Quaternion.identity );
			simul1.car.GetComponent<VehicleUnit>().isSensorControlled=true;			  
								  
		}
		 else if(simul1.car!=null && simul1.posState != SimulPos.ENTRY){
			 if(simul2.car == null)
			 {
				 simul2.car = Instantiate(CarPrefabs[UnityEngine.Random.Range(0,CarPrefabs.Count)] ,
								  entryPos.position,Quaternion.identity );
			simul2.car.GetComponent<VehicleUnit>().isSensorControlled=true;			  
			 }
			 else{
				  simul2.car.transform.position = entryPos.position;			 
		simul2.car.transform.rotation = entryPos.rotation;
			 }
			 simul2.posState = SimulPos.ENTRY;
			 return;
		 }
		 else {
		 simul1.car.transform.position = entryPos.position;			 
		simul1.car.transform.rotation = entryPos.rotation;
		 }
		 simul1.posState = SimulPos.ENTRY;
	}
	
	public void SCarAtPark(){
		if( (simul1.car != null && simul1.posState == SimulPos.PARK) || 
		(simul2.car != null && simul2.posState == SimulPos.PARK)		)
		{
			 UImanager.Instance.ShowDialogue("Another car present");
			 return;
		}
		if(simul1.car != null && simul1.posState == SimulPos.ENTRY){
			simul1.car.transform.position = parkPos.position;			 
			simul1.car.transform.rotation = parkPos.rotation;
			 simul1.posState = SimulPos.PARK;
			return;
		}
		if(simul2.car != null && simul2.posState == SimulPos.ENTRY){
			simul2.car.transform.position = parkPos.position;			 
			simul2.car.transform.rotation = parkPos.rotation;
			 simul2.posState = SimulPos.PARK;
			return;
		}	
		
		 }
		
	public void SCarAtExit() {
		
		if(simul1.car != null && simul1.posState == SimulPos.PARK){
			simul1.car.transform.position = exitPos.position;			 
			simul1.car.transform.rotation = exitPos.rotation;
			simul1.posState = SimulPos.EXIT;
			return;
		}
		if(simul2.car != null && simul2.posState == SimulPos.PARK) {
			simul2.car.transform.position = exitPos.position;			 
			simul2.car.transform.rotation = exitPos.rotation;
			simul2.posState = SimulPos.EXIT;
			return;
		}
		
	}
public void SCarDestroy() {
	    if(simul1.car != null && simul1.posState == SimulPos.EXIT) {
			Destroy(simul1.car);
			FindObjectOfType<TicketDAO>().DeleteTicket(simul1.car.GetComponent<VehicleUnit>().vehicleRegNumber);
			return;
		}
		if(simul2.car != null && simul2.posState == SimulPos.EXIT) {
			Destroy(simul2.car);
			FindObjectOfType<TicketDAO>().DeleteTicket(simul2.car.GetComponent<VehicleUnit>().vehicleRegNumber);
			return;
		}
}	
	public void ParkBtn(){
		// Stop controlling current car
		currentUsingCar.GetComponent<VehicleUnit>().isParking = true;
		createCarBtn.enabled = true;
		
	}
	public void CreateCarBtn(){
		if(CarPrefabs==null) return;
		currentUsingCar = Instantiate(CarPrefabs[UnityEngine.Random.Range(0,CarPrefabs.Count)] , CarSpawnPos.position,Quaternion.identity );
		createCarBtn.enabled = false;
	}
	public void DestroyCarBtn(){
		if( currentUsingCar == null)
		{		
		print("no car to destroy");
		return;
		}
		Destroy(currentUsingCar);
		FindObjectOfType<TicketDAO>().DeleteTicket(currentUsingCar.GetComponent<VehicleUnit>().vehicleRegNumber);
		createCarBtn.enabled = true;
	}
	
}
[Serializable]
public class Simulation{
	public GameObject car;
	public SimulPos posState = SimulPos.ENTRY;
}
public enum SimulPos{ENTRY ,EXIT , PARK }
