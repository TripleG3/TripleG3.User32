namespace TripleG3.User32.WindowsHook.Mouse;

/// <summary>
/// Specifies the event-injected flag. An application can use the following value to test the mouse flags. Value Purpose LLMHF_INJECTED Test the event-injected flag.  
/// 0
/// Specifies whether the event was injected. The value is 1 if the event was injected; otherwise, it is 0.
/// 1-15
/// Reserved.
/// </summary>
[Flags]
public enum MSLLHOOKSTRUCTFlags
{
    /// <summary>
    /// Test the event-injected (from any process) flag.
    /// </summary>
    LLMHF_INJECTED = 0x00000001,

    /// <summary>
    /// Test the event-injected (from a process running at lower integrity level) flag.
    /// </summary>
    LLMHF_LOWER_IL_INJECTED = 0x00000002
}
