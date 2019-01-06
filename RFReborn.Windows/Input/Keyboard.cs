using System;
using System.Threading;
using RFReborn.Windows.Native;
using RFReborn.Windows.Native.Enums;

namespace RFReborn.Windows.Input
{
    /// <summary>
    ///     Provides methods to manipulate and generate Keyboard input.
    /// </summary>
    public static class Keyboard
    {
        /// <summary>
        ///     Determines if the <see cref="VirtualKeyCode" /> is an ExtendedKey
        /// </summary>
        /// <param name="keyCode">The key code.</param>
        /// <returns>true if the key code is an extended key; otherwise, false.</returns>
        /// <remarks>
        ///     The extended keys consist of the ALT and CTRL keys on the right-hand side of the keyboard; the INS, DEL, HOME, END,
        ///     PAGE UP, PAGE DOWN, and arrow keys in the clusters to the left of the numeric keypad; the NUM LOCK key; the BREAK
        ///     (CTRL+PAUSE) key; the PRINT SCRN key; and the divide (/) and ENTER keys in the numeric keypad.
        ///     See http://msdn.microsoft.com/en-us/library/ms646267(v=vs.85).aspx Section "Extended-Key Flag"
        /// </remarks>
        public static bool IsExtendedKey(VirtualKeyCode keyCode)
        {
            return keyCode == VirtualKeyCode.MENU || keyCode == VirtualKeyCode.LMENU
                   || keyCode == VirtualKeyCode.RMENU || keyCode == VirtualKeyCode.CONTROL
                   || keyCode == VirtualKeyCode.RCONTROL || keyCode == VirtualKeyCode.INSERT
                   || keyCode == VirtualKeyCode.DELETE || keyCode == VirtualKeyCode.HOME
                   || keyCode == VirtualKeyCode.END || keyCode == VirtualKeyCode.PRIOR || keyCode == VirtualKeyCode.NEXT
                   || keyCode == VirtualKeyCode.RIGHT || keyCode == VirtualKeyCode.UP || keyCode == VirtualKeyCode.LEFT
                   || keyCode == VirtualKeyCode.DOWN || keyCode == VirtualKeyCode.NUMLOCK
                   || keyCode == VirtualKeyCode.CANCEL || keyCode == VirtualKeyCode.SNAPSHOT
                   || keyCode == VirtualKeyCode.DIVIDE;
        }

        /// <summary>
        ///     Posts a WM_KEYDOWN message with a virtual key to a window using PostMessage.
        /// </summary>
        /// <param name="hWnd">
        ///     Handle of the window.
        /// </param>
        /// <param name="vkCode">
        ///     Key to send down.
        /// </param>
        public static void PostMessageKeyDown(IntPtr hWnd, VirtualKeyCode vkCode)
        {
            var scanCode = User32.MapVirtualKey((uint)vkCode, 0);

            var lParam = 0x00000001 | (scanCode << 16);
            if (IsExtendedKey(vkCode))
            {
                lParam |= 0x01000000;
            }

            User32.PostMessage(hWnd, WindowsMessage.WM_KEYDOWN, (uint)vkCode, lParam);
        }

        /// <summary>
        ///     Posts a WM_KEYUP message with a virtual key to a window using PostMessage.
        /// </summary>
        /// <param name="hWnd">
        ///     Handle of the window.
        /// </param>
        /// <param name="vkCode">
        ///     Key to send up.
        /// </param>
        public static void PostMessageKeyUp(IntPtr hWnd, VirtualKeyCode vkCode)
        {
            var scanCode = User32.MapVirtualKey((uint)vkCode, 0);
            var lParam = 0xC0000001 | (scanCode << 16);
            if (IsExtendedKey(vkCode))
            {
                lParam |= 0x01000000;
            }

            User32.PostMessage(hWnd, WindowsMessage.WM_KEYUP, (uint)vkCode, lParam);
        }

        /// <summary>
        ///     Posts a WM_KEYDOWN followed by a WM_KEYUP message after the delay, with the virtual key to the window using
        ///     PostMessage.
        /// </summary>
        /// <param name="hWnd">
        ///     Handle of the window.
        /// </param>
        /// <param name="vkCode">
        ///     Key to press-
        /// </param>
        /// <param name="delay">
        ///     Delay between WM_KEYDOWN and WM_KEYUP message. Default is 5
        /// </param>
        public static void PostMessageKeyPress(IntPtr hWnd, VirtualKeyCode vkCode, int delay = 5)
        {
            var scanCode = User32.MapVirtualKey((uint)vkCode, 0);

            var lParam = 0x00000001 | (scanCode << 16);
            if (IsExtendedKey(vkCode))
            {
                lParam |= 0x01000000;
            }

            User32.PostMessage(hWnd, WindowsMessage.WM_KEYDOWN, (uint)vkCode, lParam);
            Thread.Sleep(delay);
            lParam |= 0xC0000000;
            User32.PostMessage(hWnd, WindowsMessage.WM_KEYUP, (uint)vkCode, lParam);
        }
    }
}
