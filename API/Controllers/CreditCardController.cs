using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers

{
    public class CreditCardController : BasicApiController
    {
        
    [HttpPost("test")]
// public string Hello(string ccv) 
// {
//     return ccv;
// }
//  Credit Card Month and Year Info    
//  expirationDate = new DateTime(2025, 05, 1),
//  ccUser.expirationDate2 = ccUser.expirationDate.ToString("MM/yyyy");
        public string Charge(int chargeAmt)
        {
            string AmtDonatedAccept = "false";
            if(chargeAmt%2 ==0 )
            {
             AmtDonatedAccept = "success";
            }
            else
            {
                AmtDonatedAccept = "failure";
            }
            return AmtDonatedAccept;
        }
       public int authorizeCode = 123456;
       public int convertAmtIntoOtherCurrencies(int AmtDonated, int usdAmtDonated, string AmtType)
       {
           if(AmtType == "USD")
           {
               usdAmtDonated = AmtDonated;
           }
        //    else 
        //    {
        //        check for other currencies
        //    }
           return AmtDonated;
       }

// Get
// Sending in params of 
// Returning json of: 
//
        [HttpPost("Tokenize")]
        public CreditCardUser tokenizeWithCCInfo(string typeOfTransaction, int chargeAmt, string currencyType, int usdAmount)
        {
            return new CreditCardUser
            {
                //Type of Transaction can be "AUTH_CAPTURE"
                cardTransactionType = typeOfTransaction,
                AmtDonated = chargeAmt,
                usdAmount = convertAmtIntoOtherCurrencies(chargeAmt, usdAmount, currencyType),
                currencyName = currencyType,
                processingStatus = Charge(chargeAmt),
                transactionId = Guid.NewGuid(),
                transactionApprovalDate = DateTime.Now.ToShortDateString(),
                transactionApprovalTime = DateTime.Now.ToString("HH:mm:ss"),
                authorizationCode = authorizeCode

            };
        }

        [HttpGet("Charge")]
        public CreditCardUser chargeUser()
        {
            return tokenizeWithCCInfo("AUTH_Capture",50,"USD", 50);
        }
    }
}