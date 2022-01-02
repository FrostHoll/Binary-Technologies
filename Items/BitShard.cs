using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyTestMod.Items
{
    class BitShard : ModItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bit Shard");
            Tooltip.SetDefault("This shard contains information of this world.");
        }

        public override void SetDefaults()
        {
            Item.width = 12;
            Item.height = 12;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(silver: 10);
            Item.maxStack = 999;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Wire, 10);
            recipe.AddRecipeGroup("IronBar", 3);
            recipe.AddTile(ModContent.TileType<Tiles.TilePC>());
            recipe.Register();
        }

    }
}
