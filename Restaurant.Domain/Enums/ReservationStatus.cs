namespace Restaurant.Domain.Enums;

public enum ReservationStatus
{
    //ожидает подтверждения администратором
    Pending,

    //подтверждена администратором
    Confirmed,

    //отменена клиентом или администратором
    Cancelled,

    //истекла — клиент не пришёл в течение 15 минут
    Expired
}
