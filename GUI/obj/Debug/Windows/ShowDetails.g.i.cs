// Updated by XamlIntelliSenseFileGenerator 7/06/2024 9:15:52 a. m.
#pragma checksum "..\..\..\Windows\ShowDetails.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "9B9709082DCC835F6BFF5740CDC5AF7B08FD9B0CDC1358459029E0A2FC3DEA40"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace GUI.Windows
{


    /// <summary>
    /// ShowDetails
    /// </summary>
    public partial class ShowDetails : System.Windows.Window, System.Windows.Markup.IComponentConnector
    {


#line 24 "..\..\..\Windows\ShowDetails.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnClose;

#line default
#line hidden


#line 27 "..\..\..\Windows\ShowDetails.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border InfoBorder;

#line default
#line hidden


#line 39 "..\..\..\Windows\ShowDetails.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border ListviewBorder;

#line default
#line hidden


#line 45 "..\..\..\Windows\ShowDetails.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView listviewProductos;

#line default
#line hidden


#line 97 "..\..\..\Windows\ShowDetails.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblTotalPedido;

#line default
#line hidden


#line 104 "..\..\..\Windows\ShowDetails.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cboMetodos;

#line default
#line hidden


#line 107 "..\..\..\Windows\ShowDetails.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblEectivo1;

#line default
#line hidden


#line 108 "..\..\..\Windows\ShowDetails.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border brdEfectivo1;

#line default
#line hidden


#line 109 "..\..\..\Windows\ShowDetails.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txt;

#line default
#line hidden


#line 112 "..\..\..\Windows\ShowDetails.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblCambio1;

#line default
#line hidden


#line 113 "..\..\..\Windows\ShowDetails.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblCambio2;

#line default
#line hidden


#line 114 "..\..\..\Windows\ShowDetails.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox Checkprint;

#line default
#line hidden


#line 115 "..\..\..\Windows\ShowDetails.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton btnConfirmarpago;

#line default
#line hidden

        private bool _contentLoaded;

        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (_contentLoaded)
            {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/GUI;component/windows/showdetails.xaml", System.UriKind.Relative);

#line 1 "..\..\..\Windows\ShowDetails.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);

#line default
#line hidden
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:

#line 23 "..\..\..\Windows\ShowDetails.xaml"
                    ((System.Windows.Controls.Border)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Border_MouseLeftButtonDown);

#line default
#line hidden
                    return;
                case 2:
                    this.btnClose = ((System.Windows.Controls.Button)(target));

#line 24 "..\..\..\Windows\ShowDetails.xaml"
                    this.btnClose.Click += new System.Windows.RoutedEventHandler(this.btnClose_Click);

#line default
#line hidden
                    return;
                case 3:
                    this.InfoBorder = ((System.Windows.Controls.Border)(target));
                    return;
                case 4:
                    this.ListviewBorder = ((System.Windows.Controls.Border)(target));
                    return;
                case 5:
                    this.listviewProductos = ((System.Windows.Controls.ListView)(target));
                    return;
                case 6:
                    this.lblTotalPedido = ((System.Windows.Controls.Label)(target));
                    return;
                case 7:
                    this.cboMetodos = ((System.Windows.Controls.ComboBox)(target));

#line 104 "..\..\..\Windows\ShowDetails.xaml"
                    this.cboMetodos.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cboMetodos_SelectionChanged);

#line default
#line hidden
                    return;
                case 8:
                    this.lblEectivo1 = ((System.Windows.Controls.Label)(target));
                    return;
                case 9:
                    this.brdEfectivo1 = ((System.Windows.Controls.Border)(target));
                    return;
                case 10:
                    this.txt = ((System.Windows.Controls.TextBox)(target));

#line 109 "..\..\..\Windows\ShowDetails.xaml"
                    this.txt.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtEfectivo_TextChanged);

#line default
#line hidden
                    return;
                case 11:
                    this.lblCambio1 = ((System.Windows.Controls.Label)(target));
                    return;
                case 12:
                    this.lblCambio2 = ((System.Windows.Controls.Label)(target));
                    return;
                case 13:
                    this.Checkprint = ((System.Windows.Controls.CheckBox)(target));
                    return;
                case 14:
                    this.btnConfirmarpago = ((System.Windows.Controls.RadioButton)(target));

#line 115 "..\..\..\Windows\ShowDetails.xaml"
                    this.btnConfirmarpago.Click += new System.Windows.RoutedEventHandler(this.btnConfirmarpago_Click);

#line default
#line hidden
                    return;
            }
            this._contentLoaded = true;
        }

        internal System.Windows.Controls.RadioButton rbnContado;
        internal System.Windows.Controls.RadioButton rbnCredito;
    }
}
