using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMeta_Analysis
{
    class Core
    {

        /// <summary>
        /// Нафиг это дерьмецо с хранением индексов и прочего. 
        /// Неа, не нафиг. Алиасы как хранить?
        /// 
        /// TODO: Сохранение параметров
        /// TODO: Парс числа параметров для add 
        /// TODO: Проверка уникальности header при init
        /// 
        /// </summary>
        
        class Column
        {
            public string Name { get; set; }
            public string Nick { get; set; }
            public int colNumber { get; set; }
        }

        enum command
        {
            init,
            add,
            row,
            column,
            rollback,
            no
        }

        List<Column> listOfCol;
        List<string> paramLine;
        List<string> buffer;
        command current, subcommand;
        bool Init;
        
        public Core()
        {
            listOfCol = new List<Column>();
            paramLine = new List<string>();
            buffer = new List<string>();

            current = subcommand = command.no;
            Init = false;
            
        }
        
        private void add()
        {
            //Add - добавление в указанную позицию с указанием имени/ника и значения
            //Add row добавление перечисленных значений в точной последовательности (вставка строки)
            //Add col добавление столбца в указанную позицию
            //Все действия будут стирать предыдущие значения

            if (subcommand == command.row)
            {
                string[] par = new string[paramLine.Count - 1];
                paramLine.CopyTo(1, par, 0, paramLine.Count - 1);
                Globals.ThisAddIn.clearRow(int.Parse(paramLine[0]));
                Globals.ThisAddIn.setRow(par, int.Parse(paramLine[0]));
            }
            else if (subcommand == command.no)
            {
                for (int i = 1; i < paramLine.Count; i+=2)
                {
                    Column bf = listOfCol.Find(
                      delegate (Column inp)
                      {
                          return inp.Name == paramLine[i] || inp.Nick == paramLine[i];
                      }
                    );
                    if (bf == null) throw new Exception("Неверное имя/сокращение столбца!"); // не очень здорово, ибо остальные значения не добавятся
                    Globals.ThisAddIn.setCell(paramLine[i+1], bf.colNumber, int.Parse(paramLine[0]));
                }
            }
            else if (subcommand == command.column)
            {
                Globals.ThisAddIn.clearCol(int.Parse(paramLine[0]));
                
            }
            else
            {
                // Тут должно быть исключение
            }
        }

        private void init()
        {
            List<string> buf = new List<string>();
            int index = 1, arg = 0;
            if (Init) listOfCol.Clear();
            if (subcommand == command.no)
            {
                try
                {
                    arg = int.Parse(paramLine[0]);
                    buf = Globals.ThisAddIn.getRow(1, arg);
                    foreach (var item in buf)
                    {
                        listOfCol.Add(new Column() { colNumber = index, Name = item, Nick = "" });
                        index++;
                    }
                }
                catch(Exception)
                {
                    // TODO: скорее всего бросать исключение в "выше стояющую инстанцию"
                }
            } 
            else if (subcommand == command.add)
            {
                string[] buf2 = paramLine.ToArray();
                Globals.ThisAddIn.setRow(buf2, 1);
                foreach (var item in buf2)
                {
                    listOfCol.Add(new Column() { colNumber = index, Name = item, Nick = "" });
                    index++;
                }
            }
            Init = true;
        }

        void Execute()
        {
            switch(current)
            {
                case command.add:

                    if ((paramLine.Count-1) % 2 != 0 && 
                        subcommand == command.no) throw new Exception("Неверное число параметров!");

                    if (!Init) throw new Exception("Инициализация не была произведена! Выполните ее набрав init,\nлибо кликнув на статус");

                    add();
                    break;
                case command.init:
                    init();
                    break;
                default: return;
            }
        }     

        public void Compiler(string commandline)
        {
            string[] buf = commandline.Split(' ');
            int command_res = 0;
            paramLine.Clear();            
            
            switch (buf[0])
            {
                case "init": 
                    current = command.init;
                    break;
                case "add": 
                    current = command.add;
                    break;
                default: throw new Exception("Неверная команда!");
            }
            command_res++;

            if (buf.Length > 1) // Косяк может выползти. Нужно придумать, как это лучше обработать
            {
                switch (buf[1])
                {
                    case "add": subcommand = command.add; break;
                    case "row": subcommand = command.row; break;
                    case "col": subcommand = command.column; break;
                    default: subcommand = command.no; command_res--; break; //сделал тут
                }
                command_res++;
            }

            for (int i = command_res; i < buf.Length; i++)
            {
                paramLine.Add(buf[i]);
            }

            Execute();
        }
        
    }
}
