namespace PublicAPI.Dtos
{
    public class NumbClassDto
    {
        public int Number { get; set; }
        public bool IsPrime { get; set; }
        public bool IsPerfect { get; set; }
        public string[] Properties { get; set; }
        public int DigitSum { get; set; }
        public string FunFact { get; set; }
    }

    public class NumbClassErrorDto
    {
        public string? Number { get; set; }
        public bool Error { get; set; }
    }

    //           Required JSON Response Format(200 OK):
    //{
    //               "number": 371,
    //    "is_prime": false,
    //    "is_perfect": false,
    //    "properties": ["armstrong", "odd"],
    //    "digit_sum": 11,  // sum of its digits
    //    "fun_fact": "371 is an Armstrong number because 3^3 + 7^3 + 1^3 = 371" //gotten from the numbers API
    //}
    //           Required JSON Response Format(400 Bad Request)
    //{
    //               "number": "alphabet",
    //    "error": true
    //}
}
