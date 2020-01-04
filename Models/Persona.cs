using System;

namespace FamilyTree2.DATA
{
    public partial class Persona
    {
        public const string FEMALE_GENDER_STRING = "FEMALE";
        public const string FEMALE_GENDER_VALUE = "F";
        public const string MALE_GENDER_STRING = "MALE";
        public const string MALE_GENDER_VALUE = "M";

        public String fullName
        {
            get
            {
                return string.Format("{0}  {1}", this.FirstName, LastName);
            }
        }

        public String gender_desc
        {
            get
            {
                return this.Gender == FEMALE_GENDER_VALUE ? FEMALE_GENDER_STRING : MALE_GENDER_STRING;
            }
        }

        public String birthdayString
        {
            get
            {
                return this.BirthDay.ToString("MMMM dd yyyy");
            }
        }

        public String birthdayFormat
        {
            get
            {
                return this.BirthDay.ToString("yyyy-MM-dd");
            }
        }
    }

}