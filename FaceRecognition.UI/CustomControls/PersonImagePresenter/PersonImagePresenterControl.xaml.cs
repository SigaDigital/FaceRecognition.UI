using FaceRecognition.UI.CustomControls.PersonImagePresenter;
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
        #region Dependency Properties
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
            set { SetValue(RenderModeProperty, (HeaderMode)value); }
        }

        public static readonly DependencyProperty RenderModeProperty =
            DependencyProperty.Register("RenderMode", typeof(HeaderMode),
              typeof(PersonImagePresenterControl), new PropertyMetadata(HeaderMode.VIEW));

        public ObservableCollection<string> ImagePaths
        {
            get { return (ObservableCollection<string>)GetValue(ImagePathsProperty); }
            set { SetValue(ImagePathsProperty, value); }
        }

        public static readonly DependencyProperty ImagePathsProperty =
            DependencyProperty.Register("ImagePaths", typeof(ObservableCollection<string>),
            typeof(PersonImagePresenterControl), new PropertyMetadata(null));

        public bool HasChanged
        {
            get { return (bool)GetValue(HasChangedProperty); }
            set { SetValue(HasChangedProperty, value); }
        }

        public static readonly DependencyProperty HasChangedProperty =
            DependencyProperty.Register("HasChanged", typeof(bool),
              typeof(PersonImagePresenterControl), new PropertyMetadata(false));

        #endregion

        #region Event
        public delegate void RemoveImageEventHandler(object sender, RemoveImageEventArgs e);
        public event RemoveImageEventHandler RemoveImageHandler;
        #endregion

        public PersonImagePresenterControl()
        {
            InitializeComponent();
        }

        #region Button Handlers
        private void ConfirmButtonClicked(object sender, RoutedEventArgs e)
        {
            HasChanged = true;

        }

        private void CancelButtonClicked(object sender, RoutedEventArgs e)
        {
            HasChanged = false;
        }

        private void RemoveImageClicked(object sender, RoutedEventArgs e)
        {
            var selectedImagePath = ImageContainer.SelectedItem as string;
            var selectedImageIndex = ImageContainer.SelectedIndex;

            if (this.ImagePaths != null) 
            {
                var selectedObject = this.ImagePaths.Where(x => x == selectedImagePath).FirstOrDefault();

                RemoveImageEventArgs args = new RemoveImageEventArgs { ImagePath = selectedImagePath, ItemIndex = selectedImageIndex };
                
                if (RemoveImageHandler != null)
                {
                    RemoveImageHandler(sender, args);
                }
            }
        }
        #endregion
    }
}
