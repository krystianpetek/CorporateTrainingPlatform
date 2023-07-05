using FluentValidation.Results;

namespace GarageGenius.Shared.Abstractions.Exceptions;
public class GarageGeniusValidationException : Exception
{
	public GarageGeniusValidationException(IEnumerable<ValidationFailure> errors)
	{
		Errors = errors;
	}

	public IEnumerable<ValidationFailure> Errors { get; }
}
