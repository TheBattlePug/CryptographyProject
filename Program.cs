using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CipherApplication
{
    internal class Program
    {
        static void Main(string[] args)
        {
            

            string fileToRead = "C:\\Users\\dandu\\OneDrive\\Documents\\toEncrypt.txt";
            string fileToWrite = "C:\\Users\\dandu\\OneDrive\\Documents\\EncryptedDoc.txt";

            //StreamWriter sw = new StreamWriter(fileToWrite);
            
            Encrypt(fileToRead, fileToWrite);
        }

        public static void Encrypt(string fileForReading, string fileForWriting, int sizeOfPad = 8, int largestShift = 8) 
        {
            string alphabetLower = "abcdefghijklmnopqrstuvwxyz";
            string alphabetUpper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            int[] oneTimePad = new int[sizeOfPad];

            Random random = new Random();
            for (int i = 0; i < sizeOfPad; i++) 
            {                
                oneTimePad[i] = random.Next(largestShift);
            }

            StreamReader sr = new StreamReader(fileForReading);
            StreamWriter sw = new StreamWriter(fileForWriting);

            sw.WriteLine(ArrayAsString(oneTimePad));

            string line = sr.ReadToEnd();
            int j = 0;

            string newLine = "";
            int lineLength = line.Length;

            for (int i = 0; i < lineLength; i++) 
            {
                if (alphabetLower.Contains(line[i]))
                {
                    int originalIndex = alphabetLower.IndexOf(line[i]);
                    int newIndex = originalIndex + oneTimePad[j];

                    if (newIndex > 25)
                    {
                        newIndex = newIndex - 26;
                    }

                    newLine += alphabetLower[newIndex];
                }
                else if (alphabetUpper.Contains(line[i]))
                {
                    int originalIndex = alphabetUpper.IndexOf(line[i]);
                    int newIndex = originalIndex + oneTimePad[j];

                    if (newIndex > 25)
                    {
                        newIndex = newIndex - 26;
                    }

                    newLine += alphabetUpper[newIndex];
                }
                else 
                {
                    newLine += line[i];
                }
                
                if (j == 7)
                {
                    j = 0;
                }
                j++;
            }

            Console.WriteLine(ArrayAsString(oneTimePad));
            sw.WriteLine(newLine);

            Console.ReadKey();

            sr.Close();
            sw.Close();
        }

        public static string ArrayAsString(int[] arr) 
        {
            string arrAsString = "";

            for (int i = 0; i < arr.Length; i++) 
            {
                arrAsString += arr[i].ToString();
            }

            return arrAsString;
        }
    }
}

