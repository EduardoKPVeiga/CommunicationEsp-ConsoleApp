using System.Text;

namespace Verifications
{
    public static class Check
    {
        public static bool portCheck(string _port)
        {
            if (_port == "")
                return false;

            byte[] portBytes = Encoding.ASCII.GetBytes(_port);

            foreach (byte b in portBytes)
            {
                // it has only numbers
                if (b < 48 || b > 57)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool ipCheck(string _ip)
        {
            int dotQuantity = 0;
            string ip_aux = _ip;
            string[] ip_numbers = new string[4] { "", "", "", "" };

            // size test
            if (ip_aux.Length < 7 || ip_aux.Length > 15) { return false; }

            foreach (char c in ip_aux)
            {
                // it has only numbers and dots
                if ((c >= 48 && c < 58) || c == '.')
                {
                    if (c == '.')
                    {
                        dotQuantity++;
                    }

                    else
                    {
                        if (dotQuantity < 4)
                            ip_numbers[dotQuantity] += c;
                        else
                            return false;
                    }
                }
                else { return false; }
            }

            if (dotQuantity != 3) { return false; }

            for (int i = 0; i < 4; i++)
            {
                // too many digits in each part
                if (ip_numbers[i].Length > 3) { return false; }

                int ip_numberAux = 0;
                if (ip_numbers[i] != "")
                {
                    ip_numberAux = Convert.ToInt32(ip_numbers[i]);
                }

                // it has a part with 0 digits
                else { return false; }

                if (ip_numberAux < 0 || ip_numberAux > 255) { return false; }
            }

            return true;
        }
    }
}
