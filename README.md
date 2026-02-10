# Mobile Pierre Shop Button (SMAPI)

Adds a shop icon button to the HUD. Tapping/clicking the button opens Pierre's shop when the in-game time is between **09:00 and 17:00**. Outside these hours, the mod shows:

> Pierre's shop is closed.

## Files

- `ModEntry.cs` — main SMAPI mod logic (event hookup, HUD drawing, touch/click handling, time checks, shop open behavior).
- `manifest.json` — SMAPI manifest.
- `MobilePierreShopButton.csproj` — .NET 5 project file configured for SMAPI mod builds.

## Build (Replit-friendly)

1. Put these files in one folder.
2. Restore/build with .NET 5 SDK:
   ```bash
   dotnet restore
   dotnet build -c Release
   ```
3. Copy output DLL and `manifest.json` to your Stardew Valley Mods folder.

## Notes

- Uses `RenderedHud` to draw the button in the top-right corner.
- Uses `Input.ButtonPressed` and cursor coordinates for tap detection.
- Opens the vanilla shop menu via `Utility.getShopMenu("Pierre")`.
