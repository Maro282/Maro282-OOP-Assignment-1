

namespace Part2_HotelReservationSystem;

internal class Guest
{

    public int Id { get; }
    public string Name { get; }
    public string Phone { get; }
    private List<Reservation> _reservations = new List<Reservation>();
    public IReadOnlyList<Reservation> Reservations => _reservations.AsReadOnly();

    public Guest(int id, string name, string phone)
    {
        if (id <= 0)
        {
            throw new ArgumentOutOfRangeException("Id shouldn't be negative or zero");
        }

        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(phone))
        {
            throw new ArgumentNullException("Input cann't be empty or white spaces");
        }

        Id = id;
        Name = name;
        Phone = phone;

    }

    public Reservation MakeReservation(int id, DateTime checkinDate, DateTime checkoutDate, Room room)
    {
        Reservation reservation = new Reservation(id, checkinDate, checkoutDate, room);
        _reservations.Add(reservation);
        return reservation;
    }


}
