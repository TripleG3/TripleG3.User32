using System.Runtime.InteropServices;

namespace TripleG3.User32;

public class Window
{
    /// <summary>
    /// Retrieves a handle to the desktop window. 
    /// The desktop window covers the entire screen. 
    /// The desktop window is the area on top of which other windows are painted.
    /// </summary>
    /// <returns>The return value is a handle to the desktop window.</returns>
    [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    public static extern nint GetDesktopWindow();

    /// <summary>
    /// The GetWindowDC function retrieves the device context (DC) for the entire window, including title bar, menus, and scroll bars. 
    /// A window device context permits painting anywhere in a window, 
    /// because the origin of the device context is the upper-left corner of the window instead of the client area.
    /// GetWindowDC assigns default attributes to the window device context each time it retrieves the device context.Previous attributes are lost.
    /// </summary>
    /// <param name="hWnd">
    /// A handle to the window with a device context that is to be retrieved. 
    /// If this value is NULL, GetWindowDC retrieves the device context for the entire screen.
    /// If this parameter is NULL, GetWindowDC retrieves the device context for the primary display monitor. 
    /// To get the device context for other display monitors, use the EnumDisplayMonitors and CreateDC functions.
    /// </param>
    /// <returns>
    /// If the function succeeds, the return value is a handle to a device context for the specified window.
    /// If the function fails, the return value is NULL, indicating an error or an invalid hWnd parameter.
    /// </returns>
    [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    public static extern nint GetWindowDC(nint hWnd);

    /// <summary>
    /// The ReleaseDC function releases a device context (DC), freeing it for use by other applications. 
    /// The effect of the ReleaseDC function depends on the type of DC. It frees only common and window DCs. 
    /// It has no effect on class or private DCs.
    /// </summary>
    /// <param name="hWnd">A handle to the window whose DC is to be released.</param>
    /// <param name="hDC">A handle to the DC to be released.</param>
    /// <returns>
    /// The return value indicates whether the DC was released. If the DC was released, the return value is 1.
    /// If the DC was not released, the return value is zero.
    /// </returns>
    [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    public static extern nint ReleaseDC(nint hWnd, nint hDC);

    /// <summary>
    /// Retrieves the dimensions of the bounding rectangle of the specified window. 
    /// The dimensions are given in screen coordinates that are relative to the upper-left corner of the screen.
    /// </summary>
    /// <param name="hWnd">A handle to the window.</param>
    /// <param name="lpRect">A pointer to a RECT structure that receives the screen coordinates of the upper-left and lower-right corners of the window.</param>
    /// <returns>
    /// If the function succeeds, the return value is nonzero.
    /// If the function fails, the return value is zero. To get extended error information, call GetLastError.
    /// </returns>
    /// <remarks>
    /// If the function fails, the return value is zero. To get extended error information, call GetLastError. 
    /// GetWindowRect is virtualized for DPI. 
    /// In Windows Vista and later, the Window Rect now includes the area occupied by the drop shadow. 
    /// Calling GetWindowRect will have different behavior depending on whether the window has ever been shown or not. 
    /// If the window has not been shown before, GetWindowRect will not include the area of the drop shadow. 
    /// To get the window bounds excluding the drop shadow, use DwmGetWindowAttribute, specifying DWMWA_EXTENDED_FRAME_BOUNDS. 
    /// Note that unlike the Window Rect, the DWM Extended Frame Bounds are not adjusted for DPI. 
    /// Getting the extended frame bounds can only be done after the window has been shown at least once.
    /// </remarks>
    [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    public static extern nint GetWindowRect(nint hWnd, out RECT lpRect);

    /// <summary>
    /// Retrieves a handle to the foreground window (the window with which the user is currently working). 
    /// The system assigns a slightly higher priority to the thread that creates the foreground window than it does to other threads.
    /// </summary>
    /// <returns>
    /// The return value is a handle to the foreground window. 
    /// The foreground window can be NULL in certain circumstances, such as when a window is losing activation.
    /// </returns>
    [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    public static extern nint GetForegroundWindow();
}
