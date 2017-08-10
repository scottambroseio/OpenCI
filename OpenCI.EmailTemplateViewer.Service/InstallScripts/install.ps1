Param(
	[Parameter(Mandatory=$true)]
	[string]$BinPath
);

If (-Not (Test-Path $BinPath)) {
	throw "$BinPath doesn't exist"
}
Else {
	sc.exe create OpenCIEmailTemplateViewer binPath= %1
	sc.exe start OpenCIEmailTemplateViewer
}