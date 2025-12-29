using System;
using System.Runtime.InteropServices;

namespace BrigitWindow.Bridge 
{
    public static class AviUtlBridge 
    {
        [UnmanagedCallersOnly(EntryPoint = "func_init")]
        public static int FuncInit(IntPtr dllHinst) 
        {
            return 1;
        }
    }
}