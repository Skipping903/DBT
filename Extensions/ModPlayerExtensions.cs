using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace DBT.Extensions
{
    public static class ModPlayerExtensions
    {
        public static List<T> GetItemsInInventory<T>(this ModPlayer modPlayer) where T : class => GetItemInSlots<T>(modPlayer, modPlayer.player.inventory);

        public static List<T> GetItemsInArmor<T>(this ModPlayer modPlayer, bool includeVanity = false) where T : class
        {
            Item[] slots = null;

            if (includeVanity)
                slots = modPlayer.player.armor;
            else
            {
                slots = new Item[8 + modPlayer.player.extraAccessorySlots];

                for (int i = 3; i < 8 + modPlayer.player.extraAccessorySlots; i++)
                    slots[i - 3] = modPlayer.player.armor[i];
            }

            return GetItemInSlots<T>(modPlayer, slots, includeVanity);
        }

        public static List<T> GetItemInSlots<T>(this ModPlayer modPlayer, Item[] slots) where T : class
        {
            List<T> itemsFound = new List<T>();

            foreach (Item item in slots)
            {
                bool
                    x = typeof(T).IsSubclassOf(typeof(ModItem)),
                    y = item.modItem != null,
                    z = false;

                if (y)
                    z = item.modItem.GetType().IsSubclassOf(typeof(T));

                if (x && y && z)
                    itemsFound.Add(item as T);
                else if (item.GetType().IsSubclassOf(typeof(T)))
                    itemsFound.Add(item as T);
            }

            return itemsFound;
        }
    }
}
