using System;
using System.Collections.Generic;
using DBTMod.Dynamicity;
using DBTMod.Extensions;
using DBTMod.Players;
using DBTMod.Transformations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace DBTMod.UserInterfaces.CharacterMenus
{
    public sealed class DBTMenu : UserInterfaces.DBTMenu
    {
        public const int
            PADDING_X = 20,
            PADDING_Y = PADDING_X,
            SMALL_SPACE = 4;

        private readonly Dictionary<TransformationDefinition, UIImagePair> _transformationImagePairs = new Dictionary<TransformationDefinition, UIImagePair>();
        private readonly Texture2D _unknownImageTexture, _unknownGrayImageTexture, _lockedImageTexture;
        private const string CHARACTER_MENU_PATH = "UserInterfaces/CharacterMenus";

        public DBTMenu(Mod authorMod)
        {
            this.AuthorMod = authorMod;
            BackPanelTexture = authorMod.GetTexture(CHARACTER_MENU_PATH + "/BackPanel");

            _unknownImageTexture = authorMod.GetTexture(CHARACTER_MENU_PATH + "/UnknownImage");
            _unknownGrayImageTexture = authorMod.GetTexture(CHARACTER_MENU_PATH + "/UnknownImageGray");
            _lockedImageTexture = authorMod.GetTexture(CHARACTER_MENU_PATH + "/LockedImage");
        }

        public override void OnInitialize()
        {
            BackPanel = new UIPanel();

            BackPanel.Width.Set(BackPanelTexture.Width, 0f);
            BackPanel.Height.Set(BackPanelTexture.Height, 0f);

            BackPanel.Left.Set(Main.screenWidth / 2f - BackPanel.Width.Pixels / 2f , 0f);
            BackPanel.Top.Set(Main.screenHeight / 2f - BackPanel.Height.Pixels / 2f, 0f);

            BackPanel.BackgroundColor = new Color(0, 0, 0, 0);

            Append(BackPanel);


            BackPanelImage = new UIImage(BackPanelTexture);
            BackPanelImage.Width.Set(BackPanelTexture.Width, 0f);
            BackPanelImage.Height.Set(BackPanelTexture.Height, 0f);

            BackPanelImage.Left.Set(-12, 0f);
            BackPanelImage.Top.Set(-12, 0f);

            BackPanel.Append(BackPanelImage);

            titleText = InitializeText("Character Menu", BackPanelTexture.Bounds.X, -32, 1, Color.White);
                

            int yOffset = PADDING_Y;

            foreach (KeyValuePair<TransformationDefinition, Node<TransformationDefinition>> kvp in TransformationDefinitionManager.Instance.Tree.RootedNodes)
            {
                if (!CheckIfDraw(kvp.Key)) continue;

                Texture2D texture = kvp.Key.BuffType.GetTexture();
                RecursiveInitializeTransformation(kvp.Value, ref yOffset);

                yOffset += texture.Height + SMALL_SPACE * 4;
            }

            base.OnInitialize();
        }

        private void RecursiveInitializeTransformation(Node<TransformationDefinition> node, ref int yOffset)
        {
            TransformationDefinition transformation = node.Current;
            Texture2D texture = transformation.BuffType.GetTexture();

            if (!CheckIfDraw(transformation)) return; // Needs edge-case check.
            int xOffset = PADDING_X;

            if (node.Parents.Length > 0 && _transformationImagePairs.ContainsKey(node.Parents[0].Current))
            {
                Node<TransformationDefinition> previousNode = node.Parents[0];
                UIImagePair previousPair = _transformationImagePairs[previousNode.Current];

                xOffset = _transformationImagePairs[previousNode.Current].position.X + (int) previousPair.button.Width.Pixels + SMALL_SPACE * 2;
            }

            DrawTransformation(transformation, texture, xOffset, yOffset);

            for (int i = 0; i < node.Children.Count; i++)
            {
                Node<TransformationDefinition> child = node.Children[i];
                RecursiveInitializeTransformation(child, ref yOffset);

                if (node.Children.Count > 1 && CheckIfDraw(child) && node.Children[node.Children.Count - 1] != child)
                {
                    yOffset += texture.Height + SMALL_SPACE * 2;
                }
            }
        }

        private void DrawTransformation(TransformationDefinition transformation, Texture2D icon, int left, int top)
        {
            UIImageButton transformationButton = null;
            UIImage
                unknownImage = null,
                unknownGrayImage = null,
                lockedImage = null;

            transformationButton = InitializeButton(icon, new MouseEvent((evt, element) => TrySelectingTransformation(transformation, evt, element)), left, top, BackPanelImage);

            unknownImage = InitializeImage(_unknownImageTexture, 0, 0, transformationButton);
            unknownImage.ImageScale = 0f;

            unknownGrayImage = InitializeImage(_unknownGrayImageTexture, 0, 0, unknownImage);
            unknownGrayImage.ImageScale = 0f;

            lockedImage = InitializeImage(_lockedImageTexture, 0, 0, unknownGrayImage);
            lockedImage.ImageScale = 0f;

            _transformationImagePairs.Add(transformation, new UIImagePair(new Point(left, top), transformationButton, unknownImage, unknownGrayImage, lockedImage));
        }

        private static bool CheckIfDraw(Node<TransformationDefinition> node) => node.Current.DisplayInMenu && node.Current.CheckPrePlayerConditions();
        private static bool CheckIfDraw(TransformationDefinition transformation) => transformation.DisplayInMenu && transformation.CheckPrePlayerConditions();

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            DBTPlayer player = Main.LocalPlayer.GetModPlayer<DBTPlayer>();

            foreach (KeyValuePair<TransformationDefinition, UIImagePair> kvp in _transformationImagePairs)
            {
                bool unlockable = kvp.Key.CanUnlock(player);
                bool visible = kvp.Key.DoesDisplayInCharacterMenu(player);

                if (!visible)
                {
                    kvp.Value.button.Width = StyleDimension.Empty;
                    kvp.Value.button.Height = StyleDimension.Empty;
                    kvp.Value.button.SetVisibility(0f, 0f);
                }

                kvp.Value.unknownImage.ImageScale = visible && unlockable ? 0f : 1f;
                kvp.Value.unknownImageGray.ImageScale = visible && unlockable && player.HasAcquiredTransformation(kvp.Key) ? 0f : 1f;
                kvp.Value.lockedImage.ImageScale = visible && unlockable ? 0f : 1f;
            }

            // Disabled as it crashes with SpriteBatch.
            /*for (int i = 0; i < _polyLinesToDraw.Count; i++)
                if (_polyLinesToDraw[i].Length > 1)
                    Main.spriteBatch.DrawPolyLine(_polyLinesToDraw[i], Color.White);*/
        }

        private static void TrySelectingTransformation(TransformationDefinition def, UIMouseEvent evt, UIElement listeningElement)
        {
            DBTPlayer dbtPlayer = Main.LocalPlayer.GetModPlayer<DBTPlayer>();

            if (dbtPlayer.HasAcquiredTransformation(def) && def.DoesDisplayInCharacterMenu(dbtPlayer))
            {
                // TODO Add sounds.
                //SoundHelper.PlayVanillaSound(SoundID.MenuTick);

                if (dbtPlayer.SelectedTransformation != def)
                {
                    dbtPlayer.SelectedTransformation = def;
                    Main.NewText($"Selected {def.DisplayName}, Mastery: {Math.Round(def.GetMaxMastery(dbtPlayer) * def.GetCurrentMastery(dbtPlayer), 2)}%");
                }
                else
                    Main.NewText($"{def.DisplayName} Mastery: {Math.Round(100f * def.GetCurrentMastery(dbtPlayer), 2)}%");
            }
            /*else if (def.SelectionRequirementsFailed.Invoke(player, def))
            {
                SoundHelper.PlayVanillaSound(SoundID.MenuClose);

                if (def.FailureText == null) return;
                Main.NewText(def.FailureText);
            }*/
        }

        public Mod AuthorMod { get; }

        public bool Visible { get; set; }
    }
}
