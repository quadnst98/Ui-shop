# Releases

Binary release artifacts are not committed in this repository because PR binary files are not supported.

To create `MobilePierreShopButton-1.0.1.zip` locally:

1. Add reference assemblies to `MobilePierreShopButton/.references/` (see `MobilePierreShopButton/.references/README.md`).
2. Build:
   ```bash
   dotnet build MobilePierreShopButton/MobilePierreShopButton.csproj -c Release
   ```
3. Package:
   ```bash
   mkdir -p releases/MobilePierreShopButton-1.0.1
   cp MobilePierreShopButton/bin/Release/net6.0/MobilePierreShopButton.dll releases/MobilePierreShopButton-1.0.1/
   cp MobilePierreShopButton/manifest.json releases/MobilePierreShopButton-1.0.1/
   cd releases && zip -r MobilePierreShopButton-1.0.1.zip MobilePierreShopButton-1.0.1
   ```
