namespace Restaurant.Domain.Exceptions;

public class ReservationNotPendingException(Guid reservationId)
    : DomainException($"Бронь '{reservationId}' не может быть изменена: она не находится в статусе ожидания.");
