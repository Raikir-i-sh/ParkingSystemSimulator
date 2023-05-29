using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketDAO : MonoBehaviour
{
	List<Ticket> alltickets;
        //Save ticket data to DB
	void Start(){
		alltickets = new List<Ticket>();
	}	
     public void saveTicket(Ticket ticket){
		 alltickets.Add(ticket);
    }
	public int GetNumberOfTickets(){
		return alltickets.Count;
	}
       //Get ticket using vehicle no.
	 public Ticket getTicket(string vehicleRegNumber) {
            for(int i = 0; i < alltickets.Count; i++){
				if(String.Compare(alltickets[i].vehicleRegNumber , vehicleRegNumber) == 0 )
					return alltickets[i];
			}
			//this means this car went inside parking lot from other shortcut
			return null;
	 }
	 public bool DeleteTicket(string vehicleRegNumber){
		   for(int i = 0; i < alltickets.Count; i++){
				if(String.Compare(alltickets[i].vehicleRegNumber , vehicleRegNumber) == 0 )
					return alltickets.Remove(alltickets[i]);
			}
			//this means this car went inside parking lot from other shortcut
			return false;
	 }
	 // Update the ticket in DB with Exiting time , Price ,etc.
	  public void updateTicket(Ticket ticket) {
     
    }
}
