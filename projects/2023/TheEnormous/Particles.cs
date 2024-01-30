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
    public class Particles : StoryboardObjectGenerator
    {
        private double _beatDuration;

        private const int ParticleCount = 96;
        private const double Lifetime = 3000;
        private const double Scale = 0.2;

        private const double SpawnSpread = 240;
        private const double Angle = 0;
        private const double AngleSpread = 60;
        private const double Speed = 460;

        public override void Generate()
        {
            _beatDuration = GetBeatDuration(0);

            GenerateMovingUpParticles(280, 48280, _beatDuration * 12, 72, "sb/d2.png", 0.03f);
            GenerateMovingUpParticles(50367, 64976, _beatDuration * 8, 72, "sb/d2.png", 0.03f);
            GenerateMovingUpParticles(67063, 83759, _beatDuration * 6, 72, "sb/d2.png", 0.03f);
            GenerateMovingUpParticles(83759, 100454, _beatDuration * 4, 72, "sb/d2.png", 0.03f);
            GenerateMovingUpParticles(100454, 115063, _beatDuration * 12, 72, "sb/d2.png", 0.03f);
            GenerateMovingUpParticles(117150, 132802, _beatDuration * 10, 72, "sb/d2.png", 0.03f);
            GenerateMovingUpParticles(133846, 150541, _beatDuration * 8, 72, "sb/d2.png", 0.03f);
            GenerateMovingUpParticles(150541, 192281, _beatDuration * 12, 72, "sb/d2.png", 0.03f);
            GenerateMovingUpParticles(192281, 217324, _beatDuration * 10, 72, "sb/d2.png", 0.03f);
            GenerateMovingUpParticles(219411, 234020, _beatDuration * 4, 72, "sb/d2.png", 0.03f);
            GenerateMovingUpParticles(236107, 251759, _beatDuration * 4, 72, "sb/d2.png", 0.03f);
            GenerateMovingUpParticles(252802, 269498, _beatDuration * 12, 72, "sb/d2.png", 0.03f);
            GenerateMovingUpParticles(269498, 285150, _beatDuration * 3, 96, "sb/d2.png", 0.03f);
            GenerateMovingUpParticles(286194, 294541, _beatDuration * 2, 128, "sb/d2.png", 0.03f);
            GenerateMovingUpParticles(294541, 298715, _beatDuration * 8, 96, "sb/d2.png", 0.03f);
            GenerateMovingUpParticles(298715, 302889, _beatDuration * 2, 128, "sb/d2.png", 0.03f);
            GenerateMovingUpParticles(302889, AudioDuration + _beatDuration * 4, _beatDuration * 12, 72, "sb/d2.png", 0.03f);

            GenerateMovingUpParticles(280, 48280, _beatDuration * 12, 36, "sb/c3.png", 0.025f, 0.25);
            GenerateMovingUpParticles(50367, 64976, _beatDuration * 8, 36, "sb/c3.png", 0.025f, 0.25);
            GenerateMovingUpParticles(67063, 83759, _beatDuration * 6, 36, "sb/c3.png", 0.025f, 0.25);
            GenerateMovingUpParticles(83759, 100454, _beatDuration * 4, 36, "sb/c3.png", 0.025f, 0.25);
            GenerateMovingUpParticles(100454, 115063, _beatDuration * 12, 36, "sb/c3.png", 0.025f, 0.25);
            GenerateMovingUpParticles(117150, 132802, _beatDuration * 10, 36, "sb/c3.png", 0.025f, 0.25);
            GenerateMovingUpParticles(133846, 150541, _beatDuration * 8, 36, "sb/c3.png", 0.025f, 0.25);
            GenerateMovingUpParticles(150541, 192281, _beatDuration * 12, 36, "sb/c3.png", 0.025f, 0.25);
            GenerateMovingUpParticles(192281, 217324, _beatDuration * 10, 36, "sb/c3.png", 0.025f, 0.25);
            GenerateMovingUpParticles(219411, 234020, _beatDuration * 4, 36, "sb/c3.png", 0.025f, 0.25);
            GenerateMovingUpParticles(236107, 251759, _beatDuration * 4, 36, "sb/c3.png", 0.025f, 0.25);
            GenerateMovingUpParticles(252802, 269498, _beatDuration * 12, 72, "sb/c3.png", 0.025f, 0.25);
            GenerateMovingUpParticles(269498, 285150, _beatDuration * 3, 72, "sb/c3.png", 0.025f, 0.25);
            GenerateMovingUpParticles(286194, 294541, _beatDuration * 2, 64, "sb/c3.png", 0.025f, 0.25);
            GenerateMovingUpParticles(294541, 298715, _beatDuration * 8, 72, "sb/c3.png", 0.025f, 0.25);
            GenerateMovingUpParticles(298715, 302889, _beatDuration * 2, 64, "sb/c3.png", 0.025f, 0.25);
            GenerateMovingUpParticles(302889, AudioDuration + _beatDuration * 4, _beatDuration * 12, 36, "sb/c3.png", 0.025f, 0.25);

            GenerateMovingParticles(83759 - _beatDuration * 2, 100454, "sb/d2.png");
            GenerateMovingParticles(219411 - _beatDuration * 2, 251759, "sb/d2.png");
            GenerateMovingParticles(269498 - _beatDuration * 2, 294541, "sb/d2.png");

            GenerateBumpingParticles(67063, 100454);
            GenerateBumpingParticles(219411, 251759);
        }

        private void GenerateMovingUpParticles(
            double startTime,
            double endTime,
            double particleDuration,
            int particleCount,
            string filePath = "sb/d.png",
            float scale = 0.02f,
            double opacity = 1.0
        )
        {
            Bitmap bitmap = GetMapsetBitmap(filePath);
            float bitmapScale = bitmap.Height * scale;

            using (OsbSpritePool pool = new OsbSpritePool(GetLayer("particles"), filePath, OsbOrigin.Centre, (sprite, sTime, eTime) =>
                   {
                       // Hide sprite if their start time go before real start time
                       if (sTime < startTime)
                       {
                           sprite.Fade(sTime, 0);
                           sprite.Fade(startTime, opacity);
                       }
                       else
                       {
                           sprite.Fade(sTime, opacity);
                       }

                       // Hide sprite if their end time cross real end time
                       if (eTime > endTime)
                       {
                           sprite.Fade(endTime, 0);
                       }
                   }))
            {
                double timeStep = particleDuration / particleCount;

                for (double sTime = startTime - (particleDuration + _beatDuration * 12); sTime < endTime; sTime += timeStep)
                {
                    var moveSpeed = Random(240, 360);
                    var eTime = sTime + Math.Ceiling(480f / moveSpeed) * particleDuration;
                    var sprite = pool.Get(sTime, eTime);

                    Color4 color = Beatmap.ComboColors.ElementAt(Random(Beatmap.ComboColors.Count()));
                    var startX = Random(-107, 747f);

                    sprite.MoveX(sTime, eTime, startX, startX + Random(-50, 50f));
                    sprite.MoveY(OsbEasing.InSine, sTime, eTime, 480 + bitmapScale, -bitmapScale);
                    sprite.Scale(sTime, Random(scale, scale * 2.5));
                    sprite.Color(sTime, color);
                    sprite.Additive(sTime, eTime);
                }
            }
        }

        private void GenerateMovingParticles(double startTime, double endTime, string filePath = "sb/d.png")
        {
            Bitmap bitmap = GetMapsetBitmap(filePath);

            double duration = endTime - startTime;
            int loopCount = Math.Max(1, (int)Math.Floor(duration / Lifetime));

            var layer = GetLayer("moving");
            for (var i = 0; i < ParticleCount; i++)
            {
                var spawnAngle = Random(Math.PI * 2);
                var spawnDistance = (float)(SpawnSpread * Math.Sqrt(Random(1f)));

                var moveAngle = MathHelper.DegreesToRadians(Angle + Random(-AngleSpread, AngleSpread) * 0.5f);
                var moveDistance = Speed * Lifetime * 0.001f;

                var startPosition = new Vector2(-110, 240) + new Vector2((float)Math.Cos(spawnAngle), (float)Math.Sin(spawnAngle)) * spawnDistance;
                var endPosition = startPosition + new Vector2((float)Math.Cos(moveAngle), (float)Math.Sin(moveAngle)) * (float)moveDistance;

                var loopDuration = duration / loopCount;
                var sTime = startTime + (i * loopDuration) / ParticleCount;
                var eTime = sTime + loopDuration * loopCount;

                if (!isVisible(bitmap, startPosition, endPosition, (float)loopDuration))
                    continue;

                Color4 color = Beatmap.ComboColors.ElementAt(Random(Beatmap.ComboColors.Count()));

                var particle = layer.CreateSprite(filePath);

                particle.Scale(sTime, Random(Scale * 0.25, Scale));
                particle.Color(sTime, color);
                particle.Additive(sTime, eTime);

                particle.StartLoopGroup(sTime, loopCount);
                particle.Fade(OsbEasing.Out, 0, loopDuration * 0.2, 0, color.A);
                particle.Fade(OsbEasing.In, loopDuration * 0.8, loopDuration, color.A, 0);
                particle.Move(OsbEasing.OutCubic, 0, loopDuration, startPosition, endPosition);
                particle.EndGroup();
            }
        }


        private void GenerateBumpingParticles(double startTime, double endTime)
        {
            double beatDuration = GetBeatDuration(startTime);
            double offset = GetOffset(startTime);

            using (OsbSpritePool pool = new OsbSpritePool(GetLayer("bumping_particles"), "sb/p.png", OsbOrigin.Centre, (sprite, sTime, eTime) =>
                   {
                       sprite.Scale(sTime, Random(10f, 20f));

                       if (sTime < startTime)
                       {
                           sprite.Fade(sTime, 0);
                           sprite.Fade(startTime, Random(0.2f, 0.9f));
                       }
                       else
                       {
                           sprite.Fade(sTime, Random(0.2f, 0.9f));
                       }

                       if (eTime > endTime - beatDuration)
                       {
                           sprite.Fade(endTime + _beatDuration * 4, 0);
                       }
                   }))
            {
                double sTime = startTime - beatDuration * 8;

                while (sTime <= endTime - beatDuration * 4)
                {
                    if (234020 < sTime && sTime < 236107)
                    {
                        sTime += _beatDuration;
                        continue;
                    }

                    for (int i = 0; i < Random(4, 10); i++)
                    {
                        float baseSpeed = Random(40f, 120f);
                        double eTime = sTime + Math.Ceiling(960f / baseSpeed) * _beatDuration;

                        OsbSprite sprite = pool.Get(sTime, eTime);
                        Color4 color = Beatmap.ComboColors.ElementAt(Random(Beatmap.ComboColors.Count()));

                        float moveSpeed = baseSpeed;
                        double currentTime = sTime + (sTime - offset) % _beatDuration;

                        sprite.MoveX(sTime, -127);
                        sprite.MoveY(sTime, Random(-10, 490));
                        sprite.Color(sTime, color);
                        sprite.Additive(sTime, eTime);

                        while (sprite.PositionAt(currentTime).X < 767)
                        {
                            if (234020 < currentTime && currentTime < 236107)
                            {
                                currentTime += _beatDuration;
                                continue;
                            }


                            var position = sprite.PositionAt(currentTime);
                            var rotation = sprite.RotationAt(currentTime);

                            if (currentTime > endTime - beatDuration * 4)
                            {
                                if (currentTime + 5 >= 99411)
                                {
                                    sprite.MoveX(OsbEasing.InBack, currentTime, currentTime + _beatDuration * 3, position.X, 320);
                                    sprite.MoveY(OsbEasing.InBack, currentTime, currentTime + _beatDuration * 3, position.Y, 240);

                                    Vector2 newPosition = new Vector2(
                                        Constant.Center.X * 747 * (float)Math.Cos(Random(Math.PI / 2)),
                                        Constant.Center.Y * 747 * (float)Math.Sin(Random(Math.PI / 2))
                                    );

                                    sprite.MoveX(OsbEasing.OutExpo, currentTime + _beatDuration * 3, currentTime + _beatDuration * 7, 320, newPosition.X);
                                    sprite.MoveY(OsbEasing.OutExpo, currentTime + _beatDuration * 3, currentTime + _beatDuration * 7, 240, newPosition.Y);
                                }

                                break;
                            }

                            sprite.MoveX(OsbEasing.OutExpo, currentTime, currentTime + _beatDuration, position.X, position.X + moveSpeed);
                            sprite.Rotate(OsbEasing.OutExpo, currentTime, currentTime + _beatDuration, rotation, rotation + Math.PI * 0.25f);

                            currentTime += _beatDuration;
                        }
                    }

                    sTime += _beatDuration;
                }
            }
        }

        private bool isVisible(Bitmap bitmap, Vector2 startPosition, Vector2 endPosition, float duration)
        {
            var spriteSize = new Vector2(bitmap.Width * (float)Scale, bitmap.Height * (float)Scale);
            var originVector = OsbSprite.GetOriginVector(OsbOrigin.Centre, spriteSize.X, spriteSize.Y);

            for (var t = 0; t < duration; t += 200)
            {
                var position = Vector2.Lerp(startPosition, endPosition, t / duration);
                if (OsbSprite.InScreenBounds(position, spriteSize, 0, originVector))
                    return true;
            }

            return false;
        }

        private double GetBeatDuration(double time)
            => Beatmap.GetTimingPointAt((int)time).BeatDuration;

        private double GetOffset(double time)
            => Beatmap.GetTimingPointAt((int)time).Offset;
    }
}
