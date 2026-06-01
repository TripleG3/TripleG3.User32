using System.Runtime.InteropServices;

namespace TripleG3.User32.ScreenCapture;

/// <summary>
/// Contains information about a display monitor.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct MONITORINFO
{
    /// <summary>
    /// The size of the structure, in bytes. Set this to sizeof(MONITORINFO) before calling GetMonitorInfoW.
    /// </summary>
    public uint cbSize;

    /// <summary>
    /// The display monitor rectangle, expressed in virtual-screen coordinates.
    /// </summary>
    public RECT rcMonitor;

    /// <summary>
    /// The work area rectangle of the display monitor, expressed in virtual-screen coordinates.
    /// </summary>
    public RECT rcWork;

    /// <summary>
    /// Flags that represent attributes of the display monitor.
    /// </summary>
    public uint dwFlags;

    /// <summary>
    /// Indicates that the monitor is the primary display monitor.
    /// </summary>
    public const uint MONITORINFOF_PRIMARY = 0x00000001;

    /// <summary>
    /// Creates a MONITORINFO value with cbSize initialized for GetMonitorInfoW.
    /// </summary>
    /// <returns>A MONITORINFO value with cbSize set to the unmanaged structure size.</returns>
    public static MONITORINFO Create() => new()
    {
        cbSize = (uint)Marshal.SizeOf<MONITORINFO>()
    };
}
