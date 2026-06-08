using System.Runtime.InteropServices;

namespace TripleG3.User32.WindowsHook.Mouse;

[StructLayout(LayoutKind.Sequential)]
public struct PointL
{
    public int x;
    public int y;
}