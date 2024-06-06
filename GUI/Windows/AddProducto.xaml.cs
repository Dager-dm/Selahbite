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

namespace GUI.Pages
{
    /// <summary>
    /// Lógica de interacción para AddProducto.xaml
    /// </summary>
    public partial class AddProducto : Window
    {

        public bool guardarPresionado = false;
        public Producto ProductoPropiety { get; set; }
        public Producto ProductoModified { get; set; }

        private int accion = 0;
        public AddProducto(List<CategoriasProductos> categories)
        {
            InitializeComponent();
            cboCategoria.ItemsSource = categories;
            
        }

        public AddProducto(Producto OldProducto, List<CategoriasProductos> categories)
        {
            InitializeComponent();

            cboCategoria.ItemsSource = categories;
            lblTitulo.Content = "Editar Plato/Bebida";
            txtboxNombre.Text = OldProducto.Nombre;
            txtboxValor.Text = OldProducto.Valor.ToString();
            cboCategoria.SelectedItem = OldProducto.Categoria.Nombre;
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
            guardarPresionado = true;
            if (accion == 0)
            {
                ProductoPropiety = new Producto();
                ProductoPropiety.Nombre=txtboxNombre.Text.ToString();
                ProductoPropiety.Valor = float.Parse(txtboxValor.Text);
                ProductoPropiety.Categoria=(CategoriasProductos)cboCategoria.SelectedItem;


            }
            else
            {
                ProductoModified.Nombre=txtboxNombre.Text.ToString();
                ProductoModified.Valor=float.Parse(txtboxValor.Text);
                ProductoModified.Categoria=(CategoriasProductos)cboCategoria.SelectedItem;

            }

            Close();

        }


    }
}
