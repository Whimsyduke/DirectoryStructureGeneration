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
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SetPath(string title, TextBox control)
        {
            string projectPath = control.Text;
            if (!Directory.Exists(projectPath))
            {
                projectPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }
            System.Windows.Forms.FolderBrowserDialog folderDialog = new System.Windows.Forms.FolderBrowserDialog();
            folderDialog.Description = title;
            folderDialog.SelectedPath = projectPath;
            folderDialog.ShowDialog();
            if (folderDialog.SelectedPath != String.Empty)
                control.Text = folderDialog.SelectedPath;
        }

        private void Button_StructurePath_Click(object sender, RoutedEventArgs e)
        {
            SetPath("结构目录：", TextBox_StructurePath);
        }

        private void Button_FilesPath_Click(object sender, RoutedEventArgs e)
        {
            SetPath("文件目录：", TextBox_FilesPath);
        }

        private void Button_OutputPath_Click(object sender, RoutedEventArgs e)
        {
            SetPath("生成目录：", TextBox_OutputPath);
        }

        private void Button_Generation_Click(object sender, RoutedEventArgs e)
        {
            //try
            {
                if (!Directory.Exists(TextBox_StructurePath.Text))
                {
                    MessageBox.Show("无效的结构目录:" + TextBox_StructurePath.Text);
                    return;
                }
                if (!Directory.Exists(TextBox_FilesPath.Text))
                {
                    MessageBox.Show("无效的文件目录:" + TextBox_FilesPath.Text);
                    return;
                }
                if (!Directory.Exists(TextBox_OutputPath.Text))
                {
                    MessageBox.Show("无效的生成目录:" + TextBox_OutputPath.Text);
                    return;
                }
                DirectoryInfo structDir = new DirectoryInfo(TextBox_StructurePath.Text);
                DirectoryInfo filesDir = new DirectoryInfo(TextBox_FilesPath.Text);
                DirectoryInfo outputDir = new DirectoryInfo(TextBox_OutputPath.Text);
                TreeView_Structure.Items.Add(new TreeViewItem_Structure(false, structDir.Name, filesDir.FullName, structDir.Parent.FullName, "", null));
            }
            //catch (Exception error)
            //{
            //    MessageBox.Show(error.Message);
            //    return;
            //}
        }
    }
}
