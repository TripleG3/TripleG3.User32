namespace TripleG3.User32.WindowsHook;

/// <summary>
/// Instatiates the KeyEventArgs Class with the same signature returned to our HookProc Delegate returned with SetWindowsHookEx.
/// </summary>
/// <param name="nCode">[in] Specifies a code the hook procedure uses to determine how to process the message. If nCode is less than zero, the hook procedure must pass the message to the CallNextHookEx function without further processing and should return the value returned by CallNextHookEx. This parameter can be one of the following values. 
/// HC_ACTION
/// The wParam and lParam parameters contain information about a keyboard message.</param>
/// <param name="wParam">[in] Specifies the identifier of the keyboard message. This parameter can be one of the following messages: WM_KEYDOWN, WM_KEYUP, WM_SYSKEYDOWN, or WM_SYSKEYUP. </param>
/// <param name="lParam">[in] Pointer to a KBDLLHOOKSTRUCT structure. </param>
public class WindowsHookEventArgs(int nCode, nint wParam, nint lParam) : EventArgs
{
    /// <summary>
    /// For information or wrapping purposes only.
    /// [in] Specifies a code the hook procedure uses to determine how to process the message. If nCode is less than zero, the hook procedure must pass the message to the CallNextHookEx function without further processing and should return the value returned by CallNextHookEx. This parameter can be one of the following values. 
    /// HC_ACTION
    /// The wParam and lParam parameters contain information about a keyboard message.
    /// </summary>
    public int NCode => nCode;

    /// <summary>
    /// [in] Specifies the identifier of the keyboard message. This parameter can be one of the following messages: WM_KEYDOWN, WM_KEYUP, WM_SYSKEYDOWN, or WM_SYSKEYUP. 
    /// </summary>
    public nint WParam => wParam;

    /// <summary>
    /// [in] Pointer to a KBDLLHOOKSTRUCT structure.
    /// The exact same as KeyboardHookStruct but named to match MSDN to not confuse developers wrapping this DLL.
    /// </summary>
    public nint LParam => lParam;
}