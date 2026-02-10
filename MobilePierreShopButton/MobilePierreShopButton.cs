using System;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.BellsAndWhistles;
using StardewValley.Menus;

namespace MobilePierreShopButton;

public sealed class MobilePierreShopButton : Mod
{
    private readonly int buttonWidth = 240;
    private readonly int buttonHeight = 84;

    public override void Entry(IModHelper helper)
    {
        helper.Events.Display.RenderedHud += this.OnRenderedHud;
        helper.Events.Input.ButtonPressed += this.OnButtonPressed;
    }

    private void OnRenderedHud(object? sender, RenderedHudEventArgs e)
    {
        if (!Context.IsWorldReady || Game1.eventUp || Game1.activeClickableMenu != null)
            return;

        Rectangle bounds = this.GetButtonBounds();
        SpriteBatch b = e.SpriteBatch;

        IClickableMenu.drawTextureBox(b, bounds.X, bounds.Y, bounds.Width, bounds.Height, Color.White);
        SpriteText.drawStringHorizontallyCenteredAt(b, "Pierre", bounds.Center.X, bounds.Y + (bounds.Height / 2) - 20);
    }

    private void OnButtonPressed(object? sender, ButtonPressedEventArgs e)
    {
        if (!Context.IsWorldReady || !e.Button.IsActionButton())
            return;

        if (Game1.activeClickableMenu != null || Game1.eventUp || !Context.IsPlayerFree)
            return;

        if (!this.GetButtonBounds().Contains(e.Cursor.ScreenPixels))
            return;

        if (Game1.timeOfDay >= 900 && Game1.timeOfDay < 1700 && this.TryOpenPierreShop())
            return;

        Game1.addHUDMessage(new HUDMessage("Pierre is closed", HUDMessage.error_type));
    }

    private bool TryOpenPierreShop()
    {
        IReflectedMethod? getShopStock = this.Helper.Reflection.GetMethod(typeof(Utility), "getShopStock", required: false);
        if (getShopStock is null)
            return false;

        object? stock = getShopStock.Invoke<object>("Pierre");
        if (stock is null)
            return false;

        foreach (ConstructorInfo ctor in typeof(ShopMenu).GetConstructors())
        {
            ParameterInfo[] parameters = ctor.GetParameters();
            if (parameters.Length == 0 || !parameters[0].ParameterType.IsInstanceOfType(stock))
                continue;

            object?[] args = new object?[parameters.Length];
            args[0] = stock;

            for (int i = 1; i < parameters.Length; i++)
                args[i] = this.GetDefaultValue(parameters[i]);

            if (ctor.Invoke(args) is ShopMenu shopMenu)
            {
                Game1.activeClickableMenu = shopMenu;
                return true;
            }
        }

        return false;
    }

    private object? GetDefaultValue(ParameterInfo parameter)
    {
        if (parameter.HasDefaultValue)
            return parameter.DefaultValue;

        if (parameter.ParameterType == typeof(string))
            return "Pierre";

        return parameter.ParameterType.IsValueType
            ? Activator.CreateInstance(parameter.ParameterType)
            : null;
    }

    private Rectangle GetButtonBounds()
    {
        var viewport = Game1.uiViewport;
        int x = viewport.Width / 2 - this.buttonWidth / 2;
        int y = viewport.Height / 2 - this.buttonHeight / 2;
        return new Rectangle(x, y, this.buttonWidth, this.buttonHeight);
    }
}
