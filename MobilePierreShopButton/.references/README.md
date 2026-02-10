# Reference assemblies (not committed as binaries)

This folder is intentionally text-only in git.

To compile locally, copy these DLLs from https://github.com/StardewModders/mod-reference-assemblies into this folder:

- `StardewModdingAPI.dll`
- `Stardew Valley.dll`
- `StardewValley.GameData.dll`
- `MonoGame.Framework.dll`
- `xTile.dll`
- `0Harmony.dll`
- `Newtonsoft.Json.dll`

Then build:

```bash
dotnet build MobilePierreShopButton/MobilePierreShopButton.csproj -c Release
```
