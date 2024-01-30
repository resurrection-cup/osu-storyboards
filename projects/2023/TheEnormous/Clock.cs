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
using StorybrewCommon.Storyboarding.CommandValues;

namespace StorybrewScripts
{
    public class Clock : StoryboardObjectGenerator
    {
        private const float ClockRadius = 142.0f;

        private const string FontName = "Times New Roman";

        private const float FontScale = 0.35f;

        private const string FontDirectory = "sb/f/cl";

        private FontGenerator _font;

        private double _beatDuration;

        public override void Generate()
        {
            _font = SetupFont();
            _beatDuration = GetBeatDuration(0);

            AddClock(16976, 33672, 16);
            AddClock(50367, 64976, 4, 0, (2 * Math.PI / 156) * 23, (2 * Math.PI / 12) * 6);
            AddClock(158889, 175585, 2, 0, (2 * Math.PI / 156) * 123, (2 * Math.PI / 12) * 63);
            AddClock(175585, 192281, 4, 0, (2 * Math.PI / 156) * 155, (2 * Math.PI / 12) * 11);
        }

        private void AddClock(double startTime, double endTime, double beatMultiplier = 4, double faceOffset = 0, double firstHandOffset = 0, double secondHandOffset = 0)
        {
            AddClockFace(startTime, endTime, faceOffset);
            AddClockHands(startTime, endTime, beatMultiplier, firstHandOffset, secondHandOffset);
        }

        private void AddClockHands(double startTime, double endTime, double beatMultiplier, double firstHandOffset, double secondHandOffset)
        {
            AddFirstClockHand(startTime, endTime, beatMultiplier, firstHandOffset);
            AddSecondClockHand(startTime, endTime, beatMultiplier, secondHandOffset);
        }

        private void AddFirstClockHand(double startTime, double endTime, double beatMultiplier, double offset)
        {
            OsbSprite hand = GetLayer("").CreateSprite("sb/clock_hands/hand_1.png");

            hand.Scale(startTime, 0.2);
            hand.Fade(startTime, 1);
            hand.Fade(endTime, 0);

            double beatMultiplier2 = beatMultiplier;
            CommandDecimal rotationValue = offset;

            for (double sTime = startTime; sTime < endTime; sTime += _beatDuration * beatMultiplier2)
            {
                if (sTime < 20107)
                {
                    beatMultiplier2 = 12;
                }
                else
                {
                    beatMultiplier2 = beatMultiplier;
                }

                if (
                    (31584 < sTime && sTime < 33672) ||
                    (58714 < sTime && sTime < 64976)
                )
                {
                    break;
                }

                hand.Rotate(OsbEasing.OutElastic, sTime, sTime + 500, rotationValue, rotationValue + (Math.PI * 2) / 156);

                rotationValue = hand.RotationAt(sTime + 500);
            }

            if (startTime < 31585 && 31585 < endTime)
            {
                CommandDecimal startValue = hand.RotationAt(31585);

                hand.Rotate(OsbEasing.OutElastic, 31585, 31585 + _beatDuration * 2, startValue, startValue - ((Math.PI * 2) / 156) * 2);

                startValue = hand.RotationAt(32107);

                hand.Rotate(OsbEasing.OutElastic, 32107, 32107 + _beatDuration * 2, startValue, startValue - ((Math.PI * 2) / 156) * 3);

                startValue = hand.RotationAt(32628);

                hand.Rotate(OsbEasing.InSine, 32628, 32628 + _beatDuration * 4, startValue, startValue - ((Math.PI * 2) / 156) * 12);
            }

            if (startTime < 58715 && 58715 < endTime)
            {
                CommandDecimal startValue = hand.RotationAt(58715);

                hand.Rotate(OsbEasing.InSine, 58715, 58715 + _beatDuration * 8, startValue, startValue - ((Math.PI * 2) / 156) * 24);

                startValue = hand.RotationAt(58715 + _beatDuration * 8);

                hand.Rotate(58715 + _beatDuration * 8, 58715 + _beatDuration * 16, startValue, startValue - ((Math.PI * 2) / 156) * 40);

                startValue = hand.RotationAt(58715 + _beatDuration * 16);

                hand.Rotate(58715 + _beatDuration * 16, 58715 + _beatDuration * 22, startValue, startValue - ((Math.PI * 2) / 156) * 41);

                startValue = hand.RotationAt(58715 + _beatDuration * 22);

                hand.Rotate(58715 + _beatDuration * 22, 58715 + _beatDuration * 24, startValue, startValue - ((Math.PI * 2) / 156) * 42);
            }
        }

        private void AddSecondClockHand(double startTime, double endTime, double beatMultiplier, double offset)
        {
            OsbSprite hand = GetLayer("").CreateSprite("sb/clock_hands/hand_2.png");

            hand.Scale(startTime, 0.2);
            hand.Fade(startTime, 1);
            hand.Fade(endTime, 0);

            double beatMultiplier2 = beatMultiplier;
            CommandDecimal rotationValue = offset;

            for (double sTime = startTime; sTime < endTime; sTime += _beatDuration * beatMultiplier2)
            {
                if (sTime < 20107)
                {
                    beatMultiplier2 = 12;
                }
                else
                {
                    beatMultiplier2 = beatMultiplier;
                }

                if (
                    (31584 < sTime && sTime < 33672) ||
                    (58714 < sTime && sTime < 64976)
                )
                {
                    break;
                }

                hand.Rotate(OsbEasing.OutElastic, sTime, sTime + 500, rotationValue, rotationValue + (Math.PI * 2) / 12);

                rotationValue = hand.RotationAt(sTime + 500);
            }

            if (startTime < 31585 && 31585 < endTime)
            {
                CommandDecimal startValue = hand.RotationAt(31585);

                hand.Rotate(OsbEasing.OutElastic, 31585, 31585 + _beatDuration * 2, startValue, startValue - ((Math.PI * 2) / 12) * 2);

                startValue = hand.RotationAt(32107);

                hand.Rotate(OsbEasing.OutElastic, 32107, 32107 + _beatDuration * 2, startValue, startValue - ((Math.PI * 2) / 12) * 3);

                startValue = hand.RotationAt(32628);

                hand.Rotate(OsbEasing.InSine, 32628, 32628 + _beatDuration * 4, startValue, startValue - ((Math.PI * 2) / 12) * 12);
            }

            if (startTime < 58715 && 58715 < endTime)
            {
                CommandDecimal startValue = hand.RotationAt(58715);

                hand.Rotate(OsbEasing.InSine, 58715, 58715 + _beatDuration * 8, startValue, startValue - ((Math.PI * 2) / 12) * 24);

                startValue = hand.RotationAt(58715 + _beatDuration * 8);

                hand.Rotate(58715 + _beatDuration * 8, 58715 + _beatDuration * 16, startValue, startValue - ((Math.PI * 2) / 12) * 40);

                startValue = hand.RotationAt(58715 + _beatDuration * 16);

                hand.Rotate(58715 + _beatDuration * 16, 58715 + _beatDuration * 22, startValue, startValue - ((Math.PI * 2) / 12) * 41);

                startValue = hand.RotationAt(58715 + _beatDuration * 22);

                hand.Rotate(58715 + _beatDuration * 22, 58715 + _beatDuration * 24, startValue, startValue - ((Math.PI * 2) / 12) * 42);
            }
        }

        private void AddClockFace(double startTime, double endTime, double offset = 0)
        {
            AddClockFaceNumbers(startTime, endTime, offset);
            AddClockFaceInners(startTime, endTime, offset);
        }

        private void AddClockFaceNumbers(double startTime, double endTime, double offset)
        {
            double angle = -0.5 * Math.PI + offset;

            for (int i = 12; i > 0; i--)
            {
                FontTexture texture = _font.GetTexture(ToRomanNumber(i));
                Vector2 position = new Vector2(
                    Constant.Center.X + ClockRadius * (float)Math.Cos(angle),
                    Constant.Center.Y + ClockRadius * (float)Math.Sin(angle)
                );
                OsbSprite number = GetLayer("").CreateSprite(texture.Path, OsbOrigin.Centre, position);

                number.Rotate(startTime, angle + Math.PI * 0.5);
                number.Scale(startTime, FontScale);
                number.Fade(startTime, 1);
                number.Fade(endTime, 0);

                angle -= (Math.PI * 2) / 12;
            }
        }

        private void AddClockFaceInners(double startTime, double endTime, double offset)
        {
            AddFirstClockFaceInner(startTime, endTime, offset);
            AddSecondClockFaceInner(startTime, endTime, offset);
        }

        void AddFirstClockFaceInner(double startTime, double endTime, double offset)
        {
            Bitmap circleBitmap = GetMapsetBitmap("sb/c2.png");
            OsbSprite circle = GetLayer("").CreateSprite("sb/c2.png");

            circle.Scale(startTime, (ClockRadius / (circleBitmap.Height * 0.5)) * 0.88);
            circle.Fade(startTime, 1);
            circle.Fade(endTime, 0);
        }

        private void AddSecondClockFaceInner(double startTime, double endTime, double offset)
        {
            double angle = -0.5 * Math.PI + offset;

            for (int i = 0; i < 60; i++)
            {
                Vector2 position = new Vector2(
                    Constant.Center.X + ClockRadius * 0.8f * (float)Math.Cos(angle),
                    Constant.Center.Y + ClockRadius * 0.8f * (float)Math.Sin(angle)
                );
                OsbSprite dot = GetLayer("").CreateSprite("sb/p.png", OsbOrigin.Centre, position);

                dot.Rotate(startTime, angle + Math.PI * 0.25);
                dot.Scale(startTime, i % 5 == 0 ? 4 : 1);
                dot.Fade(startTime, 1);
                dot.Fade(endTime, 0);

                angle -= (Math.PI * 2) / 60;
            }
        }

        private string ToRomanNumber(int integerNumber)
        {
            List<string> romanNumerals = new List<string> { "X", "IX", "V", "IV", "I" };
            List<int> integerNumerals = new List<int> { 10, 9, 5, 4, 1 };

            string romanNumber = string.Empty;

            while (integerNumber > 0)
            {
                int index = integerNumerals.FindIndex(x => x <= integerNumber);

                integerNumber -= integerNumerals[index];
                romanNumber += romanNumerals[index];
            }

            return romanNumber;
        }

        private FontGenerator SetupFont()
        {
            return LoadFont(FontDirectory, new FontDescription()
                {
                    FontPath = FontName,
                    FontSize = 60,
                    Color = Color4.White
                }
            );
        }

        private double GetBeatDuration(double time)
            => Beatmap.GetTimingPointAt((int)time).BeatDuration;
    }
}
