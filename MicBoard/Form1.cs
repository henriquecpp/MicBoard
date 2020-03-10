﻿using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace MicBoard
{
    public partial class Form1 : Form
    {        
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);
        //Usando código não gerenciado para poder tornar os movimentos da janela mais suave
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        protected override void WndProc(ref Message m)
        {
            const int RESIZE_HANDLE_SIZE = 10;
            
            if (m.Msg == 0x0312)
            {    // Trap WM_HOTKEY
                                
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    string currValue = dataGridView1.Rows[i].Cells[5].Value.ToString();
                    if ($"{((int)m.LParam & 0xFFFF)}+{(((int)m.LParam >> 16) & 0xFFFF)}" == currValue)
                    {
                        AudioIn.Play(dataGridView1.Rows[i].Cells[2].Value.ToString(), float.Parse(VolumeIn.Value.ToString()) / 100);
                        AudioOut.Play(dataGridView1.Rows[i].Cells[2].Value.ToString(), float.Parse(VolumeOut.Value.ToString()) / 100);
                    }                        

                }
                return;
            }

            switch (m.Msg)
            {//Todas as definicoes para cada caso em https://docs.microsoft.com/en-us/windows/win32/inputdev/wm-nchittest
                case 0x0084:
                    base.WndProc(ref m);

                    if ((int)m.Result == 0x01)
                    {
                        Point screenPoint = new Point(m.LParam.ToInt32());
                        Point clientPoint = this.PointToClient(screenPoint);
                        if (clientPoint.Y <= RESIZE_HANDLE_SIZE)
                        {
                            if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                m.Result = (IntPtr)13;
                            else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                m.Result = (IntPtr)12;
                            else
                                m.Result = (IntPtr)14;
                        }
                        else if (clientPoint.Y <= (Size.Height - RESIZE_HANDLE_SIZE))
                        {
                            if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                m.Result = (IntPtr)10;
                            else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                m.Result = (IntPtr)2;
                            else
                                m.Result = (IntPtr)11;
                        }
                        else
                        {
                            if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                m.Result = (IntPtr)16;
                            else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                m.Result = (IntPtr)15;
                            else
                                m.Result = (IntPtr)17;
                        }
                    }
                    return;
            }
            base.WndProc(ref m);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style |= 0x20000;
                return cp;
            }
        }
        public Form1()
        {
            InitializeComponent();
            //carregando a classe que renderiza as cores do menu
            menuStrip1.Renderer = new ToolStripProfessionalRenderer(new MenuColorRender());
            fillGridView();
            MountTrigger();
            
        }
        //eventos
        private void btnClose_Click(object sender, EventArgs e)
        {
            AudioIn.Stop();
            AudioOut.Stop();
            Application.Exit();            
        }

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            //para maximizar sem ocultar a taskbar do windows
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            WindowState = FormWindowState.Maximized;
            //quando o botao de maximizar é clicado, o botao de normalizar a janela é colocado na frente e vice-versa
            btnRestore.BringToFront();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            btnMaximize.BringToFront();
        }

        private void myTitleBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                btnMaximize.BringToFront();
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void adicionarSomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Title = "Procurar arquivo de audio";
            openFileDialog1.Filter = "Audio (*.mp3,*.m4a) | *.mp3; *.m4a";
            var result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                new DataManager().Save(openFileDialog1.SafeFileName, openFileDialog1.FileName);
                fillGridView();
            }
        }

        public void fillGridView()
        {
            dataGridView1.Rows.Clear();
            List<Model> dataList = new DataManager().List();
            StringBuilder sb = new StringBuilder("");
            for (int i=0; i<dataList.Count;i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = i + 1;
                dataGridView1.Rows[i].Cells[1].Value = dataList.ElementAt(i).FileName;
                dataGridView1.Rows[i].Cells[2].Value = dataList.ElementAt(i).Directory;
                dataGridView1.Rows[i].Cells[3].Value = dataList.ElementAt(i).Duration;

                if (String.IsNullOrEmpty(dataList.ElementAt(i).KeyShortcut))
                    dataGridView1.Rows[i].Cells[4].Value = "";
                else
                    dataGridView1.Rows[i].Cells[4].Value = new HotKeyManager().Translate(Array.ConvertAll(dataList.ElementAt(i).KeyShortcut.Split('+'), int.Parse));
                
                dataGridView1.Rows[i].Cells[5].Value = dataList.ElementAt(i).TriggerSum;
                sb.Clear();
            }
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DataGridView.HitTestInfo testResult = dataGridView1.HitTest(e.X, e.Y);

                if (testResult.RowIndex >= 0)
                {
                    dataGridView1.ClearSelection();
                    dataGridView1.Rows[testResult.RowIndex].Selected = true;

                    ContextMenuStrip m = new ContextMenuStrip();
                    ToolStripMenuItem tm;

                    tm = new ToolStripMenuItem("Reproduzir", null);
                    m.Items.Add(tm);

                    tm = new ToolStripMenuItem("Adicionar Tecla de atalho", null, (sender2, e2) => GetKeys(sender2, e2, (DataGridView.HitTestInfo) testResult) );
                    m.Items.Add(tm);

                    tm = new ToolStripMenuItem("Deletar", null, (sender3, e3) => DeleteItem(sender3, e3, (DataGridView.HitTestInfo)testResult) );
                    m.Items.Add(tm);
                    //cor do texto
                    m.ForeColor = Color.FromArgb(230, 230, 230);
                    m.Renderer = new ToolStripProfessionalRenderer(new MenuColorRender());

                    m.Show(dataGridView1, new Point(e.X, e.Y));
                }               

            }
        }

        private void DeleteItem(object sender3, EventArgs e3, DataGridView.HitTestInfo result)
        {
            int i = int.Parse(dataGridView1.Rows[result.RowIndex].Cells[0].Value.ToString()) - 1;
            new DataManager().Delete(i);
            fillGridView();
        }

        public void GetKeys(Object sender, EventArgs e, DataGridView.HitTestInfo result)
        {
            //Interaction.InputBox("Insira uma ou mais teclas", "Definir teclas de atalho", "");
            Dictionary<string, string> input = InputBox.ShowDialog("Insira a(s) tecla(s) de atalho:");
            if (input == null || String.IsNullOrEmpty(input.Last().Key) || String.IsNullOrEmpty(input.Last().Value)) return;
            int i = int.Parse(dataGridView1.Rows[result.RowIndex].Cells[0].Value.ToString())-1;
            new DataManager().Update(i, input.Last().Key, input.Last().Value);
            fillGridView();
            MountTrigger();
        }

        public void MountTrigger()
        {            

            for(int i=0;i<dataGridView1.Rows.Count;i++)
            {
                string currValue = dataGridView1.Rows[i].Cells[5].Value.ToString();
                //MessageBox.Show(currValue);
                if (!String.IsNullOrEmpty(currValue))
                {
                    RegisterHotKey(this.Handle, int.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString()), int.Parse(currValue.Split('+')[0]), int.Parse(currValue.Split('+')[1]) );
                }
                currValue = null;
            }
        }

        private void VolumeOut_Scroll(object sender, EventArgs e)
        {
            if(AudioOut.Speaker != null)
                AudioOut.Speaker.Volume = float.Parse(VolumeOut.Value.ToString()) / 100;
        }

        private void VolumeIn_Scroll(object sender, EventArgs e)
        {
            if (AudioIn.Microphone != null)
                AudioIn.Microphone.Volume = float.Parse(VolumeIn.Value.ToString()) / 100;
            
        }
    }
}
