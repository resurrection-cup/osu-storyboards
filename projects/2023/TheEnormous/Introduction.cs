using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using StorybrewCommon.Subtitles;
using StorybrewCommon.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace StorybrewScripts
{
    public class Introduction : StoryboardObjectGenerator
    {
        [Configurable]
        public string FontPath = "fonts/Poppins-Medium.ttf";

        private const float FontScale = 0.3f;

        private const string FontDirectory = "sb/f/i";

        private FontGenerator Font;

        private double _beatDuration;

        public override void Generate()
        {
            double[] times = { 280, 2367, 4454, 8628, 12802, 16976 };

            Font = SetupFont();
            _beatDuration = GetBeatDuration(280);

            AddLogo("sb/logos/rescup_2023_colored_glow.png", times);
            AddGroupStage("Grand Finals", times);
            AddCustomSongText("Kagetora.", "The enormous", times);
        }

        private void AddLogo(string filepath, double[] times)
        {
            Bitmap logoBitmap = GetMapsetBitmap(filepath);
            OsbSprite logo = GetLayer("logo").CreateSprite(filepath);

            logo.Scale(OsbEasing.OutExpo, times[0], times[0] + _beatDuration * 4, (480.0 / logoBitmap.Height) * 0.8, (480.0 / logoBitmap.Height) * 0.45);
            logo.Fade(OsbEasing.OutCubic, times[0], times[0] + _beatDuration * 2, 0, 1);
            logo.MoveY(OsbEasing.OutExpo, times[1], times[1] + _beatDuration * 4, 240, 200);

            logo.Fade(16976, 0);
        }

        private void AddGroupStage(string groupStage, double[] times)
        {
            GenerateLine(groupStage, times[1], times[2]);
        }

        private void AddCustomSongText(string artist, string title, double[] times)
        {
            GenerateLine("Custom song by", times[2], times[3]);
            GenerateLine(artist, times[3], times[4]);
            GenerateLine(title, times[4], times[5]);
        }

        private void GenerateLine(string line, double startTime, double endTime)
        {
            float lineWidth = 0f;
            float lineHeight = 0f;
            foreach (var letter in line)
            {
                FontTexture texture = Font.GetTexture(letter.ToString());
                lineWidth += texture.BaseWidth * FontScale;
                lineHeight = Math.Max(lineHeight, texture.BaseHeight * FontScale);
            }

            float letterX = 320 - lineWidth * 0.5f;
            double delay = 0d;
            foreach (var letter in line)
            {
                FontTexture texture = Font.GetTexture(letter.ToString());
                if (!texture.IsEmpty)
                {
                    Vector2 position = new Vector2(letterX, 260) + texture.OffsetFor(OsbOrigin.Centre) * FontScale;
                    OsbSprite sprite = GetLayer("").CreateSprite(texture.Path, OsbOrigin.Centre, position);

                    sprite.Scale(startTime + delay, FontScale);
                    sprite.MoveX(OsbEasing.OutExpo, startTime, startTime + _beatDuration * 4, 320, position.X);
                    sprite.Fade(startTime + delay, startTime + delay + _beatDuration, 0, 1);

                    if (endTime != 16976.0)
                    {
                        sprite.MoveX(OsbEasing.InExpo, endTime - _beatDuration * 4, endTime, position.X, 320);
                        sprite.Fade(endTime - _beatDuration, endTime, 1, 0);
                    }
                    else
                    {
                        sprite.Fade(endTime, 0);
                    }

                    delay += _beatDuration / 8;
                }

                letterX += texture.BaseWidth * FontScale;
            }
        }

        private FontGenerator SetupFont()
        {
            return LoadFont(FontDirectory, new FontDescription()
                {
                    FontPath = FontPath,
                    FontSize = 90,
                    Color = Color4.White
                },
                new FontGlow()
                {
                    Radius = 64,
                    // #dc7a64
                    Color = new Color4(220, 122, 100, 255)
                }
            );
        }

        private double GetBeatDuration(double time)
            => Beatmap.GetTimingPointAt((int)time).BeatDuration;
    }
}
