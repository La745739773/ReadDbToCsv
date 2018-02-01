using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;
using System.Diagnostics;
using System.Collections;
using System.Threading;
using System.Data.OleDb;
namespace ReadDbFile
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CarRbtn.Checked = true;
            PreVersionMode.Enabled = false;
            BtnReadAdditionFile.Enabled = false;
            ProgressFm = new ProgressBar(2, 100);
            //MessageBox.Show("这是一个消息测试github");
        }
        //添加代码用于测试
        SQLiteTransaction transaction = null;
        SQLiteConnection m_dbConnection;
        DataTable dt = null;
        ProgressBar ProgressFm;
        int numOfgroup = 0;
        //bool bIsSelectedMode = false;
        int Mode = -1;
        private void ReadBbBtn_Click(object sender, EventArgs e)
        {
            ProgressFm = new ProgressBar(2, 100);
            if (Mode == -1)
            {
                MessageBox.Show("未选择数据模式");
                return;
            }
            Stopwatch sw = new Stopwatch();
            sw.Start();


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
            string mySql = "SELECT origin_lat,origin_lng FROM path_data group by origin_lat,origin_lng";
            SQLiteCommand cmd = m_dbConnection.CreateCommand();
            cmd.CommandText = mySql;
            SQLiteDataAdapter dao = new SQLiteDataAdapter(cmd);
            dt = new DataTable();
            dao.Fill(dt);
            FileStream Origin_ArrayTxtFS = new FileStream(System.IO.Path.GetDirectoryName(dbPath) + "\\" + System.IO.Path.GetFileName(dbPath).Substring(0, System.IO.Path.GetFileName(dbPath).Length - 3) + "_" + "Origin-Array.txt", FileMode.OpenOrCreate);
            var utf8WithoutBom = new System.Text.UTF8Encoding(false);
            StreamWriter Origin_ArrayTxtSW = new StreamWriter(Origin_ArrayTxtFS, utf8WithoutBom);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Origin_ArrayTxtSW.WriteLine(dt.Rows[i].ItemArray[0].ToString() + "+" + dt.Rows[i].ItemArray[1].ToString());
            }
            Origin_ArrayTxtSW.Close();
            Origin_ArrayTxtFS.Close();
            m_dbConnection.Close();
            string txtPath = System.IO.Path.GetDirectoryName(dbPath) + "\\" + System.IO.Path.GetFileName(dbPath).Substring(0, System.IO.Path.GetFileName(dbPath).Length - 3) + "_" + "Origin-Array.txt";
            FileStream fileS = new FileStream(txtPath, FileMode.Open);
            StreamReader sr = new StreamReader(fileS, utf8WithoutBom);
            string srLine;


            ProgressFm.Show(this);
            int progress = 1;
            while ((srLine = sr.ReadLine()) != null)
            {
                numOfgroup++;
            }
            sr.BaseStream.Seek(0, SeekOrigin.Begin);

            if (Mode == 1)
            {
                while ((srLine = sr.ReadLine()) != null)
                {
                    ProgressFm.setPos(((progress++) / numOfgroup) * 100);//设置进度条位置
                    Car_ModeMakeroutes(dbPath, srLine);
                }
            }
            else if (Mode == 2)
            {
                while ((srLine = sr.ReadLine()) != null)
                {
                    ProgressFm.setPos(((progress++) / numOfgroup) * 100);//设置进度条位置
                    PublicTrans_Make_Routes(dbPath, srLine);
                }
            }
            else if (Mode == 3)
            {
                while ((srLine = sr.ReadLine()) != null)
                {
                    ProgressFm.setPos(((progress++) / numOfgroup) * 100);//设置进度条位置
                    Walking_Riding_Make_Routes(dbPath, srLine);
                }
            }
            else if (Mode == 4)
            {
                while ((srLine = sr.ReadLine()) != null)
                {
                    ProgressFm.setPos(((progress++) / numOfgroup) * 100);//设置进度条位置
                    Walking_Riding_Make_Routes(dbPath, srLine);
                }
            }
            ProgressFm.Close();//关闭窗体
            sw.Stop();
            TimeSpan ts2 = sw.Elapsed;
            MessageBox.Show((ts2.TotalMilliseconds / 1000).ToString("0.0") + "秒转换完毕");
        }
        private void Walking_Riding_Make_Routes(string dbPath, string csvPath)
        {            
            List<string> idArray = new List<string>();
            string conStr = @"Data Source =" + dbPath;
            m_dbConnection = new SQLiteConnection(conStr);
            m_dbConnection.Open();
            int index = csvPath.IndexOf('+');
            string origin_lng = csvPath.Substring(index + 1, csvPath.Length - index - 1);
            string origin_lat = csvPath.Substring(0, index);
            string CalculateNumSql = "SELECT count(*) FROM path_data WHERE origin_lat = '" + origin_lat + "' AND origin_lng = '" + origin_lng + "'";
            SQLiteCommand cmd1 = m_dbConnection.CreateCommand();
            cmd1.CommandText = CalculateNumSql;
            SQLiteDataReader reader = cmd1.ExecuteReader();
            reader.Read();

            FileStream csvFile = null;
            StreamWriter csvSw = null;
            if (File.Exists(System.IO.Path.GetDirectoryName(dbPath) + "\\" + csvPath + ".csv"))
            {
                File.Delete(System.IO.Path.GetDirectoryName(dbPath) + "\\" + csvPath + ".csv");
            }
            csvFile = new FileStream(System.IO.Path.GetDirectoryName(dbPath) + "\\" + csvPath + "_routes" + ".csv", FileMode.Create);
            var utf8WithoutBom = new System.Text.UTF8Encoding(false);
            csvSw = new StreamWriter(csvFile, utf8WithoutBom);

            int count = int.Parse(reader[0].ToString());
            int arrayNum = count / 500 + 1;
            DataTable dtArray = null;
            for (int i = 0; i < arrayNum; i++)
            {
                dtArray = new DataTable();
                string sqlStr = "SELECT * FROM path_data WHERE origin_lat = '" + origin_lat + "' AND origin_lng = '" + origin_lng + "'" + " limit " + (i * 500).ToString() + "," + 500.ToString();
                SQLiteCommand command = m_dbConnection.CreateCommand();
                command.CommandText = sqlStr;
                SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(command);
                dataAdapter.Fill(dtArray);
                for (int j = 0; j < dtArray.Rows.Count; j++)
                {
                    for (int k = 0; k < dtArray.Columns.Count - 1; k++)
                    {
                        csvSw.Write(dtArray.Rows[j][k] + ",");
                    }
                    csvSw.WriteLine("\"" + dtArray.Rows[j][dtArray.Columns.Count-1] + "\"");
                    idArray.Add(dtArray.Rows[j][0].ToString());
                }
                ProgressFm.setPos(((i+1)/ numOfgroup * arrayNum) * 100);//设置进度条位置
            }
            csvSw.Close();
            csvFile.Close();
            idArray.Clear();

        }
        private void PublicTrans_Make_Routes(string dbPath, string csvPath)
        {
            List<string> idArray = new List<string>();
            string conStr = @"Data Source =" + dbPath;
            m_dbConnection = new SQLiteConnection(conStr);
            m_dbConnection.Open();
            int index = csvPath.IndexOf('+');
            string origin_lng = csvPath.Substring(index + 1, csvPath.Length - index - 1);
            string origin_lat = csvPath.Substring(0, index);
            string CalculateNumSql = "SELECT count(*) FROM path_data WHERE origin_lat = '" + origin_lat + "' AND origin_lng = '" + origin_lng + "'";
            SQLiteCommand cmd1 = m_dbConnection.CreateCommand();
            cmd1.CommandText = CalculateNumSql;
            SQLiteDataReader reader = cmd1.ExecuteReader();
            reader.Read();

            FileStream csvFile = null;
            StreamWriter csvSw = null;
            if (File.Exists(System.IO.Path.GetDirectoryName(dbPath) + "\\" + csvPath + ".csv"))
            {
                File.Delete(System.IO.Path.GetDirectoryName(dbPath) + "\\" + csvPath + ".csv");
            }
            csvFile = new FileStream(System.IO.Path.GetDirectoryName(dbPath) + "\\" + csvPath + "_routes" + ".csv", FileMode.Create);
            var utf8WithoutBom = new System.Text.UTF8Encoding(false);
            csvSw = new StreamWriter(csvFile, utf8WithoutBom);

            int count = int.Parse(reader[0].ToString());
            int arrayNum = count / 500 + 1;
            DataTable dtArray = null;
            for (int i = 0; i < arrayNum; i++)
            {
                dtArray = new DataTable();
                string sqlStr = "SELECT * FROM path_data WHERE origin_lat = '" + origin_lat + "' AND origin_lng = '" + origin_lng + "'" + " limit " + (i * 500).ToString() + "," + 500.ToString();
                SQLiteCommand command = m_dbConnection.CreateCommand();
                command.CommandText = sqlStr;
                SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(command);
                dataAdapter.Fill(dtArray);
                for (int j = 0; j < dtArray.Rows.Count; j++)
                {
                    for (int k = 0; k < dtArray.Columns.Count; k++)
                    {
                        csvSw.Write(dtArray.Rows[j][k] + ",");
                    }
                    csvSw.WriteLine();
                    idArray.Add(dtArray.Rows[j][0].ToString());
                }
            }
            csvSw.Close();
            csvFile.Close();
            PublicTrans_Read_Subpaths(dbPath, csvPath, idArray);
            idArray.Clear();
        }

        private void Car_ModeMakeroutes(string dbPath, string csvPath)
        {
            List<string> idArray = new List<string>();
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
                string origin_lng = csvPath.Substring(index + 1, csvPath.Length - index - 1);
                string origin_lat = csvPath.Substring(0, index);
                string CalculateNumSql = "SELECT count(*) FROM path_data WHERE origin_lat = '" + origin_lat + "' AND origin_lng = '" + origin_lng + "'";
                SQLiteCommand cmd1 = m_dbConnection.CreateCommand();
                cmd1.CommandText = CalculateNumSql;
                SQLiteDataReader reader = cmd1.ExecuteReader();
                reader.Read();
                
                FileStream csvFile = null;
                StreamWriter csvSw = null;
                if (File.Exists(System.IO.Path.GetDirectoryName(dbPath) + "\\" + csvPath + ".csv"))
                {
                    //csvFile = new FileStream(System.IO.Path.GetDirectoryName(dbPath) + "\\" + csvPath + "routes"+ ".csv", FileMode.Open);
                    //var utf8WithoutBom = new System.Text.UTF8Encoding(false);
                    //csvSw = new StreamWriter(csvFile, utf8WithoutBom);
                    //csvSw.BaseStream.Seek(0, SeekOrigin.End);
                    File.Delete(System.IO.Path.GetDirectoryName(dbPath) + "\\" + csvPath + ".csv");
                }
                csvFile = new FileStream(System.IO.Path.GetDirectoryName(dbPath) + "\\" + csvPath + "_routes" + ".csv", FileMode.Create);
                var utf8WithoutBom = new System.Text.UTF8Encoding(false);
                csvSw = new StreamWriter(csvFile, utf8WithoutBom);
                
                int count = int.Parse(reader[0].ToString());
                int arrayNum = count / 500 + 1;
                DataTable dtArray = null;
                for (int i = 0; i < arrayNum; i++)
                {
                    dtArray = new DataTable();
                    string sqlStr = "SELECT * FROM path_data WHERE origin_lat = '" + origin_lat + "' AND origin_lng = '" + origin_lng + "'" + " limit " + (i * 500).ToString() + "," + 500.ToString();
                    SQLiteCommand command = m_dbConnection.CreateCommand();
                    command.CommandText = sqlStr;
                    SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(command);
                    dataAdapter.Fill(dtArray);
                    for (int j = 0; j < dtArray.Rows.Count; j++)
                    {
                        for (int k = 0; k < dtArray.Columns.Count; k++)
                        {
                            csvSw.Write(dtArray.Rows[j][k] + ",");
                        }
                        csvSw.WriteLine();
                        idArray.Add(dtArray.Rows[j][0].ToString());
                    }
                }
                csvSw.Close();
                csvFile.Close();
                CarModeReadsubPaths(dbPath,csvPath,idArray);
                idArray.Clear();
            }
        }
        private void  PublicTrans_Read_Subpaths(string DbPath,string CsvName,List<string> idList)
        {
            var utf8WithoutBom = new System.Text.UTF8Encoding(false);

            if (File.Exists(System.IO.Path.GetDirectoryName(DbPath) + "\\" + CsvName.Substring(0, CsvName.Length - 11) + "_subpaths" + ".csv"))
            {
                File.Delete(System.IO.Path.GetDirectoryName(DbPath) + "\\" + CsvName.Substring(0, CsvName.Length - 11) + "_subpaths" + ".csv");
            }
            FileStream subPathcsvFile = new FileStream(System.IO.Path.GetDirectoryName(DbPath) + "\\" + CsvName + "_subpaths" + ".csv", FileMode.Create);
            StreamWriter subPathcsvSw = new StreamWriter(subPathcsvFile, utf8WithoutBom);
            int num = idList.Count / 500 + 1;
            for (int i = 0; i < idList.Count / 500 + 1; i++)
            {
                DataTable dtArray = new DataTable();
                string sqlStr = "SELECT * FROM subpath WHERE ";
                for (int j = i * 500; j < (i + 1) * 500 && j < idList.Count; j++)
                {
                    string id = (string)idList[j];
                    sqlStr += "route_id = " + id + " or ";
                }
                sqlStr = sqlStr.Substring(0, sqlStr.Length - 3);
                //开启事务
                transaction = m_dbConnection.BeginTransaction();

                SQLiteCommand command = m_dbConnection.CreateCommand();
                command.CommandText = sqlStr;

                SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(command);
                dataAdapter.Fill(dtArray);
                //提交事务
                transaction.Commit();
                for (int j = 0; j < dtArray.Rows.Count; j++)
                {
                    for (int k = 0; k < 10; k++)
                    {
                        subPathcsvSw.Write(dtArray.Rows[j][k] + ",");
                    }
                    subPathcsvSw.WriteLine("\"" + dtArray.Rows[j][10] + "\"");
                }

                ProgressFm.setPos((int)((i + 1) / (double)(num * numOfgroup) * 100));//设置进度条位置
            }
            m_dbConnection.Close();
            subPathcsvSw.Close();
            subPathcsvFile.Close();
        }

        private void CarModeReadsubPaths(string DbPath,string CsvName,List<string> idList)
        {
            var utf8WithoutBom = new System.Text.UTF8Encoding(false);

            if (File.Exists(System.IO.Path.GetDirectoryName(DbPath) + "\\" + CsvName.Substring(0, CsvName.Length - 11) + "_subpaths" + ".csv"))
            {
                File.Delete(System.IO.Path.GetDirectoryName(DbPath) + "\\" + CsvName.Substring(0, CsvName.Length - 11) + "_subpaths" + ".csv");
            }
            FileStream subPathcsvFile = new FileStream(System.IO.Path.GetDirectoryName(DbPath) + "\\" + CsvName + "_subpaths" + ".csv", FileMode.Create); 
            StreamWriter subPathcsvSw = new StreamWriter(subPathcsvFile, utf8WithoutBom);
            int num = idList.Count / 500 + 1;
            for (int i = 0; i < idList.Count/500 + 1;i++)
            {
                //int index = aryLine.IndexOf(',');
                //string id = aryLine.Substring(0, index);   
                DataTable dtArray = new DataTable(); 
                string sqlStr = "SELECT * FROM subpath WHERE ";
                for(int j = i*500;j<(i+1)*500 && j < idList.Count;j++)
                {
                    string id = (string)idList[j];
                    sqlStr += "route_id = " + id + " or ";
                }
                sqlStr = sqlStr.Substring(0, sqlStr.Length - 3);

                //开启事务
                transaction = m_dbConnection.BeginTransaction();

                SQLiteCommand command = m_dbConnection.CreateCommand();
                command.CommandText = sqlStr;

                SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(command);
                dataAdapter.Fill(dtArray);
                //提交事务
                transaction.Commit();;

                for (int j = 0; j < dtArray.Rows.Count; j++)
                {
                    for (int k = 0; k < 11; k++)
                    {
                        subPathcsvSw.Write(dtArray.Rows[j][k] + ",");
                    }
                    subPathcsvSw.WriteLine("\"" + dtArray.Rows[j][11] + "\"");
                }

                ProgressFm.setPos((int)((i+1)/(double)(num * numOfgroup) * 100));//设置进度条位置

            }
             
            m_dbConnection.Close();
            subPathcsvSw.Close();
            subPathcsvFile.Close();
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
        private void extract_Routes_City_Info_Click(object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "请选择附加路径文件";
            dialog.Filter = "txt文件(*.txt)|*.txt";
            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }
            string txtPath = dialog.FileName;
            FileStream txtFS = new FileStream(txtPath, FileMode.Open);
            var utf8WithoutBom = new System.Text.UTF8Encoding(false);
            StreamReader txtSR = new StreamReader(txtFS, utf8WithoutBom);
            StreamWriter txtSW = new StreamWriter(txtFS, utf8WithoutBom);
            string aryLine = null;

            OpenFileDialog dialog2 = new OpenFileDialog();
            dialog2.Title = "请选择文件";
            dialog2.Filter = "db文件(*.db)|*.db";
            if (dialog2.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }
            string dbPath = dialog2.FileName;

            //string dbPath = @"C:\Users\i\Desktop\最新版本程序\CountySeat_info.db";
            string conStr = @"Data Source =" + dbPath;
            m_dbConnection = new SQLiteConnection(conStr);
            m_dbConnection.Open();

            double index = 0;
            ProgressBar ProgressFm2 = new ProgressBar(2, 100);
            ProgressFm2.Show(this);
            ProgressFm2.setPos(0);//设置进度条位置 
            int num = 0;
            while ((aryLine = txtSR.ReadLine()) != null)
            {
                num++;
            }
            //txtSR.BaseStream.Seek(0, SeekOrigin.Begin);
            txtFS.Close();
            txtSR.Close();
            FileStream txtFS2 = new FileStream(txtPath, FileMode.Open);
            StreamReader txtSR2 = new StreamReader(txtFS2, utf8WithoutBom);
            while((aryLine = txtSR2.ReadLine()) != null)
            {
                FileStream csvFS = new FileStream(System.IO.Path.GetDirectoryName(txtPath) + "\\" + aryLine + ".csv", FileMode.Open);
                StreamReader csvSR = new StreamReader(csvFS, utf8WithoutBom);
                SQLiteCommand cmd = m_dbConnection.CreateCommand();
                int flag = 0;
                string pathInfo = "";
                string[] pathInfoArray;
                var transaction = m_dbConnection.BeginTransaction();
                while ((pathInfo = csvSR.ReadLine()) != null)
                {
                    pathInfoArray = pathInfo.Split(',');
                    if (flag == 0)
                    {
                        string sql = "INSERT INTO county_info VALUES('" + pathInfoArray[7] + pathInfoArray[5] + "','" + pathInfoArray[5] + "','" + pathInfoArray[7] + "','" + pathInfoArray[2] + "','" + pathInfoArray[1] + "')";
                        cmd.CommandText = sql;
                        try
                        {
                            cmd.ExecuteNonQuery();
                        }
                        catch (System.Exception ex)
                        {
                        	continue;
                        }
                        flag++;
                    }
                    SQLiteCommand command = m_dbConnection.CreateCommand();
                    string sqlStr = "INSERT INTO path_info VALUES(";
                    sqlStr += pathInfoArray[0] + ",'";
                    for (int i = 1; i < 10; i++)
                    {
                        sqlStr += pathInfoArray[i] + "','";
                    }
                    sqlStr += pathInfoArray[10] + "')";

                    command.CommandText = sqlStr;
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (System.Exception ex)
                    {
                        continue;
                    }
                }
                transaction.Commit();
                csvSR.Close();
                csvFS.Close();
                index++;
                ProgressFm2.setPos((int)(((index + 1) / num) * 100));//设置进度条位置 
            }
            txtSR2.Close();
            txtFS2.Close();
            m_dbConnection.Close();
            sw.Stop();
            TimeSpan ts2 = sw.Elapsed;
            ProgressFm2.Close();
            MessageBox.Show((ts2.TotalMilliseconds / 1000).ToString("0.0") + "Finished");
            //MessageBox.Show("Finished!");
        }

        private void Add_Pop_Gdp_Btn_Click(object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "请选择人口和GDP文件";
            dialog.Filter = "csv文件(*.csv)|*.csv";
            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }
            string csvPath = dialog.FileName;
            FileStream csvFS = new FileStream(csvPath, FileMode.Open);
            var utf8WithoutBom = new System.Text.UTF8Encoding(false);
            StreamReader csvSR = new StreamReader(csvFS, utf8WithoutBom);
            int numOrder = 0;
            string aryLine;
            while ((aryLine = csvSR.ReadLine()) != null)
            {
                numOrder++;
            }
            csvSR.BaseStream.Seek(0, SeekOrigin.Begin);



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


            double index = -1;
            ProgressBar ProgressFm2 = new ProgressBar(2, 100);
            ProgressFm2.Show(this);
            ProgressFm2.setPos(0);//设置进度条位置 
            var transaction = m_dbConnection.BeginTransaction();
            while ((aryLine = csvSR.ReadLine()) != null)
            {
                if (index == -1)
                {
                    index++;
                    continue;
                }
                string[] info = aryLine.Split(',');
                string cityFullname = info[2] + info[1];
                string sql = "UPDATE county_info SET id = " + info[0] + ",population = " + info[5] + ",GDP = " + info[6] + " WHERE City_FullName = '" + cityFullname + "'";
                SQLiteCommand cmd = m_dbConnection.CreateCommand();
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                index++;
                ProgressFm2.setPos((int)(((index + 1) / numOrder) * 100));//设置进度条位置 
            }
            transaction.Commit();
            ProgressFm2.Close();
            sw.Stop();
            TimeSpan ts2 = sw.Elapsed;
            MessageBox.Show((ts2.TotalMilliseconds / 1000).ToString("0.0") + "Finished");
        }
        private void Calc_Nhour_Pop_GDP_Btn_Click(object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            int Allhour = 10;
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


            for (int i = 1; i <= Allhour; i++)
            {
                SQLiteCommand commandTemp = m_dbConnection.CreateCommand();
                string sql_AddPop = "ALTER TABLE county_info ADD '" + i.ToString() + "hourPop' DOUBLE";
                commandTemp.CommandText = sql_AddPop;
                try
                {
                    commandTemp.ExecuteNonQuery();
                }
                catch (System.Exception ex)
                {
                	continue;
                }
                
                string sql_AddGDP = "ALTER TABLE county_info ADD '" + i.ToString() + "hourGDP' DOUBLE";
                commandTemp.CommandText = sql_AddGDP;
                try
                {
                    commandTemp.ExecuteNonQuery();
                }
                catch (System.Exception ex)
                {
                    continue;
                }
                string sql_AddCounty_Array = "ALTER TABLE county_info ADD '" + i.ToString() + "County_Array' STRING";
                commandTemp.CommandText = sql_AddCounty_Array;
                try
                {
                    commandTemp.ExecuteNonQuery();
                }
                catch (System.Exception ex)
                {
                    continue;
                }
            }



            DataTable dTable = new DataTable();
            string sql = "SELECT * FROM county_info";

            //开启事务
            var transaction = m_dbConnection.BeginTransaction();

            SQLiteCommand command = m_dbConnection.CreateCommand();
            command.CommandText = sql;

            SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(command);
            dataAdapter.Fill(dTable);
            //提交事务
            transaction.Commit();


            Double[] AllPop = new Double[Allhour];
            Double[] AllGDP = new Double[Allhour];
            string[] AllCounty_Array = new string[Allhour];


            ProgressBar ProgressFm2 = new ProgressBar(2, 100);
            ProgressFm2.Show(this);
            ProgressFm2.setPos(0);//设置进度条位置 

            int numOrder = dTable.Rows.Count;
            for (int i = 0; i < numOrder; i++)
            {
                string origin = dTable.Rows[i][2].ToString();
                string origin_Region = dTable.Rows[i][3].ToString();
                string sql_SelectCountyPath = "SELECT destination,destination_region,duration_s FROM path_info WHERE origin = '" + origin + "' AND origin_region = '" + origin_Region + "'";
                var transaction2 = m_dbConnection.BeginTransaction();
                SQLiteCommand cmd_SelectCountyPath = m_dbConnection.CreateCommand();
                cmd_SelectCountyPath.CommandText = sql_SelectCountyPath;
                SQLiteDataAdapter dataAdapter2 = new SQLiteDataAdapter(cmd_SelectCountyPath);
                DataTable dT_CountyPath = new DataTable();
                dataAdapter2.Fill(dT_CountyPath);
                transaction2.Commit();

                var transaction3 = m_dbConnection.BeginTransaction();

                for (int l = 0; l < Allhour; l++)
                {
                    if (l == 0)
                    {
                        try
                        {
                            AllPop[l] = double.Parse(dTable.Rows[i][4].ToString());
                            AllGDP[l] = double.Parse(dTable.Rows[i][5].ToString());
                            AllCounty_Array[l] = dTable.Rows[i][1].ToString() + ";";
                        }
                        catch (System.Exception ex)
                        {
                            AllPop[l] = 0;
                            AllGDP[l] = 0;
                            AllCounty_Array[l] = dTable.Rows[i][1].ToString() + ";";
                        }
                        continue; 
                    }
                    try
                    {
                        AllPop[l] = 0;
                        AllGDP[l] = 0;
                        AllCounty_Array[l] = "";
                    }
                    catch (System.Exception ex)
                    {

                    }

                }

                for (int j = 0; j < dT_CountyPath.Rows.Count; j++)
                {
                    string cityFullName = dT_CountyPath.Rows[j][1].ToString() + dT_CountyPath.Rows[j][0].ToString();
                    int time = 0;
                    if (double.Parse(dT_CountyPath.Rows[j][2].ToString()) % 3600 == 0)
                    {
                        time = (int)(double.Parse(dT_CountyPath.Rows[j][2].ToString()) / 3600);
                    }
                    else
                    {
                        time = (int)(double.Parse(dT_CountyPath.Rows[j][2].ToString()) / 3600) + 1;
                    }
                    if(time >10)
                        continue;
                    string sql_Select_Pop_Gdp = "SELECT population,GDP FROM county_info WHERE City_FullName = '" + cityFullName + "'";

                    SQLiteCommand cmd_Select_Pop_Gdp = m_dbConnection.CreateCommand();
                    cmd_Select_Pop_Gdp.CommandText = sql_Select_Pop_Gdp;
                    SQLiteDataReader reader = cmd_Select_Pop_Gdp.ExecuteReader();
                    reader.Read();
                    try
                    {
                        double poplation = double.Parse(reader[0].ToString());
                        double GDP = double.Parse(reader[1].ToString());
                        AllPop[time - 1] += poplation;
                        AllGDP[time - 1] += GDP;
                        AllCounty_Array[time - 1] += cityFullName + ";";
                    }
                    catch (System.Exception ex)
                    {
                    	continue;
                    }
                }
                for (int k = 1; k < Allhour;k++ )
                {
                    AllPop[k] += AllPop[k - 1];
                    AllGDP[k] += AllPop[k - 1];
                    AllCounty_Array[k] = AllCounty_Array[k - 1] + AllCounty_Array[k];
                }
                for(int k = 0;k<Allhour;k++)
                {
                    string sql_Update_Pop_Gdp = "UPDATE county_info SET '" + (k + 1).ToString() + "hourPop' = '" + AllPop[k].ToString() + "','" + (k + 1).ToString() + "hourGDP' = '" + AllGDP[k].ToString()
                        + "','" + (k + 1).ToString() + "County_Array' = '" + AllCounty_Array[k] + "' WHERE City_FullName = '" + origin_Region + origin + "'"; //加Where条件
                    SQLiteCommand cmd_Update_Pop_Gdp = m_dbConnection.CreateCommand(); 
                    cmd_Update_Pop_Gdp.CommandText = sql_Update_Pop_Gdp;
                    cmd_Update_Pop_Gdp.ExecuteNonQuery();
                }
                transaction3.Commit();
                ProgressFm2.setPos((int)((double)(i + 1) / (double)numOrder * 100));//设置进度条位置 
            }
            ProgressFm2.Close();
            sw.Stop();
            TimeSpan ts2 = sw.Elapsed;
            MessageBox.Show((ts2.TotalMilliseconds / 1000).ToString("0.0") + "Finished");
      
        }
        private void ParseError_File_Adjust_Btn_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "请选择文件";
            dialog.Filter = "csv文件(*.csv)|*.csv";
            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }
            string csvPath = dialog.FileName;
            FileStream csvFS = new FileStream(csvPath, FileMode.Open);
            var utf8WithoutBom = new System.Text.UTF8Encoding(false);
            StreamReader csvSR = new StreamReader(csvFS, utf8WithoutBom);

            string aryLine;
            aryLine = csvSR.ReadLine();

            OpenFileDialog dialog2 = new OpenFileDialog();
            dialog2.Title = "请选择文件";
            dialog2.Filter = "csv文件(*.csv)|*.csv";
            if (dialog2.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }
            FileStream csvFS2 = new FileStream(dialog2.FileName, FileMode.Open);
            StreamWriter csvSW = new StreamWriter(csvFS2, utf8WithoutBom);
            csvSW.WriteLine(aryLine);
            while ((aryLine = csvSR.ReadLine()) != null)
            {
                csvSW.WriteLine(aryLine + ",,,,");
            }

            csvSW.Close();
            csvSR.Close();
            csvFS.Close();
            MessageBox.Show("finished");
        }

        private void Choose_OD_EXC_BTN_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Excel文件(*.xls;*.xlsx)|*.xls;*.xlsx|所有文件|*.*";
            ofd.ValidateNames = true;
            ofd.CheckPathExists = true;
            ofd.CheckFileExists = true;
            if (ofd.ShowDialog() != DialogResult.OK)
            {
                return;
                //其他代码
            }
            string strFileName = ofd.FileName;
            string connstring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strFileName + ";Extended Properties='Excel 8.0;HDR=NO;IMEX=1';"; // Office 07及以上版本 不能出现多余的空格 而且分号注意
            DataTable oriDT = new DataTable();
            DataTable desDT = new DataTable();
            using (OleDbConnection conn = new OleDbConnection(connstring))
            {
                conn.Open();
                string SheetName = "ori$";
                DataTable sheetsName = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "Table" }); //得到所有sheet的名字
                string firstSheetName = sheetsName.Rows[0][2].ToString(); //得到第一个sheet的名字
                string sql = string.Format("SELECT * FROM [{0}]", SheetName); //查询字符串
                OleDbDataAdapter ada = new OleDbDataAdapter(sql, connstring);
                //DataSet set = new DataSet();
                ada.Fill(oriDT);
                SheetName = "des$";
                sql = string.Format("SELECT * FROM [{0}]", SheetName); //查询字符串
                ada = new OleDbDataAdapter(sql, connstring);
                ada.Fill(desDT);
            }

            FileStream csvFS = new FileStream(System.IO.Path.GetDirectoryName(strFileName) + "\\od_Path.csv", FileMode.OpenOrCreate);
            var utf8WithoutBom = new System.Text.UTF8Encoding(false);
            StreamWriter csvSW = new StreamWriter(csvFS, utf8WithoutBom);
            //id,origin_lat,origin_lng,des_lat,des_lng,origin,des,origin_region,des_region
            csvSW.WriteLine("id,origin_lat,origin_lng,des_lat,des_lng,origin,des,origin_region,des_region");
            int id = 1;
            for (int i = 0; i < oriDT.Rows.Count; i++)
            {
                for (int j = 0; j < desDT.Rows.Count; j++)
                {
                    string origin_lng = oriDT.Rows[i][2].ToString();
                    string origin_lat = oriDT.Rows[i][3].ToString();
                    string des_lng = desDT.Rows[j][2].ToString();
                    string des_lat = desDT.Rows[j][3].ToString();
                    csvSW.Write(id.ToString() + ",");
                    csvSW.Write(origin_lat + "," + origin_lng + "," + des_lat + "," + des_lng + ",");
                    csvSW.WriteLine(",,,");
                    id++;
                }
            }
            MessageBox.Show("finished!");
            csvSW.Close();
            csvFS.Close();
        }
        private void CarRbtn_CheckedChanged(object sender, EventArgs e)
        {
            if (CarRbtn.Checked == true)
            {
                Mode = 1;
            }
            else
            {
                Mode = -1;
            }
        }

        private void PublictRbtn_CheckedChanged(object sender, EventArgs e)
        {
            if (PublictRbtn.Checked == true)
            {
                Mode = 2;
            }
            else
            {
                Mode = -1;
            }
        }

        private void RidRbtn_CheckedChanged(object sender, EventArgs e)
        {
            if (RidRbtn.Checked == true)
            {
                Mode = 3;
            }
            else
            {
                Mode = -1;
            }
        }

        private void WalkRbtn_CheckedChanged(object sender, EventArgs e)
        {
            if (WalkRbtn.Checked == true)
            {
                Mode = 4;
            }
            else
            {
                Mode = -1;
            }
        }



    }
}
