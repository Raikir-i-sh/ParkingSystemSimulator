using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkingSpotDAO : MonoBehaviour
{
	ParkingSpot[] allParkSpots;
	List<ParkingSpot> freeParkSpots;
	void Start(){
		 allParkSpots = FindObjectsOfType<ParkingSpot>();
		 freeParkSpots = new List<ParkingSpot>();
	}
	// Gets available slot for this specific vehicle type
    public List<ParkingSpot> getAllAvailableSlot(ParkingType parkingType){
	  
	  freeParkSpots.Clear();
      foreach(var parkspot in allParkSpots){
		  if(parkspot.IsAvailable && parkspot.m_ParkingType==parkingType)
			   freeParkSpots.Add(parkspot);
	  }
	  return freeParkSpots;
    }
	// Gets all free slot , no matter which type
	public List<ParkingSpot> getAllAvailableSlot(){
	
     freeParkSpots.Clear();
	 foreach(var parkspot in allParkSpots){
		 if(parkspot.IsAvailable)
		 freeParkSpots.Add(parkspot);
	 }
	 return freeParkSpots;
    }
	public int getTotalNumOfParkingSlot(){
		return allParkSpots.Length;
	}
    public void updateParking(ParkingSpot parkingSpot){
      
    }
}
