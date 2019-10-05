using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace ConsoleExperiment.Utils
{
    static class WindowsClipboard
    {
        public static void SetText(string text)
        {
            OpenClipboard();

            EmptyClipboard();
            IntPtr hGlobal = default;
            try
            {
                var bytes = (text.Length + 1) * 2;
                hGlobal = Marshal.AllocHGlobal(bytes);

                if (hGlobal == default)
                {
                    ThrowWin32();
                }

                var target = GlobalLock(hGlobal);

                if (target == default)
                {
                    ThrowWin32();
                }

                try
                {
                    Marshal.Copy(text.ToCharArray(), 0, target, text.Length);
                }
                finally
                {
                    GlobalUnlock(target);
                }

                if (SetClipboardData(cfUnicodeText, hGlobal) == default)
                {
                    ThrowWin32();
                }

                hGlobal = default;
            }
            finally
            {
                if (hGlobal != default)
                {
                    Marshal.FreeHGlobal(hGlobal);
                }

                CloseClipboard();
            }
        }

        public static void OpenClipboard()
        {
            var num = 10;
            while (true)
            {
                if (OpenClipboard(default))
                {
                    break;
                }

                if (--num == 0)
                {
                    ThrowWin32();
                }

                Thread.Sleep(100);
            }
        }

        const uint cfUnicodeText = 13;

        static void ThrowWin32()
        {
            throw new Win32Exception(Marshal.GetLastWin32Error());
        }
        public const byte VK_LSHIFT = 0xA0; // left shift key
        public const byte VK_TAB = 0x09;
        public const int KEYEVENTF_EXTENDEDKEY = 0x01;
        public const int KEYEVENTF_KEYUP = 0x02;
        public const byte CONTROL = 0x11;
        public const byte KEY_V = 0x56;
        /// <summary>
        /// http://pinvoke.net/default.aspx/user32.keybd_event
        /// </summary>
        public static void SendKeys()
        {
            //press the control key
            keybd_event(CONTROL, 0x45, 0, 0);

            //press the v key
            keybd_event(KEY_V, 0x45, 0, 0);

            //release the v key
            keybd_event(KEY_V, 0x45, KEYEVENTF_KEYUP, 0);

            //release the control key
            keybd_event(CONTROL, 0x45, KEYEVENTF_KEYUP, 0);
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GlobalLock(IntPtr hMem);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GlobalUnlock(IntPtr hMem);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool OpenClipboard(IntPtr hWndNewOwner);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool CloseClipboard();

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr SetClipboardData(uint uFormat, IntPtr data);

        [DllImport("user32.dll")]
        static extern bool EmptyClipboard();

        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);
    }
}
