﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;
namespace ReadDbFile
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            MessageBox.Show("这是一个消息测试github");
        }
        //添加代码用于测试

        SQLiteConnection m_dbConnection;
        DataTable dt = null;
        private void ReadBbBtn_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "请选择文件";
            dialog.Filter = "db文件(*.db)|*.db";
            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }
            string dbPath = dialog.FileName;

            string conStr = @"Data Source =" + dbPath;
            m_dbConnection = new SQLiteConnection(conStr);
            m_dbConnection.Open();
            string mySql = "SELECT origin,origin_region FROM path_data group by origin,origin_region";
            SQLiteCommand cmd = m_dbConnection.CreateCommand();
            cmd.CommandText = mySql;
            SQLiteDataAdapter dao = new SQLiteDataAdapter(cmd);
            dt = new DataTable();
            dao.Fill(dt);
            FileStream Origin_ArrayTxtFS = new FileStream(System.IO.Path.GetDirectoryName(dbPath) + "\\" + System.IO.Path.GetFileName(dbPath) + "Origin-Array.txt", FileMode.OpenOrCreate);
            var utf8WithoutBom = new System.Text.UTF8Encoding(false);
            StreamWriter Origin_ArrayTxtSW = new StreamWriter(Origin_ArrayTxtFS, utf8WithoutBom);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Origin_ArrayTxtSW.WriteLine(dt.Rows[i].ItemArray[0].ToString() + "+" + dt.Rows[i].ItemArray[1].ToString());
            }
            Origin_ArrayTxtSW.Close();
            Origin_ArrayTxtFS.Close();
            m_dbConnection.Close();


            //OpenFileDialog dialog2 = new OpenFileDialog();
            //dialog2.Title = "请选择文件";
            //dialog2.Filter = "txt文件(*.txt)|*.txt";
            //if (dialog2.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            //{
            //    return;
            //}
            string txtPath = System.IO.Path.GetDirectoryName(dbPath) + "\\" + System.IO.Path.GetFileName(dbPath) + "Origin-Array.txt";
            FileStream fileS = new FileStream(txtPath, FileMode.Open);
            StreamReader sr = new StreamReader(fileS, utf8WithoutBom);
            string srLine;
            while ((srLine = sr.ReadLine()) != null)
            {
                MakeCsvFile(dbPath, srLine);
            }
            MessageBox.Show("转换完毕");
        }
        private void MakeCsvFile(string dbPath,string csvPath)
        {
            if (csvPath.IndexOf('+') == 0)
            {
                string conStr = @"Data Source =" + dbPath;
                m_dbConnection = new SQLiteConnection(conStr);
                m_dbConnection.Open();
                string csvName = null;
                csvName = csvPath;
                string sql = "SELECT count(*) FROM path_data WHERE origin_city = '" + csvName + "'";



                SQLiteCommand cmd = m_dbConnection.CreateCommand();
                cmd.CommandText = sql;
                SQLiteDataAdapter dao = new SQLiteDataAdapter(cmd);
                dt = new DataTable();
                dao.Fill(dt);

                SQLiteCommand cmd2 = m_dbConnection.CreateCommand();
                string sql2 = "SELECT origin_city ,origin_region FROM path_data WHERE  origin_city = '" + csvName + "' limit 0,1";
                cmd2.CommandText = sql2;
                SQLiteDataReader reader = cmd2.ExecuteReader();
                reader.Read();
                if (reader["origin_city"].ToString() != reader["origin_region"].ToString())
                {
                    csvName = reader[1].ToString() + reader[0].ToString();
                }
                FileStream csvFile = null;
                StreamWriter csvSw = null;
                if (File.Exists(System.IO.Path.GetDirectoryName(dbPath) + "\\" + csvName + ".csv"))
                {
                    csvFile = new FileStream(System.IO.Path.GetDirectoryName(dbPath) + "\\" + csvName + ".csv", FileMode.Open);
                    var utf8WithoutBom = new System.Text.UTF8Encoding(false);
                    csvSw = new StreamWriter(csvFile, utf8WithoutBom);
                    csvSw.BaseStream.Seek(0, SeekOrigin.End);
                }
                else
                {
                    csvFile = new FileStream(System.IO.Path.GetDirectoryName(dbPath) + "\\" + csvName + ".csv", FileMode.Create);
                    var utf8WithoutBom = new System.Text.UTF8Encoding(false);
                    csvSw = new StreamWriter(csvFile, utf8WithoutBom);
                }

                int count = int.Parse(dt.Rows[0][0].ToString());
                int arrayNum = count / 500 + 1;
                DataTable dtArray = null;
                for (int i = 0; i < arrayNum; i++)
                {
                    dtArray = new DataTable();
                    string sqlStr = "SELECT * FROM path_data WHERE origin_city = '" + csvName + "' limit " + (i * 500).ToString() + "," + 500.ToString();
                    SQLiteCommand command = m_dbConnection.CreateCommand();
                    command.CommandText = sqlStr;
                    SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(command);
                    dataAdapter.Fill(dtArray);
                    for (int j = 0; j < dtArray.Rows.Count; j++)
                    {
                        for (int k = 0; k < 7; k++)
                        {
                            csvSw.Write(dtArray.Rows[j][k] + ",");
                        }
                        csvSw.WriteLine("\"" + dtArray.Rows[j][7] + "\"");
                    }
                }
                csvSw.Close();
                csvFile.Close();
            }
            else
            {
                string conStr = @"Data Source =" + dbPath;
                m_dbConnection = new SQLiteConnection(conStr);
                m_dbConnection.Open();
                int index = csvPath.IndexOf('+');
                string regionName = csvPath.Substring(index + 1, csvPath.Length - index - 1);
                string cityName = csvPath.Substring(0, index);
                string CalculateNumSql = "SELECT count(*) FROM path_data WHERE origin = '" + cityName + "' AND origin_region = '" + regionName+ "'" ;
                SQLiteCommand cmd1 = m_dbConnection.CreateCommand();
                cmd1.CommandText = CalculateNumSql;
                SQLiteDataReader reader = cmd1.ExecuteReader();
                reader.Read();
                
                FileStream csvFile = null;
                StreamWriter csvSw = null;
                if (File.Exists(System.IO.Path.GetDirectoryName(dbPath) + "\\" + csvPath + ".csv"))
                {
                    csvFile = new FileStream(System.IO.Path.GetDirectoryName(dbPath) + "\\" + csvPath + ".csv", FileMode.Open);
                    var utf8WithoutBom = new System.Text.UTF8Encoding(false);
                    csvSw = new StreamWriter(csvFile, utf8WithoutBom);
                    csvSw.BaseStream.Seek(0, SeekOrigin.End);
                }
                else
                {
                    csvFile = new FileStream(System.IO.Path.GetDirectoryName(dbPath) + "\\" + csvPath + ".csv", FileMode.Create);
                    var utf8WithoutBom = new System.Text.UTF8Encoding(false);
                    csvSw = new StreamWriter(csvFile, utf8WithoutBom);
                }
                int count = int.Parse(reader[0].ToString());
                int arrayNum = count / 500 + 1;
                DataTable dtArray = null;
                for (int i = 0; i < arrayNum; i++)
                {
                    dtArray = new DataTable();
                    string sqlStr = "SELECT * FROM path_data WHERE origin = '" + cityName + "'AND origin_region = '" + regionName + "'" + " limit " + (i * 500).ToString() + "," + 500.ToString();
                    SQLiteCommand command = m_dbConnection.CreateCommand();
                    command.CommandText = sqlStr;
                    SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(command);
                    dataAdapter.Fill(dtArray);
                    for (int j = 0; j < dtArray.Rows.Count; j++)
                    {
                        for (int k = 0; k < 11; k++)
                        {
                            csvSw.Write(dtArray.Rows[j][k] + ",");
                        }
                        csvSw.WriteLine("\"" + dtArray.Rows[j][11] + "\"");
                    }
                }
                csvSw.Close();
                csvFile.Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        struct myRoadStruct
        {
            public int id;
            public string origin_city;
            public string origin_region;
            public string dest_city;
            public string dest_region;
        }
        
        private void BtnReadAdditionFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "请选择附加路径文件";
            dialog.Filter = "txt文件(*.txt)|*.txt";
            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }
            OpenFileDialog dialog2 = new OpenFileDialog();
            dialog2.Title = "请选择文件";
            dialog2.Filter = "db文件(*.db)|*.db";
            if (dialog2.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }
            string dbPath = dialog2.FileName;
            string conStr = @"Data Source =" + dbPath;
            m_dbConnection = new SQLiteConnection(conStr);
            m_dbConnection.Open();



            string txtPath = dialog.FileName;
            FileStream txtFS = new FileStream(txtPath, FileMode.Open);
            var utf8WithoutBom = new System.Text.UTF8Encoding(false);
            StreamReader txtSR = new StreamReader(txtFS, utf8WithoutBom);
            StreamWriter txtSW = new StreamWriter(txtFS, utf8WithoutBom);
            string aryLine = null;
            myRoadStruct tempStruct = new myRoadStruct();
            string[] tempStrarray = null;
            string sql = null;
            DataTable dtTemp = null;


            FileStream csvFS = null;
            StreamWriter csvSW = null;
            string csvName = null;
            DialogResult dr = MessageBox.Show("是否是百度模式的数据！", "重要选择", MessageBoxButtons.YesNoCancel);
            while ((aryLine = txtSR.ReadLine()) != null)
            {
                //if (aryLine == " ")
                //    continue;
                tempStrarray = aryLine.Split(',');
                tempStruct.id = int.Parse(tempStrarray[0]);
                //if(tempStruct.id < 56953)
                //    continue;
                tempStruct.origin_city = tempStrarray[1+4];
                tempStruct.origin_region = tempStrarray[3+4];
                tempStruct.dest_city = tempStrarray[2+4];
                tempStruct.dest_region = tempStrarray[4+4];
                if(tempStruct.origin_city != tempStruct.origin_region)
                    csvName = tempStruct.origin_city + '+' + tempStruct.origin_region;
                else csvName = tempStruct.origin_city;
                if (File.Exists(System.IO.Path.GetDirectoryName(dbPath) + "\\" + csvName + ".csv") == false)
                    continue; 

                SQLiteCommand cmd = m_dbConnection.CreateCommand();
                sql = "select * from path_data where id = " + tempStruct.id.ToString();
                cmd.CommandText = sql;
                SQLiteDataAdapter dao = new SQLiteDataAdapter(cmd);
                dtTemp = new DataTable();
                dao.Fill(dtTemp);
                if(dtTemp.Rows.Count == 0)
                    continue;
                csvFS = new FileStream(System.IO.Path.GetDirectoryName(dbPath) + "\\" + csvName + ".csv", FileMode.Open);
                
                csvSW = new StreamWriter(csvFS, utf8WithoutBom);
                csvSW.BaseStream.Seek(0, SeekOrigin.End);
                
                if (dr == DialogResult.OK)
                {
                    for (int k = 0; k < 11; k++)
                    {
                        csvSW.Write(dtTemp.Rows[0][k] + ",");
                    }
                    csvSW.WriteLine("\"" + dtTemp.Rows[0][11] + "\"");
                }
                else
                {
                    for (int k = 0; k < 5; k++)
                    {
                        csvSW.Write(dtTemp.Rows[0][k] + ",");
                    }
                    csvSW.Write(tempStruct.origin_city + ",");
                    csvSW.Write(tempStruct.dest_city + ",");
                    csvSW.Write(tempStruct.origin_region + ",");
                    csvSW.Write(tempStruct.dest_region + ",");
                    csvSW.Write(dtTemp.Rows[0][5] + ",");
                    csvSW.Write(dtTemp.Rows[0][6] + ",");
                    if (dtTemp.Rows[0][7].ToString().Length > 100)
                        csvSW.WriteLine("\"" + dtTemp.Rows[0][7] + "\"");
                    else
                        csvSW.WriteLine("\"" + dtTemp.Rows[0][8] + "\"");
                    csvSW.Flush();
                    csvSW.Close();
                    csvFS.Close();
                }
            }
            txtSW.Close();
            txtSR.Close();
            txtFS.Close();
            MessageBox.Show("补充完毕");
        }
    }
}
