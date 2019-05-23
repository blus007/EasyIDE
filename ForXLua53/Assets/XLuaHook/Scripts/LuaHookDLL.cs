/*
 * Debugger for xlua
*/

namespace XLua.LuaDLL
{

    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    public partial class LuaHook
	{
#if (UNITY_IPHONE || UNITY_WEBGL || UNITY_SWITCH) && !UNITY_EDITOR
        const string LUAHOOKDLL = "__Internal";
#else
        const string LUAHOOKDLL = "xluaHook";
#endif
        [DllImport(LUAHOOKDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr luaopen_EasyDebugNative(IntPtr L);
    }
}
