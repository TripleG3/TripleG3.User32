using System.Runtime.InteropServices;

namespace TripleG3.User32.WindowsHook.Mouse;

#pragma warning disable CS9107 // Parameter is captured into the state of the enclosing type and its value is also passed to the base constructor. The value might be captured by the base class as well.
public class MouseHookEventArgs(int nCode, nint wParam, nint lParam) : WindowsHookEventArgs(nCode, wParam, lParam)
#pragma warning restore CS9107 // Parameter is captured into the state of the enclosing type and its value is also passed to the base constructor. The value might be captured by the base class as well.
{
    private MSLLHOOKSTRUCT MSLLHOOKSTRUCT => Marshal.PtrToStructure<MSLLHOOKSTRUCT>(LParam);

    /// <summary>
    /// The X coordinate of the mouse cursor on the screen.
    /// </summary>
    public int X => MSLLHOOKSTRUCT.pt.x;

    /// <summary>
    /// The Y coordinate of the mouse cursor on the screen.
    /// </summary>
    public int Y => MSLLHOOKSTRUCT.pt.y;

    /// <summary>
    /// True if the current mouse event was injected; meaning another application inserted the event and the mouse was not physically used.
    /// </summary>
    public bool IsInjected => MSLLHOOKSTRUCT.flags.HasFlag(MSLLHOOKSTRUCTFlags.LLMHF_INJECTED);

    /// <summary>
    /// Returns the direction the wheel was turned if used.
    /// </summary>
    public MouseWheelDirection WheelDirection => (WindowsMessage)wParam.ToInt32() == WindowsMessage.WM_MOUSEWHEEL
                                     ? MSLLHOOKSTRUCT.mouseData > 0
                                     ? MouseWheelDirection.Up
                                     : MouseWheelDirection.Down
                                     : MouseWheelDirection.None;

    /// <summary>
    /// The Xbutton on the mouse that was pressed or released.
    /// </summary>
    public Xbutton Xbutton => (WindowsMessage)wParam.ToInt32() == WindowsMessage.WM_XBUTTONDOWN
                           || (WindowsMessage)wParam.ToInt32() == WindowsMessage.WM_XBUTTONUP
                            ? (Xbutton)MSLLHOOKSTRUCT.mouseData
                            : Xbutton.None;
}
