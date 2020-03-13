using NAudio.Wave;

namespace MicBoard
{
    class AudioOut
    {
        
        public static WaveOut Speaker = null;
        public static AudioFileReader audioFile = null;
        public static void Play(string path, float volume)
        {            
            audioFile = new AudioFileReader(path);

            Stop();
            Speaker = new WaveOut();
            Speaker.DesiredLatency = 700;
            Speaker.NumberOfBuffers = 3;
            Speaker.Volume = volume;

            Speaker.Init(audioFile);            

            Speaker.Play();
        }

        public static void Stop()
        {
            if (Speaker != null)
            {
                Speaker.Stop();
                Speaker.Dispose();
                Speaker = null;
                audioFile = null;
            }
        }
    }
}
