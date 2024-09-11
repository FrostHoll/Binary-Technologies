using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace BinaryTechnologies.Systems
{
    public class BinaryTechRecipes : ModSystem
    {
        public override void AddRecipes()
        {
            Recipe.Create(ItemID.Wire, 5)
                .AddIngredient(ItemID.CopperBar, 2)
                .AddIngredient(ItemID.Silk)
                .AddTile(TileID.WorkBenches)
                .Register();

            Recipe.Create(ItemID.Wire, 5)
                .AddIngredient(ItemID.TinBar, 2)
                .AddIngredient(ItemID.Silk)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}
