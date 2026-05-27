namespace Restaurant.Domain.Exceptions;

public class ArgumentNullValueException(string paramName)
    : DomainException($"Параметр '{paramName}' не может быть null.");
