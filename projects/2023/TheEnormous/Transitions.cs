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
    public class Transitions : StoryboardObjectGenerator
    {
        private double _beatDuration;

        private OsbSprite _flash;

        public override void Generate()
        {
            _beatDuration = GetBeatDuration(0);
            _flash = GetLayer("flash").CreateSprite("sb/p.png");
            _flash.ScaleVec(0, 854, 480);
            _flash.Fade(0, 0);
            _flash.StartLoopGroup(298781, 63);
            _flash.Fade(0, _beatDuration * 0.25, 0.25, 0.1);
            _flash.EndGroup();

            AddDiagonalClose(33150, 33672, "#1f1f1f");
            AddDiagonalClose(232976, 234020, "#1f1f1f", OsbEasing.InExpo);

            AddInterlacedDiagonalClose(16454, 16976, "#1f1f1f");
            AddInterlacedDiagonalClose(175063, 175585, "#1f1f1f");

            AddBlackClose(47759, 48280, 48020);
            AddBlackClose(191498, 192281, 191759);

            AddSmallDiagonalSplitClose(64454, 64976);
            AddSmallDiagonalSplitClose(83237, 83759);
            AddSmallDiagonalSplitClose(114541, 115065);
            AddSmallDiagonalSplitClose(150020, 150541);
            AddSmallDiagonalSplitClose(219150, 219411);
            AddSmallDiagonalSplitClose(251237, 251759, "#1f1f1f");
            AddSmallDiagonalSplitClose(284628, 285150, "#1f1f1f");

            AddSplitScaleCloseUpDown(64976, 66020);

            AddRectangleSplit(66020, 67063);

            AddSplitScaleCloseLeftRight(116107, 117150);

            AddSplitScaleCloseHorizontal(216281, 217324, 0.25, "#1f1f1f");
            AddSplitScaleCloseVertical(132541, 132802, 1.0 / 32, "#1f1f1f");
            AddSplitScaleCloseVertical(268976, 269498, 1.0 / 32, "#1f1f1f");

            AddRotatingSquare3(48280, 50367);
            AddRotatingSquare2(100454, 115063);
            AddRotatingSquare(234020, 236107, true, "#1f1f1f");
            AddRotatingSquare4(251759, 252802, true, "#1f1f1f");
            AddRotatingSquare5(285150, 286194, true, "#1f1f1f");

            AddFlash(280, 8);
            AddFlash(16976, 8, 0.5);
            AddFlash(33672, 8);
            AddFlash(50367, 8);
            AddFlash(67063, 8);
            AddFlash(83759, 8, 0.75);
            AddFlash(100454, 8, 0.75);
            AddFlash(117150, 8);
            AddFlash(133846, 8);
            AddFlash(150541, 12, 0.75);
            AddFlash(158889, 12, 0.75);
            AddFlash(175585, 8, 0.75);
            AddFlash(192281, 8);
            AddFlash(219411, 8);
            AddFlash(236107, 8, 0.75);
            AddFlash(252802, 4, 0.75);
            AddFlash(253846, 4, 0.5);
            AddFlash(254889, 4, 0.75);
            AddFlash(255933, 4, 0.5);
            AddFlash(256976, 4, 0.75);
            AddFlash(258020, 4, 0.5);
            AddFlash(259063, 4, 0.75);
            AddFlash(260107, 4, 0.5);
            AddFlash(261150, 8);
            AddFlash(269498, 8);
            AddFlash(286194, 8);
            AddFlash(294541);
            AddFlash(298715, 0.25, 0.75, 0.1);
            AddFlash(302889);
        }

        private void AddDiagonalClose(double startTime, double endTime, string colorCode = "#ffffff", OsbEasing easing = OsbEasing.OutQuart)
        {
            double angle = Math.PI / 3;
            Vector2 position = new Vector2(-107, 0);

            for (int i = 0; i < 2; i++)
            {
                OsbSprite sprite = GetLayer("diagonal_close").CreateSprite("sb/p.png", OsbOrigin.CentreLeft, position);
                sprite.ScaleVec(easing, startTime, endTime, 0, 1500, 422, 1500);
                sprite.Rotate(startTime, angle);
                sprite.Color(startTime, colorCode);

                position = new Vector2(747, 480);
                angle += Math.PI;
            }
        }

        private void AddInterlacedDiagonalClose(double startTime, double endTime, string colorCode = "#ffffff")
        {
            double angle = Math.Atan2(107, 480);
            double width = 960.0 / 4;

            Vector2 position = new Vector2(-100 + (float)(width * 0.5f), -40);
            double delay = 0d;

            for (int i = 0; i < 4; i++)
            {
                OsbSprite sprite = GetLayer("interlaced_diagonal_close").CreateSprite("sb/p.png", i % 2 == 0 ? OsbOrigin.TopCentre : OsbOrigin.BottomCentre, position);
                sprite.ScaleVec(OsbEasing.OutQuart, startTime + delay, endTime, width, 0, width, 560);
                sprite.Fade(startTime + delay, 1);
                sprite.Fade(endTime, 0);
                sprite.Rotate(startTime + delay, angle);
                sprite.Color(startTime + delay, colorCode);

                if (i % 2 == 0)
                {
                    position.X += (float)width * 0.5f;
                }
                else
                {
                    if (i == 1)
                    {
                        position.X += (float)width * 0.5f;
                    }

                    position.X += (float)width;
                }


                position.Y = -40 + 560 * ((i + 1) % 2);

                delay += _beatDuration * 0.5;
            }
        }

        private void AddSplitScaleCloseUpDown(double startTime, double endTime, double beatMultiply = 0.25)
        {
            double spriteAmount = Math.Round((endTime - startTime) / (_beatDuration * beatMultiply));
            double width = 854.0 / spriteAmount;
            double positionX = -107 + (float)width * 0.5f;
            double delay = 0;

            for (int i = 0; i < spriteAmount; i++)
            {
                OsbSprite sprite = GetLayer("split_scale_close_updown").CreateSprite("sb/p.png",
                    i % 2 == 0 ? OsbOrigin.TopCentre : OsbOrigin.BottomCentre,
                    new Vector2((float)positionX, i % 2 == 0 ? 0 : 480));
                sprite.ScaleVec(OsbEasing.OutExpo, startTime + delay, endTime, width, 0, width, 480);

                positionX += width;
                delay += _beatDuration * beatMultiply;
            }
        }

        private void AddRectangleSplit(double startTime, double endTime, string colorCode = "#ffffff")
        {
            OsbSprite sprite = GetLayer("rectangle_split").CreateSprite("sb/p.png");
            OsbSprite black = GetLayer("rectangle_split").CreateSprite("sb/p.png", OsbOrigin.TopCentre, new Vector2(320, 190));

            sprite.ScaleVec(OsbEasing.OutExpo, startTime, startTime + _beatDuration, 960, 480, 206, 100);
            sprite.Fade(startTime, 1);
            sprite.Fade(startTime + _beatDuration * 2, 0);

            black.ScaleVec(OsbEasing.OutExpo, startTime + _beatDuration, startTime + _beatDuration * 2, 6, 0, 6, 100);
            black.Color(startTime + _beatDuration, Color.Black);

            for (int i = 0; i < 2; i++)
            {
                Vector2 position = new Vector2(320 + 53 * (i % 2 == 0 ? -1 : 1), 240);
                OsbSprite square = GetLayer("rectangle_split").CreateSprite("sb/p.png", OsbOrigin.Centre, position);

                square.MoveX(OsbEasing.OutExpo, startTime + _beatDuration * 2, startTime + _beatDuration * 3, position.X, position.X + 125 * (i % 2 == 0 ? -1 : 1));
                square.MoveY(OsbEasing.OutQuad, startTime + _beatDuration * 3, startTime + _beatDuration * 4, position.Y, position.Y + 75 * (i % 2 == 0 ? 1 : -1));
                square.Rotate(OsbEasing.OutExpo, startTime + _beatDuration * 3, startTime + _beatDuration * 4, 0, Math.PI / 4);
                square.Scale(OsbEasing.InExpo, startTime + _beatDuration * 3, startTime + _beatDuration * 4, 100, 960);
            }
        }

        private void AddSplitScaleCloseLeftRight(double startTime, double endTime, double beatMultiply = 0.25)
        {
            double spriteAmount = Math.Round((endTime - startTime) / (_beatDuration * beatMultiply));
            double height = 480.0 / spriteAmount;
            double positionY = (float)height * 0.5f;
            double delay = 0;

            for (int i = 0; i < spriteAmount; i++)
            {
                OsbSprite sprite = GetLayer("split_scale_close_leftright").CreateSprite("sb/p.png",
                    i % 2 == 0 ? OsbOrigin.CentreLeft : OsbOrigin.CentreRight,
                    new Vector2(i % 2 == 0 ? -107 : 747, (float)positionY));
                sprite.ScaleVec(OsbEasing.OutExpo, startTime + delay, endTime, 0, height, 854, height);

                positionY += height;
                delay += _beatDuration * beatMultiply;
            }
        }

        private void AddSplitScaleCloseHorizontal(double startTime, double endTime, double beatMultiply = 0.25, string colorCode = "#ffffff")
        {
            double spriteAmount = Math.Round((endTime - startTime) / (_beatDuration * beatMultiply));
            double width = 854.0 / spriteAmount;
            double positionX = -107 + (float)width * 0.5f;
            double delay = 0;

            for (int i = 0; i < spriteAmount; i++)
            {
                OsbSprite sprite = GetLayer("split_scale_close_horizontal")
                    .CreateSprite("sb/p.png", OsbOrigin.Centre, new Vector2((float)positionX, 240));
                sprite.ScaleVec(OsbEasing.OutExpo, startTime + delay, endTime, 0, 480, width, 480);
                sprite.Color(startTime + delay, colorCode);

                positionX += width;
                delay += _beatDuration * beatMultiply;
            }
        }

        private void AddSplitScaleCloseVertical(double startTime, double endTime, double beatMultiply = 0.25, string colorCode = "#ffffff")
        {
            double spriteAmount = Math.Round((endTime - startTime) / (_beatDuration * beatMultiply));
            double height = 480.0 / spriteAmount;
            double positionY = (float)height * 0.5f;
            double delay = 0;

            for (int i = 0; i < spriteAmount; i++)
            {
                OsbSprite sprite = GetLayer("split_scale_close_horizontal")
                    .CreateSprite("sb/p.png", OsbOrigin.Centre, new Vector2(320, (float)positionY));
                sprite.ScaleVec(OsbEasing.OutExpo, startTime + delay, endTime, 854, 0, 854, height);
                sprite.Color(startTime + delay, colorCode);

                positionY += height;
                delay += _beatDuration * beatMultiply;
            }
        }

        private void AddRotatingSquare(
            double startTime,
            double endTime,
            bool addSolidBg = false,
            string solidColor = "#000000"
        )
        {
            if (addSolidBg)
            {
                OsbSprite solid = GetLayer("rotating_square").CreateSprite("sb/p.png");
                solid.ScaleVec(startTime, 854, 480);
                solid.Color(startTime, solidColor);
                solid.Fade(startTime, 1);
                solid.Fade(endTime, 0);
            }

            OsbSprite sprite = GetLayer("rotating_square").CreateSprite("sb/p.png");
            sprite.Color(startTime, Color.DimGray);
            sprite.Scale(OsbEasing.OutExpo, startTime, startTime + _beatDuration * 4, 0, 120);
            sprite.Rotate(OsbEasing.OutExpo, startTime, startTime + _beatDuration * 4, 0, 5 * Math.PI / 3);
            sprite.Fade(startTime, 1);
            sprite.Fade(endTime, 0);

            sprite.Scale(OsbEasing.InExpo, endTime - _beatDuration * 4, endTime, 120, 980);
            sprite.Rotate(OsbEasing.InCirc, endTime - _beatDuration * 4, endTime,
                sprite.RotationAt(endTime - _beatDuration * 4),
                sprite.RotationAt(endTime - _beatDuration * 4) + 5 * Math.PI / 4);
        }

        private void AddRotatingSquare2(double startTime, double endTime)
        {
            OsbSprite sprite = GetLayer("rotating_square_2").CreateSprite("sb/p.png");

            sprite.Scale(OsbEasing.OutExpo, startTime, startTime + _beatDuration * 2, 0, 160);
            sprite.Rotate(OsbEasing.OutExpo, startTime, startTime + _beatDuration * 8, 0, 7 * Math.PI / 3);
            sprite.Rotate(OsbEasing.None, startTime + _beatDuration * 8, endTime, sprite.RotationAt(startTime + _beatDuration * 8), 15 * Math.PI / 4);
            sprite.Fade(startTime, 0.25);
            sprite.Color(startTime, "#e4480b");
            sprite.Additive(startTime, endTime);

            Bitmap squareBitmap = GetMapsetBitmap("sb/s.png");
            float size = (160.0f / squareBitmap.Height) * 1.05f;
            int i = 0;
            foreach (OsuHitObject hitObject in Beatmap.HitObjects)
            {
                if (hitObject.StartTime < 108802 - 5 || 115063 - 5 <= hitObject.StartTime)
                {
                    continue;
                }

                OsbSprite square = GetLayer("rotating_square_2").CreateSprite("sb/s.png");

                square.Scale(OsbEasing.OutExpo, hitObject.StartTime, hitObject.StartTime + _beatDuration, size * 3, size);
                square.Rotate(hitObject.StartTime, endTime, sprite.RotationAt(hitObject.StartTime), 15 * Math.PI / 4);
                square.Fade(hitObject.StartTime, 0.25);
                square.Fade(endTime, 0);

                size += 0.02f + 0.0002f * ++i;
            }
        }

        private void AddRotatingSquare3(double startTime, double endTime)
        {
            OsbSprite sprite = GetLayer("rotating_square").CreateSprite("sb/p.png");
            sprite.Color(startTime, Color.DimGray);
            sprite.ScaleVec(OsbEasing.OutExpo, startTime, startTime + _beatDuration * 4, 0, 0, 120, 120);
            sprite.Rotate(OsbEasing.OutExpo, startTime, startTime + _beatDuration * 4, 0, 6 * Math.PI / 4);

            sprite.ScaleVec(OsbEasing.InExpo, startTime + _beatDuration * 4, startTime + _beatDuration * 6, 120, 120, 20, 854);
            sprite.ScaleVec(OsbEasing.InExpo, startTime + _beatDuration * 6, startTime + _beatDuration * 8, 20, 854, 420, 854);
        }

        private void AddRotatingSquare4(
            double startTime,
            double endTime,
            bool addSolidBg = false,
            string solidColor = "#000000"
        )
        {
            if (addSolidBg)
            {
                OsbSprite solid = GetLayer("rotating_square").CreateSprite("sb/p.png");
                solid.ScaleVec(startTime, 854, 480);
                solid.Color(startTime, solidColor);
                solid.Fade(startTime, 1);
                solid.Fade(endTime, 0);
            }

            OsbSprite sprite = GetLayer("rotating_square").CreateSprite("sb/p.png");
            sprite.Color(startTime, Color.DimGray);
            sprite.Scale(OsbEasing.OutExpo, startTime, startTime + _beatDuration * 2, 0, 120);
            sprite.Rotate(OsbEasing.OutExpo, startTime, startTime + _beatDuration * 4, 0, 3 * Math.PI / 3);
            sprite.Scale(OsbEasing.OutExpo, endTime - _beatDuration, endTime, 120, 960);
        }

        private void AddRotatingSquare5(
            double startTime,
            double endTime,
            bool addSolidBg = false,
            string solidColor = "#000000"
        )
        {
            if (addSolidBg)
            {
                OsbSprite solid = GetLayer("rotating_square").CreateSprite("sb/p.png");
                solid.ScaleVec(startTime, 854, 480);
                solid.Color(startTime, solidColor);
                solid.Fade(startTime, 1);
                solid.Fade(endTime, 0);
            }

            OsbSprite sprite = GetLayer("rotating_square").CreateSprite("sb/p.png");
            sprite.Color(startTime, Color.DimGray);
            sprite.Scale(OsbEasing.OutExpo, startTime, startTime + _beatDuration * 2, 0, 120);
            sprite.Rotate(OsbEasing.OutExpo, startTime, startTime + _beatDuration * 2, 0, 5 * Math.PI / 3);
            sprite.Fade(startTime, 1);
            sprite.Fade(endTime, 0);

            sprite.Scale(OsbEasing.InExpo, endTime - _beatDuration * 2, endTime, 120, 980);
            sprite.Rotate(OsbEasing.InCirc, endTime - _beatDuration * 2, endTime,
                sprite.RotationAt(endTime - _beatDuration * 2),
                sprite.RotationAt(endTime - _beatDuration * 2) + 5 * Math.PI / 4);
        }

        private void AddSmallDiagonalSplitClose(double startTime, double endTime, string colorCode = "#000000")
        {
            double spriteAmount = Math.Round((endTime - startTime) / (_beatDuration / 32));
            double width = 910.0 / spriteAmount;
            double positionX = -146 + (float)width * 0.5f;
            double delay = 0;

            for (int i = 0; i < spriteAmount; i++)
            {
                OsbSprite sprite = GetLayer("small_diagonal_split_close")
                    .CreateSprite("sb/p.png", OsbOrigin.CentreLeft, new Vector2((float)positionX, 240));
                sprite.ScaleVec(OsbEasing.OutExpo, startTime + delay, endTime, 0, 490, width * 1.001, 490);
                sprite.Rotate(startTime + delay, Math.PI / 30);
                sprite.Color(startTime + delay, colorCode);

                positionX += width;
                delay += _beatDuration / 32;
            }
        }

        private void AddBlackClose(double startTime, double endTime, double middleTime = 0)
        {
            for (int i = 0; i < 2; i++)
            {
                OsbSprite sprite = GetLayer("black_close").CreateSprite("sb/p.png",
                    i % 2 == 0 ? OsbOrigin.TopCentre : OsbOrigin.BottomCentre, new Vector2(320, i % 2 * 480));


                if (startTime != 0 && middleTime != 0)
                {
                    sprite.ScaleVec(OsbEasing.OutExpo, startTime, middleTime, 854, 0, 854, 200);
                    sprite.ScaleVec(OsbEasing.InExpo, middleTime, endTime, 854, 200, 854, 240);
                }
                else
                {
                    sprite.ScaleVec(OsbEasing.OutExpo, startTime, endTime, 854, 0, 854, 240);
                }

                sprite.Color(startTime, "#000000");
            }
        }

        private void AddBlackCloseWithFollowBeat(double startTime, double beatMultiply = 1)
        {
            for (int i = 0; i < 2; i++)
            {
                OsbSprite sprite = GetLayer("black_close").CreateSprite("sb/p.png",
                    i % 2 == 0 ? OsbOrigin.TopCentre : OsbOrigin.BottomCentre, new Vector2(320, i % 2 * 480));
                sprite.Color(startTime, "#000000");

                for (int j = 0; j < 3; j++)
                {
                    sprite.ScaleVec(OsbEasing.OutExpo, startTime + (_beatDuration * beatMultiply) * j, startTime +
                                                                                                       (_beatDuration * beatMultiply) * (j + 1), 854, 80 * j, 854, 80 * (j + 1));
                }
            }
        }

        private void AddFlash(
            double startTime,
            double beatMultiply = 4,
            double startOpacity = 1,
            double endOpacity = 0,
            OsbEasing easing = OsbEasing.Out
        )
        {
            _flash.Fade(easing, startTime, startTime + _beatDuration * beatMultiply, startOpacity, endOpacity);
        }

        private double GetBeatDuration(double time)
            => Beatmap.GetTimingPointAt((int)time).BeatDuration;
    }
}
