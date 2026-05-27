namespace Restaurant.Domain.Exceptions;

public class InvalidGuestsCountException(int guestsCount)
    : DomainException($"Количество гостей ({guestsCount}) должно быть больше нуля.");
