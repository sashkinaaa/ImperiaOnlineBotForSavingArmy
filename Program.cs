using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SikuliSharp;
using System.IO;
using System.Reflection;

namespace ImperiaOnlineSavingArmyBot
{
    /*This program was written, because Imperia Online developers ruined the game 
     * by putting all the players in the same part of the map, not allowing them to save their army
     *  and all they want is players to buy diamonds. */
    class Program
    {
        //variable for setting the timeout between attacks
        const int initialTimeDelay = 2100000;
        static void Main(string[] args)
        {
            //variable for counting number of attacks, used for additional timeout between attacks
            int numberOfAttacks = 0;
            DateTime now = DateTime.Now;
            Console.WriteLine("Program started normally at: " + now);
            //every iteration of the loop sends an attack, using the saveArmy() method
              while (true)
              {
                  saveArmy();
                  System.Threading.Thread.Sleep(initialTimeDelay + numberOfAttacks*3456);
                  numberOfAttacks++;
              }
        }

        //Method for running Sikuli scripts
        public static Boolean runSikuliScript(string scriptName)
        {
            try
            {
                //check if there is a Sikuli script with the given name in the assembly directory
                if (Boolean.Parse(Sikuli.RunProject(Path.GetDirectoryName(
                Assembly.GetExecutingAssembly().Location) + "\\" + scriptName)))
                {
                    return true;
                }
                else
                    return false;
            }
            catch (FormatException fe)
            {
                Console.WriteLine("Error with Sikuli script: " + scriptName + " !");
                return false;
            }
        }
                
        //Method for refreshing the browser, skipping Captcha and attacking (saving the army)
        public static void saveArmy()
        {
            //run Sikuli script for refreshing the browser
            if (runSikuliScript("refreshBrowser.sikuli"))
            {
                //run Sikuli script that checks if there is an Anti-bot (Captcha)
                if (runSikuliScript("antiBotCheck.sikuli"))
                {
                    Console.WriteLine("Antibot needed at:" + DateTime.Now);
                    //run Sikuli script for closing the Captcha
                    if (runSikuliScript("SkipBotCheck.sikuli"))
                        Console.WriteLine("Antibot skipped at:" + DateTime.Now);
                    else
                        Console.WriteLine("Antibot failed to skip at:" + DateTime.Now);
                }
            }

            //run Sikuli script for attacking a player (saving the army)
            if (runSikuliScript("Attack.sikuli"))
            {
                Console.WriteLine("Attack sent at:" + DateTime.Now);
            }
            }
        }
    }
