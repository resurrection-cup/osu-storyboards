using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using StorybrewCommon.Subtitles;
using StorybrewCommon.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.IO;

namespace StorybrewScripts
{
    public class Credits : StoryboardObjectGenerator
    {
        [Configurable]
        public string FontPath = "fonts/Poppins-Medium.ttf";

        private const float FontScale = 0.42f;

        private const string FontDirectory = "sb/f/c";

        private FontGenerator _font;

        private double _beatDuration;

        private List<StaffGroup> _tournamentStaff;

        public override void Generate()
        {
            _font = SetupFont();
            _beatDuration = GetBeatDuration(294541);

            _tournamentStaff = LoadTournamentStaffList($"{ProjectPath}/data/tournament_staff.json");

            double[] times = { 280, 2367, 4454, 8628, 12802, 16976 };

            AddMapsetCredits(294541, 302889);

            AddTournamentCredits(302889, AudioDuration + _beatDuration * 4);
        }

        private void AddMapsetCredits(double startTime, double endTime)
        {
            AddLogo("sb/logos/rescup_2023_colored_glow.png");

            GenerateLine("The enormous", 296628, 302889, new Vector2(320, 220), FontScale * 1.25f);
            GenerateLine("Kagetora.", 296628, 302889, new Vector2(320, 261), FontScale);

            GenerateLine(Beatmap.Name, 297672, 302889, new Vector2(200, 313), 0.275f * 1.25f);
            GenerateLine("Ryuusei Aika", 297672, 302889, new Vector2(200, 348), 0.275f);
            
            GenerateLine("Hitsounding", 297672, 302889, new Vector2(200, 385), 0.275f * 1.25f);
            GenerateLine("TtmnZk", 297672, 302889, new Vector2(200, 420), 0.275f);
            
            GenerateLine("Design Art", 297672, 302889, new Vector2(460, 313), 0.275f * 1.25f);
            GenerateLine("Mimiliaa × Yumeyo", 297672, 302889, new Vector2(460, 348), 0.275f);      
            
            GenerateLine("Storyboarding", 297672, 302889, new Vector2(460, 385), 0.275f * 1.25f);
            GenerateLine("Ningguang", 297672, 302889, new Vector2(460, 420), 0.275f);
        }

        private void AddTournamentCredits(double startTime, double endTime)
        {
            Bitmap bitmap = GetMapsetBitmap("sb/credits.png");
            OsbSprite credits = GetLayer("").CreateSprite("sb/credits.png");

            credits.Scale(OsbEasing.OutExpo, startTime, startTime + _beatDuration * 4, (480.0 / bitmap.Height) * 1.5, (480.0 / bitmap.Height));
            credits.Scale(startTime + _beatDuration * 4, endTime, (480.0 / bitmap.Height), (480.0 / bitmap.Height) * 0.975);
            credits.Fade(OsbEasing.OutCubic, startTime, startTime + _beatDuration * 2, 0, 1);
            credits.Fade(endTime - _beatDuration, endTime, 1, 0);
        }

        private void AddLogo(string filePath)
        {
            Bitmap logoBitmap = GetMapsetBitmap(filePath);
            OsbSprite logo = GetLayer("logo").CreateSprite(filePath);

            logo.Scale(OsbEasing.OutExpo, 294541, 294541 + _beatDuration * 4, (480.0 / logoBitmap.Height) * 0.8, (480.0 / logoBitmap.Height) * 0.45);
            logo.Fade(OsbEasing.OutCubic, 294541, 294541 + _beatDuration * 2, 0, 1);
            logo.MoveY(OsbEasing.OutExpo, 296628, 296628 + _beatDuration * 4, 240, 150);
            logo.Fade(302889, 0);
        }

        private void GenerateLine(string line, double startTime, double endTime, Vector2 position, float fontScale)
        {
            float lineWidth = 0f;
            float lineHeight = 0f;
            foreach (var letter in line)
            {
                FontTexture texture = _font.GetTexture(letter.ToString());
                lineWidth += texture.BaseWidth * fontScale;
                lineHeight = Math.Max(lineHeight, texture.BaseHeight * fontScale);
            }

            float letterX = position.X - lineWidth * 0.5f;
            foreach (var letter in line)
            {
                FontTexture texture = _font.GetTexture(letter.ToString());
                if (!texture.IsEmpty)
                {
                    Vector2 letterPosition = new Vector2(letterX, position.Y) + texture.OffsetFor(OsbOrigin.Centre) * fontScale;
                    OsbSprite sprite = GetLayer("mapset").CreateSprite(texture.Path, OsbOrigin.Centre, letterPosition);

                    sprite.Scale(startTime, fontScale);
                    sprite.MoveX(OsbEasing.OutExpo, startTime, startTime + _beatDuration * 4, position.X, letterPosition.X);
                    sprite.Fade(startTime, startTime + _beatDuration, 0, 1);
                    sprite.Fade(endTime, 0);
                }

                letterX += texture.BaseWidth * fontScale;
            }
        }

        private List<StaffGroup> LoadTournamentStaffList(string filepath)
        {
            JsonSerializer serializer = new JsonSerializer();

            using (StreamReader streamReader = new StreamReader(filepath))
            {
                using (JsonReader reader = new JsonTextReader(streamReader))
                {
                    return serializer.Deserialize<List<StaffGroup>>(reader);
                }
            }
        }

        private FontGenerator SetupFont()
        {
            return LoadFont(FontDirectory, new FontDescription()
                {
                    FontPath = FontPath,
                    FontSize = 60,
                    Color = Color4.White
                },
                new FontGlow()
                {
                    Radius = 32,
                    // #dc7a64
                    Color = new Color4(220, 122, 100, 255)
                }
            );
        }

        private double GetBeatDuration(double time)
            => Beatmap.GetTimingPointAt((int)time).BeatDuration;
    }
}
