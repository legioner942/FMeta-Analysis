using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMeta_Analysis
{
    class Core
    {
        class Column
        {
            public string Name { get; set; }
            public string Nick { get; set; }
            public short colNumber { get; set; }
        }

        enum command
        {
            init,
            add,
            no
        }

        List<Column> listOfCol;
        command current, subcommand;
        string param;
        bool Init;
        
        public Core()
        {
            listOfCol = new List<Column>();
            current = command.no;
            Init = false;
            param = "";
        }
        
        private void add()
        {
            
        }

        private void init()
        {
            
        }

        void Execute()
        {
            string[] buf = param.Split(' ');
            switch(current)
            {
                case command.add:
                    if (buf.Length % 2 != 0) throw new Exception("Неверное число параметров!");
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
            param = "";
            
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
            buf.CopyTo(buf, 1);

            if (buf.Length > 1)
            {
                switch (buf[1])
                {
                    case "add": subcommand = command.add; break;
                    default: subcommand = command.no; break;
                }
                buf.CopyTo(buf, 1);
            }
            
            for (int i = 0; i < buf.Length; i++)
            {
                if (i % 2 == 1)
                {
                    Column bf = listOfCol.Find(
                      delegate (Column inp)
                      {
                          return inp.Name == buf[i] || inp.Nick == buf[i];
                      }
                    );
                    if (bf != null) throw new Exception("Неверная имя/сокращение столбца!");
                    param += bf.colNumber.ToString()+" ";
                }
                else param += buf[i] + " ";
            }

            Execute();
        }
        
    }
}
