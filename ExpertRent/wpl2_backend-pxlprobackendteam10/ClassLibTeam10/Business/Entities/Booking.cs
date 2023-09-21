namespace ClassLibTeam10.Business.Entities
{
    public class Booking
    {
        private int IdSeller;
        private int IdBuyer;
        private string Date;
        private string StartTime;
        private string EndTime;
        private string PaymentNum;
        private string Amount;
        private string PaymentMethod;
        private string PaymentName;
        
        public Booking(int idSeller, int idBuyer, string date, string startTime, string endTime, string paymentNum, string amount, string paymentMethod, string paymentName)
        {
            IdSeller = idSeller;
            IdBuyer = idBuyer;
            Date = date;
            StartTime = startTime;
            EndTime = endTime;
            PaymentNum = paymentNum;
            Amount = amount;
            PaymentMethod = paymentMethod;
            PaymentName = paymentName;
        }

        public int GetIdSeller()
        {
            return IdSeller;
        }
        public int GetIdBuyer()
        {
            return IdBuyer;
        }
        public string GetDate()
        {
            return Date;
        }
        
        public string GetEndTime()
        {
            return EndTime;
        }
        
        public string GetStartTime()
        {
            return StartTime;
        }

        public string GetPaymentNum()
        {
            return PaymentNum;
        }

        public string GetAmount()
        {
            return Amount;
        }

        public string GetPaymentMethod()
        {
            return PaymentMethod;
        }

        public string GetPaymentName()
        {
            return PaymentName;
        }
    }
}