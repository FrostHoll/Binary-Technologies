using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ID;

namespace BinaryTechnologies.Items.Accessories
{
	public class LifeCharm : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Life Support Device");
			Tooltip.SetDefault("Greatly increased life regeneration when Activated Portal is nearby");
		}

		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 16;
			Item.value = 65000;
			Item.rare = ItemRarityID.Lime;
			Item.maxStack = 1;
			Item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
            if (player.GetModPlayer<BinaryTechnologiesPlayer>().standingNearPortalState)
            {
				player.lifeRegen += 10;
            }
		}
	}
}
