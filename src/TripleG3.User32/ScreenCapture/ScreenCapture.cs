using System.Runtime.InteropServices;

namespace TripleG3.User32.ScreenCapture;

/// <summary>
/// Native user32.dll and gdi32.dll methods commonly used for monitor and window screen capture.
/// </summary>
public class ScreenCapture
{
    /// <summary>
    /// Enumerates display monitors that intersect the virtual screen or a device context clipping region.
    /// </summary>
    /// <param name="hdc">A display device context, or zero to enumerate monitors on the desktop.</param>
    /// <param name="lprcClip">A pointer to a RECT clipping rectangle, or zero for no clipping rectangle.</param>
    /// <param name="lpfnEnum">The callback invoked once for each enumerated monitor.</param>
    /// <param name="dwData">Application-defined data passed to the callback.</param>
    /// <returns>True if the function succeeds; otherwise, false.</returns>
    [DllImport("user32.dll", EntryPoint = "EnumDisplayMonitors", ExactSpelling = true, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool EnumDisplayMonitors(nint hdc, nint lprcClip, MonitorEnumProc lpfnEnum, nint dwData);

    /// <summary>
    /// Enumerates display monitors that intersect the virtual screen or a device context clipping region.
    /// </summary>
    /// <param name="hdc">A display device context, or zero to enumerate monitors on the desktop.</param>
    /// <param name="lprcClip">A RECT clipping rectangle.</param>
    /// <param name="lpfnEnum">The callback invoked once for each enumerated monitor.</param>
    /// <param name="dwData">Application-defined data passed to the callback.</param>
    /// <returns>True if the function succeeds; otherwise, false.</returns>
    [DllImport("user32.dll", EntryPoint = "EnumDisplayMonitors", ExactSpelling = true, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool EnumDisplayMonitors(nint hdc, [In] ref RECT lprcClip, MonitorEnumProc lpfnEnum, nint dwData);

    /// <summary>
    /// Retrieves information about a display monitor.
    /// </summary>
    /// <param name="hMonitor">A handle to the display monitor of interest.</param>
    /// <param name="lpmi">A MONITORINFO structure with cbSize initialized before the call.</param>
    /// <returns>True if the function succeeds; otherwise, false.</returns>
    [DllImport("user32.dll", EntryPoint = "GetMonitorInfoW", ExactSpelling = true, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool GetMonitorInfoW(nint hMonitor, ref MONITORINFO lpmi);

    /// <summary>
    /// Retrieves the specified system metric or system configuration setting.
    /// </summary>
    /// <param name="nIndex">The system metric or configuration setting to retrieve.</param>
    /// <returns>The requested system metric or configuration setting.</returns>
    [DllImport("user32.dll", EntryPoint = "GetSystemMetrics", ExactSpelling = true)]
    public static extern int GetSystemMetrics(int nIndex);

    /// <summary>
    /// Retrieves the dimensions of the bounding rectangle of the specified window.
    /// </summary>
    /// <param name="hWnd">A handle to the window.</param>
    /// <param name="lpRect">Receives the window rectangle in screen coordinates.</param>
    /// <returns>True if the function succeeds; otherwise, false.</returns>
    [DllImport("user32.dll", EntryPoint = "GetWindowRect", ExactSpelling = true, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool GetWindowRect(nint hWnd, out RECT lpRect);

    /// <summary>
    /// Retrieves a device context for the client area of a specified window, or for the entire screen when hWnd is zero.
    /// </summary>
    /// <param name="hWnd">A handle to the window whose device context is to be retrieved, or zero for the entire screen.</param>
    /// <returns>A handle to the device context, or zero if the function fails.</returns>
    [DllImport("user32.dll", EntryPoint = "GetDC", ExactSpelling = true)]
    public static extern nint GetDC(nint hWnd);

    /// <summary>
    /// Determines whether the specified window handle identifies an existing window.
    /// </summary>
    /// <param name="hWnd">A handle to the window to test.</param>
    /// <returns>True if the handle identifies an existing window; otherwise, false.</returns>
    [DllImport("user32.dll", EntryPoint = "IsWindow", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool IsWindow(nint hWnd);

    /// <summary>
    /// Copies a visual window into the specified device context.
    /// </summary>
    /// <param name="hwnd">A handle to the window to copy.</param>
    /// <param name="hdcBlt">A handle to the destination device context.</param>
    /// <param name="nFlags">Drawing options, such as PW_CLIENTONLY.</param>
    /// <returns>True if the function succeeds; otherwise, false.</returns>
    [DllImport("user32.dll", EntryPoint = "PrintWindow", ExactSpelling = true, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool PrintWindow(nint hwnd, nint hdcBlt, uint nFlags);

    /// <summary>
    /// Releases a device context retrieved by GetDC or GetWindowDC.
    /// </summary>
    /// <param name="hWnd">A handle to the window whose device context is to be released.</param>
    /// <param name="hDC">A handle to the device context to release.</param>
    /// <returns>1 if the device context was released; otherwise, zero.</returns>
    [DllImport("user32.dll", EntryPoint = "ReleaseDC", ExactSpelling = true)]
    public static extern int ReleaseDC(nint hWnd, nint hDC);

    /// <summary>
    /// Performs a bit-block transfer of color data from a source device context into a destination device context.
    /// </summary>
    /// <param name="hdc">A handle to the destination device context.</param>
    /// <param name="x">The x-coordinate of the upper-left corner of the destination rectangle.</param>
    /// <param name="y">The y-coordinate of the upper-left corner of the destination rectangle.</param>
    /// <param name="cx">The width of the source and destination rectangles.</param>
    /// <param name="cy">The height of the source and destination rectangles.</param>
    /// <param name="hdcSrc">A handle to the source device context.</param>
    /// <param name="x1">The x-coordinate of the upper-left corner of the source rectangle.</param>
    /// <param name="y1">The y-coordinate of the upper-left corner of the source rectangle.</param>
    /// <param name="rop">The raster-operation code.</param>
    /// <returns>True if the function succeeds; otherwise, false.</returns>
    [DllImport("gdi32.dll", EntryPoint = "BitBlt", ExactSpelling = true, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool BitBlt(nint hdc, int x, int y, int cx, int cy, nint hdcSrc, int x1, int y1, uint rop);

    /// <summary>
    /// Creates a bitmap compatible with the device associated with the specified device context.
    /// </summary>
    /// <param name="hdc">A handle to a device context.</param>
    /// <param name="cx">The bitmap width, in pixels.</param>
    /// <param name="cy">The bitmap height, in pixels.</param>
    /// <returns>A handle to the compatible bitmap, or zero if the function fails.</returns>
    [DllImport("gdi32.dll", EntryPoint = "CreateCompatibleBitmap", ExactSpelling = true, SetLastError = true)]
    public static extern nint CreateCompatibleBitmap(nint hdc, int cx, int cy);

    /// <summary>
    /// Creates a memory device context compatible with the specified device context.
    /// </summary>
    /// <param name="hdc">A handle to an existing device context, or zero to create a memory DC compatible with the current screen.</param>
    /// <returns>A handle to the memory device context, or zero if the function fails.</returns>
    [DllImport("gdi32.dll", EntryPoint = "CreateCompatibleDC", ExactSpelling = true, SetLastError = true)]
    public static extern nint CreateCompatibleDC(nint hdc);

    /// <summary>
    /// Deletes the specified device context.
    /// </summary>
    /// <param name="hdc">A handle to the device context to delete.</param>
    /// <returns>True if the function succeeds; otherwise, false.</returns>
    [DllImport("gdi32.dll", EntryPoint = "DeleteDC", ExactSpelling = true, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool DeleteDC(nint hdc);

    /// <summary>
    /// Deletes a logical pen, brush, font, bitmap, region, or palette.
    /// </summary>
    /// <param name="ho">A handle to the GDI object to delete.</param>
    /// <returns>True if the function succeeds; otherwise, false.</returns>
    [DllImport("gdi32.dll", EntryPoint = "DeleteObject", ExactSpelling = true, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool DeleteObject(nint ho);

    /// <summary>
    /// Selects an object into the specified device context.
    /// </summary>
    /// <param name="hdc">A handle to the device context.</param>
    /// <param name="h">A handle to the object to select.</param>
    /// <returns>A handle to the object being replaced, or zero if the function fails for non-region objects.</returns>
    [DllImport("gdi32.dll", EntryPoint = "SelectObject", ExactSpelling = true, SetLastError = true)]
    public static extern nint SelectObject(nint hdc, nint h);
}
