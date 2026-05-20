namespace Restaurant.Domain.Exceptions;

public class ReservationAlreadyCancelledException(Guid reservationId)
    : DomainException($"Бронь '{reservationId}' уже отменена.");
