using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace BinaryTechnologies.Items
{
    class TechnoSphereSummon : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Suspicious Looking Transmitter");
            Tooltip.SetDefault("It sends signal, which cannot be recieved in this world." +
                "\nSummons Techno Sphere, if there is Activated Portal nearby.");
            ItemID.Sets.SortingPriorityBossSpawns[Type] = 12;
        }

        public override void SetDefaults()
        {
            Item.width = 12;
            Item.height = 12;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.rare = ItemRarityID.Orange;
            Item.value = 6000;
            Item.consumable = true;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.maxStack = 20;
        }

        public override bool CanUseItem(Player player)
        {
            bool activatedPortal = player.GetModPlayer<BinaryTechnologiesPlayer>().standingNearPortalState;
            return !NPC.AnyNPCs(ModContent.NPCType<NPCs.Bosses.TechnoSphere.TechnoSphere>()) && activatedPortal;
        }

        public override bool? UseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                SoundEngine.PlaySound(SoundID.Roar, player.position, 0);

                int type = ModContent.NPCType<NPCs.Bosses.TechnoSphere.TechnoSphere>();

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    NPC.SpawnOnPlayer(player.whoAmI, type);
                }
                else
                {
                    NetMessage.SendData(MessageID.SpawnBoss, number: player.whoAmI, number2: type);
                }
            }

            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Wire, 20);
            recipe.AddRecipeGroup("IronBar", 3);
            recipe.AddIngredient(ItemID.Emerald, 1);
            recipe.AddTile(ModContent.TileType<Tiles.TilePC>());
            recipe.Register();
        }

    }
}
