using Microsoft.Extensions.Logging;


namespace Fintrellis.Common.Extensions;

public static class ExceptionExtension
{
	public static void LogError(this Exception exception, ILogger logger,
		string message)
	{
        logger.LogError(exception, message);
    }
}

