namespace Restaurant.Domain.Exceptions;

public class TableSeatsExceededException(int guestsCount, int seats)
    : DomainException($"Количество гостей ({guestsCount}) превышает вместимость столика ({seats} мест).");
