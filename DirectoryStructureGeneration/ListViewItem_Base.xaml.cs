using System;
using System.Collections.Generic;
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

namespace DirectoryStructureGeneration
{
    /// <summary>
    /// ListViewItem_Base.xaml 的交互逻辑
    /// </summary>
    public partial class ListViewItem_Base : ListViewItem
    {
        private FileInfo mFile;
        private int mCount;

        public FileInfo MFile
        {
            get
            {
                return mFile;
            }

            set
            {
                mFile = value;
            }
        }

        public int MCount
        {
            get
            {
                return mCount;
            }

            set
            {
                Label_Count.Content = value.ToString();
                mCount = value;
            }
        }

        public ListViewItem_Base()
        {
            InitializeComponent();
            AddMenu();
        }

        public ListViewItem_Base(FileInfo file)
        {
            InitializeComponent();
            mFile = file;
            mCount = 0;
            Label_Header.Content = file.Name;
            Label_Count.Content = mCount.ToString();
            AddMenu();
        }
        private void AddMenu()
        {
            MenuItem copyName = new MenuItem();
            copyName.Header = "复制文件名";
            copyName.Click += MenuItem_CoypName_Click;
            MenuItem copyPath = new MenuItem();
            copyPath.Header = "复制路径";
            copyPath.Click += MenuItem_CopyPath_Click;
            ContextMenu contextMenu = new ContextMenu();
            contextMenu.Items.Add(copyName);
            contextMenu.Items.Add(copyPath);
            ContextMenu = contextMenu;
        }

        private void MenuItem_CopyPath_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(mFile.FullName);
        }

        private void MenuItem_CoypName_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(mFile.Name);
        }
    }
}
