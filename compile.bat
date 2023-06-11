@dotnet publish -p:PublishSingleFile=true --self-contained false

copy bin\Debug\net7.0-windows\win-x64\publish\d3m0n_updater.exe d3m0n_updater.exe

@pause