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
using System.IO;

namespace StorybrewScripts
{
    public class Backgrounds : StoryboardObjectGenerator
    {
        private double _beatDuration;
        private double _offset;

        private readonly Vector2 _center = new Vector2(320, 240);

        public override void Generate()
        {
            _beatDuration = GetBeatDuration(0);
            _offset = GetOffset(0);

            AddIntroductionBackground("sb/backgrounds/bg_only_blur.png", 280, 33672);

            AddPostIntroductionBackground(Beatmap.BackgroundPath, 33672, 48280);

            AddFirstBuildUpBackground("sb/backgrounds/main_bg_blur.png", 50367, 64976);

            AddSecondBuildUpBackground("sb/backgrounds/main_bg_blur.png", 100454, 115063);

            AddFirstGlowingBumpBackground(Beatmap.BackgroundPath, 67063, 100454);
            AddFirstGlowingBumpBackground(Beatmap.BackgroundPath, 219411, 234020);
            AddFirstGlowingBumpBackground(Beatmap.BackgroundPath, 236107, 251759);

            AddSecondDropBackground(Beatmap.BackgroundPath, 117150, 150541);

            AddBreakBackground("sb/backgrounds/bg_only_blur.png", 150541, 192281);
            AddBreakBackground("sb/backgrounds/bg_only_blur.png", 261150, 269498);

            AddLeadMelodyBackground(Beatmap.BackgroundPath, 192281, 219411);

            AddLeadMelodyBackground(Beatmap.BackgroundPath, 252802, 261150);

            AddLastSectionBackground(Beatmap.BackgroundPath, 269498, 294541);

            AddBeatmapCreditsBackground("sb/backgrounds/main_bg_blur.png", 294541, 302889);
            AddTournamentCreditsBackground("sb/backgrounds/bg_only_blur.png", 302889, AudioDuration + _beatDuration * 4);

            AddSolidBackground("a", 132802, 133846, "#1f1f1f");
            AddSolidBackground("b", 217324, 219411, "#1f1f1f");
        }

        private void AddIntroductionBackground(string filePath, double startTime, double endTime)
        {
            Bitmap backgroundBitmap = GetMapsetBitmap(filePath);
            OsbSprite background = GetLayer("introduction").CreateSprite(filePath);

            background.Scale(startTime, (854.0 / backgroundBitmap.Width) * 1.2);
            background.Fade(startTime, 0.16);
            background.Fade(endTime, 0);

            MoveBackground(ref background, startTime, endTime, 12f, Math.PI / 120, 24);
        }

        private void AddPostIntroductionBackground(string filePath, double startTime, double endTime)
        {
            Bitmap backgroundBitmap = GetMapsetBitmap(filePath);
            OsbSprite background = GetLayer("introduction").CreateSprite(filePath);

            background.Scale(startTime, (854.0 / backgroundBitmap.Width) * 1.2);
            background.Fade(endTime, 0);

            MoveBackground(ref background, startTime, endTime, 12f, Math.PI / 120, 20);

            background.StartLoopGroup(startTime, 3);
            background.Fade(OsbEasing.OutSine, 0, _beatDuration * 1.5, 0.7, 0.4);
            background.Fade(OsbEasing.OutSine, _beatDuration * 1.5, _beatDuration * 3, 0.7, 0.4);
            background.Fade(OsbEasing.OutSine, _beatDuration * 3, _beatDuration * 4.5, 0.7, 0.4);
            background.Fade(_beatDuration * 4.5, _beatDuration * 8, 0.4, 0.4);
            background.EndGroup();

            background.StartLoopGroup(startTime + _beatDuration * 24, 1);
            background.Fade(OsbEasing.OutSine, 0, _beatDuration * 1.5, 0.7, 0.4);
            background.Fade(OsbEasing.OutSine, _beatDuration * 1.5, _beatDuration * 3, 0.7, 0.4);
            background.Fade(OsbEasing.OutSine, _beatDuration * 3, _beatDuration * 4, 0.7, 0.4);
            background.EndGroup();

            background.StartLoopGroup(startTime + _beatDuration * 28, 5);
            background.Fade(OsbEasing.OutSine, 0, _beatDuration * 0.25, 0.7, 0.4);
            background.EndGroup();

            background.StartLoopGroup(startTime + _beatDuration * 29.5, 5);
            background.Fade(OsbEasing.OutSine, 0, _beatDuration * 0.25, 0.7, 0.4);
            background.EndGroup();

            background.StartLoopGroup(startTime + _beatDuration * 31, 4);
            background.Fade(OsbEasing.OutSine, 0, _beatDuration * 0.25, 0.7, 0.4);
            background.EndGroup();

            background.StartLoopGroup(startTime + _beatDuration * 32, 3);
            background.Fade(OsbEasing.OutSine, 0, _beatDuration * 1.5, 0.7, 0.4);
            background.Fade(OsbEasing.OutSine, _beatDuration * 1.5, _beatDuration * 3, 0.7, 0.4);
            background.Fade(OsbEasing.OutSine, _beatDuration * 3, _beatDuration * 4.5, 0.7, 0.4);
            background.Fade(_beatDuration * 4.5, _beatDuration * 8, 0.4, 0.4);
            background.EndGroup();
        }

        private void AddFirstBuildUpBackground(string filePath, double startTime, double endTime)
        {
            Bitmap backgroundBitmap = GetMapsetBitmap(filePath);
            OsbSprite background = GetLayer("build_up").CreateSprite(filePath);

            background.Scale(startTime, (854.0 / backgroundBitmap.Width) * 1.2);
            background.Fade(startTime, 0.4);
            background.Fade(endTime, 0);

            MoveBackground(ref background, startTime, endTime, 12f, Math.PI / 120, 20);
        }

        private void AddSecondBuildUpBackground(string filePath, double startTime, double endTime)
        {
            Bitmap backgroundBitmap = GetMapsetBitmap(filePath);
            OsbSprite background = GetLayer("build_up").CreateSprite(filePath);

            background.Scale(startTime, (854.0 / backgroundBitmap.Width) * 1.2);
            background.Fade(startTime, 0.16);
            background.Fade(endTime, 0);

            MoveBackground(ref background, startTime, endTime, 12f, Math.PI / 120, 24);
        }

        private void AddSecondDropBackground(string filePath, double startTime, double endTime)
        {
            Bitmap backgroundBitmap = GetMapsetBitmap(filePath);
            OsbSprite background = GetLayer("build_up").CreateSprite(filePath);

            background.Scale(startTime, (854.0 / backgroundBitmap.Width) * 1.1);
            background.Fade(startTime - 1, 0);
            background.Fade(startTime, 0.65);
            background.Fade(endTime, 0);

            AddParallaxBackground(ref background, startTime, endTime, 8, 0.05, OsbEasing.InSine);

            int loopCount = (int)Math.Round(((endTime - _beatDuration * 4) - startTime) / (_beatDuration * 4));

            background.StartLoopGroup(startTime, loopCount);
            background.Fade(0, _beatDuration, 0.8, 0.6);
            background.Fade(_beatDuration, _beatDuration * 4, 0.6, 0.6);
            background.EndGroup();
        }

        private void AddBreakBackground(string filePath, double startTime, double endTime)
        {
            OsbSprite solid = GetLayer("break").CreateSprite("sb/p.png");
            solid.ScaleVec(startTime, 854, 480);
            solid.Color(startTime, Color.Black);
            solid.Fade(startTime, 1);
            solid.Fade(startTime + _beatDuration * 8, 0);

            Bitmap backgroundBitmap = GetMapsetBitmap(filePath);
            OsbSprite background = GetLayer("break").CreateSprite(filePath);

            background.Scale(startTime, (854.0 / backgroundBitmap.Width) * 1.2);
            background.Fade(startTime, 0.4);
            background.Fade(endTime, 0);

            MoveBackground(ref background, startTime, endTime, 12f, Math.PI / 120, 28);

            if (startTime < 268454 && 268454 < endTime)
            {
                background.Fade(268454, 268454 + _beatDuration, 0.4, 0.75);
                background.Scale(OsbEasing.OutSine, 268454, 268454 + _beatDuration * 4, (854.0 / backgroundBitmap.Width) * 1.2, (854.0 / backgroundBitmap.Width) * 1.5);
            }
        }

        private void AddFirstGlowingBumpBackground(string filePath, double startTime, double endTime)
        {
            Bitmap backgroundBitmap = GetMapsetBitmap(filePath);
            OsbSprite background = GetLayer("first_glowing_bump").CreateSprite(filePath);

            background.Scale(startTime, (854.0 / backgroundBitmap.Width) * 1.2);
            background.Fade(startTime, 1);
            background.Fade(endTime, 0);

            double beatMultiplier = 1;

            for (double sTime = startTime; sTime < endTime - _beatDuration * 0.2; sTime += _beatDuration * beatMultiplier)
            {
                beatMultiplier = 1;

                if (
                    (75150 - 5 < sTime && sTime < 75411 - 5) ||
                    (83498 - 5 < sTime && sTime < 83759 - 5) ||
                    (100194 - 5 < sTime && sTime < 100454 - 5) ||
                    (251498 - 5 < sTime && sTime < 251759 - 5)
                )
                {
                    beatMultiplier = 0.25;
                }

                if (
                    (70976 - 5 < sTime && sTime < 71237 - 5) ||
                    (79324 - 5 < sTime && sTime < 79585 - 5) ||
                    (87672 - 5 < sTime && sTime < 87933 - 5) ||
                    (91846 - 5 < sTime && sTime < 92107 - 5) ||
                    (96020 - 5 < sTime && sTime < 96280 - 5) ||
                    (223324 - 5 < sTime && sTime < 223585 - 5) ||
                    (227498 - 5 < sTime && sTime < 227759 - 5) ||
                    (231672 - 5 < sTime && sTime < 231933 - 5) ||
                    (233498 - 5 < sTime && sTime < 233759 - 5) ||
                    (240020 - 5 < sTime && sTime < 240281 - 5) ||
                    (244194 - 5 < sTime && sTime < 244454 - 5) ||
                    (248367 - 5 < sTime && sTime < 248628 - 5) ||
                    (250715 - 5 < sTime && sTime < 251498 - 5)
                )
                {
                    beatMultiplier = 0.5;
                }

                if (
                    (74367 - 5 < sTime && sTime < 75150 - 5) ||
                    (82715 - 5 < sTime && sTime < 83498 - 5)
                )
                {
                    beatMultiplier = 0.75;
                }

                background.Fade(sTime, sTime + _beatDuration * beatMultiplier, 0.5, 0.25);
            }

            MoveBackground(ref background, startTime, endTime, 12f, Math.PI / 120, 24);
        }

        private void AddLeadMelodyBackground(string filePath, double startTime, double endTime)
        {
            Bitmap backgroundBitmap = GetMapsetBitmap(filePath);
            OsbSprite background = GetLayer("lead_melody").CreateSprite(filePath);

            background.Scale(startTime, (854.0 / backgroundBitmap.Width) * 1.2);
            background.Fade(startTime, 0.6);
            background.Fade(endTime, 0);

            MoveBackground(ref background, startTime, endTime, 12f, Math.PI / 120, 24);
        }

        private void AddMelodyBackground(string filePath, double startTime, double endTime)
        {
            Bitmap backgroundBitmap = GetMapsetBitmap(filePath);
            OsbSprite background = GetLayer("lead_melody").CreateSprite(filePath);

            background.Scale(startTime, (854.0 / backgroundBitmap.Width) * 1.2);
            background.Fade(startTime, 0.6);
            background.Fade(endTime, 0);

            MoveBackground(ref background, startTime, endTime, 12f, Math.PI / 120, 24);
        }

        private void AddLastSectionBackground(string filePath, double startTime, double endTime)
        {
            Bitmap backgroundBitmap = GetMapsetBitmap(filePath);
            OsbSprite background = GetLayer("last_section").CreateSprite(filePath);

            background.Scale(startTime, (854.0 / backgroundBitmap.Width) * 1.2);
            background.Fade(startTime, 0.5);
            background.Fade(endTime, 0);

            MoveBackground(ref background, startTime, endTime, 12f, Math.PI / 120, 24);
        }

        private void AddBeatmapCreditsBackground(string filePath, double startTime, double endTime)
        {
            Bitmap backgroundBitmap = GetMapsetBitmap(filePath);
            OsbSprite background = GetLayer("tournament_credits").CreateSprite(filePath);

            background.Scale(startTime, (854.0 / backgroundBitmap.Width));
            background.Scale(OsbEasing.InSine, startTime + _beatDuration * 16, startTime + _beatDuration * 32, (854.0 / backgroundBitmap.Width),
                (854.0 / backgroundBitmap.Width) * 1.5);
            background.Fade(startTime, 0.32);
            background.Fade(endTime, 0);
        }

        private void AddTournamentCreditsBackground(string filePath, double startTime, double endTime)
        {
            Bitmap backgroundBitmap = GetMapsetBitmap(filePath);
            OsbSprite background = GetLayer("tournament_credits").CreateSprite(filePath);

            background.Scale(startTime, (854.0 / backgroundBitmap.Width));
            background.Fade(startTime, 0.32);
            background.Fade(endTime, 0);
        }

        private void MoveBackground(ref OsbSprite sprite, double startTime, double endTime, float moveRadius, double rotateAmount, double beatMultiplier = 4)
        {
            double duration = _beatDuration * beatMultiplier;
            int loopCount = (int)Math.Ceiling((endTime - startTime) / duration);

            Vector2 startPosition = new Vector2(320 - moveRadius, 240 - moveRadius);
            Vector2 endPosition = new Vector2(320 + moveRadius, 240 + moveRadius);

            sprite.StartLoopGroup(startTime, loopCount);
            sprite.Move(OsbEasing.InOutSine, 0, duration * 0.5, startPosition, endPosition);
            sprite.Move(OsbEasing.InOutSine, duration * 0.5, duration, endPosition, startPosition);
            sprite.Rotate(OsbEasing.InOutSine, 0, duration * (1 / 3.0), -rotateAmount * 0.5, -rotateAmount);
            sprite.Rotate(OsbEasing.InOutSine, duration * (1 / 3.0), duration * (2 / 3.0), -rotateAmount, rotateAmount);
            sprite.Rotate(OsbEasing.InOutSine, duration * (2 / 3.0), duration, rotateAmount, -rotateAmount * 0.5);
            sprite.EndGroup();
        }

        private void AddParallaxBackground(
            ref OsbSprite sprite,
            double startTime,
            double endTime,
            double beatDivisor = 8,
            double moveAmount = 0.1,
            OsbEasing easing = OsbEasing.InSine
        )
        {
            OsuHitObject previousHitObject = Beatmap.HitObjects.FirstOrDefault();
            foreach (OsuHitObject hitObject in Beatmap.HitObjects)
            {
                if (hitObject.StartTime < startTime - 5 || endTime - 5 <= hitObject.StartTime)
                {
                    previousHitObject = hitObject;
                    continue;
                }

                Vector2 oldVec = previousHitObject.PositionAtTime(previousHitObject.EndTime);
                Vector2 oldPos = GetTrackedLocation(oldVec, (float)moveAmount);
                Vector2 newVec = hitObject.PositionAtTime(hitObject.StartTime);
                Vector2 newPos = GetTrackedLocation(newVec, (float)moveAmount);

                sprite.Move(easing, previousHitObject.EndTime, hitObject.StartTime, oldPos.X, oldPos.Y, newPos.X,
                    newPos.Y);

                if (hitObject is OsuSlider)
                {
                    double timestep = Beatmap.GetTimingPointAt((int)hitObject.StartTime).BeatDuration / beatDivisor;
                    double starttime = hitObject.StartTime;
                    while (true)
                    {
                        double endtime = starttime + timestep;

                        bool isCompleted = hitObject.EndTime - endtime < 5;
                        if (isCompleted)
                        {
                            endtime = hitObject.EndTime;
                        }

                        oldVec = hitObject.PositionAtTime(starttime);
                        oldPos = GetTrackedLocation(oldVec, (float)moveAmount);
                        newVec = hitObject.PositionAtTime(endtime);
                        newPos = GetTrackedLocation(newVec, (float)moveAmount);

                        sprite.Move(easing, starttime + 1, endtime, oldPos.X, oldPos.Y, newPos.X, newPos.Y);

                        if (isCompleted)
                        {
                            break;
                        }

                        starttime += timestep;
                    }
                }

                previousHitObject = hitObject;
            }
        }

        private void AddSolidBackground(string layerName, double startTime, double endTime, string color)
        {
            OsbSprite solid = GetLayer(layerName).CreateSprite("sb/p.png");

            solid.ScaleVec(startTime, 854, 480);
            solid.Color(startTime, color);
            solid.Fade(startTime, 1);
            solid.Fade(endTime, 0);
        }

        private Vector2 GetTrackedLocation(Vector2 position, float moveAmount)
        {
            return new Vector2(
                -(_center.X - position.X) * moveAmount + _center.X,
                -(_center.Y - position.Y) * moveAmount + _center.Y
            );
        }

        private double GetBeatDuration(double time)
            => Beatmap.GetTimingPointAt((int)time).BeatDuration;

        private double GetOffset(double time)
            => Beatmap.GetTimingPointAt((int)time).Offset;
    }
}
