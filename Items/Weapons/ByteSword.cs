using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BinaryTechnologies.Items.Weapons
{
    class ByteSword : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 60;
            Item.DamageType = DamageClass.Melee;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 3.5f;
            Item.scale = 1.2f;
            Item.value = 18000;
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.EightProjectile>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<BitSword>(), 1);
            recipe.AddIngredient(ModContent.ItemType<ByteUpgradeModule>(), 1);
            recipe.AddTile(ModContent.TileType<Tiles.TilePC>());
            recipe.Register();
        }
    }
}
