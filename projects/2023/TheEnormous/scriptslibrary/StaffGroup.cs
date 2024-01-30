using OpenTK;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using StorybrewCommon.Subtitles;
using StorybrewCommon.Util;

namespace StorybrewCommon.Util
{
    public class StaffGroup
    {
        public string Group { get; private set; }
        public string[] Members { get; private set; }
        public Vector2 Position { get; private set; }

        public StaffGroup(string group, string[] members, Vector2 position)
        {
            Group = group;
            Members = members;
            Position = position;
        }
    }
}
