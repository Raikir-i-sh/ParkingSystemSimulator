using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkingSpot : MonoBehaviour{
    public int number = 0;
    public ParkingType m_ParkingType{get; set;}
    public bool IsAvailable{get; set;}
	public List<FakeSensor> listOfSensors;
	private int sensorCount;
	void Start(){
		sensorCount = listOfSensors.Count;
		m_ParkingType = ParkingType.CAR;
		IsAvailable = true;
		StartCoroutine(MyUpdate());
	}
    IEnumerator MyUpdate(){
		while(true){
		if ( sensorCount == 0) break;
		CheckSensorState();
		yield return new WaitForSeconds(1f);
		}
	}
	void CheckSensorState(){
		for(int i=0;i < sensorCount ; i++){
			if(listOfSensors[i].IsCarDetected){
				continue;
			}
			//if even one sensor don't detect car, user can't park
			IsAvailable = true;
			return;
		}
		print("Car can park now !!");
		
		IsAvailable = false;
		
	}
    public override bool Equals(object o) {
        if (this == o) return true;
        if (o == null || !(o is ParkingSpot)) return false;
        
        return number == ((ParkingSpot) o).number;
    }
	 public override int GetHashCode()
   {
      return number;
   }
   
}
