using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml.XPath;
using System.Collections;
using System.Diagnostics;
using System.Drawing.Printing;

namespace Web_Browser
{
    public partial class Form1 : Form
    {
        ArrayList tabpages = new ArrayList();
        ArrayList webpages = new ArrayList();
        //Too keep Current Tab
        int current_tab_count = 0;
        //To Moving of Image
        int image_animation = 0;
        bool FullScreen = false;
        int CommandPanelHeight = 0;
        private object pnlsearch;

        public Form1()
        {
            InitializeComponent();
          
            tabControl1.TabPages.Clear();
            Create_New_Tab();
            WebBrowser webpage = GetCurrentWebBrowser();
          
            webpage.GoHome();
            toolStripButton2.Visible = false;
        }

        private WebBrowser GetCurrentWebBrowser()
        {
            TabPage current_tab = tabControl1.SelectedTab;
            WebBrowser thispasge = (WebBrowser)webpages[tabpages.IndexOf(current_tab)];
            return thispasge;

        }

        private void Create_New_Tab()
        {
            if (current_tab_count == 10) return;
            TabPage newpage = new TabPage("Loading...");
            tabpages.Add(newpage);
            tabControl1.TabPages.Add(newpage);
            current_tab_count++;
            WebBrowser webpage = new WebBrowser();
            webpages.Add(webpage);
            webpage.Parent = newpage;
            webpage.Dock = DockStyle.Fill;
            webpage.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(webpage_DocumentCompleted);
            timer1.Enabled = true;
            tabControl1.SelectedTab = newpage;
        }

        private void webpage_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            UpdateAllNames();
            UpdateBackForwardButtons();
            timer1.Enabled = false;
           
        }

        private void UpdateAllNames()
        {
           
        }

        private void UpdateBackForwardButtons()
        {
            WebBrowser thiswebpage = GetCurrentWebBrowser();

            if (thiswebpage.CanGoBack) button1.Enabled = true;
            else button1.Enabled = false;

            if (thiswebpage.CanGoForward) button3.Enabled = true;
            else button3.Enabled = false;

            if (current_tab_count > 1) toolStripButton3.Enabled = true;
            else toolStripButton4.Enabled = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
                this.WindowState = FormWindowState.Normal;
            else
                this.WindowState = FormWindowState.Maximized;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void newTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Create_New_Tab();
            WebBrowser thiwebpage = GetCurrentWebBrowser();
            if (this.comboBox1.Text == "")
                thiwebpage.GoHome();
            else
                thiwebpage.Navigate(comboBox1.Text);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string url = comboBox1.Text;
            if (url == "")
                return;
            WebBrowser thispage = GetCurrentWebBrowser();
            thispage.Navigate(url);
            timer1.Enabled = true;
          
            comboBox1.Items.Add(url);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           button5.PerformClick();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WebBrowser thispage = GetCurrentWebBrowser();
            if (thispage.CanGoBack)
                thispage.GoBack();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            WebBrowser thiswebpage = GetCurrentWebBrowser();
            if (thiswebpage.CanGoForward)
                thiswebpage.GoForward();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            WebBrowser thiswebpage = GetCurrentWebBrowser();
            thiswebpage.Refresh();
            timer1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WebBrowser thiswebpage = GetCurrentWebBrowser();
            thiswebpage.GoHome();
            timer1.Enabled = true;
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            WebBrowser orkt = GetCurrentWebBrowser();
            orkt.Navigate("http://www.orkut.com");
            timer1.Enabled = true;
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {

            WebBrowser gmail = GetCurrentWebBrowser();
            gmail.Navigate("http://www.gmail.com");
            timer1.Enabled = true;

        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            WebBrowser google = GetCurrentWebBrowser();
            google.Navigate("http://www.google.com");
            timer1.Enabled = true;
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            WebBrowser msn = GetCurrentWebBrowser();
            msn.Navigate("http://www.msn.com");
            timer1.Enabled = true;
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {

            WebBrowser yahoo = GetCurrentWebBrowser();
            yahoo.Navigate("http://www.yahoo.com");
            timer1.Enabled = true;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            FullScreen = !FullScreen;
            if (FullScreen)
            {
                if (this.WindowState == FormWindowState.Maximized)
                {
                    this.WindowState = FormWindowState.Normal;
                }
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                menuStrip1.Visible = false;
                statusStrip1.Visible = false;
                statusStrip1.Height = 1;
                toolStripButton2.Visible = true;
            }
            else
            {
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.WindowState = FormWindowState.Maximized;
                menuStrip1.Visible = true;
                statusStrip1.Visible = true;
                toolStripButton2.Visible = false;
                statusStrip1.Height = CommandPanelHeight;
            }
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WebBrowser web_open = GetCurrentWebBrowser();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "wBrowser Open file...";
            ofd.AddExtension = true;
            ofd.Filter = "All Files|*.*|HTML Files|*.htm;*.html|Text Files|" +
              "*.txt|GIF Files|*.gif|JPEG Files|*.jpg;*.jpeg|" +
              "PNG Files|*.png|ART Files|*.art|AU Files|*.au|" +
              "AIFF Files|*.aiff;*.aif|XBM Files|*.xbm";
            ofd.FilterIndex = 1;
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                web_open.Navigate(ofd.FileName);
                comboBox1.Text = ofd.FileName;
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (current_tab_count < 2) return;

            TabPage current_tab = tabControl1.SelectedTab;
            WebBrowser thiswebpage = (WebBrowser)webpages[tabpages.IndexOf(current_tab)];
            thiswebpage.Dispose();
            tabpages.Remove(current_tab);
            current_tab.Dispose();
            tabControl1.TabPages.Remove(current_tab);
            current_tab_count--;
        }

        private void savePageAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WebBrowser websave = GetCurrentWebBrowser();
            websave.ShowSaveAsDialog();
        }

        private void pageSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WebBrowser webp_setup = GetCurrentWebBrowser();
            webp_setup.ShowPageSetupDialog();
        }

        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WebBrowser web_p_preview = GetCurrentWebBrowser();
            web_p_preview.ShowPrintPreviewDialog();
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WebBrowser web_print = GetCurrentWebBrowser();
            web_print.ShowPrintDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (current_tab_count < 2) return;

            TabPage current_tab = tabControl1.SelectedTab;
            WebBrowser thiswebpage = (WebBrowser)webpages[tabpages.IndexOf(current_tab)];
            thiswebpage.Dispose();
            tabpages.Remove(current_tab);
            current_tab.Dispose();
            tabControl1.TabPages.Remove(current_tab);
            current_tab_count--;
        }
    }
    }

