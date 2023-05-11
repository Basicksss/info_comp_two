using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
        /*
            Имя компьютера - hostname
            Процессор - cat /proc/cpuinfo
            материнская плата - sudo dmesg | grep DMI:
            жесткий диск - lsblk
            видеокарта - lspci | grep -E "VGA|3D"
            озу - sudo dmidecode --type 17
            сетевая карта -  lspci | grep -i 'net'
            утилиты -
            
            
        */
        /*var domain_name = await Dns.GetHostEntryAsync(DNS_name.Text);
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
            var runner4 =
                client.RunCommand("scp ./hostname.txt basicks@192.168.56.128:~/test");
            var runner5 =
                client.RunCommand("scp ./cpu.txt basicks@192.168.56.128:~/test");
            var runner6 =
                client.RunCommand("scp ./motherboard.txt basicks@192.168.56.128:~/test");
            client.Disconnect();
            if (runner.ExitStatus != 0)
            {
                var messageBoxStandardWindow = MessageBox.Avalonia.MessageBoxManager
                    .GetMessageBoxStandardWindow("Ошибка!", runner.Error);
                messageBoxStandardWindow.Show();
            }
            else if (runner2.ExitStatus != 0)
            {
                var messageBoxStandardWindow = MessageBox.Avalonia.MessageBoxManager
                    .GetMessageBoxStandardWindow("Ошибка!", runner2.Error);
                messageBoxStandardWindow.Show();
            }
            else if (runner3.ExitStatus != 0)
            {
                var messageBoxStandardWindow = MessageBox.Avalonia.MessageBoxManager
                    .GetMessageBoxStandardWindow("Ошибка!", runner3.Error);
                messageBoxStandardWindow.Show();
            }
            else if (runner4.ExitStatus != 0)
            {
                var messageBoxStandardWindow = MessageBox.Avalonia.MessageBoxManager
                    .GetMessageBoxStandardWindow("Ошибка!", runner4.Error);
                messageBoxStandardWindow.Show();
            }
            else if (runner5.ExitStatus != 0)
            {
                var messageBoxStandardWindow = MessageBox.Avalonia.MessageBoxManager
                    .GetMessageBoxStandardWindow("Ошибка!", runner5.Error);
                messageBoxStandardWindow.Show();
            }
            else if (runner6.ExitStatus != 0)
            {
                var messageBoxStandardWindow = MessageBox.Avalonia.MessageBoxManager
                    .GetMessageBoxStandardWindow("Успешно!", runner6.Error);
                messageBoxStandardWindow.Show();
            }
        }*/
        
        List<Info_comp> ic = new List<Info_comp>();
        List<Info_comps> ic2 = new List<Info_comps>();
        string[] str =  File.ReadAllLines(@"C:\Users\sasha\OneDrive\Рабочий стол\info\cputest.txt");
        string[] str2 =  File.ReadAllLines(@"C:\Users\sasha\OneDrive\Рабочий стол\info\hostname.txt");
        string[] str3 =  File.ReadAllLines(@"C:\Users\sasha\OneDrive\Рабочий стол\info\mattest.txt");

        string hs="1", cpu ="1", motherboard="1";
        bool hsisemptu = true, cpuisemptu = true, motherboardisemptu = true;
        int count=0;
        
        for (int i = 0; i < str.Length; i++)
        {
            if (str[i].Contains("model name"))
            {
                string proc = str[i].Split(':')[1];
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
            string proc = str3[i].Split(':')[1];
            motherboard = proc;
            count++;
            ic2.Add(new Info_comps()
            {
                Discription = "Материнская плата",
                Value = motherboard
            });
            break;
        }
        
        /*if (hs != "1" && cpu !="1" && motherboard != "1")
        {

            for (int i = 1; i < count; i++)
            ic.Add(new Info_comp()
            {
                ComputerName = hs,
                ProccessorName = cpu,
                MotherboardName = motherboard
            });
            Table1.Items = ic;
            var messageBoxStandardWindow = MessageBox.Avalonia.MessageBoxManager
                .GetMessageBoxStandardWindow("Успешно!", "запрос выполнился!");
            messageBoxStandardWindow.Show();
        }
        /*string cn = "test";
        InfoComps.Add(new Info_comp()
        {
            ComputerName =  cn
        });
        Load();*/
        Table1.Items = ic2;
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

    private void Othcet_pdf_OnClick(object? sender, RoutedEventArgs e)
    {
        if (Table1.Items == null)
        {
            return;
        }   
        
        using (Document document = new Document())
        {
            BaseFont baseFont = BaseFont.CreateFont("C:/Windows/Fonts/arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.NORMAL);
             
            PdfWriter.GetInstance(document, new FileStream("C:/Users/sasha/Downloads/file.pdf", FileMode.Create));
            document.Open();
        
            PdfPTable pdfTable = new PdfPTable(Table1.Columns.Count);
            pdfTable.AddCell(new PdfPCell(new Phrase("Название компьютера", font)));
            pdfTable.AddCell(new PdfPCell(new Phrase("Процессор", font)));
            pdfTable.AddCell(new PdfPCell(new Phrase("Материнская плата", font)));
             
            foreach (Info_comp computerInfo in Table1.Items)
            {
                pdfTable.AddCell(new PdfPCell(new Phrase(computerInfo.ComputerName, font)));
                pdfTable.AddCell(new PdfPCell(new Phrase(computerInfo.ProccessorName, font)));
                pdfTable.AddCell(new PdfPCell(new Phrase(computerInfo.MotherboardName, font)));
            }
        
            document.Add(pdfTable);  
        }
    }

    private void Otchet_excel_OnClick(object? sender, RoutedEventArgs e)
    {
        if (Table1.Items == null)
        {
            return;
        }

        const string filename = "C:/Users/sasha/Downloads/file.xlsx";
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
            worksheet.Cell("A1").Value = "Hostname";
            worksheet.Cell("B1").Value = "Processor";
            worksheet.Cell("C1").Value = "Motherboard";
            row++;

            foreach (Info_comp computerInfo in Table1.Items)
            {
                worksheet.Cell("A" + row).Value = computerInfo.ComputerName;
                worksheet.Cell("B" + row).Value = computerInfo.ProccessorName;
                worksheet.Cell("C" + row).Value = computerInfo.MotherboardName;
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