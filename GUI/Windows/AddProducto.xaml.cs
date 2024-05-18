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
using ENTITY;

namespace GUI.Pages
{
    /// <summary>
    /// Lógica de interacción para AddProducto.xaml
    /// </summary>
    public partial class AddProducto : Window
    {
        private List<string> categories;

        public Producto ProductoPropiety { get; set; }

        public Producto ProductoModified { get; set; }

        private int accion = 0;
        public AddProducto()
        {
            InitializeComponent();
            categories = new List<string> { "Corriente", "Gourmet", "Asado", "Comida Rapida", "Adicionales", "Bebidas", "Adicionales" };
            cboCategoria.ItemsSource = categories;
            
        }

        public AddProducto(Producto OldProducto)
        {
            InitializeComponent();
            categories = new List<string> { "Corriente", "Gourmet", "Asado", "Comida Rapida", "Adicionales", "Bebidas", "Adicionales" }; 
            cboCategoria.ItemsSource = categories;
            lblTitulo.Content = "Editar Plato/Bebida";
            txtboxNombre.Text = OldProducto.Nombre;
            txtboxId.Text = OldProducto.Id;
            txtboxValor.Text = OldProducto.Valor.ToString();
            cboCategoria.SelectedItem = OldProducto.Categoria;
            accion = 1;
            ProductoModified = new Producto();
            ProductoPropiety = OldProducto;
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    
        private void AddProductoButton_Click(object sender, RoutedEventArgs e)
        {
            if (accion == 0)
            {
                ProductoPropiety = new Producto();
                ProductoPropiety.Nombre=txtboxNombre.Text.ToString();
                ProductoPropiety.Id=txtboxId.Text.ToString();
                ProductoPropiety.Valor = float.Parse(txtboxValor.Text);
                ProductoPropiety.Categoria=cboCategoria.SelectedItem.ToString();

            }else
            {
                ProductoModified.Nombre=txtboxNombre.Text.ToString();
                ProductoModified.Id=txtboxId.Text.ToString();
                ProductoModified.Valor=float.Parse(txtboxValor.Text);
                ProductoModified.Categoria=cboCategoria.SelectedItem.ToString();

            }

            Close();

        }


    }
}
