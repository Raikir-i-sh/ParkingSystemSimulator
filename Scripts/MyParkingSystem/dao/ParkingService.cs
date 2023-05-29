using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParkingService : Singleton<ParkingService>
{
    public ParkingSpotDAO parkingSpotDAO;
    public  TicketDAO ticketDAO;
	public FakeSensor EntrySensor, ExitSensor;
	public GameObject entryGate, exitGate;
	public Button payBtn;
	
	public Coroutine entryR, exitR;
    void Start() {
		
		EntrySensor.OnSensorTriggered += OnGateEntry;
		ExitSensor.OnSensorTriggered += OnGateExit;
		
		MyUduinoManager.Instance.OnGateEntry += OpenEntryGate;
		MyUduinoManager.Instance.OnGateExit += OpenExitGate;
		if(payBtn!=null)
		payBtn.onClick.AddListener(() => TakePaymentFromUser(UImanager.Instance.CurrentProcessingticket) );
		
		StartCoroutine(UpdateParkingBillboard(3f));
	}
	IEnumerator UpdateParkingBillboard(float frequency){
		while(true){
			print("no. of cars inside = " + ticketDAO.GetNumberOfTickets() + 
			"\n no. of occupied slots = "+
			(parkingSpotDAO.getTotalNumOfParkingSlot() - parkingSpotDAO.getAllAvailableSlot().Count ) );
		
		UImanager.Instance.UpdateSmallInfo(""+ticketDAO.GetNumberOfTickets(),
				""+(parkingSpotDAO.getTotalNumOfParkingSlot() - parkingSpotDAO.getAllAvailableSlot().Count ));
		UImanager.Instance.UpdateParkingBillboard( parkingSpotDAO.getTotalNumOfParkingSlot() - 
		ticketDAO.GetNumberOfTickets() ); 
		yield return new WaitForSeconds(frequency);
		}
	}
	
	void Update(){
	//	print(entryGate.transform.localEulerAngles.z);
	}
	void OnGateEntry(object sender, VehicleUnit v){
		if (parkingSpotDAO.getAllAvailableSlot().Count == 0 ) {
			print("All spaces filled");
			UImanager.Instance.ShowDialogue("Sorry, No parking slot left");
			return;
		}
		string vehicleRegNumber = v.vehicleRegNumber;
		Ticket ticket = new Ticket();
		ticket.id = UnityEngine.Random.Range(100 , 10000);
		ticket.m_ParkingSpot= null;
		ticket.vehicleRegNumber = vehicleRegNumber;
		ticket.Price = 0;
		ticket.IsPaid = false;
		ticket.inTime = DateTime.Now;
		ticket.outTime = new DateTime();
		ticket.Parktype = v.m_Parkingtype;
		ticket.PayingMethod = TransactionOptions.CASH;
		//Giving ticket to the driver
		v.m_Ticket = ticket;
		ticketDAO.saveTicket(ticket);
		print("Ticket generated & saved");
		UImanager.Instance.ShowDialogue("Namaste!!");
		print("sensored="+v.isSensorControlled);
		if(v.isSensorControlled) return;
		OpenEntryGate();
		
	}
	public void OpenEntryGate(){
		if(entryR!=null)StopCoroutine(entryR);
		entryR = StartCoroutine(Rotate(entryGate, -90f ));
	}
	public void OpenExitGate(){
		if(exitR!=null) StopCoroutine(exitR);
		exitR = StartCoroutine(Rotate(exitGate, -90f ));
	}
   private IEnumerator Rotate(GameObject gate , float angle)
    {
     float startRotation = gate.transform.localEulerAngles.z;
     float endRotation = angle;
     float t = 0.0f;
	 
	while ( t > -90 )
     {
		t += Time.deltaTime * -50;
		if(gate.transform.localEulerAngles.z > 270 && gate.transform.localEulerAngles.z <275) break;
	  gate.transform.Rotate(0,0, 
							Time.deltaTime * -50,
							Space.Self);
         yield return null;
     }	 
	 
	 t = 0f;
	 yield return new WaitForSeconds(5f);
	 while ( t < 90 )
     {
         t += Time.deltaTime * 50;
		 if(gate.transform.localEulerAngles.z < 5) break;
         gate.transform.Rotate(0,0, 
							Time.deltaTime * 50,
							Space.Self);
         yield return null;
     }	
	 yield return null;
	
    }

	
	void OnGateExit(object sender ,VehicleUnit v){
			Ticket ticket = ticketDAO.getTicket(v.vehicleRegNumber);
			if(ticket == null){
				UImanager.Instance.ShowDialogue("No Ticket Found,How'd you get in?");
				return;
			}
			ticket.outTime = DateTime.Now;
			ticket.Price = ticket.getTotalAmountToBePaid();
			StartCoroutine(GetPaymentFromUser(ticket,v) );
	}
	//Get Pay
	IEnumerator GetPaymentFromUser(Ticket t,VehicleUnit v){
		// Get Payment from UI . Don't return until user pays money.
		
		while (!t.IsPaid){
			//Start Ticket showing UI
			UImanager.Instance.ShowTicket(t);
			yield return new WaitForSeconds(3f);// check if money paid status every 3 sec
		}
		if(v.isSensorControlled) yield break; 
		OpenExitGate();
	}
	//Called when PayBtn pressed
	void TakePaymentFromUser(Ticket t){
	//	TransactionOptions payOption;
	//	Enum.TryParse(UImanager.Instance.payingMethod.value , out payOption); //String to Enum
		t.PayingMethod = (TransactionOptions)UImanager.Instance.payingMethod.value ;	
		t.IsPaid = true;
		ticketDAO.DeleteTicket(t.vehicleRegNumber);
	}
        
}
