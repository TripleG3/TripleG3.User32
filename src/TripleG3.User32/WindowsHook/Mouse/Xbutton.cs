namespace TripleG3.User32.WindowsHook.Mouse;

/// <summary>
/// Represents the XButton if used by the mouse.
/// </summary>
/// <remarks></remarks>
public enum Xbutton
{
    /// <summary>
    /// The XButton was not used.
    /// </summary>
    None = 0,

    /// <summary>
    /// Xbutton1 was used.
    /// </summary>
    X1 = 65536,

    /// <summary>
    /// XButton2 was used.
    /// </summary>
    X2 = 131072
}
