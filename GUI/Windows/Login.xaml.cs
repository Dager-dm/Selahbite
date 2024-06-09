using BLL;
using ENTITY;
using GUI.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GUI.Windows
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        ServicioUsuarios serviciousuarios=new ServicioUsuarios();
        public Login()
        {
            InitializeComponent();
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
          Usuarios usuario = new Usuarios();
          usuario.Username= txtUsername.Text;
          usuario.Password= txtPassword.Password;
          var message=serviciousuarios.Validar(usuario);
            if (serviciousuarios.Validado)
            {
                MiMessageBox messageBox = new MiMessageBox(AfirmativeMessage.A, message);
                messageBox.ShowDialog();
                LoginSucessfull();
            }
            else
            {
                MiMessageBox messageBox = new MiMessageBox(NegativeMessage.N, message);
                messageBox.ShowDialog();
                ForgetPasword.Visibility = Visibility.Visible;
            }
        }

        private void LoginSucessfull()
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void signupBtn_Click(object sender, RoutedEventArgs e)
        {
            if (title.Text != "Crear Cuenta")
            {
               
                title.Text = "Crear Cuenta";
                loginBtn.Visibility = Visibility.Hidden;
                txtPassword2.Visibility = Visibility.Visible;
            }
            else
            {
                title.Text = "Iniciar Sesión";
                loginBtn.Visibility = Visibility.Visible;
                txtPassword2.Visibility = Visibility.Hidden;
                CreateNewUser();
            }

        }

        private void CreateNewUser()
        {
            Usuarios usuario = new Usuarios();
            usuario.Username = txtUsername.Text;
            usuario.Password = txtPassword.Password;
            serviciousuarios.Insert(usuario);
            txtUsername.Text = "";
            txtPassword.Password = "";
        }

        private void ForgetPasword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("click");
        }
    }
}
