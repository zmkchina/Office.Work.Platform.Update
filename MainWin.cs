using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Handlers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Office.Work.Platform.Update
{
    public partial class MainWin : Form
    {
        public MainWin()
        {
            InitializeComponent();

        }
        private async void MainWin_Load(object sender, System.EventArgs e)
        {
            string localFileName = Path.Combine(Application.StartupPath, "LocalUpdate.zgk");
            List<string> NeedUpdateFiles = DataRWLocalFileRepository.ReadObjFromFile<List<string>>(localFileName);

            await Task.Run(() =>
            {
                foreach (string item in NeedUpdateFiles)
                {
                    ProgressMessageHandler progress = new System.Net.Http.Handlers.ProgressMessageHandler();
                    progress.HttpReceiveProgress += (object sender, System.Net.Http.Handlers.HttpProgressEventArgs e) =>
                    {
                        this.prgBar.Value = e.ProgressPercentage;// (double)(e.BytesTransferred / e.TotalBytes) * 100;
                    };
                    DataFileRepository.DownloadFile(item, progress);
                }
            });
            if (MessageBox.Show("升级完毕，要运行主程序吗？", "完成", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string updateProgram = Path.Combine(Application.StartupPath, "Office.Work.Platform.exe");
                if (File.Exists(updateProgram))
                {
                    System.Diagnostics.Process.Start(updateProgram);
                }
            }
            Application.Exit();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
