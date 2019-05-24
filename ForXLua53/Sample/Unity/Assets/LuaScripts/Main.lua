-- xLuaHook important ------------------
-- connect to debugger here
local EasyDebug = require("EasyDebug")
-- if you wanna debug the device, 
-- modify the ip to the host where EasyIDE stays.
EasyDebug:Start("127.0.0.1", 3777) -- args are ip and port
EasyDebug:Print("Hello world!!!") -- print to debugger

CS.UnityEngine.Debug.Log('hello world') -- print to unity output

function EasyLog(message)
	EasyDebug:Print(message)
	CS.UnityEngine.Debug.Log(message)
end

require("Test")
TestPrint()

function GlobalInit()
	CS.UnityEngine.Debug.Log('call global init')
end

function GlobalDestroy()
    CS.UnityEngine.Debug.Log('call global destroy')
end

function GlobalUpdate(deltaTime)
    CS.UnityEngine.Debug.Log('call global update')
end