using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using XLua;

public class MainScript : MonoBehaviour
{
    private List<string> mSearchPaths = new List<string>();

    private string GetFullPath(ref string filename)
    {
#if UNITY_EDITOR
        string relativePath = filename.Replace('.', '/');
        foreach(string p in mSearchPaths)
        {
            string fullPath = p + relativePath + ".lua";
            if (System.IO.File.Exists(fullPath))
                return fullPath;
        }
        return relativePath + ".lua";
#else
        return "LuaScripts/" + filename + ".lua";
#endif
    }

    private byte[] GetDecodeContent(ref string filename)
    {
#if UNITY_EDITOR
        string path = GetFullPath(ref filename);
        string content = File.ReadAllText(path);
        return System.Text.Encoding.UTF8.GetBytes(content);
#else
        string path = GetFullPath(ref filename);
        TextAsset content = Resources.Load<TextAsset>(path);
        return System.Text.Encoding.UTF8.GetBytes(content.text);
#endif
    }

    public void AutoAddSearchPath(string path)
    {
        mSearchPaths.Add(path + "/");
        string[] children = Directory.GetDirectories(path);
        if (children.Length > 0)
        {
            for (int i = 0; i < children.Length; ++i)
            {
                AutoAddSearchPath(children[i]);
            }
        }
    }

    void Start()
    {
        mState = new XLua.LuaEnv();

        // xLuaHook important ------------------
        // open xlua debug hook library
        XLua.LuaHook.OpenDebug(mState.rawL);

        mState.AddLoader(GetDecodeContent);
#if UNITY_EDITOR
        AutoAddSearchPath(Application.dataPath + "/LuaScripts");

        // xLuaHook important ------------------
        // add xlua debug hook path
        AutoAddSearchPath(Application.dataPath + "/XLuaHook/LuaScripts");
#endif

        mState.DoString("require(\"Main\")");
        mLuaUpdate = mState.Global.Get<LuaUpdateDelegate>("GlobalUpdate");
        mLuaInit = mState.Global.Get<LuaInitDelegate>("GlobalInit");
        mLuaDestroy = mState.Global.Get<LuaDestroyDelegate>("GlobalDestroy");

        mLuaInit();
    }

    void Update()
    {
        if (mState != null)
        {
            mLuaUpdate(0);
            mState.Tick();
        }
    }

    private void ReleaseDelegate()
    {
        if (mLuaDestroy != null)
        {
            mLuaDestroy();
            mLuaDestroy = null;
        }
        mLuaUpdate = null;
        mLuaInit = null;
    }

    void OnDestroy()
    {
        if (mState != null)
        {
            ReleaseDelegate();
            mState.Dispose();
            mState = null;
        }
    }

    private XLua.LuaEnv mState = null;
    private LuaUpdateDelegate mLuaUpdate = null;
    private LuaInitDelegate mLuaInit = null;
    private LuaDestroyDelegate mLuaDestroy = null;

    [XLua.CSharpCallLua]
    public delegate void LuaUpdateDelegate(float deltaTime);

    [XLua.CSharpCallLua]
    public delegate void LuaInitDelegate();

    [XLua.CSharpCallLua]
    public delegate void LuaDestroyDelegate();
}
