// - 9 Напишите программу, находящую в массиве значение, встречающееся чаще всего.
using System;
using System.Text;
using Algorithm;

/* 
класс для алгоритма
класс для других функций

сохранять в таком формате, чтобы оно могло считываться с файла

  */


public class Program
{
    public enum file_action { overwrite = 1, create_new }
    public enum Choice { yes = 1, no }
    public enum MainChoice { console_inp = 1, file_inp, random_inp, exit }

    //МЭЙН
    private static void Main(string[] args) {
        
        ShowGreeting();
        ShowTask();
        MainChoice UserChoice;
        Console.WriteLine("Выберите пункт меню:\r\n [1] - Консольный ввод\r\n [2] - Файловый ввод\r\n [3] - Ввод случайных значений\r\n [4] - Завершение работы\r\n");

        UserChoice = (MainChoice)GetUserChoiceForMainMenu();
        bool saves;
        string save = "";
        while (UserChoice != MainChoice.exit)
        {            
            switch (UserChoice)
            {
                case MainChoice.console_inp:
                    Console.WriteLine("Введите размер массива");       
                    int size_list = GetIntMoreThanNull();
                    List<int> numbers = new List<int>();

                    Console.WriteLine("Введите элементы массива :");    
                    for (int i = 0; i < size_list; i++)
                    {
                        int value;
                        value = GetInt();
                        numbers.Add(value);
                    }
                    saves = saveYN();
                    if (saves == true)              //сохранить исходные данные в файл? - сохранить массив
                    {
                        save = SaveFile();
                        savefiledata(save, numbers);
                    }
                    MostCommonElement(numbers);
                    // сохранить полученные результаты в файл
                    saves = saveYN();
                    if (saves == true)
                    {
                        save = SaveFile();
                        savefileresult(save, numbers);
                    }
                    break;

          
                
                case MainChoice.file_inp:
                    break;

                case MainChoice.random_inp:
                    //Console.WriteLine("Введите размер массива");
                    //int size_array = GetIntMoreThanNull();
                    break;

                case MainChoice.exit:
                    break;


            }

            Console.WriteLine("\r\nВыберите пункт меню:\r\n [1] - Консольный ввод\r\n [2] - Файловый ввод\r\n [3] - Ввод случайных значений\r\n [4] - Завершение работы\r\n");
            UserChoice = (MainChoice)GetUserChoiceForMainMenu();






        }



        



            

    }

    //Функции красавицы:
    static void ShowGreeting()
    {
        Console.WriteLine("Здравствуйте, коллеги! ");
        Console.WriteLine("Контрольная работа №2, 9 вариант ");
        Console.WriteLine("Студент 415 А группы Лекомцев Руслан Олегович\r\n");
    }
    static void ShowTask()
    {
        Console.WriteLine("Напишите программу, находящую в массиве значение, встречающееся чаще всего.\r\n");
    }
    static int GetInt()
    {
        int UserInput;
        while (!int.TryParse(Console.ReadLine(), out UserInput))
        {
            Console.WriteLine("Ошибка ввода! Введите число");
        }
        return UserInput;
    }
    static int GetIntMoreThanNull()
    {
        int UserInput = GetInt();
        while (true)
        {
            if (UserInput > 1 || UserInput == 1)
            {
                return UserInput;
            }
            else
            {
                Console.WriteLine("Введите целое число больше 0  ");
                UserInput = GetInt();
            }
        }

    }
    static int GetUserChoice() //для диалога с пользователем 
    {
        Choice UserChoice = (Choice)GetInt();
        while (true)
        {
            if (UserChoice == Choice.yes || UserChoice == Choice.no)
            {
                return (int)UserChoice;
            }
            else
            {
                Console.WriteLine("Такого пункта меню не существует. Введите 1 или 2 ");
                UserChoice = (Choice)GetInt();
            }
        }

    }
    static int GetUserChoiceForMainMenu() //выбирать 1,2,3,4 меню основное
    {
        MainChoice UserChoice = (MainChoice)GetInt();
        while (true)
        {
            if (UserChoice == MainChoice.console_inp || UserChoice == MainChoice.file_inp || UserChoice == MainChoice.random_inp || UserChoice == MainChoice.exit) //енамы надо
            {
                return (int)UserChoice;
            }
            else
            {
                Console.WriteLine("Такого пункта меню не существует. Выберите существующий пункт ");
                UserChoice = (MainChoice)GetInt();
            }
        }

    }
    









    //Не менее красивые, но нужно доработать 
    static bool saveYN()
    {
        int UserChoice;
        Console.WriteLine("\nСохранить данные в файл?\n [1] Да \n [2] Нет ");
        UserChoice = GetUserChoice();
        while ((UserChoice != 1) && (UserChoice != 2))
        {
            UserChoice = GetUserChoice();
        }

        if (UserChoice == 1) { return true; }
        else { return false; }
    }
    static void savefiledata(string filename, List<int> data)
    {
        string datas = "";
        foreach (int item in data)
        {
            string str1 = item.ToString();
            datas = datas + str1 + ";";
        }
        byte[] info = new UTF8Encoding(true).GetBytes("Исходный массив: " + datas);
        using (FileStream fs = File.Create(filename))
        {
            fs.Write(info, 0, info.Length);
        }
    }
    static void savefileresult(string filename, List<int> numbers)
    {
        var most = numbers.GroupBy(x => x).OrderByDescending(x => x.Count()).First();
        string main = "";
        main = "Наиболее часто встречается " + most.Key + " в количестве " + most.Count();
        byte[] text = new UTF8Encoding(true).GetBytes(main);
        using (FileStream fs = File.Create(filename))
        {
            fs.Write(text, 0, text.Length);
        }
    }


    static void ReadFile(List<int> numbers, string x)
    {
        // Создать экземпляр типа StreamReader
        StreamReader fIn = new StreamReader(@x);
        int y;
        char c;
        string cc = "";
        // Цикл посимвольного чтения файла
        while (!fIn.EndOfStream) // проверка факта окончания файла
        {
            try
            {
                while ((c = (char)fIn.Read()) != ';')
                {
                    cc = cc + c;
                }
                y = Int32.Parse(cc);
                numbers.Add(y);
                cc = "";
            }
            catch (System.FormatException)
            {
                Console.WriteLine("Invalid values for an array of integer numbers!");
                break;
            }
            catch (Exception)
            {
                Console.WriteLine("Bad input!");
                break;
            }
        }
        fIn.Close();
    }
    static bool FileExist(string name)
    {
        string filename = @name;
        if (File.Exists(filename)) return true;
        else return false;
    }
    static bool FileWrit(string name)
    {
        string filename = @name;
        File.SetAttributes(filename, File.GetAttributes(filename) | FileAttributes.Normal); //read only replace
        using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.ReadWrite))
        {
            if (fs.CanWrite) return true;
            else return false;
        }
    }
    static string FileName()
    {
        string? path = "";
        path = Console.ReadLine();
        while ((path == "") || (path == null))
        {
            Console.WriteLine("Введите имя файла ");
            path = Console.ReadLine();
        }
        return path;
    }

    static string SaveFile()
    {
        int UserChoice;
        string filename = "";
        bool Exist, ReadOnly;
        Console.WriteLine("Введите имя файла: ");
        filename = FileName();
        Exist = FileExist(filename);
        if (Exist == true)
        {
            Console.WriteLine("Файл существует \n [1] Перезаписать файл \n [2] Создать новый файл ");
            UserChoice = GetUserChoice();
            while ((UserChoice != (int)file_action.overwrite) && (UserChoice != (int)file_action.create_new))
            {
                UserChoice = GetUserChoice();
            }
            if (UserChoice == 2)
            {
                do
                {
                    Console.WriteLine("Этот файл существует, введите другое имя ");
                    Console.WriteLine("Введите имя файла: ");
                    filename = FileName();
                    Exist = FileExist(filename);
                } while (Exist == true);
            }
            else
            {
                ReadOnly = FileWrit(filename);
                while (ReadOnly == false)
                {
                    Console.WriteLine("Этот файл только для чтения. Введите другое что-то . . . Enter a different file name");
                    Console.WriteLine("Введите имя файла: ");
                    filename = FileName();
                    ReadOnly = FileWrit(filename);
                }
            }
        }
        else
        {
            Console.WriteLine("Файл не существует. \n [1] Создать файл \n [2] Enter exists for overwriting");
            UserChoice = GetUserChoice();
            while ((UserChoice != 1) && (UserChoice != 2))
            {
                Console.WriteLine("\n [1] Создать файл \n [2] Enter exists for overwriting");
                UserChoice = GetUserChoice();
            }
            if (UserChoice == 2)
            {
                ReadOnly = FileWrit(filename);
                while (ReadOnly == false)
                {
                    Console.WriteLine("Этот файл только для чтения. Введите другое что-то .. . .Enter a different file name");
                    Console.WriteLine("Введите имя файла: ");
                    filename = FileName();
                    ReadOnly = FileWrit(filename);
                }
            }
        }
        return filename;
    }                 // есть косяк там исключение какое-то Enter exists for overwriting 
    
    



}








