

using System;

namespace MicBoard
{
    [Flags]
    public enum KeyEnum
    {
        BackSpace = 8,
        Shift = 16,
        Ctrl = 17,
        Alt = 18,
        CapsLock = 20,
        Esc = 27,
        Space = 32,
        PageUp = 33,
        PageDown = 34,
        End = 35,
        Home = 36,
        Left = 37,
        Up = 38,
        Right = 39,
        Down = 40,
        Insert = 45,
        Delete = 46,
        NumLock = 144
    }
}
