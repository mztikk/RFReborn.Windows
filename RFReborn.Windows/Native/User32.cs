using System;
using System.Runtime.InteropServices;

namespace RFReborn.Windows.Native
{
    /// <summary>
    /// Methods from the User32.dll
    /// </summary>
    public static class User32
    {
        /// <summary>
        ///     Sends the specified message to a window or windows. The SendMessage function calls the window procedure for the
        ///     specified window and does not return until the window procedure has processed the message.
        ///     To send a message and return immediately, use the SendMessageCallback or SendNotifyMessage function.To post a
        ///     message to a thread's message queue and return immediately, use the PostMessage or PostThreadMessage function.
        /// </summary>
        /// <param name="hWnd">
        ///     A handle to the window whose window procedure will receive the message. If this parameter is HWND_BROADCAST
        ///     ((HWND)0xffff), the message is sent to all top-level windows in the system, including disabled or invisible unowned
        ///     windows, overlapped windows, and pop-up windows; but the message is not sent to child windows.
        ///     Message sending is subject to UIPI.
        ///     The thread of a process can send messages only to message queues of threads in
        ///     processes of lesser or equal integrity level.
        /// </param>
        /// <param name="uMsg">
        ///     The message to be sent.
        /// </param>
        /// <param name="wParam">
        ///     Additional message-specific information.
        /// </param>
        /// <param name="lParam">
        ///     Additional message-specific information.
        /// </param>
        /// <returns>
        ///     The return value specifies the result of the message processing; it depends on the message sent.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int SendMessage([In] IntPtr hWnd, [In] int uMsg, [In] uint wParam, [In] uint lParam);

        /// <summary>
        ///     Translates a character to the corresponding virtual-key code and shift state for the current keyboard.
        /// </summary>
        /// <param name="ch">
        ///     The character to be translated into a virtual-key code.
        /// </param>
        /// <returns>
        ///     If the function succeeds, the low-order byte of the return value contains the virtual-key code and the high-order
        ///     byte contains the shift state, which can be a combination of the following flag bits.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern short VkKeyScan(char ch);
    }
}
