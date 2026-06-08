using TripleG3.User32.Exceptions;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace TripleG3.User32.WindowsHook;

/// <summary>
/// Initializes the HookBase Class and determines the type of hook that will be installed.
/// </summary>
/// <param name="idHook">The HookType to be installed.</param>
public abstract partial class WindowsHookBase(Hook idHook) : IWindowsHook
{
    private static readonly nint baseAddress;
    static WindowsHookBase() => baseAddress = Process.GetCurrentProcess().MainModule?.BaseAddress
                                           ?? throw new BaseAddressNullException($"BaseAddress was null when activating {nameof(WindowsHookBase)}. This happens when the call to Process.GetCurrentProcess().MainModule?.BaseAddress is null which is used as the callback address for SetWindowsHookExA.",
                                                                                 new Win32Exception(Marshal.GetLastWin32Error()));

    private readonly Hook idHook = idHook;

    /// <summary>
    /// A generic event that runs when the hook is being processed.
    /// </summary>
    /// <param name="sender">HookBase</param>
    /// <param name="e">HookBaseEventArgs</param>
    public event EventHandler<WindowsHookEventArgs> HookProcessing = delegate { };

    /// <summary>
    /// If true prevents the hook messages from reaching other applications.
    /// </summary>
    public bool IsDisabled { get; set; }

    /// <summary>
    /// A value used to determine whether or not other applications or threads that have also installed this hook type will receive this hook event.
    /// This is based on the last hook to be installed to the chain, if another application installs a hook of the same type and also does not perform the CallNextHookEx then this application or thread
    /// will not receive the hook event. By default and strong recommendation you should ALWAYS call the next hook meaning this value should always be true.
    /// --This is not the same as the Disabled property which prevents all hook events of this type from being sent to any application.
    /// </summary>
    /// <value>True to continue processing the hook event to other hooks previously installed, false to discontinue other hooks installed.</value>
    /// <returns>Value indicating whether or not you are currently returning previously installed hooks.</returns>
    /// <remarks>This value may not work for all hooks, check msdn for more information.</remarks>
    public bool IsDisabledNextHook { get; set; }

    /// <summary>
    /// Hooks the Class.  If the class is already hooked once it is unhooked and an exception is thrown.
    /// </summary>
    public void Hook()
    {
        if (HookHandle != nint.Zero)
        {
            UnHook();
            throw new HookException($"{nameof(Hook)} cannot be called twice with same handle. {nameof(UnHook)} was called prior to this exception to prevent system error.");
        }
        HookHandle = SetWindowsHookExA((int)idHook, Marshal.GetFunctionPointerForDelegate<HookProc>(MainHookProc), baseAddress, 0);
        if (HookHandle == nint.Zero)
        {
            throw new HookException($"P{nameof(Hook)} failed because SetWindowsHookEx returned a value of null.");
        }
        OnHook();
    }

    /// <summary>
    /// Unhooks the class with a call to UnHookWindowsHookEx.
    /// Sets the hookHandle as nothing (meaning IsHooked returns false) and all other values should be reset.
    /// </summary>
    public int UnHook()
    {
        var success = UnhookWindowsHookEx(HookHandle);
        HookHandle = nint.Zero;
        return success;
    }

    /// <summary>
    /// The handle that SetWindowsHookEx returns to us as a pointer to our Hook from within the user32.dll.
    /// </summary>
    public nint HookHandle { get; private set; }

    /// <summary>
    /// The Delegate passed in to the SetWindowsHookEx as the callback variable.
    /// [in] Pointer to the hook procedure. If the dwThreadId parameter is zero or specifies the identifier of a thread created by a different process, the lpfn parameter must point to a hook procedure in a DLL.
    /// Otherwise, lpfn can point to a hook procedure in the code associated with the current process. 
    /// </summary>
    /// <param name="nCode">[in] Specifies a code the hook procedure uses to determine how to process the message. If nCode is less than zero, the hook procedure must pass the message to the CallNextHookEx function without further processing and should return the value returned by CallNextHookEx. This parameter can be one of the following values. 
    /// HC_ACTION
    /// The wParam and lParam parameters contain information about a keyboard message.</param>
    /// <param name="wParam">[in] Specifies the identifier of the keyboard message. This parameter can be one of the following messages: WM_KEYDOWN, WM_KEYUP, WM_SYSKEYDOWN, or WM_SYSKEYUP. </param>
    /// <param name="lParam">[in] Pointer to a KBDLLHOOKSTRUCT structure. </param>
    private delegate nint HookProc(int nCode, nint wParam, nint lParam);

    /// <summary>
    /// The SetWindowsHookEx function installs an application-defined hook procedure into a hook chain. You would install a hook procedure to monitor the system for certain types of events.
    /// These events are associated either with a specific thread or with all threads in the same desktop as the calling thread. 
    /// </summary>
    /// <param name="hook">[in] Specifies the type of hook procedure to be installed. This parameter can be one of the following values. 
    /// WH_CALLWNDPROC
    /// Installs a hook procedure that monitors messages before the system sends them to the destination window procedure. For more information, see the CallWndProc hook procedure.
    /// WH_CALLWNDPROCRET
    /// Installs a hook procedure that monitors messages after they have been processed by the destination window procedure. For more information, see the CallWndRetProc hook procedure.
    /// WH_CBT
    /// Installs a hook procedure that receives notifications useful to a computer-based training (CBT) application. For more information, see the CBTProc hook procedure.
    /// WH_DEBUG
    /// Installs a hook procedure useful for debugging other hook procedures. For more information, see the DebugProc hook procedure.
    /// WH_FOREGROUNDIDLE
    /// Installs a hook procedure that will be called when the application's foreground thread is about to become idle.This hook is useful for performing low priority tasks during idle time.For more information, see the ForegroundIdleProc hook procedure.
    /// WH_GETMESSAGE
    /// Installs a hook procedure that monitors messages posted to a message queue. For more information, see the GetMsgProc hook procedure.
    /// WH_JOURNALPLAYBACK
    /// Installs a hook procedure that posts messages previously recorded by a WH_JOURNALRECORD hook procedure. For more information, see the JournalPlaybackProc hook procedure.
    /// WH_JOURNALRECORD
    /// Installs a hook procedure that records input messages posted to the system message queue. This hook is useful for recording macros. For more information, see the JournalRecordProc hook procedure.
    /// WH_KEYBOARD
    /// Installs a hook procedure that monitors keystroke messages. For more information, see the KeyboardProc hook procedure.
    /// WH_KEYBOARD_LL
    /// Windows NT/2000/XP: Installs a hook procedure that monitors low-level keyboard input events. For more information, see the LowLevelKeyboardProc hook procedure.
    /// WH_MOUSE
    /// Installs a hook procedure that monitors mouse messages. For more information, see the MouseProc hook procedure.
    /// WH_MOUSE_LL
    /// Windows NT/2000/XP: Installs a hook procedure that monitors low-level mouse input events. For more information, see the LowLevelMouseProc hook procedure.
    /// WH_MSGFILTER
    /// Installs a hook procedure that monitors messages generated as a result of an input event in a dialog box, message box, menu, or scroll bar. For more information, see the MessageProc hook procedure.
    /// WH_SHELL
    /// Installs a hook procedure that receives notifications useful to shell applications. For more information, see the ShellProc hook procedure.
    /// WH_SYSMSGFILTER
    /// Installs a hook procedure that monitors messages generated as a result of an input event in a dialog box, message box, menu, or scroll bar. The hook procedure monitors these messages for all applications in the same desktop as the calling thread. For more information, see the SysMsgProc hook procedure.</param>
    /// <param name="lpfn">[in] Pointer to the hook procedure. If the dwThreadId parameter is zero or specifies the identifier of a thread created by a different process, the lpfn parameter must point to a hook procedure in a DLL. Otherwise, lpfn can point to a hook procedure in the code associated with the current process. </param>
    /// <param name="hMod">[in] Handle to the DLL containing the hook procedure pointed to by the lpfn parameter. The hMod parameter must be set to NULL if the dwThreadId parameter specifies a thread created by the current process and if the hook procedure is within the code associated with the current process. </param>
    /// <param name="dwThreadId">[in] Specifies the identifier of the thread with which the hook procedure is to be associated. If this parameter is zero, the hook procedure is associated with all existing threads running in the same desktop as the calling thread. </param>
    /// <returns>If the function succeeds, the return value is the handle to the hook procedure.
    /// If the function fails, the return value is NULL. To get extended error information, call GetLastError.</returns>
    [DllImport("user32.dll", EntryPoint = "SetWindowsHookExA", ExactSpelling = true)]
    private static extern nint SetWindowsHookExA([In] int hook, [In] nint lpfn, [In] nint hMod, [In] uint dwThreadId);

    /// <summary>
    /// The CallNextHookEx function passes the hook information to the next hook procedure in the current hook chain. A hook procedure can call this function either before or after processing the hook information. 
    /// </summary>
    /// <param name="hhk">[in] Windows 95/98/ME: Handle to the current hook. An application receives this handle as a result of a previous call to the SetWindowsHookEx function. 
    /// Windows NT/XP/2003: Ignored.</param>
    /// <param name="nCode">[in] Specifies the hook code passed to the current hook procedure. The next hook procedure uses this code to determine how to process the hook information.</param>
    /// <param name="wParam">[in] Specifies the wParam value passed to the current hook procedure. The meaning of this parameter depends on the type of hook associated with the current hook chain.</param>
    /// <param name="lParam">[in] Specifies the lParam value passed to the current hook procedure. The meaning of this parameter depends on the type of hook associated with the current hook chain.</param>
    [DllImport("user32.dll", EntryPoint = "CallNextHookEx", ExactSpelling = true)]
    private static extern nint CallNextHookEx(nint hhk, int nCode, nint wParam, nint lParam);

    /// <summary>
    /// The UnhookWindowsHookEx function removes a hook procedure installed in a hook chain by the SetWindowsHookEx function. 
    /// </summary>
    /// <param name="hhk">[in] Handle to the hook to be removed. This parameter is a hook handle obtained by a previous call to SetWindowsHookEx.</param>
    /// <returns>
    /// If the function succeeds, the return value is nonzero.
    /// If the function fails, the return value is zero. To get extended error information, call GetLastError.
    /// </returns>
    [DllImport("user32.dll", EntryPoint = "UnhookWindowsHookEx", ExactSpelling = true)]
    private static extern int UnhookWindowsHookEx(nint hhk);

    /// <summary>
    /// The Delegate passed in to the SetWindowsHookEx as the callback variable.
    /// [in] Pointer to the hook procedure. If the dwThreadId parameter is zero or specifies the identifier of a thread created by a different process, the lpfn parameter must point to a hook procedure in a DLL.
    /// Otherwise, lpfn can point to a hook procedure in the code associated with the current process. 
    /// </summary>
    /// <param name="nCode">[in] Specifies a code the hook procedure uses to determine how to process the message. If nCode is less than zero, the hook procedure must pass the message to the CallNextHookEx function without further processing and should return the value returned by CallNextHookEx. This parameter can be one of the following values. 
    /// HC_ACTION
    /// The wParam and lParam parameters contain information about a keyboard message.</param>
    /// <param name="wParam">[in] Specifies the identifier of the keyboard message. This parameter can be one of the following messages: WM_KEYDOWN, WM_KEYUP, WM_SYSKEYDOWN, or WM_SYSKEYUP. </param>
    /// <param name="lParam">[in] Pointer to a KBDLLHOOKSTRUCT structure. </param>
    private nint MainHookProc(int nCode, nint wParam, nint lParam)
    {
        if (nCode < 0)
        {
            return CallNextHookEx(HookHandle, nCode, wParam, lParam);
        }
        var hookBaseEventArgs = new WindowsHookEventArgs(nCode, wParam, lParam);
        HookProcessing.Invoke(this, hookBaseEventArgs);
        OnHookProc(nCode, wParam, lParam);
        if (IsDisabled)
        {
            if (IsDisabledNextHook)
            {
                return new nint(1);
            }
            CallNextHookEx(nint.Zero, nCode, wParam, lParam);
            return new nint(1);
        }
        else
        {
            if (IsDisabledNextHook)
            {
                return nint.Zero;
            }
            return CallNextHookEx(HookHandle, nCode, wParam, lParam);
        }
    }

    /// <summary>
    /// The Delegate passed in to the SetWindowsHookEx as the callback variable.
    /// [in] Pointer to the hook procedure. If the dwThreadId parameter is zero or specifies the identifier of a thread created by a different process, the lpfn parameter must point to a hook procedure in a DLL.
    /// Otherwise, lpfn can point to a hook procedure in the code associated with the current process. 
    /// </summary>
    /// <param name="nCode">[in] Specifies a code the hook procedure uses to determine how to process the message. If nCode is less than zero, the hook procedure must pass the message to the CallNextHookEx function without further processing and should return the value returned by CallNextHookEx. This parameter can be one of the following values. 
    /// HC_ACTION
    /// The wParam and lParam parameters contain information about a keyboard message.</param>
    /// <param name="wParam">[in] Specifies the identifier of the keyboard message. This parameter can be one of the following messages: WM_KEYDOWN, WM_KEYUP, WM_SYSKEYDOWN, or WM_SYSKEYUP. </param>
    /// <param name="lParam">[in] Pointer to a KBDLLHOOKSTRUCT structure. </param>
    protected abstract void OnHookProc(int nCode, nint wParam, nint lParam);

    /// <summary>
    /// Called when Hook is called.  Run any extra unhooking code you wish to perform here.
    /// </summary>
    protected abstract void OnHook();

    /// <summary>
    /// Called when UnHook is called.  Run any extra unhooking code you wish to perform here.
    /// </summary>
    protected abstract void OnUnHook();

    ~WindowsHookBase() => UnHook();
}
