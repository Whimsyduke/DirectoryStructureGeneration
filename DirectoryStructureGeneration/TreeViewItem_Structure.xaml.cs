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
        private string mStructFile;
        private string mMatchFile;
        private string mPartPath;
        private string mBaseFilePath;
        private string mBaseStructPath;
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
                if (mIsChecked != value && mParent != null)
                {
                    mParent.ChildCheck();
                }
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
            AddMenu();
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
            AddMenu();
        }
        
        private void AddMenu()
        {
            MenuItem copyName = new MenuItem();
            copyName.Header = "复制文件名";
            copyName.Click += MenuItem_CoypName_Click;
            MenuItem copyStruct = new MenuItem();
            copyStruct.Header = "复制结构路径";
            copyStruct.Click += MenuItem_CopyStruct_Click;
            MenuItem copyFile = new MenuItem();
            copyFile.Header = "复制生成路径";
            copyFile.Click += MenuItem_CopyFile_Click;
            ContextMenu contextMenu = new ContextMenu();
            contextMenu.Items.Add(copyName);
            contextMenu.Items.Add(copyStruct);
            contextMenu.Items.Add(copyFile);
            ContextMenu = contextMenu;
        }
        
        private void MenuItem_CopyFile_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(mMatchFile);
        }

        private void MenuItem_CopyStruct_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(mStructFile);
        }

        private void MenuItem_CoypName_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(mHeader);
        }

        public void CopyDirectory(List<ListViewItem_Base> items)
        {
            if (!Directory.Exists(mMatchFile))
            {
                Directory.CreateDirectory(mMatchFile);
            }
            foreach (TreeViewItem_Structure select in Items)
            {
                if (select.MIsFile)
                {
                    select.CopyFile(items);
                }
                else
                {
                    select.CopyDirectory(items);
                }
            }
            ChildCheck();
        }

        public void CopyFile(List<ListViewItem_Base> items)
        {
            List<ListViewItem_Base> matchs = items.Where(r => r.MFile.Name == mHeader).ToList();
            if (matchs.Count == 0)
            {
                MIsChecked = false;
                return;
            }
            FileInfo file = matchs.First().MFile;
            matchs.First().MCount++;
            FileInfo match = new FileInfo(mMatchFile);
            if (!match.Directory.Exists)
            {
                match.Directory.Create();
            }
            file.CopyTo(match.FullName, true);
            MIsChecked = true;
        }

        public void ChildCheck()
        {
            if (Items.Count == 0)
            {
                MIsChecked = true;
                return;
            }
            bool isAllCheck = false;
            foreach (TreeViewItem_Structure select in Items)
            {
                isAllCheck = select.MIsChecked || isAllCheck;
            }
            MIsChecked = isAllCheck;
        }

        public bool CheckMatch(bool isAll)
        {
            bool visible = false;
            if (!mIsFile)
            {
                foreach(TreeViewItem_Structure select in Items)
                {
                    visible  = select.CheckMatch(isAll) || visible;
                }
            }
            visible = visible || isAll || !mIsChecked;
            if (visible)
            {
                Visibility = Visibility.Visible;
            }
            else
            {
                Visibility = Visibility.Collapsed;
            }
            return visible;
        }
    }
}
