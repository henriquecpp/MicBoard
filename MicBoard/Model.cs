using System;

namespace MicBoard
{
    [Serializable]
    public class Model
    {
        public string FileName { get; set; }
        public string Directory { get; set; }
        public string Duration { get; set; }
        public string KeyShortcut { get; set; } = "";
        public string TriggerSum { get; set; } = "";
    }
}
