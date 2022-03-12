using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pdfViewer2021
{


    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }




        //private async void lstpdfFile_Click(object sender, EventArgs e)
        //{
        //    // PDFファイルを開くためのピッカーを準備
        //    var picker = new Windows.Storage.Pickers.FileOpenPicker();
        //    picker.FileTypeFilter.Add(".pdf");
        //    Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();

        //    if (file != null)
        //    {
        //        try
        //        {
        //            // PDFファイルを読み込む
        //            pdfDocument = await Windows.Data.Pdf.PdfDocument.LoadFromFileAsync(file);
        //        }
        //        catch
        //        {

        //        }
        //    }

        //    if (pdfDocument != null)
        //    {
        //        // 1ページ目を読み込む
        //        using (Windows.Data.Pdf.PdfPage page = pdfDocument.GetPage(0))
        //        {
        //            // ビットマップイメージの作成
        //            var stream = new Windows.Storage.Streams.InMemoryRandomAccessStream();
        //            await page.RenderToStreamAsync(stream);
        //            Windows.UI.Xaml.Media.Imaging.BitmapImage src = new Windows.UI.Xaml.Media.Imaging.BitmapImage();

        //            // Imageオブジェクトにsrcをセット
        //            imgPdf.Source = src;

        //            // srcに作成したビットマップイメージを流し込む
        //            await src.SetSourceAsync(stream);
        //        }
        //    }

        //}

        private void lstpdfFile_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FileName = lstpdfFile.Items[lstpdfFile.SelectedIndex].ToString().Normalize();
            lblLastUpdate.Text = "最終更新" + System.IO.File.GetLastWriteTime(FolderName + "\\" + FileName).ToString();
            lblFileName.Text = FileName;
            bool a = axAcroPDF1.LoadFile(FolderName + "\\" + FileName);
            axAcroPDF1.LoadFile(FolderName + "\\" + FileName);

        }
        string FolderName;

        private void button1_Click_1(object sender, EventArgs e)
        {
            FolderBrowserDialog fbDialog = new FolderBrowserDialog();

            // ダイアログの説明文を指定する
            fbDialog.Description = "ダイアログの説明文";

            // デフォルトのフォルダを指定する
            fbDialog.SelectedPath = @"C:";
            //フォルダを選択するダイアログを表示する
            if (fbDialog.ShowDialog() == DialogResult.OK)
            {

                Console.WriteLine(fbDialog.SelectedPath);
                //フォルダー名を取得
                FolderName = fbDialog.SelectedPath;
                txtDirectory.Text = FolderName;
                //リストボックスをクリア
                lstpdfFile.Items.Clear();
                //リストボックスに水平スクロールバーを表示
                lstpdfFile.HorizontalScrollbar = true;

                //フォルダー名が取得できたら
                if (FolderName.Length > 1)
                {
                    //指定の拡張子のファイルだけ取得する場合
                    foreach (string FileName in System.IO.Directory.GetFiles(FolderName, "*.pdf"))
                    {
                        lstpdfFile.Items.Add(Path.GetFileName(FileName));
                    }
                }
            }
            else
            {
                Console.WriteLine("キャンセルされました");
            }




            // オブジェクトを破棄する
            fbDialog.Dispose();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderName = txtDirectory.Text;
            txtDirectory.Text = FolderName;
            //リストボックスをクリア
            lstpdfFile.Items.Clear();
            //リストボックスに水平スクロールバーを表示
            lstpdfFile.HorizontalScrollbar = true;

            //フォルダー名が取得できたら
            if (FolderName.Length > 1)
            {
                //指定の拡張子のファイルだけ取得する場合
                foreach (string FileName in System.IO.Directory.GetFiles(FolderName, "*.pdf"))
                {
                    lstpdfFile.Items.Add(Path.GetFileName(FileName));
                }
            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (axAcroPDF1 != null)
            {
                // This purposely SUPPRESSES the Dispose of _adobeReader that otherwise automatically happens
                // as the window shuts down. For some reason Dispose of this object is VERY slow; around 16 seconds
                // on my fast desktop. I don't think it can hang on to any important resources once the app
                // quits, so just prevent the Dispose.
                // (I tried various other things, such as loading a non-existent file and catching the resulting
                // exception, hiding the _adobeReader, Disposing it in advance (in this method)...nothing else
                // prevented the long delay on shutdown.)
                this.Controls.Remove(axAcroPDF1);
                axAcroPDF1 = null;
            }


        }

    }
}
