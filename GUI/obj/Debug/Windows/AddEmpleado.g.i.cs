﻿#pragma checksum "..\..\..\Windows\AddEmpleado.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "C12ACD907DD96111EA9F51544E1DEF232405B63E9F4774A96B0C698616620310"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using GUI.Pages;
using GUI.Styles;
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


namespace GUI.Pages {
    
    
    /// <summary>
    /// AddEmpleado
    /// </summary>
    public partial class AddEmpleado : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 31 "..\..\..\Windows\AddEmpleado.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblTitulo;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\Windows\AddEmpleado.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnClose;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\Windows\AddEmpleado.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtboxNombre;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\Windows\AddEmpleado.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtboxId;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\..\Windows\AddEmpleado.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtboxTelefono;
        
        #line default
        #line hidden
        
        
        #line 84 "..\..\..\Windows\AddEmpleado.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cboCargo;
        
        #line default
        #line hidden
        
        
        #line 88 "..\..\..\Windows\AddEmpleado.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton btnSave;
        
        #line default
        #line hidden
        
        
        #line 90 "..\..\..\Windows\AddEmpleado.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.Popup Popup;
        
        #line default
        #line hidden
        
        
        #line 91 "..\..\..\Windows\AddEmpleado.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal GUI.Styles.UserPopup Header;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/GUI;component/windows/addempleado.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Windows\AddEmpleado.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.lblTitulo = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            
            #line 33 "..\..\..\Windows\AddEmpleado.xaml"
            ((System.Windows.Controls.Border)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Border_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btnClose = ((System.Windows.Controls.Button)(target));
            
            #line 34 "..\..\..\Windows\AddEmpleado.xaml"
            this.btnClose.Click += new System.Windows.RoutedEventHandler(this.btnClose_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.txtboxNombre = ((System.Windows.Controls.TextBox)(target));
            
            #line 39 "..\..\..\Windows\AddEmpleado.xaml"
            this.txtboxNombre.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtbox_TextChanged);
            
            #line default
            #line hidden
            
            #line 39 "..\..\..\Windows\AddEmpleado.xaml"
            this.txtboxNombre.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.Alphabetic_PreviewKeyDown);
            
            #line default
            #line hidden
            return;
            case 5:
            this.txtboxId = ((System.Windows.Controls.TextBox)(target));
            
            #line 53 "..\..\..\Windows\AddEmpleado.xaml"
            this.txtboxId.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtbox_TextChanged);
            
            #line default
            #line hidden
            
            #line 53 "..\..\..\Windows\AddEmpleado.xaml"
            this.txtboxId.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.Numeric_PreviewKeyDown);
            
            #line default
            #line hidden
            return;
            case 6:
            this.txtboxTelefono = ((System.Windows.Controls.TextBox)(target));
            
            #line 67 "..\..\..\Windows\AddEmpleado.xaml"
            this.txtboxTelefono.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtbox_TextChanged);
            
            #line default
            #line hidden
            
            #line 67 "..\..\..\Windows\AddEmpleado.xaml"
            this.txtboxTelefono.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.Numeric_PreviewKeyDown);
            
            #line default
            #line hidden
            return;
            case 7:
            this.cboCargo = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 8:
            this.btnSave = ((System.Windows.Controls.RadioButton)(target));
            
            #line 88 "..\..\..\Windows\AddEmpleado.xaml"
            this.btnSave.Click += new System.Windows.RoutedEventHandler(this.AddEmpleadoButton_Click_1);
            
            #line default
            #line hidden
            return;
            case 9:
            this.Popup = ((System.Windows.Controls.Primitives.Popup)(target));
            return;
            case 10:
            this.Header = ((GUI.Styles.UserPopup)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

