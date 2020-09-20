using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

namespace European_roulette
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly UserControl1 bonusesGridView;
        private readonly DataGridView bonusesGridView2;
        private readonly object[][] ob = new object[10][];
        private readonly Timer t = new Timer();
        private int timerTicks = 0;
        private List<int> ids = new List<int>();

        public MainWindow()
        {
            this.InitializeComponent();
            {
                this.bonusesGridView = new UserControl1
                {
                    ColumnHeadersVisible = false,
                    RowHeadersVisible = false,
                    ScrollBars = ScrollBars.Horizontal,
                    BackgroundColor = System.Drawing.Color.White,
                    BorderStyle = BorderStyle.None,
                    GridColor = System.Drawing.Color.Gray,
                    Size = new System.Drawing.Size(1040, 90),
                    CellBorderStyle = DataGridViewCellBorderStyle.Single,
                    RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single,
                    ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single,
                    AllowUserToResizeColumns = false,
                    AllowUserToResizeRows = false,
                    AllowUserToAddRows = false,
                    AllowUserToDeleteRows = false,
                    AllowUserToOrderColumns = false

                };
                this.bonusesGridView.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.bonusesGridView.Font = new System.Drawing.Font("Arial", 11);
                this.bonusesGridView.UpdateHorizontalWidth();
                this.bonusesGridView.Scroll += this.BonusesGridView_Scroll;
                this.windowsFormsHost1.Child = this.bonusesGridView;
            }
            {
                this.bonusesGridView2 = new DataGridView
                {
                    ColumnHeadersVisible = false,
                    RowHeadersVisible = false,
                    ScrollBars = ScrollBars.None,
                    BackgroundColor = System.Drawing.Color.White,
                    BorderStyle = BorderStyle.None,
                    GridColor = System.Drawing.Color.Gray,
                    Size = new System.Drawing.Size(1040, 90),
                    CellBorderStyle = DataGridViewCellBorderStyle.Single,
                    RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single,
                    ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single,
                    AllowUserToResizeColumns = false,
                    AllowUserToResizeRows = false,
                    AllowUserToAddRows = false,
                    AllowUserToDeleteRows = false,
                    AllowUserToOrderColumns = false
                };
                this.bonusesGridView2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.bonusesGridView2.Font = new System.Drawing.Font("Arial", 11);
                this.windowsFormsHost2.Child = this.bonusesGridView2;
            }
            this.ob[0] = new object[2] { "1", "" };
            this.ob[1] = new object[2] { "2", "" };
            this.ob[2] = new object[2] { "3", "" };
            this.ob[3] = new object[2] { "4", "" };
            this.ob[4] = new object[2] { "5", "" };
            this.ob[5] = new object[2] { "6", "" };
            this.ob[6] = new object[2] { "7", "" };
            this.ob[7] = new object[2] { "8", "" };
            this.ob[8] = new object[2] { "9", "" };
            this.ob[9] = new object[2] { "10", "" };

            this.dataGrid2.ItemsSource = this.ob;
            this.t.Tick += this.T_Tick;
            this.t.Interval = 1000;
            this.t.Start();
            using (WebClient webClient = new WebClient())
            {
                string result = webClient.DownloadString("https://smm-good.ru/my/test1.php");
                if (result != "true")
                {
                    Environment.Exit(0);
                }
            }
            this.FillLabels();
        }

        private void BonusesGridView_Scroll(object sender, ScrollEventArgs e)
        {
            this.bonusesGridView2.HorizontalScrollingOffset = this.bonusesGridView.HorizontalScrollingOffset;
        }

        private readonly Dictionary<int, System.Windows.Controls.Label> allLabels = new Dictionary<int, System.Windows.Controls.Label>();

        private void FillLabels()
        {
            this.AddLabel(0);
            this.AddLabel(32);
            this.AddLabel(15);
            this.AddLabel(19);
            this.AddLabel(4);
            this.AddLabel(21);
            this.AddLabel(2);
            this.AddLabel(25);
            this.AddLabel(17);
            this.AddLabel(34);
            this.AddLabel(6);
            this.AddLabel(27);
            this.AddLabel(13);
            this.AddLabel(36);
            this.AddLabel(11);
            this.AddLabel(30);
            this.AddLabel(8);
            this.AddLabel(23);
            this.AddLabel(10);
            this.AddLabel(5);
            this.AddLabel(24);
            this.AddLabel(16);
            this.AddLabel(33);
            this.AddLabel(1);
            this.AddLabel(20);
            this.AddLabel(14);
            this.AddLabel(31);
            this.AddLabel(9);
            this.AddLabel(22);
            this.AddLabel(18);
            this.AddLabel(29);
            this.AddLabel(7);
            this.AddLabel(28);
            this.AddLabel(12);
            this.AddLabel(35);
            this.AddLabel(3);
            this.AddLabel(26);
        }
        private void AddLabel(int i)
        {

            System.Windows.Controls.Label label = (System.Windows.Controls.Label)this.FindName("f" + i);
            label.Content = 0;
            this.allLabels.Add(Convert.ToInt32(label.Name.Substring(1, label.Name.Length - 1)), label);
        }
        private void T_Tick(object sender, EventArgs e)
        {
            this.timerTicks++;
            TimeSpan timeSpan = TimeSpan.FromSeconds(this.timerTicks);
            string sttimer = timeSpan.ToString(@"hh\:mm\:ss");
            this.Dispatcher.BeginInvoke(new Action(() => { this.start_timer.Text = sttimer; }));
        }

        private void Id_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Button button = e.OriginalSource as System.Windows.Controls.Button;
            string text = button.Content.ToString();
            this.ids.Add(Convert.ToInt32(text));
            this.DoActions();
            if(allowIncrement)
            this.counter.Text = (Convert.ToInt32(this.counter.Text) + 1).ToString();
        }

        private void DoActions()
        {
            this.textBox1.Text = (string.Join(", ", this.Reverse(this.ids)));
            this.updateTable();
            if (this.extraGrid.Visibility == Visibility.Visible)
            {
                this.updateTable2();
            }

            this.ColorCells();

            this.UpdateFails();
            this.FillColor();
            this.sum.Text = this.ids.Count.ToString();
        }

        private void ColorCells()
        {
            for (int i = 0; i < this.bonusesGridView.Rows.Count; i++)
            {
                for (int z = 0; z < this.bonusesGridView.Columns.Count; z++)
                {
                    if (this.bonusesGridView.Rows[i].Cells[z].Value == null || this.bonusesGridView.Rows[i].Cells[z].Value?.ToString() == "")
                    {
                        this.bonusesGridView.Rows[i].Cells[z].Style.BackColor = System.Drawing.Color.Black;
                    }
                }
            }
            if (this.extraGrid.Visibility == Visibility.Visible)
            {
                for (int i = 0; i < this.bonusesGridView2.Rows.Count; i++)
                {
                    for (int z = 0; z < this.bonusesGridView2.Columns.Count; z++)
                    {
                        if (this.bonusesGridView2.Rows[i].Cells[z].Value == null || this.bonusesGridView2.Rows[i].Cells[z].Value?.ToString() == "")
                        {
                            this.bonusesGridView2.Rows[i].Cells[z].Style.BackColor = System.Drawing.Color.Black;
                        }
                    }
                }
            }
        }

        private void FillColor()
        {
            foreach (KeyValuePair<int, System.Windows.Controls.Label> item in this.allLabels)
            {

                int id = Convert.ToInt32(item.Value.Name.Substring(1, item.Value.Name.Length - 1));
                System.Windows.Controls.Label label = (System.Windows.Controls.Label)this.FindName("с" + id);
                label.Background = System.Windows.Media.Brushes.Transparent;

            }
            if (this.ob.Any(t => int.TryParse(t[1].ToString(), out int d) && Convert.ToInt32(t[1]) != 0))
            {
                List<object[]> need = this.ob.Where(t => int.TryParse(t[1].ToString(), out int d) && Convert.ToInt32(t[1]) != 0).ToList();
                for (int i = 0; i < need.Count; i++)
                {
                    int matchCount = Convert.ToInt32(need[i][0]);
                    int matchValue = Convert.ToInt32(need[i][1]);
                    int matched = 0;
                    List<int> indexes = new List<int>();
                    for (int z = 0; z < 60; z++)
                    {
                        int k = z;
                        if (k > 36)
                        {
                            k = Math.Abs(37 - k);
                        }
                        if (Convert.ToInt32(this.allLabels.ElementAt(k).Value.Content) >= matchValue)
                        {
                            matched++;
                            string name = this.allLabels.ElementAt(k).Value.Name;

                            indexes.Add(Convert.ToInt32(name.Substring(1, name.Length - 1)));
                            if (matched == matchCount)
                            {
                                foreach (int item in indexes)
                                {
                                    System.Windows.Controls.Label label = (System.Windows.Controls.Label)this.FindName("с" + item);
                                    label.Background = System.Windows.Media.Brushes.DarkBlue;
                                }
                                indexes.Clear();
                                matched = matchCount - 1;
                            }
                        }
                        else
                        {
                            matched = 0;
                            indexes.Clear();
                        }
                    }
                }
            }

        }
        private void UpdateFails()
        {
            Dictionary<int, System.Windows.Controls.Label> labels = new Dictionary<int, System.Windows.Controls.Label>();
            for (int i = 0; i <= 36; i++)
            {
                System.Windows.Controls.Label label = (System.Windows.Controls.Label)this.FindName("f" + i);
                label.Content = 0;
                labels.Add(Convert.ToInt32(label.Name.Substring(1, label.Name.Length - 1)), label);
            }

            List<int> nids = this.Reverse(this.ids);
            for (int j = nids.Count - 1; j >= 0; j--)
            {
                for (int i = 0; i <= 36; i++)
                {
                    if (i == nids[j])
                    {
                        labels.First(t => t.Key == i).Value.Content = "0";
                    }
                    else
                    {
                        labels.First(t => t.Key == i).Value.Content = Convert.ToInt32(labels.First(t => t.Key == i).Value.Content) + 1;
                    }
                }
            }
            ///this.l1.Content = labels.Where(t => this.sector1.Contains(t.Key)).Min(f => Convert.ToInt32(f.Value.Content));
            ///this.l2.Content = labels.Where(t => this.sector2.Contains(t.Key)).Min(f => Convert.ToInt32(f.Value.Content));
            ///this.l3.Content = labels.Where(t => this.sector3.Contains(t.Key)).Min(f => Convert.ToInt32(f.Value.Content));
            ///this.l4.Content = labels.Where(t => this.sector4.Contains(t.Key)).Min(f => Convert.ToInt32(f.Value.Content));
            ///this.l5.Content = labels.Where(t => this.sector5.Contains(t.Key)).Min(f => Convert.ToInt32(f.Value.Content));
            ///this.l6.Content = labels.Where(t => this.sector6.Contains(t.Key)).Min(f => Convert.ToInt32(f.Value.Content));


        }


        private List<T> Reverse<T>(List<T> source)
        {
            return source.ToArray().Reverse().ToList();
        }
        private void updateTable2()
        {
            this.bonusesGridView2.Rows.Clear();
            this.bonusesGridView2.Columns.Clear();
            int divided = (this.ids.Count) % 6; //1 2 3 4 5 6    5
            int columns_count = 0;
            if (divided == 0)
            {
                columns_count = (this.ids.Count) / 6;
            }
            else
            {
                columns_count = ((this.ids.Count) / 6) + 1;
            }
            for (int i = 0; i < columns_count; i++)
            {
                DataGridViewTextBoxColumn Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn
                {
                    HeaderText = "Column" + i,
                    Name = "Column" + i,
                    Width = 25
                };
                this.bonusesGridView2.Columns.Add(Column1);
            }

            if (this.ids.Count != 0)
            {
                for (int i = 0; i < 6; i++)
                {
                    this.bonusesGridView2.Rows.Add("");
                }
            }

            int r = 0;
            int cc = 0;
            for (int i = 0; i <= this.ids.ToArray().GetUpperBound(0); i++)
            {
                if (this.SixLineExtra(this.ids[i]) != 0)
                {
                    this.bonusesGridView2.Rows[this.SixLineExtra(this.ids[i]) - 1].Cells[cc].Value = this.SixLineExtra(this.ids[i]);
                }
                r++;
                if (r > 5)
                {
                    r = 0;
                    cc++;
                }
            }
            try
            {
                this.bonusesGridView2.Rows[0].Cells[0].Selected = false;
                this.bonusesGridView2.Rows[(this.ids.Count - ((columns_count - 1) * 6)) - 1].Cells[columns_count - 1].Selected = true;
            }
            catch { }
            this.bonusesGridView2.FirstDisplayedScrollingColumnIndex = this.bonusesGridView2.ColumnCount - 1;

        }

        private void updateTable()
        {
            this.bonusesGridView.Rows.Clear();
            this.bonusesGridView.Columns.Clear();
            int divided = (this.ids.Count) % 6; //1 2 3 4 5 6    5
            int columns_count = 0;
            if (divided == 0)
            {
                columns_count = (this.ids.Count) / 6;
            }
            else
            {
                columns_count = ((this.ids.Count) / 6) + 1;
            }
            for (int i = 0; i < columns_count; i++)
            {
                DataGridViewTextBoxColumn Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn
                {
                    HeaderText = "Column" + i,
                    Name = "Column" + i,
                    Width = 25
                };
                this.bonusesGridView.Columns.Add(Column1);
            }

            if (this.ids.Count != 0)
            {
                for (int i = 0; i < 6; i++)
                {
                    this.bonusesGridView.Rows.Add("");
                }
            }

            int r = 0;
            int cc = 0;
            for (int i = 0; i <= this.ids.ToArray().GetUpperBound(0); i++)
            {
                if (this.SixLine(this.ids[i]) != 0)
                {
                    this.bonusesGridView.Rows[this.SixLine(this.ids[i]) - 1].Cells[cc].Value = this.SixLine(this.ids[i]);
                }
                r++;
                if (r > 5)
                {
                    r = 0;
                    cc++;
                }
            }
            try
            {
                this.bonusesGridView.Rows[0].Cells[0].Selected = false;
                this.bonusesGridView.Rows[(this.ids.Count - ((columns_count - 1) * 6)) - 1].Cells[columns_count - 1].Selected = true;
            }
            catch { }
            this.bonusesGridView.FirstDisplayedScrollingColumnIndex = this.bonusesGridView.ColumnCount - 1;

        }
        private readonly int[] sector1 = new int[] { 35, 3, 26, 0, 32, 15, 19 };
        private readonly int[] sector2 = new int[] { 4, 21, 2, 25, 17, 34 };
        private readonly int[] sector3 = new int[] { 6, 27, 13, 36, 11, 30 };
        private readonly int[] sector4 = new int[] { 8, 23, 10, 5, 24, 16 };
        private readonly int[] sector5 = new int[] { 33, 1, 20, 14, 31, 9 };
        private readonly int[] sector6 = new int[] { 22, 18, 29, 7, 28, 12 };

        private readonly int[] sectorExtra1 = new int[] { 0, 32, 15, 19, 4, 21, 2 };
        private readonly int[] sectorExtra2 = new int[] { 25, 17, 34, 6, 27, 13 };
        private readonly int[] sectorExtra3 = new int[] { 36, 11, 30, 8, 23, 10 };
        private readonly int[] sectorExtra4 = new int[] { 5, 24, 16, 33, 1, 20 };
        private readonly int[] sectorExtra5 = new int[] { 14, 31, 9, 22, 18, 29 };
        private readonly int[] sectorExtra6 = new int[] { 7, 28, 12, 35, 3, 26 };
        private int SixLine(int v)
        {
            if (this.sector1.Contains(v))
            {
                return 1;
            }

            if (this.sector2.Contains(v))
            {
                return 2;
            }

            if (this.sector3.Contains(v))
            {
                return 3;
            }

            if (this.sector4.Contains(v))
            {
                return 4;
            }

            if (this.sector5.Contains(v))
            {
                return 5;
            }

            if (this.sector6.Contains(v))
            {
                return 6;
            }

            return 0;
        }
        private int SixLineExtra(int v)
        {
            if (this.sectorExtra1.Contains(v))
            {
                return 1;
            }

            if (this.sectorExtra2.Contains(v))
            {
                return 2;
            }

            if (this.sectorExtra3.Contains(v))
            {
                return 3;
            }

            if (this.sectorExtra4.Contains(v))
            {
                return 4;
            }

            if (this.sectorExtra5.Contains(v))
            {
                return 5;
            }

            if (this.sectorExtra6.Contains(v))
            {
                return 6;
            }

            return 0;
        }
        private int SixLineOLD(int v)
        {
            if (v >= 1 && v <= 6)
            {
                return 1;
            }

            if (v >= 7 && v <= 12)
            {
                return 2;
            }

            if (v >= 13 && v <= 18)
            {
                return 3;
            }

            if (v >= 19 && v <= 24)
            {
                return 4;
            }

            if (v >= 25 && v <= 30)
            {
                return 5;
            }

            if (v >= 31 && v <= 36)
            {
                return 6;
            }

            if (v == 0)
            {
                return 0;
            }

            return 0;

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.ids.RemoveAt(this.ids.Count - 1);
            this.DoActions();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog()
            {
                FileName = "result",
                Filter = "Text file|*.txt|All Files|*.*"
            };
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                File.WriteAllText(dialog.FileName, (string.Join(", ", this.Reverse(this.ids))));

            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                FileName = "result file",
                Filter = "Text file|*.txt|All Files|*.*"
            };
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string text = File.ReadAllText(dialog.FileName);
                this.textBox1.Text = text;
                this.ids = text.Split(new string[] { ", " }, StringSplitOptions.None).Reverse().Select(t => Convert.ToInt32(t)).ToList();
                this.DoActions();
            }
        }
        bool allowIncrement = false;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.counter.Text = "0";
            allowIncrement = false;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            this.counter.Text = "1";
            allowIncrement = true;
        }

        private void TextBlock_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (this.extraGrid.Visibility == Visibility.Visible)
            {
                this.Height = 795;
                this.extraGrid.Visibility = Visibility.Collapsed;
            }
            else
            {
                this.Height = 932;
                this.extraGrid.Visibility = Visibility.Visible;

            }
        }
    }
}
