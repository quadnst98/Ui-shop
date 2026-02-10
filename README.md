# Mobile Pierre Shop Button (SMAPI)

Adds a shop icon button to the HUD. Tapping/clicking the button opens Pierre's shop when the in-game time is between **09:00 and 17:00**. Outside these hours, the mod shows:

> Pierre's shop is closed.

## Files

- `ModEntry.cs` — main SMAPI mod logic (event hookup, HUD drawing, touch/click handling, time checks, shop open behavior).
- `manifest.json` — SMAPI manifest.
- `MobilePierreShopButton.csproj` and `Mod.csproj` — .NET 6 project files configured for SMAPI mod builds.

## Download
download the mod from this link
https://github.com/quadnst98/Ui-shop/releases/download/Build/Ui-shop-.zip

## Notes

- Uses `RenderedHud` to draw the button in the top-right corner.
- Uses `Input.ButtonPressed` and cursor coordinates for tap detection.
- Opens the vanilla shop menu via `Utility.getShopMenu("Pierre")`.
