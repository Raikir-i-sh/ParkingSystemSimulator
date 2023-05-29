using System;
using System.Collections;
using Uduino;
using UnityEngine;

public class MyUduinoManager : Singleton<MyUduinoManager>
    {
    
   // Uduino.UduinoManager u; // The instance of Uduino is initialized here
  //All these data are used in SimulationManager , nowhere else.
    public bool IsConnected { get;  set; }
    public float EntryDistance { get; set; }
	public float ExitDistance {get; set;}
	public float ParkDistance {get; set;}
	public int Gate{get; set;}
	public event Action OnGateEntry;
	public event Action OnGateExit;
    void Awake()
    {
		Gate = 0;
		EntryDistance = 50; // this means the car is very far
		ExitDistance = 50;
		ParkDistance = 50;
        UduinoManager.Instance.OnBoardConnected += OnBoardConnected; //Create the Delegate
        UduinoManager.Instance.OnDataReceived += OnDataReceived;    
    }
	
    private void OnDataReceived(string data, UduinoDevice device)
    {
        char[] delimiterChars = { ' ', ',', '.', ':', '\t' };
        string []words = data.Split(delimiterChars, System.StringSplitOptions.RemoveEmptyEntries);
        //Getting distance of car from gate
        for(int i=0;i < words.Length; i=i+1)
        {
			switch(words[i]) 
{
  case "entrypos":
		SimulationManager.Instance.SCarAtEntry();
    break;
  case "exitpos":
		SimulationManager.Instance.SCarAtExit();
    break;
  case "parkpos": 
		SimulationManager.Instance.SCarAtPark();
	break;
	case "gateentry": OnGateEntry?.Invoke();	
	break;	
	case "gateexit": OnGateExit?.Invoke();	
	break;  
	case "cardestroy" : SimulationManager.Instance.SCarDestroy();
	break;
  default:
    break;
} 
		}	
    }

    void Start()
    {
       // UduinoManager.Instance.pinMode(blinkPin, PinMode.Output);
     
        //To send data to uduino, command name must be same.
        //UduinoManager.Instance.sendCommand("myNodemcu", "myCommand", 10, 3);
    }
    public void OnBoardConnected(UduinoDevice deviceName)
    {   
         IsConnected = true;  
    }
    public void BoardDisconnected(UduinoDevice device)
    {
        print("Event: Board " + device.name + " disconnected.");
        IsConnected = false;
    }
 
}
