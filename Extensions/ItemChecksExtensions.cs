using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace DBT.Extensions
{
    public static class ItemChecksExtensions
    {
        public static List<T> GetItemsInInventory<T>(this Player player, bool inventory = false, bool armor = false, bool accessory = false) where T : class
        {
            List<T> items = new List<T>();

            if (inventory)
                foreach (Item item in player.inventory)
                    if (item != null && item.modItem != null && item.IsOfType<T>())
                        items.Add(item.modItem as T);

            return items;
        }

        public static bool IsOfType<T>(this Item item) where T : class => item.modItem.GetType().IsAssignableFrom();
    }
}