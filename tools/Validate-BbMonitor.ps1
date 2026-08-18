[CmdletBinding()]
param(
	[string]$OriginalRoot = (Join-Path (Split-Path -Parent $PSScriptRoot) '..\CrackBlack')
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$Root = Split-Path -Parent $PSScriptRoot
$Dist = Join-Path $Root 'dist'
$Failures = [System.Collections.Generic.List[string]]::new()

function Assert-Pass([bool]$Condition, [string]$Message) {
	if ($Condition) {
		Write-Host "PASS  $Message" -ForegroundColor Green
	} else {
		Write-Host "FAIL  $Message" -ForegroundColor Red
		$script:Failures.Add($Message)
	}
}

function Get-PeInfo([string]$Path) {
	$Bytes = [IO.File]::ReadAllBytes($Path)
	$PeOffset = [BitConverter]::ToInt32($Bytes, 0x3c)
	$OptionalHeader = $PeOffset + 24
	$Magic = [BitConverter]::ToUInt16($Bytes, $OptionalHeader)
	$DirectoryOffset = $OptionalHeader + $(if ($Magic -eq 0x20b) { 112 } else { 96 })
	[PSCustomObject]@{
		Machine = [BitConverter]::ToUInt16($Bytes, $PeOffset + 4)
		Magic = $Magic
		ClrRva = [BitConverter]::ToUInt32($Bytes, $DirectoryOffset + 14 * 8)
	}
}

function Normalize-TypeName([string]$Name) {
	if ($Name -eq 'ApplicationConfiguration') { return $Name }
	return ($Name -replace '^bb_monitor\.', 'seb_window.')
}

$Required = @(
	'bb_monitor.exe', 'bb_monitor.dll', 'bb_monitor.deps.json',
	'bb_monitor.runtimeconfig.json', 'hooking.dll',
	'input\answers.txt', 'tess\eng.traineddata', 'tess\Vietnamese.traineddata',
	'x64\tesseract50.dll', 'x64\leptonica-1.82.0.dll',
	'x86\tesseract50.dll', 'x86\leptonica-1.82.0.dll'
)
foreach ($RelativePath in $Required) {
	Assert-Pass (Test-Path -LiteralPath (Join-Path $Dist $RelativePath)) "required file: $RelativePath"
}

$ExeInfo = Get-PeInfo (Join-Path $Dist 'bb_monitor.exe')
$ManagedInfo = Get-PeInfo (Join-Path $Dist 'bb_monitor.dll')
$HookInfo = Get-PeInfo (Join-Path $Dist 'hooking.dll')
Assert-Pass ($ExeInfo.Machine -eq 0x8664 -and $ExeInfo.Magic -eq 0x20b -and $ExeInfo.ClrRva -eq 0) 'bb_monitor.exe is native x64 apphost'
Assert-Pass ($ManagedInfo.Machine -eq 0x8664 -and $ManagedInfo.Magic -eq 0x20b -and $ManagedInfo.ClrRva -ne 0) 'bb_monitor.dll is managed x64 assembly'
Assert-Pass ($HookInfo.Machine -eq 0x8664 -and $HookInfo.Magic -eq 0x20b -and $HookInfo.ClrRva -eq 0) 'hooking.dll is native x64 DLL'

$OriginalHook = Join-Path $OriginalRoot 'hooking.dll'
$NewHash = (Get-FileHash -Algorithm SHA256 -LiteralPath (Join-Path $Dist 'hooking.dll')).Hash
$OriginalHash = (Get-FileHash -Algorithm SHA256 -LiteralPath $OriginalHook).Hash
Assert-Pass ($NewHash -eq $OriginalHash) 'hooking.dll hash matches original'

$AssemblyName = [Reflection.AssemblyName]::GetAssemblyName((Join-Path $Dist 'bb_monitor.dll'))
Assert-Pass ($AssemblyName.Name -eq 'bb_monitor') 'managed assembly identity is bb_monitor'

$SourceFiles = Get-ChildItem -LiteralPath $Root -Recurse -File | Where-Object { $_.Extension -in @('.cs', '.csproj') -or $_.Name -eq 'AssemblyInfo.cs' }
$Stale = @($SourceFiles | Select-String -Pattern 'WinStatMonitor|seb_window')
Assert-Pass ($Stale.Count -eq 0) 'source has no stale WinStatMonitor/seb_window identity'

$OriginalAssembly = [Reflection.Assembly]::LoadFile((Join-Path $OriginalRoot 'WinStatMonitor.dll'))
$RebuiltAssembly = [Reflection.Assembly]::LoadFile((Join-Path $Dist 'bb_monitor.dll'))
$Flags = [Reflection.BindingFlags]'Instance,Static,Public,NonPublic,DeclaredOnly'
$OriginalTypes = @($OriginalAssembly.GetTypes() | Where-Object { $_.FullName -notmatch '(<|_003C)' } | ForEach-Object FullName)
$RebuiltTypes = @($RebuiltAssembly.GetTypes() | Where-Object { $_.FullName -notmatch '(<|_003C)' -and $_.FullName -ne 'ApplicationConfiguration' } | ForEach-Object { Normalize-TypeName $_.FullName })
$MissingTypes = @($OriginalTypes | Where-Object { $_ -notin $RebuiltTypes })
$ExtraTypes = @($RebuiltTypes | Where-Object { $_ -notin $OriginalTypes })
Assert-Pass ($MissingTypes.Count -eq 0 -and $ExtraTypes.Count -eq 0) "structural types match (original=$($OriginalTypes.Count), rebuilt=$($RebuiltTypes.Count))"

$MethodMismatches = 0
foreach ($OriginalTypeName in $OriginalTypes) {
	$RebuiltTypeName = $OriginalTypeName -replace '^seb_window\.', 'bb_monitor.'
	$OriginalType = $OriginalAssembly.GetType($OriginalTypeName)
	$RebuiltType = $RebuiltAssembly.GetType($RebuiltTypeName)
	if ($null -eq $OriginalType -or $null -eq $RebuiltType) { continue }
	$OriginalMethods = @($OriginalType.GetMethods($Flags))
	$RebuiltMethods = @($RebuiltType.GetMethods($Flags) | Where-Object {
		($RebuiltTypeName -ne 'bb_monitor.Forms.OracleForm' -and $RebuiltTypeName -ne 'bb_monitor.Program') -or ($RebuiltTypeName -eq 'bb_monitor.Forms.OracleForm' -and $_.Name -notin @('OnShown', 'ShowOverlay', 'get_OverlayRequested', 'get_IsTextDisplayReady') -and $_.Name -notlike '<ShowOverlay>*') -or ($RebuiltTypeName -eq 'bb_monitor.Program' -and $_.Name -ne 'WaitForFormReady')
	})
	if ($OriginalMethods.Count -ne $RebuiltMethods.Count) { $MethodMismatches++ }
}
Assert-Pass ($MethodMismatches -eq 0) "declared method counts match (mismatches=$MethodMismatches)"

if ($Failures.Count -gt 0) {
	Write-Host "`nValidation failed: $($Failures.Count) check(s)." -ForegroundColor Red
	exit 1
}

Write-Host "`nValidation passed. No executable was launched." -ForegroundColor Green
