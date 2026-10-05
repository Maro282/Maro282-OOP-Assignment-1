using System.Reflection;

namespace Part2_HotelReservationSystem;

internal class Program
{
    static int _passed, _failed, _nextId = 1;

    // ---------- tiny test helpers ----------
    static int NextId() => _nextId++;
    static DateTime D(int day) => new DateTime(2026, 1, day);
    static Room NewRoom(int number = 101, decimal rate = 100m) => new Room(number, RoomType.Double, rate);
    static Guest NewGuest(int id = 1) => new Guest(id, "Ahmed Ali", "01000000000");
    static Reservation Book(Guest g, Room r, int inDay, int outDay) =>
        g.MakeReservation(NextId(), D(inDay), D(outDay), r);

    static void Check(string name, bool condition)
    {
        if (condition) { _passed++; Console.WriteLine($"  PASS  {name}"); }
        else { _failed++; Console.WriteLine($"  FAIL  {name}"); }
    }

    static void Throws<T>(string name, Action action) where T : Exception
    {
        try { action(); Check(name + " (expected exception, none thrown)", false); }
        catch (Exception ex) when (ex is T) { Check(name, true); }
        catch (Exception ex) { Check($"{name} (wrong exception: {ex.GetType().Name})", false); }
    }

    static void DoesNotThrow(string name, Action action)
    {
        try { action(); Check(name, true); }
        catch (Exception ex) { Check($"{name} (unexpected {ex.GetType().Name}: {ex.Message})", false); }
    }

    static bool HasPublicSetter(Type type, string property)
    {
        var p = type.GetProperty(property)
                ?? throw new InvalidOperationException($"{type.Name}.{property} not found - fix the name in the test");
        return p.SetMethod?.IsPublic == true;
    }

    static void Section(string title) => Console.WriteLine($"\n=== {title} ===");

    // ---------- tests ----------
    static void Main()
    {
        GuestValidation();
        RoomValidation();
        ReservationValidation();
        StatusTransitions();
        TotalCost();
        MaintenanceRules();
        DoubleBooking();
        GuestHistoryProtection();
        EncapsulationChecks();

        Console.WriteLine($"\nResult: {_passed} passed, {_failed} failed");
    }

    static void GuestValidation()
    {
        Section("Guest validation");
        DoesNotThrow("valid guest is created", () => NewGuest());
        Throws<ArgumentOutOfRangeException>("id = 0 rejected", () => new Guest(0, "A", "1"));
        Throws<ArgumentOutOfRangeException>("negative id rejected", () => new Guest(-5, "A", "1"));
        Throws<ArgumentException>("null name rejected", () => new Guest(1, null!, "1"));
        Throws<ArgumentException>("empty name rejected", () => new Guest(1, "", "1"));
        Throws<ArgumentException>("whitespace name rejected", () => new Guest(1, "   ", "1"));
        Throws<ArgumentException>("null phone rejected", () => new Guest(1, "A", null!));
        Throws<ArgumentException>("empty phone rejected", () => new Guest(1, "A", ""));
        Throws<ArgumentException>("whitespace phone rejected", () => new Guest(1, "A", "  "));
    }

    static void RoomValidation()
    {
        Section("Room validation");
        DoesNotThrow("valid room is created", () => NewRoom());
        Throws<ArgumentOutOfRangeException>("room number 0 rejected", () => new Room(0, RoomType.Single, 100m));
        Throws<ArgumentOutOfRangeException>("negative room number rejected", () => new Room(-1, RoomType.Single, 100m));
        Throws<ArgumentOutOfRangeException>("undefined room type rejected", () => new Room(1, (RoomType)99, 100m));
        Throws<ArgumentOutOfRangeException>("rate 0 at creation rejected", () => new Room(1, RoomType.Single, 0m));
        Throws<ArgumentOutOfRangeException>("negative rate at creation rejected", () => new Room(1, RoomType.Single, -10m));

        var room = NewRoom();
        Throws<ArgumentOutOfRangeException>("SetNightlyRate(0) rejected", () => room.SetNightlyRate(0m));
        Throws<ArgumentOutOfRangeException>("SetNightlyRate(negative) rejected", () => room.SetNightlyRate(-1m));
        Check("failed pricing attempt leaves old rate", room.NightRate == 100m);
        room.SetNightlyRate(150m);
        Check("valid pricing change applied", room.NightRate == 150m);
    }

    static void ReservationValidation()
    {
        Section("Reservation validation");
        var g = NewGuest();
        var room = NewRoom();

        Throws<ArgumentException>("checkout == checkin rejected", () => Book(g, room, 5, 5));
        Throws<ArgumentException>("checkout before checkin rejected", () => Book(g, room, 8, 5));
        Throws<ArgumentOutOfRangeException>("reservation id 0 rejected",
            () => g.MakeReservation(0, D(1), D(3), room));
        Throws<ArgumentNullException>("null room rejected", () => g.MakeReservation(NextId(), D(1), D(3), null!));

        Check("failed attempts did not touch guest history", g.Reservations.Count == 0);
        Check("failed attempts did not block the room", room.IsAvailable(D(1), D(10)));
    }

    static void StatusTransitions()
    {
        Section("Status transitions");
        Reservation Fresh() => Book(NewGuest(), NewRoom(), 1, 4);

        var r = Fresh();
        Check("new reservation starts Pending", r.ReservationStatus == Status.Pending);
        Throws<InvalidOperationException>("Pending -> CheckedIn rejected", () => r.CheckIn());
        Throws<InvalidOperationException>("Pending -> CheckedOut rejected", () => r.CheckOut());

        r.Confirm();
        Check("Pending -> Confirmed works", r.ReservationStatus == Status.Confirmed);
        Throws<InvalidOperationException>("Confirmed -> Confirmed rejected", () => r.Confirm());
        Throws<InvalidOperationException>("Confirmed -> CheckedOut rejected", () => r.CheckOut());

        r.CheckIn();
        Check("Confirmed -> CheckedIn works", r.ReservationStatus == Status.CheckedIn);
        Throws<InvalidOperationException>("CheckedIn -> Cancelled rejected", () => r.Cancel());
        Throws<InvalidOperationException>("CheckedIn -> Confirmed rejected", () => r.Confirm());

        r.CheckOut();
        Check("CheckedIn -> CheckedOut works", r.ReservationStatus == Status.CheckedOut);
        Throws<InvalidOperationException>("CheckedOut -> CheckedIn rejected", () => r.CheckIn());
        Throws<InvalidOperationException>("CheckedOut -> Cancelled rejected", () => r.Cancel());
        Throws<InvalidOperationException>("CheckedOut -> Confirmed rejected", () => r.Confirm());

        var pending = Fresh();
        DoesNotThrow("Pending -> Cancelled works", () => pending.Cancel());
        Check("status is Cancelled", pending.ReservationStatus == Status.Cancelled);
        Throws<InvalidOperationException>("Cancelled -> Cancelled rejected", () => pending.Cancel());
        Throws<InvalidOperationException>("Cancelled -> Confirmed rejected", () => pending.Confirm());
        Throws<InvalidOperationException>("Cancelled -> CheckedIn rejected", () => pending.CheckIn());

        var confirmed = Fresh();
        confirmed.Confirm();
        DoesNotThrow("Confirmed -> Cancelled works", () => confirmed.Cancel());
    }

    static void TotalCost()
    {
        Section("Total cost");
        var room = NewRoom(rate: 100m);
        var r = Book(NewGuest(), room, 1, 4);   // 3 nights
        Check("nights = 3", r.NumberOfNights == 3);
        Check("total = 3 x 100 = 300", r.TotalCost == 300m);

        var r2 = Book(NewGuest(2), NewRoom(102, 49.99m), 10, 12);   // 2 nights, decimal rate
        Check("decimal rate: 2 x 49.99 = 99.98", r2.TotalCost == 99.98m);

        var oneNight = Book(NewGuest(3), NewRoom(103, 80m), 20, 21);
        Check("single night = rate", oneNight.TotalCost == 80m);
    }

    static void MaintenanceRules()
    {
        Section("Maintenance");
        var g = NewGuest();
        var room = NewRoom();

        room.StartMaintenance();
        Check("room is under maintenance", room.UnderMaintenance);
        Throws<InvalidOperationException>("cannot book room under maintenance", () => Book(g, room, 1, 3));
        Check("rejected booking not in guest history", g.Reservations.Count == 0);

        room.EndMaintenance();
        Check("maintenance ended", !room.UnderMaintenance);
        DoesNotThrow("can book after maintenance ends", () => Book(g, room, 1, 3));
    }

    static void DoubleBooking()
    {
        Section("Double booking");
        var room = NewRoom();
        var g1 = NewGuest(1);
        var g2 = NewGuest(2);

        var first = Book(g1, room, 10, 15);   // nights of 10..14

        Throws<InvalidOperationException>("exact same dates rejected", () => Book(g2, room, 10, 15));
        Throws<InvalidOperationException>("overlap at the start rejected", () => Book(g2, room, 8, 11));
        Throws<InvalidOperationException>("overlap at the end rejected", () => Book(g2, room, 14, 18));
        Throws<InvalidOperationException>("fully inside existing rejected", () => Book(g2, room, 11, 13));
        Throws<InvalidOperationException>("fully surrounding existing rejected", () => Book(g2, room, 8, 20));
        Check("rejected bookings not added to guest 2", g2.Reservations.Count == 0);

        DoesNotThrow("back-to-back (check-in on checkout day) allowed", () => Book(g2, room, 15, 17));
        DoesNotThrow("ends on existing check-in day allowed", () => Book(g2, room, 7, 10));
        DoesNotThrow("same dates in a different room allowed", () => Book(g2, NewRoom(202), 10, 15));

        first.Cancel();
        DoesNotThrow("cancelled reservation frees the dates", () => Book(g2, room, 10, 15));

        var other = NewRoom(303);
        var checkedOut = Book(g1, other, 1, 5);
        checkedOut.Confirm(); checkedOut.CheckIn(); checkedOut.CheckOut();
        DoesNotThrow("checked-out reservation frees the dates", () => Book(g2, other, 1, 5));
    }

    static void GuestHistoryProtection()
    {
        Section("Guest history protection");
        var g = NewGuest();
        var room = NewRoom();
        var r = Book(g, room, 1, 3);

        Check("history contains the new reservation", g.Reservations.Count == 1 && g.Reservations[0] == r);
        Check("history is exposed as IReadOnlyList", g.Reservations is IReadOnlyList<Reservation>);
        Check("cannot be cast back to List<Reservation>", !(g.Reservations is List<Reservation>));
        Check("cannot be cast to ICollection<Reservation> and modified",
            !(g.Reservations is ICollection<Reservation> c) || c.IsReadOnly);

        Book(g, NewRoom(2), 5, 7);
        Check("second booking appended (history grows)", g.Reservations.Count == 2);
    }

    static void EncapsulationChecks()
    {
        Section("Encapsulation (reflection)");
        foreach (var p in new[] { "Id", "Name", "Phone", "Reservations" })
            Check($"Guest.{p} has no public setter", !HasPublicSetter(typeof(Guest), p));

        foreach (var p in new[] { "Id", "CheckinDate", "CheckoutDate", "Room", "ReservationStatus" })
            Check($"Reservation.{p} has no public setter", !HasPublicSetter(typeof(Reservation), p));

        foreach (var p in new[] { "RoomNumber", "Type", "NightlyRate", "UnderMaintenance" })
            Check($"Room.{p} has no public setter", !HasPublicSetter(typeof(Room), p));

        // get-only (no setter at all) for the set-once properties
        Check("Reservation.Room is get-only", typeof(Reservation).GetProperty("Room")!.SetMethod == null);
        Check("Reservation.CheckinDate is get-only", typeof(Reservation).GetProperty("CheckinDate")!.SetMethod == null);
        Check("Guest.Id is get-only", typeof(Guest).GetProperty("Id")!.SetMethod == null);
    }
}