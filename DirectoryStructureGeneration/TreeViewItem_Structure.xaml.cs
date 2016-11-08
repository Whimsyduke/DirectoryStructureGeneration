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
    /// TreeViewItem_Structure.xaml 的交互逻辑
    /// </summary>
    public partial class TreeViewItem_Structure : TreeViewItem
    {
        private string mHeader;
        private bool mIsChecked;
        private bool mIsFile;
        private bool mIsVisible;
        private string mStructFile;
        private string mMatchFile;
        private string mPartPath;
        private string mBaseFilePath;
        private string mBaseStructPath;
        private int mChildVisibleCount;
        private TreeViewItem_Structure mParent;
        private Grid mGrid_Header;
        private CheckBox mCheckBox_Header;
        private Label mLabel_Header;

        public string MHeader
        {
            get
            {
                return mHeader;
            }

            set
            {
                mLabel_Header.Content = value;
                mHeader = value;
            }
        }

        public bool MIsChecked
        {
            get
            {
                return mIsChecked;
            }

            set
            {
                mCheckBox_Header.IsChecked = value;
                mIsChecked = value;
            }
        }

        public bool MIsFile
        {
            get
            {
                return mIsFile;
            }

            set
            {
                mIsFile = value;
            }
        }

        public bool MIsVisible
        {
            get
            {
                return mIsVisible;
            }

            set
            {
                if (value)
                {
                    if (mParent != null) mParent.MChildVisibleCount++;
                    Visibility = Visibility.Visible;
                }
                else
                {
                    if (mParent != null) mParent.MChildVisibleCount--;
                    Visibility = Visibility.Collapsed;
                }
                mIsVisible = value;
            }
        }

        public string MStructFile
        {
            get
            {
                return mStructFile;
            }

            set
            {
                mStructFile = value;
            }
        }

        public string MMatchFile
        {
            get
            {
                return mMatchFile;
            }

            set
            {
                mMatchFile = value;
            }
        }

        public string MPartPath
        {
            get
            {
                return mPartPath;
            }

            set
            {
                mPartPath = value;
            }
        }

        public string MBaseFilePath
        {
            get
            {
                return mBaseFilePath;
            }

            set
            {
                mBaseFilePath = value;
            }
        }

        public string MBaseStructPath
        {
            get
            {
                return mBaseStructPath;
            }

            set
            {
                mBaseStructPath = value;
            }
        }

        public TreeViewItem_Structure MParent
        {
            get
            {
                return mParent;
            }

            set
            {
                mParent = value;
            }
        }

        public int MChildVisibleCount
        {
            get
            {
                return mChildVisibleCount;
            }

            set
            {
                mChildVisibleCount = value;
            }
        }

        public TreeViewItem_Structure()
        {
            InitializeComponent();
            mGrid_Header = FindResource("Grid_Header") as Grid;
            mCheckBox_Header = FindResource("CheckBox_Header") as CheckBox;
            mLabel_Header = FindResource("Label_Header") as Label;
            mGrid_Header.Children.Add(mCheckBox_Header);
            mGrid_Header.Children.Add(mLabel_Header);
            MIsChecked = false;
            MHeader = "";
        }

        public TreeViewItem_Structure(bool isFile, string header, string baseFilePath, string baseStructPath, string partPath, TreeViewItem_Structure parent)
        {
            InitializeComponent();
            mGrid_Header = FindResource("Grid_Header") as Grid;
            mCheckBox_Header = FindResource("CheckBox_Header") as CheckBox;
            mLabel_Header = FindResource("Label_Header") as Label;
            mGrid_Header.Children.Add(mCheckBox_Header);
            mGrid_Header.Children.Add(mLabel_Header);
            mIsFile = isFile;
            MHeader = header;
            mBaseFilePath = baseFilePath;
            mBaseStructPath = baseStructPath;
            mPartPath = partPath + "\\" + header;
            mStructFile = mBaseStructPath + partPath + "\\" + header;
            mMatchFile = mBaseFilePath + partPath + "\\" + header;
            mParent = parent;
            mChildVisibleCount = 0;
            mIsChecked = false;
            DirectoryInfo dir = new DirectoryInfo(mStructFile);
            if (!isFile)
            {
                DirectoryInfo[] childDir = dir.GetDirectories();
                foreach (DirectoryInfo select in childDir)
                {
                    Items.Add(new TreeViewItem_Structure(false, select.Name, baseFilePath, baseStructPath, mPartPath, this));
                }
                FileInfo[] childFile = dir.GetFiles();
                foreach (FileInfo select in childFile)
                {
                    Items.Add(new TreeViewItem_Structure(true, select.Name, baseFilePath, baseStructPath, mPartPath, this));
                }
            }
        }
    }
}
