﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace BinaryTechnologies.Items
{
    class MegabyteShard : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.Lime;
            Item.value = 800000;
            Item.maxStack = 999;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<KilobyteShard>(), 8);
            recipe.AddIngredient(ItemID.Wire, 10);
            recipe.AddIngredient(ItemID.SpectreBar, 1);
            recipe.AddTile(ModContent.TileType<Tiles.TilePC>());
            recipe.Register();
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

    }
}
