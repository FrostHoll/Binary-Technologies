using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace MyTestMod.Items
{
    class KilobyteShard : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Kilobyte Shard");
            Tooltip.SetDefault("You feel that this shard is full of knowledge about this world...");
            ItemID.Sets.ItemIconPulse[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.Pink;
            Item.value = Item.sellPrice(silver: 10);
            Item.maxStack = 999;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Items.ByteShard>(), 8);
            recipe.AddIngredient(ItemID.Wire, 10);
            recipe.AddIngredient(ItemID.HallowedBar, 1);
            recipe.AddTile(ModContent.TileType<Tiles.TilePC>());
            recipe.Register();
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

    }
}
