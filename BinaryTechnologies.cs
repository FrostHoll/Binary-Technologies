using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Microsoft.Xna.Framework;

namespace BinaryTechnologies
{
    public class BinaryTechnologies : Mod
    {
        public static BinaryTechnologies instance;

        public static ModKeybind temp;

        public static readonly string TransPath = "Mods.BinaryTechnologies.";

        public override void Load()
        {            
            instance = this;
            temp = KeybindLoader.RegisterKeybind(this, "Test Button", "P");
        }

        

        public override void AddRecipes()
        {
            var recipe = CreateRecipe(ItemID.Wire, 5);
            recipe.AddIngredient(ItemID.CopperBar, 2);
            recipe.AddIngredient(ItemID.Silk);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
            recipe = CreateRecipe(ItemID.Wire, 5);
            recipe.AddIngredient(ItemID.TinBar, 2);
            recipe.AddIngredient(ItemID.Silk);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }  
}