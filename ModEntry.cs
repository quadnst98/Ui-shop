using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Menus;

namespace MobilePierreShopButton;

/// <summary>
/// SMAPI entry point. Adds a HUD button that opens Pierre's shop during business hours.
/// </summary>
public sealed class ModEntry : Mod
{
    // Business hours for Pierre's shop.
    private const int OpenTime = 900;
    private const int CloseTime = 1700;

    // Size/layout values for the HUD button.
    private const int BaseButtonSize = 64;
    private const int ButtonMargin = 16;

    // A small coin icon from the game's cursor sheet.
    private static readonly Rectangle IconSourceRect = new(193, 373, 9, 9);

    // Cached bounds from the latest render pass (used by click handling).
    private Rectangle buttonBounds;

    public override void Entry(IModHelper helper)
    {
        // Render the button as part of the HUD.
        helper.Events.Display.RenderedHud += this.OnRenderedHud;

        // Detect taps/clicks and open the shop when pressed.
        helper.Events.Input.ButtonPressed += this.OnButtonPressed;
    }

    /// <summary>
    /// Draw the shop button in the top-right corner of the HUD.
    /// </summary>
    private void OnRenderedHud(object? sender, RenderedHudEventArgs e)
    {
        // Only draw when a save is loaded and gameplay HUD is visible.
        if (!Context.IsWorldReady || Game1.activeClickableMenu is not null || Game1.eventUp)
            return;

        int scaledSize = (int)(BaseButtonSize * Game1.pixelZoom / 4f);
        int x = Game1.uiViewport.Width - scaledSize - ButtonMargin;
        int y = ButtonMargin;
        this.buttonBounds = new Rectangle(x, y, scaledSize, scaledSize);

        SpriteBatch spriteBatch = e.SpriteBatch;

        // Button background.
        spriteBatch.Draw(Game1.staminaRect, this.buttonBounds, new Color(25, 25, 25, 180));

        // Icon centered inside the button.
        int iconSize = (int)(scaledSize * 0.6f);
        Rectangle iconDestination = new(
            x + (scaledSize - iconSize) / 2,
            y + (scaledSize - iconSize) / 2,
            iconSize,
            iconSize);

        spriteBatch.Draw(Game1.mouseCursors, iconDestination, IconSourceRect, Color.White);
    }

    /// <summary>
    /// Handle touch/click input and open Pierre's shop if button is pressed in valid hours.
    /// </summary>
    private void OnButtonPressed(object? sender, ButtonPressedEventArgs e)
    {
        if (!Context.IsWorldReady)
            return;

        // Treat left-click/tap as the button press for mobile touch interaction.
        if (e.Button != SButton.MouseLeft)
            return;

        if (Game1.activeClickableMenu is not null || Game1.eventUp)
            return;

        Point clickPoint = e.Cursor.ScreenPixels;
        if (!this.buttonBounds.Contains(clickPoint))
            return;

        if (Game1.timeOfDay >= OpenTime && Game1.timeOfDay <= CloseTime)
        {
            // Open Pierre's vanilla shop menu.
            Game1.activeClickableMenu = Utility.getShopMenu("Pierre");
        }
        else
        {
            // Inform the player when the shop is closed.
            Game1.showRedMessage("Pierre's shop is closed.");
        }
    }
}
