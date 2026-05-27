namespace Restaurant.Domain.Exceptions;

public class RestaurantClosedException(DateTime reservationTime)
    : DomainException($"Ресторан не работает {reservationTime:dd.MM.yyyy} ({reservationTime.DayOfWeek}).");
