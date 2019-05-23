/*
 * Debugger for xlua
*/

namespace XLua
{

    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    #if USE_UNI_LUA
    #error cannot debug in UniLua
    #endif

    public class LuaHook
    {
        public static void OpenDebug(IntPtr L)
        {
            LuaDLL.LuaHook.luaopen_EasyDebugNative(L);
        }
    }
}
