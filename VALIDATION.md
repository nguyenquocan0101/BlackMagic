# bb_monitor validation

## Completed checks

- `dotnet restore` succeeds for `net8.0-windows` and `win-x64`.
- `dotnet build` succeeds with 0 errors.
- `dotnet publish` produces `dist/bb_monitor.exe` and `dist/bb_monitor.dll`.
- `bb_monitor.dll` has assembly identity `bb_monitor, Version=1.0.0.0`.
- `bb_monitor.exe` and `hooking.dll` are PE x64 files.
- `dist/bb_monitor.deps.json` names `bb_monitor`, Tesseract 5.2.0, System.Management 9.0.5, and System.ServiceProcess.ServiceController 9.0.5.
- Required `input`, `tess`, x64, and x86 runtime assets are present in `dist`.
- `dist/hooking.dll` SHA-256 matches the original `CrackBlack/hooking.dll`.
- No `seb_window` or old `WinStatMonitor` identity remains in the copied source/project files.
- Original comparison binaries are preserved under `reference-original`; the source/package root uses only the `bb_monitor` identity.
- Metadata comparison against the original managed assembly found 45/45 matching non-compiler-generated types and matching declared method counts for every matched type.
- `tools/Validate-BbMonitor.ps1` passes all 20 non-invasive checks and exits successfully without launching the executable.

Run the validator from the project directory:

```powershell
pwsh .\tools\Validate-BbMonitor.ps1
```

## Deferred live check

The application was not launched automatically. Its startup path requires administrator/debug privileges, starts and terminates a process for WMI validation, injects `hooking.dll`, monitors processes, and may restart a service. Run the final package only in an authorized Windows test environment where those side effects are expected and controlled.

Use [RUNTIME-TEST-CHECKLIST.md](RUNTIME-TEST-CHECKLIST.md) to record the remaining authorized runtime evidence.
