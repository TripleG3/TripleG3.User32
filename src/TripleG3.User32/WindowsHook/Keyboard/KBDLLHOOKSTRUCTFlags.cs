namespace TripleG3.User32.WindowsHook.Keyboard;

/// <summary>
/// The bitwise flags return with the HookProc delegate assigned to the SetWindowsHookProc.
/// Specifies the extended-key flag, event-injected flag, context code, and transition-state flag. This member is specified as follows. An application can use the following values to test the keystroke flags. Value Purpose 
/// LLKHF_EXTENDED Test the extended-key flag.  
/// LLKHF_INJECTED Test the event-injected flag.  
/// LLKHF_ALTDOWN Test the context code.  
/// LLKHF_UP Test the transition-state flag.
/// 0
/// Specifies whether the key is an extended key, such as a function key or a key on the numeric keypad. The value is 1 if the key is an extended key; otherwise, it is 0.
/// 1-3
/// Reserved.
/// 4
/// Specifies whether the event was injected. The value is 1 if the event was injected; otherwise, it is 0.
/// 5
/// Specifies the context code. The value is 1 if the ALT key is pressed; otherwise, it is 0.
/// 6
/// Reserved.
/// 7
/// Specifies the transition state. The value is 0 if the key is pressed and 1 if it is being released.
/// </summary>
/// <remarks></remarks>
[Flags]
public enum KBDLLHOOKSTRUCTFlags : uint
{
    /// <summary>
    /// Test the extended-key flag. 
    /// </summary>
    /// <remarks></remarks>
    LLKHF_EXTENDED = 0x01,

    /// <summary>
    /// Test the event-injected flag.
    /// </summary>
    /// <remarks></remarks>
    LLKHF_INJECTED = 0x10,

    /// <summary>
    /// Test the context code. 
    /// </summary>
    /// <remarks></remarks>
    LLKHF_ALTDOWN = 0x20,

    /// <summary>
    /// Test the transition-state flag. 
    /// </summary>
    /// <remarks></remarks>
    LLKHF_UP = 0x80
}