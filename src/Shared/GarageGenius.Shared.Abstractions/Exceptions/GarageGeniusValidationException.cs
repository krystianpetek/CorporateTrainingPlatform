using FluentValidation.Results;

namespace GarageGenius.Shared.Abstractions.Exceptions;
public class GarageGeniusValidationException : Exception
{
	public IEnumerable<string> Errors { get; }

	public GarageGeniusValidationException() : base("One or more validation failures have occurred.")
	{
		Errors = new List<string>();
	}

	public GarageGeniusValidationException(IEnumerable<ValidationFailure> errors) : this()
	{
		Errors = errors.Select(e => e.ErrorMessage).ToList();
	}
}
