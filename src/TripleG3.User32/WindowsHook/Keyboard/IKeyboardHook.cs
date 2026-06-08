namespace TripleG3.User32.WindowsHook.Keyboard;

public interface IKeyboardHook : IWindowsHook
{
    event EventHandler<KeyboardHookEventArgs> KeyEvent;
}