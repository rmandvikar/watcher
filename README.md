watcher
===========

A utility to mimic a directory or directories by watching for change events using FileSystemWatcher.

### Example

Add the directory (or directories) to mimic in config.
```
<add key="watch1" value="D:\RM\watcher\source\ -> D:\RM\watcher\target\" />
<add key="watch2" value="D:\RM\watcher\source2\ -> D:\RM\watcher\target2\" />
...
```

Run as windows service.
```
install command:
"C:\Windows\Microsoft.NET\Framework\v4.0.30319\installutil.exe" "D:\workspace\watcher\build\rm.WatcherWindowsService.exe"
```