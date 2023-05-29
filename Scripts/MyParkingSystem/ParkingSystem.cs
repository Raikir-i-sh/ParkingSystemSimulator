using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum ParkingType {
	HEAVY, // Trucks, Buses
    CAR, //4 wheeler
    BIKE // 2 wheeler
}
public enum TransactionOptions{
	CASH,
	CARD, 
	ESEWA,
	PREMIUM
}
public struct Fare {
    public static readonly double BIKE_RATE_PER_HOUR = 20; // In Rs.
    public static readonly double CAR_RATE_PER_HOUR = 30;
	public static readonly double BUS_RATE_PER_HOUR = 40;
}

//Singleton class
public class ParkingSystem : Singleton<ParkingSystem>
{

}
