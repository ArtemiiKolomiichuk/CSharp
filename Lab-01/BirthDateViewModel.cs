using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;


namespace Lab_01
{
    internal class BirthDateViewModel : INotifyPropertyChanged
    {
        private enum WesternZodiacs
        {
            Aries,
            Taurus,
            Gemini,
            Cancer,
            Leo,
            Virgo,
            Libra,
            Scorpio,
            Sagittarius,
            Capricorn,
            Aquarius,
            Pisces
        }

        private enum ChineseZodiacs
        {
            Rat,
            Ox,
            Tiger,
            Rabbit,
            Dragon,
            Snake,
            Horse,
            Goat,
            Monkey,
            Rooster,
            Dog,
            Pig
        }

        private readonly UserBirthDateModel user;

        public BirthDateViewModel()
        {
            user = new UserBirthDateModel();
        }

        internal void ProcessBirthDate()
        {
            ChineseZodiac = ((ChineseZodiacs)((BirthDate.Year - 4) % 12)).ToString();
            if (BirthDate.Month == 1 && BirthDate.Day >= 20 || BirthDate.Month == 2 && BirthDate.Day <= 18)
                WesternZodiac = WesternZodiacs.Aquarius.ToString();
            else if (BirthDate.Month == 2 || BirthDate.Month == 3 && BirthDate.Day <= 20)
                WesternZodiac = WesternZodiacs.Pisces.ToString();
            else if (BirthDate.Month == 3 || BirthDate.Month == 4 && BirthDate.Day <= 19)
                WesternZodiac = WesternZodiacs.Aries.ToString();
            else if (BirthDate.Month == 4 || BirthDate.Month == 5 && BirthDate.Day <= 20)
                WesternZodiac = WesternZodiacs.Taurus.ToString();
            else if (BirthDate.Month == 5 || BirthDate.Month == 6 && BirthDate.Day <= 21)
                WesternZodiac = WesternZodiacs.Gemini.ToString();
            else if (BirthDate.Month == 6 || BirthDate.Month == 7 && BirthDate.Day <= 22)
                WesternZodiac = WesternZodiacs.Cancer.ToString();
            else if (BirthDate.Month == 7 || BirthDate.Month == 8 && BirthDate.Day <= 22)
                WesternZodiac = WesternZodiacs.Leo.ToString();
            else if (BirthDate.Month == 8 || BirthDate.Month == 9 && BirthDate.Day <= 22)
                WesternZodiac = WesternZodiacs.Virgo.ToString();
            else if (BirthDate.Month == 9 || BirthDate.Month == 10 && BirthDate.Day <= 23)
                WesternZodiac = WesternZodiacs.Libra.ToString();
            else if (BirthDate.Month == 10 || BirthDate.Month == 11 && BirthDate.Day <= 22)
                WesternZodiac = WesternZodiacs.Scorpio.ToString();
            else if (BirthDate.Month == 11 || BirthDate.Month == 12 && BirthDate.Day <= 21)
                WesternZodiac = WesternZodiacs.Sagittarius.ToString();
            else
                WesternZodiac = WesternZodiacs.Capricorn.ToString();

            if (BirthDate.Month == DateTime.Now.Month && BirthDate.Day == DateTime.Now.Day)
            {
                BirthdayMessage = "Happy Birthday!";
            }
            else
            {
                BirthdayMessage = "";
            }
        }

        public DateTime BirthDate
        {
            get { return user.birthDate; }
            set
            {
                var age = DateTime.Now.Year - value.Year - (DateTime.Now.DayOfYear < value.DayOfYear ? 1 : 0);
                if (age < 0 || age > 135)
                {
                    MessageBox.Show("Calculated age is not in the proper range. (Age must be in range [0,135])", "Age error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Age = "";
                    WesternZodiac = "";
                    ChineseZodiac = "";
                    BirthdayMessage = "";
                }
                else
                {
                    user.birthDate = value;
                    Age = age.ToString();
                    ProcessBirthDate();
                }
            }
        }

        public string Age
        {
            get { return $"Age: {user.age}"; }
            set
            {
                user.age = int.Parse(value);
                OnPropertyChanged(nameof(Age));
            }
        }

        public string WesternZodiac
        {
            get { return user.westernZodiac; }
            set
            {
                user.westernZodiac = $"Western zodiac sign: {value}";
                OnPropertyChanged(nameof(WesternZodiac));
            }
        }

        public string ChineseZodiac
        {
            get { return user.chineseZodiac; }
            set
            {
                user.chineseZodiac = $"Chinese zodiac sign: {value}";
                OnPropertyChanged(nameof(ChineseZodiac));
            }
        }

        private string _birthdayMessage = "";
        public string BirthdayMessage
        {
            get { return _birthdayMessage; }
            set
            {
                _birthdayMessage = value;
                OnPropertyChanged(nameof(BirthdayMessage));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
