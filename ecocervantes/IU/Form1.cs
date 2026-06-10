using System;
using System.Linq;
using System.Windows.Forms;
using MongoDB.Driver;

namespace ecocervantes
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnRegistro_Click(object sender, EventArgs e)
        {
            var db = MongoDBConexion.GetDB();
            var usuarios = db.GetCollection<Usuario>("usuarios");

            if (!txtPassword.Text.Any(char.IsLetter) || !txtPassword.Text.Any(char.IsDigit))
            {
                MessageBox.Show("Contraseña debe tener letras y números");
                return;
            }

            usuarios.InsertOne(new Usuario
            {
                Nombre = txtCorreo.Text,
                Correo = txtCorreo.Text,
                Telefono = "000",
                Password = txtPassword.Text
            });

            MessageBox.Show("Usuario registrado ✅");
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var db = MongoDBConexion.GetDB();
            var usuarios = db.GetCollection<Usuario>("usuarios");

            var user = usuarios.Find(x => x.Correo == txtCorreo.Text).FirstOrDefault();

            if (user != null && user.Password == txtPassword.Text)
            {
                MessageBox.Show("Bienvenido ✅");

                FormDashboard f = new FormDashboard();
                f.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Datos incorrectos ❌");
            }
        }
    }
}