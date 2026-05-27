using Restaurant.Domain.Entities.Base;
using Restaurant.Domain.Enums;
using Restaurant.Domain.Exceptions;

namespace Restaurant.Domain.Entities;

//бронь столика
public class Reservation : Entity<Guid>
{
    public Client Client { get; private set; } = default!;
    public Table Table { get; private set; } = default!;
    public Guid? ConfirmedBy { get; private set; }
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }
    public int GuestsCount { get; private set; }
    public ReservationStatus Status { get; private set; }

    protected Reservation()
    {
    }

    public Reservation(Client client, Table table, DateTime startTime, DateTime endTime, int guestsCount)
        : this(Guid.NewGuid(), client, table, startTime, endTime, guestsCount)
    {
    }

    protected Reservation(Guid id, Client client, Table table, DateTime startTime, DateTime endTime, int guestsCount)
        : base(id)
    {
        if (client is null)
            throw new ArgumentNullValueException(nameof(client));

        if (table is null)
            throw new ArgumentNullValueException(nameof(table));

        if (guestsCount <= 0)
            throw new InvalidGuestsCountException(guestsCount);

        if (startTime <= DateTime.UtcNow)
            throw new InvalidReservationTimeException(startTime);

        if (endTime <= startTime)
            throw new InvalidReservationTimeException(endTime);

        Client = client;
        Table = table;
        StartTime = startTime;
        EndTime = endTime;
        GuestsCount = guestsCount;
        Status = ReservationStatus.Pending;
    }

    //подтвердить бронь(только администратор)
    public void Confirm(Guid adminId)
    {
        if (Status != ReservationStatus.Pending)
            throw new ReservationNotPendingException(Id);

        ConfirmedBy = adminId;
        Status = ReservationStatus.Confirmed;
    }

    //отменить бронь(клиент или администратор)
    public void Cancel()
    {
        if (Status == ReservationStatus.Cancelled)
            throw new ReservationAlreadyCancelledException(Id);

        if (Status == ReservationStatus.Expired)
            throw new InvalidReservationStatusException(Id, "нельзя отменить истёкшую бронь.");

        Status = ReservationStatus.Cancelled;
    }

    //истёкшая, клиент не пришёл в течение 15 минут
    public void Expire()
    {
        if (Status != ReservationStatus.Confirmed)
            throw new InvalidReservationStatusException(Id, "бронь нельзя пометить как истёкшую: она не подтверждена.");

        Status = ReservationStatus.Expired;
    }

    //перенести бронь на другое время или другой столик
    //для повторного подтверждения
    public void Transfer(Table newTable, DateTime newStartTime, DateTime newEndTime, int newGuestsCount)
    {
        if (Status != ReservationStatus.Pending && Status != ReservationStatus.Confirmed)
            throw new InvalidReservationStatusException(Id, $"нельзя перенести: статус '{Status}'.");

        if (newTable is null)
            throw new ArgumentNullValueException(nameof(newTable));

        if (newStartTime <= DateTime.UtcNow)
            throw new InvalidReservationTimeException(newStartTime);

        if (newEndTime <= newStartTime)
            throw new InvalidReservationTimeException(newEndTime);

        if (newGuestsCount <= 0)
            throw new InvalidGuestsCountException(newGuestsCount);

        Table = newTable;
        StartTime = newStartTime;
        EndTime = newEndTime;
        GuestsCount = newGuestsCount;
        ConfirmedBy = null;
        Status = ReservationStatus.Pending;
    }
}
