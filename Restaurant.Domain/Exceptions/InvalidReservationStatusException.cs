namespace Restaurant.Domain.Exceptions;

public class InvalidReservationStatusException(Guid reservationId, string message)
    : DomainException($"Бронь '{reservationId}': {message}");
