using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MicBoard
{
    class InputBox
    {
        static TextBox textBox;
        static string dicKey = "";
        static string dicValue = "";
        static Dictionary<string, string> KeyValues = new Dictionary<string, string>();
        static List<int> keysPressed = new List<int>();
        public static Dictionary<string, string> ShowDialog(string title)
        {
            Form prompt = new Form()
            {
                Width = 500,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = title,
                StartPosition = FormStartPosition.CenterScreen
            };
            textBox = new TextBox() { Left = 50, Top = 30, Width = 400 };
            textBox.KeyDown += new KeyEventHandler(KeyCapture);
            textBox.KeyUp += new KeyEventHandler(KeyRelease);
            textBox.ReadOnly = true;

            Button clear = new Button() { Left = 50, Width = 100, Top = 70, Text = "Clear" };
            clear.Click += (sender, e) => { textBox.Text = ""; ClearAll(); textBox.Focus();};

            Button comfirm = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 70, DialogResult = DialogResult.OK };
            comfirm.Click += (sender, e) => { prompt.Close(); };

            prompt.MaximizeBox = false;
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(comfirm);
            prompt.Controls.Add(clear);
            prompt.AcceptButton = comfirm;
            prompt.FormClosed += (sender, e) => { ClearAll(); };
            
            return prompt.ShowDialog() == DialogResult.OK ? new Dictionary<string, string>() { { dicKey.ToString(), dicValue.ToString() } } : null;
        }


        private static void KeyCapture(object sender, KeyEventArgs e)
        {
            int key = (int)(Keys)(e.KeyCode);
            
            if (!keysPressed.Contains(key))
            {
                
                keysPressed.Add(key);

                KeyValues = new HotKeyManager().Sort(string.Join("+", keysPressed.ToArray()));
                dicKey = KeyValues.Last().Key;
                dicValue = KeyValues.Last().Value;                
                textBox.Text = new HotKeyManager().Translate(Array.ConvertAll( KeyValues.First().Key.Split('+'), int.Parse) );
                
            }            

        }
        private static void KeyRelease(object sender, KeyEventArgs e)
        {
            int key = (int)(Keys)(e.KeyCode);
            
            keysPressed.Remove(key);
            

        }

        private static void ClearAll()
        {
            KeyValues.Clear();
            keysPressed.Clear();
        }
        
    }
}
