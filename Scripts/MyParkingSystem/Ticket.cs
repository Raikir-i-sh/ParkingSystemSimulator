using System;


public class Ticket {
	public int id{get; set;}
	public ParkingSpot m_ParkingSpot{get; set;}
	public string vehicleRegNumber{get; set;}
	
	public DateTime inTime{get; set;}
	public DateTime outTime{get; set;}
	public double ParkingDuration{get; set;}
	
	public ParkingType Parktype{get; set;}
	public double Price{get; set;}
	public TransactionOptions PayingMethod{get; set;}
	public readonly int tax = 13;
	public bool IsPaid{ get; set; }
	
		
        public double getTotalAmountToBePaid()
        {
			ParkingDuration = getStayinTime();	
            // 1 ghanta ko Rs.25 generally
			// 1 minute = 1 hour in-game time
			switch (Parktype){
            case ParkingType.CAR: {
                Price = ParkingDuration * Fare.CAR_RATE_PER_HOUR;
				break;
            }
            case ParkingType.BIKE: {
                Price = ParkingDuration * Fare.BIKE_RATE_PER_HOUR;
				
                break;
            }
			 case ParkingType.HEAVY: {
                Price = ParkingDuration * Fare.BUS_RATE_PER_HOUR;
				
                break;
            }
            default: Price = ParkingDuration * 25; break;
			}
			return Price + tax/100 * Price;
           
        }
		public double getStayinTime()
        {
			if( (inTime == null) ||  DateTime.Compare(outTime,inTime) <= 0  ){
			 throw new ArgumentException("Invalid date or OutTime less than InTime");
		  }
		  // It should be in hours but can't wait so long in a simulation , can we?
			ParkingDuration = outTime.Subtract(inTime).TotalMinutes;
            return ParkingDuration;
        }
		
	}
