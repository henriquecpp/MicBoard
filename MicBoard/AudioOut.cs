using NAudio.Wave;
using System;
using System.Linq;
using System.Windows.Forms;
using VideoLibrary;


namespace MicBoard
{
    class AudioOut
    {
        
        public static WaveOut Speaker = null;
        public static AudioFileReader AudioFile = null;
        public static MediaFoundationReader AudioWeb = null;
        public static void Play(string path, float volume)
        {            
            Stop();

            Speaker = new WaveOut();

            Speaker.Volume = volume;
            
            Uri uriTest;
            bool isURL = Uri.TryCreate(path, UriKind.Absolute, out uriTest) && (uriTest.Scheme == Uri.UriSchemeHttp || uriTest.Scheme == Uri.UriSchemeHttps);

            if (isURL)
            {                
                string source = YouTube.Default.GetAllVideos(path).FirstOrDefault(v => v.AudioBitrate >= 96).Uri;
                AudioWeb = new MediaFoundationReader(source);
                Speaker.Init(AudioWeb);
            }                
            else
            {
                AudioFile = new AudioFileReader(path);
                Speaker.Init(AudioFile);
            }
            
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
