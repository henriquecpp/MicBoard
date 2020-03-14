using NAudio.Wave;

namespace MicBoard
{
    class AudioOut
    {
        
        public static WaveOut Speaker = null;
        public static AudioFileReader AudioFile = null;
        public static void Play(string path, float volume)
        {            
            Stop();

            AudioFile = new AudioFileReader(path);

            Speaker = new WaveOut();
            Speaker.DesiredLatency = 700;
            Speaker.NumberOfBuffers = 3;
            Speaker.Volume = volume;

            Speaker.Init(AudioFile);            

            Speaker.Play();
        }

        public static void Stop()
        {
            if (Speaker != null)
            {
                Speaker.Stop();
                Speaker.Dispose();
                Speaker = null;
                AudioFile = null;
            }
        }
    }
}
