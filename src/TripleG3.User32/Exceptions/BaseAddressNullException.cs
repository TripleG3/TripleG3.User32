namespace TripleG3.User32.Exceptions;

[Serializable]
public class BaseAddressNullException(string? message, Exception? innerException) : Exception(message, innerException) { }
