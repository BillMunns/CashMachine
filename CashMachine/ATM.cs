using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashMachine
{
    public class ATM
    {
        public Dictionary<int, int> cashOnHand;
        public Dictionary<int, int> cashNeeded;

        public ATM()
        {
            cashOnHand = new Dictionary<int, int>()                 // what the ATM has
            {
                {100,10},{50,10},{20,10},{10,10},{5,10},{1,10}      // in descending order for calculation simplification
            };

            cashNeeded = new Dictionary<int, int>()                 // what the withdrawal will need
            {
                {100,0},{50,0},{20,0},{10,0},{5,0},{1,0}            // in descending order for calculation simplification
            };            
        }

        public void InquirySelected(string denominations)
        {   //Displays the number of bills in requested denomination present in the cash machine 
            try
            {
                List<string> amounts = denominations.Replace("I ", "").Split(' ').ToList();                 // parse out denominations into a list, removing 'I ' prefix 
                if (validateInquiry(amounts))                                                               // ensure that all values in array are in proper format
                {
                    Console.WriteLine("Machine Balance:");
                    foreach (string amount in amounts)
                    {
                        foreach (var pairing in cashOnHand.ToList())
                        {
                            if (Convert.ToInt32(amount.Replace("$", "")) == pairing.Key)                    // print only denominations requested
                            {
                                Console.WriteLine(String.Format("${0} - {1}", pairing.Key.ToString(), pairing.Value.ToString()));
                            }
                        }
                    }
                    Console.WriteLine("\r\n");
                }
                else
                {
                    Console.WriteLine("Invalid Command\r\n");
                }
            }
            catch
            {
                Console.WriteLine("Invalid Command\r\n");
            }
        }

        public void InquiryALL()
        {   //Displays the number of bills in all denominations present in the cash machine 
            try
            {
                Console.WriteLine("Machine Balance:");
                var temp = cashOnHand.Select(x => "$" + x.Key.ToString() + " - " + x.Value.ToString()).ToList();           // print all denominations
                Console.WriteLine(String.Join("\r\n", temp));
                Console.WriteLine("\r\n");
            }
            catch
            {
                Console.WriteLine("Invalid Command\r\n");
            }
        }

        public void Withdraw(string command)
        {   //Withdraws that amount from the cash machine and adjusts remaining balance

            if (command.Length > 1 && command[1] != ' ')                    // ensure that we're "W $xxx" not "Wx $xxx"
            {
                Console.WriteLine("Invalid Command\r\n");
                return;
            }

            try
            {
                int amount = Convert.ToInt32(command.Split('$')[1]);        // parse out int from $xxx string
                int remainingAmount = amount;                               // pre-assign amount to remaining amount that we decrement with Calculate

                foreach (var key in cashOnHand.Keys.ToList())               
                {
                    remainingAmount = Calculate(remainingAmount, key);      // do the math
                    if (remainingAmount == 0) break;                        // we got it
                }

                if (remainingAmount != 0)
                {
                    Console.WriteLine("Failure: Insufficient funds\r\n");
                    ReturnNeedToOnHand();                                   // re-add to cashOnHand what we put in cashNeeded (and zero out cashNeeded) 
                }
                else
                {
                    Console.WriteLine(String.Format("Success: Dispensed ${0}\r\n", amount));
                    InquiryALL();                                           // display onhand inquiry
                    ResetCashNeeded();                                      // zero out cashNeeded
                }
            }
            catch
            {
                Console.WriteLine("Invalid Command\r\n");
            }

        }
        
        public void ReloadATM(string command)
        {
            if (command.Length > 1)
            {
                Console.WriteLine("Invalid Command\r\n");
                return;
            }
            try
            {
                foreach (var key in cashOnHand.Keys.ToList())
                {
                    cashOnHand[key] = 10;                                   // reset to 10
                }
                InquiryALL();                                               // display balance
            }
            catch
            {
                Console.WriteLine("Invalid Command\r\n");
            }
        }

        public bool validateInquiry(List<string> amounts)
        {
            if (isDollar(amounts) && isKey(amounts))
            {   
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool isDollar(List<string> amounts)
        {   // validate that all values in array begin with "$" 
            try
            {

                string prefix = new string(amounts.Select(s => s[0]).ToArray());
                if (prefix.Replace("$", "").Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool isKey(List<string> amounts) 
        {   // validate that value(s) requested is/are a proper denomination e.g. there ain't no $25 bill
            try
            {
                foreach (string amount in amounts)
                {
                    if (!cashOnHand.Keys.Contains(Convert.ToInt32(amount.Replace("$", ""))))
                    {
                        return false;
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public int Calculate(int amount, int dollarBill)
        {   // return remaining amount based on dollarbill, optimized by call of highest to lowest
            int remainingAmount;
            cashNeeded[dollarBill] = amount / dollarBill;
            if (cashNeeded[dollarBill] <= cashOnHand[dollarBill])
            {
                cashOnHand[dollarBill] -= cashNeeded[dollarBill];                       // we only need some of what's left on hand
                remainingAmount = amount % dollarBill;                                  // mod div returns remaining amount
            }
            else
            {
                cashNeeded[dollarBill] = cashOnHand[dollarBill];                        // we need all of what's left on hand
                cashOnHand[dollarBill] = 0;
                remainingAmount = amount - (cashNeeded[dollarBill] * dollarBill);       // calculate the remaining amount
            }
            
            return remainingAmount;
        }

        private void ResetCashNeeded()
        {   // re-initialize (as zero) for next request
            cashNeeded = new Dictionary<int, int>()
            {
                {100,0},{50,0},{20,0},{10,0},{5,0},{1,0}
            }; 
        }

        private void ReturnNeedToOnHand()
        {   // withdraw was voided, add values we put into cashNeeded back into cashOnHand
            foreach (var key in cashNeeded.Keys.ToList())
            {
                cashOnHand[key] += cashNeeded[key];
                cashNeeded[key] = 0;
            }
        }
    }
}
