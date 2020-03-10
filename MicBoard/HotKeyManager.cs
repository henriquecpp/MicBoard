using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace MicBoard
{
    class HotKeyManager
    {
        [DllImport("user32.dll")]
        private static extern int ToUnicode(uint virtualKeyCode, uint scanCode,
            byte[] keyboardState,
            [Out, MarshalAs(UnmanagedType.LPWStr, SizeConst = 64)]
            StringBuilder receivingBuffer,
            int bufferSize, uint flags);
        
        public string Translate(int[] keyArr)
        {            
            StringBuilder buf = new StringBuilder(256);
            byte[] keyboardState = new byte[256];
            StringBuilder result = new StringBuilder();
            foreach (int key in keyArr)
            {
                if (String.IsNullOrEmpty(result.ToString()))
                {
                    //convertendo o enum para uma list de inteiro e verificando se contem a key passada como param
                    //caso sim, sera pegado o nome da key através do seu valor contida no enum
                    if (Enum.GetValues(typeof(KeyEnum)).Cast<int>().Contains(key)) result.Append( ((KeyEnum)key).ToString() );
                    else if (((Keys)key).ToString().Contains("NumPad") || ((Keys)key).ToString().Contains("Win")) result.Append( ((Keys)key).ToString() );
                    else
                    {
                        ToUnicode((uint)key, 0, keyboardState, buf, 256, 0);
                        result.Append(buf.ToString().ToUpper());
                        buf.Clear();
                    }
                }
                else
                {
                    //convertendo o enum para uma list de inteiro e verificando se contem a key passada como param
                    //caso sim, sera pegado o nome da key através do seu valor contida no enum
                    if (Enum.GetValues(typeof(KeyEnum)).Cast<int>().Contains(key)) result.Append("+" + ((KeyEnum)key).ToString() );
                    else if (((Keys)key).ToString().Contains("NumPad") || ((Keys)key).ToString().Contains("Win")) result.Append("+" + ((Keys)key).ToString() );
                    else
                    {
                        ToUnicode((uint)key, 0, keyboardState, buf, 256, 0);
                        result.Append("+" + buf.ToString().ToUpper()); ;
                        buf.Clear();
                    }
                }
            }
            return result.ToString();
        }

        public Dictionary<string, string> Sort(string text)
        {
            string[] arr = text.Split('+');
            Dictionary<int, int> modifiers = new Dictionary<int, int>();
            //verificar enum de ModifiersKeys em http://www.pinvoke.net/default.aspx/user32/RegisterHotKey.html
            int sumModifiers = 0, anotherKey = 0;
            foreach (string value in arr)
            {
                Keys currKey = (Keys)Enum.Parse(typeof(Keys), value);
                string KeyString = currKey.ToString();
                if ( KeyString.Contains("Control") )
                {
                    modifiers.Add((int)(Keys)currKey, 1);
                    sumModifiers += 2;
                }
                else if ( KeyString.Contains("Menu") )
                {
                    modifiers.Add((int)(Keys)currKey, 2);
                    sumModifiers += 1;
                }
                else if( KeyString.Contains("Shift") )
                {
                    modifiers.Add((int)(Keys)currKey, 3);
                    sumModifiers += 4;
                }
                else if( KeyString.Contains("Win") )
                {
                    modifiers.Add((int)(Keys)currKey, 4);
                    sumModifiers += 8;
                }
                //se nao houver nenhuma tecla modificadora, sera colocado em ultimo (por padrao, qualquer tecla que nao seja modificadora, deve ficar por último)
                else
                {
                    if (modifiers.ContainsValue(5))
                    {
                        modifiers.Remove(modifiers.First(v => v.Value == 5).Key);
                        modifiers.Add((int)(Keys)currKey, 5);
                        anotherKey = (int)(Keys)currKey;
                    }
                    else
                    {
                        modifiers.Add((int)(Keys)currKey, 5);
                        anotherKey = (int)(Keys)currKey;
                    }
                }
            }
            /*convertendo o dicionário para um lista, pra poder organizar a ordem das teclas pressionadas de acordo com os exemplos de  
            "Guidelines for Keyboard User Interface Design" ( CTRL + ALT + SHIFT + WIN + (Any other key) )*/
            //link: https://docs.microsoft.com/pt-br/previous-versions/windows/desktop/dnacc/guidelines-for-keyboard-user-interface-design
            List<KeyValuePair<int, int>> list = modifiers.ToList();

            //ordenando a lista. Note  que nas condicionais acima, foi colocado o número junto as keys de acordo com o nível de prioridade na ordem
            list.Sort((x, y) => x.Value.CompareTo(y.Value));

            //passando o mesmo dicionario, porém na ordem correta
            modifiers = list.ToDictionary(x => x.Key, y => y.Value);
            text = string.Join("+", modifiers.Keys.ToArray());
            //MessageBox.Show(text+" - "+sumModifiers + "+" + anotherKey);
            return new Dictionary<string, string>() { { text, (sumModifiers + "+" + anotherKey) } };
        }

        public static List<int> EnumToList()
        {
            return Enum.GetValues(typeof(KeyEnum)).OfType<int>().ToList();
        }

        
    }
}
