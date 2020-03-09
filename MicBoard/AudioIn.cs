using NAudio.Wave;

namespace MicBoard
{
    class AudioIn
    {
        private static WaveOut Mic = null;
        private static AudioFileReader audioFile = null;
        public static void Play(string path)
        {            
            audioFile = new AudioFileReader(path);

            Stop();
            Mic = new WaveOut();
            Mic.DeviceNumber = 1;

            Mic.DesiredLatency = 700;
            Mic.NumberOfBuffers = 3;
            Mic.Volume = 0.5f;

            Mic.Init(audioFile);

            Mic.Play();;
        }

        public static void Stop()
        {
            if (Mic != null)
            {
                Mic.Stop();
                Mic.Dispose();
                Mic = null;
            }
        }

    }
}
