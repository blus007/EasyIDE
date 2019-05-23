![](Doc/EasyIDE.png)

关于EasyIDE
-----

EasyIDE 是一款免费的软件，可以运行在 Windows 和 Mac系统上。\
目前最主要的功能是远程调试 Lua，可调试平台包括 Windows，MacOS，iOS，Android。

范例
-----
在 /ForXLua53/Sample/Unity 目录下有一份针对xLua调试的范例。
- 在菜单中选择 Language->简体中文，把语言设置成中文
- 打开 /Editor/Win64/EasyIDE.exe (Mac系统请打开 /Editor/Mac/EasyIDE.app)
- 在菜单中选择 文件->打开->工程 找到并选择 /ForXLua53/Sample/Unity/DebugForPC.eproj
  (如果是调试设备包可以使用 DebugForDevice.eproj，因为PC端和设备包的文件路径不一样，
  所以做了两份配置)
- 在菜单中选择 工程->启动调试服务器，在左下角会显示 调试端口: 3777 (等待连接)，这就表示
  调试器已经准备就绪
- 在 Main.lua 的第 9 行设置断点，可以点击行号右侧的空白区域，或则使用F9设置断点
- 用 Unity 打开工程目录 /ForXLua53/Sample/Unity
- 选择 Assets->Scenes->xLuaHookScene.unity 打开场景
- 运行场景，此时就会有中断，断点停留在 Main.lua 的第 9 行，快捷键与Visual Studio类似


联系方式
-----
QQ号码:1264508414\
E-Mail:1264508414@qq.com

结尾
-----
如果你有什么疑问都可以联系我，或则给我留言。
