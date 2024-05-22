using BLL;
using ENTITY;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI.Pages
{
    /// <summary>
    /// Lógica de interacción para Deudas.xaml
    /// </summary>
    public partial class Deudas : Page
    {
        public Deudas()
        {
            InitializeComponent();
            serviciodeuda = new ServicioDeuda();
            miListView.ItemsSource = serviciodeuda.GetAllDeudas();
            miListView.SetValue(ScrollViewer.HorizontalScrollBarVisibilityProperty, ScrollBarVisibility.Disabled);
        }

        private void NewEmployee(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            AddEmpleado addEmpleadoWindow = new AddEmpleado();
            addEmpleadoWindow.Owner = mainWindow;
            addEmpleadoWindow.ShowDialog();
            serviciodeuda.AddDeuda(addDeudaWindow.DeudaPropiety);
            Refreshlistview();
        }
        private void ListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var listView = sender as ListView;
            var gridView = listView.View as GridView;
            var width = listView.ActualWidth / gridView.Columns.Count;
            foreach (var column in gridView.Columns)
            {
                column.Width = width;
            }
        }
        public void Refreshlistview()
        {
            miListView.ItemsSource = null;
            miListView.ItemsSource = serviciodeuda.GetAllDeudas();
        }

        //private void btnEditar_Click(object sender, RoutedEventArgs e)
        //{
        //    Button btnEditar = sender as Button;
        //    if (btnEditar != null)
        //    {
        //        ListViewItem listViewItem = FindAncestor<ListViewItem>(btnEditar);
        //        if (listViewItem != null)
        //        {

        //            Empleado item = listViewItem.DataContext as Empleado;
        //            if (item != null)
        //            {

        //                MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
        //                AddEmpleado addEmpleadoWindow = new AddEmpleado(item);
        //                addEmpleadoWindow.Owner = mainWindow;
        //                addEmpleadoWindow.ShowDialog();
        //                servicioempleado.EditEmpleado(addEmpleadoWindow.EmpleadoPropiety, addEmpleadoWindow.EmpleadoModified);
        //                Refreshlistview();

        //            }
        //        }
        //    }
        //}

        //private void btnBorrar_Click(object sender, RoutedEventArgs e)
        //{
        //    Button btnBorrar = sender as Button;
        //    if (btnBorrar != null)
        //    {
        //        ListViewItem listViewItem = FindAncestor<ListViewItem>(btnBorrar);
        //        if (listViewItem != null)
        //        {

        //            Empleado item = listViewItem.DataContext as Empleado;
        //            if (item != null)
        //            {


        //                MiMessageBox messageBox = new MiMessageBox("¿Está seguro de borrar\n" + " el Empleado " + item.Nombre + "?");
        //                bool? resultado = messageBox.ShowDialog();

        //                if (resultado == true)
        //                {
        //                    servicioempleado.DeleteEmpleado(item);
        //                    Refreshlistview();
        //                }
        //            }
        //        }
        //    }
        //}

        private T FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }

        private void TxtBusqueda_TextChanged(object sender, TextChangedEventArgs e)
        {

            string filtro = txbBusqueda.Text.ToLower();
            List<Deudas> deudas = serviciodeuda.GetAllDeudas();


            List<Deudas> deudasFiltrados = deudas.Where(c => c.Nombre.ToLower().Contains(filtro)).ToList();


            miListView.ItemsSource = deudasFiltrados;
        }
    }
}
