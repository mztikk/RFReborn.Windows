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
        ///     Modifier Keys.
        /// </summary>
        public enum Modifier : byte
        {
            /// <summary>
            ///     No modifier.
            /// </summary>
            None,

            /// <summary>
            ///     Shift modifier.
            /// </summary>
            Shift,

            /// <summary>
            ///     Ctrl modifier.
            /// </summary>
            Ctrl,

            /// <summary>
            ///     Alt modifier.
            /// </summary>
            Alt
        }

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
        public static bool IsExtendedKey(VirtualKeyCode keyCode) => keyCode == VirtualKeyCode.MENU || keyCode == VirtualKeyCode.LMENU
                   || keyCode == VirtualKeyCode.RMENU || keyCode == VirtualKeyCode.CONTROL
                   || keyCode == VirtualKeyCode.RCONTROL || keyCode == VirtualKeyCode.INSERT
                   || keyCode == VirtualKeyCode.DELETE || keyCode == VirtualKeyCode.HOME
                   || keyCode == VirtualKeyCode.END || keyCode == VirtualKeyCode.PRIOR || keyCode == VirtualKeyCode.NEXT
                   || keyCode == VirtualKeyCode.RIGHT || keyCode == VirtualKeyCode.UP || keyCode == VirtualKeyCode.LEFT
                   || keyCode == VirtualKeyCode.DOWN || keyCode == VirtualKeyCode.NUMLOCK
                   || keyCode == VirtualKeyCode.CANCEL || keyCode == VirtualKeyCode.SNAPSHOT
                   || keyCode == VirtualKeyCode.DIVIDE;

        /// <summary>
        ///     Translates a char to a virtual key code and modifier.
        /// </summary>
        /// <param name="ch">
        ///     Char to translate.
        /// </param>
        /// <returns>Virtual key code of the car and the modifier needed.</returns>
        public static (VirtualKeyCode, Modifier) VkKeyScan(char ch)
        {
            short data = User32.VkKeyScan(ch);
            VirtualKeyCode vkc = (VirtualKeyCode)(data & 0x2ff);
            int b = data >> 8;
            Modifier mod = Modifier.None;
            if ((b & 1) != 0)
            {
                mod = Modifier.Shift;
            }
            else if ((b & 2) != 0)
            {
                mod = Modifier.Ctrl;
            }
            else if ((b & 4) != 0)
            {
                mod = Modifier.Alt;
            }

            return (vkc, mod);
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
            uint scanCode = User32.MapVirtualKey((uint)vkCode, 0);

            uint lParam = 0x00000001 | (scanCode << 16);
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
            uint scanCode = User32.MapVirtualKey((uint)vkCode, 0);
            uint lParam = 0xC0000001 | (scanCode << 16);
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
            uint scanCode = User32.MapVirtualKey((uint)vkCode, 0);

            uint lParam = 0x00000001 | (scanCode << 16);
            if (IsExtendedKey(vkCode))
            {
                lParam |= 0x01000000;
            }

            User32.PostMessage(hWnd, WindowsMessage.WM_KEYDOWN, (uint)vkCode, lParam);
            Thread.Sleep(delay);
            lParam |= 0xC0000000;
            User32.PostMessage(hWnd, WindowsMessage.WM_KEYUP, (uint)vkCode, lParam);
        }

        /// <summary>
        ///     Sends a string(every char in a string) to the window using PostMessage.
        /// </summary>
        /// <param name="hWnd">
        ///     Handle of the window.
        /// </param>
        /// <param name="textToSend">
        ///     String to send,
        /// </param>
        /// <param name="delay">
        ///     Delay between each PostMessage call.
        /// </param>
        public static void PostMessageString(IntPtr hWnd, string textToSend, int delay = 5)
        {
            for (int i = 0; i < textToSend.Length; i++)
            {
                (VirtualKeyCode key, Modifier mod) = VkKeyScan(textToSend[i]);
                switch (mod)
                {
                    case Modifier.Shift:
                        PostMessageKeyDown(hWnd, VirtualKeyCode.SHIFT);
                        break;
                    case Modifier.Ctrl:
                        PostMessageKeyDown(hWnd, VirtualKeyCode.CONTROL);
                        break;
                    case Modifier.Alt:
                        PostMessageKeyDown(hWnd, VirtualKeyCode.MENU);
                        break;
                }

                PostMessageKeyPress(hWnd, key, delay);

                switch (mod)
                {
                    case Modifier.Shift:
                        PostMessageKeyUp(hWnd, VirtualKeyCode.SHIFT);
                        break;
                    case Modifier.Ctrl:
                        PostMessageKeyUp(hWnd, VirtualKeyCode.CONTROL);
                        break;
                    case Modifier.Alt:
                        PostMessageKeyUp(hWnd, VirtualKeyCode.MENU);
                        break;
                }

                if (i < textToSend.Length - 1)
                {
                    Thread.Sleep(delay);
                }
            }
        }

        /// <summary>
        /// Determines whether a key is up or down at the time the function is called, and whether the key was pressed after a previous call to GetAsyncKeyState.
        /// </summary>
        /// <param name="vKey">The virtual-key code</param>
        /// <returns>returns <see langword="true"/> if the given key is down, <see langword="false"/> otherwise.</returns>
        public static bool IsKeyDown(VirtualKeyCode vKey) => User32.GetAsyncKeyState(vKey);

        /// <summary>
        /// Sends a keybd_event with the virtual key and either <see cref="KeyEventF.EXTENDEDKEY"/> or <see cref="KeyEventF.NONE"/> as flags.
        /// </summary>
        /// <param name="vKey">Key to send down.</param>
        public static void KeyboardEventDown(VirtualKeyCode vKey)
        {
            bool extended = IsExtendedKey(vKey);
            User32.keybd_event(vKey, 0, extended ? KeyEventF.EXTENDEDKEY : KeyEventF.NONE, 0);
        }

        /// <summary>
        /// Sends a keybd_event with the virtual key and <see cref="KeyEventF.KEYUP"/> flags.
        /// </summary>
        /// <param name="vKey">Key to send up.</param>
        public static void KeyboardEventUp(VirtualKeyCode vKey) => User32.keybd_event(vKey, 0, KeyEventF.KEYUP, 0);

        /// <summary>
        /// Sends a <see cref="KeyboardEventDown(VirtualKeyCode)"/> followed by a <see cref="KeyboardEventUp(VirtualKeyCode)"/> seperated with the given delay.
        /// </summary>
        /// <param name="vKey">Key to press</param>
        /// <param name="delay">Delay between down and up.(Default: 5)</param>
        public static void KeyBoardEventPress(VirtualKeyCode vKey, int delay = 5)
        {
            KeyboardEventDown(vKey);
            Thread.Sleep(delay);
            KeyboardEventUp(vKey);
        }
    }
}
