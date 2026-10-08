using System;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace TCPClient
{
    public partial class Form1 : Form
    {
        private TcpClient client;
        private NetworkStream stream;

        public Form1()
        {
            InitializeComponent();
            ConnectToServer();
        }

        private void ConnectToServer()
        {
            try
            {
                client = new TcpClient("127.0.0.1", 5000);
                stream = client.GetStream();

                textBox1.AppendText("Подключение к серверу установлено." + Environment.NewLine);
            }
            catch
            {
                MessageBox.Show("Не удалось подключиться к серверу.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SendMessage("Привет");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SendMessage("Час");
        }

        private void SendMessage(string message)
        {
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(message);

                stream.Write(data, 0, data.Length);

                byte[] buffer = new byte[1024];

                int bytes = stream.Read(buffer, 0, buffer.Length);

                string response = Encoding.UTF8.GetString(buffer, 0, bytes);

                textBox1.AppendText("Клиент: " + message + Environment.NewLine);
                textBox1.AppendText("Сервер: " + response + Environment.NewLine);
            }
            catch
            {
                MessageBox.Show("Ошибка соединения с сервером.");
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            stream?.Close();
            client?.Close();
        }
    }
}