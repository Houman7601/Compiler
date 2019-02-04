using Compiler.Classes;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Linq;
using Microsoft.Win32;
using System;
using System.IO;

namespace Compiler
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Token> Mainlist = new List<Token>();


        public MainWindow()
        {
            InitializeComponent();
        }

        private void _btnGO_Click(object sender, RoutedEventArgs e)
        {
            List<Token> tokens = new List<Token>();
            Checkers func = new Checkers();
            TokenList tokenlist = new TokenList();
            string expr, strnum = "";
            int row = 1, col = 1, cnt, blockNO = 0;
            expr = _txtCode.Text;
            cnt = expr.Length;

            for (int i = 0; i < cnt;)
            {
                if (expr[i] == ' ')
                {
                    col++;
                }
                else if (expr[i] == '\t')
                {
                    col += 7;
                }
                else if (expr[i] == '#')
                {

                }
                else if (expr[i] == '$')
                {
                    Token tk = new Token()
                    {
                        Name = expr[i].ToString(),
                        Type = "Error",
                        BlockNumber = blockNO,
                        Row = row,
                        Col = col
                    };
                    tokens.Add(tk);
                    i++;
                    col++;
                    continue;
                }
                #region Comment
                else if (expr[i] == '/' && expr[i + 1] == '*')
                {
                    i += 2;
                    while (true)
                    {
                        if (i == expr.Length - 1)
                        {
                            i++;
                            break;
                        }
                        if (expr[i + 1] == '/' && expr[i] == '*')
                        {
                            i += 1;
                            break;
                        }
                        else i++;
                    }
                }
                #endregion

                #region New Line
                else if (expr[i] == '\n')
                {
                    col = 1;
                    row++;
                }

                #endregion

                #region Is Digit
                else if (func.IsDigit(expr[i]))
                {
                    strnum = "";
                    while (func.IsDigit(expr[i]) || expr[i] == '.') //start with digit
                    {
                        strnum += expr[i++];
                        if (i == cnt)
                            break;
                    }
                    Token tk = new Token()
                    {
                        Name = strnum,
                        Type = "Number",
                        BlockNumber = blockNO,
                        Row = row,
                        Col = col
                    };
                    tokens.Add(tk);
                    col++;
                    continue;
                }
                #endregion

                #region Start Block
                else if (expr[i] == '{')
                {
                    blockNO++;
                    Token tk = new Token()
                    {
                        Name = expr[i].ToString(),
                        Type = "Start Block",
                        BlockNumber = blockNO,
                        Row = row,
                        Col = col
                    };
                    tokens.Add(tk);
                    i++;
                    col++;
                    continue;
                }
                #endregion

                #region End Block
                else if (expr[i] == '}')
                {
                    blockNO--;
                    Token tk = new Token()
                    {
                        Name = expr[i].ToString(),
                        Type = "End Block",
                        BlockNumber = blockNO,
                        Row = row,
                        Col = col
                    };
                    tokens.Add(tk);
                    i++;
                    col++;
                    continue;
                }
                #endregion
                else if (func.IsVar(expr[i]))
                {
                    int coltemp = 0;
                    #region Is Identifier
                    Token tk;
                    string concat = "";
                    while (func.IsVar(expr[i]) || func.IsDigit(expr[i]) || expr[i] == '_') //start with var
                    {
                        concat += expr[i].ToString();
                        i++;
                        coltemp++;
                        if (cnt == i)
                        {
                            break;
                        }
                    }
                    #endregion

                    #region Is Keyword
                    if (tokenlist.KeyWords.Exists(x => x.Equals(concat)))
                    {

                        tk = new Token()
                        {
                            Name = concat,
                            Type = "Keyword",
                            BlockNumber = blockNO,
                            Row = row,
                            Col = col
                        };
                    }
                    else
                    {
                        tk = new Token()
                        {
                            Name = concat,
                            Type = "Identifier",
                            BlockNumber = blockNO,
                            Row = row,
                            Col = col
                        };
                    }
                    tokens.Add(tk);
                    col += coltemp;
                    continue;
                }
                #endregion

                #region Is Dellimeter
                else if (tokenlist.Dellimeters.Exists(x => x.Equals(expr[i].ToString())))
                {
                    Token tk = new Token()
                    {
                        Name = expr[i].ToString(),
                        Type = "Delimiter",
                        BlockNumber = blockNO,
                        Row = row,
                        Col = col
                    };
                    tokens.Add(tk);
                    i++;
                    col++;
                    continue;
                }
                #endregion

                #region Is Operator
                else if (tokenlist.Operators.Exists(x => x.Equals(expr[i].ToString())))
                {
                    Token tk = new Token();
                    if (tokenlist.Operators.Exists(x => x.Equals(expr[i + 1].ToString())))
                    {

                        tk = new Token()
                        {
                            Name = expr[i].ToString() + expr[i + 1].ToString(),
                            Type = "Operator",
                            BlockNumber = blockNO,
                            Row = row,
                            Col = col
                        };
                        i++;
                    }
                    else
                    {
                        tk = new Token()
                        {
                            Name = expr[i].ToString(),
                            Type = "Operator",
                            BlockNumber = blockNO,
                            Row = row,
                            Col = col
                        };
                    }
                    i++;
                    col++;
                    tokens.Add(tk);
                    continue;
                    #endregion
                }
                i++;

            }
            _dtgResult.ItemsSource = tokens;
            Mainlist.Clear();
            Mainlist = tokens;

        }

        private void _btnclear_Click(object sender, RoutedEventArgs e)
        {
            string filelines;
            OpenFileDialog theDialog = new OpenFileDialog();
            
            theDialog.Title = "Open Text File";
            theDialog.Filter = "txt files|*.txt";
            if (theDialog.ShowDialog() == true)
            {
                try
                {
                    filelines = File.ReadAllText(theDialog.FileName);
                    _txtCode.Text = filelines;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }

            
        }

        private void _txtSearch_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            _dtgResult.ItemsSource = Mainlist.Where(x => x.Name.Contains(_txtSearch.Text));
        }

        private void _drag_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.DragMove();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
