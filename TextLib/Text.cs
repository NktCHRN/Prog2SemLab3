using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextLib
{
    public class Text
    {
		private uint _size;
		private string[] _text;
		private uint _vowelsQuantity;
		public bool ErrorFlag						//свойство - статус последней операции
        {
			get;
			private set;
        }
		public uint VowelsQuantity					//свойство - кол-во гласных букв в тексте
		{
			get 
			{
				return _vowelsQuantity;
			}
			private set
			{
				_vowelsQuantity = value;
			}
		}

		public int this[int index]					//индексатор
        {
			get
            {
				if (index >= 0 && index < _size)	//проверяем, не введен ли индекс меньше нуля или >= размеру массива
				{
					ErrorFlag = false;
					return _text[index].Length;		//возвращаем кол-во символов в этой строке
				}
				else
				{
					ErrorFlag = true;						//index меньше нуля или больше, чем строк в тексте
					return 0;
				}
            }
        }
		public Text()
		{
			_text = null;                                    //присваиваем массиву строк null
			_size = 0;
			VowelsQuantity = 0;
			ErrorFlag = false;
		}
		public Text(string[] text, uint size)
		{                                                   //конструктор с параметром - массивом объектов-строк класса string
			VowelsQuantity = 0;
			if (text != null)
			{                                               //проверяем переданный массив строк на null
				this._size = size;
				this._text = new string[size];
				for (uint i = 0; i < size; i++)
                    for (int j = 0; j < text[i].Length; j++)
						if (IsVowel(text[i][j]))
							VowelsQuantity++;
				for (uint i = 0; i < size; i++)
					this._text[i] = text[i];    //копируем содержимое
			}
			else
			{
				this._text = null;
				this._size = 0;
			}
			ErrorFlag = false;
		}

		public Text(in Text copyFrom)
		{                           //конструктор копирования
			_size = copyFrom._size;
			VowelsQuantity = copyFrom.VowelsQuantity;
			if (_size > 0)
			{
				_text = new string[_size];
				for (uint i = 0; i < _size; i++)
					_text[i] = _text[i];
			}
			else
			{
				_text = null;
			}
			ErrorFlag = false;
		}

		private string[] CopyArray()                            //private метод копирования массива строк
		{
			string[] newArray = new string[_size];
			for (uint i = 0; i < _size; i++)                       //построчно копируем массив строк
				newArray[i] = _text[i];
			return newArray;
		}

		public void AddString(in string strToAdd)
		{
			if (_size > 0)                                           //проверяем размер существующего цикла
			{
				for (int i = 0; i < strToAdd.Length; i++)
					if (IsVowel(strToAdd[i]))
						VowelsQuantity++;
				string[] copy = CopyArray();                      //копируем массив строк
				_size++;
				_text = new string[_size];                          //создаем новый массив строк
				for (uint i = 0; i < _size - 1; i++)
					_text[i] = copy[i];                              //переносим туда поэлементно старые строки
				_text[_size - 1] = strToAdd;         //добавляем новую строку
			}
			else
			{
				_size++;
				for (int i = 0; i < strToAdd.Length; i++)
					if (IsVowel(strToAdd[i]))
						VowelsQuantity++;
				_text = new string[_size];                          //создаем новый массив строк
				_text[0] = strToAdd;                   //добавляем новую строку
			}
			ErrorFlag = false;
		}

		public void DelString(uint index)
		{
			if (index < _size)                                       //проверяем, не передан ли превыщающий размеры массива индекс
			{
				ErrorFlag = false;
				if (_size > 1)                                       //проверяем, не 1 ли - размер массива строк
				{
					for (int j = 0; j < _text[index].Length; j++)
						if (IsVowel(_text[index][j]))
							VowelsQuantity--;
					string temp;
					for (uint i = index; i < _size - 1; i++)         //переставляем строки, дабы удаляемая стала в конец
					{
						temp = _text[i];
						_text[i] = _text[i + 1];
						_text[i + 1] = temp;
					}
					_size--;
					string[] copy = CopyArray();                  //копируем массив размером на 1 элемент меньше
					_text = copy;                                    //присваиваем адрес нового в переменную text
				}
				else
				{
					_text = null;
					VowelsQuantity = 0;
					_size = 0;
				}
			}
			else
            {
				ErrorFlag = true;
            }
		}

		public void Clear()
		{
			if (_text != null)
				_text = null;
			VowelsQuantity = 0;
			_size = 0;
			ErrorFlag = false;
		}
		public string GetStringForPrint()
		{
			if (_text != null)
			{
				string stringForPrint = "";                                                 //начальная точка для каждой строки
				for (uint i = 0; i < _size; i++)
					stringForPrint += _text[i] + '\n';
				ErrorFlag = false;
				return stringForPrint;
			}
			else
			{
				string stringForPrint = "\r";                                     //создаем строку с одним элементом
				ErrorFlag = true;
				return stringForPrint;
			}
		}

		private bool IsVowel(char symbol)
        {
			string vowels = "aeiouаоуыэяюёиеєії";
            for (int i = 0; i < vowels.Length; i++)
				if (symbol == vowels[i])
					return true;
			return false;
        }
	}
}
