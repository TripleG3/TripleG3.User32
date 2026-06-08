using System.Runtime.InteropServices;

namespace TripleG3.User32.WindowsHook.Keyboard;

/// <summary>
/// Passed with KeyEvents from the Keyboard class with all the information needed to read and handle the current key event.
/// </summary>
/// <remarks></remarks>
/// <remarks>
/// Instatiates the KeyEventArgs Class with the same signature returned to our HookProc Delegate returned with SetWindowsHookEx.
/// </remarks>
/// <param name="nCode">[in] Specifies a code the hook procedure uses to determine how to process the message. if (nCode is less than zero, the hook procedure must pass the message to the CallNextHookEx function without further processing and should return the value returned by CallNextHookEx. This parameter can be one of the following values. 
/// HC_ACTION
/// The wParam and lParam parameters contain information about a keyboard message.</param>
/// <param name="wParam">[in] Specifies the identifier of the keyboard message. This parameter can be one of the following messages: WM_KEYDOWN, WM_KEYUP, WM_SYSKEYDOWN, or WM_SYSKEYUP. </param>
/// <param name="lParam">[in] Pointer to a KBDLLHOOKSTRUCT structure. </param>
/// <remarks></remarks>
#pragma warning disable CS9107 // Parameter is captured into the state of the enclosing type and its value is also passed to the base constructor. The value might be captured by the base class as well.
public class KeyboardHookEventArgs(int nCode, nint wParam, nint lParam) : WindowsHookEventArgs(nCode, wParam, lParam)
#pragma warning restore CS9107 // Parameter is captured into the state of the enclosing type and its value is also passed to the base constructor. The value might be captured by the base class as well.
{
    private KBDLLHOOKSTRUCT keyboardHookStruct = Marshal.PtrToStructure<KBDLLHOOKSTRUCT>(lParam);

    /// <summary>
    /// True if the key is an extended key.
    /// </summary>
    /// <value></value>
    /// <returns></returns>
    /// <remarks>The flag value in KBDLLHOOKSTRUCTFlags.LLKHF_EXTENDED compared to the flag sent with the event.</remarks>
    public bool IsExtendedKey => KBDLLHOOKSTRUCTFlags.LLKHF_EXTENDED == (keyboardHookStruct.flags & KBDLLHOOKSTRUCTFlags.LLKHF_EXTENDED);

    /// <summary>
    /// True if the current key event was injected; meaning another application inserted the event and the keyboard was not physically used.
    /// </summary>
    /// <remarks>The flag value in KBDLLHOOKSTRUCTFlags.LLKHF_INJECTED compared to the flag sent with the event.</remarks>
    public bool IsInjected => KBDLLHOOKSTRUCTFlags.LLKHF_INJECTED == (keyboardHookStruct.flags & KBDLLHOOKSTRUCTFlags.LLKHF_INJECTED);

    /// <summary>
    /// True if the Alt Key is down.
    /// </summary>
    /// <value></value>
    /// <returns></returns>
    /// <remarks>The flag value in KBDLLHOOKSTRUCTFlags.LLKHF_INJECTED compared to the flag sent with the event.</remarks>
    public bool IsAltDown => KBDLLHOOKSTRUCTFlags.LLKHF_ALTDOWN == (keyboardHookStruct.flags & KBDLLHOOKSTRUCTFlags.LLKHF_ALTDOWN);

    /// <summary>
    /// True if the Key is up.
    /// </summary>
    /// <value></value>
    /// <returns></returns>
    /// <remarks>The flag value in KBDLLHOOKSTRUCTFlags.LLKHF_INJECTED compared to the flag sent with the event.</remarks>
    public bool IsKeyUp => KBDLLHOOKSTRUCTFlags.LLKHF_UP == (keyboardHookStruct.flags & KBDLLHOOKSTRUCTFlags.LLKHF_UP);

    /// <summary>
    /// KBDLLHOOKSTRUCT.vkCode
    /// </summary>
    public uint VirtualKeyCode => keyboardHookStruct.vkCode;

    /// <summary>
    /// KBDLLHOOKSTRUCT.scanCode
    /// </summary>
    public uint ScanCode => keyboardHookStruct.scanCode;

    /// <summary>
    /// KBDLLHOOKSTRUCT.time
    /// </summary>
    public uint Time => keyboardHookStruct.time;

    /// <summary>
    /// KBDLLHOOKSTRUCT.dwExtraInfo
    /// </summary>
    public uint ExtraInfo => keyboardHookStruct.dwExtraInfo;

    /// <summary>
    /// WindowsMessage
    /// </summary>
    public WindowsMessage WindowsMessage => (WindowsMessage)wParam.ToInt32();
}