namespace FH.Domain.Exceptions
{
	public class FhException : Exception
	{
		public FhException(string message) 
			: base(message)
		{
		}
		
		public FhException(string message, Exception exception) 
			: base(message, exception)
		{
		}
	}
}