# Mobile Pierre Shop Button (SMAPI)

Adds a shop icon button to the HUD. Tapping/clicking the button opens Pierre's shop when the in-game time is between **09:00 and 17:00**. Outside these hours, the mod shows:

> Pierre's shop is closed.

## Files

- `ModEntry.cs` — main SMAPI mod logic (event hookup, HUD drawing, touch/click handling, time checks, shop open behavior).
- `manifest.json` — SMAPI manifest.
- `MobilePierreShopButton.csproj` — .NET 5 project file configured for SMAPI mod builds.



## Notes

- Uses `RenderedHud` to draw the button in the top-right corner.
- Uses `Input.ButtonPressed` and cursor coordinates for tap detection.
- Opens the vanilla shop menu via `Utility.getShopMenu("Pierre")`.
