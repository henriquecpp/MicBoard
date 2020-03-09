using NAudio.Wave;

namespace MicBoard
{
    class AudioOut
    {
        
        private static WasapiOut Speaker = null;
        private static AudioFileReader audioFile = null;
        public static void Play(string path)
        {            
            audioFile = new AudioFileReader(path);

            Stop();
            Speaker = new WasapiOut();

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
            }
        }
    }
}
