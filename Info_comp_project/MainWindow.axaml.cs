using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Net;
using System.Threading.Tasks;
using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Renci.SshNet;

namespace Info_comp_project;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    
    private async void Request_OnClick(object? sender, RoutedEventArgs e)
    {
        if (DNS_name.Text == " " || loginTB.Text == " " || PassTB.Text == " ")
        {
            var messageBoxStandardWindow33 = MessageBox.Avalonia.MessageBoxManager
                .GetMessageBoxStandardWindow("Ошибка!","Пустые поля!");
            messageBoxStandardWindow33.Show();
        }
        else
        {
            /*
            Имя компьютера - hostname
            Процессор - cat /proc/cpuinfo
            материнская плата - sudo dmesg | grep DMI:
            жесткий диск - lsblk
            видеокарта - lspci | grep -E "VGA|3D"
            озу - sudo dmidecode --type 17
            сетевая карта -  lspci | grep -i 'net'            
        */
        
        ProcessStartInfo startInfo2 = new ProcessStartInfo() { FileName = "/bin/bash", Arguments = "mkdir info", }; 
        Process proces = new Process() { StartInfo = startInfo2, };
        proces.Start();
        
        
        var mainhost = System.Net.Dns.GetHostName();
        IPAddress addres1 = Dns.GetHostAddresses(mainhost).First<IPAddress>(f=>f.AddressFamily==System.Net.Sockets.AddressFamily.InterNetwork);
        string addres2 = Convert.ToString(addres1);

        var abc = await Dns.GetHostEntryAsync(DNS_name.Text);
        string domain_name = Convert.ToString(abc);
        IPAddress addres3 = Dns.GetHostAddresses(domain_name).First<IPAddress>(f=>f.AddressFamily==System.Net.Sockets.AddressFamily.InterNetwork);
        string addres4 = Convert.ToString(addres3);
        string host = Convert.ToString(domain_name);
        string username;
        string password;
        username = loginTB.Text;
        password = PassTB.Text;

        var connect_info = new ConnectionInfo(host, username, new PasswordAuthenticationMethod(username, password));
        
        using (var client = new SshClient(connect_info))
        {
            client.Connect();
            var runner = client.RunCommand("lscpu > hostname.txt");
            var runner2 = client.RunCommand("cat /proc/cpuinfo > cpu.txt");
            var runner3 = client.RunCommand("sudo dmesg | grep DMI: > motherboard.txt");
            var runner4 = client.RunCommand("lsblk > harddisk.txt");
            var runner5 = client.RunCommand("lspci | grep -E  'VGA|3D' > GPUout.txt");
            var runner6 = client.RunCommand("sudo dmidecode --type 17 > OZUout.txt");
            var runner7 = client.RunCommand("lspci | grep -i 'net' > netcard.txt");

            var runner22 = client.RunCommand("mkdir test");
            var runner8 =
                client.RunCommand($"scp ./hostname.txt {host}@{addres2}:~/test");
            var runner9 =
                client.RunCommand($"scp ./cpu.txt {host}@{addres2}:~/test");
            var runner10 =
                client.RunCommand($"scp ./motherboard.txt {host}@{addres2}:~/test");
            var runner11 =
                client.RunCommand($"scp ./harddisk.txt {host}@{addres2}:~/test");
            var runner12 =
                client.RunCommand($"scp ./GPUout.txt {host}@{addres2}:~/test");
            var runner13 =
                client.RunCommand($"scp ./OZUout.txt {host}@{addres2}:~/test");
            var runner14 =
                client.RunCommand($"scp ./netcard.txt {host}@{addres2}:~/test"); 
            
            var runner15 = client.RunCommand("rm hostname.txt");
            var runner16 = client.RunCommand("rm cpu.txt");
            var runner17 = client.RunCommand("rm motherboard.txt");
            var runner18 = client.RunCommand("rm harddisk.txt");
            var runner19 = client.RunCommand("rm GPUout.txt");
            var runner20 = client.RunCommand("rm OZUout.txt");
            var runner21 = client.RunCommand("rm netcard.txt");
            var runner23 = client.RunCommand("rmdir test");

            
            client.Disconnect();
            if (runner.ExitStatus != 0)
            {
                var messageBoxStandardWindow = MessageBox.Avalonia.MessageBoxManager
                    .GetMessageBoxStandardWindow("Ошибка!", runner.Error);
                messageBoxStandardWindow.Show();
            }
            else if (runner2.ExitStatus != 0)
            {
                var messageBoxStandardWindow2 = MessageBox.Avalonia.MessageBoxManager
                    .GetMessageBoxStandardWindow("Ошибка!", runner2.Error);
                messageBoxStandardWindow2.Show();
            }
            else if (runner3.ExitStatus != 0)
            {
                var messageBoxStandardWindow3 = MessageBox.Avalonia.MessageBoxManager
                    .GetMessageBoxStandardWindow("Ошибка!", runner3.Error);
                messageBoxStandardWindow3.Show();
            }
            else if (runner4.ExitStatus != 0)
            {
                var messageBoxStandardWindow4 = MessageBox.Avalonia.MessageBoxManager
                    .GetMessageBoxStandardWindow("Ошибка!", runner4.Error);
                messageBoxStandardWindow4.Show();
            }
            else if (runner5.ExitStatus != 0)
            {
                var messageBoxStandardWindow5 = MessageBox.Avalonia.MessageBoxManager
                    .GetMessageBoxStandardWindow("Ошибка!", runner5.Error);
                messageBoxStandardWindow5.Show();
            }
            else if (runner6.ExitStatus != 0)
            {
                var messageBoxStandardWindow6 = MessageBox.Avalonia.MessageBoxManager
                    .GetMessageBoxStandardWindow("Успешно!", runner6.Error);
                messageBoxStandardWindow6.Show();
            }
            else if (runner7.ExitStatus != 0)
            {
                var messageBoxStandardWindow7 = MessageBox.Avalonia.MessageBoxManager
                    .GetMessageBoxStandardWindow("Ошибка!", runner7.Error);
                messageBoxStandardWindow7.Show();
            }
            else if (runner8.ExitStatus != 0)
            {
                var messageBoxStandardWindow8 = MessageBox.Avalonia.MessageBoxManager
                    .GetMessageBoxStandardWindow("Ошибка!", runner8.Error);
                messageBoxStandardWindow8.Show();
            }
            else if (runner9.ExitStatus != 0)
            {
                var messageBoxStandardWindow9 = MessageBox.Avalonia.MessageBoxManager
                    .GetMessageBoxStandardWindow("Ошибка!", runner9.Error);
                messageBoxStandardWindow9.Show();
            }
            else if (runner10.ExitStatus != 0)
            {
                var messageBoxStandardWindow10 = MessageBox.Avalonia.MessageBoxManager
                    .GetMessageBoxStandardWindow("Ошибка!", runner10.Error);
                messageBoxStandardWindow10.Show();
            }
            else if (runner11.ExitStatus != 0)
            {
                var messageBoxStandardWindow11 = MessageBox.Avalonia.MessageBoxManager
                    .GetMessageBoxStandardWindow("Успешно!", runner11.Error);
                messageBoxStandardWindow11.Show();
            }
            else if (runner12.ExitStatus != 0)
            {
                var messageBoxStandardWindow12 = MessageBox.Avalonia.MessageBoxManager
                    .GetMessageBoxStandardWindow("Успешно!", runner12.Error);
                messageBoxStandardWindow12.Show();
            }
            else if (runner13.ExitStatus != 0)
            {
                var messageBoxStandardWindow13 = MessageBox.Avalonia.MessageBoxManager
                    .GetMessageBoxStandardWindow("Успешно!", runner13.Error);
                messageBoxStandardWindow13.Show();
            }
            else if (runner14.ExitStatus != 0)
            {
                var messageBoxStandardWindow14 = MessageBox.Avalonia.MessageBoxManager
                    .GetMessageBoxStandardWindow("Успешно!", runner14.Error);
                messageBoxStandardWindow14.Show();
            }
            else if (runner15.ExitStatus != 0)
            {
                var messageBoxStandardWindow15 = MessageBox.Avalonia.MessageBoxManager
                    .GetMessageBoxStandardWindow("Успешно!", runner15.Error);
                messageBoxStandardWindow15.Show();
            }
            else if (runner16.ExitStatus != 0)
            {
                var messageBoxStandardWindow16 = MessageBox.Avalonia.MessageBoxManager
                    .GetMessageBoxStandardWindow("Успешно!", runner16.Error);
                messageBoxStandardWindow16.Show();
            }
            else if (runner17.ExitStatus != 0)
            {
                var messageBoxStandardWindow17 = MessageBox.Avalonia.MessageBoxManager
                    .GetMessageBoxStandardWindow("Успешно!", runner17.Error);
                messageBoxStandardWindow17.Show();
            }
            else if (runner18.ExitStatus != 0)
            {
                var messageBoxStandardWindow18 = MessageBox.Avalonia.MessageBoxManager
                    .GetMessageBoxStandardWindow("Успешно!", runner18.Error);
                messageBoxStandardWindow18.Show();
            }
            else if (runner19.ExitStatus != 0)
            {
                var messageBoxStandardWindow19 = MessageBox.Avalonia.MessageBoxManager
                    .GetMessageBoxStandardWindow("Успешно!", runner19.Error);
                messageBoxStandardWindow19.Show();
            }
            else if (runner20.ExitStatus != 0)
            {
                var messageBoxStandardWindow20 = MessageBox.Avalonia.MessageBoxManager
                    .GetMessageBoxStandardWindow("Успешно!", runner20.Error);
                messageBoxStandardWindow20.Show();
            }
            else if (runner21.ExitStatus != 0)
            {
                var messageBoxStandardWindow21 = MessageBox.Avalonia.MessageBoxManager
                    .GetMessageBoxStandardWindow("Успешно!", runner21.Error);
                messageBoxStandardWindow21.Show();
            }
        }

        List<Info_comps> ic2 = new List<Info_comps>();
        string[] str =  File.ReadAllLines(@".\info\cputest.txt");
        string[] str2 =  File.ReadAllLines(@".\info\hostname.txt");
        string[] str3 =  File.ReadAllLines(@".\info\mattest.txt");
        string[] str4 =  File.ReadAllLines(@".\info\OZUout.txt");
        string[] str5 =  File.ReadAllLines(@".\info\GPUout.txt");
        string[] str6 =  File.ReadAllLines(@".\info\harddisk.txt");
        string[] str7 =  File.ReadAllLines(@".\info\netcard.txt");

        
        string hs="1", cpu ="1", motherboard="1", harddisk = "1", videocard = "1", ozu = "1", networkcard = "1", proc = "1";
        int count=0;
        
        for (int i = 0; i < str.Length; i++)
        {
            if (str[i].Contains("model name"))
            {
                proc = str[i].Split(':')[1];
                cpu = proc;
                count++;
                ic2.Add(new Info_comps()
                {
                    Discription = "Процессор",
                    Value = cpu
                });
                break;
            }
        }
        for (int i = 0; i < str2.Length; i++)
        {
            hs = str2[i];
            count++;
            ic2.Add(new Info_comps()
            {
                Discription = "Название компьютера",
                Value = hs
            });
            break;
        }
        for (int i = 0; i < str3.Length; i++)
        {
            
            proc = str3[i].Split(':')[1];
            motherboard = proc;
            count++;
            ic2.Add(new Info_comps()
            {
                Discription = "Материнская плата",
                Value = motherboard
            });
            break;
        }
        
        for (int i = 0; i < str6.Length; i++)
        {
            if (str6[i].Contains("sda"))
            {
                string disk = str6[i];
                harddisk = disk;
                count++;
                ic2.Add(new Info_comps()
                {
                    Discription = "Жесткий диск",
                    Value = harddisk
                });
                break;
            }
            
        }
        
        for (int i = 0; i < str5.Length; i++)
        {
            string card = str5[i].Split(':')[1];
            videocard = card;
            count++;
            ic2.Add(new Info_comps()
            {
                Discription = "Видеокарта",
                Value = videocard
            });
            break;
        }
        
        for (int i = 0; i < str4.Length; i++)
        {
            if (str4[i].Contains("Size"))
            {
                string op = str4[i].Split(':')[1];
                ozu = op;
                count++;
                ic2.Add(new Info_comps()
                {
                    Discription = "ОЗУ",
                    Value = ozu
                });
                break;
            }
            
        }
        
        for (int i = 0; i < str7.Length; i++)
        {
            string net = str7[i].Split("r:")[1];
            networkcard = net;
            count++;
            ic2.Add(new Info_comps()
            {
                Discription = "Сетевая карта",
                Value = networkcard
            });
            break;
        }
        Table1.Items = ic2;
        ProcessStartInfo startInfo = new ProcessStartInfo() {FileName = "/bin/bash", Arguments = "cd ./info; rm cputest.txt hostname.txt mattest.txt OZUout.txt GPUout.txt harddisk.txt netcard.txt"};
        proces = new Process() { StartInfo = startInfo, };
        proces.Start();
        var messageBoxStandardWindow22 = MessageBox.Avalonia.MessageBoxManager
            .GetMessageBoxStandardWindow("Успешно!", "запрос выполнился!");
        messageBoxStandardWindow22.Show();
        
        ProcessStartInfo startInfo3 = new ProcessStartInfo() { FileName = "/bin/bash", Arguments = "rmdir info", }; 
        Process proces2 = new Process() { StartInfo = startInfo3, };
        proces2.Start();
        }
    }

    private void OnAutoGeneratingColumn(object? sender, DataGridAutoGeneratingColumnEventArgs e)
    {
    }
    
    public bool IsFileInUse(string path)
    {
        FileStream? stream = null;
        
        try {
            stream = new FileStream(path, FileMode.Open, FileAccess.Read);
        } 
        catch (IOException) {
            return true;
        }
        finally
        {
            stream?.Close();
        }

        return false;
    }

    private void Otchet_excel_OnClick(object? sender, RoutedEventArgs e)
    {
        if (Table1.Items == null)
        {
            return;
        }

        const string filename = "Домашняя/Desktop/file.xlsx";
        if (IsFileInUse(filename))
        {
            var messageBoxStandardWindow = MessageBox.Avalonia.MessageBoxManager
                .GetMessageBoxStandardWindow("Ошибка!", "Файл открыт!");
            messageBoxStandardWindow.ShowDialog(this);
            return;
        }

        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("Информация о комлектующих");
         
            int row = 1;
            worksheet.Cell("A1").Value = "Название";
            worksheet.Cell("B1").Value = "Значение";
            row++;

            foreach (Info_comps computerInfo in Table1.Items)
            {
                worksheet.Cell("A" + row).Value = computerInfo.Discription;
                worksheet.Cell("B" + row).Value = computerInfo.Value;
                row++;
            }

            worksheet.Columns().AdjustToContents();
            workbook.SaveAs(filename);
        }
    }

    private void Table1_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if(Table1.SelectedItem is Info_comp selectedItem)
        {
            var ii = selectedItem.ComputerName;
        }
    }
}