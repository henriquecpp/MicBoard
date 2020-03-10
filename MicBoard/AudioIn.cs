using NAudio.Wave;

namespace MicBoard
{
    class AudioIn
    {
        public static WaveOut Microphone = null;
        private static AudioFileReader audioFile = null;
        public static void Play(string path, float volume)
        {            
            audioFile = new AudioFileReader(path);

            Stop();
            Microphone = new WaveOut();
            Microphone.DeviceNumber = 1;

            Microphone.DesiredLatency = 700;
            Microphone.NumberOfBuffers = 3;
            Microphone.Volume = volume;

            Microphone.Init(audioFile);

            Microphone.Play();;
        }

        public static void Stop()
        {
            if (Microphone != null)
            {
                Microphone.Stop();
                Microphone.Dispose();
                Microphone = null;
            }
        }

    }
}
