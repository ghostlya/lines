using System;
using System.Windows.Forms;

namespace linesk
{
    /// <summary>
    /// Диалоговое окно изменения имени игрока
    /// </summary>
    public partial class FormPlayerName : Form
    {
        public FormPlayerName()
        {
            InitializeComponent();
        }

        public string PlayerName = "Игрок";
        

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            PlayerName = textBoxName.Text;
        }

        private void FormPlayerName_Load(object sender, EventArgs e)
        {
            textBoxName.Text = PlayerName;          // Имя текущего игрока

        }
    }
}
