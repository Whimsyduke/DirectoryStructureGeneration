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
                mCount = value;
            }
        }

        public ListViewItem_Base()
        {
            InitializeComponent();
        }

        public ListViewItem_Base(FileInfo file)
        {
            InitializeComponent();
            mFile = file;
            mCount = 0;
        }
    }
}
