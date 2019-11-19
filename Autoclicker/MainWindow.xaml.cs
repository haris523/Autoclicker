using DesktopWPFAppLowLevelKeyboardHook;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Autoclicker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);

        const uint MOUSEEVENTF_ABSOLUTE = 0x8000;
        const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        const uint MOUSEEVENTF_LEFTUP = 0x0004;
        const uint MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        const uint MOUSEEVENTF_MIDDLEUP = 0x0040;
        const uint MOUSEEVENTF_MOVE = 0x0001;
        const uint MOUSEEVENTF_RIGHTDOWN = 0x0008;
        const uint MOUSEEVENTF_RIGHTUP = 0x0010;
        const uint MOUSEEVENTF_XDOWN = 0x0080;
        const uint MOUSEEVENTF_XUP = 0x0100;
        const uint MOUSEEVENTF_WHEEL = 0x0800;
        const uint MOUSEEVENTF_HWHEEL = 0x01000;
        bool izvrsavaSe = false;
        private LowLevelKeyboardListener _listener;
        CancellationTokenSource tsOld = new CancellationTokenSource();
        Task task;
       /* private void Pokreni_Click(object sender, RoutedEventArgs e)
        {
                izvrsavaSe = true;
                int str, str2, leftClickTimer, afkTimer, brojZavrsenihSekundi = 0, triggerAfkDonjiStr, triggerAfkGornjiStr, triggerAfkTimer,
            afkTimerDonjiStr, afkTimerGornjiStr, holddownDonjiStr, holddownGornjiStr, holddownTimer;
                Random rnd = new Random();
                Random triggerAfkRnd = new Random();
                Random afkTimerRnd = new Random();
                Random holddownTimerRnd = new Random();
                //vrijednosti iz xamla
                str = Int32.Parse(intervalDonja.Text);
                str2 = Int32.Parse(intervalGornja.Text);
                triggerAfkDonjiStr = Int32.Parse(triggerAfkDonji.Text);
                triggerAfkGornjiStr = Int32.Parse(triggerAfkGornji.Text);
                afkTimerDonjiStr = Int32.Parse(afkTimerDonji.Text);
                afkTimerGornjiStr = Int32.Parse(afkTimerGornji.Text);
                holddownDonjiStr = Int32.Parse(holddownDonji.Text);
                holddownGornjiStr = Int32.Parse(holddownGornji.Text);

                //random pocetna vrijednost za afk timer
                triggerAfkTimer = triggerAfkRnd.Next(triggerAfkDonjiStr, triggerAfkGornjiStr + 1);
                triggerAfkTimer = triggerAfkTimer * 60 * 1000;
                var task = Task.Run(async () => {
                    //Debug.Print("5 sec ceka");
                    await Task.Delay(5000);
                    for (; ; )
                    {
                        holddownTimer = holddownTimerRnd.Next(holddownDonjiStr, holddownGornjiStr + 1);
                        //U slucaju da je proslo 10 min za trigger(recimo) hocu da mi dadne random vrijeme prije slj klika i resetuje random vrijeme cekanja
                        if (brojZavrsenihSekundi >= triggerAfkTimer)
                        {
                            brojZavrsenihSekundi = 0;
                            afkTimer = afkTimerRnd.Next(afkTimerDonjiStr, afkTimerGornjiStr + 1);
                            triggerAfkTimer = triggerAfkRnd.Next(triggerAfkDonjiStr, triggerAfkGornjiStr + 1);
                            triggerAfkTimer = triggerAfkTimer * 60 * 1000;
                            //Debug.Print("Ceka"+afkTimer+"sekundi");
                            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                            await Task.Delay(holddownTimer);
                            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                            await Task.Delay(afkTimer * 1000);
                        }
                        leftClickTimer = rnd.Next(str, str2 + 1);
                        //Debug.Print("left click timer " + leftClickTimer);
                        brojZavrsenihSekundi += leftClickTimer;
                        await Task.Delay(leftClickTimer);
                        //Debug.Print("Hello World after" + leftClickTimer + "seconds");
                        mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                        //Debug.Print("startan klik");
                        await Task.Delay(holddownTimer);
                        mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                        //Debug.Print("endan klik");

                    }
                });
            

        }*/
        private void Form_Load(object sender, RoutedEventArgs e)
        {
            _listener = new LowLevelKeyboardListener();
            _listener.OnKeyPressed += _listener_OnKeyPressed;

            _listener.HookKeyboard();
        }
        void _listener_OnKeyPressed(object sender, KeyPressedArgs e)
        {
            Debug.Print("Key je pressed "+e.KeyPressed.ToString());
            // ... Test for F5 key.
            if (e.KeyPressed.ToString() == Key.F5.ToString())
            {
                int str, str2, leftClickTimer, afkTimer, brojZavrsenihSekundi = 0, triggerAfkDonjiStr, triggerAfkGornjiStr, triggerAfkTimer,
           afkTimerDonjiStr, afkTimerGornjiStr, holddownDonjiStr, holddownGornjiStr, holddownTimer;
                Random rnd = new Random();
                Random triggerAfkRnd = new Random();
                Random afkTimerRnd = new Random();
                Random holddownTimerRnd = new Random();
                //vrijednosti iz xamla
                str = Int32.Parse(intervalDonja.Text);
                str2 = Int32.Parse(intervalGornja.Text);
                triggerAfkDonjiStr = Int32.Parse(triggerAfkDonji.Text);
                triggerAfkGornjiStr = Int32.Parse(triggerAfkGornji.Text);
                afkTimerDonjiStr = Int32.Parse(afkTimerDonji.Text);
                afkTimerGornjiStr = Int32.Parse(afkTimerGornji.Text);
                holddownDonjiStr = Int32.Parse(holddownDonji.Text);
                holddownGornjiStr = Int32.Parse(holddownGornji.Text);

                //random pocetna vrijednost za afk timer
                triggerAfkTimer = triggerAfkRnd.Next(triggerAfkDonjiStr, triggerAfkGornjiStr + 1);
                triggerAfkTimer = triggerAfkTimer * 60 * 1000;
                var ts = new CancellationTokenSource();
                if (!izvrsavaSe)
                    tsOld = ts;
                CancellationToken ct = tsOld.Token;
                if (!izvrsavaSe)
                {
                    task = Task.Run(async () =>
                    {

                        Debug.Print("Pocinje");
                        izvrsavaSe = true;
                        for (; ; )
                        {
                            holddownTimer = holddownTimerRnd.Next(holddownDonjiStr, holddownGornjiStr + 1);
                            //U slucaju da je proslo 10 min za trigger(recimo) hocu da mi dadne random vrijeme prije slj klika i resetuje random vrijeme cekanja
                            if (brojZavrsenihSekundi >= triggerAfkTimer)
                            {
                                brojZavrsenihSekundi = 0;
                                afkTimer = afkTimerRnd.Next(afkTimerDonjiStr, afkTimerGornjiStr + 1);
                                triggerAfkTimer = triggerAfkRnd.Next(triggerAfkDonjiStr, triggerAfkGornjiStr + 1);
                                triggerAfkTimer = triggerAfkTimer * 60 * 1000;
                                //Debug.Print("Ceka"+afkTimer+"sekundi");
                                mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                                await Task.Delay(holddownTimer);
                                mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                                await Task.Delay(afkTimer * 1000);
                            }
                            leftClickTimer = rnd.Next(str, str2 + 1);
                            //Debug.Print("left click timer " + leftClickTimer);
                            brojZavrsenihSekundi += leftClickTimer;
                            await Task.Delay(leftClickTimer);
                            //Debug.Print("Hello World after" + leftClickTimer + "seconds");
                            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                            //Debug.Print("startan klik");
                            await Task.Delay(holddownTimer);
                            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                            //Debug.Print("endan klik");
                            //Debug.Print("ISpred cancelanja");
                            if (ct.IsCancellationRequested)
                            {
                                // another thread decided to cancel
                                //Console.WriteLine("task canceled");
                                break;
                            }

                        }

                    }, ct);
                }
                else
                {
                    izvrsavaSe = false;
                    //Thread.Sleep(3000);
                    Debug.Print("Zavrsi");
                    // Can't wait anymore => cancel this task                  
                    tsOld.Cancel();
                }
            }

        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _listener.UnHookKeyboard();
        }

       /* private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            // ... Test for F5 key.
            if (e.Key == Key.F5)
            {
                int str, str2, leftClickTimer, afkTimer, brojZavrsenihSekundi = 0, triggerAfkDonjiStr, triggerAfkGornjiStr, triggerAfkTimer,
           afkTimerDonjiStr, afkTimerGornjiStr, holddownDonjiStr, holddownGornjiStr, holddownTimer;
                Random rnd = new Random();
                Random triggerAfkRnd = new Random();
                Random afkTimerRnd = new Random();
                Random holddownTimerRnd = new Random();
                //vrijednosti iz xamla
                str = Int32.Parse(intervalDonja.Text);
                str2 = Int32.Parse(intervalGornja.Text);
                triggerAfkDonjiStr = Int32.Parse(triggerAfkDonji.Text);
                triggerAfkGornjiStr = Int32.Parse(triggerAfkGornji.Text);
                afkTimerDonjiStr = Int32.Parse(afkTimerDonji.Text);
                afkTimerGornjiStr = Int32.Parse(afkTimerGornji.Text);
                holddownDonjiStr = Int32.Parse(holddownDonji.Text);
                holddownGornjiStr = Int32.Parse(holddownGornji.Text);

                //random pocetna vrijednost za afk timer
                triggerAfkTimer = triggerAfkRnd.Next(triggerAfkDonjiStr, triggerAfkGornjiStr + 1);
                triggerAfkTimer = triggerAfkTimer * 60 * 1000;
                var ts = new CancellationTokenSource();
                if(!izvrsavaSe)
                 tsOld = ts;
                CancellationToken ct = tsOld.Token;
                if (!izvrsavaSe) { 
                task = Task.Run(async () =>
                {

                    Debug.Print("Pocinje");
                    izvrsavaSe = true;
                    for (; ; )
                    {
                        holddownTimer = holddownTimerRnd.Next(holddownDonjiStr, holddownGornjiStr + 1);
                        //U slucaju da je proslo 10 min za trigger(recimo) hocu da mi dadne random vrijeme prije slj klika i resetuje random vrijeme cekanja
                        if (brojZavrsenihSekundi >= triggerAfkTimer)
                        {
                            brojZavrsenihSekundi = 0;
                            afkTimer = afkTimerRnd.Next(afkTimerDonjiStr, afkTimerGornjiStr + 1);
                            triggerAfkTimer = triggerAfkRnd.Next(triggerAfkDonjiStr, triggerAfkGornjiStr + 1);
                            triggerAfkTimer = triggerAfkTimer * 60 * 1000;
                            //Debug.Print("Ceka"+afkTimer+"sekundi");
                            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                            await Task.Delay(holddownTimer);
                            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                            await Task.Delay(afkTimer * 1000);
                        }
                        leftClickTimer = rnd.Next(str, str2 + 1);
                        //Debug.Print("left click timer " + leftClickTimer);
                        brojZavrsenihSekundi += leftClickTimer;
                        await Task.Delay(leftClickTimer);
                        Debug.Print("Hello World after" + leftClickTimer + "seconds");
                        mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                        //Debug.Print("startan klik");
                        await Task.Delay(holddownTimer);
                        mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                        //Debug.Print("endan klik");
                        Debug.Print("ISpred cancelanja");
                        if (ct.IsCancellationRequested)
                        {
                            // another thread decided to cancel
                            Console.WriteLine("task canceled");
                            break;
                        }

                    }

                }, ct);
            }
            else
            {
                izvrsavaSe = false;
                Thread.Sleep(3000);
                Debug.Print("Zavrsi");
                    // Can't wait anymore => cancel this task                  
                    tsOld.Cancel();
            }
            }
        }*/
    }
}
