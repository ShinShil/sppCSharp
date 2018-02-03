using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace spp_lr5
{
    class InputBlockerAsync
    {
        [DllImport("User32.Dll", EntryPoint = "PostMessageA")]
        private static extern bool PostMessage(IntPtr hWnd, uint msg, int wParam, int lParam);
        private const int VK_RETURN = 0x0D;
        private const int WM_KEYDOWN = 0x100;

        private bool isThreadTerminated = false;
        private bool isWaiting = false;
        private IntPtr threadHandle = IntPtr.Zero;
        private Thread thread;

        private AsyncCallback AsyncCallback;
        private IAsyncResult asyncResult;

        private IntPtr inputProcessStream;

        public InputBlockerAsync(AsyncCallback callback = null, IAsyncResult result = null)
        {
            asyncResult = result;
            AsyncCallback = callback;
            SetToDefault();
        }

        public void Wait()
        {
            if (!isWaiting)
            {
                thread.Start();
                isWaiting = true;
            }
        }

        public void Continue()
        {
            if (isWaiting)
            {
                if (threadHandle != IntPtr.Zero)
                {
                    isThreadTerminated = true;
                    PostMessage(inputProcessStream, WM_KEYDOWN, VK_RETURN, 0);
                    //PostMessage(threadHandle, WM_KEYDOWN, VK_RETURN, 0);
                    isWaiting = false;
                }
            }
        }

        private void SetToDefault()
        {
            threadHandle = IntPtr.Zero;
            isWaiting = false;
            isThreadTerminated = false;
            thread = new Thread(new ThreadStart(WaitingThread));
        }

        private void WaitingThread()
        {
            threadHandle = System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle;
            inputProcessStream = System.Diagnostics.Process.GetCurrentProcess().Handle;
            Console.ReadKey(true);
            if (!isThreadTerminated)
            {
                AsyncCallback?.Invoke(asyncResult);
            }
            isThreadTerminated = false;
        }
    }
}
