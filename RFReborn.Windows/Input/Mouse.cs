using System;
using System.Threading;
using RFReborn.Windows.Native;
using RFReborn.Windows.Native.Enums;

namespace RFReborn.Windows.Input
{
    /// <summary>
    /// Class to manipulate and generate mouse input.
    /// </summary>
    public static class Mouse
    {
        /// <summary>
        ///     Posts a WM_LBUTTONDOWN followed by a WM_LBUTTONUP to a window, at the specified x- and y-
        ///     coordinates.
        /// </summary>
        /// <param name="hWnd">
        ///     Handle to the window.
        /// </param>
        /// <param name="xPos">
        ///     x-coordinate
        /// </param>
        /// <param name="yPos">
        ///     y-coordinate
        /// </param>
        /// <param name="delay">
        ///     Delay between WM_LBUTTONDOWN and WM_LBUTTONUP
        /// </param>
        public static void PostMessageLButtonClick(IntPtr hWnd, int xPos, int yPos, int delay = 5)
        {
            var lParam = (uint)((yPos << 16) | xPos);

            User32.PostMessage(hWnd, WindowsMessage.WM_LBUTTONDOWN, (uint)MouseKey.MK_LBUTTON, lParam);

            Thread.Sleep(delay);

            User32.PostMessage(hWnd, WindowsMessage.WM_LBUTTONUP, 0, lParam);
        }

        /// <summary>
        ///     Posts a WM_LBUTTONDOWN to a window, at the specified coordinates.
        /// </summary>
        /// <param name="hWnd">
        ///     Handle to the window.
        /// </param>
        /// <param name="xPos">
        ///     x-coordinate
        /// </param>
        /// <param name="yPos">
        ///     y-coordinate
        /// </param>
        public static void PostMessageLButtonDown(IntPtr hWnd, int xPos, int yPos)
        {
            var lParam = (uint)((yPos << 16) | xPos);

            User32.PostMessage(hWnd, WindowsMessage.WM_LBUTTONDOWN, (uint)MouseKey.MK_LBUTTON, lParam);
        }

        /// <summary>
        ///     Posts a WM_LBUTTONUP to a window, at the specified coordinates.
        /// </summary>
        /// <param name="hWnd">
        ///     Handle to the window.
        /// </param>
        /// <param name="xPos">
        ///     x-coordinate
        /// </param>
        /// <param name="yPos">
        ///     y-coordinate
        /// </param>
        public static void PostMessageLButtonUp(IntPtr hWnd, int xPos, int yPos)
        {
            var lParam = (uint)((yPos << 16) | xPos);

            User32.PostMessage(hWnd, WindowsMessage.WM_LBUTTONUP, 0, lParam);
        }

        /// <summary>
        ///     Posts a WM_LBUTTONDOWN, WM_LBUTTONUP, WM_LBUTTONDBLCLK and then WM_LBUTTONUP to a window, at the specified
        ///     coordinates.
        /// </summary>
        /// <param name="hWnd">
        ///     Handle to the window.
        /// </param>
        /// <param name="xPos">
        ///     x-coordinate
        /// </param>
        /// <param name="yPos">
        ///     y-coordinate
        /// </param>
        /// <param name="delay">
        ///     Delay between each PostMessage.
        /// </param>
        public static void PostMessageLButtonDoubleClick(IntPtr hWnd, int xPos, int yPos, int delay = 5)
        {
            var lParam = (uint)((yPos << 16) | xPos);

            User32.PostMessage(hWnd, WindowsMessage.WM_LBUTTONDOWN, (uint)MouseKey.MK_LBUTTON, lParam);
            Thread.Sleep(delay);
            User32.PostMessage(hWnd, WindowsMessage.WM_LBUTTONUP, 0, lParam);
            Thread.Sleep(delay);
            User32.PostMessage(hWnd, WindowsMessage.WM_LBUTTONDBLCLK, (uint)MouseKey.MK_LBUTTON, lParam);
            Thread.Sleep(delay);
            User32.PostMessage(hWnd, WindowsMessage.WM_LBUTTONUP, 0, lParam);
        }

        /// <summary>
        ///     Posts a WM_RBUTTONDOWN followed by a WM_RBUTTONUP to a window, at the specified coordinates.
        /// </summary>
        /// <param name="hWnd">
        ///     Handle to the window.
        /// </param>
        /// <param name="xPos">
        ///     x-coordinate
        /// </param>
        /// <param name="yPos">
        ///     y-coordinate
        /// </param>
        /// <param name="delay">
        ///     Delay between WM_RBUTTONDOWN and WM_RBUTTONUP
        /// </param>
        public static void PostMessageRButtonClick(IntPtr hWnd, int xPos, int yPos, int delay = 5)
        {
            var lParam = (uint)((yPos << 16) | xPos);

            User32.PostMessage(hWnd, WindowsMessage.WM_RBUTTONDOWN, (uint)MouseKey.MK_RBUTTON, lParam);

            Thread.Sleep(delay);

            User32.PostMessage(hWnd, WindowsMessage.WM_RBUTTONUP, 0, lParam);
        }

        /// <summary>
        ///     Posts a WM_RBUTTONDOWN, WM_RBUTTONUP, WM_RBUTTONDBLCLK and then WM_RBUTTONUP to a window, at the specified
        ///     coordinates.
        /// </summary>
        /// <param name="hWnd">
        ///     Handle to the window.
        /// </param>
        /// <param name="xPos">
        ///     x-coordinate
        /// </param>
        /// <param name="yPos">
        ///     y-coordinate
        /// </param>
        /// <param name="delay">
        ///     Delay between each PostMessage.
        /// </param>
        public static void PostMessageRButtonDoubleClick(IntPtr hWnd, int xPos, int yPos, int delay = 5)
        {
            var lParam = (uint)((yPos << 16) | xPos);

            User32.PostMessage(hWnd, WindowsMessage.WM_RBUTTONDOWN, (uint)MouseKey.MK_LBUTTON, lParam);
            Thread.Sleep(delay);
            User32.PostMessage(hWnd, WindowsMessage.WM_RBUTTONUP, 0, lParam);
            Thread.Sleep(delay);
            User32.PostMessage(hWnd, WindowsMessage.WM_RBUTTONDBLCLK, (uint)MouseKey.MK_LBUTTON, lParam);
            Thread.Sleep(delay);
            User32.PostMessage(hWnd, WindowsMessage.WM_RBUTTONUP, 0, lParam);
        }

        /// <summary>
        ///     Posts a WM_RBUTTONDOWN to a window, at the specified coordinates.
        /// </summary>
        /// <param name="hWnd">
        ///     Handle to the window.
        /// </param>
        /// <param name="xPos">
        ///     x-coordinate
        /// </param>
        /// <param name="yPos">
        ///     y-coordinate
        /// </param>
        public static void PostMessageRButtonDown(IntPtr hWnd, int xPos, int yPos)
        {
            var lParam = (uint)((yPos << 16) | xPos);

            User32.PostMessage(hWnd, WindowsMessage.WM_RBUTTONDOWN, (uint)MouseKey.MK_RBUTTON, lParam);
        }

        /// <summary>
        ///     Posts a WM_RBUTTONUP to a window, at the specified coordinates.
        /// </summary>
        /// <param name="hWnd">
        ///     Handle to the window.
        /// </param>
        /// <param name="xPos">
        ///     x-coordinate
        /// </param>
        /// <param name="yPos">
        ///     y-coordinate
        /// </param>
        public static void PostMessageRButtonUp(IntPtr hWnd, int xPos, int yPos)
        {
            var lParam = (uint)((yPos << 16) | xPos);

            User32.PostMessage(hWnd, WindowsMessage.WM_RBUTTONUP, 0, lParam);
        }
    }
}
