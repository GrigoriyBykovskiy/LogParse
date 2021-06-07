using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LogParse
{
    public partial class Form1 : Form
    {
        static AutoResetEvent waitHandler = new AutoResetEvent(true);
        public List<string> journals = new List<string>();
        public List<int> journalsLength = new List<int>();
        public Dictionary<string, Color> signatures = new Dictionary<string, Color>();

        public Form1()
        {
            InitializeComponent();
            this.signatures.Add("form", Color.Red);
            this.comboBoxSignature.Items.Add("form");
            this.signatures.Add("string", Color.Blue);
            this.comboBoxSignature.Items.Add("string");
        }

        private void buttonHighlightSignature_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxSignature.Items.Count == 0)
                {
                    throw new Exception("Сигнатура не может быть пустой!");
                }

                if (comboBoxSignature.SelectedItem == null)
                {
                    throw new Exception("Необходимо выбрать сигнатуру!");
                }
                
                if (String.IsNullOrEmpty(richTextBoxLog.Text))
                {
                    throw new Exception("Необходимо выбрать хотя бы один лог-журнал!");
                }

                var buf = comboBoxSignature.SelectedItem.ToString();

                if (!signatures.ContainsKey(buf))
                {
                    Random rand = new Random();
                    Color tmp = Color.FromArgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255));
                    while (signatures.ContainsValue(tmp))
                    {
                        tmp = Color.FromArgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255));
                    }
                    signatures.Add(buf, tmp);
                }
                
                //signatura = buf;
                waitHandler.WaitOne();
                richTextBoxParseResult.AppendText("Результаты парсинга журналов на вхождения строки " + buf + ": \n");
                // richTextBoxParseResult.Text += "Результаты парсинга журналов на вхождения строки "
                //                               + buf + ": \n";
                waitHandler.Set();

                // var padding = 0;
                // for (int i = 0; i < 2; i++)
                // {
                //     waitHandler.WaitOne();
                //     string textBuffer = File.ReadAllText(journals[i], Encoding.Default);
                //     waitHandler.Set();
                //     padding += textBuffer.Length;
                // }
                
                Parallel.For(0, journals.Count, ctr =>
                {
                    Stopwatch time = new Stopwatch();
                    time.Start();
                    
                    var signatura = buf;
                    Regex regex = new Regex(@signatura,  RegexOptions.Singleline);
                    RichTextBox bufRTB = new RichTextBox();
                    
                    waitHandler.WaitOne();
                    
                    string textBuffer = File.ReadAllText(journals[ctr], Encoding.Default);
                    bufRTB.AppendText(textBuffer + '\n');
                    MatchCollection matches = regex.Matches(bufRTB.Text);
                    
                    waitHandler.Set();

                    if (matches.Count > 0)
                    {
                        foreach (Match match in matches)
                        {
                            waitHandler.WaitOne();
                            richTextBoxParseResult.Text += "В журнале " + journals[ctr] + " найдено вхождение строки: ";
                            richTextBoxParseResult.Text += match.Index + " " + match.Value + "\r\n";
                            waitHandler.Set();
                            
                            Color value;
                            
                            waitHandler.WaitOne();
                            if (!signatures.TryGetValue(buf, out value))
                                throw new Exception("Невозможно получить значение цвета!");
                            waitHandler.Set();
                            
                            var padding = 0;
                
                            for (int i = 0; i < ctr; i++)
                            {
                                waitHandler.WaitOne();
                                padding += journalsLength[i];
                                waitHandler.Set();
                            }
                            
                            waitHandler.WaitOne();
                            SetSelectionStyle(match.Index + padding, match.Length, FontStyle.Underline, value);
                            waitHandler.Set();
                        }
                    }
                    else
                    {
                        waitHandler.WaitOne();
                        richTextBoxParseResult.Text += "В журнале " + journals[ctr] + " совпадений не найдено." + "\r\n";
                        waitHandler.Set();
                    }
                    
                    time.Stop();
                    
                    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}",
                        time.Elapsed.Hours, 
                        time.Elapsed.Minutes, 
                        time.Elapsed.Seconds,
                        time.Elapsed.Milliseconds);
                    
                    waitHandler.WaitOne();
                    richTextBoxParseResult.AppendText("Время парсинга журнала " + journals[ctr] + ' ' + elapsedTime + "\r\n");
                    waitHandler.Set();
                });
                
            }
            catch (Exception excptn)
            {
                MessageBox.Show("Анта бака!\nОшибка при работе с сигнатурой!\n" + excptn.Message + excptn.StackTrace, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void comboBoxSignature_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (comboBoxSignature.Items.Count > 0 && !String.IsNullOrEmpty(comboBoxSignature.Text))
                {
                    bool isExist = false;
                    foreach (var item in comboBoxSignature.Items)
                    {
                        if (item.ToString() == comboBoxSignature.Text)
                            isExist = true;
                    }
                    if (!isExist)
                        comboBoxSignature.Items.Add(comboBoxSignature.Text);
                }
                else
                {
                    if (!String.IsNullOrEmpty(comboBoxSignature.Text))
                        comboBoxSignature.Items.Add(comboBoxSignature.Text);
                }
            }
        }

        private void buttonGetLog_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                openFileDialog.Multiselect = true;
                
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        journals.Clear();
                        journalsLength.Clear();
                        richTextBoxLog.Text = "";
                        
                        foreach (var filePath in openFileDialog.FileNames)
                        {
                            if (!journals.Contains(filePath))
                            {
                                string textBuffer = File.ReadAllText(filePath, Encoding.Default);
                                richTextBoxLog.AppendText(textBuffer + '\n');/*File.ReadAllText(filePath, Encoding.Default);*/
                                
                                RichTextBox bufRTB = new RichTextBox();
                                bufRTB.AppendText(textBuffer + '\n');
                                
                                journals.Add(filePath);
                                journalsLength.Add(bufRTB.Text.Length);
                            }
                        }
                        
                    }
                    catch (Exception excptn)
                    {
                        MessageBox.Show("Анта бака!\nОшибка при считывании лог-файла!\n" + excptn.Message + excptn.StackTrace, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        
        private void SetSelectionStyle(int startIndex, int length, FontStyle style, Color color)
        {
            richTextBoxLog.Select(startIndex, length);
            richTextBoxLog.SelectionFont = new Font(richTextBoxLog.SelectionFont,
                richTextBoxLog.SelectionFont.Style | style);
            richTextBoxLog.SelectionColor = color;
        }
    }
}