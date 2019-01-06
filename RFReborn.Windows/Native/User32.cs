using System;
using System.Runtime.InteropServices;
using RFReborn.Windows.Native.Enums;

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

        /// <summary>
        ///     Translates (maps) a virtual-key code into a scan code or character value, or translates a scan code into a
        ///     virtual-key code.
        ///     To specify a handle to the keyboard layout to use for translating the specified code, use the MapVirtualKeyEx
        ///     function.
        /// </summary>
        /// <param name="uCode">
        ///     The virtual key code or scan code for a key. How this value is interpreted depends on the value of the uMapType
        ///     parameter.
        /// </param>
        /// <param name="uMapType">
        ///     The translation to be performed. The value of this parameter depends on the value of the uCode parameter.
        /// </param>
        /// <returns>
        ///     The return value is either a scan code, a virtual-key code, or a character value, depending on the value of uCode
        ///     and uMapType. If there is no translation, the return value is zero.
        /// </returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern uint MapVirtualKey(uint uCode, uint uMapType);

        /// <summary>
        ///     Places (posts) a message in the message queue associated with the thread that created the specified window and
        ///     returns without waiting for the thread to process the message.
        ///     To post a message in the message queue associated with a thread, use the PostThreadMessage function.
        /// </summary>
        /// <param name="hWnd">
        ///     A handle to the window whose window procedure is to receive the message. The following values have special
        ///     meanings.
        ///     HWND_BROADCAST ((HWND)0xffff): The message is posted to all top-level windows in the system, including disabled or
        ///     invisible unowned windows, overlapped windows, and pop-up windows. The message is not posted to child windows.
        ///     NULL: The function behaves like a call to PostThreadMessage with the dwThreadId parameter set to the identifier of
        ///     the current thread.
        /// </param>
        /// <param name="msg">
        ///     The message to be posted.
        /// </param>
        /// <param name="wParam">
        ///     Additional message-specific information.
        /// </param>
        /// <param name="lParam">
        ///     Additional message-specific information.
        /// </param>
        /// <returns>
        ///     If the function succeeds, the return value is nonzero.
        ///     If the function fails, the return value is zero.To get extended error information, call GetLastError.GetLastError
        ///     returns ERROR_NOT_ENOUGH_QUOTA when the limit is hit.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PostMessage([In] IntPtr hWnd, [In] uint msg, [In] uint wParam, [In] uint lParam);

        /// <summary>
        ///     Places (posts) a message in the message queue associated with the thread that created the specified window and
        ///     returns without waiting for the thread to process the message.
        ///     To post a message in the message queue associated with a thread, use the PostThreadMessage function.
        /// </summary>
        /// <param name="hWnd">
        ///     A handle to the window whose window procedure is to receive the message. The following values have special
        ///     meanings.
        ///     HWND_BROADCAST ((HWND)0xffff): The message is posted to all top-level windows in the system, including disabled or
        ///     invisible unowned windows, overlapped windows, and pop-up windows. The message is not posted to child windows.
        ///     NULL: The function behaves like a call to PostThreadMessage with the dwThreadId parameter set to the identifier of
        ///     the current thread.
        /// </param>
        /// <param name="msg">
        ///     The message to be posted.
        /// </param>
        /// <param name="wParam">
        ///     Additional message-specific information.
        /// </param>
        /// <param name="lParam">
        ///     Additional message-specific information.
        /// </param>
        /// <returns>
        ///     If the function succeeds, the return value is nonzero.
        ///     If the function fails, the return value is zero.To get extended error information, call GetLastError.GetLastError
        ///     returns ERROR_NOT_ENOUGH_QUOTA when the limit is hit.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PostMessage([In] IntPtr hWnd, [In] uint msg, [In] ulong wParam, [In] uint lParam);

        /// <summary>
        ///     Places (posts) a message in the message queue associated with the thread that created the specified window and
        ///     returns without waiting for the thread to process the message.
        ///     To post a message in the message queue associated with a thread, use the PostThreadMessage function.
        /// </summary>
        /// <param name="hWnd">
        ///     A handle to the window whose window procedure is to receive the message. The following values have special
        ///     meanings.
        ///     HWND_BROADCAST ((HWND)0xffff): The message is posted to all top-level windows in the system, including disabled or
        ///     invisible unowned windows, overlapped windows, and pop-up windows. The message is not posted to child windows.
        ///     NULL: The function behaves like a call to PostThreadMessage with the dwThreadId parameter set to the identifier of
        ///     the current thread.
        /// </param>
        /// <param name="msg">
        ///     The message to be posted.
        /// </param>
        /// <param name="wParam">
        ///     Additional message-specific information.
        /// </param>
        /// <param name="lParam">
        ///     Additional message-specific information.
        /// </param>
        /// <returns>
        ///     If the function succeeds, the return value is nonzero.
        ///     If the function fails, the return value is zero.To get extended error information, call GetLastError.GetLastError
        ///     returns ERROR_NOT_ENOUGH_QUOTA when the limit is hit.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PostMessage([In] IntPtr hWnd, [In] WindowsMessage msg, [In] uint wParam,
            [In] uint lParam);

        /// <summary>
        ///     Places (posts) a message in the message queue associated with the thread that created the specified window and
        ///     returns without waiting for the thread to process the message.
        ///     To post a message in the message queue associated with a thread, use the PostThreadMessage function.
        /// </summary>
        /// <param name="hWnd">
        ///     A handle to the window whose window procedure is to receive the message. The following values have special
        ///     meanings.
        ///     HWND_BROADCAST ((HWND)0xffff): The message is posted to all top-level windows in the system, including disabled or
        ///     invisible unowned windows, overlapped windows, and pop-up windows. The message is not posted to child windows.
        ///     NULL: The function behaves like a call to PostThreadMessage with the dwThreadId parameter set to the identifier of
        ///     the current thread.
        /// </param>
        /// <param name="msg">
        ///     The message to be posted.
        /// </param>
        /// <param name="wParam">
        ///     Additional message-specific information.
        /// </param>
        /// <param name="lParam">
        ///     Additional message-specific information.
        /// </param>
        /// <returns>
        ///     If the function succeeds, the return value is nonzero.
        ///     If the function fails, the return value is zero.To get extended error information, call GetLastError.GetLastError
        ///     returns ERROR_NOT_ENOUGH_QUOTA when the limit is hit.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PostMessage([In] IntPtr hWnd, [In] WindowsMessage msg, [In] ulong wParam,
            [In] uint lParam);
    }
}
