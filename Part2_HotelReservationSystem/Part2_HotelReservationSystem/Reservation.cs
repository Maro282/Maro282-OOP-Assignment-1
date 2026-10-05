

namespace Part2_HotelReservationSystem;

internal class Reservation
{

    public int Id { get; }
    public DateTime CheckinDate { get; }
    public DateTime CheckoutDate { get; }
    public Status ReservationStatus { get; private set; }
    public int NumberOfNights => (CheckoutDate - CheckinDate).Days;

    public Room Room { get; }
    public decimal TotalCost => Room.NightRate * NumberOfNights;




    public Reservation(int id, DateTime checkinDate, DateTime checkoutDate, Room room)
    {
        if (room is null)
        {
            throw new ArgumentNullException();
        }

        if (id <= 0)
        {
            throw new ArgumentOutOfRangeException("Id shouldn't be negative or zero");
        }

        if (checkoutDate <= checkinDate)
        {
            throw new ArgumentException("Checkout date must be greater than checkin date");
        }


        if (room.UnderMaintenance)
        {
            throw new InvalidOperationException("Can't book under maintenance room");
        }

        if (!room.IsAvailable(checkinDate, checkoutDate))
        {
            throw new Exception("Room booked at that date range");
        }

        Id = id;
        CheckinDate = checkinDate;
        CheckoutDate = checkoutDate;
        ReservationStatus = Status.Pending;

    }


    public void Confirm()
    {
        if (ReservationStatus != Status.Pending)
        {
            throw new InvalidOperationException("Reservation must be Pending to make if confirmed");

        }

        ReservationStatus = Status.Confirmed;
        Console.WriteLine("Reservation Confirmed successfully");
    }
    public void CheckIn()
    {
        if (ReservationStatus != Status.Confirmed)
        {
            throw new InvalidOperationException("Reservation must be Confirmed to make it Checkedin");

        }

        ReservationStatus = Status.CheckedIn;
        Console.WriteLine("Reservation now checkedin successfully");
    }
    public void CheckOut()
    {
        if (ReservationStatus != Status.CheckedIn)
        {
            throw new InvalidOperationException("Reservation must be CheckedIN to make it CheckedOUt");

        }

        ReservationStatus = Status.CheckedOut;
        Console.WriteLine("Reservation now checkedout successfully");
    }
    public void Cancel()
    {
        if (ReservationStatus != Status.Pending && ReservationStatus != Status.Confirmed)
        {
            throw new InvalidOperationException("Reservation Can't be cancelled");

        }

        ReservationStatus = Status.Cancelled;
        Console.WriteLine("Reservation cance;;ed successfully");
    }



}

internal enum Status
{
    Pending,
    Confirmed,
    CheckedIn,
    CheckedOut,
    Cancelled
}
