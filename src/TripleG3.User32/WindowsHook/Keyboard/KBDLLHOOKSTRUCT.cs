using System.Runtime.InteropServices;

namespace TripleG3.User32.WindowsHook.Keyboard;

/// <summary>
/// The Structure returned from the SetWindowsHookEx as the lParam value. Returns information about the key event.
/// </summary>
/// <remarks></remarks>
[StructLayout(LayoutKind.Sequential)]
public struct KBDLLHOOKSTRUCT
{
    /// <summary>
    /// Virtual Key Code returned with the key event.
    /// </summary>
    /// <remarks></remarks>
    public uint vkCode;

    /// <summary>
    /// Hardware Scancode returned with the key event.
    /// </summary>
    /// <remarks></remarks>
    public uint scanCode;

    /// <summary>
    /// The bit-wise flags returned from the key event as BLDLLHOOKSTRUCTFlags.
    /// </summary>
    /// <remarks></remarks>
    public KBDLLHOOKSTRUCTFlags flags;

    /// <summary>
    /// The time of the key event from the time of system start.
    /// </summary>
    /// <remarks></remarks>
    public uint time;

    /// <summary>
    /// Extra information associated with the key event; currently not used.
    /// </summary>
    /// <remarks></remarks>
    public uint dwExtraInfo;
}
