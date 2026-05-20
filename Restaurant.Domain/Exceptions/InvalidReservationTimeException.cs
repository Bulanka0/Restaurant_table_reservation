namespace Restaurant.Domain.Exceptions;

public class InvalidReservationTimeException(DateTime reservedAt)
    : DomainException($"Время бронирования '{reservedAt:dd.MM.yyyy HH:mm}' некорректно. Нельзя бронировать на прошедшее время.");
