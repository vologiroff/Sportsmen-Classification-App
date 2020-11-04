using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMain : MonoBehaviour
{
    public InputField playerName;
    public InputField x1;
    public InputField x2;
    public InputField x3;
    public InputField x4;
    public InputField x5;
    public InputField x6;
    public InputField x7;
    public InputField x8;
    public InputField x9;
    public InputField x10;
    public InputField x11;
    public InputField x12;
    public Text outputWatchText;
    public Text outputValidation;
    public Text outputValidation1;
    public Text outputDatabaseNames;
    public Text outputDatabaseData;
    public GameObject testWindow;
    public GameObject validationWindow;
    public GameObject databaseWindow;
    public GameObject errorWindow;
    public GameObject errorWindow2;
    public GameObject errorPlayerNotFound;
    public InputField playerToDelete;
    public InputField playerToTransfer;
    public Text textShowWebView;
    public Text URL;
    public Text playerToValidate;


    private string path1;
    private string path2;
    private string tempPath1;
    private string tempPath2;
    private string[] class1;
    private string[] class2;
    private int[,] x_Class1;
    private int[,] x_Class2;
    private int[] sumX_Class1;
    private int[] sumX_Class2;
    private double[] a_hat_Class1;
    private double[] a_hat_Class2;
    private double[,] xMinesa_hat_Class1;
    private double[,] xMinesa_hat_Class2;
    private double[] xMinesa_hat_sum_Class1;
    private double[] xMinesa_hat_sum_Class2;
    private double[,] E_Class1;
    private double[,] E_Class2;
    private double[,] E_hat;
    private double[] a1_Mines_a2;
    private double[] half_sum_a1_a2;
    private Matrix preLastMatrixE;
    private int[,,] x_Class1_v;
    private int[,,] x_Class2_v;
    private int[,] sumX_Class1_v;
    private int[,] sumX_Class2_v;
    private double[,] a_hat_Class1_v;
    private double[,] a_hat_Class2_v;
    private double[,,] xMinesa_hat_Class1_v;
    private double[,,] xMinesa_hat_Class2_v;
    private double[,] xMinesa_hat_sum_Class1_v;
    private double[,] xMinesa_hat_sum_Class2_v;
    private double[,,] E_Class1_v;
    private double[,,] E_Class2_v;
    private double[,,] E_hat_v;
    private double[,] a1_Mines_a2_v;
    private double[,] half_sum_a1_a2_v;
    private Matrix preLastMatrixE_v;
    private int n1;
    private int n2;
    private int m = 6;
    private int[,,] testPlayers1;
    private int[,,] testPlayers2;
    private int[] x;
    private bool playerType;
    private bool classToShow;
    private bool testStartError;
    private int[] lastPlayerCharacteristics1;
    private int[] lastPlayerCharacteristics2;
    private int[] lastPlayerCharacteristicsCopy1;
    private int[] lastPlayerCharacteristicsCopy2;
    private int numOfValidation;

    // Start is called before the first frame update
    void Start()
    {
        //lastPlayerCharacteristics1 = new int[7];
        //lastPlayerCharacteristics2 = new int[7];
        numOfValidation = 1;
        Learn();
    }



    public void Learn()
    {

        path1 = Application.persistentDataPath + "/Class1.txt";
        path2 = Application.persistentDataPath + "/Class2.txt";
        Debug.Log(path1);

        if(!File.Exists(path1)) {

#if UNITY_EDITOR
            tempPath1 = Application.dataPath + "/StreamingAssets/Class1.txt";
            tempPath2 = Application.dataPath + "/StreamingAssets/Class2.txt";
#endif

#if UNITY_IPHONE
        tempPath1 = Application.dataPath + "/Raw/Class1.txt";
        tempPath2 = Application.dataPath + "/Raw/Class2.txt";
#endif

            string filePath1 = Path.Combine(Application.streamingAssetsPath, "Class1.txt");

            if (filePath1.Contains("://"))
            {
                WWW www = new WWW(filePath1);
                while (!www.isDone) { }
                File.WriteAllText(path1, www.text);
                class1 = www.text.Split('\n');
            }
            else
            {
                class1 = File.ReadAllLines(tempPath1);
                File.AppendAllLines(path1, class1);
                class1 = File.ReadAllLines(path1);
            }


            string filePath2 = Path.Combine(Application.streamingAssetsPath, "Class2.txt");

            if (filePath2.Contains("://"))
            {
                WWW www = new WWW(filePath2);
                while (!www.isDone) { }
                File.WriteAllText(path2, www.text);
                class2 = www.text.Split('\n');
            }
            else
            {
                class2 = File.ReadAllLines(tempPath2);
                File.AppendAllLines(path2, class2);
                class2 = File.ReadAllLines(path2);
            }

            foreach (string s in class1)
            {
                string[] tempClass1 = class1;

                File.WriteAllText(path1, "");

                int j = 0;
                foreach (string line in tempClass1)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        if (j == 0)
                        {
                            File.AppendAllText(path1, line);
                        }
                        else
                        {
                            File.AppendAllText(path1, "\n" + line);
                        }
                    }

                    j++;
                }

                class1 = File.ReadAllLines(path1);
            }

            foreach (string s in class2)
            {
                string[] tempClass2 = class2;

                File.WriteAllText(path2, "");

                int j = 0;
                foreach (string line in tempClass2)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        if (j == 0)
                        {
                            File.AppendAllText(path2, line);
                        }
                        else
                        {
                            File.AppendAllText(path2, "\n" + line);
                        }
                    }

                    j++;
                }

                class2 = File.ReadAllLines(path2);
            }

        } else
        {
            class1 = File.ReadAllLines(path1);
            class2 = File.ReadAllLines(path2);
        }


        //Debug.Log("KANYA: " + filePath1);
        //Debug.Log("KANYA: " + filePath2);
        //Debug.Log("KANYA: " + class1.Length);

        n1 = 0;
        foreach (string s in class1)
        {
            if (n1 == 0)
            {
                n1++;
                continue;
            }
            n1++;
        }

        n2 = 0;
        foreach (string s in class2)
        {
            if (n2 == 0)
            {
                n2++;
                continue;
            }
            n2++;
        }

        Debug.Log(n1);
        Debug.Log(n2);


        x_Class1 = new int[n1 + 1, m + 1];
        x_Class2 = new int[n2 + 1, m + 1];
        sumX_Class1 = new int[m + 1];
        sumX_Class2 = new int[m + 1];
        a_hat_Class1 = new double[m + 1];
        a_hat_Class2 = new double[m + 1];
        xMinesa_hat_Class1 = new double[n1 + 1, m + 1];
        xMinesa_hat_Class2 = new double[n2 + 1, m + 1];
        xMinesa_hat_sum_Class1 = new double[n1 + 1];
        xMinesa_hat_sum_Class2 = new double[n2 + 1];
        E_Class1 = new double[m + 1, m + 1];
        E_Class2 = new double[m + 1, m + 1];
        E_hat = new double[m + 1, m + 1];
        a1_Mines_a2 = new double[m + 1];
        half_sum_a1_a2 = new double[m + 1];
        preLastMatrixE = new Matrix(1, m);


        int i = 0;
        foreach (string s in class1)
        {
            if (i == 0)
            {
                i++;
                continue;
            }
            string str1 = s;
            string str2 = "";
            char[] mas = str1.ToCharArray();
            int j = 0;
            foreach (char h in mas)
            {
                if (char.IsDigit(h) == true)
                {
                    j++;
                    str2 += h;
                    if (j % 2 == 0)
                    {
                        x_Class1[i, j / 2] = Convert.ToInt32(str2);
                        str2 = "";
                    }
                }
            }

            i++;
        }

        /*for (int k = 1; k <= 6; k++)
        {
            lastPlayerCharacteristics1[k] = x_Class1[n1, k];
        }*/



        i = 0;
        foreach (string s in class2)
        {
            if (i == 0)
            {
                i++;
                continue;
            }
            string str1 = s;
            string str2 = "";
            char[] mas = str1.ToCharArray();
            int j = 0;
            foreach (char h in mas)
            {
                if (char.IsDigit(h) == true)
                {
                    j++;
                    str2 += h;
                    if (j % 2 == 0)
                    {
                        x_Class2[i, j / 2] = Convert.ToInt32(str2);
                        str2 = "";
                    }
                }
            }

            i++;
        }

        /*for (int k = 1; k <= 6; k++)
        {
            lastPlayerCharacteristics2[k] = x_Class2[n2, k];
        }*/

        Debug.Log("a_hat_Class1[k]");
        for (int k = 1; k <= 6; k++)
        {
            for (int j = 1; j <= n1-1; j++)
            {
                sumX_Class1[k] += x_Class1[j, k];
                //sumX_Class2[k] += x_Class2[j, k];
            }

            a_hat_Class1[k] = (double)sumX_Class1[k] / (double)(n1-1);
            Debug.Log(" " + a_hat_Class1[k]);
            //a_hat_Class2[k] = (double)sumX_Class2[k] / (double)n2;

            for (int j = 1; j <= n1-1; j++)
            {
                xMinesa_hat_Class1[j, k] = x_Class1[j, k] - a_hat_Class1[k];
                //xMinesa_hat_Class2[j, k] = x_Class2[j, k] - a_hat_Class2[k];
            }

        }

        Debug.Log("\na_hat_Class2[k]");
        for (int k = 1; k <= 6; k++)
        {
            for (int j = 1; j <= n2-1; j++)
            {
                //sumX_Class1[k] += x_Class1[j, k];
                sumX_Class2[k] += x_Class2[j, k];
            }

            //a_hat_Class1[k] = (double)sumX_Class1[k] / (double)n1;
            a_hat_Class2[k] = (double)sumX_Class2[k] / (double)(n2-1);
            Debug.Log(" " + a_hat_Class2[k]);
            for (int j = 1; j <= n2-1; j++)
            {
                //xMinesa_hat_Class1[j, k] = x_Class1[j, k] - a_hat_Class1[k];
                xMinesa_hat_Class2[j, k] = x_Class2[j, k] - a_hat_Class2[k];
            }

        }



        for (int q = 1; q <= 6; q++)
        {
            for (int w = 1; w <= n1-1; w++)
            {
                xMinesa_hat_sum_Class1[q] += xMinesa_hat_Class1[w, 1] * xMinesa_hat_Class1[w, q];
            }
        }

        for (int q = 7; q <= 11; q++)
        {
            for (int w = 1; w <= n1-1; w++)
            {
                xMinesa_hat_sum_Class1[q] += xMinesa_hat_Class1[w, 2] * xMinesa_hat_Class1[w, q - 5];
            }
        }

        for (int q = 12; q <= 15; q++)
        {
            for (int w = 1; w <= n1-1; w++)
            {
                xMinesa_hat_sum_Class1[q] += xMinesa_hat_Class1[w, 3] * xMinesa_hat_Class1[w, q - 9];
            }
        }

        for (int q = 16; q <= 18; q++)
        {
            for (int w = 1; w <= n1-1; w++)
            {
                xMinesa_hat_sum_Class1[q] += xMinesa_hat_Class1[w, 4] * xMinesa_hat_Class1[w, q - 12];
            }
        }

        for (int q = 19; q <= 20; q++)
        {
            for (int w = 1; w <= n1-1; w++)
            {
                xMinesa_hat_sum_Class1[q] += xMinesa_hat_Class1[w, 5] * xMinesa_hat_Class1[w, q - 14];
            }
        }

        for (int q = 21; q <= 21; q++)
        {
            for (int w = 1; w <= n1-1; w++)
            {
                xMinesa_hat_sum_Class1[q] += xMinesa_hat_Class1[w, 6] * xMinesa_hat_Class1[w, q - 15];
            }
        }



        for (int q = 1; q <= 6; q++)
        {
            for (int w = 1; w <= n2-1; w++)
            {
                xMinesa_hat_sum_Class2[q] += xMinesa_hat_Class2[w, 1] * xMinesa_hat_Class2[w, q];
            }
        }

        for (int q = 7; q <= 11; q++)
        {
            for (int w = 1; w <= n2-1; w++)
            {
                xMinesa_hat_sum_Class2[q] += xMinesa_hat_Class2[w, 2] * xMinesa_hat_Class2[w, q - 5];
            }
        }

        for (int q = 12; q <= 15; q++)
        {
            for (int w = 1; w <= n2-1; w++)
            {
                xMinesa_hat_sum_Class2[q] += xMinesa_hat_Class2[w, 3] * xMinesa_hat_Class2[w, q - 9];
            }
        }

        for (int q = 16; q <= 18; q++)
        {
            for (int w = 1; w <= n2-1; w++)
            {
                xMinesa_hat_sum_Class2[q] += xMinesa_hat_Class2[w, 4] * xMinesa_hat_Class2[w, q - 12];
            }
        }

        for (int q = 19; q <= 20; q++)
        {
            for (int w = 1; w <= n2-1; w++)
            {
                xMinesa_hat_sum_Class2[q] += xMinesa_hat_Class2[w, 5] * xMinesa_hat_Class2[w, q - 14];
            }
        }

        for (int q = 21; q <= 21; q++)
        {
            for (int w = 1; w <= n2-1; w++)
            {
                xMinesa_hat_sum_Class2[q] += xMinesa_hat_Class2[w, 6] * xMinesa_hat_Class2[w, q - 15];
            }
        }



        for (int j = 1; j <= 6; j++)
        {
            E_Class1[1, j] = xMinesa_hat_sum_Class1[j] / (double)(n1-1);
            E_Class2[1, j] = xMinesa_hat_sum_Class2[j] / (double)(n2-1);

            E_Class1[j, 1] = xMinesa_hat_sum_Class1[j] / (double)(n1-1);
            E_Class2[j, 1] = xMinesa_hat_sum_Class2[j] / (double)(n2-1);
        }

        for (int j = 7; j <= 11; j++)
        {
            E_Class1[2, j - 5] = xMinesa_hat_sum_Class1[j] / (double)(n1-1);
            E_Class2[2, j - 5] = xMinesa_hat_sum_Class2[j] / (double)(n2-1);

            E_Class1[j - 5, 2] = xMinesa_hat_sum_Class1[j] / (double)(n1-1);
            E_Class2[j - 5, 2] = xMinesa_hat_sum_Class2[j] / (double)(n2-1);
        }

        for (int j = 12; j <= 15; j++)
        {
            E_Class1[3, j - 9] = xMinesa_hat_sum_Class1[j] / (double)(n1-1);
            E_Class2[3, j - 9] = xMinesa_hat_sum_Class2[j] / (double)(n2-1);

            E_Class1[j - 9, 3] = xMinesa_hat_sum_Class1[j] / (double)(n1-1);
            E_Class2[j - 9, 3] = xMinesa_hat_sum_Class2[j] / (double)(n2-1);
        }

        for (int j = 16; j <= 18; j++)
        {
            E_Class1[4, j - 12] = xMinesa_hat_sum_Class1[j] / (double)(n1-1);
            E_Class2[4, j - 12] = xMinesa_hat_sum_Class2[j] / (double)(n2-1);

            E_Class1[j - 12, 4] = xMinesa_hat_sum_Class1[j] / (double)(n1-1);
            E_Class2[j - 12, 4] = xMinesa_hat_sum_Class2[j] / (double)(n2-1);
        }

        for (int j = 19; j <= 20; j++)
        {
            E_Class1[5, j - 14] = xMinesa_hat_sum_Class1[j] / (double)(n1-1);
            E_Class2[5, j - 14] = xMinesa_hat_sum_Class2[j] / (double)(n2-1);

            E_Class1[j - 14, 5] = xMinesa_hat_sum_Class1[j] / (double)(n1-1);
            E_Class2[j - 14, 5] = xMinesa_hat_sum_Class2[j] / (double)(n2-1);
        }

        for (int j = 21; j <= 21; j++)
        {
            E_Class1[6, j - 15] = xMinesa_hat_sum_Class1[j] / (double)(n1-1);
            E_Class2[6, j - 15] = xMinesa_hat_sum_Class2[j] / (double)(n2-1);

            E_Class1[j - 15, 6] = xMinesa_hat_sum_Class1[j] / (double)(n1-1);
            E_Class2[j - 15, 6] = xMinesa_hat_sum_Class2[j] / (double)(n2-1);
        }


        Debug.Log("\nE_hat");
        for (int k = 1; k <= 6; k++)
        {
            Debug.Log("\n");
            for(int j = 1; j <= 6; j++)
            {
                E_hat[k, j] = ((double)(n1-1) * E_Class1[k, j] + (double)(n2-1) * E_Class2[k, j]) / ((double)(n1-1) + (double)(n2-1) - 2);
                Debug.Log(E_hat[k, j] + " ");
            }
        }

        var testMatrix = new Matrix(6, 6);

        for (int k = 1; k <= 6; k++)
        {
            for (int j = 1; j <= 6; j++)
            {
                testMatrix[k-1, j-1] = E_hat[k, j];
            }
        }

        var inversibleMatrix = testMatrix.CreateInvertibleMatrix();

        Debug.Log("\n inversibleMatrix");
        for (int k = 1; k <= 6; k++)
        {
            Debug.Log("\n");
            for (int j = 1; j <= 6; j++)
            {
                Debug.Log(inversibleMatrix[k-1,j-1] + " ");
            }
        }


        var tempTranspose = new Matrix(1, 6);

        Debug.Log("\n a1_Mines_a2[j]");
        for (int j = 1; j <= 6; j++)
        {
            a1_Mines_a2[j] = a_hat_Class1[j] - a_hat_Class2[j];
            Debug.Log(a1_Mines_a2[j] + " ");
            tempTranspose[0, j-1] = a1_Mines_a2[j];
        }

        Debug.Log("\n half_sum_a1_a2[j]");
        for (int j = 1; j <= 6; j++)
        {
            half_sum_a1_a2[j] = (a_hat_Class1[j] + a_hat_Class2[j]) / (double)2;
            Debug.Log(half_sum_a1_a2[j] + " ");
        }

        var multipliedMatrixE = tempTranspose * inversibleMatrix;

        for (int j = 1; j <= 6; j++)
        {
            preLastMatrixE[0, j - 1] =  multipliedMatrixE[0, j - 1];
        }

        //Debug.Log(multipliedMatrixE[0,0] + "\n");

    }


    public void OnClickTest()
    {

        /*x1.text = "13";
        x2.text = "5";
        x3.text = "5";
        x4.text = "80";
        x5.text = "80";
        x6.text = "9";
        x7.text = "10";
        x8.text = "40";
        x9.text = "50";
        x10.text = "70";
        x11.text = "60";
        x12.text = "30";*/

        testStartError = false;

        if (playerName.text == "" || x1.text == "" || x2.text == "" || x3.text == "" || x4.text == "" || x5.text == "" || x6.text == "" ||
            x7.text == "" || x8.text == "" || x9.text == "" || x10.text == "" || x11.text == "" || x12.text == "")
        {
            errorWindow.gameObject.SetActive(true);
            testStartError = true;

        } else {

            errorWindow2.gameObject.SetActive(false);

            int i = 0;
            foreach (string s in class1)
            {
                if (i == 0)
                {
                    i++;
                    continue;
                }

                char[] mas = s.ToCharArray();
                string name = "";

                foreach (char h in mas)
                {
                    if (!char.IsDigit(h))
                    {
                        name += h;
                    }
                    else
                    {
                        break;
                    }
                }

                if (name.Trim().ToLower().Equals(playerName.text.Trim().ToLower()))
                {
                    errorWindow2.gameObject.SetActive(true);
                    testStartError = true;
                    break;
                }

                i++;
            }

            i = 0;
            foreach (string s in class2)
            {
                if (i == 0)
                {
                    i++;
                    continue;
                }

                char[] mas = s.ToCharArray();
                string name = "";

                foreach (char h in mas)
                {
                    if (!char.IsDigit(h))
                    {
                        name += h;
                    }
                    else
                    {
                        break;
                    }
                }

                if (name.Trim().ToLower().Equals(playerName.text.Trim().ToLower()))
                {
                    errorWindow2.gameObject.SetActive(true);
                    testStartError = true;
                    break;
                }

                i++;
            }

        }

        if(!testStartError)
        {
            errorWindow.gameObject.SetActive(false);
            playerToValidate.text = "Cross-validation for player " + playerName.text.Trim();

            x = new int[m + 1];
            x[1] = Convert.ToInt32(5 * (29 - Convert.ToDouble(this.x1.text)));
            x[2] = Convert.ToInt32(this.x2.text) * 5 + Convert.ToInt32(this.x3.text) * 5;
            x[3] = Convert.ToInt32(Convert.ToDouble(this.x4.text) * 0.75 + Convert.ToDouble(this.x5.text) * 0.25);
            x[4] = Convert.ToInt32((Convert.ToInt32(this.x6.text) * 9 + (100 - Convert.ToDouble(this.x7.text))) / 2);
            x[5] = Convert.ToInt32((Convert.ToDouble(this.x8.text) + Convert.ToDouble(this.x9.text) + Convert.ToDouble(this.x10.text)) / 3);
            x[6] = Convert.ToInt32((Convert.ToDouble(this.x11.text) + Convert.ToInt32(this.x12.text) * 2) / 2);
            

            for (int i = 1; i <= 6; i++)
            {
                if(x[i] < 1)
                {
                    x[i] = 1;
                } else if (x[i] > 99)
                {
                    x[i] = 99;
                }
            }

            /*x[1] = 90;
            x[2] = 81;
            x[3] = 73;
            x[4] = 82;
            x[5] = 44;
            x[6] = 71;*/

            var lastMatrix = new Matrix(m, 1);

            for (int j = 1; j <= 6; j++)
            {
                lastMatrix[j - 1, 0] = (x[j] - half_sum_a1_a2[j]);
            }

            var testMatrix = new Matrix(1, 1);

            testMatrix = preLastMatrixE * lastMatrix;

            testWindow.SetActive(true);                                         //Выводим окно результата

            
            if (testMatrix[0, 0] < 0)
            {
                outputWatchText.text = playerName.text + " is a defensive player";
                lastPlayerCharacteristics2 = new int[7];
                lastPlayerCharacteristics2[1] = x[1];
                lastPlayerCharacteristics2[2] = x[2];
                lastPlayerCharacteristics2[3] = x[3];
                lastPlayerCharacteristics2[4] = x[4];
                lastPlayerCharacteristics2[5] = x[5];
                lastPlayerCharacteristics2[6] = x[6];
                textShowWebView.text = "Click here to watch defenders tutorials";
                URL.text = "https://volkant0778.wixsite.com/mysite/defenders";
                playerType = false;
            }
            else
            {
                outputWatchText.text = playerName.text + " is an attacking player";
                lastPlayerCharacteristics1 = new int[7];
                lastPlayerCharacteristics1[1] = x[1];
                lastPlayerCharacteristics1[2] = x[2];
                lastPlayerCharacteristics1[3] = x[3];
                lastPlayerCharacteristics1[4] = x[4];
                lastPlayerCharacteristics1[5] = x[5];
                lastPlayerCharacteristics1[6] = x[6];
                textShowWebView.text = "Click here to watch attackers tutorials";
                URL.text = "https://volkant0778.wixsite.com/mysite";
                playerType = true;
            }

            for(int i = 1; i <= 6; i++)
            {
                Debug.Log("x[" + i + "]: " + x[i] + "\n");
            }
            Debug.Log("Результат вычислений для игрока " + playerName.text + ": " + testMatrix[0, 0]);
            
        }

    }


    public void OnClickBack()
    {
        SceneManager.LoadScene("StartScreen");
    }


    public void StartValidation()                                               /////////////*************VALIDATION*************////////////////////
    {
        int numRounds1 = (n1 - 1) / 8;
        int numRounds2 = (n2 - 1) / 8;

        Debug.Log(n1);
        Debug.Log(n2);

        int ost1 = 0;
        int ost2 = 0;

        int mod1 = (n1 - 1) % 8;
        int mod2 = (n2 - 1) % 8;

        if (mod1 != 0)
        {
            ost1 = 1;
        }

        if (mod2 != 0)
        {
            ost2 = 1;
        }

        testPlayers1 = new int[numRounds1 + 2, 8 + 1, m + 1];                   //список тестовых игроков (номер раунда, номер игрока, номер характеристики)
        testPlayers2 = new int[numRounds2 + 2, 8 + 1, m + 1];

        double[] percentage1 = new double[9];
        double[] percentage2 = new double[9];

        x_Class1_v = new int[numRounds1 + 2, n1 + 1, m + 1];                                //список игроков не из тестовой восьмерки
        sumX_Class1_v = new int[numRounds1 + 2, m + 1];
        a_hat_Class1_v = new double[numRounds1 + 2, m + 1];
        xMinesa_hat_Class1_v = new double[numRounds1 + 2, n1 + 1, m + 1];
        xMinesa_hat_sum_Class1_v = new double[numRounds1 + 2, n1 + 1];
        E_Class1_v = new double[numRounds1 + 2, m + 1, m + 1];

        x_Class2_v = new int[numRounds2 + 2, n2 + 1, m + 1];
        sumX_Class2_v = new int[numRounds2 + 2, m + 1];
        a_hat_Class2_v = new double[numRounds2 + 2, m + 1];
        xMinesa_hat_Class2_v = new double[numRounds2 + 2, n2 + 1, m + 1];
        xMinesa_hat_sum_Class2_v = new double[numRounds2 + 2, n2 + 1];
        E_Class2_v = new double[numRounds2 + 2, m + 1, m + 1];

        for (int l = 1; l <= numRounds1 + ost1; l++)                            //номер текущего раунда
        {
            int i = 0;                                                          //номер текущего игрока
            int numTemp = 1;                                                    //номер тестируемого игрока
            int numNewString = 1;                                               //номер текущей строки нетестовой восьмерки
            
            foreach (string s in class1)
            {
                if(i == 0)
                {
                    i++;
                    continue;
                }

                if (i > (l-1)*8 && i <= l*8)                                    //выделяем тестовую восьмерку для каждого из раундов
                {
                    for (int k = 1; k <= 6; k++)
                    {
                        testPlayers1[l, numTemp, k] = x_Class1[i, k];
                    }

                    i++;
                    numTemp++;
                    continue;
                }


                for(int k = 1; k <= 6; k++)                                     //выделяем список остальных игроков
                {
                    x_Class1_v[l, numNewString, k] = x_Class1[i, k];
                }

                i++;
                numNewString++;
            }


            for (int k = 1; k <= 6; k++)
            {
                for (int j = 1; j <= n1 - 1 - 8; j++)
                {
                    sumX_Class1_v[l, k] += x_Class1_v[l, j, k];
                }

                a_hat_Class1_v[l, k] = (double)sumX_Class1_v[l, k] / (double)(n1 - 1 - 8);

                for (int j = 1; j <= n1 - 1 - 8; j++)
                {
                    xMinesa_hat_Class1_v[l, j, k] = x_Class1_v[l, j, k] - a_hat_Class1_v[l, k];
                }

            }


            for (int q = 1; q <= 6; q++)
            {
                for (int w = 1; w <= n1 - 1 - 8; w++)
                {
                    xMinesa_hat_sum_Class1_v[l, q] += xMinesa_hat_Class1_v[l, w, 1] * xMinesa_hat_Class1_v[l, w, q];
                }
            }

            for (int q = 7; q <= 11; q++)
            {
                for (int w = 1; w <= n1 - 1 - 8; w++)
                {
                    xMinesa_hat_sum_Class1_v[l, q] += xMinesa_hat_Class1_v[l, w, 2] * xMinesa_hat_Class1_v[l, w, q - 5];
                }
            }

            for (int q = 12; q <= 15; q++)
            {
                for (int w = 1; w <= n1 - 1 - 8; w++)
                {
                    xMinesa_hat_sum_Class1_v[l, q] += xMinesa_hat_Class1_v[l, w, 3] * xMinesa_hat_Class1_v[l, w, q - 9];
                }
            }

            for (int q = 16; q <= 18; q++)
            {
                for (int w = 1; w <= n1 - 1 - 8; w++)
                {
                    xMinesa_hat_sum_Class1_v[l, q] += xMinesa_hat_Class1_v[l, w, 4] * xMinesa_hat_Class1_v[l, w, q - 12];
                }
            }

            for (int q = 19; q <= 20; q++)
            {
                for (int w = 1; w <= n1 - 1 - 8; w++)
                {
                    xMinesa_hat_sum_Class1_v[l, q] += xMinesa_hat_Class1_v[l, w, 5] * xMinesa_hat_Class1_v[l, w, q - 14];
                }
            }

            for (int q = 21; q <= 21; q++)
            {
                for (int w = 1; w <= n1 - 1 - 8; w++)
                {
                    xMinesa_hat_sum_Class1_v[l, q] += xMinesa_hat_Class1_v[l, w, 6] * xMinesa_hat_Class1_v[l, w, q - 15];
                }
            }


            for (int j = 1; j <= 6; j++)
            {
                E_Class1_v[l, 1, j] = xMinesa_hat_sum_Class1_v[l, j] / (double)(n1 - 1 - 8);

                E_Class1_v[l, j, 1] = xMinesa_hat_sum_Class1_v[l, j] / (double)(n1 - 1 - 8);
            }

            for (int j = 7; j <= 11; j++)
            {
                E_Class1_v[l, 2, j - 5] = xMinesa_hat_sum_Class1_v[l, j] / (double)(n1 - 1 - 8);

                E_Class1_v[l, j - 5, 2] = xMinesa_hat_sum_Class1_v[l, j] / (double)(n1 - 1 - 8);
            }

            for (int j = 12; j <= 15; j++)
            {
                E_Class1_v[l, 3, j - 9] = xMinesa_hat_sum_Class1_v[l, j] / (double)(n1 - 1 - 8);

                E_Class1_v[l, j - 9, 3] = xMinesa_hat_sum_Class1_v[l, j] / (double)(n1 - 1 - 8);
            }

            for (int j = 16; j <= 18; j++)
            {
                E_Class1_v[l, 4, j - 12] = xMinesa_hat_sum_Class1_v[l, j] / (double)(n1 - 1 - 8);

                E_Class1_v[l, j - 12, 4] = xMinesa_hat_sum_Class1_v[l, j] / (double)(n1 - 1 - 8);
            }

            for (int j = 19; j <= 20; j++)
            {
                E_Class1_v[l, 5, j - 14] = xMinesa_hat_sum_Class1_v[l, j] / (double)(n1 - 1 - 8);

                E_Class1_v[l, j - 14, 5] = xMinesa_hat_sum_Class1_v[l, j] / (double)(n1 - 1 - 8);
            }

            for (int j = 21; j <= 21; j++)
            {
                E_Class1_v[l, 6, j - 15] = xMinesa_hat_sum_Class1_v[l, j] / (double)(n1 - 1 - 8);

                E_Class1_v[l, j - 15, 6] = xMinesa_hat_sum_Class1_v[l, j] / (double)(n1 - 1 - 8);
            }

        }
        
        

        for (int l = 1; l <= numRounds2 + ost2; l++)
        {
            int i = 0;
            int numTemp = 1;
            int numNewString = 1;

            foreach (string s in class2)
            {
                if (i == 0)
                {
                    i++;
                    continue;
                }

                if (i > (l - 1) * 8 && i <= l * 8)
                {
                    for (int k = 1; k <= 6; k++)
                    {
                        testPlayers2[l, numTemp, k] = x_Class2[i, k];
                    }
                    i++;
                    numTemp++;
                    continue;
                }


                for (int k = 1; k <= 6; k++)
                {
                    x_Class2_v[l, numNewString, k] = x_Class2[i, k];
                }
                i++;
                numNewString++;
            }


            for (int k = 1; k <= 6; k++)
            {
                for (int j = 1; j <= n2 - 1 - 8; j++)
                {
                    sumX_Class2_v[l, k] += x_Class2_v[l, j, k];
                }

                a_hat_Class2_v[l, k] = (double)sumX_Class2_v[l, k] / (double)(n2 - 1 - 8);

                for (int j = 1; j <= n2 - 1 - 8; j++)
                {
                    xMinesa_hat_Class2_v[l, j, k] = x_Class2_v[l, j, k] - a_hat_Class2_v[l, k];
                }

            }


            for (int q = 1; q <= 6; q++)
            {
                for (int w = 1; w <= n2 - 1 - 8; w++)
                {
                    xMinesa_hat_sum_Class2_v[l, q] += xMinesa_hat_Class2_v[l, w, 1] * xMinesa_hat_Class2_v[l, w, q];
                }
            }

            for (int q = 7; q <= 11; q++)
            {
                for (int w = 1; w <= n2 - 1 - 8; w++)
                {
                    xMinesa_hat_sum_Class2_v[l, q] += xMinesa_hat_Class2_v[l, w, 2] * xMinesa_hat_Class2_v[l, w, q - 5];
                }
            }

            for (int q = 12; q <= 15; q++)
            {
                for (int w = 1; w <= n2 - 1 - 8; w++)
                {
                    xMinesa_hat_sum_Class2_v[l, q] += xMinesa_hat_Class2_v[l, w, 3] * xMinesa_hat_Class2_v[l, w, q - 9];
                }
            }

            for (int q = 16; q <= 18; q++)
            {
                for (int w = 1; w <= n2 - 1 - 8; w++)
                {
                    xMinesa_hat_sum_Class2_v[l, q] += xMinesa_hat_Class2_v[l, w, 4] * xMinesa_hat_Class2_v[l, w, q - 12];
                }
            }

            for (int q = 19; q <= 20; q++)
            {
                for (int w = 1; w <= n2 - 1 - 8; w++)
                {
                    xMinesa_hat_sum_Class2_v[l, q] += xMinesa_hat_Class2_v[l, w, 5] * xMinesa_hat_Class2_v[l, w, q - 14];
                }
            }

            for (int q = 21; q <= 21; q++)
            {
                for (int w = 1; w <= n2 - 1 - 8; w++)
                {
                    xMinesa_hat_sum_Class2_v[l, q] += xMinesa_hat_Class2_v[l, w, 6] * xMinesa_hat_Class2_v[l, w, q - 15];
                }
            }


            for (int j = 1; j <= 6; j++)
            {
                E_Class2_v[l, 1, j] = xMinesa_hat_sum_Class2_v[l, j] / (double)(n2 - 1 - 8);

                E_Class2_v[l, j, 1] = xMinesa_hat_sum_Class2_v[l, j] / (double)(n2 - 1 - 8);
            }

            for (int j = 7; j <= 11; j++)
            {
                E_Class2_v[l, 2, j - 5] = xMinesa_hat_sum_Class2_v[l, j] / (double)(n2 - 1 - 8);

                E_Class2_v[l, j - 5, 2] = xMinesa_hat_sum_Class2_v[l, j] / (double)(n2 - 1 - 8);
            }

            for (int j = 12; j <= 15; j++)
            {
                E_Class2_v[l, 3, j - 9] = xMinesa_hat_sum_Class2_v[l, j] / (double)(n2 - 1 - 8);

                E_Class2_v[l, j - 9, 3] = xMinesa_hat_sum_Class2_v[l, j] / (double)(n2 - 1 - 8);
            }

            for (int j = 16; j <= 18; j++)
            {
                E_Class2_v[l, 4, j - 12] = xMinesa_hat_sum_Class2_v[l, j] / (double)(n2 - 1 - 8);

                E_Class2_v[l, j - 12, 4] = xMinesa_hat_sum_Class2_v[l, j] / (double)(n2 - 1 - 8);
            }

            for (int j = 19; j <= 20; j++)
            {
                E_Class2_v[l, 5, j - 14] = xMinesa_hat_sum_Class2_v[l, j] / (double)(n2 - 1 - 8);

                E_Class2_v[l, j - 14, 5] = xMinesa_hat_sum_Class2_v[l, j] / (double)(n2 - 1 - 8);
            }

            for (int j = 21; j <= 21; j++)
            {
                E_Class2_v[l, 6, j - 15] = xMinesa_hat_sum_Class2_v[l, j] / (double)(n2 - 1 - 8);

                E_Class2_v[l, j - 15, 6] = xMinesa_hat_sum_Class2_v[l, j] / (double)(n2 - 1 - 8);
            }

        }


        for (int l = 1; l <= numRounds1 + ost1; l++)
        {
            E_hat_v = new double[numRounds1 + 2, m + 1, m + 1];
            a1_Mines_a2_v = new double[numRounds1 + 2, m + 1];
            half_sum_a1_a2_v = new double[numRounds1 + 2, m + 1];
            preLastMatrixE_v = new Matrix(1, m);

            for (int k = 1; k <= 6; k++)
            {
                for (int j = 1; j <= 6; j++)
                {
                    E_hat_v[l, k, j] = ((double)(n1 - 1 - 8) * E_Class1_v[l, k, j] + (double)(n2 - 1 - 8) * E_Class2_v[l, k, j]) / ((double)(n1 - 1 - 8) + (double)(n2 - 1 - 8) - 2);
                }
            }

            var testMatrix_v = new Matrix(6, 6);

            for (int k = 1; k <= 6; k++)
            {
                for (int j = 1; j <= 6; j++)
                {
                    testMatrix_v[k - 1, j - 1] = E_hat_v[l, k, j];
                    
                }
            }
            

            var inversibleMatrix_v = testMatrix_v.CreateInvertibleMatrix();

            var tempTranspose_v = new Matrix(1, 6);

            for (int j = 1; j <= 6; j++)
            {
                a1_Mines_a2_v[l, j] = a_hat_Class1_v[l, j] - a_hat_Class2_v[l, j];
                tempTranspose_v[0, j - 1] = a1_Mines_a2_v[l, j];
            }


            for (int j = 1; j <= 6; j++)
            {
                half_sum_a1_a2_v[l, j] = (a_hat_Class1_v[l, j] + a_hat_Class2_v[l, j]) / (double)2;
            }


            var multipliedMatrixE_v = tempTranspose_v * inversibleMatrix_v;

            for (int j = 1; j <= 6; j++)
            {
                preLastMatrixE_v[0, j - 1] = multipliedMatrixE_v[0, j - 1];
            }
        
            int successful1 = 0;                                                //количество игроков, успешно прошедших валидацию в текущем раунде
            int numPlayersInRound = 8;                                          //количество игроков в текущем раунде

            if(l == numRounds1 + ost1 && ost1 == 1)                             //если есть неполный раунд количество игроков в текущем раунде меняется соответственно
            {
                numPlayersInRound = mod1;
            }

            for (int n = 1; n <= numPlayersInRound; n++)                        
            {
                int[] x = new int[m + 1];                                       //характеристики тестовых игроков текущего раунда
                if(n == 1 && lastPlayerCharacteristics1 != null)
                {
                    Debug.Log("Ласт плеер в нап");
                    x[1] = lastPlayerCharacteristics1[1];
                    x[2] = lastPlayerCharacteristics1[2];
                    x[3] = lastPlayerCharacteristics1[3];
                    x[4] = lastPlayerCharacteristics1[4];
                    x[5] = lastPlayerCharacteristics1[5];
                    x[6] = lastPlayerCharacteristics1[6];
                } else
                {
                    x[1] = testPlayers1[l, n, 1];
                    x[2] = testPlayers1[l, n, 2];
                    x[3] = testPlayers1[l, n, 3];
                    x[4] = testPlayers1[l, n, 4];
                    x[5] = testPlayers1[l, n, 5];
                    x[6] = testPlayers1[l, n, 6];
                }

                var lastMatrix_v = new Matrix(m, 1);

                for (int j = 1; j <= 6; j++)
                {
                    lastMatrix_v[j - 1, 0] = (x[j] - half_sum_a1_a2_v[l, j]);
                }

                var testMatrix_vv = new Matrix(1, 1);

                testMatrix_vv = preLastMatrixE_v * lastMatrix_v;

                if (testMatrix_vv[0, 0] >= 0)
                {
                    successful1++;
                }

            }
            Debug.Log("Attackers: R" + l + " " + "player " + successful1);
            if (l == numRounds1 + ost1 && ost1 == 1)
            {
                percentage1[l] = (double)(8 - (mod1 - successful1)) / (double)8 * (double)100;
            }
            else
            {
                percentage1[l] = (double)successful1 / (double)8 * (double)100;
            }

        } 

        double average1 = 100;

        for(int k = 1; k <= numRounds1 + ost1; k++)
        {
            if(percentage1[k] < average1)
            average1 = percentage1[k];
        } 


        validationWindow.gameObject.SetActive(true);


        for (int l = 1; l <= numRounds2 + ost2; l++)                            //***********2CLASS***************
        {
            E_hat_v = new double[numRounds2 + 2, m + 1, m + 1];
            a1_Mines_a2_v = new double[numRounds2 + 2, m + 1];
            half_sum_a1_a2_v = new double[numRounds2 + 2, m + 1];
            preLastMatrixE_v = new Matrix(1, m);

            for (int k = 1; k <= 6; k++)
            {
                for (int j = 1; j <= 6; j++)
                {
                    E_hat_v[l, k, j] = ((double)(n1 - 1 - 8) * E_Class1_v[l, k, j] + (double)(n2 - 1 - 8) * E_Class2_v[l, k, j]) / ((double)(n1 - 1 - 8) + (double)(n2 - 1 - 8) - 2);
                }
            }

            var testMatrix_v = new Matrix(6, 6);

            for (int k = 1; k <= 6; k++)
            {
                for (int j = 1; j <= 6; j++)
                {
                    testMatrix_v[k - 1, j - 1] = E_hat_v[l, k, j];
                }
            }

            var inversibleMatrix_v = testMatrix_v.CreateInvertibleMatrix();

            var tempTranspose_v = new Matrix(1, 6);

            for (int j = 1; j <= 6; j++)
            {
                a1_Mines_a2_v[l, j] = a_hat_Class1_v[l, j] - a_hat_Class2_v[l, j];
                tempTranspose_v[0, j - 1] = a1_Mines_a2_v[l, j];
            }


            for (int j = 1; j <= 6; j++)
            {
                half_sum_a1_a2_v[l, j] = (a_hat_Class1_v[l, j] + a_hat_Class2_v[l, j]) / (double)2;
            }


            var multipliedMatrixE_v = tempTranspose_v * inversibleMatrix_v;

            for (int j = 1; j <= 6; j++)
            {
                preLastMatrixE_v[0, j - 1] = multipliedMatrixE_v[0, j - 1];
            }

            int successful2 = 0;                                                //количество игроков, успешно прошедших валидацию в текущем раунде
            int numPlayersInRound = 8;                                          //количество игроков в текущем раунде

            if (l == numRounds2 + ost2 && ost2 == 1)                            //если есть неполный раунд количество игроков в текущем раунде меняется соответственно
            {
                numPlayersInRound = mod2;
            }

            for (int n = 1; n <= numPlayersInRound; n++)
            {
                int[] x = new int[m + 1];                                       //характеристики тестовых игроков текущего раунда
                if(n == 1 && lastPlayerCharacteristics2 != null)
                {
                    Debug.Log("Ласт плеер в защ");
                    x[1] = lastPlayerCharacteristics2[1];
                    x[2] = lastPlayerCharacteristics2[2];
                    x[3] = lastPlayerCharacteristics2[3];
                    x[4] = lastPlayerCharacteristics2[4];
                    x[5] = lastPlayerCharacteristics2[5];
                    x[6] = lastPlayerCharacteristics2[6];
                } else
                {
                    x[1] = testPlayers2[l, n, 1];
                    x[2] = testPlayers2[l, n, 2];
                    x[3] = testPlayers2[l, n, 3];
                    x[4] = testPlayers2[l, n, 4];
                    x[5] = testPlayers2[l, n, 5];
                    x[6] = testPlayers2[l, n, 6];
                }

                var lastMatrix_v = new Matrix(m, 1);

                for (int j = 1; j <= 6; j++)
                {
                    lastMatrix_v[j - 1, 0] = (x[j] - half_sum_a1_a2_v[l, j]);
                }

                var testMatrix_vv = new Matrix(1, 1);

                testMatrix_vv = preLastMatrixE_v * lastMatrix_v;

                if (testMatrix_vv[0, 0] < 0)
                {
                    successful2++;
                }

            }
            Debug.Log("Defenders: R" + l + " " + "player " + successful2);
            if (l == numRounds2 + ost2 && ost2 == 1)
            {
                percentage2[l] = (double)(8 - (mod2 - successful2)) / (double)8 * (double)100;
            }
            else
            {
                percentage2[l] = (double)successful2 / (double)8 * (double)100;
            }

        }
        

        double average2 = 100;

        for (int k = 1; k <= numRounds2 + ost2; k++)
        {
            if (percentage2[k] < average2)
                average2 = percentage2[k];
        }


        if (numOfValidation == 1 && lastPlayerCharacteristics1 == null && lastPlayerCharacteristics2 == null)
        {
            outputValidation.text = "No tested players yet\n Test player or Update the database";
            numOfValidation++;
            StartValidation();
        } else if (numOfValidation == 1)
        { 
            outputValidation.text = "Player modeling accuracy: " + ((average1 + average2) / (double)2) + "%\n";
            Debug.Log("Player modeling accuracy: " + ((average1 + average2) / (double)2) + "%\n");
            lastPlayerCharacteristicsCopy1 = lastPlayerCharacteristics1;
            lastPlayerCharacteristicsCopy2 = lastPlayerCharacteristics2;

            lastPlayerCharacteristics1 = null;
            lastPlayerCharacteristics2 = null;

            numOfValidation++;
            StartValidation();
        } else
        {
            outputValidation1.text = "Overall analysis modeling accuracy: " + ((average1 + average2) / (double)2) + "%\n";
            Debug.Log("Overall analysis modeling accuracy: " + ((average1 + average2) / (double)2) + "%\n");
            numOfValidation--;
            lastPlayerCharacteristics1 = lastPlayerCharacteristicsCopy1;
            lastPlayerCharacteristics2 = lastPlayerCharacteristicsCopy2;
        }

    }


    public void AddPlayerToAttackers()
    {
        path1 = Application.persistentDataPath + "/Class1.txt";
        path2 = Application.persistentDataPath + "/Class2.txt";

        File.AppendAllText(path1, "\n" + playerName.text + "   " + x[1] + " " + x[2] + " " +
            x[3] + " " + x[4] + " " + x[5] + " " + x[6]);

        Learn();
    }


    public void AddPlayerToDefenders()
    {
        path1 = Application.persistentDataPath + "/Class1.txt";
        path2 = Application.persistentDataPath + "/Class2.txt";

        File.AppendAllText(path2, "\n" + playerName.text + "   " + x[1] + " " + x[2] + " " +
           x[3] + " " + x[4] + " " + x[5] + " " + x[6]);

        Learn();
    }


    public void ShowDatabase()
    {
        databaseWindow.gameObject.SetActive(true);
        ShowClass1();
    }

    
    public void ShowClass1()
    {
        outputDatabaseNames.text = "";
        outputDatabaseData.text = "";
        classToShow = true;

        int i = 0;

        foreach (string s in class1)
        {
            if (i == 0)
            {
                i++;
                continue;
            }

            char[] mas = s.ToCharArray();

            outputDatabaseNames.text += "      ";
            foreach (char h in mas)
            {
                if (!char.IsDigit(h))
                {
                    outputDatabaseNames.text += h;
                }
                else
                {
                    outputDatabaseNames.text += "\n";
                    for (int k = 1; k <= 6; k++)
                    {
                        outputDatabaseData.text += x_Class1[i, k] + " ";
                    }
                    outputDatabaseData.text += "\n";
                    break;
                }
            }
            i++;
        }
    }


    public void ShowClass2()
    {
        outputDatabaseNames.text = "";
        outputDatabaseData.text = "";
        classToShow = false;

        int i = 0;

        foreach (string s in class2)
        {
            if (i == 0)
            {
                i++;
                continue;
            }

            char[] mas = s.ToCharArray();

            outputDatabaseNames.text += "      ";
            foreach (char h in mas)
            {
                if (!char.IsDigit(h))
                {
                    outputDatabaseNames.text += h;
                }
                else
                {
                    outputDatabaseNames.text += "\n";
                    for (int k = 1; k <= 6; k++)
                    {
                        outputDatabaseData.text += x_Class2[i, k] + " ";
                    }
                    outputDatabaseData.text += "\n";
                    break;
                }
            }
            i++;
        }
    }


    public void DeletePlayer()
    {
        int i = 0;
        
        if (classToShow)
        {
            
            foreach (string s in class1)
            {
                if(i <= 40)
                {
                    i++;
                    continue;
                }

                char[] mas = s.ToCharArray();
                string name = "";

                foreach (char h in mas)
                {
                    if (!char.IsDigit(h))
                    {
                        name += h;
                    }
                    else
                    {
                        break;
                    }
                }

                if (name.Trim().ToLower().Equals(playerToDelete.text.Trim().ToLower()))
                {
                    if(name.Trim().ToLower().Equals(playerToValidate.text.Trim().ToLower()))
                    {
                        playerToValidate.text = "";
                        lastPlayerCharacteristics1 = null;
                        lastPlayerCharacteristics2 = null;
                        lastPlayerCharacteristicsCopy1 = null;
                        lastPlayerCharacteristicsCopy2 = null;
                    }

                    class1[i] = class1[i].Remove(s.Trim().ToLower().IndexOf(playerToDelete.text.Trim().ToLower()));
                    n1--;

                    string[] tempClass1 = class1;

                    File.WriteAllText(path1, "");

                    int j = 0;
                    foreach (string line in tempClass1)
                    {
                        if (!string.IsNullOrWhiteSpace(line))
                        {
                            if (j == 0)
                            {
                                File.AppendAllText(path1, line);
                            }
                            else
                            {
                                File.AppendAllText(path1, "\n" + line);
                            }
                        }

                        j++;
                    }

                    class1 = File.ReadAllLines(path1);

                    errorPlayerNotFound.SetActive(false);

                    Learn();
                    ShowClass1();

                    break;
                }

                i++;
                errorPlayerNotFound.SetActive(true);
            }
        }
        else
        {
            
            foreach (string s in class2)
            {
                if (i <= 40)
                {
                    i++;
                    continue;
                }

                char[] mas = s.ToCharArray();
                string name = "";

                foreach (char h in mas)
                {
                    if (!char.IsDigit(h))
                    {
                        name += h;
                    }
                    else
                    {
                        break;
                    }
                }

                if (name.Trim().ToLower().Equals(playerToDelete.text.Trim().ToLower()))
                {
                    playerToValidate.text = "";
                    lastPlayerCharacteristics1 = null;
                    lastPlayerCharacteristics2 = null;
                    lastPlayerCharacteristicsCopy1 = null;
                    lastPlayerCharacteristicsCopy2 = null;

                    class2[i] = class2[i].Remove(s.Trim().ToLower().IndexOf(playerToDelete.text.Trim().ToLower()));
                    n2--;

                    string[] tempClass2 = class2;

                    File.WriteAllText(path2, "");

                    int j = 0;
                    foreach (string line in tempClass2)
                    {
                        if (!string.IsNullOrWhiteSpace(line))
                        {
                            if (j == 0)
                            {
                                File.AppendAllText(path2, line);
                            }
                            else
                            {
                                File.AppendAllText(path2, "\n" + line);
                            }
                        }
                                
                        j++;
                    }

                    class2 = File.ReadAllLines(path2);

                    errorPlayerNotFound.SetActive(false);

                    Learn();
                    ShowClass2();

                    break;
                }

                i++;
                errorPlayerNotFound.SetActive(true);
            }
        }
    }


    public void TransferPlayer()
    {
        int i = 0;

        if (classToShow)
        {

            foreach (string s in class1)
            {
                if (i <= 40)
                {
                    i++;
                    continue;
                }

                char[] mas = s.ToCharArray();
                string name = "";
                string str2 = "";
                int k = 0;
                foreach (char h in mas)
                {
                    if (!char.IsDigit(h))
                    {
                        name += h;
                    }
                    else
                    {
                        break;
                    }
                }

                if (name.Trim().ToLower().Equals(playerToTransfer.text.Trim().ToLower()))
                {
                    path1 = Application.persistentDataPath + "/Class1.txt";
                    path2 = Application.persistentDataPath + "/Class2.txt";

                    playerToValidate.text = "Croos-validation for player " + name.Trim();
                    lastPlayerCharacteristics2 = new int[7];
                    lastPlayerCharacteristics1 = null;

                    mas = s.ToCharArray();
                    name = "";
                    str2 = "";
                    k = 0;
                    foreach (char h in mas)
                    {
                        if (char.IsDigit(h))
                        {
                            k++;
                            str2 += h;
                            if (k % 2 == 0)
                            {
                                lastPlayerCharacteristics2[k / 2] = Convert.ToInt32(str2);
                                str2 = "";
                            }
                        }
                    }

                    File.AppendAllText(path2, "\n" + s);

                    class1[i] = class1[i].Remove(s.Trim().ToLower().IndexOf(playerToTransfer.text.Trim().ToLower()));
                    n1--;

                    string[] tempClass1 = class1;

                    File.WriteAllText(path1, "");

                    int j = 0;
                    foreach (string line in tempClass1)
                    {
                        if (!string.IsNullOrWhiteSpace(line))
                        {
                            if (j == 0)
                            {
                                File.AppendAllText(path1, line);
                            }
                            else
                            {
                                File.AppendAllText(path1, "\n" + line);
                            }
                        }

                        j++;
                    }

                    class1 = File.ReadAllLines(path1);
                    class2 = File.ReadAllLines(path2);

                    errorPlayerNotFound.SetActive(false);

                    Learn();

                    ShowClass1();

                    break;
                }

                i++;
                errorPlayerNotFound.SetActive(true);
            }
        }
        else
        {

            foreach (string s in class2)
            {
                if (i <= 40)
                {
                    i++;
                    continue;
                }

                char[] mas = s.ToCharArray();
                string name = "";
                string str2 = "";
                int k = 0;
                foreach (char h in mas)
                {
                    if (!char.IsDigit(h))
                    {
                        name += h;
                    }
                    else
                    {
                        break;
                    }
                }

                if (name.Trim().ToLower().Equals(playerToTransfer.text.Trim().ToLower().ToLower()))
                {
                    path1 = Application.persistentDataPath + "/Class1.txt";
                    path2 = Application.persistentDataPath + "/Class2.txt";

                    playerToValidate.text = "Croos-validation for player " + name.Trim();
                    lastPlayerCharacteristics1 = new int[7];
                    lastPlayerCharacteristics2 = null;

                    mas = s.ToCharArray();
                    name = "";
                    str2 = "";
                    k = 0;
                    foreach (char h in mas)
                    {
                        if (char.IsDigit(h))
                        {
                            k++;
                            str2 += h;
                            if (k % 2 == 0)
                            {
                                lastPlayerCharacteristics1[k / 2] = Convert.ToInt32(str2);
                                str2 = "";
                            }
                        }
                    }

                    File.AppendAllText(path1, "\n" + s);

                    class2[i] = class2[i].Remove(s.Trim().ToLower().IndexOf(playerToTransfer.text.Trim().ToLower()));
                    n2--;

                    string[] tempClass2 = class2;

                    File.WriteAllText(path2, "");

                    int j = 0;
                    foreach (string line in tempClass2)
                    {
                        if (!string.IsNullOrWhiteSpace(line))
                        {
                            if (j == 0)
                            {
                                File.AppendAllText(path2, line);
                            }
                            else
                            {
                                File.AppendAllText(path2, "\n" + line);
                            }
                        }

                        j++;
                    }

                    class1 = File.ReadAllLines(path1);
                    class2 = File.ReadAllLines(path2);

                    errorPlayerNotFound.SetActive(false);

                    Learn();
                    
                    ShowClass2();

                    break;
                }

                i++;
                errorPlayerNotFound.SetActive(true);
            }
        }
    }


    public void OnClickShowWebView()
    {
        WebViewObject webViewObject = FindObjectOfType<WebViewObject>().GetComponent<WebViewObject>();
        if (webViewObject)
        {
            webViewObject.LoadURL(URL.text.Replace(" ", "%20"));
            webViewObject.SetVisibility(true);
        }
    }


    public void OnClickCloseWebView()
    {
        WebViewObject webViewObject = FindObjectOfType<WebViewObject>().GetComponent<WebViewObject>();
        if (webViewObject)
        {
            webViewObject.SetVisibility(false);
        }
    }


}


public class Matrix
{
    private double[,] data;
    private double precalculatedDeterminant = double.NaN;

    private int m;
    public int M { get => this.m; }

    private int n;
    public int N { get => this.n; }

    public bool IsSquare { get => this.M == this.N; }

    public void ProcessFunctionOverData(Action<int, int> func)
    {
        for (var i = 0; i < this.M; i++)
        {
            for (var j = 0; j < this.N; j++)
            {
                func(i, j);
            }
        }
    }

    public static Matrix CreateIdentityMatrix(int n)
    {
        var result = new Matrix(n, n);
        for (var i = 0; i < n; i++)
        {
            result[i, i] = 1;
        }
        return result;
    }

    public Matrix CreateTransposeMatrix()
    {
        var result = new Matrix(this.N, this.M);
        result.ProcessFunctionOverData((i, j) => result[i, j] = this[j, i]);
        return result;
    }

    public Matrix(int m, int n)
    {
        this.m = m;
        this.n = n;
        this.data = new double[m, n];
        this.ProcessFunctionOverData((i, j) => this.data[i, j] = 0);
    }

    public double this[int x, int y]
    {
        get
        {
            return this.data[x, y];
        }
        set
        {
            this.data[x, y] = value;
            this.precalculatedDeterminant = double.NaN;
        }
    }

    public double CalculateDeterminant()
    {
        if (!double.IsNaN(this.precalculatedDeterminant))
        {
            return this.precalculatedDeterminant;
        }
        if (!this.IsSquare)
        {
            throw new InvalidOperationException("determinant can be calculated only for square matrix");
        }
        if (this.N == 2)
        {
            return this[0, 0] * this[1, 1] - this[0, 1] * this[1, 0];
        }
        double result = 0;
        for (var j = 0; j < this.N; j++)
        {
            result += (j % 2 == 1 ? 1 : -1) * this[1, j] *
                this.CreateMatrixWithoutColumn(j).CreateMatrixWithoutRow(1).CalculateDeterminant();
        }
        this.precalculatedDeterminant = result;
        return result;
    }

    public Matrix CreateInvertibleMatrix()
    {
        if (this.M != this.N)
            return null;
        var determinant = CalculateDeterminant();
        if (Math.Abs(determinant) < Constants.DoubleComparisonDelta)
            return null;

        var result = new Matrix(M, M);
        ProcessFunctionOverData((i, j) =>
        {
            result[i, j] = ((i + j) % 2 == 1 ? -1 : 1) * CalculateMinor(i, j) / determinant;
        });

        result = result.CreateTransposeMatrix();
        return result;
    }

    private double CalculateMinor(int i, int j)
    {
        return CreateMatrixWithoutColumn(j).CreateMatrixWithoutRow(i).CalculateDeterminant();
    }

    private Matrix CreateMatrixWithoutRow(int row)
    {
        if (row < 0 || row >= this.M)
        {
            throw new ArgumentException("invalid row index");
        }
        var result = new Matrix(this.M - 1, this.N);
        result.ProcessFunctionOverData((i, j) => result[i, j] = i < row ? this[i, j] : this[i + 1, j]);
        return result;
    }

    private Matrix CreateMatrixWithoutColumn(int column)
    {
        if (column < 0 || column >= this.N)
        {
            throw new ArgumentException("invalid column index");
        }
        var result = new Matrix(this.M, this.N - 1);
        result.ProcessFunctionOverData((i, j) => result[i, j] = j < column ? this[i, j] : this[i, j + 1]);
        return result;
    }

    public static Matrix operator *(Matrix matrix, double value)
    {
        var result = new Matrix(matrix.M, matrix.N);
        result.ProcessFunctionOverData((i, j) => result[i, j] = matrix[i, j] * value);
        return result;
    }

    public static Matrix operator *(Matrix matrix, Matrix matrix2)
    {
        if (matrix.N != matrix2.M)
        {
            throw new ArgumentException("matrixes can not be multiplied");
        }
        var result = new Matrix(matrix.M, matrix2.N);
        result.ProcessFunctionOverData((i, j) =>
        {
            for (var k = 0; k < matrix.N; k++)
            {
                result[i, j] += matrix[i, k] * matrix2[k, j];
            }
        });
        return result;
    }

    public static Matrix operator +(Matrix matrix, Matrix matrix2)
    {
        if (matrix.M != matrix2.M || matrix.N != matrix2.N)
        {
            throw new ArgumentException("matrixes dimensions should be equal");
        }
        var result = new Matrix(matrix.M, matrix.N);
        result.ProcessFunctionOverData((i, j) => result[i, j] = matrix[i, j] + matrix2[i, j]);
        return result;
    }

    public static Matrix operator -(Matrix matrix, Matrix matrix2)
    {
        return matrix + (matrix2 * -1);
    }
}


public static class Constants
{
    public const double DoubleComparisonDelta = 0.0000001;
}
