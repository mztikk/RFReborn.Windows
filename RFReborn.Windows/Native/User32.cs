using System;
using System.Runtime.InteropServices;
using RFReborn.Windows.Native.Enums;
using RFReborn.Windows.Native.Structs;

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

        /// <summary>
        ///     The mouse_event function synthesizes mouse motion and button clicks.
        /// </summary>
        /// <param name="dwFlags">
        ///     Controls various aspects of mouse motion and button clicking. This parameter can be certain combinations of
        ///     <see cref="MouseEventF" />.
        /// </param>
        /// <param name="dx">
        ///     The mouse's absolute position along the x-axis or its amount of motion since the last mouse event was generated,
        ///     depending on the setting of MOUSEEVENTF_ABSOLUTE. Absolute data is specified as the mouse's actual x-coordinate;
        ///     relative data is specified as the number of mickeys moved. A mickey is the amount that a mouse has to move for it
        ///     to report that it has moved.
        /// </param>
        /// <param name="dy">
        ///     The mouse's absolute position along the y-axis or its amount of motion since the last mouse event was generated,
        ///     depending on the setting of MOUSEEVENTF_ABSOLUTE. Absolute data is specified as the mouse's actual y-coordinate;
        ///     relative data is specified as the number of mickeys moved.
        /// </param>
        /// <param name="dwData">
        ///     If dwFlags contains MOUSEEVENTF_WHEEL, then dwData specifies the amount of wheel movement. A positive value
        ///     indicates that the wheel was rotated forward, away from the user; a negative value indicates that the wheel was
        ///     rotated backward, toward the user. One wheel click is defined as WHEEL_DELTA, which is 120.
        ///     If dwFlags contains MOUSEEVENTF_HWHEEL, then dwData specifies the amount of wheel movement.A positive value
        ///     indicates that the wheel was tilted to the right; a negative value indicates that the wheel was tilted to the left.
        ///     If dwFlags contains MOUSEEVENTF_XDOWN or MOUSEEVENTF_XUP, then dwData specifies which X buttons were pressed or
        ///     released. This value may be any combination of the following flags.
        ///     If dwFlags is not MOUSEEVENTF_WHEEL, MOUSEEVENTF_XDOWN, or MOUSEEVENTF_XUP, then dwData should be zero.
        /// </param>
        /// <param name="dwExtraInfo">
        ///     An additional value associated with the mouse event. An application calls GetMessageExtraInfo to obtain this extra
        ///     information.
        /// </param>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern void mouse_event([In] MouseEventF dwFlags, [In] int dx, [In] int dy, [In] uint dwData,
            [In] uint dwExtraInfo);

        /// <summary>
        ///     Moves the cursor to the specified screen coordinates. If the new coordinates are not within the screen rectangle
        ///     set by the most recent ClipCursor function call, the system automatically adjusts the coordinates so that the
        ///     cursor stays within the rectangle.
        /// </summary>
        /// <param name="x">
        ///     The new x-coordinate of the cursor, in screen coordinates.
        /// </param>
        /// <param name="y">
        ///     The new y-coordinate of the cursor, in screen coordinates.
        /// </param>
        /// <returns>
        ///     Returns nonzero if successful or zero otherwise. To get extended error information, call GetLastError.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetCursorPos([In] int x, [In] int y);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetCursorPos([Out] out POINT point);

        /// <summary>
        ///     Sets the position of the cursor in physical coordinates.
        /// </summary>
        /// <param name="x">
        ///     The new x-coordinate of the cursor, in physical coordinates.
        /// </param>
        /// <param name="y">
        ///     The new y-coordinate of the cursor, in physical coordinates.
        /// </param>
        /// <returns>
        ///     TRUE if successful; otherwise FALSE.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetPhysicalCursorPos([In] int x, [In] int y);

        /// <summary>
        /// Determines whether a key is up or down at the time the function is called, and whether the key was pressed after a previous call to GetAsyncKeyState
        /// </summary>
        /// <param name="vKey">
        /// The virtual-key code. For more information, see Virtual Key Codes.
        /// You can use left- and right-distinguishing constants to specify certain keys.See the Remarks section for further information.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value specifies whether the key was pressed since the last call to GetAsyncKeyState, and whether the key is currently up or down.
        /// If the most significant bit is set, the key is down, and if the least significant bit is set, the key was pressed after the previous call to GetAsyncKeyState.
        /// However, you should not rely on this last behavior; for more information, see the Remarks.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetAsyncKeyState([In] VirtualKeyCode vKey);

        /// <summary>
        ///     Synthesizes a keystroke. The system can use such a synthesized keystroke to generate a WM_KEYUP or WM_KEYDOWN
        ///     message. The keyboard driver's interrupt handler calls the keybd_event function.
        /// </summary>
        /// <param name="bVk">
        ///     A virtual-key code. The code must be a value in the range 1 to 254. For a complete list,
        ///     <see cref="VirtualKeyCode" />.
        /// </param>
        /// <param name="bScan">
        ///     A hardware scan code for the key.
        /// </param>
        /// <param name="dwFlags">
        ///     Controls various aspects of function operation. This parameter can be one or more of the following values.
        ///     <see cref="KeyEventF" />
        /// </param>
        /// <param name="dwExtraInfo">
        ///     An additional value associated with the key stroke.
        /// </param>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern void keybd_event([In] VirtualKeyCode bVk, [In] byte bScan, [In] KeyEventF dwFlags,
            [In] uint dwExtraInfo);

        /// <summary>
        ///     The set foreground window.
        /// </summary>
        /// <param name="hWnd">
        ///     The h wnd.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        /// <summary>
        /// Retrieves a handle to the foreground window (the window with which the user is currently working). The system assigns a slightly higher priority to the thread that creates the foreground window than it does to other threads.
        /// </summary>
        /// <returns>The return value is a handle to the foreground window. The foreground window can be NULL in certain circumstances, such as when a window is losing activation.</returns>
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        /// <summary>
        /// Retrieves the dimensions of the bounding rectangle of the specified window. The dimensions are given in screen coordinates that are relative to the upper-left corner of the screen.
        /// </summary>
        /// <param name="hWnd">A handle to the window.</param>
        /// <param name="rect">A pointer to a RECT structure that receives the screen coordinates of the upper-left and lower-right corners of the window.</param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero.
        /// If the function fails, the return value is zero.To get extended error information, call GetLastError.
        /// </returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect([In] IntPtr hWnd, [Out] out RECT rect);

        /// <summary>
        ///     Synthesizes keystrokes, mouse motions, and button clicks.
        /// </summary>
        /// <param name="nInputs">
        ///     The number of structures in the pInputs array.
        /// </param>
        /// <param name="pInputs">
        ///     An array of INPUT structures. Each structure represents an event to be inserted into the keyboard or mouse input
        ///     stream.
        /// </param>
        /// <param name="cbSize">
        ///     The size, in bytes, of an INPUT structure. If cbSize is not the size of an INPUT structure, the function fails.
        /// </param>
        /// <returns>
        ///     The function returns the number of events that it successfully inserted into the keyboard or mouse input stream. If
        ///     the function returns zero, the input was already blocked by another thread. To get extended error information, call
        ///     GetLastError.
        ///     This function fails when it is blocked by UIPI.Note that neither GetLastError nor the return value will indicate
        ///     the failure was caused by UIPI blocking.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint SendInput([In] uint nInputs, [MarshalAs(UnmanagedType.LPArray)] [In]
            INPUT[] pInputs, [In] int cbSize);

        /// <summary>
        /// Sets an event hook function for a range of events.
        /// </summary>
        /// <param name="eventMin">Specifies the event constant for the lowest event value in the range of events that are handled by the hook function. This parameter can be set to EVENT_MIN to indicate the lowest possible event value.</param>
        /// <param name="eventMax">Specifies the event constant for the highest event value in the range of events that are handled by the hook function. This parameter can be set to EVENT_MAX to indicate the highest possible event value.</param>
        /// <param name="hmodWinEventProc">Handle to the DLL that contains the hook function at lpfnWinEventProc, if the WINEVENT_INCONTEXT flag is specified in the dwFlags parameter. If the hook function is not located in a DLL, or if the WINEVENT_OUTOFCONTEXT flag is specified, this parameter is NULL.</param>
        /// <param name="lpfnWinEventProc">Pointer to the event hook function. For more information about this function, see WinEventProc.</param>
        /// <param name="idProcess">Specifies the ID of the process from which the hook function receives events. Specify zero (0) to receive events from all processes on the current desktop.</param>
        /// <param name="idThread">Specifies the ID of the thread from which the hook function receives events. If this parameter is zero, the hook function is associated with all existing threads on the current desktop.</param>
        /// <param name="dwFlags">Flag values that specify the location of the hook function and of the events to be skipped.</param>
        /// <returns>
        /// If successful, returns an HWINEVENTHOOK value that identifies this event hook instance. Applications save this return value to use it with the UnhookWinEvent function.
        /// If unsuccessful, returns zero.
        /// </returns>
        [DllImport("user32.dll")]
        public static extern IntPtr SetWinEventHook(EventConstant eventMin, EventConstant eventMax, IntPtr hmodWinEventProc, WinEventDelegate lpfnWinEventProc, uint idProcess, uint idThread, WinEvent dwFlags);

        /// <summary>
        /// An application-defined callback (or hook) function that the system calls in response to events generated by an accessible object. The hook function processes the event notifications as required. Clients install the hook function and request specific types of event notifications by calling SetWinEventHook.
        /// The WINEVENTPROC type defines a pointer to this callback function. WinEventProc is a placeholder for the application-defined function name.
        /// </summary>
        /// <param name="hWinEventHook">Handle to an event hook function. This value is returned by SetWinEventHook when the hook function is installed and is specific to each instance of the hook function.</param>
        /// <param name="eventType">Specifies the event that occurred. This value is one of the event constants.</param>
        /// <param name="hwnd">Handle to the window that generates the event, or NULL if no window is associated with the event. For example, the mouse pointer is not associated with a window.</param>
        /// <param name="idObject">Identifies the object associated with the event. This is one of the object identifiers or a custom object ID.</param>
        /// <param name="idChild">Identifies whether the event was triggered by an object or a child element of the object. If this value is CHILDID_SELF, the event was triggered by the object; otherwise, this value is the child ID of the element that triggered the event.</param>
        /// <param name="idEventThread"></param>
        /// <param name="dwmsEventTime">Specifies the time, in milliseconds, that the event was generated.</param>
        /// <remarks>
        /// Within the hook function, the parameters hwnd, idObject, and idChild are used when calling AccessibleObjectFromEvent.
        /// Servers generate events by calling NotifyWinEvent.
        /// Create multiple callback functions to handle different events.For more information, see Registering a Hook Function.
        ///</remarks>
        public delegate void WinEventDelegate(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint idEventThread, uint dwmsEventTime);


        /// <summary>
        /// Removes an event hook function created by a previous call to SetWinEventHook.
        /// </summary>
        /// <param name="hWinEventHook">Handle to the event hook returned in the previous call to SetWinEventHook.</param>
        /// <returns>
        /// If successful, returns TRUE; otherwise, returns FALSE.
        /// Three common errors cause this function to fail:
        /// The hWinEventHook parameter is NULL or not valid.
        /// The event hook specified by hWinEventHook was already removed.
        /// UnhookWinEvent is called from a thread that is different from the original call to SetWinEventHook.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnhookWinEvent(IntPtr hWinEventHook);

        /// <summary>
        /// Retrieves a message from the calling thread's message queue. The function dispatches incoming sent messages until a posted message is available for retrieval.
        /// </summary>
        /// <param name="lpMsg">A pointer to an MSG structure that receives message information from the thread's message queue.</param>
        /// <param name="hWnd">A handle to the window whose messages are to be retrieved. The window must belong to the current thread.
        ///If hWnd is NULL, GetMessage retrieves messages for any window that belongs to the current thread, and any messages on the current thread's message queue whose hwnd value is NULL (see the MSG structure). Therefore if hWnd is NULL, both window messages and thread messages are processed.
        ///If hWnd is -1, GetMessage retrieves only messages on the current thread's message queue whose hwnd value is NULL, that is, thread messages as posted by PostMessage (when the hWnd parameter is NULL) or PostThreadMessage.
        ///</param>
        /// <param name="wMsgFilterMin">The integer value of the lowest message value to be retrieved. Use WM_KEYFIRST (0x0100) to specify the first keyboard message or WM_MOUSEFIRST (0x0200) to specify the first mouse message.
        ///Use WM_INPUT here and in wMsgFilterMax to specify only the WM_INPUT messages.
        ///If wMsgFilterMin and wMsgFilterMax are both zero, GetMessage returns all available messages(that is, no range filtering is performed).
        ///</param>
        /// <param name="wMsgFilterMax">The integer value of the highest message value to be retrieved. Use WM_KEYLAST to specify the last keyboard message or WM_MOUSELAST to specify the last mouse message.
        ///Use WM_INPUT here and in wMsgFilterMin to specify only the WM_INPUT messages.
        ///If wMsgFilterMin and wMsgFilterMax are both zero, GetMessage returns all available messages(that is, no range filtering is performed).
        ///</param>
        /// <returns>If the function retrieves a message other than WM_QUIT, the return value is nonzero.
        ///If the function retrieves the WM_QUIT message, the return value is zero.
        ///If there is an error, the return value is -1. For example, the function fails if hWnd is an invalid window handle or lpMsg is an invalid pointer.To get extended error information, call GetLastError.
        ///</returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetMessage([Out] out MSG lpMsg, [In] IntPtr hWnd, [In] uint wMsgFilterMin, [In] uint wMsgFilterMax);


        /// <summary>
        /// Translates virtual-key messages into character messages. The character messages are posted to the calling thread's message queue, to be read the next time the thread calls the GetMessage or PeekMessage function.
        /// </summary>
        /// <param name="lpMsg">A pointer to an MSG structure that contains message information retrieved from the calling thread's message queue by using the GetMessage or PeekMessage function.</param>
        /// <returns>If the message is translated (that is, a character message is posted to the thread's message queue), the return value is nonzero.
        /// If the message is WM_KEYDOWN, WM_KEYUP, WM_SYSKEYDOWN, or WM_SYSKEYUP, the return value is nonzero, regardless of the translation.
        /// If the message is not translated (that is, a character message is not posted to the thread's message queue), the return value is zero.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool TranslateMessage([In] ref MSG lpMsg);

        /// <summary>
        /// Dispatches a message to a window procedure. It is typically used to dispatch a message retrieved by the GetMessage function.
        /// </summary>
        /// <param name="lpMsg">A pointer to a structure that contains the message.</param>
        /// <returns>The return value specifies the value returned by the window procedure. Although its meaning depends on the message being dispatched, the return value generally is ignored.</returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DispatchMessage([In] ref MSG lpMsg);


        /// <summary>
        /// Indicates to the system that a thread has made a request to terminate (quit). It is typically used in response to a WM_DESTROY message.
        /// </summary>
        /// <param name="nExitCode">The application exit code. This value is used as the wParam parameter of the WM_QUIT message.</param>
        /// <remarks>
        /// The PostQuitMessage function posts a WM_QUIT message to the thread's message queue and returns immediately; the function simply indicates to the system that the thread is requesting to quit at some time in the future.
        /// When the thread retrieves the WM_QUIT message from its message queue, it should exit its message loop and return control to the system.The exit value returned to the system must be the wParam parameter of the WM_QUIT message.
        /// </remarks>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern void PostQuitMessage([In] int nExitCode);
    }
}
