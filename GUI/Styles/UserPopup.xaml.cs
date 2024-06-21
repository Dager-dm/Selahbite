using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI.Styles
{
    /// <summary>
    /// Lógica de interacción para UserPopup.xaml
    /// </summary>
    public partial class UserPopup : UserControl
    {
        public UserPopup()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty NewBackgroundProperty = DependencyProperty.Register(
            "Background", typeof(Brush), typeof(UserPopup), new PropertyMetadata(default(Brush)));

        public Brush newBackground
        {
            get { return (Brush)GetValue(NewBackgroundProperty); }
            set { SetValue(NewBackgroundProperty, value); }
        }

        private void FadeInOutStoryboard_Completed(object sender, EventArgs e)
        {
            // Restablece la propiedad Opacity para permitir que la animación se ejecute de nuevo
            this.Opacity = 0;
           
            
        } 

    }
}
