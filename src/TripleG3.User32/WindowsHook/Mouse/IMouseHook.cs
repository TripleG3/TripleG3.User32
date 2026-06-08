namespace TripleG3.User32.WindowsHook.Mouse;

public interface IMouseHook : IWindowsHook
{
    event EventHandler<MouseHookEventArgs> MouseEvent;
}