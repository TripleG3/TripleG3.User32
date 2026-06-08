using System.Runtime.InteropServices;

namespace TripleG3.User32.WindowsHook.Mouse;

/// <summary>
/// Used to perform a low level hook on the mouse then read and fire mouse event information.
/// </summary>
public class MouseHook : WindowsHookBase, IMouseHook
{
    public MouseHook() : base(WindowsHook.Hook.WH_MOUSE_LL) { }

    public event EventHandler<MouseHookEventArgs> MouseEvent = (sender, e) => { };

    protected override void OnHook() { }
    protected override void OnUnHook() { }


    /// <summary>
    /// The address of the delegate passed to SetWindowsHookEx.  Pointed to by LowLevelMouseProc_Delegated, instatiation of LowLevelMouseProc.
    /// </summary>
    /// <param name="nCode">[in] Specifies a code the hook procedure uses to determine how to process the message.
    /// If nCode is less than zero, the hook procedure must pass the message to the CallNextHookEx function without further processing and should return the value returned by CallNextHookEx.
    /// This parameter can be one of the following values. 
    /// HC_ACTION
    /// The wParam and lParam parameters contain information about a mouse message</param>
    /// <param name="wParam">[in] Specifies the identifier of the mouse message. This parameter can be one of the following messages:
    /// WM_LBUTTONDOWN, WM_LBUTTONUP, WM_MOUSEMOVE, WM_MOUSEWHEEL, WM_MOUSEHWHEEL, WM_RBUTTONDOWN, or WM_RBUTTONUP.</param>
    /// <param name="lParam">[in] Pointer to an MSLLHOOKSTRUCT structure.</param>
    /// <returns>True if that event should be handled and not passed to other applications.</returns>
    /// <remarks>An application installs the hook procedure by specifying the WH_MOUSE_LL hook type and a pointer to the hook procedure in a call to the SetWindowsHookEx function.
    /// This hook is called in the context of the thread that installed it. The call is made by sending a message to the thread that installed the hook. Therefore, the thread that installed the hook must have a message loop.
    /// The hook procedure should process a message in less time than the data entry specified in the LowLevelHooksTimeout value in the following registry key: 
    /// HKEY_CURRENT_USER\Control Panel\Desktop
    /// The value is in milliseconds. If the hook procedure does not return during this interval, the system will pass the message to the next hook.
    /// Note that debug hooks cannot track this type of hook.</remarks>
    protected override void OnHookProc(int nCode, nint wParam, [In] nint lParam)
    {
        var mouseEventArgs = new MouseHookEventArgs(nCode, wParam, lParam);
        MouseEvent.Invoke(this, mouseEventArgs);
    }
}
