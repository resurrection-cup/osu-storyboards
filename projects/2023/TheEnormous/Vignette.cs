using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using System.Drawing;

namespace StorybrewScripts
{
    public class Vignette : StoryboardObjectGenerator
    {
        private double _beatDuration;

        public override void Generate()
        {
            _beatDuration = GetBeatDuration(0);

            Bitmap bitmap = GetMapsetBitmap("sb/vig.png");
            OsbSprite sprite = GetLayer("").CreateSprite("sb/vig.png");
            sprite.Scale(280 - _beatDuration * 4, 480f / bitmap.Height);
            sprite.Fade(280 - _beatDuration * 4, 1);
            sprite.Fade(AudioDuration + _beatDuration * 8, AudioDuration + _beatDuration * 16, 1, 0);
        }

        private double GetBeatDuration(double time)
            => Beatmap.GetTimingPointAt((int)time).BeatDuration;
    }
}
