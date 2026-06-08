namespace TripleG3.User32.Exceptions;

[Serializable]
public class HookException(string? message) : Exception(message) { }