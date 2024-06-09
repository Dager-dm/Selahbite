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
using BLL;
using ENTITY;
using GUI.Pages;

namespace GUI.Windows
{
    /// <summary>
    /// Lógica de interacción para AddEgresos.xaml
    /// </summary>
    public partial class AddEgresos : Window
    {
        
        public bool guardarPresionado = false;
        public Egreso egreso { get; set; }
        public AddEgresos()
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


        private void CreateEgreso()
        {
            egreso = new Egreso();
            egreso.Descripcion=txtboxDescripcion.Text;
            egreso.Fecha = DateTime.Now;
            egreso.Recibidor = txtboxRecibidor.Text;
            egreso.Valor=float.Parse(txtboxValor.Text);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
          guardarPresionado=true;
          CreateEgreso();
          Close();
        }
    }
}
