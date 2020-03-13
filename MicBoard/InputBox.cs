using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MicBoard
{
    public partial class InputBox : Form
    {
        public Dictionary<string, string> KeyValues { get; set; }
        List<int> keysPressed = new List<int>();
        public InputBox()
        {
            InitializeComponent();
            this.MaximizeBox = false;
        }
        private void KeyCapture(object sender, KeyEventArgs e)
        {
            int key = (int)(Keys)(e.KeyCode);

            if (!keysPressed.Contains(key))
            {

                keysPressed.Add(key);

                KeyValues = new HotKeyManager().Sort(string.Join("+", keysPressed.ToArray()));
                
                InputField.Text = new HotKeyManager().Translate(Array.ConvertAll(KeyValues.First().Key.Split('+'), int.Parse));

            }

        }
        private void KeyRelease(object sender, KeyEventArgs e)
        {
            int key = (int)(Keys)(e.KeyCode);

            keysPressed.Remove(key);


        }

        private void ClearAll(object sender, EventArgs e)
        {
            KeyValues.Clear();
            keysPressed.Clear();
            InputField.Text = "";
            InputField.Focus();
        }

    }
}
