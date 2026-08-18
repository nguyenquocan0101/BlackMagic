# Authorized runtime validation checklist

Run this checklist only on a disposable Windows VM or a machine where you are authorized to monitor the target software and restart its service. Do not use a production or examination environment.

## Before starting

- Record the Windows version, architecture, .NET Desktop Runtime version, and current service state.
- Take a VM snapshot or otherwise establish a rollback point.
- Confirm that `dist` is the package under test and that `hooking.dll` is the intended x64 binary.
- Keep Process Monitor/Event Viewer available for diagnostics.

## Behavior checklist

- [ ] Application startup and prerequisite status UI.
- [ ] Overlay window creation, topmost behavior, opacity, and desktop handling.
- [ ] Text loading from `input` and display scrolling/search behavior.
- [ ] Keyboard and mouse interactions.
- [ ] OCR selection flow with English and Vietnamese language data.
- [ ] Logging and quit-file behavior.
- [ ] Process/WMI monitoring and service behavior, only where explicitly authorized.
- [ ] Clean shutdown and restoration of the VM/service state.

## Evidence to record

- Screenshots or screen recordings of user-visible behavior.
- `text.txt` and any relevant Windows Event Viewer entries.
- Process tree and loaded-module observations from the test VM.
- A pass/fail result for each checklist item and any deviation from the original.

The static validator does not launch the application and cannot replace this authorized runtime test.
