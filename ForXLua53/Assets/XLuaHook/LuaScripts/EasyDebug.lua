local EasyDebug = {
	nativeLib = nil,
	pathSet = false,
	useStatic = true,
}

function EasyDebug:SetDLLPath(dllDir, dllExt, dllPrefix)
	if self.pathSet then
		return
	end
	self.useStatic = false
	local dllPath
	if not string.match(dllDir, ".*[\\/]$") then
		dllDir = dllDir .. "/"
	end
	if #dllExt > 0 and not string.match(dllExt, "^%..*") then
		dllExt = "." .. dllExt
	end
	if dllPrefix and dllPrefix ~= "" then
		dllPath = dllDir .. dllPrefix .. "Hook" .. dllExt
	elseif _VERSION == "Lua 5.1" then
		dllPath = dllDir .. "Lua51Hook" .. dllExt
	elseif _VERSION == "Lua 5.2" then
		dllPath = dllDir .. "Lua52Hook" .. dllExt
	elseif _VERSION == "Lua 5.3" then
		dllPath = dllDir .. "Lua53Hook" .. dllExt
	end
	package.cpath = dllPath .. ";" .. package.cpath
	self.pathSet = true
end

function EasyDebug:Start(ip, port)
	if not self.useStatic and not self.pathSet then
		return false
	end
	if not self.nativeLib then
		self.nativeLib = require("EasyDebugNative")
		if not self.nativeLib then
			return false
		end
	end
	ip = ip or "127.0.0.1"
	port = port or 3777
	self.nativeLib.Start(ip, port)
	return true
end

function EasyDebug:Stop()
	if not self.nativeLib then
		return
	end
	self.nativeLib.Stop()
end

function EasyDebug:Print(message)
	if not self.nativeLib then
		return
	end
	self.nativeLib.Print(message)
end

setmetatable(EasyDebug, {
	__gc = function (obj)
		EasyDebug:Stop()
	end})

return EasyDebug