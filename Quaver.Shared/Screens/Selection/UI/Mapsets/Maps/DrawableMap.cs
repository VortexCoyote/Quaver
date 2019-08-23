using Quaver.Shared.Database.Maps;
using Quaver.Shared.Graphics.Containers;
using Wobble.Bindables;
using Wobble.Graphics;

namespace Quaver.Shared.Screens.Selection.UI.Mapsets.Maps
{
    public sealed class DrawableMap : PoolableSprite<Map>
    {
        /// <inheritdoc />
        /// <summary>
        /// </summary>
        public override int HEIGHT { get; } = DrawableMapset.MapsetHeight;

        /// <summary>
        /// </summary>
        private DrawableMapContainer DrawableContainer { get; }

        /// <summary>
        /// </summary>
        public bool IsSelected => MapManager.Selected.Value == Item;

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="container"></param>
        /// <param name="item"></param>
        /// <param name="index"></param>
        public DrawableMap(PoolableScrollContainer<Map> container, Map item, int index) : base(container, item, index)
        {
            Size = new ScalableVector2(DrawableMapset.WIDTH, HEIGHT);

            DrawableContainer = new DrawableMapContainer(this)
            {
                Parent = this,
                Alignment = Alignment.BotRight,
                UsePreviousSpriteBatchOptions = true
            };

            Alpha = 0;
            UsePreviousSpriteBatchOptions = true;

            MapManager.Selected.ValueChanged += OnMapChanged;
            UpdateContent(item, index);
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        public override void Destroy()
        {
            // ReSharper disable once DelegateSubtraction
            MapManager.Selected.ValueChanged -= OnMapChanged;
            base.Destroy();
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="item"></param>
        /// <param name="index"></param>
        public override void UpdateContent(Map item, int index)
        {
            Item = item;
            Index = index;

            DrawableContainer.UpdateContent(Item, Index);

            if (IsSelected)
                Select();
            else
                Deselect();
        }

        /// <summary>
        /// </summary>
        public void Select()
        {
            DrawableContainer.Select();
        }

        /// <summary>
        /// </summary>
        public void Deselect()
        {
            DrawableContainer.Deselect();
        }

        /// <summary>
        ///     Handles opening/closing the mapset when the map has changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMapChanged(object sender, BindableValueChangedEventArgs<Map> e)
        {
            if (Item == e.Value)
                Select();
            else if (!IsSelected)
                Deselect();
        }
    }
}