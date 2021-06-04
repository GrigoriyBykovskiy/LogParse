using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace LogParse
{
    public partial class Form1 : Form
    {
        public MatchCollection myMatch;
        Dictionary<string, Color> signatures = new Dictionary<string, Color>();
        public string signatura;
        public string fileText;
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
                
                signatura = buf;
                
                Regex regex = new Regex(@signatura);
                MatchCollection matches = regex.Matches(fileText);
                
                myMatch = matches;
                
                richTextBoxParseResult.Text = "Все вхождения строки "
                                   + signatura + " в исходном тексте: " + "\r\n";
                
                if (matches.Count > 0)
                {
                    foreach (Match match in matches)
                    {
                        richTextBoxParseResult.Text += match.Index + " " + match.Value + "\r\n";
                        Color value;
                        if (!signatures.TryGetValue(buf, out value))
                            throw new Exception("Невозможно получить значение цвета!");
                        SetSelectionStyle(match.Index, match.Length, FontStyle.Underline, value);
                    }
                }
                else
                {
                    richTextBoxParseResult.Text += "Совпадений не найдено" + "\r\n";
                }
                
                
                MessageBox.Show("Анта бака!\nВсе хорошо\n", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        foreach (var filePath in openFileDialog.FileNames)
                        {
                            richTextBoxLog.Text += File.ReadAllText(filePath, Encoding.Default);
                        }

                        fileText = richTextBoxLog.Text;
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