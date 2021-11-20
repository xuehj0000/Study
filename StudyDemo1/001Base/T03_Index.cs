using System;
using System.Collections.Generic;
using System.Text;

namespace StudyDemo1
{
    /// <summary>
    /// 索引器， 可以让 class 像数组一样访问class中的每个元素
    /// </summary>
    public class T03_Index
    {
        private string[] nameList = new string[10];
        public T03_Index()
        {
            for (int i = 0; i < nameList.Length; i++)
            {
                nameList[i] = "N/A";
            }
        }

        public string this[int index]
        {
            get
            {
                string tmp;
                if(index>=0 && index <= nameList.Length - 1)
                {
                    tmp = nameList[index];
                }
                else
                {
                    tmp = "";
                }
                return tmp;
            }
            set
            {
                string tmp;
                if (index >= 0 && index <= nameList.Length - 1)
                {
                    nameList[index] = value;
                }
            }
        }
    }
}
