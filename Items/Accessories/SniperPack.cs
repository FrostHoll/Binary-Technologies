using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ID;

namespace BinaryTechnologies.Items.Accessories
{
	public class SniperPack : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Starter Sniper's Kit");
			Tooltip.SetDefault("8% increased ranged damage" +
				"\n4% increased ranged crit chance" +
				"\nRanged attacks have a small chance to confuse your foe");
		}

		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 16;
			Item.value = 65000;
			Item.rare = ItemRarityID.Pink;
			Item.maxStack = 1;
			Item.accessory = true;
			Item.expert = true;
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetDamage(DamageClass.Ranged) *= 1.08f;
			player.GetCritChance(DamageClass.Ranged) += 4;
			player.GetModPlayer<BinaryTechnologiesPlayer>().sniperPack = true;
		}
	}
}
