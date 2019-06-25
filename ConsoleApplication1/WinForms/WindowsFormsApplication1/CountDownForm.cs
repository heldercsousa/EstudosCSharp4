using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EstudosCSharp.Delegates;

namespace WindowsFormsApplication1
{
    public partial class CountDownForm : Form
    {
        private CountDownEvent2 _countDown;

        public CountDownForm()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            nupSeconds.Enabled = false;

            _countDown = new CountDownEvent2((uint)nupSeconds.Value);
            _countDown.Tick += _countDown_Tick;
            _countDown.Finished += _countDown_Finished;
            _countDown.Start();
        }

        /// <summary>
        /// this.Invoke() permite que a Thread do Windows.Forms consiga trabalhar com uma outra Thread existente dentro do CountDownEvent. Sem este Invoke, o sistema gera um erro em tempo de execução.
        /// Ao usar o Invoke, a thread do CountDown é  executada dentro da Thread do UI da WIndows Form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _countDown_Finished(object sender, EventArgs e)
        {
            this.Invoke(new Action( () => { 
                MessageBox.Show("Countdown finished!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                _countDown.Finished -= _countDown_Finished;
                _countDown.Tick -= _countDown_Tick;

                lblTime.Text = "";
                btnStart.Enabled = true;
                nupSeconds.Enabled = true;
            }));
        }

        void _countDown_Tick(object sender, CountDownEvent2.TickEventArgs e)
        {
            this.Invoke(new Action( () => {
                lblTime.Text = string.Format("{0} seconds left", e.Seconds);
            }
            ));
        }
    }
}
