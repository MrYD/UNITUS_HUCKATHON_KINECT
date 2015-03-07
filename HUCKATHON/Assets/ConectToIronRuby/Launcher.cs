using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConectToIronRuby
{
    static class Launcher
    {
        public static void  LaunchIronRuby()
        {
            System.Diagnostics.Process p = new System.Diagnostics.Process();

            //入力できるようにする
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;

            //非同期で出力を読み取れるようにする
            p.StartInfo.RedirectStandardOutput = true;
            //p.OutputDataReceived += p_OutputDataReceived;

            p.StartInfo.FileName =
                System.Environment.CurrentDirectory + @"\Assets\ConectToIronRuby\ir64.exe";
            p.StartInfo.CreateNoWindow = true;

            //起動
            p.Start();

            //非同期で出力の読み取りを開始
            p.BeginOutputReadLine();

            //入力のストリームを取得
            System.IO.StreamWriter sw = p.StandardInput;
            if (sw.BaseStream.CanWrite)
            {
                //終了する
                sw.WriteLine("exit");
            }
            sw.Close();

            p.WaitForExit();
        }
    }
}