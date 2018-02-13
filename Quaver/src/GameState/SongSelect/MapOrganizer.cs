﻿using Quaver.Graphics;
using Quaver.Graphics.Button;
using Quaver.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Quaver.Database.Beatmaps;
using Quaver.Graphics.Sprite;
using Quaver.Audio;
using Quaver.Database;
using Quaver.Utility;

namespace Quaver.GameState.SongSelect
{
    /// <summary>
    ///     Insert song sorting + other map organizing tools here
    /// </summary>
    class MapOrganizer : IHelper
    {
        private ButtonOrganizer ButtonOrganizer = new ButtonOrganizer();

        private Boundary Boundary { get; set; }

        public bool ScrollingDisabled { get; set; }

        public object TogglePitch { get; private set; }

        public void Draw()
        {
            ButtonOrganizer.Draw();
            Boundary.Draw();
        }

        public void Initialize(IGameState state)
        {
            Boundary = new Boundary();
            ButtonOrganizer.Initialize(state);
            //SelectMap((int)Math.Floor(Util.Random(0, SongSelectButtons.Count)));
        }

        public void UnloadContent()
        {
            //Logger.Log("UNLOADED", LogColors.GameError);
            ButtonOrganizer.UnloadContent();
            /*for (var i=0; i<SongSelectButtons.Count; i++)
                SongSelectButtons[i].Clicked -= SongSelectEvents[i];
            SongSelectButtons.Clear();
            SongSelectEvents.Clear();*/
            Boundary.Destroy();
        }

        public void Update(double dt)
        {
            var tween = Math.Min(dt / 70, 1);

            // Update Position of Boundary
            //var posDifference = Util.Tween(TargetPosition, Boundary.PosY, tween) - Boundary.PosY;
            //if (Math.Abs(posDifference) > 0.5f) Boundary.PosY += posDifference;

            /* SelectedMapTween = Util.Tween(SelectedMapIndex, SelectedMapTween, tween);
            for (var i=0; i<SongSelectButtons.Count; i++)
            {
                var button = SongSelectButtons[i];
                var selectedOffset = Math.Abs(SelectedMapTween - i)+1;

                button.PosX = -(30 / selectedOffset) - 5;

            }*/

            ButtonOrganizer.Update(dt);
            Boundary.Update(dt);
        }

        /// <summary>
        ///     Changes the map when a song select button is clicked.
        /// </summary>
        private void OnSongSelectButtonClick(object sender, EventArgs e, int index)
        {
            SelectMap(index);
        }

        private void SelectMap(int index)
        {
            /*
            ScrollingDisabled = true;
            var map = SongSelectButtons[index].Map;
            Logger.Update("MapSelected", "Map Selected: " + map.Artist + " - " + map.Title + " [" + map.DifficultyName + "]");

            SongSelectButtons[SelectedMapIndex].Selected = false;
            SongSelectButtons[index].Selected = true;
            SelectedMapIndex = index;
            TargetPosition = (GameBase.WindowRectangle.Height / 2f) - ((float)index / SongSelectButtons.Count) * OrganizerSize;

            var oldMapAudioPath = GameBase.SelectedBeatmap.Directory + "/" + GameBase.SelectedBeatmap.AudioPath;
            Beatmap.ChangeBeatmap(map);

            // Only load the audio again if the new map's audio isn't the same as the old ones.
            if (oldMapAudioPath != map.Directory + "/" + map.AudioPath)
                SongManager.ReloadSong(true);

            // Load background asynchronously if the backgrounds actually do differ
            if (GameBase.LastBackgroundPath != map.Directory + "/" + map.BackgroundPath)
            {
                Task.Run(() =>
                {
                    BackgroundManager.LoadBackground();
                }).ContinueWith(t =>
                {
                    // After loading, change the background
                    BackgroundManager.Change(GameBase.CurrentBackground);
                });
            }

            // Load all the local scores from this map 
            // TODO: Add filters, this should come after there's some sort of UI to do so
            // TODO #2: Actually display these scores on-screen somewhere. Add loading animation before running task.
            // TODO #3: Move this somewhere so that it automatically loads the scores upon first load as well.
            Task.Run(async () => await LocalScoreCache.SelectBeatmapScores(GameBase.SelectedBeatmap.Md5Checksum))
                .ContinueWith(t => Logger.Log($"Successfully loaded {t.Result.Count} local scores for this map.", LogColors.GameInfo,0.2f));

            //TODO: make it so scrolling is disabled until background has been loaded
            ScrollingDisabled = false;*/
        }

        public void SetMapOrganizerPosition(float scale)
        {
            //TargetPosition = scale * OrganizerSize;
        }

        public void OffsetMapOrganizerPosition(float offset)
        {
            //TargetPosition += offset * 2;
        }

        public void OffsetMapOrganizerIndex(int offset)
        {
            /*var newIndex = SelectedMapIndex + offset;
            if (newIndex >= 0 && newIndex < SongSelectButtons.Count)
            {
                SelectMap(newIndex);
            }*/
        }
    }
}
