using NAudio.Wave;
using System;
using System.Linq;
using VideoLibrary;

namespace MicBoard
{
    class AudioIn
    {
        public static WaveOut Microphone = null;
        public static AudioFileReader AudioFile = null;
        public static MediaFoundationReader AudioWeb = null;
        public static void Play(string path, float volume)
        {            
            Stop();
            
            Microphone = new WaveOut();

            Microphone.DeviceNumber = 1;            
            Microphone.Volume = volume;

            Uri uriTest;
            bool isURL = Uri.TryCreate(path, UriKind.Absolute, out uriTest) && (uriTest.Scheme == Uri.UriSchemeHttp || uriTest.Scheme == Uri.UriSchemeHttps);

            if (isURL)
            {
                string source = YouTube.Default.GetAllVideos(path).FirstOrDefault(v => v.AudioFormat == AudioFormat.Aac && v.AdaptiveKind == AdaptiveKind.Audio).Uri;
                AudioWeb = new MediaFoundationReader(source);
                Microphone.Init(AudioWeb);
            }
            else
            {
                AudioFile = new AudioFileReader(path);
                Microphone.Init(AudioFile);
            }

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
                AudioWeb = null;
            }
        }

    }
}
