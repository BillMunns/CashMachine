using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CashMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            ATM bank = new ATM();

            while (true)
            {                
                string command = Console.ReadLine().ToUpper();

                switch (command.ToUpper().ToCharArray()[0])
                {
                    case 'R':
                        bank.ReloadATM();
                        break;
                    case 'W':
                        bank.Withdraw(command);
                        break;
                    case 'I':
                        bank.InquirySelected(command);
                        break;
                    case 'Q':
                        Environment.Exit(0);
                        break;
                    case 'J':
                        //must... resist... urge... to... joke...
                        //Console.WriteLine("");
                        //Console.WriteLine("####### ####### ####### ######     #     # #######       #        #####  ####### ######     #    #     #     #####     #    ####### ");
                        //Console.WriteLine("#       #       #       #     #    ##   ## #            # #      #     #    #    #     #   # #    #   #     #     #   # #      #    ");
                        //Console.WriteLine("#       #       #       #     #    # # # # #           #   #     #          #    #     #  #   #    # #      #        #   #     #    ");
                        //Console.WriteLine("#####   #####   #####   #     #    #  #  # #####      #     #     #####     #    ######  #     #    #       #       #     #    #    ");
                        //Console.WriteLine("#       #       #       #     #    #     # #          #######          #    #    #   #   #######    #       #       #######    #    ");
                        //Console.WriteLine("#       #       #       #     #    #     # #          #     #    #     #    #    #    #  #     #    #       #     # #     #    #    ");
                        //Console.WriteLine("#       ####### ####### ######     #     # #######    #     #     #####     #    #     # #     #    #        #####  #     #    #    ");
                        //Console.WriteLine("");
                        Console.WriteLine("Invalid Command\r\n");
                        break;
                    default:
                        Console.WriteLine("Invalid Command\r\n");
                        break;
                }
            }

        }
    }
}
