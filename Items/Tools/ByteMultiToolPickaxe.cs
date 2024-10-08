﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace BinaryTechnologies.Items.Tools
{
	public class ByteMultiToolPickaxe : ModItem
	{
        public override void SetDefaults()
		{
			Item.damage = 12;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 18;
			Item.useAnimation = 23;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 2f;
			Item.value = 18000;
			Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundID.Item1;
			Item.pick = 100;
			Item.autoReuse = true;
			Item.useTurn = true;
		}

        public override void AddRecipes()
        {
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<BitMultiToolPickaxe>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ByteUpgradeModule>(), 1);
			recipe.AddTile(ModContent.TileType<Tiles.TilePC>());
			recipe.Register();
        }

        public override bool CanRightClick()
		{
			return true;
		}

		public override void RightClick(Player player)
        {
			player.QuickSpawnItem(Item.GetSource_GiftOrReward(), ModContent.ItemType<ByteMultiToolHamaxe>());
        }

		public override bool? PrefixChance(int pre, UnifiedRandom rand) => (pre != -3 && pre != -1);
	}
}