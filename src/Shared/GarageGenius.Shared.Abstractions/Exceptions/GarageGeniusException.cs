using System.Net;

namespace GarageGenius.Shared.Abstractions.Exceptions;
public abstract class GarageGeniusException : Exception
{
	protected GarageGeniusException(string message) : base(message)
	{
	}

	protected GarageGeniusException(string message, HttpStatusCode httpStatusCode) : base(message)
	{
		StatusCode = httpStatusCode;
	}

	public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.InternalServerError;
}