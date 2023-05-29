using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Singleton class
public class UImanager : Singleton<UImanager>
{
	public GameObject adminPgPanel;
	public GameObject mainmenuPanel; // shows History , Quit
	
	public GameObject receiptPanel; //Ticket panel
	[Space(10)]
	public Text parkingBillboardTxt;
	[Space(10)]
	public GameObject dialoguePanel;
	public Text dialogueText;
	[Space(10)]
	public Text carInsideTxt;
	public Text occupiedSlotTxt;
	[Space(10)]
	[Header("Admin Page",order =1 )]
	public Text adminName;
	public Text password;
	
	//[Header("Main Menu")]
	[Header("Ticket")]
	public Text tokenNum;
	public Text parkingType;
	public Text vehicleRegNum;
	public Text inTime;
	public Text outTime;
	public Text duration;
	// payin method should be changable dropdown menu
	public Dropdown payingMethod;
	public Text price;
	public Text tax;
	public Ticket CurrentProcessingticket{get; set;}
	
	
	void Start(){
		receiptPanel.SetActive(false);
	}
	public void UpdateParkingBillboard(int freeslotleft){
		parkingBillboardTxt.text = freeslotleft.ToString();
	}
	public void UpdateSmallInfo(string s1, string s2){
		carInsideTxt.text = s1;
		occupiedSlotTxt.text = s2;
	}
	void Update(){
		if(Input.GetKeyDown(KeyCode.Escape)){
			mainmenuPanel.SetActive(false);
			
			
		}
		
	}
	Coroutine dialogue;
	public void ShowDialogue(string msg){
		dialogueText.text = msg;
		dialoguePanel.SetActive(true);
		
		if(dialogue != null) StopCoroutine(dialogue);
		dialogue = StartCoroutine( PrintDialogue() );
		
	}
	IEnumerator PrintDialogue(){
		yield return new WaitForSeconds(5f);
		dialoguePanel.SetActive(false);
	}
	bool CheckAdminValidity(){
		if(String.Equals("userAdmin",adminName.text) &&
		String.Equals("password",password.text) ){
			
		return true;
		}
		else return false;
	}
	#region Main Menu Buttons
	public void HistoryBtn(){
		mainmenuPanel.SetActive(false);
		//TODO:  see how to fill similar template fields
		
	}
	public void QuitBtn(){
		print("App Quit");
	}
	#endregion
	public void ShowTicket(Ticket t){
		tokenNum.text = t.id.ToString();
		parkingType.text = t.Parktype.ToString();
		vehicleRegNum.text = t.vehicleRegNumber;
		inTime.text = t.inTime.ToString();
		outTime.text = t.outTime.ToString();
		duration.text = t.ParkingDuration.ToString();
		//payingMethod.text = t.PayingMethod.ToString();
		price.text = ((int)t.Price).ToString();
		tax.text = t.tax.ToString();
		CurrentProcessingticket = t;
		receiptPanel.SetActive(true);
	}
	public void ShowHistory(){
		
	}
	// called when "PAY' pressed
	public void PayBtn(){
		
		receiptPanel.SetActive(false);
		ShowDialogue("Thank you for coming");
	}
	
}
