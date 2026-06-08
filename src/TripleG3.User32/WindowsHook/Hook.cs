namespace TripleG3.User32.WindowsHook;

public enum Hook
{
    WH_JOURNALRECORD = 0,
    WH_JOURNALPLAYBACK = 1,
    WH_KEYBOARD = 2,
    WH_GETMESSAGE = 3,
    WH_CALLWNDPROC = 4,
    WH_CBT = 5,
    WH_SYSMSGFILTER = 6,
    WH_MOUSE = 7,
    WH_HARDWARE = 8,
    WH_DEBUG = 9,
    WH_SHELL = 10,
    WH_FOREGROUNDIDLE = 11,
    WH_CALLWNDPROCRET = 12,
    /// <summary>
    /// Low-level keyboard input events. For more information, see the LowLevelKeyboardProc hook procedure.
    /// Used as the hook value in SetWindowsHookEx. 
    /// </summary>
    WH_KEYBOARD_LL = 13,
    /// <summary>
    /// Low-level mouse input events. For more information, see the LowLevelMouseProc hook procedure.
    /// </summary>
    WH_MOUSE_LL = 14
}
