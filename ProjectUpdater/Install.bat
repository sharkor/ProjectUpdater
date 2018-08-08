%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\installutil.exe ProjectUpdater.exe
Net Start ProjectUpdater
sc config ProjectUpdater start= auto
pause