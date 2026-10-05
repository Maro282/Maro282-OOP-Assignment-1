

namespace Part2_HotelReservationSystem;

internal class Room
{

    public int RoomNumber { get; }
    public RoomType Type { get; }

    public bool UnderMaintenance { get; private set; }

    public decimal NightRate { get; private set; }

    private List<Reservation> _roomReservations = new List<Reservation>();

    public Room(int roomNumber, RoomType type, decimal nightRate)
    {

        if (roomNumber <= 0)
        {
            throw new ArgumentOutOfRangeException("Enter valid room number");
        }

        if (!Enum.IsDefined(typeof(RoomType), type))
        {
            throw new ArgumentOutOfRangeException("Room type not valid");
        }

        if (nightRate <= 0)
        {
            throw new ArgumentOutOfRangeException("Night rate not valid");
        }


        RoomNumber = roomNumber;
        Type = type;
        NightRate = nightRate;
    }


    public void SetNightlyRate(decimal rate)
    {

        if (rate <= 0)
        {
            throw new ArgumentOutOfRangeException("Rate cannot be 0 or negative");
        }

        NightRate = rate;

    }


    public void StartMaintenance() => UnderMaintenance = true;
    public void EndMaintenance() => UnderMaintenance = false;


    public bool IsAvailable(DateTime checkedin, DateTime checkedout) => !_roomReservations.Any(r => r.ReservationStatus != Status.Cancelled
                                                                           && r.ReservationStatus != Status.CheckedOut
                                                                           && checkedin < r.CheckoutDate
                                                                           && r.CheckinDate < checkedout);




    public void AddReservation(Reservation r) => _roomReservations.Add(r);

}

public enum RoomType
{
    Single,
    Double,
    Suite
}
