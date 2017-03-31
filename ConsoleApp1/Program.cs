using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static public bool[][][] s_box1;
        static public bool[][][] s_box2;
        static public bool[] k1;
        static public bool[] k2;
        static void Main(string[] args)
        {
            initialisationsand();
            string chose = Console.ReadLine();

            string saisie = Console.ReadLine();
            SDES test = new SDES(saisie);
            
            //#### Clé de cryptage ####
            k1 = P8(decoupedecale(P10(test.master_key), 1));
            k2 = P8(decoupedecale(P10(test.master_key), 3));
            string saisie2 = Console.ReadLine();
            string textbin = ToBinary(ConvertToByteArray(saisie2, Encoding.ASCII));
            bool[][] matricebool = matricetextbin(textbin);


            for (int i=0;i<saisie2.Length;i++)
            {
                System.Console.WriteLine("Resultat : ");
                /* byte[] arr1 = Array.ConvertAll(crypte(matricebool[i]), b => b ? (byte)1 : (byte)0);
                 string cArray = Encoding.ASCII.GetString(arr1);*/
                bool[] tete;
                if (chose == "1")
                {
                     tete = crypte(matricebool[i]);
                }
                else
                {
                    tete = decrypte(matricebool[i]);
                }
                
                for (int j = 0; j < 8; j++)
                { 
                    System.Console.Write(" {0} ", tete[j]);
                }
                System.Console.WriteLine(".");
            }

            
            Console.ReadLine();
        }
        static public bool[] crypte(bool[] input)
        {
           return IPmoin1(fk(sw(fk(IP(input),k1)),k2));

        }

        static public bool[] decrypte(bool[] input)
        {
            return IPmoin1(fk(sw(fk(IP(input), k2)), k1));

        }

        static public bool[] f(bool[] right, bool[] sk)
        {
           return P4(sandboxe(leOuExclusive(EP(right),sk)));
        }
        static public bool[] fk(bool[] bits, bool[] key)
        {
            decoupe(bits ,out bool[] left, out bool[] right);

            bool[] k = f(right, key);
            leOuExclusive(left, k);

            bool[] a = new bool[8];
            int i = 0;
            foreach (bool t in left)
            {
                a[i] = t;
                i++;
            }
            foreach (bool t in right)
            {
                a[i] = t;
                i++;
            }

            return a;
        }

        static public bool[] sw(bool[] key)
        {

            bool[] permutatedArray = new bool[8];

            permutatedArray[0] = key[4];
            permutatedArray[1] = key[5];
            permutatedArray[2] = key[6];
            permutatedArray[3] = key[7];
            permutatedArray[4] = key[0];
            permutatedArray[5] = key[1];
            permutatedArray[6] = key[2];
            permutatedArray[7] = key[3];

            return permutatedArray;
        }
        private static void initialisationsand()
        {
            s_box1 = new bool[4][][];
            s_box2 = new bool[4][][];
            for (int i = 0; i < 4; i++)
            {
                s_box1[i] = new bool[4][];
                s_box2[i] = new bool[4][];
                for (int j = 0; j < 4; j++)
                {
                    s_box1[i][j] = new bool[2];
                    s_box2[i][j] = new bool[2];
                }
            }
            sandbox();
        }



        public static bool[] leOuExclusive(bool[] a,bool[] b)
        {

            bool[] c = new bool[a.Length];
            for(int i = 0; i< a.Length; i++)
            {
                if(a[i]== b[i])
                {
                    c[i] = false;
                 
                }
                else
                {
                    c[i] = true;
                }
            }

            return c;
        }

        public static byte[] ConvertToByteArray(string str, Encoding encoding)
        {
            return encoding.GetBytes(str);
        }
        
        public static String ToBinary(Byte[] data) //function convert binaire to string ex: 1 to "1"
        {
            return string.Join("", data.Select(byt => Convert.ToString(byt, 2).PadLeft(8, '0')));
        }


        public static void sandbox()
        {
            bool[] b0 = new bool[2];
            b0[0] = false;
            b0[1] = false;

            bool[] b1 = new bool[2];
            b1[0] = false;
            b1[1] = true;

            bool[] b2 = new bool[2];
            b2[0] = true;
            b2[1] = false;

            bool[] b3 = new bool[2];
            b3[0] = true;
            b3[1] = true;

            s_box1[0][0] = b1;
            s_box1[0][1] = b0;
            s_box1[0][2] = b3;
            s_box1[0][3] = b2;

            s_box1[1][0] = b3;
            s_box1[1][1] = b2;
            s_box1[1][2] = b1;
            s_box1[1][3] = b0;

            s_box1[2][0] = b0;
            s_box1[2][1] = b2;
            s_box1[2][2] = b1;
            s_box1[2][3] = b3;

            s_box1[3][0] = b3;
            s_box1[3][1] = b1;
            s_box1[3][2] = b3;
            s_box1[3][3] = b2;
            // ---------------------
            s_box2[0][0] = b0;
            s_box2[0][1] = b1;
            s_box2[0][2] = b2;
            s_box2[0][3] = b3;

            s_box2[1][0] = b2;
            s_box2[1][1] = b0;
            s_box2[1][2] = b1;
            s_box2[1][3] = b3;

            s_box2[2][0] = b3;
            s_box2[2][1] = b0;
            s_box2[2][2] = b1;
            s_box2[2][3] = b0;

            s_box2[3][0] = b2;
            s_box2[3][1] = b1;
            s_box2[3][2] = b0;
            s_box2[3][3] = b3;
        }

        public static bool[] sandboxe(bool[] a)
        {
            bool[] resultat = new bool[4];
            bool[] b;
            bool[] c;
            decoupe(a,out b, out c);

            resultat[0] = s_box1[booltoint(b[0], b[3])][booltoint(b[1], b[2])][0];
            resultat[1] = s_box1[booltoint(b[0], b[3])][booltoint(b[1], b[2])][1];
            resultat[2] = s_box2[booltoint(c[0], c[3])][booltoint(c[1], c[2])][0];
            resultat[3] = s_box2[booltoint(c[0], c[3])][booltoint(c[1], c[2])][1];


            return resultat;
        }

        static public int booltoint(bool a, bool b)
        {
            int i = -1;
            if(a == false && b == false)
            {
                i = 0;
            }
            else if (a == false && b == true)
            {
                i = 1;
            }
            else if (a == true && b == false)
            {
                i = 2;
            }
            else if (a == true && b == true)
            {
                i = 3;
            }
            return i;

        }
        public static bool[][] matricetextbin(string test) // creation de matrice bool a partir d'un string
        {
            bool[][] mat = new bool[test.Length/8][];
            for (int i = 0; i < test.Length/8 ; i++)
            {
                mat[i] = new bool[8];
            }
            bool[] chars = stringtobool(test);
            for (int i=0;i<test.Length/8; i++)
            {
                for(int j=0;j<8;j++ )
                {
                    mat[i][j] = chars[(i * 8) + j];
                }
            }
            return mat;
        }

        //generates  permated array P10
        static bool[] P10(bool[] key)
         {
            //0 1 2 3 4 5 6 7 8 9
            //2 4 1 6 3 9 0 8 7 5
            bool[] permutatedArray = new bool[10];
  
             permutatedArray[0] = key[2];
             permutatedArray[1] = key[4];
             permutatedArray[2] = key[1];
             permutatedArray[3] = key[6];
             permutatedArray[4] = key[3];
             permutatedArray[5] = key[9];
             permutatedArray[6] = key[0];
             permutatedArray[7] = key[8];
             permutatedArray[8] = key[7];
             permutatedArray[9] = key[5];
 
              return permutatedArray;
        }

        static bool[] P4(bool[] key)
        {
            //0 1 2 3 4 5 6 7 8 9
            //2 4 1 6 3 9 0 8 7 5
            bool[] permutatedArray = new bool[4];

            permutatedArray[0] = key[1];
            permutatedArray[1] = key[3];
            permutatedArray[2] = key[2];
            permutatedArray[3] = key[0];

            return permutatedArray;
        }

        static bool[] EP(bool[] key)
        {
            bool[] permutatedArray = new bool[8];

            permutatedArray[0] = key[3];
            permutatedArray[1] = key[0];
            permutatedArray[2] = key[1];
            permutatedArray[3] = key[2];
            permutatedArray[4] = key[1];
            permutatedArray[5] = key[2];
            permutatedArray[6] = key[3];
            permutatedArray[7] = key[0];

            return permutatedArray;
        }
        static bool[] IP(bool[] key)
        {
            //0 1 2 3 4 5 6 7 8 9
            //2 4 1 6 3 9 0 8 7 5
            bool[] permutatedArray = new bool[8];

            permutatedArray[0] = key[1];
            permutatedArray[1] = key[5];
            permutatedArray[2] = key[2];
            permutatedArray[3] = key[0];
            permutatedArray[4] = key[3];
            permutatedArray[5] = key[7];
            permutatedArray[6] = key[4];
            permutatedArray[7] = key[6];

            return permutatedArray;
        }
        static bool[] IPmoin1(bool[] key)
        {
            //0 1 2 3 4 5 6 7 8 9
            //2 4 1 6 3 9 0 8 7 5
            bool[] permutatedArray = new bool[8];

            permutatedArray[0] = key[3];
            permutatedArray[1] = key[0];
            permutatedArray[2] = key[2];
            permutatedArray[3] = key[4];
            permutatedArray[4] = key[6];
            permutatedArray[5] = key[1];
            permutatedArray[6] = key[7];
            permutatedArray[7] = key[5];

            return permutatedArray;
        }
        static bool[] P8(bool[] key)
        {
            //0 1 2 3 4 5 6 7 8 9
            //2 4 1 6 3 9 0 8 7 5
            bool[] permutatedArray = new bool[8];

            permutatedArray[0] = key[5];
            permutatedArray[1] = key[2];
            permutatedArray[2] = key[6];
            permutatedArray[3] = key[3];
            permutatedArray[4] = key[7];
            permutatedArray[5] = key[4];
            permutatedArray[6] = key[9];
            permutatedArray[7] = key[8];


            return permutatedArray;
        }
        public static bool[] decoupedecale(bool[] a, int bitNumber) 
        {
            bool[] b, c;
            decoupe(a, out b, out c);
            b = circularLeftShift(b, bitNumber);
            c = circularLeftShift(c, bitNumber);
            int i = 0;
            foreach (bool t in b)
            {
                a[i] = t;
                i++;
            }
            foreach (bool t in c)
            {
                a[i] = t;
                i++;
            }

            return a;
        }

        private static void decoupe(bool[] a, out bool[] b, out bool[] c) // decoupage d'un tableau en 2 bool
        {
            int  i = 0;
            int j = 0;
            b = new bool[a.Length / 2];
            c = new bool[a.Length / 2];
            foreach (bool t in a)
            {
                if (i < a.Length / 2)
                {
                    b[j] = t;
                }
                else if (i == a.Length / 2)
                {
                    j = 0;
                    c[j] = t;
                }
                else
                {
                    c[j] = t;
                }
                j++;
                i++;
            }
        }

        public static  bool[] circularLeftShift(bool[] a, int bitNumber)
          {
            bool[] b = new bool[a.Length];
            int i = 0;
            foreach(bool t in a)
            {
                if (i < bitNumber)
                {
                    b[a.Length - bitNumber + i ] = t;
                }
                else
                {
                    b[i - bitNumber] = t;
                }
                i++;
            }

 
             return b;
          }
       static public bool[] stringtobool(String _key)
        {
            char[] chars = _key.ToCharArray();
            bool[] master_key = new bool[chars.Length];
            // code a inserer pour transformer une chaine du type
            var i = 0;

            foreach (var car in chars)
            {
                if (car == '0')
                {
                    master_key[i] = false;
                }
                if (car == '1')
                {
                    master_key[i] = true;
                }
                i++;
            }
            return master_key;
        }
    }

    public class SDES
    {
        public bool[] master_key;
        public SDES(String _key)
        {
            char[] chars = _key.ToCharArray();
            master_key = new bool[chars.Length];
            // code a inserer pour transformer une chaine du type
            var i = 0;
           
            


            foreach(var car in chars)
            {
                if(car == '0' )
                {
                    master_key[i] = false;
                }
                if (car == '1')
                {
                    master_key[i] = true;
                }
                i++;
            }
    }
}
}
