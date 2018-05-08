using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake1
{
    public partial class MainView : Form
    {
        MainControler mainControler;

        public MainView()
        {
            InitializeComponent();
        }

        internal void Init(MainControler mainControler)
        {
            this.mainControler = mainControler;
        }

        private void MainView_KeyDown(object sender, KeyEventArgs e)
        {
            this.mainControler.KeyDown(e.KeyCode);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            this.mainControler.DrawSnake(e.Graphics);
        }

        public Size GetPanelSize()
        {
            return this.panel1.Size;
        }

        internal void RefreshPanel()
        {
            this.panel1.Refresh();
        }

        public void SetLives(int lives)
        {
            lblLives.Text = string.Format("Życia: {0}", lives);
        }

        public void SetPoints(int points)
        {
            lblPoints.Text = string.Format("Punkty: {0}", points);
        }

        public void SetTime(int time)
        {
            lblTime.Text = string.Format("Czas koloru: {0}", time);
        }

        public void SetPause(bool pause)
        {
            //lblPause.Visible = pause;
            if (pause)
                lblPause.Text = string.Format("Pauza");
            else
                lblPause.Text = string.Format("");
        }

        internal void SetCanGothroughWalls(bool canGothroughWalls)
        {
            if (canGothroughWalls) {
                this.panel1.BorderStyle = BorderStyle.Fixed3D;
            }else
            {
                this.panel1.BorderStyle = BorderStyle.FixedSingle;
            }
        }
    }
}
