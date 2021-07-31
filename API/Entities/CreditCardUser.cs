using System;

namespace API.Entities
{
    public class CreditCardUser
    {
        // public string nameOnCard { get; set; }
        // public int cardNumber { get; set; }
        // public DateTime expirationDate { get; set; }
        // public int securityCode { get; set; }
        //public string expirationDate2 {get; set;}
        public string cardTransactionType {get; set;}
        public int AmtDonated {get; set;}
        public int usdAmount {get; set;}
        public string currencyName {get; set;}
        public string processingStatus {get; set;}
        public Guid transactionId {get; set;}
        public string transactionApprovalDate {get; set;}
        public string transactionApprovalTime {get; set;}
        public int authorizationCode {get; set;}

    }
}