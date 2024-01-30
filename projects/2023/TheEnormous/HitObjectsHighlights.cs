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
    public class HitObjectsHighlights : StoryboardObjectGenerator
    {
        private double _beatDuration;
        private double _offset;

        public override void Generate()
        {
            _beatDuration = GetBeatDuration(0);
            _offset = GetOffset(0);

            GenerateGlowingHighlightWithDrawSlider(67063, 100454);
            GenerateGlowingHighlightWithDrawSlider(117150, 132802);
            GenerateGlowingHighlightWithDrawSlider(133846, 150672);
            GenerateGlowingHighlightWithDrawSlider(219411, 234020);
            GenerateGlowingHighlightWithDrawSlider(236107, 251759);
            GenerateGlowingHighlightWithDrawSlider(269498, 278889);
            GenerateGlowingHighlightWithDrawSlider(279933, 280976);
            GenerateGlowingHighlightWithDrawSlider(282020, 283063);
            GenerateGlowingHighlightWithDrawSlider(284107, 285150);
            GenerateGlowingHighlightWithDrawSlider(286194, 294541);
            GenerateGlowingHighlightWithDrawSlider(298713, 302889);

            GenerateMovingLineHighlight(33672, 48280);
            GenerateMovingLineHighlight(217324, 219411);
            GenerateMovingLineHighlight(298715, 302889);

            GenerateExplodingParticles(33672, 48280, "exploding", true, 8, _offset, 24, 48);
            GenerateExplodingParticles(33672, 48280, "exploding", true, 8, _offset + _beatDuration, 12, 24);
            GenerateExplodingParticles(33672, 48280, "exploding", true, 8, _offset + _beatDuration * 1.5, 24, 48);
            GenerateExplodingParticles(33672, 48280, "exploding", true, 8, _offset + _beatDuration * 2.5, 12, 24);
            GenerateExplodingParticles(33672, 48280, "exploding", true, 8, _offset + _beatDuration * 3, 24, 48);
            GenerateExplodingParticles(40976, 42020, "exploding", false, 8, _offset, 24, 48);
            GenerateExplodingParticles(117150, 132802, "exploding", true, 2, _offset, 24, 48);
            GenerateExplodingParticles(133846, 142194, "exploding", true, 2, _offset, 24, 48);
            GenerateExplodingParticles(192281, 200628, "exploding", true, 16, _offset, 24, 48);
            GenerateExplodingParticles(200628, 207933, "exploding", false, 8, _offset, 24, 48);
            GenerateExplodingParticles(208976, 217324, "exploding", false, 8, _offset, 24, 48);
            GenerateExplodingParticles(260107, 261150, "exploding", false, 8, _offset, 24, 48);
            GenerateExplodingParticles(261150, 268454, "exploding", true, 8, _offset, 24, 48);
            GenerateExplodingParticles(269498, 278889, "exploding", false, 8, _offset, 18, 36);
            GenerateExplodingParticles(279933, 280976, "exploding", false, 8, _offset, 18, 36);
            GenerateExplodingParticles(282020, 283063, "exploding", false, 8, _offset, 18, 36);
            GenerateExplodingParticles(284107, 285150, "exploding", false, 8, _offset, 18, 36);
            GenerateExplodingParticles(286194, 294541, "exploding", false, 8, _offset, 18, 36);
            GenerateExplodingParticles(298713, 302889, "exploding", false, 8, _offset, 24, 52);

            GenerateRingHighlight(83759, 99672, true, 1, _offset);
            GenerateRingHighlight(117150, 132802, true, 1, _offset);
            GenerateRingHighlight(133846, 148454, true, 2, _offset);
            GenerateRingHighlight(261150, 275065, true, 8, _offset);
            GenerateRingHighlight(269498, 278889, false, 8, _offset);
            GenerateRingHighlight(279933, 280976, false, 8, _offset);
            GenerateRingHighlight(282020, 283063, false, 8, _offset);
            GenerateRingHighlight(284107, 285150, false, 8, _offset);
            GenerateRingHighlight(286194, 295585, false, 8, _offset);
            GenerateRingHighlight(298713, 302889, false, 8, _offset);

            GenerateStrikeHighlight(20107, 28585, true, 16, _offset - _beatDuration * 4);
            GenerateStrikeHighlight(83759, 99672, true, 1, _offset);
            GenerateStrikeHighlight(117150, 132802, true, 1, _offset);
            GenerateStrikeHighlight(133846, 150020, true, 1, _offset);
            GenerateStrikeHighlight(278889, 279933, false, 8, _offset);
            GenerateStrikeHighlight(280976, 282020, false, 8, _offset);
            GenerateStrikeHighlight(283063, 284107, false, 8, _offset);
            GenerateStrikeHighlight(286194, 295585, false, 8, _offset);

            GenerateFunkyHighlight(115063, 117150, 8);
            GenerateFunkyHighlight(124454, 125498);
            GenerateFunkyHighlight(132802, 133846);
            GenerateFunkyHighlight(142194, 143237);
            GenerateFunkyHighlight(252802, 253846 );
            GenerateFunkyHighlight(254889, 255933);
            GenerateFunkyHighlight(256976, 258020);
            GenerateFunkyHighlight(259063, 260107);
        }

        private void GenerateGlowingHighlightWithDrawSlider(
            double startTime,
            double endTime,
            bool needBeat = false,
            double beatMultiplier = 4,
            int beatDivisor = 32
        )
        {
            _offset = GetOffset(startTime);

            using (OsbSpritePool pool = new OsbSpritePool(GetLayer("glowing_with_draw_slider"), "sb/hl.png",
                       OsbOrigin.Centre,
                       (sprite, poolStartTime, poolEndTime) => { }))
            {
                foreach (OsuHitObject hitObject in Beatmap.HitObjects)
                {
                    if (hitObject.StartTime < startTime - 5 || endTime - 5 < hitObject.StartTime)
                    {
                        continue;
                    }

                    if (needBeat && !IsNeededBeat(hitObject.StartTime, _offset, beatMultiplier))
                    {
                        continue;
                    }

                    double endtime = (hitObject is OsuSpinner ? hitObject.EndTime : hitObject.StartTime) +
                                     _beatDuration * 4;
                    OsbSprite sprite = pool.Get(hitObject.StartTime, endtime);

                    sprite.Move(hitObject.StartTime, hitObject.Position);
                    sprite.Scale(hitObject.StartTime, endtime, 0.5, 0.2);
                    sprite.Fade(hitObject.StartTime, hitObject.StartTime + _beatDuration / 4, 0, 1);
                    sprite.Fade(hitObject is OsuSpinner ? hitObject.EndTime : hitObject.StartTime + _beatDuration / 4,
                        endtime, 1, 0);
                    sprite.Additive(hitObject.StartTime, endtime);
                    sprite.Color(hitObject.StartTime, hitObject.Color);

                    if (hitObject is OsuSlider)
                    {
                        double timestep = Beatmap.GetTimingPointAt((int)hitObject.StartTime).BeatDuration / beatDivisor;
                        double start_time = hitObject.StartTime;
                        while (true)
                        {
                            double end_time = start_time + timestep;

                            bool isCompleted = hitObject.EndTime - end_time < 5;
                            if (isCompleted)
                            {
                                end_time = hitObject.EndTime;
                            }

                            var sliderSprite = pool.Get(start_time, start_time + _beatDuration * 4);
                            sliderSprite.Move(start_time, hitObject.PositionAtTime(start_time));
                            sliderSprite.Scale(start_time, start_time + _beatDuration * 4, 0.5, 0.2);
                            sliderSprite.Fade(start_time, start_time + _beatDuration / 4, 0, 1);
                            sliderSprite.Fade(start_time + _beatDuration / 4, start_time + _beatDuration * 4, 1, 0);
                            sliderSprite.Additive(start_time, start_time + _beatDuration * 4);
                            sliderSprite.Color(start_time, hitObject.Color);

                            if (isCompleted)
                            {
                                break;
                            }

                            start_time += timestep;
                        }
                    }
                }
            }
        }

        private void GenerateRingHighlight(
            double startTime,
            double endTime,
            bool needBeat = false,
            double beatMultiplier = 4,
            double offset = 0
        )
        {
            using (OsbSpritePool pool = new OsbSpritePool(GetLayer("rings"), "sb/c.png", OsbOrigin.Centre,
                       (sprite, poolStartTime, poolEndTime) => { sprite.Additive(poolStartTime, poolEndTime); }))
            {
                foreach (OsuHitObject hitObject in Beatmap.HitObjects)
                {
                    if (hitObject.StartTime < startTime - 5 || endTime - 5 < hitObject.StartTime)
                    {
                        continue;
                    }

                    if (needBeat && !IsNeededBeat(hitObject.StartTime, offset, beatMultiplier))
                    {
                        continue;
                    }

                    OsbSprite sprite = pool.Get(hitObject.StartTime, hitObject.StartTime + _beatDuration * 2);
                    sprite.Move(hitObject.StartTime, hitObject.Position);
                    sprite.Fade(hitObject.StartTime, hitObject.StartTime + _beatDuration * 2, 1, 0);
                    sprite.Scale(OsbEasing.Out, hitObject.StartTime, hitObject.StartTime + _beatDuration * 2, 0, 0.35);
                    sprite.Additive(hitObject.StartTime, hitObject.StartTime + _beatDuration * 2);
                }
            }
        }

        private void GenerateStrikeHighlight(
            double startTime,
            double endTime,
            bool needBeat = false,
            double beatMultiplier = 4,
            double offset = 0
        )
        {
            using (OsbSpritePool pool = new OsbSpritePool(GetLayer("strikes"), "sb/p.png", OsbOrigin.Centre,
                       (sprite, poolStartTime, poolEndTime) => { sprite.Additive(poolStartTime, poolEndTime); }))
            {
                foreach (OsuHitObject hitObject in Beatmap.HitObjects)
                {
                    if (hitObject.StartTime < startTime - 5 || endTime - 5 < hitObject.StartTime)
                    {
                        continue;
                    }

                    if (needBeat && !IsNeededBeat(hitObject.StartTime, offset, beatMultiplier))
                    {
                        continue;
                    }

                    OsbSprite sprite = pool.Get(hitObject.StartTime, hitObject.StartTime + _beatDuration * 2);
                    sprite.Fade(hitObject.StartTime, hitObject.StartTime + _beatDuration * 2, 1, 0);
                    sprite.ScaleVec(OsbEasing.Out, hitObject.StartTime, hitObject.StartTime + _beatDuration * 2, 4, 1500,
                        0, 1500);
                    sprite.Move(hitObject.StartTime, hitObject.StartTime + _beatDuration * 2, hitObject.Position,
                        hitObject.Position);
                    sprite.Rotate(hitObject.StartTime, Random(-Math.PI / 6, Math.PI / 6));
                    sprite.Additive(hitObject.StartTime, hitObject.StartTime + _beatDuration * 2);
                }
            }
        }

        private void GenerateExplodingParticles(
            double startTime,
            double endTime,
            string layer = "exploding",
            bool needBeat = false,
            double beatMultiplier = 4,
            double offset = 0,
            int minParticleCount = 16,
            int maxParticleCount = 32,
            double explodingRadius = 120,
            bool focus = false
        )
        {
            using (OsbSpritePool pool = new OsbSpritePool(GetLayer(layer), "sb/d.png", OsbOrigin.Centre,
                       (sprite, poolStartTime, poolEndTime) => { sprite.Additive(poolStartTime, poolEndTime); }))
            {
                pool.MaxPoolDuration = 10000;
                foreach (OsuHitObject hitObject in Beatmap.HitObjects)
                {
                    if (hitObject.StartTime < startTime - 5 || endTime - 5 < hitObject.StartTime)
                    {
                        continue;
                    }

                    if (needBeat && !IsNeededBeat(hitObject.StartTime, offset, beatMultiplier))
                    {
                        continue;
                    }

                    for (int i = 0; i < Random(minParticleCount, maxParticleCount + 1); i++)
                    {
                        double angle = Random(Math.PI * 2);
                        double radius = Random(explodingRadius * 0.1, explodingRadius);
                        Vector2 endPosition = hitObject.Position + new Vector2(
                            (float)(Math.Cos(angle) * radius),
                            (float)(Math.Sin(angle) * radius)
                        );

                        double particleDuration = Random(_beatDuration * 2, _beatDuration * 4);
                        double focusTime = 0;
                        if (focus)
                        {
                            focusTime = Random(_beatDuration * 2, _beatDuration * 4);
                        }

                        OsbSprite sprite = pool.Get(hitObject.StartTime,
                            hitObject.StartTime + particleDuration + focusTime);
                        sprite.Move(OsbEasing.OutExpo, hitObject.StartTime, hitObject.StartTime + particleDuration,
                            hitObject.Position, endPosition);

                        if (focus)
                        {
                            sprite.Move(OsbEasing.InExpo, hitObject.StartTime + particleDuration,
                                hitObject.StartTime + particleDuration + focusTime, endPosition, new Vector2(182, 156));
                            sprite.Scale(hitObject.StartTime, radius * Random(0.001, 0.0001));
                            sprite.Fade(hitObject.StartTime, 1);
                            sprite.Fade(OsbEasing.In, hitObject.StartTime + particleDuration,
                                hitObject.StartTime + particleDuration + focusTime, 1, 0);
                            sprite.Additive(hitObject.StartTime + particleDuration, hitObject.StartTime + particleDuration + focusTime);
                        }
                        else
                        {
                            sprite.Scale(hitObject.StartTime, hitObject.StartTime + particleDuration, radius * 0.001,
                                0);
                            sprite.Fade(hitObject.StartTime, hitObject.StartTime + particleDuration, 1, 0);
                        }
                    }
                }
            }
        }

        private void GenerateMovingLineHighlight(double startTime, double endTime)
        {
            OsbSprite line = GetLayer("moving_line").CreateSprite("sb/p.png");

            if (startTime == 217324)
            {
                line.ScaleVec(OsbEasing.Out, startTime, startTime + _beatDuration * 0.25, 0, 480, 16, 480);
            }
            else
            {
                line.ScaleVec(startTime, 16, 480);
            }

            line.Fade(startTime - 1, 0);
            line.Fade(startTime, 0.5);
            line.Fade(endTime, 0);
            line.Additive(startTime, endTime);

            OsuHitObject lastHitObject = Beatmap.HitObjects.First();

            foreach (var hitObject in Beatmap.HitObjects)
            {
                if (hitObject.StartTime < startTime - 5 || endTime - 5 <= hitObject.StartTime)
                {
                    lastHitObject = hitObject;
                    continue;
                }

                line.MoveX(lastHitObject.EndTime, hitObject.StartTime, lastHitObject.PositionAtTime(lastHitObject.EndTime).X, hitObject.Position.X);

                if (hitObject is OsuSlider)
                {
                    line.Fade(hitObject.StartTime, 1);
                    var timestep = _beatDuration / 8;
                    var starttime = hitObject.StartTime;
                    while (true)
                    {
                        var end_time = starttime + timestep;

                        var complete = hitObject.EndTime - end_time < 5;
                        if (complete) end_time = hitObject.EndTime;

                        var startPosition = line.PositionAt(starttime);
                        line.MoveX(starttime, end_time, startPosition.X, hitObject.PositionAtTime(end_time).X);

                        if (complete) break;
                        starttime += timestep;
                    }
                }

                lastHitObject = hitObject;
            }
        }

        private void GenerateFunkyHighlight(double startTime, double endTime, int count = 4)
        {
            OsuHitObject startHitObject = Beatmap.HitObjects.Where(x => startTime - 5 < x.StartTime && x.StartTime < startTime + 5).FirstOrDefault();
            OsuHitObject endHitObject = Beatmap.HitObjects.Where(x => endTime - 5 < x.StartTime && x.StartTime < endTime + 5).FirstOrDefault();

            for (int i = 0; i < 64; i++)
            {
                OsbSprite sprite = GetLayer("funky").CreateSprite("sb/p.png");

                double angle = Random(Math.PI * 2);
                double radius = Random(80f, 160f);
                Vector2 position = startHitObject.Position + new Vector2(
                    (float)(Math.Cos(angle) * radius),
                    (float)(Math.Sin(angle) * radius)
                );

                sprite.Move(OsbEasing.OutExpo, startHitObject.StartTime, startHitObject.StartTime + _beatDuration, startHitObject.Position, position);
                sprite.Move(OsbEasing.InCirc, endHitObject.StartTime - _beatDuration, endHitObject.StartTime, position, endHitObject.Position);
                sprite.Scale(startHitObject.StartTime, Random(12f, 18f));
                sprite.Fade(startHitObject.StartTime, Random(0.35f, 0.9f));
                sprite.Fade(endHitObject.StartTime, 0);
                sprite.Additive(startHitObject.StartTime, endHitObject.EndTime);

                double angle2 = 0;

                for (int j = 1; j < count; j++)
                {
                    sprite.Rotate(OsbEasing.OutExpo, startHitObject.StartTime + _beatDuration * j, startHitObject.StartTime + _beatDuration * (j + 1), angle2,
                        angle2 + Math.PI / 4);

                    angle2 += Math.PI / 4;
                }
            }
        }

        private bool IsNeededBeat(double time, double offset, double beatMultiplier)
        {
            double dist = (time - offset - _beatDuration * beatMultiplier) % (_beatDuration * beatMultiplier);
            return dist < 5 || dist > _beatDuration * beatMultiplier - 5;
        }

        public double GetBeatDuration(double time)
            => Beatmap.GetTimingPointAt((int)time).BeatDuration;

        public double GetOffset(double time)
            => Beatmap.GetTimingPointAt((int)time).Offset;
    }
}
