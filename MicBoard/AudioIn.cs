using NAudio.Wave;

namespace MicBoard
{
    class AudioIn
    {
        public static WaveOut Microphone = null;
        public static AudioFileReader AudioFile = null;
        public static void Play(string path, float volume)
        {            
            Stop();
            
            AudioFile = new AudioFileReader(path);

            Microphone = new WaveOut();
            Microphone.DeviceNumber = 1;

            Microphone.DesiredLatency = 700;
            Microphone.NumberOfBuffers = 3;
            Microphone.Volume = volume;

            Microphone.Init(AudioFile);

            Microphone.Play();;
        }

        public static void Stop()
        {
            if (Microphone != null)
            {
                Microphone.Stop();
                Microphone.Dispose();
                Microphone = null;
                AudioFile = null;
            }
        }

    }
}
