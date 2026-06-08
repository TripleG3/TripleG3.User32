namespace TripleG3.User32.WindowsHook.Keyboard;

public class KeyboardHook : WindowsHookBase, IKeyboardHook
{
    public KeyboardHook() : base(WindowsHook.Hook.WH_KEYBOARD_LL) { }

    /// <summary>
    /// Generic KeyEvent, raised for every key stroke event. Happens before all other Key Events.
    /// if (Handled is set to True the remaining appropriate events KeyUp, KeyDown, SysKeyUp, and SysKeyDown events will still fire but the Key will still be handled for other applications.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <remarks></remarks>
    public event EventHandler<KeyboardHookEventArgs> KeyEvent = (sender, e) => { };

    /// <summary>
    /// Runs when Hook is called.
    /// </summary>
    /// <remarks></remarks>
    protected override void OnHook() { }

    /// <summary>
    /// Runs when UnHook is called.
    /// </summary>
    /// <remarks></remarks>
    protected override void OnUnHook() { }

    /// <summary>
    /// The Method address delegated to the HookProc Delegate and assigned to the HookProc_Delegated variable.
    /// </summary>
    /// <param name="nCode">[in] Specifies a code the hook procedure uses to determine how to process the message. if (nCode is less than zero, the hook procedure must pass the message to the CallNextHookEx function without further processing and should return the value returned by CallNextHookEx. This parameter can be one of the following values. 
    /// HC_ACTION
    /// The wParam and lParam parameters contain information about a keyboard message.</param>
    /// <param name="wParam">[in] Specifies the identifier of the keyboard message. This parameter can be one of the following messages: WM_KEYDOWN, WM_KEYUP, WM_SYSKEYDOWN, or WM_SYSKEYUP. </param>
    /// <param name="lParam">[in] Pointer to a KBDLLHOOKSTRUCT structure. </param>
    /// <returns>True if that event should be handled and not passed to other applications.</returns>
    /// <remarks></remarks>
    protected override void OnHookProc(int nCode, nint wParam, nint lParam)
    {
        var keyEventArgs = new KeyboardHookEventArgs(nCode, wParam, lParam);
        KeyEvent(this, keyEventArgs);
    }
}