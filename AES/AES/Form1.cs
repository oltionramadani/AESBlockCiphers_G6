using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AES
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {

            clearTextResults();

            if (txtKey.Text.Trim() == "" || txtMessage.Text.Trim() == "")
            {
                MessageBox.Show("Do not leave any field blank");
                return;
            }

            List<byte[,]> keys = generateKeys();
            List<char[]> roundMessage = splitMessage(txtMessage.Text.Trim());


            String encrypted = "";
            foreach (char[] chunk in roundMessage)
            {
                encrypted += encrypt(keys, chunk);
            }

            txtCiphertext.Text = encrypted;
            
        }


        private string encrypt(List<byte[,]> keys, char[] roundMessage)
        {
            
            //convert message to matrix of bytes
            int[,] initialMatrix = new int[4, 4];

            int xPos = 0, yPos = -1;
            for (int i = 0; i < 16; i++)
            {
               
                if (i % 4 == 0)
                {
                    yPos++;
                    xPos = 0;

                }

                initialMatrix[xPos, yPos] = (byte) roundMessage[i];

                xPos++;
            }

            /*
            initialMatrix = new int[4, 4] { { 0x32, 0x88, 0x31, 0xe0},
            { 0x43, 0x5a, 0x31, 0x37},
            { 0xf6, 0x30, 0x98, 0x07},
            { 0xa8, 0x8d, 0xa2, 0x34}};
            */

            //Print first round of matrix
            printMatrix("Input:", initialMatrix, 0, false);
            
            //fill other section matrix with blank lines
            printMatrix("Input:", initialMatrix, 1, true);
            printMatrix("Input:", initialMatrix, 2, true);
            printMatrix("Input:", initialMatrix, 3, true);

            //first round key
            int[,] finalMatrix = addRoundKey(keys[0], initialMatrix);

            int[,] subBytedMatrix, shiftedMatrix, mixedColumns;
            int[,] roundedMatrix = new int[4, 4];

            for (int i = 0; i < 9; i++)
            {
                printMatrix("Round: " + (i + 1) + " ", finalMatrix, 0, false);
                subBytedMatrix = getSubBytes(finalMatrix);

                printMatrix("Round: " + (i + 1) + " ", subBytedMatrix, 1, false);
                shiftedMatrix = getShiftRows(subBytedMatrix);

                printMatrix("Round: " + (i + 1) + " ", shiftedMatrix, 2, false);
                mixedColumns = getMixedColumns(shiftedMatrix);


                printMatrix("Round: " + (i + 1) + " ", mixedColumns, 3, false);
                roundedMatrix = addRoundKey(keys[i+1], mixedColumns);

                finalMatrix = roundedMatrix;

                int a = 0;
            }

            printMatrix("Round: 10 ", finalMatrix, 0, false);
            subBytedMatrix = getSubBytes(finalMatrix);


            printMatrix("Round: 10 ", subBytedMatrix, 1, false);
            shiftedMatrix = getShiftRows(subBytedMatrix);

            printMatrix("Round: 10 ", shiftedMatrix, 2, false);
            roundedMatrix = addRoundKey(keys[10], shiftedMatrix);


            printMatrix("Round: 10 ", finalMatrix, 3, true);

            printMatrix("Output: ", roundedMatrix, 0, false);

            string finalMessage = "";


            //rotate matrix
            //we dont know why
            //but it is a must ??

            finalMatrix = new int[4, 4];

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                    finalMatrix[i,j] = roundedMatrix[j,i];
            }


            for (int i=0;i<4;i++)
            {
                for (int j = 0; j < 4; j++)
                    finalMessage += (char) roundedMatrix[j,i];
            }   

            return finalMessage;
        }


        private List<byte[,]> generateKeys()
        {
            string key = txtKey.Text.Trim();


            //Add Padding
            // we take 'o' character as default padding
            for (int i = key.Length; i < 16; i++)
            {
                key += "o";
            }

            //Split string to characters
            char[] keyChars = key.ToCharArray();

            //prepare array to get byte equivalent of chars
            byte[] keyBytes = new byte[16];
            byte[] keyHex = new byte[16];

            string msg = "";
            //store characters as bytes
            for (int i = 0; i < 16; i++)
            {
                keyBytes[i] = (byte)keyChars[i];

                msg += keyBytes[i] + "\n";

            }

            /*
            keyBytes[0] = 0x2b;
            keyBytes[1] = 0x28;
            keyBytes[2] = 0xab;
            keyBytes[3] = 0x09;
            keyBytes[4] = 0x7e;
            keyBytes[5] = 0xae;
            keyBytes[6] = 0xf7;
            keyBytes[7] = 0xcf;
            keyBytes[8] = 0x15;
            keyBytes[9] = 0xd2;
            keyBytes[10] = 0x15;
            keyBytes[11] = 0x4f;
            keyBytes[12] = 0x16;
            keyBytes[13] = 0xa6;
            keyBytes[14] = 0x88;
            keyBytes[15] = 0x3c;
            */

            msg = "";
            int index = 0;

            //Prepare to save all keys here
            List<byte[,]> roundKeys = new List<byte[,]>();

            //split 128 bits to 4 sections with 4 bytes
            byte[,] splitKey = new byte[4,4];

            int xPos = 0, yPos = -1;
            for (int i = 0; i < keyBytes.Length; i++)
            {
                if (i % 4 == 0)
                {
                    yPos++;
                    xPos = 0;

                }

                splitKey[xPos, yPos] = keyBytes[i];

                xPos++;
            }

            roundKeys.Add(splitKey);

            printMatrix("Key 0:", splitKey, 4);

            byte[,] generatedKey = new byte[4,4];
            //Start generating 10 other keys
            for (int i = 1; i < 11; i++)
            {
                generatedKey = getKey(roundKeys[i - 1], i - 1);
                roundKeys.Add(generatedKey);

                printMatrix("Key " + i + ": ", generatedKey, 4);
            }

            return roundKeys;
        }

        private byte[,] getKey(byte[,] prevKey, int index)
        {
            //Rcon Table
            byte[] rconTable = { 1, 2, 4, 8, 16, 32, 64, 128, 27, 54};

            byte[,] retKey = new byte[4,4];

            byte[] newKey = new byte[4];

            byte[] w0 = new byte[4];
            byte[] w3 = new byte[4];

            for (int i = 0; i < 4; i++)
            {
                w0[i] = prevKey[i, 0];
                w3[i] = prevKey[i, 3];
            }
                
            
            w3 = shiftByte(w3);

            //translate w3 to values from sbox
            w3 = exchangeW3(w3);

            byte[] rcon = { rconTable[index], 0, 0, 0 };

            for (int k = 0; k < 4; k++)
            {
                retKey[k,0] = (byte)(w0[k] ^ w3[k] ^ rcon[k]);
            }
            

            // Generating W1, W2, W3
            for (int i = 1; i < 4; i++)
            {
                newKey = new byte[4];

                byte[] current = new byte[4];
                byte[] prev = new byte[4];

                for (int h = 0; h < 4; h++)
                {
                    prev[h] = prevKey[h, i];
                    current[h] = retKey[h, i-1];
                }

                //byte[] current = prevKey[i];
                //byte[] prev = retKey[i - 1];

                for (int j = 0; j < 4; j++)
                {
                    byte b = (byte)(current[j] ^ prev[j]);
                    retKey[j, i] = b;
                }
                int a = 5;
            }

            return retKey;
        }


        private int[,] getSubBytes(int[,] inMatrix)
        {

            string[] hexMs = new string[16];
            //return message to hex
            int index = 0;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    hexMs[index] = inMatrix[i, j].ToString("x2");
                    index++;
                }
            }

            int msLength = hexMs.Length;

            int[,] subBytedMatrix = new int[4, 4];

            int xPos = 0, yPos = -1;

            for (int i = 0; i < msLength; i++)
            {
                char x = hexMs[i][0];
                char y = hexMs[i][1];

                int val = getValueFromSbox(x, y);

                if(i%4 == 0)
                {
                    yPos++;
                    xPos = 0;
                   
                }

                subBytedMatrix[yPos, xPos] = val;

                xPos++;
            }

            //MessageBox.Show(subBytedMatrix.GetLength(0) + "");

            return subBytedMatrix;
        }


        private int[,] getShiftRows(int[,] matrix)
        {
     
            for(int i = 1; i < 4; i++)
            {

                int shifts = i;
                while (shifts > 0)
                {
                    int tmp = matrix[i,0];

                    for (int j = 0; j < 3; j++)
                    {
                        matrix[i,j] = matrix[i,j+1];
                    }

                    matrix[i,3] = tmp;


                    shifts--;
                }

            }

            return matrix;
        }
       

        private int[,] getMixedColumns(int [,] matrix)
        {
            /*
            matrix = new int[4, 4] {
                {0xd4, 0xe0, 0xb8, 0x1e },
                {0xbf, 0xb4, 0x41, 0x27 },
                {0x5d, 0x52, 0x11, 0x98 },
                {0x30, 0xae, 0xf1, 0xe5 }
            };
            */

            int[,] newMatrix = new int[4, 4];

            string msg = "";
            for(int a = 0; a < 4; a++)
            {
                for(int b = 0; b < 4; b++)
                {
                    msg += matrix[a, b] + " ";
                }
                msg += "\n";
            }

            int[,] rgfMatrix = getRGFMatrix();
            string resMsg = "";
            //Matrix multiply
            for(int i = 0; i < 4; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    resMsg = "";
                    int res = 0;
                    for(int k = 0; k < 4; k++)
                    {
                        int sendX = rgfMatrix[j, k];

                        int sendY = matrix[k, i];

                        /*
                         int sendX = rgfMatrix[i, k];

                        int sendY = matrix[k, j];
                         */

                        int x = multiplyBits(sendX, sendY);

                        resMsg += "X:" + sendX + " Y:" + sendY + " Res:" + x+"\n";
                        //MessageBox.Show(resMsg);
                        if (res == 0)
                            res = x;
                        else
                            res = res ^ x;
                    }
                    newMatrix[j,i] = res;
                }
            }

            return newMatrix;
        }

        private int[,] addRoundKey(byte[,] key, int[,] matrix)
        {
            /*
            key = new List<byte[]>();
            byte[] b = { 0xa0, 0x88, 0x23, 0x2a };
            key.Add(b);


            byte[] b2 = { 0xfa, 0x54, 0xa3, 0x6c };
            key.Add(b2);

            byte[] b3 = { 0xfe, 0x2c, 0x39, 0x76 };
            key.Add(b3);


            byte[] b1 = { 0x17, 0xb1, 0x39, 0x05 };
            key.Add(b1);
            */

            int[,] roundedMatrix = new int[4, 4];

            for(int i = 0; i < 4; i++)
            {
                for(int j = 0; j < 4; j++)
                {   
                    int[] xBits = intToBitArray(key[i,j]);

                    int[] yBits = intToBitArray(matrix[i,j]);

                    int[] res = xorBits(xBits, yBits);

                    roundedMatrix[i, j] = bitArrayToInt(res); 
                }
            }

            return roundedMatrix;
        }


        private byte[] exchangeW3(byte[] w3)
        {
            byte[] retW3 = new byte[4];

            for(int i = 0; i < 4; i++)
            {
                //return value to hex
                string val =  w3[i].ToString("x2");
                int intVal = getValueFromSbox(val[0], val[1]);
                retW3[i] = (byte) intVal;
            }

            return retW3;
        }

        private int multiplyBits(int x, int y)
        {
            if (x == 1)
                return y;
            else
            {
                BitArray b = new BitArray(new byte[] { (byte) y });
                int[] bits = b.Cast<bool>().Select(bit => bit ? 1 : 0).ToArray();

                int[] reverseBits = new int[8];

                //we reverse it because b.Cast funciton 
                //saves bits in reversed way
                for (int i = 0; i < 8; i++)
                    reverseBits[i] = bits[7-i];


                int shiftedVal = reverseBits[0];

                int[] shiftedReverseBits = new int[8];
                shiftedReverseBits = (shiftLeft(reverseBits, 1));

                int res = 0;
                //if left shifted value was 0
                //return that array of bits
                int[] xOredRes = new int[8];
                if (shiftedVal == 0)
                {
                    res = bitArrayToInt(shiftedReverseBits);
                }
                //otherwise xor that array with a constant array 
                //which is 1B
                else
                {
                    int[] defaultVectorXor = { 0, 0, 0, 1, 1, 0, 1, 1 };
                    xOredRes = xorBits(shiftedReverseBits,defaultVectorXor );

                    res = bitArrayToInt(xOredRes);
                }

                if (x == 3)
                {
                    int[] xOredRes3 = new int[8];
                    if (shiftedVal == 0) { 
                        xOredRes3 = xorBits(reverseBits, shiftedReverseBits);
                    }
                    else
                    {
                        xOredRes3 = xorBits(reverseBits, xOredRes);
                    }
                    res = bitArrayToInt(xOredRes3);
                   
                }

                return res;

            }
            
        }

        private int getValueFromSbox(char x, char y)
        {
            int xPos = getPosFromHex(x);
            int yPos = getPosFromHex(y);

            int[,] matrix = getSboxMatrix();



            return matrix[xPos, yPos];
        }

        private int getPosFromHex(char val)
        {
            char[] hexValues = {'0','1', '2', '3', '4', '5', '6', '7', '8',
                                '9', 'A', 'B', 'C', 'D', 'E', 'F'};

            for(int i =0;i<16; i++)
            {
                if (Char.ToUpper(val) == hexValues[i])
                    return i;
            }

            return -1;

        }

        private int[,] getSboxMatrix()
        {
            int[,] matrix = new int[16, 16]{
             { 0x63 ,0x7c ,0x77 ,0x7b ,0xf2 ,0x6b ,0x6f ,0xc5 ,0x30 ,0x01 ,0x67 ,0x2b ,0xfe ,0xd7 ,0xab ,0x76 },
             { 0xca ,0x82 ,0xc9 ,0x7d ,0xfa ,0x59 ,0x47 ,0xf0 ,0xad ,0xd4 ,0xa2 ,0xaf ,0x9c ,0xa4 ,0x72 ,0xc0},
             { 0xb7 ,0xfd ,0x93 ,0x26 ,0x36 ,0x3f ,0xf7 ,0xcc ,0x34 ,0xa5 ,0xe5 ,0xf1 ,0x71 ,0xd8 ,0x31 ,0x15},
             { 0x04 ,0xc7 ,0x23 ,0xc3 ,0x18 ,0x96 ,0x05 ,0x9a ,0x07 ,0x12 ,0x80 ,0xe2 ,0xeb ,0x27 ,0xb2 ,0x75},
             { 0x09 ,0x83 ,0x2c ,0x1a ,0x1b ,0x6e ,0x5a ,0xa0 ,0x52 ,0x3b ,0xd6 ,0xb3 ,0x29 ,0xe3 ,0x2f ,0x84},
             { 0x53 ,0xd1 ,0x00 ,0xed ,0x20 ,0xfc ,0xb1 ,0x5b ,0x6a ,0xcb ,0xbe ,0x39 ,0x4a ,0x4c ,0x58 ,0xcf},
             { 0xd0 ,0xef ,0xaa ,0xfb ,0x43 ,0x4d ,0x33 ,0x85 ,0x45 ,0xf9 ,0x02 ,0x7f ,0x50 ,0x3c ,0x9f ,0xa8},
             { 0x51 ,0xa3 ,0x40 ,0x8f ,0x92 ,0x9d ,0x38 ,0xf5 ,0xbc ,0xb6 ,0xda ,0x21 ,0x10 ,0xff ,0xf3 ,0xd2},
             { 0xcd ,0x0c ,0x13 ,0xec ,0x5f ,0x97 ,0x44 ,0x17 ,0xc4 ,0xa7 ,0x7e ,0x3d ,0x64 ,0x5d ,0x19 ,0x73},
             { 0x60 ,0x81 ,0x4f ,0xdc ,0x22 ,0x2a ,0x90 ,0x88 ,0x46 ,0xee ,0xb8 ,0x14 ,0xde ,0x5e ,0x0b ,0xdb},
             { 0xe0 ,0x32 ,0x3a ,0x0a ,0x49 ,0x06 ,0x24 ,0x5c ,0xc2 ,0xd3 ,0xac ,0x62 ,0x91 ,0x95 ,0xe4 ,0x79},
             { 0xe7 ,0xc8 ,0x37 ,0x6d ,0x8d ,0xd5 ,0x4e ,0xa9 ,0x6c ,0x56 ,0xf4 ,0xea ,0x65 ,0x7a ,0xae ,0x08},
             { 0xba ,0x78 ,0x25 ,0x2e ,0x1c ,0xa6 ,0xb4 ,0xc6 ,0xe8 ,0xdd ,0x74 ,0x1f ,0x4b ,0xbd ,0x8b ,0x8a},
             { 0x70 ,0x3e ,0xb5 ,0x66 ,0x48 ,0x03 ,0xf6 ,0x0e ,0x61 ,0x35 ,0x57 ,0xb9 ,0x86 ,0xc1 ,0x1d ,0x9e},
             { 0xe1 ,0xf8 ,0x98 ,0x11 ,0x69 ,0xd9 ,0x8e ,0x94 ,0x9b ,0x1e ,0x87 ,0xe9 ,0xce ,0x55 ,0x28 ,0xdf},
             { 0x8c ,0xa1 ,0x89 ,0x0d ,0xbf ,0xe6 ,0x42 ,0x68 ,0x41 ,0x99 ,0x2d ,0x0f ,0xb0 ,0x54 ,0xbb ,0x16}
            };

            return matrix;
        }

        private int[,] getRGFMatrix()
        {
            int[,] matrix = new int[4, 4]
            {
                {2, 3, 1, 1 },
                {1, 2, 3, 1 },
                {1, 1, 2, 3 },
                {3, 1, 1, 2 }
            };

            return matrix;
        }


        private List<char[]> splitMessage(string message)
        {
            int msLength = message.Length;
            if (msLength == 16)
            {
                return new List<char[]>() { message.ToCharArray() };
            }

            if (msLength < 16)
            {
                //Add Padding
                // we take 'o' character as default padding
                for (int i = msLength; i < 16; i++)
                {
                    message += "o";
                }

                return new List<char[]>() { message.ToCharArray() };
            }


            //Add Padding
            // we take 'o' character as default padding
           
            while(message.Length % 16 != 0)
            {
                message += "o";
            }

            List<char[]> retMessage = new List<char[]>();
            //split into strings of size 16

            char[] chunk = new char[16];


            int index = 0;
            for (int i = 0; i < msLength; i++)
            {
                if ((i % 16 == 0))
                { 
                    index = 0;
                    chunk = new char[16];

                    retMessage.Add(chunk);
                }

                chunk[index] = message[i];
                index++;
            }



            return retMessage;
        }


        private byte[] shiftByte(byte[] toShift)
        {
            byte[] ret = new byte[4];

            byte tmp = toShift[0];

            for(int i = 0; i<3; i++)
            {
                ret[i] = toShift[i + 1];
            }

            ret[3] = tmp;

            return ret;
        }



        private int[] shiftLeft(int[] toShift, int shifts)
        {
            int[] arr = new int[8];

            while (shifts > 0)
            {
                for (int i = 0; i < 7; i++)
                {
                    arr[i] = toShift[i + 1];
                }

                arr[7] = 0;

                
                shifts--;
            }

            return arr;
        }

        private int[] intToBitArray(int x)
        {
            BitArray b = new BitArray(new byte[] { (byte)x });
            int[] bits = b.Cast<bool>().Select(bit => bit ? 1 : 0).ToArray();

            int[] reverseBits = new int[8];

            //we reverse it because b.Cast funciton 
            //saves bits in reversed way
            for (int i = 0; i < 8; i++)
                reverseBits[i] = bits[7 - i];

            return reverseBits;
        }

        private int bitArrayToInt(int[] reverseBits)
        {
            double val = 0;
            for(int i = 0; i < 8; i++)
            {
                val += Math.Pow(2, 7-i) * reverseBits[i];
            }

            return (int) val;
        }

        private int[] xorBits(int[] x, int[] y)
        {
            int[] retArr = new int[8];

            for (int i = 0; i < 8; i++)
                retArr[i] = x[i] ^ y[i];

            return retArr;
        }


        private void printMatrix(string title, int[,] matrix, int display, bool blank)
        {
            string txt = "";

            if (blank)
            {
                txt = "\r\n\r\n\r\n\r\n\r\n\r\n";
            }
            else
            {

                txt = title + "\r\n\r\n";

                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        txt += matrix[i, j] + "  ";
                    }

                    txt += "\r\n";
                }
            }


            if (display == 0 && !blank)
                txtStartRoundResult.Text += txt + "\r\n";
            else if (display == 1)
                txtSubBytesResult.Text += txt + "\r\n";
            else if (display == 2)
                txtShiftRowsResult.Text += txt + "\r\n";
            else if (display == 3)
                txtMixColumnsResult.Text += txt + "\r\n";
            else if (display == 4)
                txtKeyResult.Text += txt + "\r\n";
        }


        private void printMatrix(string title, byte[,] matrix, int display)
        {
            string txt = title + "\r\n\r\n";

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    txt += matrix[i, j] + "  ";
                }

                txt += "\r\n";
            }


            if(display == 0)
                txtStartRoundResult.Text += txt + "\r\n";
            else if (display == 1)
                txtSubBytesResult.Text += txt + "\r\n";
            else if (display == 2)
                txtShiftRowsResult.Text += txt + "\r\n";
            else if (display == 3)
                txtMixColumnsResult.Text += txt + "\r\n";
            else if (display == 4)
                txtKeyResult.Text += txt + "\r\n";
        }

        private void clearTextResults()
        {
            txtStartRoundResult.Clear();
            txtSubBytesResult.Clear();
            txtShiftRowsResult.Clear();
            txtMixColumnsResult.Clear();
            txtKeyResult.Clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
