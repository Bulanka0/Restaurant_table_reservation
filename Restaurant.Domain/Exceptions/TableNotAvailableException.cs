namespace Restaurant.Domain.Exceptions;

public class TableNotAvailableException(int tableNumber)
    : DomainException($"Столик №{tableNumber} недоступен для бронирования.");
