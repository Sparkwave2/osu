// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Game.Overlays.Dialog;
using osu.Game.Scoring;
using System.Diagnostics;
using osu.Framework.Graphics.Sprites;
using osu.Game.Beatmaps;

namespace osu.Game.Screens.Select
{
    public class LocalScoreDeleteDialog : PopupDialog
    {
        private readonly ScoreInfo score;

        [Resolved]
        private ScoreManager scoreManager { get; set; }

        [Resolved]
        private BeatmapManager beatmapManager { get; set; }

        public LocalScoreDeleteDialog(ScoreInfo score)
        {
            this.score = score;
            Debug.Assert(score != null);
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            BeatmapInfo beatmap = beatmapManager.QueryBeatmap(b => b.ID == score.BeatmapInfoID);
            Debug.Assert(beatmap != null);

            string accuracy = string.Format(score.Accuracy % 1 == 0 ? @"{0:P0}" : @"{0:P2}", score.Accuracy);

            BodyText = $@"{score.User}'s {accuracy} {score.Rank} Rank on {beatmap}";
            Icon = FontAwesome.Solid.Eraser;
            HeaderText = @"Deleting this local score. Are you sure?";
            Buttons = new PopupDialogButton[]
            {
                new PopupDialogOkButton
                {
                    Text = @"Yes. Please.",
                    Action = () => scoreManager?.Delete(score)
                },
                new PopupDialogCancelButton
                {
                    Text = @"No, I'm still attached.",
                },
            };
        }
    }
}
