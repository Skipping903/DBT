using System;
using System.Collections.Generic;
using DBTR.Dynamicity;
using DBTR.Extensions;
using DBTR.Players;
using DBTR.Transformations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace DBTR.UserInterfaces.CharacterMenus
{
    public sealed class CharacterMenu : DBTRMenu
    {
        public const int
            PADDING_X = 20,
            PADDING_Y = PADDING_X,
            SMALL_SPACE = 4;

        private readonly Dictionary<TransformationDefinition, Tuple<Point, UIImagePair>> _transformationImagePairs = new Dictionary<TransformationDefinition, Tuple<Point, UIImagePair>>();

        private readonly Texture2D _unknownImageTexture, _unknownGrayImageTexture, _lockedImageTexture;

        public CharacterMenu(Mod authorMod)
        {
            this.AuthorMod = authorMod;
            BackPanelTexture = authorMod.GetTexture("UserInterfaces/CharacterMenus/BackPanel");

            _unknownImageTexture = authorMod.GetTexture("UserInterfaces/CharacterMenus/UnknownImage");
            _unknownGrayImageTexture = authorMod.GetTexture("UserInterfaces/CharacterMenus/UnknownImageGray");
            _lockedImageTexture = authorMod.GetTexture("UserInterfaces/CharacterMenus/LockedImage");
        }

        public override void OnInitialize()
        {
            BackPanel = new UIPanel();

            BackPanel.Width.Set(BackPanelTexture.Width, 0f);
            BackPanel.Height.Set(BackPanelTexture.Height, 0f);

            BackPanel.Left.Set(Main.screenWidth / 2f - BackPanel.Width.Pixels / 2f , 0f);
            BackPanel.Top.Set(Main.screenHeight / 2f - BackPanel.Height.Pixels / 2f, 0f);

            BackPanel.BackgroundColor = new Color(0, 0, 0, 0);
            BackPanel.OnMouseDown += new MouseEvent(DragStart);
            BackPanel.OnMouseUp += new MouseEvent(DragEnd);

            Append(BackPanel);


            BackPanelImage = new UIImage(BackPanelTexture);
            BackPanelImage.Width.Set(BackPanelTexture.Width, 0f);
            BackPanelImage.Height.Set(BackPanelTexture.Height, 0f);

            BackPanelImage.Left.Set(-12, 0f);
            BackPanelImage.Top.Set(-12, 0f);

            BackPanel.Append(BackPanelImage);

            int
                rowXOffset = PADDING_X,
                rowYOffset = PADDING_Y;

            InitializeText(ref titleText, "Character Menu", BackPanelTexture.Bounds.X, -32, 1, Color.White);

            foreach (KeyValuePair<TransformationDefinition, Node<TransformationDefinition>> root in TransformationDefinitionManager.Instance.Tree.RootedNodes)
            {
                // Skip if the transformation is not displayed or does not meet the conditions.
                if (!root.Key.DisplayInMenu || !root.Key.CheckPrePlayerConditions()) continue;

                RecursiveDrawTransformation(root.Value, ref rowXOffset, ref rowYOffset);

                rowYOffset += SMALL_SPACE * 2;
                rowXOffset = PADDING_X;
            }

            /*foreach (KeyValuePair<TransformationDefinition, Node<TransformationDefinition>> kvp in TransformationDefinitionManager.Instance.Tree.Nodes)
            {
                if (kvp.Value.Parents.Length > 0) continue; // We only want to draw the first column as the root transformations.
                if (!kvp.Key.DisplayInMenu || !kvp.Key.CheckPrePlayerConditions()) continue; // Skip this transformation if its not supposed to be drawn.

                StandardDrawTransformation(kvp.Value, ref rowXOffset, ref rowYOffset);
                RecursiveDrawTransformations(kvp.Value, ref rowXOffset, ref rowYOffset);

                rowXOffset = PADDING_X; // Reset back to the initial padding upon finishing one root.
            }*/
        }

        private void RecursiveDrawTransformation(Node<TransformationDefinition> node, ref int xOffset, ref int yOffset)
        {
            // Skip if the transformation is not displayed or does not meet the conditions.
            if (!node.Current.DisplayInMenu && !node.Current.CheckPrePlayerConditions()) return;

            int stepXOffset = xOffset;

            Texture2D iconTexture = node.Current.BuffType.GetTexture();

            DrawTransformation(node, iconTexture, ref xOffset, ref yOffset);
            xOffset += iconTexture.Width + SMALL_SPACE;

            for (int i = 0; i < node.Children.Count; i++)
            {
                RecursiveDrawTransformation(node.Children[i], ref xOffset, ref yOffset);

                yOffset += iconTexture.Height + SMALL_SPACE * 2;
            }

            xOffset = stepXOffset;
        }

        private void DrawTransformation(Node<TransformationDefinition> node, Texture2D icon, ref int xOffset, ref int yOffset)
        {
            UIImageButton transformationButton = null;
            UIImage
                unknownImage = null,
                unknownGrayImage = null,
                lockedImage = null;

            InitializeButton(ref transformationButton, icon, new MouseEvent((evt, element) => TrySelectingTransformation(node.Current, evt, element)), xOffset, yOffset);

            InitializeImage(ref unknownImage, _unknownImageTexture, 0, 0, transformationButton);
            unknownImage.ImageScale = 0f;

            InitializeImage(ref unknownGrayImage, _unknownGrayImageTexture, 0, 0, unknownImage);
            unknownGrayImage.ImageScale = 0f;

            InitializeImage(ref lockedImage, _lockedImageTexture, 0, 0, unknownGrayImage);
            lockedImage.ImageScale = 0f;

            _transformationImagePairs.Add(node.Current, new Tuple<Point, UIImagePair>(new Point(xOffset, yOffset), new UIImagePair(transformationButton, unknownImage, unknownGrayImage, lockedImage)));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            DBTRPlayer player = Main.LocalPlayer.GetModPlayer<DBTRPlayer>();

            foreach (KeyValuePair<TransformationDefinition, Tuple<Point, UIImagePair>> kvp in _transformationImagePairs)
            {
                bool unlockable = kvp.Key.CanUnlock(player);
                bool visible = kvp.Key.DoesDisplayInCharacterMenu(player);

                if (!visible)
                {
                    kvp.Value.Item2.button.Width = StyleDimension.Empty;
                    kvp.Value.Item2.button.Height = StyleDimension.Empty;
                    kvp.Value.Item2.button.SetVisibility(0f, 0f);
                }

                kvp.Value.Item2.unknownImage.ImageScale = visible && unlockable ? 0f : 1f;
                kvp.Value.Item2.unknownImageGray.ImageScale = visible && unlockable && player.HasAcquiredTransformation(kvp.Key) ? 0f : 1f;
                kvp.Value.Item2.lockedImage.ImageScale = visible && unlockable ? 0f : 1f;
            }

            // Disabled as it crashes with SpriteBatch.
            /*for (int i = 0; i < _polyLinesToDraw.Count; i++)
                if (_polyLinesToDraw[i].Length > 1)
                    Main.spriteBatch.DrawPolyLine(_polyLinesToDraw[i], Color.White);*/
        }

        /*private void RecursiveDrawTransformations(Node<TransformationDefinition> node, ref int rowXOffset, ref int rowYOffset)
        {
            int stepXOffset = rowXOffset;

            for (int i = 0; i < node.Children.Count; i++)
            {
                Node<TransformationDefinition> child = node.Children[i];

                // We don't want to add the same transformation twice to the interface
                // TODO Fix so the same transformation isn't called twice.
                if (_transformationImagePairs.ContainsKey(child.Current)) continue; 

                if (!child.Current.DisplayInMenu || !child.Current.CheckPrePlayerConditions()) continue; // Skip this transformation if its not supposed to be drawn.

                StandardDrawTransformation(child, ref rowXOffset, ref rowYOffset);
                RecursiveDrawTransformations(node, ref rowXOffset, ref rowYOffset);

                if (i + 1 < node.Children.Count)
                    rowYOffset += child.Current.BuffType.GetTexture().Height + SMALL_SPACE * 2;
            }

            rowXOffset = stepXOffset;
        }

        private void StandardDrawTransformation(Node<TransformationDefinition> node, ref int rowXOffset, ref int rowYOffset)
        {
            UIImageButton transformationButton = null;
            UIImage
                unknownImage = null,
                unknownGrayImage = null,
                lockedImage = null;

            Texture2D transformationIcon = node.Current.BuffType.GetTexture();

            InitializeButton(ref transformationButton, transformationIcon, new MouseEvent((evt, element) => TrySelectingTransformation(node.Current, evt, element)), rowXOffset, rowYOffset);

            InitializeImage(ref unknownImage, _unknownImageTexture, 0, 0, transformationButton);
            unknownImage.ImageScale = 0f;

            InitializeImage(ref unknownGrayImage, _unknownGrayImageTexture, 0, 0, unknownImage);
            unknownGrayImage.ImageScale = 0f;

            InitializeImage(ref lockedImage, _lockedImageTexture, 0, 0, unknownGrayImage);
            lockedImage.ImageScale = 0f;

            _transformationImagePairs.Add(node.Current, new Tuple<Point, UIImagePair>(new Point(rowXOffset, rowYOffset), new UIImagePair(transformationButton, unknownImage, unknownGrayImage, lockedImage)));
            rowXOffset += transformationIcon.Width + SMALL_SPACE;
        }*/

        private static void TrySelectingTransformation(TransformationDefinition def, UIMouseEvent evt, UIElement listeningElement)
        {
            DBTRPlayer dbtrPlayer = Main.LocalPlayer.GetModPlayer<DBTRPlayer>();

            if (dbtrPlayer.HasAcquiredTransformation(def) && def.DoesDisplayInCharacterMenu(dbtrPlayer))
            {
                // TODO Add sounds.
                //SoundHelper.PlayVanillaSound(SoundID.MenuTick);

                if (dbtrPlayer.SelectedTransformation == def)
                {
                    dbtrPlayer.SelectedTransformation = def;
                    Main.NewText($"Selected {def.DisplayName}, Mastery: {Math.Round(def.GetMaxMastery(dbtrPlayer) * def.GetCurrentMastery(dbtrPlayer), 2)}%");
                }
                else
                    Main.NewText($"{def.DisplayInMenu} Mastery: {Math.Round(100f * def.GetCurrentMastery(dbtrPlayer), 2)}%");
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
