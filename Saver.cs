using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Lab_5_2
{
    class Saver
    {
        private string path = Path.GetFullPath(@"MatrixOpen\");
        private string Fname = "matrRes" + ".txt";          //путь к файлу.
        ConsoleColor ResultColor;

        public string CreateFile()
        {
            Console.WriteLine("Создание файла\n");
            FileStream data = File.Create(path + Fname);
            data.Close();
            return "Файл создан";
        }
        public string OpenFile()
        {
            string result;
            try
            {
                result = "Файл открыт";
                File.Open(path + Fname, FileMode.Open);
            }
            catch (Exception)
            {
                result = "Файл не существует, не удалось открыть";
            };
            return result;
        }
        public string WriteAllText()
        {
          
              File.WriteAllText(path + Fname, "текст");
              return "додано у файл";         
        }
        public string AppendAllText()
        {
            File.AppendAllText(path + Fname, "тест метода AppendAllText ()"); //допишет в конец файлу
            return "додано у файл";

        }

        public string DeleteFile()
        {
            File.Delete(path+Fname);
            return "Файл удален успешно!";
        }

        public void ActionMenu(string text, ConsoleColor color)
        {
            Console.Clear();
            Console.ForegroundColor = color;
            Console.WriteLine(text + "\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Выберите действие\n1.Создать файл\n2. Открыть файл.\n3. Удалить файл.\n4. Додати у файл\n5. Выйти из программы");
            switch (Console.ReadLine())
            {
                case "1":
                    text = CreateFile();
                    ResultColor = ConsoleColor.Green;
                    break;
                case "2":
                    text = OpenFile();
                    ResultColor = ConsoleColor.Blue;
                    break;
                case "3":
                    text = DeleteFile();
                    ResultColor = ConsoleColor.Red;
                    break;
                case "4":
                    text = WriteAllText();
                    ResultColor = ConsoleColor.Red;
                    break;
                case "5":
                    return;
            }
            ActionMenu(text, ResultColor);
        }
     
    }
}