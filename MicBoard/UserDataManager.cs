using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MicBoard
{
    class UserDataManager
    {
        string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MicBoardUserSettings.xml";
        public void SavePreferences(UserPrefModel u)
        {
            if (File.Exists(docPath)) Update(u);
            else
            {
                XDocument doc = new XDocument(new XElement("UserPreferences", new XElement("Volume", 
                    new XElement("SpeakerVolume", u.SpeakerVolume), new XElement("MicVolume", u.MicVolume))));
                doc.Save(docPath);
            }
        }

        public void Update(UserPrefModel u)
        {
            XDocument doc = XDocument.Load(docPath);
            //como os elementos não são uma sequencia, é preciso especificar qual tag deve-se alterar
            doc.Element("UserPreferences").Element("Volume").Element("SpeakerVolume").Value = u.SpeakerVolume.ToString();
            doc.Element("UserPreferences").Element("Volume").Element("MicVolume").Value = u.MicVolume.ToString();

            doc.Save(docPath);
        }

        public UserPrefModel LoadVolumeSettings()
        {
            UserPrefModel u = new UserPrefModel();

            if (!File.Exists(docPath)) return null;

            foreach (XElement level in XElement.Load(docPath).Elements("Volume"))
            {
                u.SpeakerVolume = int.Parse(level.Element("SpeakerVolume").Value);
                u.MicVolume = int.Parse(level.Element("MicVolume").Value);
            }

            return u;
        }
    }
}
