using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
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

namespace FaceRecognition.UI.CustomControls
{
    /// <summary>
    /// Interaction logic for PersonImagePresenterControl.xaml
    /// </summary>
    public partial class PersonImagePresenterControl : UserControl
    {
        public string PersonName
        {
            get { return (string)GetValue(PersonNameProperty); }
            set { SetValue(PersonNameProperty, value); }
        }

        public static readonly DependencyProperty PersonNameProperty =
            DependencyProperty.Register("PersonName", typeof(string),
              typeof(PersonImagePresenterControl), new PropertyMetadata(""));

        public int RenderMode
        {
            get { return (int)GetValue(RenderModeProperty); }
            set { SetValue(RenderModeProperty, value); }
        }

        public static readonly DependencyProperty RenderModeProperty =
            DependencyProperty.Register("RenderMode", typeof(int),
              typeof(PersonImagePresenterControl), new PropertyMetadata(0));

        public ObservableCollection<string> ImagePaths
        {
            get { return (ObservableCollection<string>)GetValue(ImagePathsProperty); }
            set { SetValue(ImagePathsProperty, value); }
        }

        public static readonly DependencyProperty ImagePathsProperty =
            DependencyProperty.Register("ImagePaths", typeof(ObservableCollection<string>),
            typeof(PersonImagePresenterControl), new PropertyMetadata(null));

        public PersonImagePresenterControl()
        {
            InitializeComponent();
        }
    }
}
