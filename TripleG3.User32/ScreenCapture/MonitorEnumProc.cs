using System.Runtime.InteropServices;

namespace TripleG3.User32.ScreenCapture;

/// <summary>
/// Application-defined callback function used with EnumDisplayMonitors.
/// </summary>
/// <param name="hMonitor">A handle to the display monitor.</param>
/// <param name="hdcMonitor">A handle to a device context for the monitor, or zero when EnumDisplayMonitors was called with a zero HDC.</param>
/// <param name="lprcMonitor">The monitor rectangle in device-context or virtual-screen coordinates.</param>
/// <param name="dwData">Application-defined data passed from EnumDisplayMonitors.</param>
/// <returns>True to continue enumeration; false to stop enumeration.</returns>
[UnmanagedFunctionPointer(CallingConvention.Winapi)]
[return: MarshalAs(UnmanagedType.Bool)]
public delegate bool MonitorEnumProc(nint hMonitor, nint hdcMonitor, ref RECT lprcMonitor, nint dwData);
