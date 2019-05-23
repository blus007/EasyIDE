using System;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using UnityEditor;

public class BuildMenu
{
    [MenuItem("Build/Generate lua to Resources")]
    private static void GenerateLuaToResources()
    {
        UnityEngine.Debug.Log("Start Generate Lua");
        string outputPath = Application.dataPath + "/Resources/LuaScripts";
        if (!Directory.Exists(outputPath))
        {
            Directory.CreateDirectory(outputPath);
        }
        else
        {
            string[] oldFiles = Directory.GetFiles(outputPath, "*.lua.txt", SearchOption.AllDirectories);
            for (int i = 0; i < oldFiles.Length; ++i)
            {
                string oldFile = oldFiles[i];
                File.Delete(oldFile);
            }
        }
        CopyLuaFiles(Application.dataPath + "/LuaScripts", outputPath);
        CopyLuaFiles(Application.dataPath + "/XLuaHook/LuaScripts", outputPath);
        string[] files = Directory.GetFiles(outputPath, "*.lua.txt.meta", SearchOption.AllDirectories);
        int metaPostLen = ".meta".Length;
        for (int i = 0; i < files.Length; ++i)
        {
            string metafile = files[i];
            string file = metafile.Substring(0, metafile.Length - metaPostLen);
            if (!File.Exists(file))
            {
                File.Delete(metafile);
            }
        }
        UnityEngine.Debug.Log("End Generate Lua");
    }

    private static void CopyLuaFiles(string inputPath, string outputPath)
    {
        string[] files = Directory.GetFiles(inputPath, "*.lua", SearchOption.AllDirectories);
        for (int i = 0; i < files.Length; ++i)
        {
            string file = files[i];
            FileInfo fileinfo = new FileInfo(file);
            string newFile = outputPath + "/" + fileinfo.Name + ".txt";
            if (File.Exists(newFile))
            {
                UnityEngine.Debug.LogErrorFormat("Name %s is duplicated!");
            }
            File.Copy(file, newFile);
        }
    }
}