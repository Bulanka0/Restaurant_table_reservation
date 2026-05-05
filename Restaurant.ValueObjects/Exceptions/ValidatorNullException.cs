namespace Restaurant.ValueObjects.Exceptions;

public class ValidatorNullException(string paramName)
    : ArgumentNullException(paramName, "для типа должен быть указан валидатор");
