using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BinaryTechnologies.Items.Weapons
{
    class GigabyteSword : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 150;
            Item.DamageType = DamageClass.Melee;
            Item.width = 48;
            Item.height = 48;
            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 4.5f;
            Item.scale = 1f;
            Item.value = 1400000;
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.GigabyteSwordProjectile>();
            Item.shootSpeed = 11f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Weapons.MegabyteSword>(), 1);
            recipe.AddIngredient(ModContent.ItemType<Items.GigabyteUpgradeModule>(), 1);
            recipe.AddTile(ModContent.TileType<Tiles.TilePC>());
            recipe.Register();
        }
    }
}
