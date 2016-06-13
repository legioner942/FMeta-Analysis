using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;
using System.Security;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMeta_Analysis
{
    public class Column
    {
        public string Name { get; set; }
        public string Nick { get; set; }
        public int colNumber { get; set; }
    }

    public class Core
    {

        /// <summary>
        /// -Нафиг это дерьмецо с хранением индексов и прочего. 
        /// -Неа, не нафиг. Алиасы как хранить?
        /// 
        /// TODO: Доделать set и пока все. 
        /// 
        /// </summary>
        enum command
        {
            init,
            add,
            row,
            column,
            rollback,
            set,
            nick,
            headerColumn,
            load,
            save,
            delete,
            clear,
            no = -1
        }

        public static readonly List<string> reduction = new List<string>(new string[]{
            "init",
            "add",
            "row",
            "col",
            "roll",
            "set",
            "nick",
            "hcol",
            "load",
            "save",
            "del",
            "clear",
            "list"
        });

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

        private string colNumtoAlph(int colnum)
        {
            int im = colnum, dec = 0;
            string param = "";

            if (colnum >= 0 && colnum <= 26) return ((char)(colnum + 64)).ToString();
            else if (colnum % 26 == 0)
            {
                string buf = colNumtoAlph(colnum - 1);
                return buf.Remove(buf.Length - 1) + "Z";
            }
            do
            {
                dec = im % 26;
                im = im / 26;
                param += ((char)(dec + 64)).ToString();
            } while (im != 0);
            if (param.Length > 1) param = new string(param.Reverse().ToArray());
            return param;
        }

        private int AlphtoColNum(string alpha)
        {
            int result = 0;

            if (alpha.Length == 1)
            {
                return alpha[0] - 64;
            }
            else if (alpha[alpha.Length - 1] == 'Z')
            {
                string buf = alpha.Remove(alpha.Length - 1) + "Y";
                return AlphtoColNum(buf) + 1;
            }

            if (alpha.Length > 1) alpha = new string(alpha.Reverse().ToArray());

            for (int i = 0; i < alpha.Length; i++)
            {
                result += (int)Math.Pow(26, i) * (alpha[i] - 64);
            }

            return result;
        }

        private void set()
        {
            if (subcommand == command.nick)
            {
                if (paramLine.Count % 2 != 0) throw new Exception("Неверное число параметров!");
                for (int i = 0; i < paramLine.Count; i+=2)
                {
                    Column bf = listOfCol.Find(
                            delegate (Column inp)
                            {
                                return inp.Name == paramLine[i];
                            });
                    if (bf == null) throw new Exception("Столбца с именем"+paramLine[i]+" нет в документе!");
                    else
                    {
                        Column bf2 = null;
                        if (!paramLine[i + 1].Equals(" ")) bf2 = listOfCol.Find(
                            delegate (Column inp)
                            {
                                return inp.Nick == paramLine[i+1];
                            });
                        if (bf2 != null) throw new Exception("Сокращение "+paramLine[i+1]+" уже есть у "+bf2.Name);
                        else
                        {
                            listOfCol.Remove(bf);
                            bf.Nick = paramLine[i + 1];
                            listOfCol.Insert(bf.colNumber-1, bf);
                        }
                    }
                }  
            }
            else if (subcommand == command.headerColumn)
            {
                //Все путем, хватит морочить голову с участком. Ищем имя, на которое пытаемся заменить.
                //Очевидно, что на этапе инициализации программа не допускает дублирование, 
                //поэтому, все более или менее хорошо
                if (paramLine.Count % 2 != 0) throw new Exception("Неверное число параметров!");
                for (int i = 0; i < paramLine.Count; i += 2)
                {
                    Column bf = listOfCol.Find(
                            delegate (Column inp)
                            {
                                return inp.Name == paramLine[i];
                            });
                    if (bf == null) throw new Exception("Столбца с именем" + paramLine[i] + " нет в документе!");
                    else
                    {
                        Column bf2 = listOfCol.Find( 
                            delegate (Column inp)
                            {
                                return inp.Name == paramLine[i + 1];
                            });
                        if (bf2 != null) throw new Exception("столбец с именем " + paramLine[i + 1] + " уже есть у " + bf2.Name);
                        else
                        {
                            listOfCol.Remove(bf);
                            bf.Name = paramLine[i + 1];
                            listOfCol.Insert(bf.colNumber - 1, bf);
                            Globals.ThisAddIn.setCell(bf.Name, 1, bf.colNumber.ToString());
                        }
                    }
                }
            }
            else if (subcommand == command.no)
            {
                //нужно ли тут что - то дополнять?
                throw new Exception("Ошибка выполнения команды set: субкомманда не была распознана");
            }
            else
            {
                throw new Exception("Ошибка выполнения команды set: субкомманда не была распознана");
            }
        }

        private void add()
        {
            //Add - добавление в указанную позицию с указанием имени/ника и значения
            //Add row добавление перечисленных значений в точной последовательности (вставка строки)
            //Add col добавление столбца в указанную позицию
            //Все действия кроме именованной вставки строки будут очищать строку/столбец

            if (subcommand == command.row)
            {
                string[] par = new string[paramLine.Count - 1];
                paramLine.CopyTo(1, par, 0, paramLine.Count - 1);
                if (paramLine[0].Equals("new") || paramLine[0].Equals("n")) paramLine[0] =
                          (Globals.ThisAddIn.getRowCountWithData() + 1).ToString();
                else Globals.ThisAddIn.clearRow(int.Parse(paramLine[0]));
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
                    if (paramLine[0].Equals("new") || paramLine[0].Equals("n")) paramLine[0] =
                          (Globals.ThisAddIn.getRowCountWithData()+1).ToString();
                    Globals.ThisAddIn.setCell(paramLine[i+1], int.Parse(paramLine[0]), bf.colNumber.ToString());
                }
            }
            else if (subcommand == command.column)
            {
                Globals.ThisAddIn.clearCol(paramLine[0]);
                for (int i = 1; i < paramLine.Count; i++)
                {
                    Globals.ThisAddIn.setCell(paramLine[i], i+1, paramLine[0]);
                }
            }
            else
            {
                throw new Exception("Ошибка выполнения команды add: субкомманда не была распознана!");
            }
        }

        private void init()
        {
            List<string> buf = new List<string>();
            int index = 1;
            string arg;
            if (Init) listOfCol.Clear();
            if (subcommand == command.no)
            {
                try
                {
                    arg = paramLine[0].ToUpper();
                    buf = Globals.ThisAddIn.getRow(1, arg);
                    foreach (var item in buf)
                    {
                        Column bf = listOfCol.Find(
                            delegate (Column inp)
                            {
                                return inp.Name == item || inp.Nick == item;
                            }
                        );

                        if (bf == null) listOfCol.Add(new Column() { colNumber = index, Name = item, Nick = "" });
                        else throw new Exception("Повторяются имена столбцов!");

                        index++;
                        
                    }
                }
                catch(Exception e)
                {
                    throw e;
                }
            } 
            else if (subcommand == command.add)
            {
                try
                {
                    string[] buf2 = paramLine.ToArray();
                    Globals.ThisAddIn.clearRow(1);
                    foreach (var item in buf2)
                    {
                        Column bf = listOfCol.Find(
                                delegate (Column inp)
                                {
                                    return inp.Name == item || inp.Nick == item;
                                }
                            );

                        if (bf == null) listOfCol.Add(new Column() { colNumber = index, Name = item, Nick = "" });
                        else
                        {
                            listOfCol.Clear();
                            throw new Exception("Повторяются имена столбцов!");
                        }
                        index++;
                    }
                    Globals.ThisAddIn.setRow(buf2, 1);
                }
                catch(Exception e)
                {
                    throw e;
                }
            }
            Init = true;
            Globals.ThisAddIn.console.setStatus(Init);
        }

        private void delete()
        {
            if (subcommand == command.row)
            {
                int numrow = int.Parse(paramLine[0]);
                if (numrow <= 0) throw new Exception("Попытка удаления несуществующей строки!");
                if (numrow == 1)
                {
                    if (MessageShower.ShowWarning("Вы собираетесь удалить заголовок и очистить информацию о сокращениях. Продолжить?")
                     == System.Windows.Forms.DialogResult.Cancel)
                    {
                        return;
                    }
                }
                Globals.ThisAddIn.clearRow(numrow);
                listOfCol.Clear();
                Init = false;
                Globals.ThisAddIn.console.setStatus(Init);
            }
            else if (subcommand == command.column)
            {
                Globals.ThisAddIn.clearCol(paramLine[0], true);
                int param = AlphtoColNum(paramLine[0]);
                for (int i = param; i < listOfCol.Count; i++)
                {
                    listOfCol[i].colNumber--;
                }
                listOfCol.Remove(listOfCol[AlphtoColNum(paramLine[0])-1]);
            }
        }
         
        private void clear()
        {
            if (subcommand == command.row)
            {
                int numrow = int.Parse(paramLine[0]);
                if (numrow <= 0) throw new Exception("Попытка удаления несуществующей строки!");
                if (numrow==1)
                {
                    if (MessageShower.ShowWarning("Вы собираетесь очистить заголовок. Продолжить?")
                     == System.Windows.Forms.DialogResult.Cancel)
                    {
                        return;
                    }
                }
                Globals.ThisAddIn.clearRow(numrow);
            }
            else if (subcommand == command.column)
            {
                save_column_name_toFile("dump");
                Globals.ThisAddIn.clearCol(paramLine[0]);
                listOfCol.Clear();
                load_from_file("dump");
            }
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
                case command.set:
                    if (!Init) throw new Exception("Инициализация не была произведена! Выполните ее набрав init,\nлибо кликнув на статус");
                    else set();
                    break;

                case command.load:
                    if (paramLine.Count == 0) throw new Exception("Не указано имя файла");
                    load_from_file(paramLine[0]);
                    break;

                case command.save:
                    if (paramLine.Count == 0) throw new Exception("Не указано имя файла");
                    save_column_name_toFile(paramLine[0]);
                    break;

                case command.clear:
                    clear();
                    break;

                case command.delete:
                    delete();
                    break;
                
                default: return;
            }
        }

        private string RemoveSpaces(string txt)
        {
            while (txt.Contains("  "))
            {
                txt = txt.Replace("  ", " ");
            }
            return txt;            
        }

        public void Compiler(string commandline)
        {
            commandline = RemoveSpaces(commandline);
            string[] buf = commandline.Split(' ');
            int command_res = 1;
            paramLine.Clear();

            int ind = -1;
            ind = reduction.FindIndex(
                delegate (string inp)
                {
                    return buf[0] == inp;
                }
            );
            current = (command)ind;
            if (current == command.no) throw new Exception("Неверная команда!");

            ind = -1;
            ind = reduction.FindIndex(
                delegate (string inp)
                {
                    return buf[1] == inp;
                }
            );
            subcommand = (command)ind;
            if (subcommand != command.no) command_res++;

            for (int i = command_res; i < buf.Length; i++)
            {
                if (buf[i] == "nil") buf[i] = " ";
                paramLine.Add(buf[i]);
            }

            Execute();
        }

        public void save_column_name_toFile(string filename)
        {
            string path = FormState.Default.SaverFolder;
            bool dump = filename.CompareTo("dump") == 0, fileExist = false;
            JavaScriptSerializer ser = new JavaScriptSerializer();

            if (!File.Exists(path)) Directory.CreateDirectory(path);

            path = Path.Combine(path, filename+".json");

            if (listOfCol.Count == 0 && !dump) throw new Exception("Ошибка сохранения: Нечего сохранять");
            else if (dump) return;
            var out_serialize = ser.Serialize(listOfCol);

            if (File.Exists(path)&&!dump)
            {
                if (MessageShower.ShowWarning("Файл с таким именем уже существует. Перезаписать?")
                     == System.Windows.Forms.DialogResult.OK)
                {
                    fileExist = true;
                }
                else return;
            }
            if (dump||fileExist) File.Delete(path);
            File.WriteAllText(path, out_serialize);
        }
        
        public void load_from_file(string filename)
        {
            string path = FormState.Default.SaverFolder;
            MessageShower.Debug(path);
            path = Path.Combine(path, filename + ".json");
            if (!File.Exists(path)) throw new Exception("Файла с таким именем не существует!");
            string in_ser = File.ReadAllText(path);
            JavaScriptSerializer ser = new JavaScriptSerializer();

            if (listOfCol.Count != 0)
            {
                if (MessageShower.ShowWarning("Инициализация уже была проведена. Загрузка уничтожит изменения\nХотите продолжить?")
                     == System.Windows.Forms.DialogResult.Cancel)
                    return;
            }

            listOfCol = ser.Deserialize<List<Column>>(in_ser);
            Globals.ThisAddIn.clearRow(1);
            foreach (var item in listOfCol)
            {
                Globals.ThisAddIn.setCell(item.Name, 1, colNumtoAlph(item.colNumber));
            }
            Init = true;
            Globals.ThisAddIn.console.setStatus(Init);
        }
        
        public List<Column> getListCol()
        {
            return listOfCol;
        }
    }
}
